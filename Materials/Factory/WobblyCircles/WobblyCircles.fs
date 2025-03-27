/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "Wobbly Circles",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 },
        { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 0.00 },
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN": 1.0, "MAX": 20.0, "DEFAULT": 5.0 },
        { "LABEL": "Glow", "NAME": "glowPower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
        { "LABEL": "Shape_1", "NAME": "shape_1", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape_2", "NAME": "shape_2", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape_3", "NAME": "shape_3", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Noise Scale", "NAME": "noiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Wobble", "NAME": "wobble", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Glow Color", "NAME": "glowColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 5.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
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
    vec2 uv = texCoord*scale;
    vec2 pos = ( (uv+vec2(1)*wobble)*(vec2(1.0)+wobble*worleyNoise(uv*5.2+vec2(animation_time)))  ) ;

    vec3 seed;

    pos = repeat(pos, vec2( 1.0 / iterations) ,posId);
    seed = vec3(posId.xy*noiseScale*0.2,animation_time);

    float noise_1 = (ridgedNoise(seed)+1.0)*0.015;
    float noise_2 = fBm(seed) *0.05;
    float noise_3 =  vnoise(seed*vec3(vec2(1.5),1))*0.02;
    float noise_4 =  vnoise(seed*2.0)*0.1;
    float noise_5 =  worleyNoise(seed.zyx);

    vec2 rpos = rotate(pos,rotation*noise_5);
    float line1 = line(rpos , vec2(-1,noise_1),vec2(1,noise_1), noise_3*shape_1 );
    float line2 = line(pos , vec2(noise_1,-1),vec2(noise_1,1), noise_2*shape_2 );
    float line3 = line(pos +vec2(noise_4), vec2(0,-1),vec2(0,1), noise_2*shape_3 );
    float rec = rectangle(rpos *vec2(noise_5)*4.0, vec2(0.006), noise_2*shape_3 );
    float lines = blend(line1,line2);
    lines = blend(lines,line3);
    lines = blend(lines,rec);

    vec3 color = fill( backgroundColor.rgb, fillColor.rgb, lines);
    color += glow(glowColor.rgb,lines,glowPower*0.1);

    // Apply contrast
    color = mix(vec3(0.5), saturate(color), contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
