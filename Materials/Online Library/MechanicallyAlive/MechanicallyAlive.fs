/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/ttsGzX",
  "VSN": "1.0",
  "INPUTS" : [

    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Stretch",
        "NAME": "F1",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.4,
        "DEFAULT": 0.115
    },
    {
        "Label": "Glow",
        "NAME": "F2",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.75
    },
    {
        "Label": "Density",
        "NAME": "F3",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 3.0,
        "DEFAULT": 1.5
    },
    {
        "Label": "Complexity",
        "NAME": "complexity",
        "TYPE": "float",
        "MIN": 1.0,
        "MAX": 4.0,
        "DEFAULT": 2.5
    },
    {
        "Label": "Complex Step",
        "NAME": "comp_step",
        "TYPE": "float",
        "MIN": 0.1,
        "MAX": 1.0,
        "DEFAULT": 0.5
    },
    {
        "LABEL": "BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

#define PI2 PI*2.

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    float m = 0.;
    float stp = PI/5.;
    float odd = -1.;
    for(float n=1.; n<complexity; n+=comp_step){
        odd *= -1.;
        float t = iTime * odd * .3;
        for(float i=0.0001; i<PI2; i+=stp){

            vec2 uvi = uv*n + vec2(cos(i + n*stp*.5 + t)*.4, sin(i + n*stp*.5 + t)*.4);
            float l = length(uvi);
            m += smoothstep(F1 * n, .0, l)*F3;
        }
    }

    m = step(F2, fract(m*F3));

    vec3 col = vec3(1.) * m;

    return vec4(col,1.0);

}