/*{
    "CREDIT": "c0de4, adapted by Jason Beyers",

    "DESCRIPTION": "Wild Circle shader. Fro http:\/\/glslsandbox.com\/e#40806.0",

    "VSN": "1.1",

    "INPUTS": [



        {
            "LABEL": "Wild Circle/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wild Circle/Shift",
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

float mat_time = mat_time_source - mat_offset * mat_offset_scale * 10.;

float random (in vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))
                * 43758.5453123);
}

// The MIT License
// Copyright © 2013 Inigo Quilez
float noise(vec2 st) {
    vec2 i = floor(st);
    vec2 f = fract(st);
    vec2 u = f*f*(3.0-2.0*f);
    return mix( mix( random( i + vec2(0.0,0.0) ),
                     random( i + vec2(1.0,0.0) ), u.x),
                mix( random( i + vec2(0.0,1.0) ),
                     random( i + vec2(1.0,1.0) ), u.x), u.y);
}


const vec3 lightDir = vec3(-0.577, 0.577, 0.577);

// wrote: @c0de4
float distanceFunc(vec3 p) {
    float c = cos(mat_time) - fract(length(p * 2.)) + p.z + fract(p.z * 2.);
    float s = sin(p.z * (p.z) + mat_time * .1) * p.x;
    float dist1 = length(cos(fract(p * 8.) * fract(length(c*p+cos(mat_time))) * .01 - s * .1 + mat_time)) + c - 1.0;
    float dist2 = length(fract(p+noise(vec2(mat_time*.1)))*2.) - cos(mat_time);
    float dist3 = length(p) - 1.0 + p.x * p.x + p.y * p.y;

    return length(fract(mix(dist3, mix(dist1, dist3, noise(vec2(p.y+cos(p.z + mat_time)))), noise(vec2(c,s) + sin(mat_time)))));
}

vec3 getNormal(vec3 p) {
  const float d = 0.0001;
  return
    normalize (
      vec3 (
        distanceFunc(p+vec3(d,0.0,0.0))-distanceFunc(p+vec3(-d,0.0,0.0)),
        distanceFunc(p+vec3(0.0,d,0.0))-distanceFunc(p+vec3(0.0,-d,0.0)),
        distanceFunc(p+vec3(0.0,0.0,d))-distanceFunc(p+vec3(0.0,0.0,-d))
      )
    );
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

    vec2 p = uv;

    vec3 cPos = vec3(.0, 0., 12.+cos(mat_time*.1)*8.);
    vec3 cDir = vec3(0., 0., -1.);
    vec3 cUp  = vec3(0., 1., 0.);
    vec3 cSide = cross(cDir, cUp);
    float focus = 1.0;

    vec3 ray = normalize(cSide * p.x + cUp * p.y + cDir * focus);

    float t = 0.0, d;
    vec3 posOnRay = cPos;

    for(int i=0; i< 5; ++i) {
      d = distanceFunc(posOnRay);
      t += d;
      posOnRay = cPos + t * ray;
    }


    if(abs(d) < .01) {
      vec3 normal = getNormal(posOnRay);
      float diff = clamp(dot(lightDir, normal), 0.1, 1.0);
      out_color = vec4(vec3(diff+.7, diff+sin(mat_time*.1)+.2, normal+.5), 1.0);
    } else {
      out_color = vec4(0.);
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
