/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wl3fR7",

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
            "LABEL": "Fractal/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Fractal/Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Size",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Cycle",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Level",
            "NAME": "mat_level",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Fractal/Occulsion",
            "NAME": "mat_occulsion",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Iterations 1",
            "NAME": "mat_iterations1",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 10,
            "DEFAULT": 7
        },
        {
            "LABEL": "Fractal/Iterations 2",
            "NAME": "mat_iterations2",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 130,
            "DEFAULT": 130
        },


        {
            "LABEL": "Animation 1/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Animation 1/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 1/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 1/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 1/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 1/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 1/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Animation 2/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Animation 2/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 2/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 2/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 2/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 2/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 2/Restart",
            "NAME": "mat_a2_restart",
            "TYPE": "event",
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
        {
            "LABEL": "Color/Back Level",
            "NAME": "mat_back_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
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
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Mix"
        },
        {
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Sensitivity",
            "NAME": "mat_back_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

    ],
    "GENERATORS": [
        {
            "NAME": "mat_a1_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a1_speed",
                "speed_curve":2,
                "reverse": "mat_a1_reverse",
                "strob" : "mat_a1_strob",
                "reset": "mat_a1_restart",
                "bpm_sync": "mat_a1_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_a2_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a2_speed",
                "speed_curve":2,
                "reverse": "mat_a2_reverse",
                "strob" : "mat_a2_strob",
                "reset": "mat_a2_restart",
                "bpm_sync": "mat_a2_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = (mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale) * 2.;
float mat_a2_time = (mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 2.;

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

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 flipX(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the X axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.x = 1.-new_coord.x;
    new_coord -= vec2(0.5);
    return new_coord;
}

vec2 flipY(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the Y axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.y = 1.-new_coord.y;
    new_coord -= vec2(0.5);
    return new_coord;
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
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}

// License CC0: Slicing a 4D apollian
// Was experimenting with slicing 3D fractals in 2D.
// Naturally I wondered how it looks if we +1 dimension
// Based upon: https://www.shadertoy.com/view/4ds3zn


int   max_iter      = mat_iterations2;
const vec3  bone          = vec3(0.89, 0.855, 0.788);

void rot(inout vec2 p, float a) {
  float c = cos(a);
  float s = sin(a);
  p = vec2(c*p.x + s*p.y, -s*p.x + c*p.y);
}

float box(vec3 p, vec3 b) {
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

float apollian(vec4 p, float s) {
  float scale = 1.0;

  for(int i=0; i<mat_iterations1; ++i) {
    p        = -1.0 + 2.0*fract(0.5*p+0.5);

    float r2 = dot(p,p) / pow(mat_size,0.5);

    float k  = s/r2;
    p       *= k;
    scale   *= k;
  }

  vec4 ap = abs(p) / scale;
  float d = length(ap.yw) * mat_level;
  d = min(d, length(ap.xz));

  return 0.55*d;
}

float df(vec3 p) {
  vec4 p4 = vec4(p, 0.125 * mat_cycle1);
  float tm = 0.1*mat_a1_time;
  rot(p4.xw, tm*sqrt(0.5));
  rot(p4.yw, tm*sqrt(0.4));
  rot(p4.zw, tm*sqrt(0.3));
  float d1 = apollian(p4, 1.0/0.75);
  float db = box(p - vec3(0.0, 0.5, 0.0), vec3(1.5)) - 0.5;
  return max(d1, db);
}


float intersect(vec3 ro, vec3 rd, out int iter) {
  float res;
  float t = 0.2;
  iter = max_iter;

  for(int i = 0; i < max_iter; ++i) {
    vec3 p = ro + rd * t;
    res = df(p);
    if(res < 0.0003 * t || res > 20.) {
      iter = i;
      break;
    }
    t += res;
  }

  if(res > 20.) t = -1.;
  return t;
}

float ambientOcclusion(vec3 p, vec3 n) {
  float stepSize = 0.012 * mat_occulsion;
  float t = stepSize;

  float oc = 0.0;

  for(int i = 0; i < 12; i++) {
    float d = df(p + n * t);
    oc += t - d;
    t += stepSize;
  }

  return clamp(oc, 0.0, 1.0);
}

vec3 normal(in vec3 pos) {
  vec3  eps = vec3(.001,0.0,0.0);
  vec3 nor;
  nor.x = df(pos+eps.xyy) - df(pos-eps.xyy);
  nor.y = df(pos+eps.yxy) - df(pos-eps.yxy);
  nor.z = df(pos+eps.yyx) - df(pos-eps.yyx);
  return normalize(nor);
}

vec3 lighting(vec3 p, vec3 rd, int iter) {
  vec3 n = normal(p);
  float fake = float(iter)/float(max_iter);
  float fakeAmb = exp(-fake*fake*9.0);
  float amb = ambientOcclusion(p, n);

  vec3 col = vec3(mix(1.0, 0.125, pow(amb, 3.0)))*vec3(fakeAmb)*bone;
  return col;
}

vec3 post(vec3 col, vec2 q) {
  col=pow(clamp(col,0.0,1.0),vec3(0.65));
  col=col*0.6+0.4*col*col*(3.0-2.0*col);  // contrast
  col=mix(col, vec3(dot(col, vec3(0.33))), -0.5);  // satuation
  // col*=0.5+0.5*pow(19.0*q.x*q.y*(1.0-q.x)*(1.0-q.y),0.7);  // vigneting
  return col;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 q=texCoord;
    // vec2 uv = -1.0 + 2.0*q;
    // uv.y += 0.225;
    // uv.x*=RENDERSIZE.x/RENDERSIZE.y;

    vec3 la = vec3(0.0, 0.0, 0.0);
    vec3 ro = vec3(-4.0, 1., -0.0) * mat_zoom;
    rot(ro.xz, 2.0*PI*mat_a2_time/120.0);

    ro.y += mat_orbit.x * 4.;
    ro.x += mat_orbit.y * 4.;

    vec3 cf = normalize(la-ro);
    vec3 cs = normalize(cross(cf,vec3(0.0,1.0,0.0)));
    vec3 cu = normalize(cross(cs,cf));
    vec3 rd = normalize(uv.x*cs + uv.y*cu + 3.0*cf);  // transform from view to world
    vec3 bg = mat_back_level * mix(bone*0.5, bone, smoothstep(-1.0, 1.0, uv.y));
    vec3 col = bg;
    vec3 p=ro;
    int iter = 0;

    float t = intersect(ro, rd, iter);

    if(t > -0.5) {
      p = ro + t * rd;
      col = lighting(p, rd, iter);
      col = mix(col, bg, 1.0-exp(-0.001*t*t));
    }

    col=post(col, q);
    out_color=vec4(col.x,col.y,col.z,1.0);

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // General way to add transparency to any shader
    if (mat_back_mode == 1) {
        // Differentiate front & back colors using a hard cut w/ threshold
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = mat_back_color;
        } else {
            out_color = mat_front_color;
        }
    } else {
        // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * mat_back_sensitivity + mat_back_thresh);
    }

    return out_color;
}
