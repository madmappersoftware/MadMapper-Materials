/*
{
  "CATEGORIES" : [
    "procedural",
    "2d",
    "psychedelic"
  ],
  "CREDIT": "mojovideotech, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from interactiveshaderformat.com/sketches/976",
  "VSN": "1.0",
  "INPUTS" : [


    {
        "LABEL": "Animation/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Animation/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Animation/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Animation/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Animation/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Pattern/Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },

    {
        "Label": "Pattern/Center",
        "NAME": "center",
        "TYPE": "point2D",
        "DEFAULT": [
            0.5,
            0.5
        ],
        "MAX" : [
            1.0,
            1.0
        ],
        "MIN" : [
            0.0,
            0.0
        ]
    },
    {
        "Label": "Pattern/Width",
        "NAME": "width",
        "TYPE": "float",
        "DEFAULT": 0.25,
        "MIN": 0.01,
        "MAX": 0.50
    },
    {
        "Label": "Pattern/Blend",
        "NAME": "blend",
        "TYPE": "float",
        "DEFAULT": 3.0,
        "MIN": 0.0,
        "MAX": 5.0
    },
    {
        "Label": "Pattern/Color",
        "NAME": "color",
        "TYPE": "float",
        "DEFAULT": 1.0,
        "MIN": 0.0,
        "MAX": 1.0
    },
    {
        "Label": "Pattern/Freq",
        "NAME": "freq",
        "TYPE": "float",
        "DEFAULT": 0.33,
        "MIN": 0.01,
        "MAX": 3.0
    },
    {
        "Label": "Pattern/Amp",
        "NAME": "amp",
        "TYPE": "float",
        "DEFAULT": 0.5,
        "MIN": -1.0,
        "MAX": 3.0
    },
    {
        "Label": "Pattern/Depth",
        "NAME": "depth",
        "TYPE": "float",
        "DEFAULT": 0.75,
        "MIN": 0.1,
        "MAX": 3.0
    },
    {
        "Label": "Pattern/Mixer",
        "NAME": "mixer",
        "TYPE": "float",
        "DEFAULT": 0.5,
        "MIN": 0.0,
        "MAX": 1.0
    },
    {
        "Label": "Pattern/Warp",
        "NAME": "warp",
        "TYPE": "point2D",
        "DEFAULT": [
            1.0,
           -0.1
        ],
        "MAX" : [
            4.0,
            4.0
        ],
        "MIN" : [
            -4.0,
            -4.0
        ]
    }


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

// RippleRingVortex by mojovideotech
// interactiveshaderformat.com/sketches/976

#define r3  0.318309886183791

vec3 hsv(float h,float s,float v) {
    return mix(vec3(1.),clamp((abs(fract(h+vec3(3.,2.,1.)/3.)*6.-3.)-1.),0.,1.),s)*v;
}
float circle(vec2 p, float r) {
    return smoothstep(width, 0.0, abs(length(p)-r))*depth;
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord;
    //

    vec2 center_adj = center;
    center_adj.y = 1. - center_adj.y;
    uv -= center_adj;

    uv *= mat_zoom * 20.;

    // uv *= 202.0 - size;
    float r = smoothstep(-blend, blend, sin(iTime-length(uv)*freq))+amp;
//  vec2 rep = vec2(warp.x,warp.y);
    vec2 rep = vec2(sin(r-warp.x),cos(r3*r)-warp.y);
    vec2 p1 = pow(uv, rep)-sin(r*r3);
    vec2 p2 = mod(p1, rep)-cos(r*r3);
    vec2 p3 = mod(p2, rep)+sin(r*r3);
    vec2 p4 = mod(p3, rep)+cos(r*r3);
    vec2 p5 = mod(p4, rep)-sin(r*r3);
    vec2 p6 = mod(p5, rep)-cos(r*r3);
    vec2 p7 = mod(p6, rep)+sin(r*r3);
    vec2 p8 = mod(p7, rep)+cos(r*r3);

    float c; // = 0.0;
    float d;
    c += circle(p1, r);
    d += circle(p2, r);
    c += circle(p3, r);
    d += circle(p4, r);
    c += circle(p5, r);
    d += circle(p6, r);
    c += circle(p7, r);
    d += circle(p8, r);

    return vec4(hsv(r-color,r+c, mix(c,d,mixer)), 1.0);




}