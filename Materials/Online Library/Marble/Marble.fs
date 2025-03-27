/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Stefan Gustavson, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https://github.com/ashima/webgl-noise",
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
        "LABEL": "Center",
        "NAME": "_center",
        "TYPE": "point2D",
        "DEFAULT": [
            0.5,
            0.5
        ],
        "MIN": [0.0, 0.0],
        "MAX": [1.0, 1.0]
    },




    {
        "Label": "Shape/Stripe",
        "NAME" : "_stripe",
        "TYPE" : "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "Label": "Shape/Scale",
        "NAME": "mat_shape_scale",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Shape/Scale X",
        "NAME": "mat_shape_scale_x",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Shape/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Shape/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Shape/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.7
    },
    {
        "Label": "Shape/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Shape/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Noise/Shape",
        "NAME": "_shape",
        "TYPE": "point2D",
        "DEFAULT": [
            3.0,
            2.0
        ],
        "MIN": [0.0, 0.0],
        "MAX": [5.0, 5.0]
    },

    {
        "LABEL": "Noise/BPM Sync",
        "NAME": "mat_noise_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Reverse",
        "NAME": "mat_noise_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Speed",
        "NAME": "mat_noise_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.7
    },

    {
        "Label": "Noise/Offset",
        "NAME": "mat_noise_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Noise/Strob",
        "NAME": "mat_noise_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Color/Color",
        "NAME": "_colormap",
        "TYPE": "point2D",
        "DEFAULT": [
            1.0,
            0.0
        ],
        "MIN": [0.0, 0.0],
        "MAX": [3.0, 3.0]
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
        "NAME": "mat_noise_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_noise_speed",
            "speed_curve":2,
            "strob" : "mat_noise_strob",
            "reverse": "mat_noise_reverse",
            "bpm_sync": "mat_noise_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

//
// GLSL textureless classic 2D noise "cnoise",
// with an RSL-style periodic variant "pnoise".
// Author:  Stefan Gustavson (stefan.gustavson@liu.se)
// Version: 2011-08-22
//
// Many thanks to Ian McEwan of Ashima Arts for the
// ideas for permutation and gradient selection.
//
// Copyright (c) 2011 Stefan Gustavson. All rights reserved.
// Distributed under the MIT license. See LICENSE file.
// https://github.com/ashima/webgl-noise
//

vec4 mod289(vec4 x)
{
  return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec4 permute(vec4 x)
{
  return mod289(((x*34.0)+1.0)*x);
}

vec4 taylorInvSqrt(vec4 r)
{
  return 1.79284291400159 - 0.85373472095314 * r;
}

vec2 fade(vec2 t) {
  return t*t*t*(t*(t*6.0-15.0)+10.0);
}

// Classic Perlin noise
float noise(vec2 P)
{
  vec4 Pi = floor(P.xyxy) + vec4(0.0, 0.0, 1.0, 1.0);
  vec4 Pf = fract(P.xyxy) - vec4(0.0, 0.0, 1.0, 1.0);
  Pi = mod289(Pi); // To avoid truncation effects in permutation
  vec4 ix = Pi.xzxz;
  vec4 iy = Pi.yyww;
  vec4 fx = Pf.xzxz;
  vec4 fy = Pf.yyww;

  vec4 i = permute(permute(ix) + iy);

  vec4 gx = fract(i / 42.) * 2.0 - 1.0 ;
  vec4 gy = abs(gx) - 0.5 ;
  vec4 tx = floor(gx + 0.5);
  gx = gx - tx;

  vec2 g00 = vec2(gx.x,gy.x);
  vec2 g10 = vec2(gx.y,gy.y);
  vec2 g01 = vec2(gx.z,gy.z);
  vec2 g11 = vec2(gx.w,gy.w);

  vec4 norm = taylorInvSqrt(vec4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
  g00 *= norm.x;
  g01 *= norm.y;
  g10 *= norm.z;
  g11 *= norm.w;

  float n00 = dot(g00, vec2(fx.x, fy.x));
  float n10 = dot(g10, vec2(fx.y, fy.y));
  float n01 = dot(g01, vec2(fx.z, fy.z));
  float n11 = dot(g11, vec2(fx.w, fy.w));

  vec2 fade_xy = fade(Pf.xy);
  vec2 n_x = mix(vec2(n00, n01), vec2(n10, n11), fade_xy.x);
  float n_xy = mix(n_x.x, n_x.y, fade_xy.y);
  return 2.3 * n_xy;
}
///
//
// END NOISE SNIPPET
//
//


float fbm(vec2 p) {
    const mat2 m = mat2( 0.80,  0.60, -0.60,  0.80 );
    p *= 3.;
    float f = 0.0;
    f += 0.500000*(0.5+0.5*noise( p )); p = m*p*2.02;
    f += 0.250000*(0.5+0.5*noise( p )); p = m*p*2.03;
    f += 0.125000*(0.5+0.5*noise( p )); p = m*p*2.01;
    f += 0.062500*(0.5+0.5*noise( p )); p = m*p*2.04;
    f += 0.031250*(0.5+0.5*noise( p )); p = m*p*2.01;
    f += 0.015625*(0.5+0.5*noise( p ));
    return f/0.96875;

}

vec3   iResolution = vec3(RENDERSIZE, 1.0);
float  iGlobalTime = TIME;


const int MAX_ITER = 20;


mat3 RotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;

    return mat3(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c);
}

vec2 rotate(in vec2 v, in float a) {
    return vec2(cos(a)*v.x + sin(a)*v.y, -sin(a)*v.x + cos(a)*v.y);
}

float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    t = t * _colormap.x + _colormap.y;
    return a + b*cos( 6.28318*(c*t+d) );
}


vec3 palette(float f ) {
    //return pal(f, vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.0,1.0,1.0),vec3(0.0,0.25,0.25) );
    return pal(f, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.10,0.20) );
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 center = _center;
    center.y = 1. - center.y;
    vec2 uv = texCoord - center;

    uv *= mat_zoom;

    vec3 cameraDir = vec3(1., 0., 0.);
    vec3 cameraOrigin = vec3(-3., 0., 0.);

    // vec2 dir = normalize(uv - 0.5) * 0.1;
    vec2 dir = normalize(uv) * 0.1;

    float shape_time = mat_time - mat_offset * 10;
    float noise_time = mat_noise_time - mat_noise_offset * 10;

    float _animation = -1.* shape_time * 0.2;
    float _bg_animation = noise_time * 0.5;

    // vec2 r = abs(uv - 0.5);
    vec2 r = abs(uv);
    r *= mat_shape_scale;
    r.x *= mat_shape_scale_x;

    float manhattan = _stripe? r.x - r.y : r.x + r.y;
    manhattan = fract(manhattan * 2. + _animation);

    vec3 color = vec3(palette(fbm(uv - _bg_animation - _shape.y  * fbm(uv + _bg_animation + _shape.x  * fbm(uv)))));

    if (manhattan < 0.5) {
        color = 1.0 - color;
    }

    // color *= manhattan;


    return vec4(color, 1.0);




}