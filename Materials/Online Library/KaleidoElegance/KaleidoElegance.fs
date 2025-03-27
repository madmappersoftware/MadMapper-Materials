/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "VSN": "1.2",
  "CREDIT": "Jason Beyers",
    "INPUTS": [
        {
            "Label": "UV/Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "Label": "Kaleido/Depth",
            "NAME": "mat_depth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "Label": "Kaleido/Cycle 1",
            "NAME": "mat_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "Label": "Kaleido/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Kaleido/Stroke",
            "NAME": "mat_stroke",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "Label": "Kaleido/Sharpness",
            "NAME": "mat_sharpness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "Label": "Animation/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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

            "LABEL": "Color/Front Color",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },


    ],
    "GENERATORS": [
        {
            "NAME": "mat_time_source",
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
  "DESCRIPTION" : "From http:\/\/glslsandbox.com\/e#50318.0"
}
*/


#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}


//repost
//fascinating form
//ive got something in mind for tomorrow you may appreciate
//sphinx

vec4 materialColorForPixel(vec2 texCoord) {
  // vec2 uv = (2. * gl_FragCoord.xy - RENDERSIZE) / RENDERSIZE.y;

  vec2 uv = texCoord - vec2(0.5);
  uv *= 4. * mat_zoom;

  uv += vec2(0.5);
  uv = matRot2D(uv, 2*PI*mat_rotate / 360);
  uv -= vec2(0.5);

  vec2 uv_shift = mat_shift_amount;
  uv_shift += vec2(0.5);
  uv_shift.x = 1.-uv_shift.x;
  uv_shift -= vec2(0.5);
  uv += uv_shift;


  float s = .5 * mat_depth;
  float sn = sin(.1 + mat_time / 10.);
  float cs = cos(.1 +  mat_time / 10.);
  mat2 rot = mat2(cs, sn, -sn, cs);
  for (int i = 0; i < 10; i++) {
    uv = abs(uv) - s;
    uv *= rot;
    s *= .92;
  }
  float l = length(uv);
  float a = atan(uv.x, uv.y) * mat_cycle;
  float m = 6.28 / 3.;
  a = mat_cycle2 * mod(a + m / 2., m) - m / 2.;
  float d = abs(l * cos(a) - .2) / mat_stroke;
//  d = min(d, abs(uv.x) + .01);
  vec3 col = vec3(smoothstep(.04, .01 * pow(mat_sharpness,0.9), d)) * mat_front_color.rgb;

  return mix(mat_back_color, mat_front_color, mat_luma(col));

}