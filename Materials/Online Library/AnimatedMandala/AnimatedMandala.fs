/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "n01se, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/ltyBz1 by n01se.  Inspired by and forked from https:\/\/www.shadertoy.com\/view\/4s3yDr",
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
        "Label": "Steps",
        "NAME": "steps",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Thickness",
        "NAME": "thickness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Flow/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Flow/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Flow/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Rotate/BPM Sync",
        "NAME": "mat_rot_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Reverse",
        "NAME": "mat_rot_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Speed",
        "NAME": "mat_rot_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Rotate/Offset",
        "NAME": "mat_rot_offset",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Rotate/Strob",
        "NAME": "mat_rot_strob",
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
    },
    {
        "NAME": "mat_rot_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_rot_speed",
            "speed_curve":2,
            "strob" : "mat_rot_strob",
            "reverse": "mat_rot_reverse",
            "bpm_sync": "mat_rot_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 40.;


#define TAU 2.0*PI

void amod(inout vec2 p, float c) {
    float m = TAU / c;
    float a = mod(atan(p.x, p.y)-m*.5, m) - m*.5;
    p = vec2(cos(a), sin(a)) * length(p);
}

void mo(inout vec2 p, vec2 d) {
    p = abs(p) - d;
    if(p.y>p.x)p=p.yx;
}

mat2 r2d(float a) {
    float c = cos(a), s = sin(a);
    return mat2(c, s, -s, c);
}

float smooth_stairs(float x) {
    return tanh(5.*(x-floor(x)-0.5))/tanh(2.5)*0.5+floor(x)+0.5;
}

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec4 materialColorForPixel(vec2 texCoord) {

    float rot_time = mat_rot_time - mat_rot_offset;

    vec2 uv = rotate2D(texCoord,PI*rot_time) - vec2(0.5);
    uv *= mat_zoom * 3.0;

    //amod(uv, 5.5+sin(iTime*0.2)*2.5);
    float nrays = abs(mod(iTime*0.3, 10.)-5.);
    amod(uv, steps*8.-smooth_stairs(nrays));

    mo(uv, vec2(1.2+sin(iTime*0.3), 0.6+sin(iTime*0.5)*0.3));
    uv *= r2d(PI/12.-PI/8.*mod(iTime*0.2, 16.));
    mo(uv, vec2(1.1+sin(iTime*0.5)*0.7, 0.4+0.5*1.5));
    uv *= r2d(PI/6.-mod(iTime*0.25, 12.0)*PI/6.0);
    mo(uv, vec2(.7+sin(iTime*0.45)*0.2, .2));

    //uv *= 10.;
    uv *= 30.;
    float l = min(abs(uv.x), abs(uv.y));
    //float l = max(abs(uv.x), abs(uv.y));
    //float l = abs(uv.x) + abs(uv.y);
    float d = sin(l) - .3 + thickness - 1.;

    d = smoothstep(0., fwidth(d), d);
    return vec4(sqrt(d));




}