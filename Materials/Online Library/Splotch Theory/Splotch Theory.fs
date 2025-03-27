/*{
    "CREDIT": "dok, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wsGBWw",

    "VSN": "1.0",

    "INPUTS": [


        {
            "NAME": "mat_audio",
            "TYPE": "audio"
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
            "LABEL": "Pattern/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/Back Pattern",
            "NAME": "mat_back_pattern",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
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
            "LABEL": "Audio/Audio React",
            "NAME": "mat_audio_react",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "Label": "Audio/Level",
            "NAME": "mat_audio_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 0.25;

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


/* bonzomatic source */

#define PI2 (2.0*PI)

float vol = 0.005 * pow(mat_audio_level, 2.);

mat2 rot2(float a) {return mat2(cos(a), -sin(a), sin(a), cos(a));}

float ht(vec2 uv, float a, float v)
{
    uv *= 10.0;
    uv *= rot2(a);
    uv.x += 0.5 * floor(uv.y);
    return step(length(fract(uv) - 0.5), v / 2.0);
}

vec4 audio_lookup(float i) {

    if (mat_audio_react) {
        return IMG_NORM_PIXEL(mat_audio, i);
    } else {
        return vec4(1.);
    }
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

    vec2 uv_orig = uv;


    vec3 col = vec3(0);
    vec2 sc = uv;
    vec2 pv = uv;
    vec2 tv = uv;
    vec2 cv=  uv;
            vec2 dv = uv;
    float circle = length(cv) - mat_time + audio_lookup(0.01).r * 100.0 * vol;
    float dist_2 =  1.5 * sin(mat_time / 100.0) * tan(mat_time * 0.5);
    for (float i = 0.0; i < 10.0; i++) {
            float a = PI2 * (i/10.0);
            cv.x += dist_2 * sin(mat_time + a);
            cv.y += dist_2 * cos(mat_time + a);
    circle = min(circle,
    length(cv) - mat_time + audio_lookup(0.01).r * 100.0 * vol
    );
    }

    float dist = 1.5;
    tv.x += 0.001 * tan(mat_time);
    tv.x += dist * sin(mat_time);
    tv.y += dist * cos(mat_time);

    /* zoom */
    uv *= mix(0.5, 2.0, 0.5 + 0.5 * sin(mat_time / 10.0));
    /* roto */
    uv *= rot2(0.0025 * mat_time);

    float speed  = mat_time / 10.0;
    uv.y += 0.05 * sin(uv.x * 1.0 * PI2+ speed);
    uv.y = fract(uv.y * 10.0 * mat_back_pattern);
    col.r = ht(uv, PI2 * sin(mat_time / 10.0), 0.5 * uv.y * mat_back_pattern);

    col.rgb = col.rrr;
    float lsize = 10.0 * (0.5 + 0.5 * sin(mat_time / 10.0));
    col.rgb *= step(mix(0.4, 0.4, 0.5 + 0.5 * sin(vol * audio_lookup(0.01).r)), fract(length(tv * lsize)));

    /* invert */
    if (fract(circle) < 0.5 * mat_limit)
            col.rgb = col.rgb;
    else
            col.rgb = 1.0 - col.rrr;

    /* wash */
    if ((audio_lookup(0.01).r * vol) > 0.8)
            col.rgb = 1.0 - col.rgb;

    vec2 off;
    off.x += dist * sin(mat_time / 10.0);
    off.y += dist * cos(mat_time / 10.0);

//      sc = vec2(length(sc), atan(sc.x,sc.y)/PI2));
    if (ht(sc+off, 0.0, length(sc) + mix(0.0, 0.5, audio_lookup(0.02).r * vol)) > 0.5)
            col = 1.0 - col;

    out_color = mix(mat_back_color, mat_front_color, mat_luma(col));

    return out_color;
}
