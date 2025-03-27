/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Fluid Noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [ 
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.00001, "MAX": 10.0, "DEFAULT": 2 },  
        { "LABEL": "Offset", "NAME": "offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },  
        { "LABEL": "Level/Base Level", "NAME": "noise_strength", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },  
        { "LABEL": "Level/Audio Level", "NAME": "audio_strength", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },  
        { "LABEL": "NoiseAnim/Noise Speed", "NAME": "noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.4 }, 
        { "LABEL": "NoiseAnim/Reverse", "NAME": "noise_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "ColorAnim/Color Speed", "NAME": "color_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0 }, 
        { "LABEL": "ColorAnim/Reverse", "NAME": "color_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "NAME": "spectrum_512", "TYPE": "audioFFT", "SIZE": 512, "ATTACK": 0.2, "DECAY": 0.0, "RELEASE": 0.6 },
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {"NAME": "noise_time", "TYPE": "time_base", "PARAMS": {"speed": "noise_speed", "reverse": "noise_reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "color_time", "TYPE": "time_base", "PARAMS": {"speed": "color_speed", "reverse": "color_reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Simplex Noise
    vec3 uv = vec3( ( texCoord - vec2( 0.5 ) ) * scale, noise_time );

    float n = fBm( uv ) + offset + color_time;

    float value = audio_strength * texture( spectrum_512, vec2( n, 0.5 ) ).r;

    float noise_value = vnoise( vec2(20 * n, 0) );
    value += noise_strength * noise_value * noise_value;

    // Interpolate Color
    vec3 color = mix( backgroundColor.rgb, foregroundColor.rgb, saturate( value ) );

    // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
