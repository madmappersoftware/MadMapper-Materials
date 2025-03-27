/*{
    "CREDIT": "nabr, adapted by Jason Beyers",

    "DESCRIPTION": "Marble Swirl generator. From http:\/\/glslsandbox.com\/e#60061.0",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Marble Swirl/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Marble Swirl/Shift",
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


// translation by nabr
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License
// Contact the author for other licensing options
//

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * mat_offset_scale * 8.;

// based on e#30073.3
float mat_tex(vec3 st)
{
    // based on e#30073.3
    vec3 i = st;
    for (float n = 0.; n < 100.; n += 1.)
    {
        float t = (1000.+mat_time * 0.0125) * n;
        i = 10. * st + vec3(cos(t - i.x) + cos(t - i.y), sin(t - i.y) + cos(t + i.x) , sin(.25*mat_time)-cos(.25 * i.z));
        i.xy = st.xz + (cos(t) * i.xy + sin(t) * vec2(i.y, -i.x));
    }
    return (.45 - min(cos(2. * mat_time  + (2. * i.x)) * -.45, (6. - length(i))));
}
//
// Parallax mapping by nimitz (twitter: @stormoid)
// https://www.shadertoy.com/view/4lSGRh
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License
// Contact the author for other licensing options
//
vec3 mat_prlpos(vec3 p, vec3 n, vec3 rd)
{
    vec3 tgt = reflect(n, -rd); //(n * dot(rd, n) - rd);
    tgt /= (abs(dot(tgt, rd))) + 1.;
    p += (tgt * mat_tex(p) * .501);
    return p;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    vec2 st = uv;

    //dither by iq - https://www.shadertoy.com/view/3tj3DW
    // vec3 dthr = 4.*fract(sin(gl_FragCoord.x*vec3(13,1,11)+gl_FragCoord.y*vec3(1,7,5))*158.391832)/255.0;
    vec3 dthr = 4.*fract(sin(st.x*vec3(13,1,11)+st.y*vec3(1,7,5))*158.391832)/255.0;

    vec3 o = vec3(.001), d = normalize(vec3(st, -1));

    // sphere
    vec3 sc = vec3(0, 0, 2);
    float sr2 = 1.;

    vec3 ld = normalize(vec3(3. * sin(2.*mat_time), 2. * cos(2.*mat_time), -6));

    vec3 col = vec3(.757);
    out_color.rgb = pow(.25*vec3(.05, .26, .62), vec3(.454545));

    // raytrace
    o -= sc;
    float b = dot(o, d),
        c = dot(o, o) - sr2 ,
        det = b * b - c;
    if (det > 0.)
    {
        vec3 hit = o + (-b + sqrt(det)) * d;
        vec3 n = normalize(hit - sc);
        // half viewdir
        vec3 hd = normalize(ld + (-sc));
        float spe = pow(max(0., dot(hd, n)), 64.0);
        hit = mat_prlpos(hit, n, d);
        float fc = pow(clamp(1.005 + dot(n, d), 0.0, 1.0), 60.0);
        col = cos(vec3(0,1,1.57) +  distance(mat_tex(.5*hit), mat_tex(12.*n)*2.)) * .35 + .5;
        col *=  spe * .85 / sqrt(det) * 1. + fc;
        out_color.rgb = dthr+mix(out_color.xyz, col, max(0., det));
    }

    out_color.a = 1.;

    // out_color = vec4(1.);







    return out_color;
}
