/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Passion, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/MllSRr",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
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
            "NAME": "mat_uv_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Fractal/Magic",
            "NAME": "mat_magic",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.325
        },


        {
            "LABEL": "Fractal/Cycle 1",
            "NAME": "mat_cycle_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Cycle 2",
            "NAME": "mat_cycle_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Cycle 3",
            "NAME": "mat_cycle_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Fractal/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 32,
            "DEFAULT": 16
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
            "LABEL": "Animation/Counter UV",
            "NAME": "mat_anim_counter_uv",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Animation/X Adjust",
            "NAME": "mat_anim_x_adjust",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.73
        },
        {
            "LABEL": "Animation/Y Adjust",
            "NAME": "mat_anim_y_adjust",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": -0.03
        },



        {
            "LABEL": "Wobble/Animate",
            "NAME": "mat_uv_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Wobble/Zoom Factor",
            "NAME": "mat_uv_zoom_factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wobble/Rot Factor",
            "NAME": "mat_uv_rot_factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wobble/XY Factor",
            "NAME": "mat_uv_xy_factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Wobble/Speed",
            "NAME": "mat_uv_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Wobble/BPM Sync",
            "NAME": "mat_uv_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wobble/Reverse",
            "NAME": "mat_uv_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Wobble/Offset",
            "NAME": "mat_uv_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Wobble/Strob",
            "NAME": "mat_uv_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_scroll_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_scroll_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_scroll_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_scroll_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Dark Mode",
            "NAME": "mat_color_dark",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },


        {
            "LABEL": "Color/Cycle 1",
            "NAME": "mat_color_cycle_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color/Cycle 2",
            "NAME": "mat_color_cycle_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color/Cycle 3",
            "NAME": "mat_color_cycle_3",
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
            "NAME": "mat_uv_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_uv_speed",
                "speed_curve":2,
                "reverse": "mat_uv_reverse",
                "strob" : "mat_uv_strob",
                "bpm_sync": "mat_uv_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 0.5;

float mat_uv_time = (mat_uv_time_source - mat_uv_offset) * 0.25;

float mat_scroll_time = (mat_scroll_time_source - mat_scroll_offset) * 0.25;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


// variation of https://www.shadertoy.com/view/4ljGDd by dgreensp

// const int MAGIC_BOX_ITERS = 16;
// const float MAGIC_BOX_MAGIC = 0.325;

float magicBox(vec3 p) {
    // The fractal lives in a 1x1x1 box with mirrors on all sides.
    // Take p anywhere in space and calculate the corresponding position
    // inside the box, 0<(x,y,z)<1
    p = 1.0 - abs(1.0 - mod(p, 2.0)) * mat_cycle_2;

    float lastLength = length(p) * mat_cycle_3;
    float tot = 0.0;
    // This is the fractal.  More iterations gives a more detailed
    // fractal at the expense of more computation.
    for (int i=0; i < mat_iterations; i++) {
      // The number subtracted here is a "magic" paremeter that
      // produces rather different fractals for different values.
      p = abs(p)/(lastLength*lastLength) - mat_magic;
      float newLength = length(p);
      tot += abs(newLength-lastLength);
      lastLength = newLength;
    }

    return tot;
}

// A random 3x3 unitary matrix, used to avoid artifacts from slicing the
// volume along the same axes as the fractal's bounding box.
const mat3 M = mat3(0.28862355854826727, 0.6997227302779844, 0.6535170557707412,
                    0.06997493955670424, 0.6653237235314099, -0.7432683571499161,
                    -0.9548821651308448, 0.26025457467376617, 0.14306504491456504);


mat2 mat_rot(float deg)
{
    return mat2(cos(deg),-sin(deg),
                sin(deg), cos(deg));

}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_uv_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;

    float uv_time = mat_uv_time;

    if (!mat_uv_animate) {
        uv_time = 0.;
    }

    uv*=3.5+.5-sin(uv_time*.75*mat_uv_zoom_factor);   //zoom in and out

    ////uv*=4.;
    // scroll a certain number of screenfuls/second
    uv-=.5;                  // Center

    uv*=mat_rot(sin(-uv_time*1.6*mat_uv_rot_factor)/3.); // a bit of rotation
    uv.y -= uv_time*1.8*mat_uv_xy_factor;           // move uv y coordinate
    uv.x += sin(uv_time*.12*mat_uv_xy_factor)*21.;  // move uv x coordinate side to side

    // Rotate uv onto the random axes given by M, and scale
    // it down a bit so we aren't looking at the entire
    // 1x1x1 fractal volume.  Making the coefficient smaller
    // "zooms in", which may reduce large-scale repetition
    // but requires more fractal iterations to get the same
    // level of detail.

    // counteract the fractal movement
    // the fractal animation itself causes a UV shift
    // I wasn't able to find an exact offset, so it is adjustable instead
    if (mat_anim_counter_uv) {
        uv.x += mat_time * mat_anim_x_adjust;
        uv.y += mat_time * mat_anim_y_adjust;
    }

    if (mat_scroll_animate) {
        float shift_time_x = mat_scroll_time * cos(2.*PI * mat_scroll_angle/360.0);
        float shift_time_y = mat_scroll_time * sin(2.*PI * mat_scroll_angle/360.0);
        uv.x -= shift_time_x;
        uv.y -= shift_time_y;

    }

    vec3 p = 0.5*M*vec3(uv, 0.0);

    float r = magicBox(p-mat_time*.25);

    // Scale to taste.  Also consider non-linear mappings.
    r *= 0.05245 * mat_cycle_1;      // try .0245
    // coloring

    out_color = vec4(vec3(cos(r*r-3.3 * mat_color_cycle_1),cos(r+r+r+2.49 * mat_color_cycle_2),cos(r+r+r+r-3.9 * mat_color_cycle_3)),1.0);


    if (mat_color_dark) {
        out_color.rgb = sqrt(out_color.rgb);
        out_color.rgb = normalize(out_color.rgb);
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

    if (!mat_color_dark) {
        out_color.rgb = sqrt(out_color.rgb);
    }

    // Apply brightness
    out_color.rgb += mat_brightness;

    return out_color;
}
