/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Plento, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/Wsjfzc",

    "VSN": "1.1",

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
            "LABEL": "UV/Mirror",
            "NAME": "mat_mirror",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Boxes/Depth",
            "NAME": "mat_depth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Boxes/Size",
            "NAME": "mat_box_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Boxes/Blend",
            "NAME": "mat_blend",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Boxes/Translucense",
            "NAME": "mat_translucense",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Boxes/Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Boxes/Glow Iter",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 300,
            "DEFAULT": 100
        },


        {
            "LABEL": "XY Movement/Direction",
            "NAME": "mat_angle",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "XY Movement/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "XY Movement/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "XY Movement/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "XY Movement/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "XY Movement/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Z Movement/Speed",
            "NAME": "mat_z_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Z Movement/BPM Sync",
            "NAME": "mat_z_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Z Movement/Reverse",
            "NAME": "mat_z_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Z Movement/Offset",
            "NAME": "mat_z_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Z Movement/Strob",
            "NAME": "mat_z_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Apply Background",
            "NAME": "mat_apply_background",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
        {
            "NAME": "mat_z_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_z_speed",
                "speed_curve":2,
                "reverse": "mat_z_reverse",
                "strob" : "mat_z_strob",
                "bpm_sync": "mat_z_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 16.;
float mat_z_time = mat_z_time_source - mat_z_offset * 16.;

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


#define R vec2(1024.)

float box( vec3 p, vec3 b ){
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

float map(vec3 rp){
    vec3 id = floor(rp/7.);

    float altz = mod(id.z, 2.);
    float altx = mod(id.x, 2.);

    float time_x = mat_time * cos(PI * mat_angle);
    float time_y = mat_time * sin(PI * mat_angle);

    if (mat_mirror) {
        if(altz == 0.)
            rp.y += time_y;
        else
            rp.y -= time_y;

        if(altx == 0.)
            rp.x += time_x;
        else
            rp.x -= time_x;

    } else {
        rp.y += time_y;
        rp.x += time_x;
    }



    rp = mod(rp, vec3(7.))-vec3(7.)*0.5;

    return (box(rp, mat_box_size*vec3(1.))-.2) / mat_blend;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;

    vec3 rd = normalize(vec3(uv, 0.8));

    vec3 ro = vec3(0.);
    ro.z+=mat_z_time*2.;
    vec3 col = vec3(0);

    float t = 0., d = 0., td = 0.;

    const float h = 0.1;

    vec3 p = vec3(0);

    for(int i = 0; i < mat_iterations; i++){
        p = ro + rd*t;
        d = abs(map(p));

        d = max(d, 0.003 * pow(mat_translucense, 4.));

        if(d < h){
            float ld = h - d;
            float w = (1. - td)*ld;
            td += w;

        }
        col += mat_glow*0.5 + 0.5*cos(vec3(0.5, 1., 0.1)*(t+ro.z)+vec3(1., 0., 3.) );

        t += d*.7;
        if(t > 45. * mat_depth){
            t = 45. * mat_depth;
            break;
        }

    }


    col*=0.012;
    col = pow(col, vec3(5));
    col = 1.-exp(-col*2.);

    // col *= 1.-abs(uv.y*1.2);

    out_color = vec4(sqrt(clamp(col, .0, 1.)), 1.);

    if (mat_apply_background) {
        out_color = mix(mat_back_color, out_color, mat_luma(col));
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
