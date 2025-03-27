/*{
    "CREDIT": "cardinalsine, adapted by Jason Beyers",

    "DESCRIPTION": "Iris generator. From https:\/\/www.shadertoy.com\/view\/4ltBRj",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Iris/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Iris/Shift",
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

#define PHI 1.61803398875

float mat_time = mat_time_source - mat_offset;

float u_t = mat_time;

float fibonacciHash(float x) {
    return mod((1./PHI)*x,1.);
}

float cosInterp(float x, float p0, float p1) {
    return p0+(p1-p0)*0.5*(1.-cos(PI*fract(x)));
}

float distanceDet(vec2 p) {
    float d0 = distance(vec2(-1.,1.), p.xy),
          d1 = distance(vec2(1.,1.), p.xy),
          d2 = distance(vec2(-1.,1.), p.xy),
          d3 = distance(vec2(-1.,-1.), p.xy);

    mat2 m0 = mat2(d0, d1,
                   d2, d3);

    return determinant(m0);
}

float fHashNoiseF(float x) {
    // integer part of coordinates
    float x_i = floor(x);

    // hash
    float x0 = fibonacciHash(x_i),
          x1 = fibonacciHash(x_i+1.);

    float fx = cosInterp(x, x0, x1);
    return fx;
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

    float x_co = fHashNoiseF(1.*(distanceDet(uv.xy-2.)+u_t/16.));
    float y_co = fHashNoiseF(1.*(distanceDet(uv.yx-2.)));
    vec3 col = vec3(0.);
    vec3 colOffset = vec3(0.,0.5,1.)*(1.-distance(vec2(0.), uv.xy))*(0.5+0.5*sin(4.*mat_time));

    float env = clamp((1.-pow(distance(vec2(0.,0.),uv.xy/2.), 4.)),0.,1.);
    float gaussian2d = (exp(-1.*((uv.x*uv.x)+(uv.y*uv.y))));

    float sum = 0.;
    float a = 0.;

    for (float i=1.; i<96.; i++) {
        a = (1./i)*(0.75+0.25*(x_co+y_co));
        sum += a;
        col += a*cos(14.*i*gaussian2d+2.*mat_time+(2.*a*colOffset));
    }
    col = (1./sum)*(1.+col)*env;
    //col = sqrt(col);

    // Output to screen
    out_color = vec4(col,1.0);




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
