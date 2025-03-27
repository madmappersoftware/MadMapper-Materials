/*{
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/7sSfRG",

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
            "LABEL": "Voronoi/Scale",
            "NAME": "mat_voronoi_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Voronoi/Offset",
            "NAME": "mat_voronoi_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },


        {
            "LABEL": "Voronoi/Align",
            "NAME": "mat_align",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Voronoi/Distort 1",
            "NAME": "mat_distort1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Voronoi/Distort 2",
            "NAME": "mat_distort2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Voronoi/Seed",
            "NAME": "mat_seed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Voronoi/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Voronoi/Limit",
            "NAME": "mat_limit",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 10,
            "DEFAULT": 5
        },
        {
            "LABEL": "Voronoi/Transparency",
            "NAME": "mat_transparent",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Voronoi/Detail",
            "NAME": "mat_detail",
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

         {
            "LABEL": "Color/Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

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

// License CC0: Voronoi Glass Panes
//  Tinkering with the voronoi pattern from a Shane shader on saturday evening
//  Kind of hackish overall but that never stopped me from sharing a shader before
//  Shane shader, it's worth a look: https://www.shadertoy.com/view/Mld3Rn

// #define RESOLUTION  RENDERSIZE

#define TAU         (2.0*PI)
#define PI_2        (0.5*3.141592654)
#define ROT(a)      mat2(cos(a), sin(a), -sin(a), cos(a))
#define DOT2(x)     dot(x,x)

const float planeDist = 1.0-0.25;

float g_hmul = 1.0;

// License: Unknown, author: nmz (twitter: @stormoid), found: https://www.shadertoy.com/view/NdfyRM
float sRGB(float t) { return mix(1.055*pow(t, 1./2.4) - 0.055, 12.92*t, step(t, 0.0031308)); }
// License: Unknown, author: nmz (twitter: @stormoid), found: https://www.shadertoy.com/view/NdfyRM
vec3 sRGB(in vec3 c) { return vec3 (sRGB(c.x), sRGB(c.y), sRGB(c.z)); }

// License: WTFPL, author: sam hocevar, found: https://stackoverflow.com/a/17897228/418488
const vec4 hsv2rgb_K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
// vec3 hsv2rgb(vec3 c) {
//   vec3 p = abs(fract(c.xxx + hsv2rgb_K.xyz) * 6.0 - hsv2rgb_K.www);
//   return c.z * mix(hsv2rgb_K.xxx, clamp(p - hsv2rgb_K.xxx, 0.0, 1.0), c.y);
// }
// License: WTFPL, author: sam hocevar, found: https://stackoverflow.com/a/17897228/418488
//  Macro version of above to enable compile-time constants
#define HSV2RGB(c)  (c.z * mix(hsv2rgb_K.xxx, clamp(abs(fract(c.xxx + hsv2rgb_K.xyz) * 6.0 - hsv2rgb_K.www) - hsv2rgb_K.xxx, 0.0, 1.0), c.y))

float zoomOuter = 1.0;
float zoomInner = 0.2;

float tanh_approx(float x) {
//  return tanh(x);
  float x2 = x*x;
  return clamp(x*(27.0 + x2)/(27.0+9.0*x2), -1.0, 1.0);
}

// License: Unknown, author: Unknown, found: don't remember
vec4 alphaBlend(vec4 back, vec4 front) {
  float w = front.w + back.w*(1.0-front.w);
  vec3 xyz = (front.xyz*front.w + back.xyz*back.w*(1.0-front.w))/w;
  return w > 0.0 ? vec4(xyz, w) : vec4(0.0);
}

// License: Unknown, author: Unknown, found: don't remember
vec3 alphaBlend(vec3 back, vec4 front) {
  return mix(back, front.xyz, front.w);
}

// License: Unknown, author: Unknown, found: don't remember
float hash(float co) {
  return fract(sin(co*12.9898) * 13758.5453) * mat_seed;
}

vec2 hash2(vec2 p) {
  p = vec2(dot (p, vec2 (127.1, 311.7)), dot (p, vec2 (269.5, 183.3)));
  return fract (sin (p)*43758.5453123) * (1. - mat_align);
}

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/articles/distfunctions2d
float hex(vec2 p, float r ) {
  const vec3 k  = 0.5*vec3(-sqrt(3.0), 1.0, sqrt(4.0/3.0));
  p = abs(p);
  p -= 2.0*min(dot(k.xy,p),0.0)*k.xy;
  p -= vec2(clamp(p.x, -k.z*r, k.z*r), r);
  return length(p)*sign(p.y);
}

vec3 offset(float z) {
  float a = z;
  vec2 p = -0.1*(vec2(cos(a), sin(a*sqrt(2.0))) + vec2(cos(a*sqrt(0.75)), sin(a*sqrt(0.5))));
  return vec3(p, z);
}

vec3 doffset(float z) {
  float eps = 0.05;
  return (offset(z + eps) - offset(z - eps))/(2.0*eps);
}

vec3 ddoffset(float z) {
  float eps = 0.05;
  return (doffset(z + eps) - doffset(z - eps))/(2.0*eps);
}

vec3 skyColor(vec3 ro, vec3 rd) {
  float ld = max(dot(rd, vec3(0.0, 0.0, 1.0)), 0.0);
  vec3 scol = HSV2RGB(vec3(0.1, 0.25, 0.9));
  return scol*tanh_approx(3.0*pow(ld, 100.0)) * pow(mat_glow,1.5);
}

float voronoi2(vec2 p){
  vec2 g = floor(p), o; p -= g;

  vec3 d = vec3(1);

  for(int y = -1; y <= 1; y++){
    for(int x = -1; x <= 1; x++){
      o = vec2(x, y);
      o += hash2(g + o) - p;
      d.z = dot(o, o);
      d.y = max(d.x, min(d.y, d.z));
      d.x = min(d.x, d.z);
    }
  }

  return max(d.y/1.2 - d.x, 0.0)/1.2 * mat_distort1;
}

float hf2(vec2 p) {
  float zo = zoomOuter;
  float zi = zoomInner;

  p /= zo;
  p /= zi;

  float d = -voronoi2(p);
  d *= zi*zo;

  float h = 0.2*tanh_approx(3.0*smoothstep(0.0, 1.0*zo*zi, -d));

  return h*zo*zi * mat_distort2;
}

float height(vec2 p) {
  return -hf2(p)*g_hmul * mat_distort2;
}

vec3 normal(vec2 p, float eps) {
  vec2 v;
  vec2 w;
  vec2 e = vec2(0.00001, 0);

  vec3 n;
  n.x = height(p + e.xy) - height(p - e.xy);
  n.y = height(p + e.yx) - height(p - e.yx);
  n.z = -2.0*e.x;

  return normalize(n);
}

vec4 plane(vec3 pro, vec3 ro, vec3 rd, vec3 pp, vec3 off, float aa, float n_, out vec3 pnor) {
  float h0 = hash(n_);
  float h1 = fract(7793.0*h0);
  float h2 = fract(6337.0*h0);

  vec2 p = (pp-off*vec3(1.0, 1.0, 0.0)).xy * mat_fov;
  const float s = 1.0;
  vec3 lp1 = vec3(5.0,  1.0, 0.0)*vec3(s, 1.0, s)+pro;
  vec3 lp2 = vec3(-5.0, 1.0, 0.0)*vec3(s, 1.0, s)+pro;
  const float hsz = 0.2;
  float hd = hex(p.yx, hsz);

  g_hmul = smoothstep(0.0, 0.125, (hd-hsz/2.0));

  p += vec2(h0,h1)*20.0;
  p *= mix(0.5, 1.0, h2);


  vec2 offset = mat_voronoi_offset;
  offset += vec2(0.5);
  offset.x = 1.-offset.x;
  offset -= vec2(0.5);

  // p += vec2(0.5);
  p *= pow(mat_voronoi_scale,2.);
  p += offset;
  float he  = height(p);
  vec3  nor = normal(p,2.0*aa);
  vec3 po   = pp;

  pnor = nor;

  vec3 ld1 = normalize(lp1 - po);
  vec3 ld2 = normalize(lp2 - po);

  float diff1 = max(dot(nor, ld1), 0.0);
  float diff2 = max(dot(nor, ld2), 0.0);
  diff1 = ld1.z*nor.z;;

  vec3  ref   = reflect(rd, nor);
  float ref1  = max(dot(ref, ld1), 0.0);
  float ref2  = max(dot(ref, ld2), 0.0);

  const vec3 mat   = HSV2RGB(vec3(0.55, 0.45, 0.05));
  const vec3 lcol1 = HSV2RGB(vec3(0.6, 0.5, 0.9));
  const vec3 lcol2 = HSV2RGB(vec3(0.1, 0.65, 0.9));

  float hf = smoothstep(0.0, 0.0002, -he);
  vec3 lpow1 = 1.0*lcol1/DOT2(ld1);
  vec3 lpow2 = 1.0*lcol2/DOT2(ld2);
  vec3 col = vec3(0.0);
  col += hf*mat*diff1*diff1*lpow1;
  col += hf*mat*diff2*diff2*lpow2;
  float spes = 20.0;
  col += pow(ref1, spes)*lcol1;
  col += pow(ref2, spes)*lcol2;

  float t = 1.0;
  t *= smoothstep(aa, -aa, -(hd-hsz/4.0));
  t *= mix(1.0, 0.75, hf * pow(mat_transparent,0.9));

  return vec4(col, t);
}

vec3 color(vec3 ww, vec3 uu, vec3 vv, vec3 pro, vec3 ro, vec2 p) {
  float lp = length(p);
  vec2 np = p + 1.0/vec2(1000. * mat_detail);
  float rdd = 2.0+tanh_approx(length(0.25*p));

  vec3 rd = normalize(p.x*uu + p.y*vv + rdd*ww);
  vec3 nrd = normalize(np.x*uu + np.y*vv + rdd*ww);

  int furthest = mat_limit;
  int fadeFrom = max(furthest-2, 0);

  float fadeDist = planeDist*float(furthest - fadeFrom);
  float nz = floor(ro.z / planeDist);

  vec3 skyCol = skyColor(ro, rd);

  vec4 acol = vec4(0.0);
  const float cutOff = 0.98;
  bool cutOut = false;

  // Steps from nearest to furthest plane and accumulates the color
  for (int i = 1; i <= furthest; ++i) {
    float pz = planeDist*nz + planeDist*float(i);

    float pd = (pz - ro.z)/rd.z;

    if (pd > 0.0 && acol.w < cutOff) {
      vec3 pp = ro + rd*pd;
      vec3 npp = ro + nrd*pd;

      float aa = 3.0*length(pp - npp);

      vec3 off = offset(pp.z);

      vec3 pnor = vec3(0.0);
      vec4 pcol = plane(pro, ro, rd, pp, off, aa, nz+float(i), pnor);

      vec3 refr = refract(rd, pnor, 1.0-0.075);
      if (pcol.w > (1.0-cutOff)&&refr != vec3(0.0)) {
        rd = refr;
      }

      float dz = pp.z-ro.z;
      const float fi = -0.;
      float fadeIn = smoothstep(planeDist*(float(furthest)+fi), planeDist*(float(fadeFrom)-fi), dz);
      float fadeOut = smoothstep(0.0, planeDist*0.1, dz);
      pcol.w *= fadeOut*fadeIn;

      acol = alphaBlend(pcol, acol);
    } else {
      cutOut = true;
      acol.w = acol.w > cutOff ? 1.0 : acol.w;
      break;
    }

  }

  vec3 col = alphaBlend(skyCol, acol);
// To debug cutouts due to transparency
//  col += cutOut ? vec3(1.0, -1.0, 0.0) : vec3(0.0);
  return col;
}

vec3 effect(vec2 p, vec2 q) {
  float z   = 0.33*planeDist*mat_time;
  vec3 pro  = offset(z-1.0);
  vec3 ro   = offset(z);
  vec3 dro  = doffset(z);
  vec3 ddro = ddoffset(z);

  vec3 ww = normalize(dro);
  vec3 uu = normalize(cross(normalize(vec3(0.0,1.0,0.0)+ddro), ww));
  vec3 vv = cross(ww, uu);

  vec3 col = color(ww, uu, vv, pro, ro, p);

  return col;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 p = uv;
    vec2 q = texCoord;

    vec3 col = effect(p, q);
    // col *= smoothstep(0.0, 4.0, mat_time);
    col = sRGB(col) * mat_gain;

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
