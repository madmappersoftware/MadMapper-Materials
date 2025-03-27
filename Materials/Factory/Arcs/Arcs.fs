/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Radius", "NAME": "base_radius", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Radius Speed", "NAME": "speed0", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Rotate Speed", "NAME": "speed1",  "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Glow Width", "NAME": "glowWidth", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Audio/Audio React.", "NAME": "audio_reactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "generate_as_define" },
        { "LABEL": "Audio/Radius", "NAME": "radius_anim", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
        { "LABEL": "Audio/Width", "NAME": "width_anim", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
        { "LABEL": "Inverted", "NAME": "INVERTED", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define" },
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Glow", "NAME": "glowColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
        { "NAME": "spectrum_16", "TYPE": "audioFFT", "SIZE": 16, "ATTACK": 0.0, "DECAY": 0.1, "RELEASE": 0.4 }
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "speed0", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "speed1", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadCommon.glsl"
#include "MadSDF.glsl"

// With Intel Iris Pro, we get a switch to CPU renderer if using 16 particles - reaching uniform maximum number if using on a Surface 3D for instance
#define NUM_ARCS 8

flat in vec3 vNoises[NUM_ARCS];

vec4 materialColorForPixel( vec2 texCoord )
{
    // render arcs
    vec2 p = translate( texCoord, vec2( 0.5 ) );
    float dist = 1.0;
    float animA, animB;
    for( int i = 0; i < NUM_ARCS; ++i ) {
        #ifdef audio_reactive_IS_true
            const float invFftSize = 1.0f / float( 16.0f );
            animA = radius_anim * texture( spectrum_16, vec2( float(i) * invFftSize, 0.0 ) ).r;
            animB = width_anim * texture( spectrum_16, vec2( float(16-i) * invFftSize, 0.0 ) ).r;
        #else
            animA = radius_anim;
            animB = width_anim;
        #endif
        #if defined( INVERTED_IS_false )
            float radius = base_radius * (0.35 - ( animA * float(i) * 0.05 * vNoises[i].y + vNoises[i].x * animA )); // radius
        #else
            float radius = base_radius * 2 * ( animA * float(i) * 0.05 * vNoises[i].y + vNoises[i].x * animA ); // radius
        #endif
        dist = blend( dist, arc( rotate( p, PI * cos( 15.0 * vNoises[i].x ) ), 
            radius,
            0.1 + vNoises[i].x * 5.0,                         // angle
            animB * animB * vNoises[i].z * vNoises[i].z * 0.2 + 0.001 ) );       // width
    }
    
    // colors
    vec4 col = fill( backgroundColor, fillColor, dist );
    col += glow( glowColor, dist, 0.0125 * glowWidth ) * 0.5;
    col += glow( glowColor, dist, 0.05 * glowWidth ) * 0.4;
    col += glow( glowColor, dist, 0.35 * glowWidth ) * 0.1;
    col += glow( vec4(1), dist, 0.5 * glowWidth ) * 0.2;
    col += glow( glowColor, dist, 0.75 * glowWidth ) * 0.1;
    
    // Apply contrast
    col.rgb = mix(vec3(0.5), col.rgb, contrast);

    // Apply brightness
    col.rgb += vec3(brightness);

    return saturate( col );
}
