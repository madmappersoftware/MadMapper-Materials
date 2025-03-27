/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Dithered Lensing generator. From http:\/\/glslsandbox.com\/e#40770.3",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "Dithered Lensing/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Dithered Lensing/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 128,
            "DEFAULT": 64
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
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 4.;

    vec2 p = uv;

    float color =0.0;
    //p += vec2(cos(mat_time*.01), sin(mat_time*.01));

    for(float i = 0.;i < mat_iterations; i++) {
        float a = cos(mat_time * .01 + i) + random(p*.05 * i);
        p += vec2(cos(mat_time*.05 * random(vec2(i))), sin(mat_time*.5 * random(vec2(i))))*.1;

        color += .1 * sin(length(p*2.) / mix(cos(mat_time*.5+noise(p)), .01 + sin(fract(noise(vec2(p.x*noise(vec2(p.x))-noise(vec2(p.x, p.x)))+mat_time))*.2) * cos(p.y+p.y+mat_time), noise(vec2(p.x * p.x + cos(mat_time) + a + p.y * p.y + sin(mat_time) )) ));
        float b = .0001 / length(p*.001+noise(vec2(i, cos(mat_time)))) + i * cos(mat_time*.1) - 4.;

        color *= b ;
    }

    out_color = vec4( vec3(0.01, color, color*.1+cos(mat_time)*.05), 1.0 );







    return out_color;
}
