/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "inigo quilez, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#47165.3",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Bump Noise/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bump Noise/Depth",
            "NAME": "mat_depth",
            "TYPE": "float",
            "MIN": 0.05,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Bump Noise/Viscosity",
            "NAME": "mat_viscosity",
            "TYPE": "float",
            "MIN": 0.0,
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_use_alpha",
            "LABEL": "Color/Alpha",
            "TYPE": "bool",
            "DEFAULT": 1,
            "FLAGS": "button"
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset;

// Modified version of https://www.shadertoy.com/view/MstXWn

// cd publi http://evasion.imag.fr/~Fabrice.Neyret/flownoise/index.gb.html
//          http://mrl.nyu.edu/~perlin/flownoise-talk/

// The raw principle is trivial: rotate the gradients in Perlin noise.
// Complication: checkboard-signed direction, hierarchical rotation speed (many possibilities).
// Not implemented here: pseudo-advection of one scale by the other.

// --- Perlin noise by inigo quilez - iq/2013   https://www.shadertoy.com/view/XdXGW8

vec2 mat_hash(vec2 p)
{
    p = vec2(dot(p,vec2(127.1, 311.7)),
             dot(p,vec2(269.5, 183.3)));

    return 2.0 * fract(sin(p) * 43758.5453123) - 1.0;
}

float mat_noise(vec2 p, float t)
{
    vec2 i = floor(p);
    vec2 f = fract(p);

//  vec2 u = f;
//  vec2 u = f * f * (3.0 - 2.0 * f);
    vec2 u = f * f * f * (10.0 + f * (6.0 * f - 15.0));
//  vec2 u = f * f * f * f * (f * (f * (-20.0 * f + 70.0) - 84.0) + 35.0);

    mat2 R = mat2(cos(t), -sin(t), sin(t), cos(t));

    return 2.0 * mix(mix(dot(mat_hash(i + vec2(0,0)) * R, (f - vec2(0,0))),
                             dot(mat_hash(i + vec2(1,0)) * R, (f - vec2(1,0))), u.x),
                         mix(dot(mat_hash(i + vec2(0,1)) * R, (f - vec2(0,1))),
                             dot(mat_hash(i + vec2(1,1)) * R, (f - vec2(1,1))), u.x), u.y);
}

float mat_m_noise(vec2 p, float t) {
    return mat_noise(p, t);
}

float mat_turb(vec2 p, float t) {
    float f = 0.0;
    mat2 m = mat2(1.6,  1.2, -1.2,  1.6);
    f  = 0.5000 * mat_m_noise(p, t); p = m*p;
    f += 0.2500 * mat_m_noise(p, t * -2.1); p = m*p;
    f += 0.1250 * mat_m_noise(p, t * 4.1); p = m*p;
    f += 0.0625 * mat_m_noise(p, t * -8.1); p = m*p;
    return f / .9375;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec2 pos = uv;

    float f  = mat_turb(1.0 * pos, mat_time);
    float f2 = mat_turb(1.0 * pos + vec2(0.0, 0.01), mat_time);
    float f3 = mat_turb(1.0 * pos + vec2(0.01, 0.0), mat_time);
    float dfdy = (f2 - f) / 0.01;
    float dfdx = (f3 - f) / 0.01;

    //gl_FragColor = vec4(0.5 + 0.5 * f);

    out_color = vec4(0.2 + 0.7 * dot(normalize(vec3(dfdx, dfdy, 1.0/mat_depth)), normalize(vec3(0.0,1.0,1.0*mat_viscosity))));

    if (!mat_use_alpha) {
        out_color.a = 1.;
    }



    //gl_FragColor = mix(vec4(0,0,.3,1), vec4(1.3), vec4(.5 + .5* f));


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
