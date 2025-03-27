/*{
    "CREDIT": "FabriceNeyret2, adapted by Jason Beyers",

    "DESCRIPTION": "Traeculum generator.  From https://www.shadertoy.com/view/4dKSDV",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Traeculum/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Traeculum/Alpha",
            "NAME": "mat_alpha",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
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

// simplification of https://www.shadertoy.com/view/lt2GDt
// used for 3D trabeculum here : https://www.shadertoy.com/view/MlB3Wt

#define H(n) fract( 1e4 * sin( n.x+n.y/.7 +vec2(1,12.34) ) )

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 10.;



    vec4 O = vec4(1.);
    // vec2 R = iResolution.xy, p,c;
    vec2 p;
    vec2 c;
    vec2 U = uv;
    // U *= mat_zoom;
    U += mat_time;
    float l;

    O += 1e9-O;  // --- Worley noise: return O.xyz = sorted distance to first 3 nodes
    for (int k=0; k<9; k++) // replace loops i,j: -1..1
    { // windows Angle bug with ,, instead of {}
                p = ceil(U) + vec2(k-k/3*3,k/3)-2., // cell id = floor(U)+vec2(i,j)
                l = dot(c = H(p) + p-U , c);        // distance^2 to its node
                  l < O.x  ? O.yz=O.xy, O.x=l       // ordered 3 min distances
                : l < O.y  ? O.z =O.y , O.y=l
                : l < O.z  ?            O.z=l : l;
    }
    O = 5.*sqrt(O);


    // --- smooth distance to borders and nodes

 // l = 1./(1./(O.y-O.x)+1./(O.z-O.x)); // Formula (c) Fabrice NEYRET - BSD3:mention author.
 // O += smoothstep(.0,.3, l-.5) -O;
    O -= O.x;  O += 4.*( O.y/(O.y/O.z+1.) - .5 ) - O;  // simplified form
    out_color = O;

    out_color.a = mat_alpha;







    return out_color;
}
