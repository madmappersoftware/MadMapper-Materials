/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "TAGS": "graphic",
    "DESCRIPTION": "Cubic Circles.",
    "VSN": "1.0",
    "INPUTS": [  
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.00 },  
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN": 1.0, "MAX": 20.0, "DEFAULT": 16.0 },
        { "LABEL": "Glow", "NAME": "glowPower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Shape 1", "NAME": "shape_1", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Noise Scale", "NAME": "noiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Glow Color", "NAME": "glowColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 5.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        {"NAME": "anim_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"}
    ]
}*/

#define NOISE_TEXTURE_BASED
#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{

    // first grid
    vec2 posId;
    vec2 pos = ( texCoord + vec2(0.5)  ) * 1.0f;
    vec3 seed;

    pos = repeat(pos,vec2(1/iterations),posId);
    pos = rotate(pos,rotation);
    seed = vec3(posId.xy*noiseScale*0.1,anim_time);

    float noise_1 = ridgedNoise(seed);
    float noise_2 = fBm(seed) *0.05;
    float noise_3 =  vnoise(seed*vec3(vec2(1.5),1))*0.1;
    float noise_4 =  noise(seed*2.0);

    float rec = rectangle(pos ,  vec2(noise_1*0.04) );
    float rec2 = rectangle(pos,shape_1*vec2(rotation,noise_2) );
    float rec3 = circle(pos, noise_4*0.3 );

    float finalShape = subtract(rec,rec2);
    finalShape = subtract(finalShape,rec3);

    vec3 color = fill( backgroundColor.rgb, fillColor.rgb, finalShape );
    color += glow(glowColor.rgb,finalShape,glowPower*0.1);

    // Apply contrast
    color = mix(vec3(0.5), saturate(color), contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
