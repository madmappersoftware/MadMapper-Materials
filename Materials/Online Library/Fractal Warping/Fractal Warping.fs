/*{
    "CREDIT": "oneshade, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/tlGcDD",

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
            "LABEL": "Warp/Warp 1",
            "NAME": "mat_warp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Warp/Warp 2",
            "NAME": "mat_warp2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Glitch/Glitch 1",
            "NAME": "mat_glitch",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Glitch/Glitch 2",
            "NAME": "mat_glitch2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "LABEL": "Scroll/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },


        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },



        {
            "LABEL": "Color/Cycle",
            "NAME": "mat_cycle",
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
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 32. * mat_offset_scale) * 0.25;

float mat_shift_time = (mat_shift_time_source - mat_shift_offset) * 0.25;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


// Hue to RGB function from Fabrice's shadertoyunofficial blog:
// #define hue2rgb(hue) 0.6 + 0.6 * cos(6.3 * hue + vec3(0.0, 23.0, 21.0))
#define hue2rgb(hue) 0.6 + 0.6 * cos(6.3 * hue + pow(mat_cycle, 1.)*vec3(0.0, 23.0, 21.0))

// Hash from "Hash without Sine" by Dave_Hoskins (https://www.shadertoy.com/view/4djSRW):
// I modified it to try avoiding some annoying symmetry hash13() appears to have on the xy plane.
float noise(in vec3 p) {
    p = fract(p * 0.731 - p.x * 253.567);
    p += dot(p + p, p.yzx + 33.33);
    return fract((p.x + p.y) * p.z);
}

float snoise(in vec3 p) {
    vec3 cell = floor(p);
    vec3 local = fract(p);
    // local *= local * (3.0 - 2.0 * local);

    local *= local * ((3.0 - mat_glitch) - 2.0 * (local + mat_glitch2));

    float ldb = noise(cell);                       // Left, Down, Back
    float rdb = noise(cell + vec3(1.0, 0.0, 0.0)); // Right, Down, Back
    float ldf = noise(cell + vec3(0.0, 0.0, 1.0)); // Left, Down, Front
    float rdf = noise(cell + vec3(1.0, 0.0, 1.0)); // Right, Down, Front
    float lub = noise(cell + vec3(0.0, 1.0, 0.0)); // Left, Up, Back
    float rub = noise(cell + vec3(1.0, 1.0, 0.0)); // Right, Up, Back
    float luf = noise(cell + vec3(0.0, 1.0, 1.0)); // Left, Up, Front
    float ruf = noise(cell + 1.0);                 // Right, Up, Front

    return mix(mix(mix(ldb, rdb, local.x),
                   mix(ldf, rdf, local.x),
                   local.z),

               mix(mix(lub, rub, local.x),
                   mix(luf, ruf, local.x),
                   local.z),

               local.y);
}

float fnoise(in vec3 p, in float scale, in float octaves) {
    p *= scale;

    float value = 0.0;
    float nscale = 1.0;
    float tscale = 0.0;

    for (float octave=0.0; octave < octaves; octave++) {
        value += snoise(p) * nscale;
        tscale += nscale;
        nscale *= 0.5 * pow(mat_warp, 0.8);
        p *= 2.0 * mat_warp2;
    }

    return value / tscale;
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
    // uv_shift += vec2(0.5);
    // uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    // uv_shift -= vec2(0.5);

    uv += uv_shift;

    if (mat_shift_animate) {

        float shift_time_x = mat_shift_time * cos(PI * mat_shift_angle);
        float shift_time_y = mat_shift_time * sin(PI * mat_shift_angle);
        uv.x -= shift_time_x;
        uv.y -= shift_time_y;

    }

    float time = mat_time * 0.25;
    // iq's domain warping technique:
    float warp = 0.0;
    for (int i=0; i < 5; i++) {
        warp = fnoise(vec3(uv, time) + warp, 2.0, 5.0);
    }
    out_color = vec4(hue2rgb(warp), 1.0);


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
