/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#45351.1",
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
        "Label": "Depth",
        "NAME": "depth",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 2.0
    },
    {
        "Label": "Fog",
        "NAME": "fog_amount",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Glow",
        "NAME": "glow",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 2.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Move/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Move/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Move/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Move/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Move/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "Label": "Blur/Amount",
        "NAME": "mat_blur_amount",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.2
    },
    {
        "LABEL": "Blur/BPM Sync",
        "NAME": "mat_blur_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Blur/Reverse",
        "NAME": "mat_blur_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Blur/Speed",
        "NAME": "mat_blur_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Blur/Offset",
        "NAME": "mat_blur_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Blur/Strob",
        "NAME": "mat_blur_strob",
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
        "NAME": "mat_blur_time",
        "TYPE": "animator",
        "PARAMS": {
            "speed": "mat_blur_speed",
            "speed_curve":2,
            "strob" : "mat_blur_strob",
            "reverse": "mat_blur_reverse",
            "bpm_sync": "mat_blur_bpm_sync",
            "shape": "mat_blur_shape",
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

/*
 * Original shader from: https://www.shadertoy.com/view/4ljSDt
 */


float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float map(vec3 p)
{
    vec3 q = fract(p) * 2.0 - 1.0;
    //return length(q) - 0.1;
    return sdBox(q, vec3(0.25));
}

float trace(vec3 o, vec3 r)
{
    float t = 0.0;
    for (int i = 0; i < 32; ++i)
    {
        vec3 p = o + r * t;
        float d = map(p);
        t += d * 0.5;
    }
    return t;
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom * 2.;

    float blur_time = mat_blur_time - mat_blur_offset * 10.;

    // float depth = 2.0;
    vec3 r = normalize(vec3(uv, depth));
    float the = iTime * 0.25;  // Parameter for rotation

    r.xz *= mat2(cos(the), -sin(the), sin(the), cos(the));  // Rotation matrix for 3d space
    vec3 o = vec3(0.0, 0.25 * iTime, -0.5 * iTime);  // Movement in 3d space

    float st = (sin(blur_time) + 2.5) * 0.4 * (1. - mat_blur_amount); // blur in and out

    float t = trace(o, r * st);

    float fog = fog_amount / (1.0 + t * t * 0.1);

    vec3 fc = vec3(fog * 2.0 * glow);  // glow intensity


    vec3 tint = vec3(st * 0.5,st,st + 0.0); // glow color
    return vec4(fc * tint, 1.0);


}