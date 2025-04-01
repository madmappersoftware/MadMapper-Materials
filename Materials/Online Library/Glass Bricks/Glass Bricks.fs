/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "104, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/tlsSR7",

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
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Flicker + Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "Label": "Flicker + Scroll/Flicker",
            "NAME": "mat_layers",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Flicker + Scroll/Drift",
            "NAME": "mat_drift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "Label": "Flicker + Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },




        {
            "LABEL": "Flicker + Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Flicker + Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Flicker + Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Flicker + Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Flicker + Scroll/Offset Scale",
            "NAME": "mat_shift_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Flicker + Scroll/Strob",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Rotate/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Rotate/Animate",
            "NAME": "mat_rotate_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Bounce",
            "NAME": "mat_rotate_bounce",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Range",
            "NAME": "mat_bounce_range",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Strob",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Distort/Animate",
            "NAME": "mat_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Distort/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Distort/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Distort/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Distort/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Distort/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Vignette",
            "NAME": "mat_vignette",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
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
            "NAME": "mat_shift_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shift_speed",
                "speed_curve":2,
                "reverse": "mat_shift_reverse",
                "strob" : "mat_shift_strob",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_rotate_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rotate_speed",
                "speed_curve":2,
                "reverse": "mat_rotate_reverse",
                "strob" : "mat_rotate_strob",
                "bpm_sync": "mat_rotate_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8.;

float mat_shift_time = mat_shift_time_source - mat_shift_offset * 8. * mat_shift_offset_scale;

float mat_rotate_time = (mat_rotate_time_source - mat_rotate_offset * 32.) * 0.5;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// credits: Dave_Hoskins Hash functions: https://www.shadertoy.com/view/4djSRW

float layers = 4. * mat_layers; //4.
float layerDrift = 0.3 * mat_drift; //0.3

// const float PI = 3.141592654;

mat2 rot2D(float r){
    float c = cos(r), s = sin(r);
    return mat2(c, s, -s, c);
}
float nsin(float x) {return sin(x)*.5+.5; }
vec2 nsin(vec2 x) {return sin(x)*.5+.5; }
vec3 hash32(vec2 p){
    vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yxz+19.19);
    return fract((p3.xxy+p3.yzz)*p3.zyx);
}
// returns { RGB, dist to edge (0 = edge, 1 = center) }
vec4 disco(vec2 uv) {
    float v = abs(cos(uv.x * PI * 2.) + cos(uv.y *PI * 2.)) * .5;
    uv.x -= .5;
    vec3 cid2 = hash32(vec2(floor(uv.x - uv.y), floor(uv.x + uv.y))); // generate a color
    return vec4(cid2, v);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    vec2 uv_orig = uv;
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



    // if (mat_shift_animate) {

    //     if (mat_shift_fixed_dir) {

    //         uv.x -= shift_time_x;
    //         uv.y -= shift_time_y;
    //     } else {
    //         uv -= vec2(cos(mat_shift_time/3.)*1.5, -.5*mat_shift_time);
    //     }

    // }

    if (!mat_rotate_animate) {
        mat_rotate_time = 0.;
    }
    if (!mat_shift_animate) {
        mat_shift_time = 0.;
    }

    if (!mat_animate) {
        mat_time = 0.;
    }



    float shift_time_x = mat_shift_time * cos(PI * mat_shift_angle);
    float shift_time_y = mat_shift_time * sin(PI * mat_shift_angle);


    // float t = (mat_time+200.) * .6; //t = 0.;
    // float t = (mat_time+200.) * .6; //t = 0.;
    // uv *= 21.;

    uv *= 25.;

    if (mat_rotate_bounce) {
        uv *= rot2D(sin(mat_rotate_time*2.)*.03 * pow(mat_bounce_range, 2.));
    } else {
        uv *= rot2D(mat_rotate_time*0.125);
    }

    // uv.x += mat_shift_time*3.;

    vec2 uv_offset;

    uv_offset = vec2(shift_time_x, shift_time_y) * layerDrift;

    uv += uv_offset*3.;

    // start with tile pattern
    vec2 uv2 = uv;

    // uv2 += mat_shift_time*layerDrift;// sync with below; pattern drift
    uv2 += uv_offset;// sync with below; pattern drift

    uv2 = sin(uv2-PI*.5)*.5+.5;// sync with below. actually i have no idea why -PI/2

    out_color.r = min(uv2.x,uv2.y);// grid pattern
    out_color = vec4(pow(out_color.r, .4));
    out_color = clamp(out_color, 0.,.6)/.6;// plateau
    // layer in bricks
    for(float i = 0.; i <=layers; ++i) {

        // uv += sin(uv+mat_shift_time*layerDrift)*(1.+sin(mat_time)*.3);
        uv += sin(uv+uv_offset)*(1.+sin(mat_time)*.3);

        vec4 d = disco(uv);
        d.a = pow(d.a, .2);//sin(t*1.2+i)*.5+.5
        // d.a = sin(mat_shift_time*1.2+i)*.5+.5;
        out_color *= clamp(d*d.a,.25, 1.);
    }
    // post
    out_color = clamp(out_color,.0,1.);

    // vec2 N = (gl_FragCoord.xy / R )- .5;// norm coords
    vec2 N = uv_orig;

    out_color = 1.-pow(1.-out_color, vec4((layers - .5) * 12.));// curve

    // out_color.rgb += hash32(gl_FragCoord.xy + mat_time).r*.07;//noise

    out_color.rgb += hash32(uv_orig + mat_time).r*.07;//noise


    N *= mat_vignette;
    // out_color *= 1.1-step(.4,abs(N.y));
    out_color *= (1.0-dot(N,N*2.));// vingette
    out_color.a = 1.;




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
