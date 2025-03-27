/*
{
  "CATEGORIES" : [
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
        "MAX": 10.0,
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
  "DESCRIPTION" : "Trippy liquid. Converted from http:\/\/glslsandbox.com\/e#47429.0"
}
*/


// #include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;
#define TWO_PI 6.28318530718
#define rotate(a) mat2(cos(a), sin(a), -sin(a), cos(a))
#define spiral(u, a, r, t, d) abs(sin(t + r * length(u) + a * (d * atan(u.y, u.x))))
#define flower(u, a, r, t) spiral(u, a, r, t, 1.) * spiral(u, a, r, t, -1.)
#define sinp(a) .5 + sin(a) * .5
#define polar(a, t) a * vec2(cos(t), sin(t))


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 st = texCoord - vec2(0.5);

    st *= mat_zoom;

    st = rotate(iTime / 8.) * st;

    vec3 col;
    vec2 o = vec2(cos(iTime / 4.1), sin(iTime / 2.));

    float t = -.001*iTime;
    vec2 mp = 1.+4.*mouse.xy;

    for (int i = 0; i < 10; i++) {
        for (float i = 0.; i < TWO_PI; i += TWO_PI / 16.) {
            t += .6 * flower(vec2(st + polar(1., i)), 6.+mp.x, (4.4+mp.y) *
                             sinp(iTime / 2.), iTime / 4.);
        }
    col[i] = sin(5. * t + length(st) * 8. * sinp(t));
  }

  return vec4(mix(col, vec3(1., .7, 0.), col.r), 1.0);


}