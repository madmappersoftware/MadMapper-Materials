/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
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
    ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#39748.1"
}
*/


// ACID
//precision mediump float;

float iTime = mat_time - mat_offset * 10;

vec3 fn (vec2 p) {
    return vec3(sin(p.x * 100.0 + cos(p.y * 20.0 + sin(p.x * 100.0 + cos(p.y * 20.0 + sin(p.x * 100.0 + cos(p.y * 100.0))))) + iTime * 5.0)) * (vec3(p.x, p.y, p.x + p.y) + vec3(sin(iTime), sin(iTime + 2.094), sin(iTime + 4.188)));
}

vec4 materialColorForPixel(vec2 texCoord) {
    // vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy );

    vec2 p = texCoord - vec2(0.5);

  p *= 0.5 * mat_zoom;

    vec3 c = fn(p);



    for(float i = 0.0; i<2.0;i++) {
        c = (fn(p + i) + c) / (1.2 + sin(iTime * .5));
    }

    return vec4(c, 1.0);
}