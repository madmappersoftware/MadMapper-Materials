/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Flopine, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/tdBfWh",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Python Swirl/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Python Swirl/Mod A",
            "NAME": "mat_mod_a",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Python Swirl/Mod B",
            "NAME": "mat_mod_b",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Python Swirl/Threshold",
            "NAME": "mat_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Python Swirl/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 8,
            "MAX": 128,
            "DEFAULT": 64
        },
        {
            "LABEL": "Python Swirl/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "Label": "Python Swirl/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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

float mat_time = mat_time_source - mat_offset * 2. * PI;

// --------[ Original ShaderToy begins here ]---------- //
// Code by Flopine

// Thanks to wsmind, leon, XT95, lsdlive, lamogui,
// Coyhot, Alkama,YX, NuSan and slerpy for teaching me

// Thanks LJ for giving me the spark :3

// Thanks to the Cookie Collective, which build a cozy and safe environment for me
// and other to sprout :)  https://twitter.com/CookieDemoparty

// Shader made for Everyday ATI challenge

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

#define TAU 6.2831853071
#define dt mod(mat_time,TAU)

mat2 rot (float a)
{return mat2(cos(a),sin(a),-sin(a),cos(a));}

void moda(inout vec2 p, float rep)
{
    float per = TAU/rep;
    float a = mod(atan(p.y,p.x), per)-per*0.5;
    p = vec2(cos(a),sin(a))*length(p);
}

void mo (inout vec2 p, vec2 d)
{
    p = abs(p)-d;
    if (p.y>p.x) p = p.yx;
}

float cyl (vec3 p, float r, float h)
{
    return max(length(p.xy)-r,abs(p.z)-h);
}

float prim1 (vec3 p)
{
    float width = 0.05;
    p.xz *= rot(p.y*8.);
    mo(p.xz, vec2(0.1));
    moda(p.xz, 5.);
    p.x -= width*2.5;
    return cyl(p.xzy, width, 6.);
}

float SDF (vec3 p)
{
    p.yz *= rot(TAU/4.);
    p.xz *= rot(sin(p.y*1.5+dt));
    mo(p.xz, vec2(.7));
    moda(p.xz, 6.);
    p.x -= 0.1+(p.y+3.);
    return prim1(p);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    vec3 ro = vec3(uv*4.,-20.),
        rd = vec3(0.,0.,1.),
        p = ro,
        col = vec3(0.);

    float shad,d=0.;
    bool hit = false;

    for (float i=0.; i<float(mat_iterations);i++)
    {
        d = SDF(p);
        if (d<0.01 * mat_threshold)
        {
            hit = true;
            shad = i/float(mat_iterations);
            break;
        }
        p += d*rd*0.3;
    }

    if (hit)
    {
        col = vec3(smoothstep(mat_mod_a * 0.7,mat_mod_b * 0.8,1.-shad));
    }
    // Output to screen
    out_color = vec4(sqrt(col),1.0);


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
