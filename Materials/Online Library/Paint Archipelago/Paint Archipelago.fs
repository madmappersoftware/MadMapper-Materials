/*{
    "CREDIT": "samloeschen, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#54438.0",

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
            "LABEL": "FBM/Value",
            "NAME": "mat_fbm_val",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "FBM/Freq",
            "NAME": "mat_fbm_freq",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "FBM/Freq Step",
            "NAME": "mat_fbm_freq_step",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "FBM/Amp",
            "NAME": "mat_fbm_amp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "FBM/Amp Step",
            "NAME": "mat_fbm_amp_step",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "FBM/Iterations",
            "NAME": "mat_fbm_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 32,
            "DEFAULT": 14
        },

        {
            "LABEL": "Noise 1/Scale",
            "NAME": "mat_noise1_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 1/Speed",
            "NAME": "mat_noise1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 1/BPM Sync",
            "NAME": "mat_noise1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise 1/Reverse",
            "NAME": "mat_noise1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise 1/Offset",
            "NAME": "mat_noise1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise 1/Offset Scale",
            "NAME": "mat_noise1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Noise 1/Strob",
            "NAME": "mat_noise1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise 1/Restart",
            "NAME": "mat_noise1_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Noise 2/Scale",
            "NAME": "mat_noise2_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 2/Speed",
            "NAME": "mat_noise2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 2/BPM Sync",
            "NAME": "mat_noise2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise 2/Reverse",
            "NAME": "mat_noise2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise 2/Offset",
            "NAME": "mat_noise2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise 2/Offset Scale",
            "NAME": "mat_noise2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Noise 2/Strob",
            "NAME": "mat_noise2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise 2/Restart",
            "NAME": "mat_noise2_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Noise 3/Scale",
            "NAME": "mat_noise3_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 3/Speed",
            "NAME": "mat_noise3_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise 3/BPM Sync",
            "NAME": "mat_noise3_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise 3/Reverse",
            "NAME": "mat_noise3_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise 3/Offset",
            "NAME": "mat_noise3_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise 3/Offset Scale",
            "NAME": "mat_noise3_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Noise 3/Strob",
            "NAME": "mat_noise3_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise 3/Restart",
            "NAME": "mat_noise3_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Color/Offset",
            "NAME": "mat_offset_color",
            "TYPE": "color",
            "DEFAULT": [
                0.1,
                0.1,
                0.2,
                1.0
            ]
        },


        {
            "LABEL": "Color/Cycle",
            "NAME": "mat_color_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Power",
            "NAME": "mat_color_pow",
            "TYPE": "float",
            "MIN": 1.0,
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
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },


    ],
    "GENERATORS": [
        {
            "NAME": "mat_noise1_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_noise1_speed",
                "speed_curve":2,
                "reverse": "mat_noise1_reverse",
                "strob" : "mat_noise1_strob",
                "bpm_sync": "mat_noise1_bpm_sync",
                "reset": "mat_noise1_restart",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_noise2_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_noise2_speed",
                "speed_curve":2,
                "reverse": "mat_noise2_reverse",
                "strob" : "mat_noise2_strob",
                "bpm_sync": "mat_noise2_bpm_sync",
                "reset": "mat_noise2_restart",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_noise3_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_noise3_speed",
                "speed_curve":2,
                "reverse": "mat_noise3_reverse",
                "strob" : "mat_noise3_strob",
                "bpm_sync": "mat_noise3_bpm_sync",
                "reset": "mat_noise3_noise1_restart",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_noise1_time = (mat_noise1_time_source - mat_noise1_offset * 64. * mat_noise1_offset_scale) * 4.;
float mat_noise2_time = (mat_noise2_time_source - mat_noise2_offset * 64. * mat_noise2_offset_scale) * 4.;
float mat_noise3_time = (mat_noise3_time_source - mat_noise3_offset * 64. * mat_noise3_offset_scale) * 4.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float hash(float n) { return fract(sin(n) * 1e4); }
float hash(vec2 p) { return fract(1e4 * sin(17.0 * p.x + p.y * 0.1) * (0.1 + abs(sin(p.y * 13.0 + p.x)))); }

float noise(float x) {
    float i = floor(x);
    float f = fract(x);
    float u = f * f * (3.0 - 2.0 * f);
    return mix(hash(i), hash(i + 1.0), u);
}

float noise(vec2 x) {
    vec2 i = floor(x);
    vec2 f = fract(x);
    float a = hash(i);
    float b = hash(i + vec2(1.0, 0.0));
    float c = hash(i + vec2(0.0, 1.0));
    float d = hash(i + vec2(1.0, 1.0));
    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}

float fbm (in vec2 p) {

    float value = 0.0 + pow(mat_fbm_val,0.25) - 1.;
    float freq = 1.0 * mat_fbm_freq;
    float amp = 0.5 * pow(mat_fbm_amp,0.75);

    for (int i = 0; i < mat_fbm_iterations; i++) {
        value += amp * (noise((p - vec2(1.0)) * freq));
        freq *= 1.9 * pow(mat_fbm_freq_step,0.125);
        amp *= 0.6 * pow(mat_fbm_amp_step,0.125);
    }
    return value;
}

float pattern(in vec2 p) {
    vec2 offset = vec2(-0.5);

    vec2 aPos = vec2(sin(mat_noise1_time * 0.005), sin(mat_noise1_time * 0.01)) * 6.;
    vec2 aScale = vec2(3.0) * pow(mat_noise1_scale,1.5); //base fbm scale
    float a = fbm(p * aScale + aPos);

    vec2 bPos = vec2(sin(mat_noise2_time * 0.01), sin(mat_noise2_time * 0.01)) * 1.;
    vec2 bScale = vec2(0.6) * pow(mat_noise2_scale,1.5);
    float b = fbm((p + a) * bScale + bPos);

    vec2 cPos = vec2(-0.6, -0.5) + vec2(sin(-mat_noise3_time * 0.001), sin(mat_noise3_time * 0.01)) * 2.;
    vec2 cScale = vec2(3.2) * pow(mat_noise3_scale,1.25);
    float c = fbm((p + b) * cScale + cPos);
    return c;
}

//iq palette
vec3 palette(in float t) {
    vec3 a = vec3(0.5, 0.5, 0.5);
    vec3 b = vec3(0.45, 0.25, 0.14);
    vec3 c = vec3(1.0 ,1.0, 1.0);
    // vec3 d = fract(vec3(0.0, 0.1, 0.2) + mat_color_cycle);
    vec3 d = fract(mat_offset_color.rgb + mat_color_cycle) * mat_color_pow;
    return a + b * cos(6.28318 * (c * t + d));
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

    vec2 uv_shift = mat_shift_amount;
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

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    float value = pow(pattern(uv), 2.); // more "islands"
    vec3 color = palette(value);
    out_color = vec4(color, 1.);

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
