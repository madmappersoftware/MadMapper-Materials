/*{
    "CREDIT": "mrange, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/fd33zn",

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
            "LABEL": "UV/Flip X",
            "NAME": "mat_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "UV/Flip Y",
            "NAME": "mat_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Torus/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Torus/Distance",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Torus/Tilt",
            "NAME": "mat_tilt",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.7
        },
        {
            "LABEL": "Torus/Camera 1",
            "NAME": "mat_cam1",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Torus/Camera 2",
            "NAME": "mat_cam2",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },

        {
            "LABEL": "Torus/Expand",
            "NAME": "mat_expand",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },




        {
            "LABEL": "Torus/Mod 1",
            "NAME": "mat_mod1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Torus/Mod 2",
            "NAME": "mat_mod2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Torus/Mod 3",
            "NAME": "mat_mod3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Torus/Mod 4",
            "NAME": "mat_mod4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Light/Position",
            "NAME": "mat_light_pos",
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Color/Glow",
            "NAME": "mat_light",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "DEFAULT": 0.0
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

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 2.;

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
    uv *= mat_scale * 2.;

    uv = mirrorUV(uv);



    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 2.;
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

    // if (mat_flip_x) {
    //     uv.x = 1. - uv.x;
    // }
    // if (mat_flip_y) {
    //     uv.y = 1. - uv.y;
    // }

    return uv;
}


// License CC0: Saturday Torus
//  Inspired by: https://www.istockphoto.com/photo/black-and-white-stripes-projection-on-torus-gm488221403-39181884


#define TAU         (2.0*PI)

#define ROT(a)      mat2(cos(a), sin(a), -sin(a), cos(a))
#define PCOS(x)     (0.5+0.5*cos(x))

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/articles/intersectors
float rayTorus(vec3 ro, vec3 rd, vec2 tor) {
  float po = 1.0;

  float Ra2 = tor.x*tor.x * mat_expand;
  float ra2 = tor.y*tor.y;

  float m = dot(ro,ro);
  float n = dot(ro,rd);

  // bounding sphere
  {
    float h = n*n - m + (tor.x+tor.y)*(tor.x+tor.y);
    if(h<0.0) return -1.0;

    // float t = -n-sqrt(h); // could use this to compute intersections from ro+t*rd
  }

  // find quartic equation
  float k = (m - ra2 - Ra2)/2.0;
  float k3 = n;
  float k2 = n*n + Ra2*rd.z*rd.z + k;
  float k1 = k*n + Ra2*ro.z*rd.z;
  float k0 = k*k + Ra2*ro.z*ro.z - Ra2*ra2;

  #ifndef TORUS_REDUCE_PRECISION
  // prevent |c1| from being too close to zero
  if(abs(k3*(k3*k3 - k2) + k1) < 0.01)
  {
    po = -1.0;
    float tmp=k1; k1=k3; k3=tmp;
    k0 = 1.0/k0;
    k1 = k1*k0;
    k2 = k2*k0;
    k3 = k3*k0;
  }
  #endif

  float c2 = 2.0*k2 - 3.0*k3*k3;
  float c1 = k3*(k3*k3 - k2) + k1;
  float c0 = k3*(k3*(-3.0*k3*k3 + 4.0*k2) - 8.0*k1) + 4.0*k0;


  c2 /= 3.0;
  c1 *= 2.0;
  c0 /= 3.0;

  float Q = c2*c2 + c0;
  float R = 3.0*c0*c2 - c2*c2*c2 - c1*c1;

  float h = R*R - Q*Q*Q;
  float z = 0.0;
  if(h < 0.0) {
    // 4 intersections
    float sQ = sqrt(Q);
    z = 2.0*sQ*cos(acos(R/(sQ*Q)) / 3.0);
  } else {
    // 2 intersections
    float sQ = pow(sqrt(h) + abs(R), 1.0/3.0);
    z = sign(R)*abs(sQ + Q/sQ);
  }
  z = c2 - z;

  float d1 = z   - 3.0*c2;
  float d2 = z*z - 3.0*c0;
  if(abs(d1) < 1.0e-4) {
    if(d2 < 0.0) return -1.0;
    d2 = sqrt(d2);
  } else {
    if(d1 < 0.0) return -1.0;
    d1 = sqrt(d1/2.0);
    d2 = c1/d1;
  }

  //----------------------------------

  float result = 1e20;

  h = d1*d1 - z + d2;
  if(h > 0.0) {
    h = sqrt(h);
    float t1 = -d1 - h - k3; t1 = (po<0.0)?2.0/t1:t1;
    float t2 = -d1 + h - k3; t2 = (po<0.0)?2.0/t2:t2;
    if(t1 > 0.0) result=t1;
    if(t2 > 0.0) result=min(result,t2);
  }

  h = d1*d1 - z - d2;
  if(h > 0.0) {
    h = sqrt(h);
    float t1 = d1 - h - k3;  t1 = (po<0.0)?2.0/t1:t1;
    float t2 = d1 + h - k3;  t2 = (po<0.0)?2.0/t2:t2;
    if(t1 > 0.0) result=min(result,t1);
    if(t2 > 0.0) result=min(result,t2);
  }

  return result;
}

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/articles/intersectors
vec3 torusNormal(vec3 pos, vec2 tor) {
  return normalize(pos*(dot(pos,pos)- tor.y*tor.y - tor.x*tor.x*vec3(1.0,1.0,-1.0)));
}

// License: Unknown, author: Unknown, found: don't remember
float tanh_approx(float x) {
  //  Found this somewhere on the interwebs
  //  return tanh(x);
  float x2 = x*x;
  return clamp(x*(27.0 + x2)/(27.0+9.0*x2), -1.0, 1.0);
}

vec3 color(vec2 p, vec2 q) {
  float rdd = 2.0 * mat_fov;
  vec3 ro  = 1.*vec3(0. + mat_cam2.x * 0.5, 0.75 * mat_dist, -0.2 + mat_cam2.y * 0.5);
  vec3 la  = vec3(0.0 + mat_cam1.x, 0.0  - pow(mat_tilt,2.), 0.2 - mat_cam1.y);
  vec3 up  = vec3(0.3, 0.0, 1.0);
  vec3 lp1 = ro;
  lp1.xy  *= ROT(0.85 + mat_light_pos.x * 2.);
  lp1.xz  *= ROT(-0.5 + mat_light_pos.y);

  vec3 ww = normalize(la - ro);
  vec3 uu = normalize(cross(up, ww));
  vec3 vv = normalize(cross(ww,uu));
  vec3 rd = normalize(p.x*uu + p.y*vv + rdd*ww);

  const vec2 tor = 0.55*vec2(1.0, 0.75);
  float td    = rayTorus(ro, rd, tor) * mat_mod4;
  vec3  tpos  = ro + rd*td;

  tpos.x *= mat_mod1;
  tpos.y *= mat_mod2;
  tpos.z *= mat_mod3;
  vec3  tnor  = -torusNormal(tpos, tor);
  vec3  tref  = reflect(rd, tnor);

  vec3  ldif1 = lp1 - tpos;
  float ldd1  = dot(ldif1, ldif1);
  float ldl1  = sqrt(ldd1);
  vec3  ld1   = ldif1/ldl1;
  vec3  sro   = tpos+0.05*tnor;
  float sd    = rayTorus(sro, ld1, tor);
  vec3  spos  = sro+ld1*sd;
  vec3  snor  = -torusNormal(spos, tor);

  float dif1  = max(dot(tnor, ld1), 0.0);
  float spe1  = pow(max(dot(tref, ld1), 0.0), 10.0);
  float r     = length(tpos.xy);
  float a     = atan(tpos.y, tpos.x)-PI*tpos.z/(r+0.5*abs(tpos.z))-(mat_time * TAU)/45.0;
  float s     = mix(0.05, 0.5, tanh_approx(2.0*abs(td-0.75)));
  vec3  bcol0 = vec3(0.3);
  vec3  bcol1 = vec3(0.025);
  vec3  tcol  = mix(bcol0, bcol1, smoothstep(-s, s, sin(9.0*a)));

  vec3 col = vec3(0.0);

  if (td > -1.0) {
    col += tcol*mix(0.2, 1.0, dif1/ldd1)+0.25*spe1;
    col *= sqrt(abs(dot(rd, tnor)));
  }

  if (sd < ldl1) {
    col *= mix(1.0, 0.0, pow(abs(dot(ld1, snor)), 3.0*tanh_approx(sd)));
  }

  return col;
}

// License: MIT, author: Inigo Quilez, found: https://iquilezles.org/www/index.htm
vec3 postProcess(vec3 col, vec2 q) {
  col = clamp(col, 0.0, 1.0);
  col = pow(col, 1.0/vec3(2.2));
  col = col*0.6+0.4*col*col*(3.0-2.0*col);
  col = mix(col, vec3(dot(col, vec3(0.33))), -0.4);
  col *=0.5 * mat_brightness+0.5*pow(19.0*q.x*q.y*(1.0-q.x)*(1.0-q.y),0.7) * mat_light;
  return col;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    if (mat_flip_x) {
        uv.x = 1. - uv.x;
    }
    if (mat_flip_y) {
        uv.y = 1. - uv.y;
    }

    vec2 q = texCoord;
    vec2 p = uv;

    // p.y -= 0.25;
    // p.x *= RESOLUTION.x/RESOLUTION.y;
    vec3 col = color(p, q);
    col = postProcess(col, q);
    out_color = vec4(col, 1.0);

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
