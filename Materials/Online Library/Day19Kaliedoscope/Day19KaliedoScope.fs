/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "IMPORTED" : [
    {
      "NAME" : "iChannel1",
      "PATH" : "8de3a3924cb95bd0e95a443fff0326c869f9d4979cd1d5b6e94e2a01f5be53e9.jpg"
    },
    {
      "NAME" : "iChannel0",
      "PATH" : "08b42b43ae9d3c0605da11d0eac86618ea888e62cdd9518ee8b9097488b31560.png"
    }
  ],

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
],
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/3sXGWj by kiyoshidainagon.  ref: shadertoy.com\/view\/4sfGzs"
}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

const float DEVISION_NUM =  7.0;
const float REDUCTION_VALUE = 0.4;

const vec3 BASE_COLOR = vec3(0.2, 0.8, 0.5);

vec2 kaleido(vec2 uv) {
    float th = atan(uv.y, uv.x);
    float r = pow(length(uv), 0.5);
    float f = 2.0 * PI / DEVISION_NUM / 2.0;

    th = abs(mod(th + f / 4.0, f)  - f / 2.0) / ( 1.0 + r);
    return vec2(cos(th), sin(th)) * r * REDUCTION_VALUE;
}

vec2 transform(vec2 at) {
    vec2 v;
    float th = 0.2 * iTime;
    v.x = at.x * cos(th) - at.y * sin(th) - 0.2 * sin(th);
    v.y = at.x * sin(th) + at.y * cos(th) + 0.2 * cos(th);
    return v;
}

vec3 scene(vec2 at) {
    return IMG_NORM_PIXEL(iChannel0,mod(at * 1.0,1.0)).xyz;

}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    // uv *= mat_zoom;

    vec2 q = uv;
    // q.x = mix(-1.0, 1.0, q.x);
    // q.y = mix(-1.0, 1.0, q.y);
    // q.y *= RENDERSIZE.y / RENDERSIZE.x;

    q *= mat_zoom;

    vec3 color = scene(transform(kaleido(q)));
    // Output to screen
    return vec4(color * BASE_COLOR, 1.0);



}