/*{
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "Blobby",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [  
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.5, "MAX": 2.0, "DEFAULT": 0.5 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },
        { "LABEL": "Color/Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 5.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 3, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"}
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM
#define NOISE_TEXTURE_BASED

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // spiral sdf
    vec2 posId;
    vec2 uv = texCoord*scale;
    vec2 p = translate( uv, vec2( 0.5 )+noise(uv+100*worleyNoise(animation_time*vec2(0.001,0.003))));

    float dist = blend( 1.0, rectangle( p, 0.5 ) );

    // coloring
    vec3 color = fill( backgroundColor.rgb, fillColor.rgb, dist );
    color = stroke( color, strokeColor.rgb, dist, .1f );

    // Apply contrast
    color = mix(vec3(0.5), saturate(color), contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
