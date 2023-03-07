/*{
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "Line Repeat - animated with a noise.",
    "VSN": "1.0",
    "TAGS": "line,graphic",
    "INPUTS": [
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false },
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0, "MAX" : 4.0, "DEFAULT": 0.55 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN" : 1.0, "MAX" : 50.0, "DEFAULT": 6 },
        { "LABEL": "Thickness", "NAME": "thickness", "TYPE": "float", "DEFAULT": 0.02, "MIN": 0.001, "MAX": 1.0 }
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "bpm_sync": "bpmsync", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"}
    ]
}*/

#define SDF_ANTIALIASING_NONE
#define NOISE_TEXTURE_BASED

#include "MadSDF.glsl"
#include "MadNoise.glsl"
#include "MadCommon.glsl"

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 sc = vec2(0.0);
    vec2 uv = vec2(0.5) + (texCoord - vec2(0.5)) * (iterations*0.1);
    float radius1 = 0.1 / 8.0;
    float radius2 = 0.1 / 8.0;
    vec2 center1 = repeat(texCoord*sc,vec2(1.0/8.0));
    vec2 center2 = repeat(texCoord*sc,vec2(1.0/8.0));
    vec2 posId;

    float shapeMorph = (1+cos(animation_time*PI))/2;

    vec2 centerLines = repeat(uv,vec2(0.1),posId);
    vec2 noise_1 = dvnoise( vec3(posId,animation_time) ).xy;
    vec2 noise_2 = dWorleyNoise( vec3(posId,animation_time) ).xy;
    vec2 noise_3 = dBillowedNoise( vec3(posId*0.4,animation_time) ).xy;

    float dist_circle =  rectangle(center1+noise_2,0.1);
    float dist_rect = max(0,1-shapeMorph) * rectangle(center2+noise_3*0.1,radius2*noise_3.x);
    float dist_lines = rectangle( centerLines+noise_1*0.3 ,0.1);
    float dist = (dist_circle + dist_rect) - dist_lines;

    return vec4(stroke(vec3(0,0,0),vec3(1,1,1),dist,thickness *0.4), 1);
}
