/*{
    "CREDIT": "by mojovideotech, adapted by Jason Beyers",
    "CATEGORIES": [
        "procedural",
        "2d",
        "Automatically Converted"
    ],
    "TAGS": "converted_from_isf",
    "VSN": "1.1",
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/4dBSRK by iq.  Some sort of undefined colored grid thingy.",

    "INPUTS": [
        {
            "LABEL" : "seed",
            "MAX": [2.0,2.0],
            "MIN": [0.01,0.01],
            "DEFAULT":[1.0,1.0],
            "NAME": "seed",
            "TYPE": "point2D"
        },
        {
            "LABEL" : "Size",
            "NAME": "size",
            "TYPE": "float",
            "DEFAULT": 4,
            "MIN": 2,
            "MAX": 20
        },
        {
            "LABEL": "BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "strob" : "mat_strob",
                "reverse": "mat_reverse",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]
}
*/

// IQ_ColoredGridThingy by mojovideotech
// source : www.shadertoy.com/view/4dBSRK
// created by IQ : www.iquilezles.org/
// interactive mods by DoctorMojo : www.mojovideotech.com/

// Originally Created by inigo quilez - iq/2014
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

vec4 materialColorForPixel( vec2 texCoord ) {

    float iTime = mat_time - mat_offset;

    vec2  px = (22.0-size)*(4*isf_FragNormCoord.xy);

    float id = 0.5 + 0.5*cos(iTime + sin(dot(floor(px+0.5),vec2(113.1*seed.x,17.81)))*43758.545*seed.y);

    vec3  co = 0.5 + 0.5*cos(iTime + 3.5*id + vec3(0.0,1.57,3.14) );

    vec2  pa = smoothstep( 0.0, 0.2, id*(0.5 + 0.5*cos(6.2831*px)) );

    return vec4( co*pa.x*pa.y, 1.0 );
}