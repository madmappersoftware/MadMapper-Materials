/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#46205.3"
}
*/


// http://glslsandbox.com/e#46205.3

#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset*10;

//#extension GL_OES_standard_derivatives : enable

#define PI 3.141592653589793
#define TWO_PI  6.283


float backOut(float t) {
    float f = 1.0 - t;
    return 1.0 - (pow(f, 3.0) - f * sin(f * PI));
}

vec2 random(vec2 p) {
    return fract(sin(vec2(dot(p,vec2(127.1, 311.7)), dot(p, vec2(269.5, 183.3)))) * 43758.5453);
}

mat2 rotate2d(float _angle){
    return mat2(cos(_angle), -sin(_angle),  sin(_angle), cos(_angle));
}

float map(float value, float beforeMin, float beforeMax, float afterMin, float afterMax) {
    return afterMin + (afterMax - afterMin) * ((value - beforeMin) / (beforeMax - beforeMin));
}

float obliqueLine(vec2 uv){
    return step(0.6, fract((uv.x + uv.y + iTime * 0.8) * 4.0));
}

vec4 materialColorForPixel( vec2 texCoord ) {
    // vec2 uv = (gl_FragCoord.xy * 2.0 - RENDERSIZE) / min(RENDERSIZE.x, RENDERSIZE.y);
    vec2 uv = texCoord - vec2(0.5);

    uv *= 2. * mat_zoom;
    vec2 scaledUv = uv * 8.0;

    scaledUv -= 0.5;
    scaledUv *= rotate2d(iTime * 0.2);
    scaledUv += 0.5;

    vec2 i_st = floor(scaledUv);
    vec2 f_st = fract(scaledUv);

    float m_dist = 1.0;

    // float t = iTime * 1.5;
    // float speed = (floor(t) + backOut(fract(t)));

    float t = iTime;
    float speed = (floor(t) + backOut(fract(t)));



    for (int j = -1; j <= 1; j++) {
        for (int i = -1; i <= 1; i++) {
            // Neighbor place in the grid
            vec2 neighbor = vec2(float(i), float(j));

            // Random position from current + neighbor place in the grid
            vec2 offset = random(i_st + neighbor);

            // Animate the offset
            offset = vec2(
                map(sin(speed + TWO_PI * offset).x, -1.0, 1.0, 0.0, 1.0),
                map(sin(speed + TWO_PI * offset).y, -1.0, 1.0, 0.0, 1.0)
            );

            // Position of the cell
            vec2 pos = neighbor + offset - f_st;

            // Cell distance
            float dist = length(pos);

            // Metaball
            m_dist = min(m_dist, m_dist * dist);
        }
    }

    float cell = 1.0 - step(0.1, m_dist);

    vec3 color = vec3(0.05);
    color += cell;

    color.r *= obliqueLine(uv * 4.0);
    color.g *= obliqueLine(uv * 8.0);

    return vec4(color, 1.0);
}