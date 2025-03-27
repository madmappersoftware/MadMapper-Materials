/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Particles - sound reactive.",
    "VSN": "1.0",
    "TAGS": "particles,graphic,sound",
    "INPUTS": [ 
        { "NAME": "spectrum_16", "TYPE": "audioFFT", "SIZE": 16, "ATTACK": 0.0, "DECAY": 0.1, "RELEASE": 0.4 },
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Glow", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.1, "DEFAULT": 0.05 },
        { "LABEL": "Render Link", "NAME": "render_link", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define" },
        { "LABEL": "Audio/Bass Glow", "NAME": "bassStrokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 4, "DEFAULT": 0 },
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/ColorA", "NAME": "colorA", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },
        { "LABEL": "Color/ColorB", "NAME": "colorB", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadCommon.glsl"
#include "MadSDF.glsl"

#ifdef SURFACE_IS_surface3D
    // With Intel Iris Pro, we get a switch to CPU renderer if using 16 particles - too many uniforms ?
    #define NUM_PARTICLES 8
#else
    #define NUM_PARTICLES 16
#endif

flat in vec3 vParticles[NUM_PARTICLES];

vec3 renderParticle( vec2 p, vec2 position, float size, vec3 c, int id )
{
    float dist = circle( p, position, size * 0.1 );
    vec3 color = fill( vec3(0), saturate( c * 10.0 ), dist );   
    color += glow( c * 0.2 + vec3(0.5), dist, strokeWidth * 300.0 * size );
    color += glow( c * 0.2 + vec3(0.3), dist, strokeWidth * 1260.0 * size );
    color += glow( c * 0.2, dist, strokeWidth * 8260.0 * size );
    return color;
}

vec3 renderLink( vec2 p, vec2 a, vec2 b )
{
    float dist = line( p, a, b, 0.001 );
    vec2 diff = a - b;
    float alpha = saturate( 1.0 - ( diff.x * diff.x + diff.y * diff.y ) * 50.0 );
    vec3 color = fill( vec3(0), vec3(1), dist ) * 0.1 * alpha;
    color += glow( vec3(1), dist, strokeWidth ) * 0.1 * alpha;
    return color;
}

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p      = translate( texCoord, vec2( 0.5 ) );
    float size  = 0.02;
    vec3 color  = backgroundColor.rgb;
    for( int i = 0; i < NUM_PARTICLES; i++ ) {
        color += renderParticle( p, 
                    vParticles[i].xy, 
                    0.075 * vParticles[i].z * scale, 
                    mix( colorA.rgb, colorB.rgb, float(i) / float( NUM_PARTICLES ) ),
                    i );
    }

    #ifdef render_link_IS_true
        for( int i = 1; i < NUM_PARTICLES - 1; i++ ) {
            color += renderLink( p, vParticles[i].xy, vParticles[i-1].xy );
            color += renderLink( p, vParticles[i].xy, vParticles[i+1].xy );
            color += renderLink( p, vParticles[i-1].xy, vParticles[i+1].xy );
        }
    #endif

    color = saturate( color );

     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}

