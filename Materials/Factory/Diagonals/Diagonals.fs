/*{
    "CREDIT": "1024 architecture",
    "TAGS": "graphic",
    "DESCRIPTION": "Diagonals.",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Rotation", "NAME": "base_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN": 1.0, "MAX": 20.0, "DEFAULT": 20.0 },
        { "LABEL": "Shape 1", "NAME": "shape_1", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape 2", "NAME": "shape_2", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Noise Scale", "NAME": "noiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
        { "LABEL": "Rotate/Auto Rotate", "NAME": "autorotateactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Rotate/Speed", "NAME": "autorotatespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 4.0 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 5.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "autorotatespeed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"}
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM
#define NOISE_TEXTURE_BASED

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // first grid
    vec2 posId;
    vec2 pos = ( texCoord + vec2(animation_time*0.1,0) ) * 1.0f;
    vec3 seed;

    pos = repeat(pos, vec2( 1.0 / iterations) ,posId);
    seed = vec3(posId.xy*noiseScale*0.2,animation_time);

    float noise_1 = ridgedNoise(seed)*0.2;
    float noise_2 = fBm(seed) *0.05;

    float rotation_value = base_rotation;
    if (autorotateactive) rotation_value += rotation_time;

    vec2 rpos = rotate(pos,rotation_value);
    float line1 = line(rpos , vec2(-1,noise_1),vec2(1,-noise_1), noise_2*shape_1 );
    float line2 = line(rpos , vec2(noise_1,-1),vec2(-noise_1,1), noise_2*shape_2 );
    float lines = blend(line1,line2);

    vec3 color = fill( backgroundColor.rgb, fillColor.rgb, lines );

    // Apply contrast
    color = mix(vec3(0.5), saturate(color), contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
