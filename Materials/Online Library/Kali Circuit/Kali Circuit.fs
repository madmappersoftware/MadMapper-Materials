/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mojovideotech, adapted by Jason Beyers",

    "DESCRIPTION": "Kali Circuit generator.",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Kali Circuit/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Kali Circuit/Center",
            "NAME" :    "mat_center",
            "TYPE" :    "point2D",
            "DEFAULT" : [ 0.0, 0.0 ],
            "MAX" :     [ 3.0, 3.0 ],
            "MIN" :     [ -3.0, -3.0 ]
        },
        {
            "LABEL": "Kali Circuit/Glow",
            "NAME" :    "mat_glow",
            "TYPE" :    "float",
            "DEFAULT" : 3.0,
            "MIN" :     0.0,
            "MAX" :     9.0
        },
        {
            "LABEL": "Kali Circuit/Intensity",
            "NAME" :    "mat_intensity",
            "TYPE" :    "float",
            "DEFAULT" : 0.00125,
            "MIN" :     0.0005,
            "MAX" :     0.0025
        },
        {
            "LABEL": "Kali Circuit/Trace",
            "NAME" :    "mat_trace",
            "TYPE" :    "float",
            "DEFAULT" : 40.0,
            "MIN" :     10.0,
            "MAX" :     100.0
        },

        {
            "LABEL": "Kali Circuit/Sharpness",
            "NAME" :    "mat_sharpness",
            "TYPE" :    "float",
            "DEFAULT" : 1.0,
            "MIN" :     0.1,
            "MAX" :     4.0
        },


        {
            "LABEL": "Animate Pulse/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animate Pulse/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animate Pulse/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "Label": "Animate Pulse/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animate Pulse/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Animate Scroll/BPM Sync",
            "NAME": "mat_scroll_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animate Scroll/Reverse",
            "NAME": "mat_scroll_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animate Scroll/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "Label": "Animate Scroll/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animate Scroll/Strob",
            "NAME": "mat_scroll_strob",
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
        {
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

////////////////////////////////////////////////////////////
// UltimateKaliCircuits  by mojovideotech
//
// based on :
// shadertoy/XlX3Rj  by Kali
//
// Creative Commons Attribution-NonCommercial-ShareAlike 3.0
////////////////////////////////////////////////////////////


#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 20.;
float mat_scroll_time = mat_scroll_time_source - mat_scroll_offset * 20.;

#define     pisq    9.869604401089359   // pi squared, pi^2
#define     twpi    6.283185307179586   // two pi, 2*pi
#define     phicu   4.236067977499791   // phi cubed, phi^3
#define     cuphi   1.173984996705329   // cube root of phi
#define     rctwpi  0.159154943091895   // reciprocal of twpi, 1/twpi
#define     r36     0.027777777777778


vec3 color = vec3(0.0,0.5,0.0);
float S = mat_glow;
float T = mat_time*0.005;
float T2 = mat_scroll_time*0.005;

vec2 hash22(vec2 p) { return fract(vec2(21.0, 97.0)*sin(dot(p, vec2(92.0, 61.0)))); }

void formula(vec2 uv, vec2 z, float f)
{
    float m = 0.0;
    float o, ot2, ot=ot2=10000.0;
    for (int i=0; i<9; i++) {
        z = abs(z)/clamp(dot(z,z), 0.1, 0.5)-f;
        float l = length(z);
        o = min(max(abs(min(z.x, z.y)),-l+0.25), abs(l-0.25));
        ot = min(ot, o);
        ot2 = min(l*0.1, ot2);
        m = max(m, float(i)*(1.0-abs(sign(ot-o))));
    }
    m += 1.0;
    float w = mat_intensity*m*2.0;
    float circ = pow(max(0.0,w-ot2)/w,6.0);
    S += max(pow(max(0.0,w-ot)/w,0.25),circ);
    vec3 col = vec3(hash22(z),f);
    color += col*(0.4+mod(m/9.0-T*mat_trace+ot2*2.0, 1.0)*1.6);
    color += vec3(1.0, 0.7, 0.3)*circ*(10.0-m)*3.0
             *smoothstep(0.0, 0.5, vec3(f, uv.x, uv.y));
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    vec2 pos = uv;

    vec2 center_alt = mat_center;
    center_alt.x = 1. - center_alt.x;

    uv -= center_alt;
    vec2 uv_orig = uv;

    // vec2 pos = uv;

    // float a = T + mod(T, floor(mat_runtime))*cuphi;
    float a = T2 + T2*cuphi;

    float mat_circuit_offset = 0.05;

    float b = a*phicu;
    uv *= mat2(cos(b),sin(b),-sin(b),cos(b));
    uv += vec2(sin(a),cos(a*cuphi))*pisq;
    // uv *= offset;
    uv *= mat_circuit_offset;
    // float pix = cuphi/RENDERSIZE.x*offset;
    // float pix = cuphi/2000.;
    // float pix = cuphi/RENDERSIZE.x;
    float pix = cuphi/(4000. * mat_sharpness / mat_scale)*mat_circuit_offset;
    float c = 1.5+mod(floor(T2), 16.0)*0.125;
    for (int aa=0; aa<36; aa++) {
        vec2 aauv = floor(vec2(float(aa)*rctwpi, mod(float(aa), twpi)));
        formula(uv_orig, uv+aauv*pix, c);
    }
    S *= r36, color *= r36;
    vec3 colo = mix(vec3(0.025), color, S)*(1.5-length(pos));
    colo *= vec3(1.2, 1.1, 1.0);
    out_color = vec4(colo, 1.0);


    return out_color;
}
