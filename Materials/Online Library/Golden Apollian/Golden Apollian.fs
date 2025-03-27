/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/WlcfRS",

    "VSN": "1.0",

    "INPUTS": [



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
            "LABEL": "Tunnel/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/Center Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tunnel/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tunnel/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Tunnel/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Tunnel/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Tunnel/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tunnel/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Pattern/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/Cycle 3",
            "NAME": "mat_cycle3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Speed",
            "NAME": "mat_speed_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/BPM Sync",
            "NAME": "mat_bpm_sync_2",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Pattern/Reverse",
            "NAME": "mat_reverse_2",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Pattern/Offset",
            "NAME": "mat_offset_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Pattern/Offset Scale",
            "NAME": "mat_offset_scale_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Pattern/Strob",
            "NAME": "mat_strob_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Pattern/Restart",
            "NAME": "mat_restart_2",
            "TYPE": "event",
        },

        {
            "LABEL": "Detail/Sharpness",
            "NAME": "mat_sharpness",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Post/Post Processing",
            "NAME": "mat_post",
            "TYPE": "bool",
            "DEFAULT": 1,
            "FLAGS": "button"
        },

        {
            "LABEL": "Background/Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.667,
                0.8,
                1.0,
                1.0
            ]
        },


        {
            "LABEL": "Background/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
            "NAME": "mat_time_source_2",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed_2",
                "speed_curve":2,
                "reverse": "mat_reverse_2",
                "strob" : "mat_strob_2",
                "reset": "mat_restart_2",
                "bpm_sync": "mat_bpm_sync_2",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_time_2 = mat_time_source_2 - mat_offset_2 * 8. * mat_offset_scale_2;

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


// Licence CC0: Golden apollian
// More late night coding

// -----------------------------------------------------------------------------
// COMMON
// -----------------------------------------------------------------------------


#define TAU             (2.0*PI)


#define ROT(a)          mat2(cos(a), sin(a), -sin(a), cos(a))
#define PSIN(x)         (0.5+0.5*sin(x))
#define LESS(a,b,c)     mix(a,b,step(0.,c))
#define SABS(x,k)       LESS((.5/(k))*(x)*(x)+(k)*.5,abs(x),abs(x)-(k))
#define L2(x)           dot(x, x)
#define PLANE_PERIOD    5.0

const vec3 std_gamma   = vec3(2.2, 2.2, 2.2);
// const vec3 planeCol    = vec3(1.0, 1.2, 1.5);
// const vec3 baseRingCol = pow(vec3(1.0, 0.65, 0.25), vec3(0.6));
// const vec3 sunCol      = vec3(1.25, 1.0, 1.1)/1.25;

vec3 planeCol    = mat_back_color.rgb / 1.5;
const vec3 baseRingCol = pow(vec3(1.0, 0.65, 0.25), vec3(0.6));
const vec3 sunCol      = vec3(1.25, 1.0, 1.1)/1.25;

struct effect {
  float lw;
  float tw;
  float sk;
  float cs;
};

const effect effects[] = effect[](
    effect(0.125, 0.0, 0.0, 0.0)
  , effect(0.125, 0.0, 0.0, 1.0)
  , effect(0.125, 0.0, 1.0, 1.0)
  , effect(0.125, 1.0, 1.0, 1.0)
  , effect(0.125, 1.0, 1.0, 0.0)
  , effect(0.125, 1.0, 0.0, 0.0)
  );
effect current_effect = effects[5];

float hash(float co) {
  co += 100.0;
  return fract(sin(co*12.9898) * 13758.5453);
}

vec2 toPolar(vec2 p) {
  return vec2(length(p), atan(p.y, p.x));
}

vec2 toRect(vec2 p) {
  return vec2(p.x*cos(p.y), p.x*sin(p.y));
}

float modMirror1(inout float p, float size) {
  float halfsize = size*0.5;
  float c = floor((p + halfsize)/size);
  p = mod(p + halfsize,size) - halfsize;
  p *= mod(c, 2.0)*2.0 - 1.0;
  return c;
}

float smoothKaleidoscope(inout vec2 p, float sm, float rep) {
  vec2 hp = p;
  vec2 hpp = toPolar(hp);
  float rn = modMirror1(hpp.y, TAU/rep);
  float sa = PI/rep - SABS(PI/rep - abs(hpp.y), sm);
  hpp.y = sign(hpp.y)*(sa);
  hp = toRect(hpp);
  p = hp;
  return rn;
}

vec4 alphaBlend(vec4 back, vec4 front) {
  float w = front.w + back.w*(1.0-front.w);
  vec3 xyz = (front.xyz*front.w + back.xyz*back.w*(1.0-front.w))/w;
  return w > 0.0 ? vec4(xyz, w) : vec4(0.0);
}

vec3 alphaBlend(vec3 back, vec4 front) {
  return mix(back, front.xyz, front.w);
}

float tanh_approx(float x) {
//  return tanh(x);
  float x2 = x*x;
  return clamp(x*(27.0 + x2)/(27.0+9.0*x2), -1.0, 1.0);
}

float pmin(float a, float b, float k) {
  float h = clamp(0.5+0.5*(b-a)/k, 0.0, 1.0);

  return mix(b, a, h) - k*h*(1.0-h);
}

float circle(vec2 p, float r) {
  return length(p) - r;
}

float hex(vec2 p, float r) {
  const vec3 k = vec3(-sqrt(3.0)/2.0,1.0/2.0,sqrt(3.0)/3.0);
  p = p.yx;
  p = abs(p);
  p -= 2.0*min(dot(k.xy,p),0.0)*k.xy;
  p -= vec2(clamp(p.x, -k.z*r, k.z*r), r);
  return length(p)*sign(p.y);
}

float apollian(vec4 p, float s) {
  float scale = 1.0;
  for(int i=0; i<7; ++i) {
    p = -1.0 + 2.0*fract(0.5*p+0.5);
    float r2 = dot(p,p);
    float k  = s/r2;
    p       *= k;
    scale   *= k;
  }

  float lw = 0.00125*current_effect.lw;

  float d0 = abs(p.y)-lw*scale;
  float d1 = abs(circle(p.xz, 0.005*scale))-lw*scale;
  float d = d0;
  d = mix(d, min(d, d1), current_effect.tw);
  return (d/scale);
}

// -----------------------------------------------------------------------------
// PATH
// -----------------------------------------------------------------------------

// The path function
vec3 offset(float z) {
  float a = z;
  vec2 p = -0.075*(vec2(cos(a), sin(a*sqrt(2.0))) + vec2(cos(a*sqrt(0.75)), sin(a*sqrt(0.5))));
  return vec3(p, z);
}

// The derivate of the path function
//  Used to generate where we are looking
vec3 doffset(float z) {
  float eps = 0.1;
  return 0.5*(offset(z + eps) - offset(z - eps))/eps;
}

// The second derivate of the path function
//  Used to generate tilt
vec3 ddoffset(float z) {
  float eps = 0.1;
  return 0.125*(doffset(z + eps) - doffset(z - eps))/eps;
}

// -----------------------------------------------------------------------------
// PLANE MARCHER
// -----------------------------------------------------------------------------

float weird(vec2 p, float h) {
  float z = 4.0 * mat_cycle1;
  float tm = 0.1*mat_time_2+h*10.0;
  p *= ROT(tm*0.5);
  float r = 0.5;
  vec4 off = vec4(r*PSIN(tm*sqrt(3.0)), r*PSIN(tm*sqrt(1.5)), r*PSIN(tm*sqrt(2.0)), 0.0)  * mat_cycle2;
  vec4 pp = vec4(p.x, p.y, 0.0, 0.0)+off;
  pp.w = 0.125*(1.0-tanh_approx(length(pp.xyz)));
  pp.yz *= ROT(tm);
  pp.xz *= ROT(tm*sqrt(0.5));
  pp /= z;
  float d = apollian(pp, mat_cycle3*0.8+h);
  return d*z;
}

float circles(vec2 p) {
  vec2 pp = toPolar(p);
  const float ss = 2.0;
  pp.x = fract(pp.x/ss)*ss;
  p = toRect(pp);
  float d = circle(p, 1.0);
  return d;
}

vec2 df(vec2 p, float h) {
  vec2 wp = p;
  float rep = 2.0*round(mix(5.0, 15.0, h*h));
  float ss = 0.05*6.0/rep;

  if (current_effect.sk > 0.0) {
    smoothKaleidoscope(wp, ss, rep);
  }

  float d0 = weird(wp, h);
  float d1 = hex(p, 0.25)-0.1;
  float d2 = circles(p);
  const float lw = 0.0125;
  d2 = abs(d2)-lw;
  float d = d0;

  if (current_effect.cs > 0.0) {
    d  = pmin(d, d2, 0.1);
  }

  d  = pmin(d, abs(d1)-lw, 0.1);
  d  = max(d, -(d1+lw));
  return vec2(d, d1+lw);
}

vec2 df(vec3 p, vec3 off, float s, mat2 rot, float h) {
  vec2 p2 = p.xy;
  p2 -= off.xy;
  p2 *= rot;
  return df(p2/s, h)*s;
}

vec3 skyColor(vec3 ro, vec3 rd) {
  float ld = max(dot(rd, vec3(0.0, 0.0, 1.0)), 0.0);
  return 1.0*sunCol*tanh_approx(3.0*pow(ld, 100.0));
}

vec4 plane(vec3 ro, vec3 rd, vec3 pp, float pd, vec3 off, float aa, float n) {
  int pi = int(mod(n/PLANE_PERIOD, float(effects.length())));
  current_effect = effects[pi];

  float h = hash(n);
  float s = 0.25*mix(0.5, 0.25, h);
  const float lw = 0.0235;
  const float lh = 1.25;

  const vec3 nor  = vec3(0.0, 0.0, -1.0);
  const vec3 loff = 2.0*vec3(0.25*0.5, 0.125*0.5, -0.125);
  vec3 lp1  = ro + loff;
  vec3 lp2  = ro + loff*vec3(-2.0, 1.0, 1.0);

  vec2 p = pp.xy-off.xy;

  mat2 rot = ROT(TAU*h);

  vec2 d2 = df(pp, off, s, rot, h);

  vec3 ld1   = normalize(lp1 - pp);
  vec3 ld2   = normalize(lp2 - pp);
  float dif1 = pow(max(dot(nor, ld1), 0.0), 5.0);
  float dif2 = pow(max(dot(nor, ld2), 0.0), 5.0);
  vec3 ref   = reflect(rd, nor);
  float spe1= pow(max(dot(ref, ld1), 0.0), 30.0);
  float spe2= pow(max(dot(ref, ld2), 0.0), 30.0);

  const float boff = 0.0125*0.5;
  float dbt = boff/rd.z;

  vec3 bpp = ro + (pd + dbt)*rd;
  vec2 bp = bpp.xy - off.xy;

  vec3 srd1 = normalize(lp1-bpp);
  vec3 srd2 = normalize(lp2-bpp);
  float bl21= L2(lp1-bpp);
  float bl22= L2(lp2-bpp);

  float st1 = -boff/srd1.z;
  float st2 = -boff/srd2.z;

  vec3 spp1 = bpp + st1*srd1;
  vec3 spp2 = bpp + st2*srd2;

  vec2 bd  = df(bpp, off, s, rot, h);
  vec2 sd1 = df(spp1, off, s, rot, h);
  vec2 sd2 = df(spp2, off, s, rot, h);

  vec3 col  = vec3(0.0);
  const float ss = 200.0;

  col       += 0.1125*planeCol*dif1*(1.0-exp(-ss*(max((sd1.x), 0.0))))/bl21;
  col       += 0.1125*planeCol*dif2*0.5*(1.0-exp(-ss*(max((sd2.x), 0.0))))/bl22;

  vec3 ringCol = baseRingCol;
  ringCol *= vec3(clamp(0.1+2.5*(0.1+0.25*((dif1*dif1/bl21+dif2*dif2/bl22))), 0.0, 1.0));
  ringCol += sqrt(baseRingCol)*spe1*2.0;
  ringCol += sqrt(baseRingCol)*spe2*2.0;
  col       = mix(col, ringCol, smoothstep(-aa, aa, -d2.x));

  float ha = smoothstep(-aa, aa, bd.y);

  return vec4(col, mix(0.0, 1.0, ha));
}

vec3 color(vec3 ww, vec3 uu, vec3 vv, vec3 ro, vec2 p) {
  float lp = length(p);
  vec2 np = p + 1.0/(vec2(1024.) * pow(mat_sharpness,2.) * 2.);
  float rdd = (2.0-0.5*tanh_approx(lp)) * mat_fov;  // Playing around with rdd can give interesting distortions
  vec3 rd = normalize(p.x*uu + p.y*vv + rdd*ww);
  vec3 nrd = normalize(np.x*uu + np.y*vv + rdd*ww);

  const float planeDist = 1.0-0.75;
  const int furthest = 9;
  const int fadeFrom = max(furthest-4, 0);
  const float fadeDist = planeDist*float(furthest - fadeFrom);
  float nz = floor(ro.z / planeDist);

  vec3 skyCol = skyColor(ro, rd) * mat_glow;

  // Steps from nearest to furthest plane and accumulates the color

  vec4 acol = vec4(0.0);
  const float cutOff = 0.95;
  bool cutOut = false;

  for (int i = 1; i <= furthest; ++i) {
    float pz = planeDist*nz + planeDist*float(i);

    float pd = (pz - ro.z)/rd.z;

    if (pd > 0.0 && acol.w < cutOff) {
      vec3 pp = ro + rd*pd;
      vec3 npp = ro + nrd*pd;

      float aa = 3.0*length(pp - npp);

      vec3 off = offset(pp.z);

      vec4 pcol = plane(ro, rd, pp, pd, off, aa, nz+float(i));

      float nz = pp.z-ro.z;
      float fadeIn = exp(-2.5*max((nz - planeDist*float(fadeFrom))/fadeDist, 0.0));
      float fadeOut = smoothstep(0.0, planeDist*0.1, nz);
      pcol.xyz = mix(skyCol, pcol.xyz, (fadeIn));
      pcol.w *= fadeOut;

      pcol = clamp(pcol, 0.0, 1.0);

      acol = alphaBlend(pcol, acol);
    } else {
      cutOut = true;
      break;
    }

  }

  vec3 col = alphaBlend(skyCol, acol);
// To debug cutouts due to transparency
//  col += cutOut ? vec3(1.0, -1.0, 0.0) : vec3(0.0);
  return col;
}

vec3 postProcess(vec3 col, vec2 q) {
    if (mat_post) {
        col = clamp(col, 0.0, 1.0);
        col = pow(col, 1.0/std_gamma);
        col = col*0.6+0.4*col*col*(3.0-2.0*col);
        col = mix(col, vec3(dot(col, vec3(0.33))), -0.4);
        col *=0.5+0.5*pow(19.0*q.x*q.y*(1.0-q.x)*(1.0-q.y),0.7);
    }
    return col;
}

vec3 color(vec2 p, vec2 q) {
  float tm  = mat_time*0.125;
  vec3 ro   = offset(tm);
  vec3 dro  = doffset(tm);
  vec3 ddro = ddoffset(tm);

  vec3 ww = normalize(dro);
  vec3 uu = normalize(cross(normalize(vec3(0.0,1.0,0.0)+ddro), ww));
  vec3 vv = normalize(cross(ww, uu));

  vec3 col = color(ww, uu, vv, ro, p);
  col = clamp(col, 0.0, 1.0);
  // col *= smoothstep(0.0, 5.0, mat_time);
  col = postProcess(col, q);

  return col;
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 p = uv;
    vec2 q = texCoord;
    vec3 col = color(p, q);
    out_color = vec4(col, 1.0);

    if (mat_luma_to_alpha) {
        out_color.a = mat_luma(out_color);
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


    return out_color;
}
