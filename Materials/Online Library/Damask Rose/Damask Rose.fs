/*{
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/flKfzh",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
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
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },

        {
            "LABEL": "Rose/Scale",
            "NAME": "mat_shape_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Density",
            "NAME": "mat_complexity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Density Scale",
            "NAME": "mat_complexity_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Twist",
            "NAME": "mat_twist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Petals",
            "NAME": "mat_petals",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 12,
            "DEFAULT": 2
        },
        {
            "LABEL": "Rose/Erode",
            "NAME": "mat_erode",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 3
        },

        {
            "LABEL": "Rose/Height 1",
            "NAME": "mat_height1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Height 2",
            "NAME": "mat_height2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Edge",
            "NAME": "mat_edge",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Rose/Distort 1",
            "NAME": "mat_distort1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Rose/Distort 2",
            "NAME": "mat_distort2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Rose/Distort 3",
            "NAME": "mat_distort3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rose/Cycle 3",
            "NAME": "mat_cycle3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Rose/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.1,
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Lighting/Power",
            "NAME": "mat_light_pwr",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting/Distance",
            "NAME": "mat_light_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/Angle",
            "NAME": "mat_light_angle",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Lighting/Spill",
            "NAME": "mat_background",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting Angle/Animate",
            "NAME": "mat_animate_lighting",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Lighting Angle/Range",
            "NAME": "mat_lighting_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Lighting Angle/Signal",
            "NAME": "mat_lighting_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },

        {
            "LABEL": "Lighting Angle/Filter",
            "NAME": "mat_lighting_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Lighting Angle/Curve",
            "NAME": "mat_lighting_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting Angle/Speed",
            "NAME": "mat_lighting_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting Angle/BPM Sync",
            "NAME": "mat_lighting_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Lighting Angle/Reverse",
            "NAME": "mat_lighting_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Lighting Angle/Offset",
            "NAME": "mat_lighting_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Lighting Angle/Strobe",
            "NAME": "mat_lighting_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Lighting Angle/Restart",
            "NAME": "mat_lighting_restart",
            "TYPE": "event",

        },

        {
            "LABEL": "Light 1/Power",
            "NAME": "mat_light1_pwr",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Light 1/Distance",
            "NAME": "mat_light1_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Light 1/Angle",
            "NAME": "mat_light1_angle",
            "TYPE": "float",
            "MIN": 0.,
            "MAX": 360.,
            "DEFAULT": 225.
        },

        {
            "LABEL": "Light 1/Hue",
            "NAME": "mat_light1_hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Light 2/Power",
            "NAME": "mat_light2_pwr",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Light 2/Distance",
            "NAME": "mat_light2_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Light 2/Angle",
            "NAME": "mat_light2_angle",
            "TYPE": "float",
            "MIN": 0.,
            "MAX": 360.,
            "DEFAULT": 315.
        },
        {
            "LABEL": "Light 2/Hue",
            "NAME": "mat_light2_hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },



        {
            "LABEL": "Glow 1/Power",
            "NAME": "mat_glow1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Glow 2/Power",
            "NAME": "mat_glow2",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Glow 3/Power",
            "NAME": "mat_glow3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Glow 3/Hue",
            "NAME": "mat_glow_hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },



        {
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Saturation",
            "NAME": "mat_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "mat_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
        },

    ],
    "GENERATORS": [
        {
            "NAME": "mat_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "reverse": "mat_reverse",
                "strob" : "mat_strob",
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_lighting_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_lighting_speed",
                "speed_curve":2,
                "strob" : "mat_lighting_strob",
                "reverse": "mat_lighting_reverse",
                "bpm_sync": "mat_lighting_bpm_sync",
                "reset": "mat_lighting_restart",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 32. * mat_offset_scale) * 4.;
float mat_lighting_time = fract((mat_lighting_time_source * 0.125  - mat_lighting_offset));

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale * 2.5;


    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate + 180.) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*(mat_rotate + 180.) / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate + 180.) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    // uv.y = 1. - uv.y;
    // uv.y -= 0.5;

    return uv;
}

vec3 applyHue(vec3 color, float hue, float saturation) {
    // Apply Hue Shift and saturation
    if (hue > 0.01 || saturation != 0) {
        vec3 hsv = rgb2hsv(color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+hue));
        hsv.y = max(hsv.y + saturation, 0);
        color.rgb = hsv2rgb(hsv);
    }
    return color;
}

// Easing & filtering functions

float matEaseInOut(float t, float curve) {
    // Simple ease-in-out function
    if (t < 0.5) {
        return pow(2 * t, curve) / 2.;
    } else {
        return 1. - pow(-2. * t + 2, curve) / 2.;
    }
}

float matEaseIn(float t, float curve) {
    // Simple ease-in function
    return pow(t, curve);
}

float matEaseOut(float t, float curve) {
    // Simple ease-out function
    return 1. - pow(1. - t, curve);
}

float matEaseOutIn(float t, float curve) {
    // Ease-out-in function
    // This gets squirrely with high curve values
    return (pow(t, 3.) - 2. * pow(t, 2.) + t) * curve + (-2. * pow(t,3.) + 3. * pow(t, 2.)) + (pow(t, 3.) - pow(t, 2.)) * curve;
}


float matFilter(float t, int filter_type, float curve) {
    // Apply one of four filters to time-varying variable t (ranging from 0 to 1) with curve

    if (filter_type == 0) { // Ease In
        return matEaseIn(t, curve);
    } else if (filter_type == 1) { // Ease Out
        return matEaseOut(t, curve);
    } else if (filter_type == 2) { // Ease In Out
        return matEaseInOut(t, curve);
    } else { // Ease Out In
        return matEaseOutIn(t, curve);
    }
}


// CC0: Windows Terminal Damask Rose
//  Been tinkering creating Windows Terminal shaders
//  Created this as a version of an earlier shader
//  Thought it turned out decent so sharing

// https://mrange.github.io/windows-terminal-shader-gallery/

// Define to use a faster atan implementation
//  Introduces slight assymmetries that don't look outright terrible at least
//#define FASTATAN

#define PI_2        (0.5*PI)
#define TAU         (2.0*PI)
#define ROT(a)      mat2(cos(a), sin(a), -sin(a), cos(a))


#if defined(FASTATAN)
#define ATAN atan_approx
#else
#define ATAN atan
#endif

const float hf = 0.015;

// License: WTFPL, author: sam hocevar, found: https://stackoverflow.com/a/17897228/418488
const vec4 hsv2rgb_K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
// vec3 hsv2rgb(vec3 c) {
//   vec3 p = abs(fract(c.xxx + hsv2rgb_K.xyz) * 6.0 - hsv2rgb_K.www);
//   return c.z * mix(hsv2rgb_K.xxx, clamp(p - hsv2rgb_K.xxx, 0.0, 1.0), c.y);
// }
// License: WTFPL, author: sam hocevar, found: https://stackoverflow.com/a/17897228/418488
//  Macro version of above to enable compile-time constants
#define HSV2RGB(c)  (c.z * mix(hsv2rgb_K.xxx, clamp(abs(fract(c.xxx + hsv2rgb_K.xyz) * 6.0 - hsv2rgb_K.www) - hsv2rgb_K.xxx, 0.0, 1.0), c.y))

// License: Unknown, author: nmz (twitter: @stormoid), found: https://www.shadertoy.com/view/NdfyRM
vec3 sRGB(vec3 t) {
  return mix(1.055*pow(t, vec3(1./2.4)) - 0.055, 12.92*t, step(t, vec3(0.0031308)));
}

// License: Unknown, author: Matt Taylor (https://github.com/64), found: https://64.github.io/tonemapping/
vec3 aces_approx(vec3 v) {
  v = max(v, 0.0);
  v *= 0.6f;
  float a = 2.51f;
  float b = 0.03f;
  float c = 2.43f;
  float d = 0.59f;
  float e = 0.14f;
  return clamp((v*(a*v+b))/(v*(c*v+d)+e), 0.0f, 1.0f) * pow(mat_glow1, 2.);
}

// License: Unknown, author: Unknown, found: don't remember
float tanh_approx(float x) {
//  return tanh(x);
  float x2 = x*x;
  return clamp(x*(27.0 + x2)/(27.0+9.0*x2), -1.0, 1.0) * mat_cycle2;
}

// License: MIT, author: Pascal Gilcher, found: https://www.shadertoy.com/view/flSXRV
float atan_approx(float y, float x) {
  float cosatan2 = x / (abs(x) + abs(y));
  float t = PI_2 - cosatan2 * PI_2;
  return y < 0.0 ? -t : t;
}

// License: MIT, author: Inigo Quilez, found: https://www.iquilezles.org/www/articles/smin/smin.htm
float pmin(float a, float b, float k) {
  float h = clamp(0.5+0.5*(b-a)/k, 0.0, 1.0);
  return (mix(b, a, h) * mat_distort3 - k*h*(1.0-h) * pow(mat_edge,3.));
}

float pabs(float a, float k) {
  return -pmin(a, -a, k) * mat_cycle1;
}

float height(vec2 p) {
//  float tm = mat_time-2.*length(p);
  float tm = mat_time;
  const float xm = 0.5*0.005123;
  float ym = mix(0.125, 0.25, 0.5-0.5*cos(TAU*mat_time/600.0));

  p *= 0.4 * mat_shape_scale;

  float d = length(p);
  float c = 1E6;
  float x = pow(d, 0.1);
  float y = (mat_petals/2*ATAN(p.x, p.y)+0.05*tm-2.0*d * pow(mat_twist,1.5)) / TAU;

  for (float i = 0.; i < int(mat_erode); ++i) {
    float v = length(fract(vec2(x * pow(mat_complexity,0.25 * mat_complexity_sensitivity) - tm*i*xm, fract(y + i*ym)*.5)*20.)*2.-1.) * mat_distort2;
    c = pmin(c, v, 0.125) * mat_distort1;
  }

  float h =  (-hf*mat_cycle3+hf*(pabs(tanh_approx(5.5*d-80.*c*c*d*d*(mat_height2*.55-d))-0.25*d*mat_background, 0.25)));

  return h * pow(mat_height1,1.5);
}

vec3 normal(vec2 p) {
  vec2 e = vec2(4.0/(mat_detail * 2000.), 0);

  vec3 n;
  n.x = height(p + e.xy) - height(p - e.xy);
  n.y = -2.0*e.x;
  n.z = height(p + e.yx) - height(p - e.yx);

  return normalize(n);
}

vec3 color(vec2 p) {
  const float ss = 1.25;
  const float hh = 1.95;

  // float rotate;
  float lighting_time = 0.;

  if (mat_animate_lighting) {

      if (mat_lighting_signal == 0) { // Saw
          lighting_time = mat_lighting_time;
      } else if (mat_lighting_signal == 1) { // Inverse Saw
          lighting_time = 1. - mat_lighting_time;
      } else if (mat_lighting_signal == 2) { // Square
          lighting_time = floor(mat_lighting_time + 0.5);
      } else if (mat_lighting_signal == 3) { // Inverse Square
          lighting_time = 1. - floor(mat_lighting_time + 0.5);
      } else if (mat_lighting_signal == 4) { // Triangle
          lighting_time = abs(0.5 - mat_lighting_time);
      } else { // Sine
          lighting_time = 0.5 + 0.5 * sin(2. * PI * mat_lighting_time);
      }

      lighting_time = 1. - lighting_time;
      lighting_time = matFilter(lighting_time, mat_lighting_filter, mat_lighting_curve);
      float range_min = mat_lighting_range[0];
      float range_max = mat_lighting_range[1];
      lighting_time = range_min + lighting_time * (range_max - range_min);

  }

  lighting_time *= 360.;


  vec2 lp1c = vec2(0., 0.);
  vec2 lp2c = vec2(0., 0.);

  lp1c = matRot2D(lp1c, 2*PI*(-mat_light1_angle - mat_light_angle - 135. - lighting_time) / 360);
  lp2c = matRot2D(lp2c, 2*PI*(-mat_light2_angle - mat_light_angle - 135. - lighting_time) / 360);

  lp1c = (lp1c - 0.5) * 2.;
  lp2c = (lp2c - 0.5) * 2.;

  vec3 lp1 = -vec3(lp1c.x, hh, lp1c.y)*vec3(ss, 1.0, ss) * mat_light1_dist * mat_light_dist;
  vec3 lp2 = -vec3(lp2c.x, hh, lp2c.y)*vec3(ss, 1.0, ss) * mat_light2_dist * mat_light_dist;

  vec3 light1_color = vec3(0.30, 0.35, 2.0);
  vec3 light2_color = vec3(0.57, 0.6 , 2.0);
  vec3 glow_color = vec3(0.55, 0.9, 0.05);

  light1_color = applyHue(light1_color, mat_light1_hue, 0.);
  light2_color = applyHue(light2_color, mat_light2_hue, 0.);
  glow_color = applyHue(glow_color, mat_glow_hue, 0.);

  vec3 lcol1 = HSV2RGB(light1_color) * mat_light1_pwr * mat_light_pwr;
  vec3 lcol2 = HSV2RGB(light2_color) * mat_light2_pwr * mat_light_pwr;
  vec3 mat   = HSV2RGB(glow_color) * pow(mat_glow3, 2.);

  // vec3 lcol1 = HSV2RGB(mat_light1_color.rgb * vec3(2.)) * mat_light1_pwr;
  // vec3 lcol2 = HSV2RGB(mat_light2_color.rgb* vec3(2.)) * mat_light2_pwr;
  // vec3 mat   = HSV2RGB(mat_glow_color.rgb* vec3(2.)) * pow(mat_glow3, 2.);
  float spe  = 16.0 / mat_glow2;

  float h = height(p);
  vec3  n = normal(p);

  vec3 ro = vec3(0.0, 8.0, 0.0);
  vec3 pp = vec3(p.x, 0.0, p.y);

  vec3 po = vec3(p.x, 0.0, p.y);
  vec3 rd = normalize(ro - po);

  vec3 ld1 = normalize(lp1 - po);
  vec3 ld2 = normalize(lp2 - po);

  float diff1 = max(dot(n, ld1), 0.0);
  float diff2 = max(dot(n, ld2), 0.0);

  vec3  rn    = n;
  vec3  ref   = reflect(rd, rn);
  float ref1  = max(dot(ref, ld1), 0.0);
  float ref2  = max(dot(ref, ld2), 0.0);

  float dm = tanh_approx(abs(h)*120.0);
  float rm = dm;
  dm *= dm;

  vec3 lpow1 = dm*mat*lcol1;
  vec3 lpow2 = dm*mat*lcol2;

  vec3 col = vec3(0.0);
  col += diff1*diff1*lpow1;
  col += diff2*diff2*lpow2;

  col += rm*pow(ref1, spe)*lcol1;
  col += rm*pow(ref2, spe)*lcol2;

  return col;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 q = uv + vec2(0.5);
    vec2 p = uv;

    vec3 col = color(p);
    col = aces_approx(col);
    col = sRGB(col);

    out_color = vec4(col, 1.0);

    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply Hue Shift and saturation
    if (mat_hue_shift > 0.01 || mat_saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+mat_hue_shift));
        hsv.y = max(hsv.y + mat_saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    return out_color;
}
