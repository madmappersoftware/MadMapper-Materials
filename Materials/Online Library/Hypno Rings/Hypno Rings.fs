/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Hypno Rings generator. From http:\/\/glslsandbox.com\/e#38937.2",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Hypno Rings/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Hypno Rings/Count",
            "NAME": "mat_count",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 32.0,
            "DEFAULT": 12.0
        },

        {
            "LABEL": "Hypno Rings/Spread",
            "NAME": "mat_spread",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Hypno Rings/Thickness",
            "NAME": "mat_thickness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.75
        },

        {
            "LABEL": "Hypno Rings/AA",
            "NAME": "mat_sharpness",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Hypno Rings/Shift",
            "NAME": "mat_shift",
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "NAME": "mat_alpha",
            "LABEL": "Color/Alpha",
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
        }
    ]

}*/

#include "MadCommon.glsl"

// RingCarrouselAntiAliased.glsl

float mat_time = mat_time_source - mat_offset * 10.;

// draw ring at given position
float mat_draw_ring(vec2 uv, vec2 pos, float radius, float thick)
{
  return clamp(((1.0-abs(length(uv-pos)-radius))-1.00+thick)*mat_sharpness*300., 0.0, 1.0);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    vec2 position = texCoord;
    uv *= mat_scale;

    vec2 shift = mat_shift;
    shift += vec2(0.5);
    shift.x = 1. - shift.x;
    shift -= vec2(0.5);

    uv += shift;

    position = uv / 2.;

    float aspectRatio = 1.;

    vec2 dims = vec2(aspectRatio, 1.0);
    vec2 midpt = dims * 0.5;
    vec3 color = vec3(0.);
    float increment = dims.x / (20.0 * mat_spread);
    float angle = atan(position.y - midpt.y, position.x - midpt.x);
    angle = mod(angle, PI * 2.);
    float a = mat_alpha;
    for(float i = 1.0; i < mat_count; i += 1.0)
    {
        vec2 p2 = vec2(position.x + sin(i + mat_time * 2.0) * increment * 0.5
                  ,position.y + cos(i + mat_time * 2.0) * increment * 0.5);

        float rc = mat_draw_ring(uv, p2, i * increment, mat_thickness*0.02);
        if(rc > 0.0)
        {
            color = rc * vec3(sin(i) * 0.25 + 0.5
                         ,sin(i + mat_time) * 0.5 + 0.5
                     ,sin(i + mat_time * 1.2) * 0.5 + 0.5);
            a = 1.;
            break;
        }
    }
    out_color = vec4( color, a );


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
