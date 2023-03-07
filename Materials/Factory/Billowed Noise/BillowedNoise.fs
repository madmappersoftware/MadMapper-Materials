/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Billowed Noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.00001, "MAX": 20.0, "DEFAULT": 2.0 },  
        { "LABEL": "Noise Speed", "NAME": "zspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Speed X", "NAME": "xspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Speed Y", "NAME": "yspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -0.5 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 2 },
        { "LABEL": "Color/Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": true },  
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed", "speed_curve": 4, "reverse": "reverse", "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv     = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * scale * scale + vec2( xtime, ytime ), ztime );

    // Simplex Noise
    float n     = billowedNoise( uv );

    if (invert) n = 1 - n;

    // Interpolate Color
    vec3 color  = mix( backgroundColor.rgb, foregroundColor.rgb, n );
 
     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
