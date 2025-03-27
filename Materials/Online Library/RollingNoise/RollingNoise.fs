/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [
    {
      "NAME" : "mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "MIN" : [
        0,
        0
      ]
    },
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45139.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

// modified by @hintz
// modified by @quilime

float iTime = mat_time - mat_offset * 10;


#define PI 3.14159
#define TWO_PI (PI * 2.0)
#define N 7.0

vec4 materialColorForPixel(vec2 texCoord)
{
    // vec2 v = (gl_FragCoord.xy - RENDERSIZE) / min(RENDERSIZE.y,RENDERSIZE.x) * 7.0;

    vec2 v = texCoord - vec2(0.5);

    v *= 7. * mat_zoom;

    float col = 0.1;

    for(float i = 0.0; i < N; i++)
    {
        float a = i * (TWO_PI/N);
        col += cos(TWO_PI*(v.x * cos(.32*a) + v.y * sin(2.81*a)+ mouse.y +i*mouse.x + sin(iTime*0.01)*100.0 ));
    }

     col /= 1.0;

    return vec4(col, col, col, 1.0);


}