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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43760.1"
}
*/


// http://glslsandbox.com/e#43760.1

#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;
//#extension GL_OES_standard_derivatives : enable


float map(float value, float beforeMin, float beforeMax, float afterMin, float afterMax) {
    return afterMin + (afterMax - afterMin) * ((value - beforeMin) / (beforeMax - beforeMin));
}

float box(vec2 _st, vec2 _size){
    _size = vec2(0.5) - _size * 0.5;  // Adjust size.
    vec2 uv = step(_size, _st);
    uv *= step(_size, vec2(1.0) - _st);
    return uv.x * uv.y;
}

float exposeInOut(float t) {
    if (t == 0.0) {
        return 0.0;

    } else if (t == 1.0) {
        return 1.0;

    } else if ((t /= 0.5) < 1.0) {
        return 0.5 * pow(2.0, 10.0 * (t - 1.0));

    } else {
        return 0.5 * (-pow(2.0, -10.0 * --t) + 2.0);
    }
}

float ease(float t) {
    return exposeInOut(t);
}

mat2 rotate2d(float _angle){
    return mat2(cos(_angle), -sin(_angle),  sin(_angle), cos(_angle));
}

vec4 materialColorForPixel( vec2 texCoord ) {
    // vec2 uv = (gl_FragCoord.xy * 2.0 - RENDERSIZE.xy) / min(RENDERSIZE.x, RENDERSIZE.y);

    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    uv *= rotate2d(iTime * 0.2);

    vec2 scaledUv = uv * 10.0;
    vec2 repeatedUv = fract(scaledUv);

    float len = length(uv);
    float s = sin((iTime + len) * 10.0);
    float  r = 0.4 * s;

    float b = box(repeatedUv, vec2(r));
    vec3 color = vec3(b ,0.0, 0.1);

    if (b >= 1.0) {
        float t = map(sin(floor(iTime * 5.0) + ease(fract(iTime * 5.0))), -1.0, 1.0, 0.3, 1.0);
        color = vec3(0.5, t * 0.5, t);
    }
    return vec4(color, 1.0);
}