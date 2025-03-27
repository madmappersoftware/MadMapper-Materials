/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "Square Wave",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN": 1.0, "MAX": 100.0, "DEFAULT": 50.0 },
        { "LABEL": "Glow", "NAME": "glowPower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Shape_1", "NAME": "shape_1", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape_2", "NAME": "shape_2", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape_3", "NAME": "shape_3", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Noise Scale", "NAME": "noiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Glow Color", "NAME": "glowColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
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
    vec2 pos = ( texCoord  ) ;
    vec3 seed;

    pos = repeat(pos, vec2( 1.0 / iterations) ,posId);
    seed = vec3(posId.xy*noiseScale*0.2,animation_time);

    float noise_1 = ridgedNoise(seed);
    float noise_2 = fBm(seed) *0.05;
    float noise_3 =  vnoise(seed*vec3(vec2(1.5),1))*0.02;
    float noise_4 =  noise(seed*2.0);

    float rec = rectangle(pos ,  (1.0 / iterations)*noise_1*0.5 );
    float rec2 = rectangle(pos,shape_1*vec2(noise_2,0.1));
    float rec3 = rectangle(pos+ vec2(noise_3), shape_2*vec2(0.01,0.5) );
    float rec4 = rectangle(pos+ vec2(noise_4), shape_3*vec2(noise_4,0.422) );

    float finalShape = subtract(rec,rec2);
    finalShape = subtract(finalShape,rec3);
    finalShape = subtract(finalShape,rec4);

    vec4 color = fill( backgroundColor, fillColor, finalShape );
    color += glow(glowColor,finalShape,glowPower*0.1);

    return saturate( color );
}
