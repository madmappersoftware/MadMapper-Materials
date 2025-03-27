/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#40019.1",
  "VSN": "1.0",
  "INPUTS" : [

    {
      "LABEL" : "Shift",
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
        "Label": "Stretch",
        "NAME": "stretch",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.5
    },
    {
        "Label": "Glow",
        "NAME": "glow",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Alpha",
        "NAME": "use_alpha",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
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
        "MAX": 4.0,
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

float iTime = mat_time - mat_offset * 20;

float rand(float n){return fract(sin(n) * 43758.5453123);}

float rand(vec2 n) {
    return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453);
}

float noise(float p){
    float fl = floor(p);
  float fc = fract(p);
    return mix(rand(fl), rand(fl + 1.0), fc);
}

float noise(vec2 n) {
    vec2 d = vec2(0.0, 1.0);
  vec2 b = floor(n);
    vec2 f = smoothstep(vec2(0.0), vec2(1.0), fract(n));
    float h = mix(
        mix(rand(b), rand(b + d.yx), f.x)
        , mix(rand(b + d.xy), rand(b + d.yy), f.x)
        , f.y);
    return h;
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    uv += mouse / 4.0;
    uv *= mat_zoom * 10.;

    uv.x /= stretch;

    uv.x += noise(uv*3.+6.)*clamp(sin(iTime*0.5-uv.x),0.,1.);
    uv.y += noise(uv*3.-5.)*clamp(sin(iTime*0.5-uv.x),0.,1.);
    float color = 0.0;
    color += glow*.05/(uv.x-floor(uv.x))+glow*.05/(uv.y-floor(uv.y));
    float alpha = 1.0;
    if (use_alpha) {
        alpha *= color;
    }
    return vec4( vec3(1.)*color, alpha);




}