/*{
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/sl3yRM",

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
            "LABEL": "Shape/Inner Sphere",
            "NAME": "mat_inner_sphere",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button",
        },
        {
            "LABEL": "Shape/Poly U",
            "NAME": "mat_poly_u",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Poly V",
            "NAME": "mat_poly_v",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Poly M",
            "NAME": "mat_poly_m",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Shape/Poly Type",
            "NAME": "mat_poly_type",
            "TYPE": "int",
            "MIN": 2,
            "MAX": 5,
            "DEFAULT": 3
        },
        {
            "LABEL": "Shape/Container Size",
            "NAME": "mat_container_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Orbit/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },

        {
            "LABEL": "Orbit/Speed",
            "NAME": "mat_orbit_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Orbit/BPM Sync",
            "NAME": "mat_orbit_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Orbit/Reverse",
            "NAME": "mat_orbit_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Orbit/Offset",
            "NAME": "mat_orbit_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Orbit/Offset Scale",
            "NAME": "mat_orbit_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Orbit/Strob",
            "NAME": "mat_orbit_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Orbit/Restart",
            "NAME": "mat_orbit_restart",
            "TYPE": "event",
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
            "LABEL": "Color/Light",
            "NAME": "mat_light",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color/Gain",
            "NAME": "mat_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "NAME": "mat_orbit_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_orbit_speed",
                "speed_curve":2,
                "reverse": "mat_orbit_reverse",
                "strob" : "mat_orbit_strob",
                "reset": "mat_orbit_restart",
                "bpm_sync": "mat_orbit_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_orbit_time = mat_orbit_time_source - mat_orbit_offset * 8. * mat_orbit_offset_scale;

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
    uv *= mat_scale * 2.;

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



// CC0: Time for some inner reflections
// An evolution of: https://www.shadertoy.com/view/7dKBDt
// After a tip from shane found knighty's shader: https://www.shadertoy.com/view/MsKGzw
// Knighty's shaders allows one to experiment with many cool polyhedras

// Original inspiration from:https://www.youtube.com/watch?v=qNoQXF2dKBs

// ------------------------------------------------------------------------------------
// Here are some parameters to experiment with

#define INNER_SPHERE
// #define GOT_BEER

float poly_U        = mat_poly_u;  // [0, inf]
float poly_V        = mat_poly_v;  // [0, inf]
float poly_W        = mat_poly_m;  // [0, inf]
int   poly_type     = mat_poly_type;    // [2, 5]

const float zoom = 3.0;
// ------------------------------------------------------------------------------------

// #define PI          3.141592654
#define TAU         (2.0*PI)

#define TOLERANCE       0.0001
#define MAX_RAY_LENGTH  20.0
#define MAX_RAY_MARCHES 60
#define NORM_OFF        0.001
#define MAX_BOUNCES     6



#define ROT(a)          mat2(cos(a), sin(a), -sin(a), cos(a))


// License: Unknown, author: knighty, found: https://www.shadertoy.com/view/MsKGzw
float poly_cospin   = cos(PI/float(poly_type));
float poly_scospin  = sqrt(0.75-poly_cospin*poly_cospin);
vec3  poly_nc       = vec3(-0.5, -poly_cospin, poly_scospin);
vec3  poly_pab      = vec3(0., 0., 1.);
vec3  poly_pbc_     = vec3(poly_scospin, 0., 0.5);
vec3  poly_pca_     = vec3(0., poly_scospin, poly_cospin);
vec3  poly_p        = normalize((poly_U*poly_pab+poly_V*poly_pbc_+poly_W*poly_pca_));
vec3  poly_pbc      = normalize(poly_pbc_);
vec3  poly_pca      = normalize(poly_pca_);

const float initt = 0.125;

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
  return clamp((v*(a*v+b))/(v*(c*v+d)+e), 0.0f, 1.0f);
}

// License: Unknown, author: Unknown, found: don't remember
float tanh_approx(float x) {
  //  Found this somewhere on the interwebs
  //  return tanh(x);
  float x2 = x*x;
  return clamp(x*(27.0 + x2)/(27.0+9.0*x2), -1.0, 1.0);
}

// http://mercury.sexy/hg_sdf/
vec2 mod2(inout vec2 p, vec2 size) {
  vec2 c = floor((p + size*0.5)/size);
  p = mod(p + size*0.5,size) - size*0.5;
  return c;
}

float circle(vec2 p, float r) {
  return length(p) - r;
}

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm
float box(vec2 p, vec2 b) {
  vec2 d = abs(p)-b;
  return length(max(d,0.0)) + min(max(d.x,d.y),0.0);
}

// License: MIT, author: Inigo Quilez, found: https://www.iquilezles.org/www/articles/smin/smin.htm
float pmin(float a, float b, float k) {
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/www/articles/intersectors/intersectors.htm
float rayPlane(vec3 ro, vec3 rd, vec4 p) {
  return -(dot(ro,p.xyz)+p.w)/dot(rd,p.xyz);
}

// License: Unknown, author: knighty, found: https://www.shadertoy.com/view/MsKGzw
void poly_fold(inout vec3 pos) {
  vec3 p = pos;

  for(int i = 0; i < poly_type; ++i){
    p.xy  = abs(p.xy);
    p    -= 2.*min(0., dot(p,poly_nc)) * poly_nc;
  }

  pos = p / mat_container_size;
}

float poly_plane(vec3 pos) {
  float d0 = dot(pos, poly_pab);
  float d1 = dot(pos, poly_pbc);
  float d2 = dot(pos, poly_pca);
  float d = d0;
  d = max(d, d1);
  d = max(d, d2);
  return d;
}

float poly_corner(vec3 pos) {
  float d = length(pos) - .1;
  return d;
}

float dot2(vec3 p) {
  return dot(p, p);
}

float poly_edge(vec3 pos) {
  float dla = dot2(pos-min(0., pos.x)*vec3(1., 0., 0.));
  float dlb = dot2(pos-min(0., pos.y)*vec3(0., 1., 0.));
  float dlc = dot2(pos-min(0., dot(pos, poly_nc))*poly_nc);
  return sqrt(min(min(dla, dlb), dlc))-0.025;
}

float poly_planes(vec3 pos, out vec3 pp) {
  poly_fold(pos);
  pos -= poly_p;

  pp = pos;
  return poly_plane(pos);
}

float poly_edges(vec3 pos, out vec3 pp) {
  poly_fold(pos);
  pos -= poly_p;

  pp = pos;
  return poly_edge(pos);
}


float blobs(vec2 p) {
  // Generates a grid of dots
  vec2 bp = p;
  vec2 bn = mod2(bp, vec2(3.0));

  vec2 dp = p;
  vec2 dn = mod2(dp, vec2(0.25));
  float ddots = length(dp);

  // Blobs
  float dblobs = 1E6;
  for (int i = 0; i < 5; ++i) {
    float dd = circle(bp-1.0*vec2(sin(mat_time+float(i)), sin(float(i*i)+mat_time*sqrt(0.5))), 0.1);
    dblobs = pmin(dblobs, dd, 0.35);
  }

  float d = 1E6;
  d = min(d, ddots);
  // Smooth min between blobs and dots makes it look somewhat amoeba like
  d = pmin(d, dblobs, 0.35);
  return d;
}

vec3 skyColor(vec3 ro, vec3 rd) {
  const vec3 gcol = HSV2RGB(vec3(0.45, 0.6, 1.0));
  vec3 col = clamp(vec3(0.0025/abs(rd.y))*gcol, 0.0, 1.0);

  float tp0  = rayPlane(ro, rd, vec4(vec3(0.0, 1.0, 0.0), 4.0));
  float tp1  = rayPlane(ro, rd, vec4(vec3(0.0, -1.0, 0.0), 6.0));
  float tp = tp1;
  tp = max(tp0,tp1);
  if (tp > 0.0) {
    vec3 pos  = ro + tp*rd;
    const float fz = 0.25;
    const float bz = 1.0/fz;
    vec2 bpos = pos.xz/bz;
    float db = blobs(bpos)*bz;
    db = abs(db);
    vec2 pp = pos.xz*fz;
    float m = 0.5+0.25*(sin(3.0*pp.x+mat_time*2.1)+sin(3.3*pp.y+mat_time*2.0));
    m *= m;
    m *= m;
    pp = fract(pp+0.5)-0.5;
    float dp = pmin(abs(pp.x), abs(pp.y), 0.125);
    dp = min(dp, db);
    vec3 hsv = vec3(0.4+mix(0.15,0.0, m), tanh_approx(mix(50.0, 10.0, m)*dp), 1.0);
    vec3 pcol = 1.5*hsv2rgb(hsv)*exp(-mix(30.0, 10.0, m)*dp);

    float f = 1.0-tanh_approx(0.1*length(pos.xz));
    col = mix(col, pcol , f);
  }


  if (tp1 > 0.0) {
    vec3 pos  = ro + tp1*rd;
    vec2 pp = pos.xz;
    float db = box(pp, vec2(6.0, 9.0))-1.0;

    col += vec3(2.0)*gcol*rd.y*smoothstep(0.25, 0.0, db);
    col += vec3(0.8)*gcol*exp(-0.5*max(db, 0.0));
  }


  return col * mat_light;
}

float dfExclusion(vec3 p, out vec3 pp) {
  return -poly_edges(p/zoom, pp)*zoom;
}

float shape(vec3 p) {
  vec3 pp;
  return poly_planes(p/zoom, pp)*zoom;
}

float df0(vec3 p) {
  float d0 = shape(p);
  float d = d0;
  return d;
}

float df1(vec3 p) {
  float d0 = -shape(p);
  float d = d0;
    if (mat_inner_sphere) {
      float d1 = length(p) - 2.;
      d = min(d, d1);
    }
  return d;
}

vec3 normal1(vec3 pos) {
  vec2  eps = vec2(NORM_OFF,0.0);
  vec3 nor;
  nor.x = df1(pos+eps.xyy) - df1(pos-eps.xyy);
  nor.y = df1(pos+eps.yxy) - df1(pos-eps.yxy);
  nor.z = df1(pos+eps.yyx) - df1(pos-eps.yyx);
  return normalize(nor);
}

float rayMarch1(vec3 ro, vec3 rd) {
  float t = 0.0;
  for (int i = 0; i < MAX_RAY_MARCHES; i++) {
    if (t > MAX_RAY_LENGTH) {
      t = MAX_RAY_LENGTH;
      break;
    }
    float d = df1(ro + rd*t);
    if (d < TOLERANCE) {
      break;
    }
    t  += d;
  }
  return t;
}

vec3 normal0(vec3 pos) {
  vec2  eps = vec2(NORM_OFF,0.0);
  vec3 nor;
  nor.x = df0(pos+eps.xyy) - df0(pos-eps.xyy);
  nor.y = df0(pos+eps.yxy) - df0(pos-eps.yxy);
  nor.z = df0(pos+eps.yyx) - df0(pos-eps.yyx);
  return normalize(nor);
}

float rayMarch0(vec3 ro, vec3 rd) {
  float t = 0.0;
  for (int i = 0; i < MAX_RAY_MARCHES; i++) {
    if (t > MAX_RAY_LENGTH) {
      t = MAX_RAY_LENGTH;
      break;
    }
    float d = df0(ro + rd*t);
    if (d < TOLERANCE) {
      break;
    }
    t  += d;
  }
  return t;
}

vec3 render1(vec3 ro, vec3 rd) {
  vec3 agg = vec3(0.0, 0.0, 0.0);
  float tagg = initt;
  vec3 ragg = vec3(1.0);

  for (int bounce = 0; bounce < MAX_BOUNCES; ++bounce) {
    float mragg = max(max(ragg.x, ragg.y), ragg.z);
    if (mragg < 0.1) break;
    float st = rayMarch1(ro, rd);
    tagg += st;
    vec3 sp = ro+rd*st;
    vec3 spp;
    float de = dfExclusion(sp, spp);
    vec3 sn = normal1(sp);

    float si = cos(5.0*TAU*zoom*spp.z-0.5*sp.y+mat_time);
    const vec3 lcol = vec3(1.0, 1.5, 2.0)*0.8;
    float lf = mix(0.0, 1.0, smoothstep(0., 0.9, si));

    vec3 gcol = ragg*lcol*exp(8.0*(min(de-0.2, 0.0)));
    // Will never miss
    if (de < 0.0) {
      agg += gcol;
      ragg *= vec3(0.5, 0.6,0.8);
    } else {
      agg += gcol*lf;
      agg += ragg*lcol*1.5*lf;
      ragg = vec3(0.0);
    }

    rd = reflect(rd, sn);
    ro = sp+initt*rd;
    tagg += initt;
  }
#if defined(GOT_BEER)
  return agg*exp(-.5*vec3(0.3, 0.15, 0.1)*tagg);
#else
  return agg;
#endif
}

vec3 render0(vec3 ro, vec3 rd) {
  vec3 skyCol = skyColor(ro, rd);

  vec3 col = skyCol;

  float st = rayMarch0(ro, rd);
  vec3 sp = ro+rd*st;
  vec3 sn = normal0(sp);
    vec3 spp;
  float de = dfExclusion(sp, spp);
  float ptime = mod(mat_time, 30.0);
  if (st < MAX_RAY_LENGTH) {
    float sfre = 1.0+dot(rd, sn);
    sfre *= sfre;
    sfre = mix(0.1, 1.0, sfre);
    vec3 sref   = reflect(rd, sn);
    vec3 srefr  = refract(rd, sn, 0.9);
    vec3 ssky = sfre*skyColor(sp, sref);

    if (de > 0.0) {
      col = ssky;
    } else {
      col = 0.5*sfre*ssky;
      vec3 col1 = (1.0-sfre)*render1(sp+srefr*initt, srefr);
      col += col1;
    }

  }

  return col;
}

vec3 effect(vec2 p) {
  vec3 ro = 0.8*vec3(0.0, 4.0, 5.0);
  const vec3 la = vec3(0.0, 0.0, 0.0);
  const vec3 up = vec3(0.0, 1.0, 0.0);
  float a = 0.5*(-0.5+0.5*sin(0.123*mat_orbit_time));
  float b = 0.1*mat_orbit_time;

    vec2 m = mat_orbit.xy;
    // Get angle from mouse position

    a -=2.0*m.y;
    b -=2.0*m.x;



  // if (iMouse.x > 0.0) {
  //   vec2 m = (2.0*iMouse.xy-RENDERSIZE.xy)/RENDERSIZE.y;
  //   // Get angle from mouse position
  //   a =-2.0*m.y;
  //   b =-2.0*m.x;
  // }

  ro.yz *= ROT(a);
  ro.xz *= ROT(b);

  vec3 ww = normalize(la - ro);
  vec3 uu = normalize(cross(up, ww ));
  vec3 vv = normalize(cross(ww,uu));
  float fov = tan(TAU/6.0);
  vec3 rd = normalize(-p.x*uu + p.y*vv + fov*ww);

  vec3 col = render0(ro, rd);

  return col;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 q = uv + vec2(0.);
    vec2 p = uv;
    // p.x *= RESOLUTION.x/RESOLUTION.y;
    vec3 col = vec3(0.0);
    col = effect(p);
    col *= smoothstep(0.0, 4.0, mat_time);
    col = aces_approx(col) * pow(mat_gain,2.);
    col = sRGB(col);
    out_color = vec4(col, 1.0);

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
