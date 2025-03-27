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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#46592.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

const vec3 color_inside = vec3(0, 0, 0);
const vec3 color_outside = vec3(1, 1, 1);
const vec3 color_strip = vec3(1, 0, 0);

// const float ANIMATION_SPEED = 0.5;
const float ANIMATION_SPEED = 1.0;
const float GRADIENT_FACTOR = 80.0;
const float DARKENING_FACTOR = 2.0;
const float PI = 3.1415926535897932384626433832795;

float cycleTime() {
    return mod(iTime * ANIMATION_SPEED, 1.0);
}

float radiusTranslation(float x) {
    return 1.0 / x;
}

float timeTranslation() {
    return cycleTime() * 2.0 * PI;
}

float mixFunction(float x) {
    return sin(radiusTranslation(x) + timeTranslation());
}

vec3 sharpMix(vec3 a, vec3 b, float x, float gf) {
    return mix(a, b, clamp(x * gf - gf / 2.0, 0.0, 1.0));
}

float gf(float radius) {
    return radius * GRADIENT_FACTOR;
}

vec3 color(float radius) {
    return sharpMix(color_inside, color_outside, mixFunction(radius), gf(radius)) * clamp(sqrt(radius * DARKENING_FACTOR), 0.0, 1.0);
}

vec4 materialColorForPixel( vec2 texCoord ) {
    // float ratio = RENDERSIZE.x / RENDERSIZE.y;
    // vec2 pos = ((gl_FragCoord.xy / RENDERSIZE.xy) - 0.5) * vec2(ratio, 1.0);

    vec2 pos = texCoord - vec2(0.5);
    pos *= mat_zoom;
    float radius = sqrt(pos.x * pos.x + pos.y * pos.y);

    return vec4(color(radius), 1.0);
}