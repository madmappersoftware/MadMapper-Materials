/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#4124.0",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Techno Swirl/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Techno Swirl/Shift",
            "NAME": "mat_shift",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },






        {
            "LABEL": "Ring/Split",
            "NAME": "mat_split",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ring/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ring/Repeat",
            "NAME": "mat_repeat",
            "TYPE": "int",
            "MIN": 0,
            "MAX": 4,
            "DEFAULT": 1
        },


        {
            "LABEL": "Ring/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ring/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Ring/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Ring/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Ring/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },



        {
            "LABEL": "Swirl/Thickness",
            "NAME": "mat_swirl_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Swirl/Repeat",
            "NAME": "mat_swirl_repeat",
            "TYPE": "int",
            "MIN": 0,
            "MAX": 4,
            "DEFAULT": 1
        },

        {
            "LABEL": "Swirl/Speed",
            "NAME": "mat_swirl_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Swirl/BPM Sync",
            "NAME": "mat_swirl_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Swirl/Reverse",
            "NAME": "mat_swirl_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Swirl/Offset",
            "NAME": "mat_swirl_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Swirl/Strob",
            "NAME": "mat_swirl_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Ring Color",
            "NAME": "mat_ring_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                0.0,
                0.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Swirl Color",
            "NAME": "mat_swirl_color",
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
                0.0
            ]
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
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_swirl_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_swirl_speed",
                "speed_curve":2,
                "reverse": "mat_swirl_reverse",
                "strob" : "mat_strob",
                "bpm_sync": "mat_swirl_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 2. * PI;
float mat_swirl_time = mat_swirl_time_source - mat_swirl_offset * 2. * PI;

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec2 shift = mat_shift;
    shift += vec2(0.5);
    shift.x = 1. - shift.x;
    shift -= vec2(0.5);
    uv += shift;

    float r = length(uv);
    float a = atan(uv.y, uv.x);

    if (mod(r, 0.3) > 0.2 * mat_split && mod(a - mat_time, PI / (3.0 * mat_repeat)) < 0.5 * mat_fill) {
        out_color = mat_ring_color;
    }   else {
        out_color = mat_swirl_color * vec4(float(mod(a + r + mat_swirl_time, PI / (1.5 * mat_swirl_repeat)) < 0.3 * mat_swirl_fill));
    }

    if ( out_color.rgb == vec3(0.) ) {
        out_color = mat_back_color;
    }


    return out_color;
}
