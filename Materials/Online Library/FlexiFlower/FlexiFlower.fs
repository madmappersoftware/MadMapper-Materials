/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [


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
  "DESCRIPTION" : "Animated flower pattern.  Converted from http:\/\/glslsandbox.com\/e#3891.0"
}
*/

// #include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

//FlexiFlower by CuriousChettai@gmail.com

float iTime = mat_time - mat_offset * 10;



vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uPos = texCoord - vec2(0.5);

    uPos *= mat_zoom;

    float angle = atan(uPos.y, uPos.x);
    float len = sqrt(uPos.x*uPos.x + uPos.y*uPos.y);

    float newAngle = angle - 0.1*sin(len*20.0-iTime*8.0) - 0.9*sin(len*3.0-iTime);
    float flower = 1.0 - smoothstep(0.2, 0.5, len);;
    flower *= 5.0 * (sin(newAngle*21.0 )+1.0);
    vec3 flowerColor = vec3(flower*0.4, flower*0.1, flower*1.6);

    float gradient = smoothstep(0.5, 1.5, len);
    vec3 gradientColor = vec3(gradient*0.1, gradient*0.5, gradient*0.9);

    vec3 color = flowerColor + gradientColor;
    return vec4(color, 1.0);




}