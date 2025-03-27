/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#49123.6"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable

#define PI 3.14159265359

float iTime = mat_time - mat_offset * 10;


vec3 colorA = vec3(0.149, 0.141, 0.912);
vec3 colorB = vec3(1.000, 0.833, 0.224);

vec2 circle1(vec2 pt) {
    return vec2(abs(pt.x) + abs(pt.y), 0.8);
}

vec2 circle2(vec2 pt) {
    return vec2(length(pt), 0.4);
}

vec2 wave1(vec2 pt) {
    float r = length(pt);
    return vec2(0.5 * sin(r * 100.0) + 0.5, 0.5);
}

vec2 wave2(vec2 pt) {
    float r = abs(pt.x) + abs(pt.y);
    return vec2(0.5 * sin(r * 50.0) + 0.5, 0.5);
}

vec4 materialColorForPixel(vec2 texCoord) {

    // vec2 pt = ( gl_FragCoord.xy * 2.0 - RENDERSIZE ) / min(RENDERSIZE.x, RENDERSIZE.y);

    vec2 pt = texCoord - vec2(0.5);
    pt *= mat_zoom;

    float t = iTime * 2.0;
    float idx = floor(mod(t, 4.0));

    vec2 f = vec2(0.0);
    vec2 g = vec2(0.0);
    float r = smoothstep(0.2, 0.8, mod(t, 1.0));

    if (idx == 0.0) {
        f = circle1(pt);
        g = circle2(pt);
    } else if (idx == 1.0) {
        f = circle2(pt);
        g = wave1(pt);
    } else if (idx == 2.0) {
        f = wave1(pt);
        g = wave2(pt);
    } else if (idx == 3.0) {
        f = wave2(pt);
        g = circle1(pt);
    }

    vec2 h = mix(f, g, r);
    float gray = step(h.x, h.y);

    vec3 color = mix(colorA, colorB, gray);

    return vec4(color, 1.0 );

}