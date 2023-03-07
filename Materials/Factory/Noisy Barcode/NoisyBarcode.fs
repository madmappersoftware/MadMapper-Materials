/*{
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Noisy barcode.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [ 
        { "Label": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.00001, "MAX": 60.0, "DEFAULT": 20.0 },  
        { "Label": "Curve", "NAME": "mat_curve", "TYPE": "float", "MIN": -10.0, "MAX": 10.0, "DEFAULT": 0.0 },          
        { "Label": "Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0, "MAX": 360.0, "DEFAULT": 0.0 },  
        { "Label": "Noise/Noise Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.7 },
        { "Label": "Noise/Reverse", "NAME": "mat_noise_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "Label": "Rotate/Rotate", "NAME": "mat_rotation_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define,button" },  
        { "Label": "Rotate/Speed", "NAME": "mat_rotation_speed", "TYPE": "float", "MIN": 0, "MAX": 4, "DEFAULT": 1.0 },  
        { "Label": "Rotate/Reverse", "NAME": "mat_rotation_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "Label": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "reverse": "mat_noise_reverse", "speed_curve": 3, "link_speed_to_global_bpm":true}},
        {"NAME": "mat_rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_rotation_speed", "reverse": "mat_rotation_reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 uv     = ( texCoord - vec2( 0.5 ) );
    float r     = length( uv ) * mat_curve;
    r += mat_rotation * PI / 180;
    if (mat_rotation_active) {
        r += mat_rotation_time;
    }
    uv.x        = ( uv.x * cos( r ) + uv.y * sin( r ) );
    uv.y        = 0;    

    float n     = fBm( vec3( uv.xy * mat_scale, mat_noise_time ) ) * 2.0;

    // Interpolate Color
    vec3 color  = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, vec3( n ) );
 
     // Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);

    // Apply brightness
    color += vec3(mat_brightness);

    return vec4( color, 1.0f );
}
