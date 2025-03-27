/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#45138.0",
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
        "Label": "Complexity",
        "NAME": "complexity",
        "TYPE": "float",
        "MIN": 1.0,
        "MAX": 10.0,
        "DEFAULT": 5.0
    },
    {
        "Label": "Stretch",
        "NAME": "stretch",
        "TYPE": "float",
        "MIN": 2.0,
        "MAX": 30.0,
        "DEFAULT": 5.0
    },
    {
        "Label": "Background",
        "NAME": "background",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.1
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
        "MAX": 8.0,
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

#define TWO_PI (PI * 2.0)
// #define N 5.0

// modified by @hintz
// modified by @quilime

float func(vec2 p, float a) {
    return cos(TWO_PI*(p.x * cos(a) + p.y * sin(a) + sin(iTime*0.001)*10.0 ));
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 v = texCoord - vec2(0.5);
    v *= mat_zoom;

    float scroll_time = mat_scroll_time - mat_scroll_offset * 10.;
    if (!mat_scroll) {
        scroll_time = 0.;
    }

    v.x -= scroll_time;

    float col = background;

    for(float i = 0.0; i < complexity; i++)
    {
        float a = i * (TWO_PI/stretch);
        // float c0 = func(v, a);
        // float c1 = func(1.1 * v, 2.0 * a);
        // float c2 = func(vec2(c0, c1), a);
        // float c3 = func(0.99 * vec2(0.9 * c1, 1.3 * c0), c1);
        // float c4 = func(vec2(c2, c3), a);

        float c0 = func(v, a);
        float c1 = func(v, 0.1 * c0);
        float c2 = func(vec2(c0 * c1, c1), a);
        float c3 = func(vec2(0.2 * c1, c0), 0.06 * i * c2);
        float c4 = func(vec2(0.1 * c2 * c1, c1 * c3), c0);

        col += smoothstep(0.9, 2.0, 0.01 / (c4 * c4));
    }

     //col /= 1.0;

    return vec4(col, col, col, 1.0);




}