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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#42552.3"
}
*/


// http://glslsandbox.com/e#42552.3

#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

//#extension GL_OES_standard_derivatives : enable



float random(float n) {
  return fract(abs(sin(n * 55.753) * 367.34));
}

float box(vec2 uv, float size) {
  vec2 rect = step(-size, uv) * (1.0 - step(size, uv));
  return min(rect.x, rect.y);
}

vec4 materialColorForPixel(vec2 texCoord) {
  // vec2 uv = (gl_FragCoord.xy * 2.0 - RENDERSIZE.xy) / min(RENDERSIZE.x, RENDERSIZE.y);

  vec2 uv = texCoord - vec2(0.5);

  uv *= mat_zoom;

  float speed = iTime * 0.3;
  float scale = 4.0;
  float offset = 1.0;
  vec3 color = vec3(0.0);

  for (int i = 0; i < 3; i++) {
    if (i == 0) {
      uv.x += speed;
    } else {
      uv.x -= speed;
    }

    uv *= scale;
    uv = fract(uv);
    uv -= 0.5;

    float r = random(offset);
    vec3 box = vec3(0.2 + r, 0.6 * r, mod(8.0 * r, 1.0)) * box(uv, 0.3);
    color += box;
    scale /= 1.0;
    offset /= 2.0;
  }

  return vec4(color, 1.0);
}