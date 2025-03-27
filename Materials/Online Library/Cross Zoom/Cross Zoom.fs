/*{
    "CREDIT": "anastadunbar, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/MscSW8 and http://bit.ly/1RrlZYP",

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
            "LABEL": "Shape/Shape",
            "NAME": "mat_shape",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Zoom/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Zoom/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Zoom/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Zoom/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Zoom/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Zoom/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Zoom/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Rotate/Animate",
            "NAME": "mat_rot_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button",
        },
        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rot_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rot_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rot_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rot_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Offset Scale",
            "NAME": "mat_rot_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Rotate/Strob",
            "NAME": "mat_rot_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Rotate/Restart",
            "NAME": "mat_rot_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
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
            "NAME": "mat_rot_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rot_speed",
                "speed_curve":2,
                "reverse": "mat_rot_reverse",
                "strob" : "mat_rot_strob",
                "reset": "mat_rot_restart",
                "bpm_sync": "mat_rot_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 0.5;
float mat_rot_time = (mat_rot_time_source - mat_rot_offset * 8. * mat_rot_offset_scale) * 0.5;

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


vec2 rotate(float angle,vec2 position)
{
    mat2 matrix = mat2(cos(angle),-sin(angle),sin(angle),cos(angle));
    return position*matrix;
}
float logn(float a,float b) { return log(a)/log(b); }
#define clamps(x) clamp(x,0.,1.)
float texture1(vec2 uv2,vec2 uv,float in_scale,float id) {
    float c = 0.;
    //(uv2.y*2.)/(1.-in_scale); (1.-in_scale)/2.;
    uv2 -= .5;
    float thickness = in_scale/(1.+in_scale); float sharpness = 4000.;
    c = ((min(abs(uv2.x),abs(uv2.y))*2.)-thickness)*sharpness;
    if (mod(id,2.) == 0.) {
      c = -(((min(abs(uv2.y+uv2.x),abs(uv2.y-uv2.x))*2.)-thickness)*sharpness);
    }
    c = clamps(c);
    return c;
}
float texture2(vec2 uv, float time, float rot_time) {
    if (mat_rot_animate) {
        uv = rotate(rot_time,uv);
    }

    float fracttime = mod(time,2.);
    float in_scale = .07 * pow(mat_shape, 2.); //How deep?
    vec2 zuv = uv*pow(in_scale,fracttime); //Zooming
    float d = (max(abs(zuv.x),abs(zuv.y))*2.); //Square dist
    float id = floor(logn(d,in_scale));
    vec2 uv2 = (zuv/pow(in_scale,id))+.5;
    return texture1(uv2,uv,in_scale,id);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);



    float c = 0.;
    #define LOOPS 8.
    for (float i = 0.; i < LOOPS; i++) {
        c += texture2(uv+(vec2(cos(i),sin(i))*.0005),mat_time-(i*.003), mat_rot_time-(i*.003));
    }
    out_color = vec4(vec3(c/LOOPS),1.);

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    if (mat_luma(out_color.rgb) > 0.02) {
        out_color = mat_front_color;
    } else {
        out_color = mat_back_color;
    }

    return out_color;
}
