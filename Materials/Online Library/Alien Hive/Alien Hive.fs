/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "Alien Hive generator. From https://www.shadertoy.com/view/ttt3zX",

    "VSN": "1.1",

    "INPUTS": [


        {
            "LABEL": "Alien Hive/Reps",
            "NAME": "mat_reps",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 30,
            "DEFAULT": 11
        },

        {
            "LABEL": "Alien Hive/Max Iter",
            "NAME": "mat_max_iter",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 200,
            "DEFAULT": 120
        },



        {
            "LABEL": "Alien Hive/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Alien Hive/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

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
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_hue_shift",
            "LABEL": "Color/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        }


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
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * mat_offset_scale * 100.;

// --------[ Original ShaderToy begins here ]---------- //
// Alien engine
// Based upon: https://www.shadertoy.com/view/4ds3zn



#define TAU        (2.0*PI)
#define TOLERANCE  0.0003
// #define mat_reps       11
#define mat_max_dist   20.
// #define mat_max_iter   120

const vec3  green  = vec3(1.5, 2.0, 1.0);
const vec3  dark   = vec3(0.2);

void rot(inout vec2 p, float a) {
  float c = cos(a);
  float s = sin(a);
  p = vec2(c*p.x + s*p.y, -s*p.x + c*p.y);
}

void r45(inout vec2 p) {
    p = (p + vec2(p.y, -p.x))*sqrt(0.5);
}

float apollian(vec3 p, float tolerance, out int layer) {
  const float s = 1.9;
  float scale = 1.0;

  float r = 0.2;
  vec3 o = vec3(0.22, 0.0, 0.0);

  float d = 0.0;

  for(int i = 0; i < mat_reps; ++i) {
    p = (-1.00 + 2.0*fract(0.5*p+0.5));
//    rot(p.xz, -float(i)*PI/4.0);
    r45(p.xz);

    float r2 = dot(p,p) + 0.0;
    float k = s/r2;
    float ss = pow((1.0 + float(i)), -0.15);
    p *= pow(k, ss);
    scale *= pow(k, -ss*ss);
    d = 0.25*abs(p.y)*scale;
    layer = i;
    if(abs(d) < tolerance) break;
  }

  return d;
}

float df(vec3 p, float tolerance, out int layer) {
  float d = apollian(p, tolerance, layer);
  return d;
}


float intersect(vec3 ro, vec3 rd, out int iter, out int layer) {
  float res;
  float t = 1.6;
  iter = mat_max_iter;

  for(int i = 0; i < mat_max_iter; ++i) {
    vec3 p = ro + rd * t;
    float tolerance = TOLERANCE * t;
    res = df(p, tolerance, layer);
    if(res < tolerance || res > mat_max_dist) {
      iter = i;
      break;
    }
    t += res;
  }

  if(res > mat_max_dist) t = -1.;

  return t;
}

float ambientOcclusion(vec3 p, vec3 n) {
  float stepSize = 0.012;
  float t = stepSize;

  float oc = 0.0;

  int layer;

  for(int i = 0; i < 12; i++) {
    float tolerance = TOLERANCE * t;
    float d = df(p + n * t, tolerance, layer);
    oc += t - d;
    t += stepSize;
  }

  return clamp(oc, 0.0, 1.0);
}

vec3 normal(in vec3 pos) {
  vec3 eps = vec3(.001,0.0,0.0);
  vec3 nor;
  int layer;
  float tolerance = TOLERANCE * eps.x;
  nor.x = df(pos+eps.xyy, tolerance, layer) - df(pos-eps.xyy, tolerance, layer);
  nor.y = df(pos+eps.yxy, tolerance, layer) - df(pos-eps.yxy, tolerance, layer);
  nor.z = df(pos+eps.yyx, tolerance, layer) - df(pos-eps.yyx, tolerance, layer);
  return normalize(nor);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);


    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= mat_scale * 4.;

    uv.y = 1. - uv.y;

    vec2 q=uv + vec2(mat_scale * 8.);

    vec3 la = vec3(0.0,0.5,0.0);
    vec3 ro = vec3(2.5, 1.5, 0.0);
    rot(ro.xz, mat_time/40.0);

    vec3 cf = normalize(la-ro);
    vec3 cs = normalize(cross(cf,vec3(0.0,1.0,0.0)));
    vec3 cu = normalize(cross(cs,cf));
    vec3 rd = normalize(uv.x*cs + uv.y*cu + 3.0*cf);

    vec3 bg = mix(dark*0.25, dark*0.5, smoothstep(-1.0, 1.0, uv.y));
    vec3 col = bg;

    vec3 p=ro;

    int iter = 0;
    int layer = 0;

    float t = intersect(ro, rd, iter, layer);

    if(t > -1.0) {
      p = ro + t * rd;
      vec3 n = normal(p);
      float fake = float(iter)/float(mat_max_iter);
      float fakeAmb = exp(-fake*fake*4.0);
      float amb = ambientOcclusion(p, n);


      vec3 dif;

      float ll = length(p);

      if (layer == 0)
      {
        dif = 0.75*green;
      } else {
        dif = green*pow((1.0 + 0.5*cos(-PI*2.0*float(layer)/float(mat_reps) + mat_time*0.25 - 0.5*PI*ll)), 4.0)/pow(float(layer), 1.5);
      }


      const float fogPeriod = TAU*2.0;
      float fogHeight = 0.25 + 0.325*(abs(p.y) + 0.125*(sin(fogPeriod*p.x) * cos(fogPeriod*p.z)));
      float dfog = (fogHeight - ro.y)/rd.y;
      float fogDepth = t > dfog && dfog > 0.0 ? t - dfog : 0.0;
      float fogFactor = exp(-fogDepth*4.0);

      col = dif;
      col *= vec3(mix(1.0, 0.125, pow(amb, 3.0)))*vec3(fakeAmb);
      col = mix(green*0.5, col, fogFactor);
      col = mix(bg, col, exp(-0.0125*t*t));
    }

    float pp = 1.0 - (1.0 - step(0.5, q.y))*smoothstep(0.85, 1.3, length(2.0*q-1.0));


    col *= pp;


    out_color=vec4(col.x,col.y,col.z,1.0);




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
