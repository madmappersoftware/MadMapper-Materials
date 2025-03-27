/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Catzpaw, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#39775.0",
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
        "Label": "Iterations",
        "NAME": "ITER",
        "TYPE": "int",
        "MIN": 20,
        "MAX": 150,
        "DEFAULT": 96
    },

    {
        "Label": "EPS",
        "NAME": "EPS",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.1,
        "DEFAULT": 0.01
    },

    {
        "Label": "Near",
        "NAME": "NEAR",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 3.0,
        "DEFAULT": 1.0
    },

    {
        "Label": "Far",
        "NAME": "FAR",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 100.0,
        "DEFAULT": 40.0
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
        "DEFAULT": 0.5
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

//--- marine snow
// by Catzpaw 2017

float iTime = mat_time - mat_offset * 10;

// #define ITER 96
// #define EPS 0.01
// #define NEAR 1.
// #define FAR 40.

float map(vec3 p){vec3 p2=floor((p+.25)*.5);p=mod(p*4.+1.,2.)-1.;
    float v=fract(sin(p2.x*133.3)*19.9+sin(p2.y*177.7)*13.3+sin(p2.z*199.9)*17.7);
    if(v<.9)return .8;return length(p)-4.4+v*4.;}

float trace(vec3 ro,vec3 rd){float t=NEAR,d;
    for(int i=0;i<ITER;i++){d=map(ro+rd*t);if(abs(d)<EPS||t>FAR)break;t+=step(d,1.)*d*.1+d*.3;}
    return min(t,FAR);}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    float si=sin(iTime*.1),co=cos(iTime*.1);uv*=mat2(si,-co,co,si);
    float v=1.-trace(vec3(0,iTime*7.,-iTime*10.),vec3(uv,-.8))/FAR;
    return vec4(vec3(1.8,1.6,1.3)*v,1);

    // return vec4(color*v);




}