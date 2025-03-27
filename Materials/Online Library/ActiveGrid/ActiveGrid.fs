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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#48409.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable
float iTime = mat_time - mat_offset * 10;

float layer(vec2 uv) {
    vec2 iv = floor(uv);
    vec2 gv = fract(uv) - .5;
    float image = .01 / abs(gv.y);
    image += .01 / abs(gv.x);
    return image;
}

vec4 materialColorForPixel(vec2 texCoord) {
    // vec2 uv = (2. * gl_FragCoord.xy - RENDERSIZE) / RENDERSIZE.y;

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;
    float image = 0.;
    image += layer(uv * 10. + iTime / 10.) * .1;
    image += layer(uv * 5. - iTime) * .2;
    image += layer(uv * 2.5 + iTime) * .4;
    image += layer(uv * 1.25 - iTime) * .8;
    image += layer(uv * 1.25 + iTime) * .1;
    return vec4(vec3(image), 1.);
}