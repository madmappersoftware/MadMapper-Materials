/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "alro, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/ttlGDf",

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
            "LABEL": "Warp/Strength",
            "NAME": "mat_strength",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Warp/Scale XY",
            "NAME": "mat_scale_xy",
            "TYPE": "point2D",
            "MIN": [0.0,0.0],
            "MAX": [4.0,4.0],
            "DEFAULT": [1.0,1.0]
        },
        {
            "LABEL": "Warp/Step Size",
            "NAME": "mat_step_size",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Warp/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "Label": "Flow/Direction",
            "NAME": "mat_flow_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Flow/Speed",
            "NAME": "mat_flow_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Flow/BPM Sync",
            "NAME": "mat_flow_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Flow/Reverse",
            "NAME": "mat_flow_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Flow/Offset",
            "NAME": "mat_flow_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Flow/Offset Scale",
            "NAME": "mat_flow_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Flow/Strob",
            "NAME": "mat_flow_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise/Direction",
            "NAME": "mat_noise_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 270.0
        },
        {
            "LABEL": "Noise/Speed",
            "NAME": "mat_noise_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/BPM Sync",
            "NAME": "mat_noise_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise/Reverse",
            "NAME": "mat_noise_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise/Offset",
            "NAME": "mat_noise_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise/Offset Scale",
            "NAME": "mat_noise_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Noise/Strob",
            "NAME": "mat_noise_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise/Restart",
            "NAME": "mat_noise_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Color Fade/Animate",
            "NAME": "mat_fade_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Fade/Speed",
            "NAME": "mat_fade_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color Fade/BPM Sync",
            "NAME": "mat_fade_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Fade/Reverse",
            "NAME": "mat_fade_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Color Fade/Offset",
            "NAME": "mat_fade_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Color Fade/Offset Scale",
            "NAME": "mat_fade_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Color Fade/Strob",
            "NAME": "mat_fade_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },



        {
            "LABEL": "Color/Cycle",
            "NAME": "mat_color_cycle",
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
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
        },




    ],
    "GENERATORS": [
        {
            "NAME": "mat_noise_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_noise_speed",
                "speed_curve":2,
                "reverse": "mat_noise_reverse",
                "strob" : "mat_noise_strob",
                "reset": "mat_noise_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_flow_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_flow_speed",
                "speed_curve":2,
                "reverse": "mat_flow_reverse",
                "strob" : "mat_flow_strob",
                "bpm_sync": "mat_flow_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_fade_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_fade_speed",
                "speed_curve":2,
                "reverse": "mat_fade_reverse",
                "strob" : "mat_fade_strob",
                "bpm_sync": "mat_fade_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_noise_time = (mat_noise_time_source - mat_noise_offset * 16. * mat_noise_offset_scale) * 0.5;

float mat_flow_time = (mat_flow_time_source - mat_flow_offset * 8. * mat_flow_offset_scale) * 0.5;

float mat_fade_time = (mat_fade_time_source - mat_fade_offset * 16. * mat_fade_offset_scale) * 0.5;

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

/*
    Copyright (c) 2019 al-ro

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
#define AA

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    if (!mat_fade_animate) {
        mat_fade_time = 0.;
    }

    vec2 flow_offset = vec2(0.);
    float flow_time_x = mat_flow_time * cos(2.*PI * mat_flow_angle/360.0);
    float flow_time_y = mat_flow_time * sin(2.*PI * mat_flow_angle/360.0);
    flow_offset.x -= flow_time_x;
    flow_offset.y -= flow_time_y;

    float strength = 0.4 * mat_strength;

    float noise_time_x = mat_noise_time/3.0 * cos(2.*PI * mat_noise_angle/360.0);
    float noise_time_y = mat_noise_time/3.0 * sin(2.*PI * mat_noise_angle/360.0);

    float multiplier = 2000. * mat_detail;

    vec3 col = vec3(0);
    vec2 fC = uv * multiplier;

    #ifdef AA
    for(int i = -1; i <= 1; i++) {
        for(int j = -1; j <= 1; j++) {

            fC = uv * multiplier + vec2(i,j)/3.0;

            #endif

            //Normalized pixel coordinates (from 0 to 1)
            vec2 pos = fC / multiplier;

            pos = 4.0*(vec2(0.5) - pos);

            for(float k = 1.0; k < 7.0; k+=1.0 * mat_step_size){

                pos.x += strength * sin(2.0*noise_time_x+k*1.5 * mat_scale_xy.x * pos.y);
                pos.y += strength * cos(2.0*noise_time_y+k*1.5 * mat_scale_xy.y * pos.x);

                pos += flow_offset * 0.5;
            }

            //Time varying pixel colour
            col += 0.5 + 0.5*cos(mat_fade_time+pos.xyx+vec3(0,2,4) * mat_color_cycle);

            #ifdef AA
        }
    }

    col /= 9.0;
    #endif

    //Gamma
    col = pow(col, vec3(0.4545));

    //Fragment colour
    out_color = vec4(col,1.0);

    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
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

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    return out_color;
}
