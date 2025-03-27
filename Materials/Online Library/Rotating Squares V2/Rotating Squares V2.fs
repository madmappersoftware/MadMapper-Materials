/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers, Modified by Simon Holden - Removed forced red, and added multiplier. ",
  "VSN": "1.0",
  "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
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
            "Label": "Multiplier",
            "NAME": "mat_rot",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 10.0,
            "DEFAULT": 0.0
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
    ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#42183.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable

float iTime = mat_time - mat_offset * 10;

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 p = texCoord - vec2(0.5);

    p *= mat_zoom;

    float a = iTime * mat_rot;
    mat2 rot = mat2(cos(a), sin(a), -sin(a), cos(a));
    p = rot * p;

    vec3 col = vec3(0);

    float v = fract(-1.5*iTime+abs(p.x+p.y)*5.0+abs(p.x-p.y)*5.0);
    float f = smoothstep(0.0, 0.7, v) - smoothstep(0.8, 0.9, v);

    f = f / (1. + length(p));

    //f = step(0.8, v);
    col = f * vec3(1,1,1);
    return vec4(col, 1.0);

}