/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https://www.shadertoy.com/view/4dGGzc",
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
        "Label": "Displace",
        "NAME": "displace",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.1,
        "DEFAULT": 0.01
    },
    {
        "Label": "Grid Size",
        "NAME": "gridSize",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 50.0,
        "DEFAULT": 20.0
    },
    {
        "Label": "Wave",
        "NAME": "wave",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 5.0
    },
    {
        "Label": "Brightness",
        "NAME": "brightness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.5
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
        "DEFAULT": 0.75
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
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 40.;

#define TWO_PI 6.2831853072

const float timeScale = 0.2;

// const float displace = 0.01;
// const float gridSize = 20.0;
// const float wave = 5.0;
// const float brightness = 1.5;

vec2 rotate(in vec2 v, in float angle) {
    float c = cos(angle);
    float s = sin(angle);
    return v * mat2(c, -s, s, c);
}

vec3 coordToHex(in vec2 coord, in float scale, in float angle) {
    vec2 c = rotate(coord, angle);
    float q = (1.0 / 3.0 * sqrt(3.0) * c.x - 1.0 / 3.0 * c.y) * scale;
    float r = 2.0 / 3.0 * c.y * scale;
    return vec3(q, r, -q - r);
}

vec3 hexToCell(in vec3 hex, in float m) {
    return fract(hex / m) * 2.0 - 1.0;
}

float absMax(in vec3 v) {
    return max(max(abs(v.x), abs(v.y)), abs(v.z));
}

float nsin(in float value) {
    return sin(value * TWO_PI) * 0.5 + 0.5;
}

float hexToFloat(in vec3 hex, in float amt) {
    return mix(absMax(hex), 1.0 - length(hex) / sqrt(3.0), amt);
}

float calc(in vec2 tx, in float time) {
    float angle = PI * nsin(time * 0.1) + PI / 6.0;
    float len = 1.0 - length(tx) * nsin(time);
    float value = 0.0;
    vec3 hex = coordToHex(tx, gridSize * nsin(time * 0.1), angle);

    for (int i = 0; i < 3; i++) {
        float offset = float(i) / 3.0;
        vec3 cell = hexToCell(hex, 1.0 + float(i));
        value += nsin(hexToFloat(cell,nsin(len + time + offset)) *
                  wave * nsin(time * 0.5 + offset) + len + time);
    }

    return value / 3.0;
}



vec4 materialColorForPixel(vec2 texCoord) {

    vec2 tx = texCoord - vec2(0.5);
    tx *= mat_zoom;

    // vec2 tx = (fragCoord / iResolution.xy) - 0.5;

    tx *= mat_zoom;
    // tx.x *= iResolution.x / iResolution.y;
    float time = iTime * timeScale;
    vec3 rgb = vec3(0.0, 0.0, 0.0);
    for (int i = 0; i < 3; i++) {
        float time2 = time + float(i) * displace;
        rgb[i] += pow(calc(tx, time2), 2.0);
    }
    return vec4(rgb * brightness, 1.0);




}