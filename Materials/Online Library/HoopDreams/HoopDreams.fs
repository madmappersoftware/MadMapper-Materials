/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Original by Joseph Fiola, adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [

        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 20.0,
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
            "Label": "Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": -3.14159265358979323846,
            "MAX": 3.14159265358979323846,
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

        {
            "NAME": "invert",
            "TYPE": "bool",
            "DEFAULT": 0.0
        },


        {
            "NAME": "size",
            "TYPE": "float",
            "DEFAULT": 0.35,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "thickness",
            "TYPE": "float",
            "DEFAULT": 0.001,
            "MIN": 0.001,
            "MAX": 0.5
        },
        {
            "NAME": "lineEffect",
            "TYPE": "float",
            "DEFAULT": 0.001,
            "MIN": 0.0,
            "MAX": 0.2
        },
        {
            "NAME": "patternOffset",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": -1.0,
            "MAX": 1.0
        },
        {
            "NAME": "rSin",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": -5.0,
            "MAX": 5.0
        },
        {
            "NAME": "xCos",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": -5.0,
            "MAX": 5.0
        },
        {
            "NAME": "ySin",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": -5.0,
            "MAX": 5.0
        },
        {
            "NAME": "blur",
            "TYPE": "float",
            "DEFAULT": 0.005,
            "MIN": 0.001,
            "MAX": 0.5
        },
        {
            "NAME": "function",
            "TYPE": "long",
            "VALUES": [
                0,
                1
            ],
            "LABELS": [
                "abs",
                "fract"
            ],
            "DEFAULT": 0
        },
        {
            "NAME": "rotate",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": -1.0,
            "MAX": 1.0
        },
        {
            "NAME": "pos",
            "TYPE": "point2D",
            "DEFAULT": [
                0.5,
                0.5
            ],
            "MIN": [
                0.0,
                0.0
            ],
            "MAX": [
                1.0,
                1.0
            ]
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
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#48338.1"
}
*/


#include "MadCommon.glsl"
#include "MadNoise.glsl"

const int   NUM_CIRCLES     = 50;

// #define PI 3.14159265358979323846
#define TWO_PI PI*2

//#extension GL_OES_standard_derivatives : enable
float iTime = mat_time - mat_offset;


vec3 drawCircle(vec2 p, vec2 center, float radius, float edgeWidth, vec3 color)
{
    float dist = length(p - center);
    vec3 ret;

    float look;
    if (function == 0) look = abs(dist -size);
    else if (function == 1) look = fract(dist -size);

    ret = color * (1.0 - lineEffect - smoothstep(radius, (radius+edgeWidth),  look  ));

    return ret;
}

vec3 invertColor(vec3 color) {
    return vec3(color *-1.0 + 1.0);
}

//rotation function
vec2 rot(vec2 uv,float a){
    return vec2(uv.x*cos(a)-uv.y*sin(a),uv.y*cos(a)+uv.x*sin(a));
}



vec4 materialColorForPixel(vec2 texCoord) {

        vec2 uv = texCoord;
        uv -= vec2(pos);
        // uv.x*=RENDERSIZE.x/RENDERSIZE.y;
        uv *= mat_zoom;

        uv=rot(uv,rotate * PI);


        vec3 color = vec3(0.0);
        float angleIncrement = TWO_PI / float(NUM_CIRCLES);


        for (int i = 0; i < NUM_CIRCLES; ++i) {
            float t = angleIncrement*(float(i));
            float r = sin(rSin * t + iTime);   // In VDMX I use the following line and control the "animate" slider
            //float r = sin(rSin * t + animate);
            vec2 p = vec2(r*cos(t*xCos), r*sin(t*ySin));

            uv=rot(uv,patternOffset * PI);

            if (lineEffect >= 0.2) color = invertColor(color);

            color += drawCircle(uv, p, thickness, blur, vec3(1.0));
        }

        if (invert) color = invertColor(color);

        return vec4(color,1.0);

}