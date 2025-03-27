/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "panna_pudi, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/sttSz8",

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
            "LABEL": "Custom/Pattern Scale",
            "NAME": "mat_pattern_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Custom/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Custom/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Custom/Spread",
            "NAME": "mat_spread",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Custom/Travel",
            "NAME": "mat_travel",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Custom/Kaleido",
            "NAME": "mat_kaleido",
            "TYPE": "int",
            "MIN": 0,
            "MAX": 4,
            "DEFAULT": 1
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
            "LABEL": "Post/Vignette",
            "NAME": "mat_vignette",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Post/AA",
            "NAME": "mat_aa",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Background/Background",
            "NAME": "mat_background",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },


        {
            "LABEL": "Background/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                0.0
            ]
        },

        {
            "LABEL": "Background/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Cut"
        },
        {
            "LABEL": "Background/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },


        {
            "LABEL": "Color/Gain",
            "NAME": "mat_gain",
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
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale * 350. * 2.;

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 350.;
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

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}



const float RECORD_PERIOD = 15.;

// #define NOAA

// By gaz: https://www.shadertoy.com/view/WdfcWr
vec2 pSFold(vec2 p, float n) {
    float e = 5e-3;
    float h = floor(log2(n));
    float a = 6.2831 * exp2(h) / n;
    for (float i = 0.0; i < h + 2.0 * mat_kaleido; i++) {
        vec2 v = vec2(-cos(a), sin(a));
        float g = dot(p, v);
        // https://iquilezles.org/articles/functions
        p -= (g - sqrt(g * g + e)) * v;
        a *= 0.5;
    }
    return p;
}

mat2 rot(float a) {
    float c = cos(a), s = sin(a);
    return mat2(c, -s, s, c);
}

float hash(float n) {
    return fract(sin(n) * 43758.5453123);
}

float hash21(vec2 coord){
    return fract(sin(dot(coord.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

// looping noise
// note x range must be same size or bigger than loopLen
// use bigger loopLen for more detail in loop
float loop_noise(float x, float loopLen) {
    // cycle the edges
    x = mod(x, loopLen);

    float i = floor(x);  // floored integer component
    float f = fract(x);  // fractional component
    float u =
        f * f * f * (f * (f * 6. - 15.) + 10.) * mat_travel;  // use f to generate a curve

    // interpolate from the current edge to the next one wrt cycles
    return mix(hash(i), hash(mod(i + 1.0, loopLen)), u) * mat_cycle1;
}

vec3 pattern(vec2 uv, float time, float n) {
    float num_seg = n;
    uv *= rot(PI / num_seg) * mat_pattern_scale;
    uv = pSFold(uv, num_seg);

    float loop_length = RECORD_PERIOD;
    float transition_start = RECORD_PERIOD / 3.;

    float phi = atan(uv.y, uv.x + 1e-6);
    phi = phi / PI * 0.5 + 0.5;
    float seg = floor(phi * num_seg);
    float width = sin(seg) + 8. * pow(mat_spread,2.5);

    float theta = (seg + 0.5) / num_seg * PI * 2.;
    vec2 dir1 = vec2(cos(theta), sin(theta));
    vec2 dir2 = vec2(-dir1.y, dir1.x);

    float radial_length = dot(dir1, uv);
    float prog = radial_length / width;
    float idx = floor(prog);

    const int NUM_CHANNELS = 3;
    vec3 col = vec3(0.);
    for (int i = 0; i < NUM_CHANNELS; ++i) {
        float off = float(i) / float(NUM_CHANNELS) - 1.5;
        time = time + off * .015;

        float theta1 = loop_noise(idx * 34.61798 + time,      loop_length);
        float theta2 = loop_noise(idx * 21.63448 + time + 1., loop_length);

        float transition_progress =
            (time - transition_start) / (loop_length - transition_start);
        float progress = clamp(transition_progress, 0., 1.);

        float threshold = mix(theta1, theta2, progress);

        float width2 = fract(idx * 32.721784) * 500. * mat_cycle2;
        float slide = fract(idx * seg * 32.74853) * 50. /* * time */
                      + loop_noise(time, loop_length) * 100. +
                      1000. * (hash(idx) - 0.5);
        float prog2 = (dot(dir2, uv) - slide) / width2;

        float c = clamp(width  * (fract(prog)  - threshold),      0., 1.)
                * clamp(width2 * (fract(prog2) + threshold - 1.), 0., 1.);

        c *= mat_gain;

        col[i] = c;
    }
    return col;
}

float v1gnette(vec2 uv) {
    vec2 d = abs(uv) * 1.21;
    d = pow(d, vec2(2.0));
    return pow(clamp(1.0 - dot(d, d), 0., 1.0), 3.5);
}

vec3 draw(vec2 uv, float fold_n, float release, float time) {
    vec3 col = vec3(0.);

    vec2 block = floor(uv / vec2(64.));
    vec2 uv_noise = block / vec2(125);
    uv_noise += floor(vec2(fract(time)) * vec2(1234.0, 3543.0)) / vec2(2);

    float block_thresh = pow(fract(time), 2.0) * 0.5 * release;
    float line_thresh = pow(fract(time), 2.0) * 0.5 * release;

    vec2 uv_r = uv, uv_g = uv, uv_b = uv;

    if (hash21(uv_noise) < block_thresh ||
        hash21(uv_noise) < line_thresh) {
        vec2 dist = (fract(uv_noise) - 0.5);
        uv_r += dist * 50.1;
        uv_g += dist * 40.2;
        uv_b += dist * 60.125;
    }

    uv_r += 2.;

    col.r = pattern(uv_r, time, fold_n).r;
    col.g = pattern(uv_g, time, fold_n).g;
    col.b = pattern(uv_b, time, fold_n).b;
    return col;
}

// #define NOAA

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    float time = mat_time;

    vec3 col = vec3(0.);
    float n = RECORD_PERIOD / 2.;
    // https://www.desmos.com/calculator/w4u0k6gejv
    float release = smoothstep(1., 0., abs(mod(2. * time + 1., n) - 1.));
    release = pow(release, 5.);
    float type = step(n / 2., mod(time, n));
    float fold_n = 4. + 2. * type;
    uv *= 1. - 0.08 * type;

    if (!mat_aa) {
        col = draw(uv, fold_n, release, time);
    } else {
        const int N = 1;
        for (int i = -N; i <= N; i++) {
            for (int j = -N; j <= N; j++) {
                col += draw(uv - vec2(i, j),
                            fold_n, release, time);
            }
        }
        float k = 2. * float(N) + 1.;
        col /= k * k;
    }

    col *= v1gnette((texCoord - vec2(0.5)) / 1000 * pow(mat_vignette,2.));

    out_color = vec4(col,1.0);


    if (mat_background) {
        // General way to add transparency to any shader
        if (mat_back_mode == 1) {
            // Differentiate front & back colors using a hard cut w/ threshold
            if (mat_luma(out_color.rgb) < mat_back_thresh) {
                out_color = mat_back_color;
            }
        } else {
            // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
            out_color = mix(mat_back_color, out_color, mat_luma(out_color.rgb) + mat_back_thresh);
        }
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
