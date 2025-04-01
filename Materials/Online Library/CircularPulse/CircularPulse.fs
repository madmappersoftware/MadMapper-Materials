/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43733.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

//#extension GL_OES_standard_derivatives : enable


float hex(vec2 p)
{
    p.x *= 0.57735;
    p = abs((mod(p, 0.8) - 0.4));
    return abs(max(p.x + p.x, p.y) - 0.5);
}


vec4 materialColorForPixel( vec2 texCoord ) {

    // vec2 pos = gl_FragCoord.xy - RENDERSIZE / 2.;
    // vec2 p = 40. * pos / RENDERSIZE.x;

    vec2 pos = texCoord - vec2(0.5);

    vec2 p = 40. * pos;

    p *= mat_zoom;



    float s = sin(dot(p, p) / -64. + iTime * 4.);
    s = pow(abs(s), 0.5) * sign(s);
    float  r = .35 + .25 * s;
    float t = pow(abs(sin(iTime * 4.)), 0.2) * sign(sin(iTime * 4.));
    t *= 0.25;
    p *= mat2(cos(t), -sin(t), sin(t), cos(t));
    return vec4(smoothstep(r - 0.1, r + 0.1, hex(p)));
}