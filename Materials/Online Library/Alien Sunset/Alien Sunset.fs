/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "greenbird10, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3lfGWr",

    "VSN": "1.0",

    "IMPORTED": {
        "mat_iChannel0": {
            "NAME": "mat_iChannel0",
            "PATH": "79520a3d3a0f4d3caa440802ef4362e99d54e12b1392973e4ea321840970a88a.jpg"
        }
    },

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
            "LABEL": "Common/X Limit",
            "NAME": "mat_x_limit",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Mountains/FOV",
            "NAME": "mat_mountain_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Mountains/Frequency",
            "NAME": "mat_mountain_freq",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Mountains/Seed",
            "NAME": "mat_mountain_seed",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Mountains/Step Size",
            "NAME": "mat_mountain_step",
            "TYPE": "float",
            "MIN": 0.5,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Mountains/Offset Y",
            "NAME": "mat_mountain_offset_y",
            "TYPE": "float",
            "MIN": -1.,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Mountains/Rotate",
            "NAME": "mat_mountain_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Mountains/Position",
            "NAME": "mat_mountain_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Mountains/Position Scale",
            "NAME": "mat_mountain_pos_scale",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Sun/Level",
            "NAME": "mat_sun_level",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Sun/Size",
            "NAME": "mat_sun_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Sun/Position",
            "NAME": "mat_sun_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Sun/Position Scale",
            "NAME": "mat_sun_pos_scale",
            "TYPE": "float",
            "MIN": 0.01,
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
            "LABEL": "Color/Mountains",
            "NAME": "mat_mountain_color",
            "TYPE": "color",
            "DEFAULT": [
                0.05,
                0.1,
                0.35,
                1.0
            ]
        },
        {
            "LABEL": "Color/Sun",
            "NAME": "mat_sun_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.7,
                0.6,
                1.0
            ]
        },


        {
            "LABEL": "Color/Sky",
            "NAME": "mat_sky_color",
            "TYPE": "color",
            "DEFAULT": [
                0.75,
                0.3,
                0.22,
                1.0
            ]
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
            "LABEL": "Color/Alpha",
            "NAME": "mat_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
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

float mat_time = (mat_time_source - mat_offset * 16. * mat_offset_scale) * 4.;

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

// Created by greenbird10
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0


// Referrence:
// [2TC 15] Mystery Mountains.
// David Hoskins.

// Add texture layers of differing frequencies and magnitudes...
#define F +IMG_NORM_PIXEL(mat_iChannel0,mod(.3*mat_mountain_seed+p.xz*s*0.0001 * mat_mountain_freq,1.0))/(s+=s)

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    uv = flipY(uv, 1.);
    uv = flipX(uv, 1.);




    // vec4 p = vec4(gl_FragCoord.xy/RENDERSIZE.xy, 1, 1) - .5;
    vec4 p = vec4(uv.x, uv.y, 1. * pow(mat_mountain_fov, 2.), 0.5);
    vec4 d = p;
    d.y -= .07;
    p.z += (mat_time + 90.) * 5.;

    vec2 mountain_pos = flipY(mat_mountain_pos, mat_mountain_pos_scale * 0.5);


    d.xy += vec2(0.5);
    d.xy = matRot2D(d.xy, 2*PI*mat_mountain_rotate / 360);
    d.xy -= vec2(0.5);

    d.xy += mountain_pos;

    vec4 t;
    vec4 color;
    for(float i = 1.5; i > 0.; i -= .002 * mat_mountain_step)
    {
        float s =.5;

        //height  //p.xz = (x,time), s = amplitude
        t = F F F F;
        // put abs() around d.x to prevent black screen on some device
        // Thanks @dean_the_coder
        color = t*i + pow(abs(d.x) / (mat_x_limit * 4.), 4.);
        if(t.x > p.y*.007+1.2 - mat_mountain_offset_y)
        {
            break;
        }
        p += d;
    }

    float white = smoothstep(0.9, 2., color.x+color.y+color.z);
    color = 1. - pow(color, vec4(0.8));
    // color.x += .05;
    // color.y += .1;
    // color.z += .35;

    color.rgb += mat_mountain_color.rgb;
    color.a = mat_mountain_color.a;

    // color = mix(vec4(0.75, 0.3, 0.22, 0), color, white);
    color = mix(mat_sky_color, color, white);

    vec2 st = uv;
    st.y += 0.5;

    // vec2 st = texCoord;

    vec2 sun_pos = flipX(mat_sun_pos, mat_sun_pos_scale);

    float offset = 2.;


    float circle = distance(st, vec2(.5 + sun_pos.x, .55 + sun_pos.y));
    circle = step(circle, 0.2 * mat_sun_size);
    // circle *= pow(st.y, 4.) * 2.5 * mat_sun_level;

    // circle *= pow(st.y + 0.5, 4.) * 2.5 * mat_sun_level;

    circle *= pow(st.y + offset, 4.) * 2.5 * mat_sun_level * 0.005;
    circle *= (1. - white);
    // color = mix(color, vec4(0., 0.7, 0.6, 0), circle);
    color = mix(color, mat_sun_color, circle);

    out_color = color;

    if (!mat_alpha) {
        out_color.a = 1.;
    }


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
