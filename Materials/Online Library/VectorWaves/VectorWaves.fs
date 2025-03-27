/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "avin, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/WtGXRw by avin. Disable scrolling for more logical zooming.",
  "VSN": "1.1",
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
        "Label": "Scale Y",
        "NAME": "scale_y",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Rotate",
        "NAME": "mat_rotate",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Thickness",
        "NAME": "mat_thickness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Noise/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Noise/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Noise/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

     {
        "LABEL": "Scroll/Animate",
        "NAME": "mat_scroll",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/Match Rotation",
        "NAME": "mat_scroll_match_rotate",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/BPM Sync",
        "NAME": "mat_scroll_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Reverse",
        "NAME": "mat_scroll_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Speed",
        "NAME": "mat_scroll_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Angle",
        "NAME": "mat_scroll_angle",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "Label": "Scroll/Offset",
        "NAME": "mat_scroll_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Strob",
        "NAME": "mat_scroll_strob",
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
        "NAME": "mat_scroll_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_scroll_speed",
            "speed_curve":2,
            "strob" : "mat_scroll_strob",
            "reverse": "mat_scroll_reverse",
            "bpm_sync": "mat_scroll_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;



#define SIZE 50.
#define COL1 vec3(32, 43, 51) / 255.0
#define COL2 vec3(235, 241, 245) / 255.0

#define SF 1. / min(RENDERSIZE.x, RENDERSIZE.y) * SIZE
#define SS(l, s) smoothstep(SF, -SF, l - s)

#define MOD3 vec3(.1031, .11369, .13787)

vec3 hash33(vec3 p3)
{
    p3 = fract(p3 * MOD3);
    p3 += dot(p3, p3.yxz + 19.19);
    return -1.0 + 2.0 * fract(vec3((p3.x + p3.y) * p3.z, (p3.x + p3.z) * p3.y, (p3.y + p3.z) * p3.x));
}

float snoise(vec3 p)
{
    const float K1 = 0.333333333;
    const float K2 = 0.166666667;

    vec3 i = floor(p + (p.x + p.y + p.z) * K1);
    vec3 d0 = p - (i - (i.x + i.y + i.z) * K2);

    vec3 e = step(vec3(0.0), d0 - d0.yzx);
    vec3 i1 = e * (1.0 - e.zxy);
    vec3 i2 = 1.0 - e.zxy * (1.0 - e);

    vec3 d1 = d0 - (i1 - 1.0 * K2);
    vec3 d2 = d0 - (i2 - 2.0 * K2);
    vec3 d3 = d0 - (1.0 - 3.0 * K2);

    vec4 h = max(0.6 - vec4(dot(d0, d0), dot(d1, d1), dot(d2, d2), dot(d3, d3)), 0.0);
    vec4 n = h * h * h * h * vec4(dot(d0, hash33(i)), dot(d1, hash33(i + i1)), dot(d2, hash33(i + i2)), dot(d3, hash33(i + 1.0)));

    return dot(vec4(31.316), n);
}

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec4 materialColorForPixel(vec2 texCoord) {


    vec2 uv = rotate2D(texCoord,PI*mat_rotate) - vec2(0.5);

    float scroll_angle = mat_scroll_angle * 0.5;
    if (mat_scroll_match_rotate) {
        scroll_angle = mat_rotate;
    }
    // mat_scroll_angle += 0.5;

    float scroll_time = mat_scroll_time - mat_scroll_offset * 10.;
    if (!mat_scroll) {
        scroll_time = 0.;
    }
    float scroll_time_x = scroll_time * cos(PI * scroll_angle);
    float scroll_time_y = scroll_time * sin(PI * scroll_angle);
    uv.x -= scroll_time_x;
    uv.y -= scroll_time_y;

    // uv = rotate2D(uv,PI*mat_rotate);

    uv *= mat_zoom * 0.5;

    uv.y *= SIZE * scale_y;


    float yid = floor(uv.y);
    uv.y = fract(uv.y) - .5;
    float mask = 0.;
    for (float ofs = -1.; ofs <= 1.; ofs += 1.) {
        vec2 iuv = uv + vec2(0., ofs);
        float iid = yid - ofs;
        float fx = snoise(vec3(uv.x * 10. + iid * 100., iid, iTime));
        float fx2 = snoise(vec3(uv.x * 10. + (iid - 1.) * 100., (iid - 1.), iTime));
        float m = SS(abs(iuv.y + fx), .35 * mat_thickness);
        mask = max(mask, m * (fx2 + iuv.y + .5));
    }
    mask = smoothstep(0., 1., mask * .75);
    vec3 col = mix(COL1, COL2, mask);
    return vec4(col, 1.0);






}