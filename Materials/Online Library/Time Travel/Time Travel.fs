/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Flopine, adapted by Jason Beyers",

    "DESCRIPTION": "From https:\/\/www.shadertoy.com\/view\/MdcBzf",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Time Travel/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Time Travel/Outer Shape",
            "NAME": "mat_outer_shape",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Time Travel/Inner Radius",
            "NAME": "mat_inner_radius",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Time Travel/Hourglass M",
            "NAME": "mat_hourglass_middle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Time Travel/Hourglass C",
            "NAME": "mat_hourglass_column",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Time Travel/Hourglass E",
            "NAME": "mat_hourglass_end",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Time Travel/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 100,
            "DEFAULT": 65
        },
        {
            "LABEL": "Time Travel/Shift",
            "NAME": "mat_shift_amount",
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
#define time mat_time


float  mid  = 0.;

float mat_tiktak(float period)
{
    float tik = floor(time)+pow(fract(time),3.);
    tik *= 3.*period;
    return tik;
}

mat2 mat_rot (float a)
{
    return mat2(cos(a),sin(a),-sin(a),cos(a));
}

vec2 mat_moda (vec2 p, float per)
{
    float a = atan(p.y,p.x);
    float l = length(p);
    a = mod(a-per/2.,per)-per/2.;
    return vec2(cos(a),sin(a))*l;
}

float mat_stmin(float a, float b, float k, float n)
{
    float st = k/n;
    float u = b-k;
    return min(min(a,b) , 0.5 * (u+a+abs(mod(u-a+st,2.*st)-st)));
}

float od (vec3 p, float d)
{
    return dot(p,normalize(sign(p)))-d;
}

float mat_box (vec3 p, vec3 c)
{
    return length(max(abs(p)-c,0.));
}

float cylY(vec3 p, float r, float h)
{
    return max(length(p.xz)-r, abs(p.y)-h);
}

float cylZ(vec3 p, float r, float h)
{
    return max(length(p.xy)-r, abs(p.z)-h);
}

float prim1 (vec3 p, float h)
{
    p.xz *= mat_rot(p.y);
    p.xz = mat_moda(p.xz, 2.*PI/5.);
    p.x -= .6 * mat_hourglass_middle;
    return cylY(p,0.07 * mat_hourglass_column,h);
}

float prim2 (vec3 p, float h)
{
    return min(cylY(vec3(p.x,p.y+h,p.z),1.,0.2*mat_hourglass_end), cylY(vec3(p.x,p.y-h,p.z),1.,0.2*mat_hourglass_end));
}

float sablier (vec3 p)
{
    float h = 1.8;
    float s1 = mat_stmin(prim1(p,h), prim2(p,h),0.3,5.);
    p.xz *= mat_rot(time);
    p.xy *= mat_rot(time);
    return min(s1,od(p,0.3));
}

float mat_ring (vec3 p)
{
    p *= 1.2 * mat_outer_shape;
    float s1 = max(-cylZ(p,0.6*mat_inner_radius,1.), cylZ(p,1.,0.3));
    p.xy = mat_moda(p.xy, 2.*PI/8.);
    p.x -= 1.2 * mat_outer_shape;
    return mat_stmin(mat_box(p,vec3(0.4,0.2,0.2)), s1,0.3,5.);
}

float mat_sdf (vec3 p)
{
    float per = 6.;
    float d = 0.;

    p.z = mod(p.z-per/2.,per)-per/2.;

    vec3 pp = p;
    p.xy *=mat_rot(mat_tiktak(0.5));
    float r1 = mat_ring (p);

    p = pp;

    p.xy *=mat_rot(-mat_tiktak(0.5));
    p.xy = mat_moda(p.xy,2.*PI/5.);
    p.x -= 5.;
    float s = sablier(p);

    if (d<r1)
    {
        mid = 1.;
        d = r1;
    }

    if (d>s)
    {
        mid = 2.;
        d = s;
    }

    return d;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= mat_scale;
    vec3 ro = vec3(0.001,0.001,time*3.); vec3 p = ro;
    vec3 dir = normalize(vec3(uv,1.));
    float shad = 0.;
    for (float i = 0.; i<mat_iterations; i++)
    {
        float d = mat_sdf(p);
        if (d<0.001)
        {
            shad = i/mat_iterations;
            break;
        }
        p+=d*dir*0.35;
    }
    float t = length(ro-p);
    vec3 col = vec3(0.);
    if (mid == 1.) col = vec3(1.-shad)/vec3(0.3,0.8,0.)*0.8;
    if (mid == 2.) col = mix(vec3(shad), vec3(0.1,0.5,0.7), 1.-abs(p.y)+2.);
    col = mix(col,length(uv)* vec3(0.,0.,0.1),1.-exp(-0.001*t*t));
    out_color = vec4(col,1.);


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
