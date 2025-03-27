/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "TAKUSAKU, adapted by Jason Beyers",

    "DESCRIPTION": "Glass Garden generator. From https:\/\/www.shadertoy.com\/view\/3s2Bzy",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Glass Garden/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Glass Garden/Shift",
            "NAME": "mat_shift",
            "TYPE": "point2D",
            "MIN": [0.0,0.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.5,0.5]
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

float mat_time = mat_time_source - mat_offset * 10.;

float rand (in vec2 _st) {
    return fract(sin(dot(_st.xy, vec2(-0.370,0.200)))*757.161);
}

float noise (in vec2 _st) {
    const vec2 d = vec2(0.0, 1.0);
    vec2 b = floor(_st), f = smoothstep(vec2(0.), vec2(0.5), fract(_st));
    return mix(mix(rand(b), rand(b + d.yx), f.x), mix(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
}

float fbm ( in vec2 _st) {
    float v = 0.23 + 0.05 * sin(mat_time*0.2);
    float a = .5;
    // Rotate to reduce axial bias
    mat2 rot = mat2(cos(0.5), sin(0.5), -sin(0.5), acos(0.5));
    for (int i = 0; i < 5; ++i) {
        v += a * noise(_st);
        _st = rot * _st * 8.0;
        a *= 0.32;
    }
    return v;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 3.;

    vec2 shift_alt = mat_shift;
    shift_alt.x = 1. - shift_alt.x;
    uv -= vec2(0.5);
    uv += shift_alt;

    vec2 st = uv;

    vec2 co = st;
    co.x += 0.9*sin(mat_time);
    co.y += 4.;
    float len;
    for (int i = 0; i < 3; i++) {
        len = length(co);
        co.x +=  sin(co.y + mat_time * 0.620)*0.;
        co.y +=  cos(co.x + mat_time * 0.164)*0.1;
    }
    len -= 3.;

    vec3 col = vec3(0.);
    vec2 q = vec2(0.);
    q.x = fbm( st + 1.0);
    q.y = fbm( st + vec2(-0.450,0.650));
    vec2 r = vec2(0.);
    r.x = fbm( st + 1.0*q + vec2(0.570,0.520)+ 0.16*mat_time );
    r.y = fbm( st + 1.0*q + vec2(0.340,-0.570)+ 0.1*mat_time);

    for (float i = 0.; i < 3.; i++) {
        r += 1.0 / abs(mod(st.xy, 0.732* i) * 10.) * 1.;// Glass block grid
    }
    float f = fbm(st+r);

    col = mix(col, cos(len + vec3(0.0, 1.0, 0.3)), 1.0);
    col = mix(vec3(0.562,0.680,0.482), vec3(0.357,0.518,0.600), col);

    out_color = vec4(2.0*(f*f*f+.6*f*f+.5*f)*col,1.);







    return out_color;
}
