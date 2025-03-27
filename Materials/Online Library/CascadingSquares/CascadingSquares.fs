/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
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
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#52889.0"
}
*/

/*
 * Original shader from: https://www.shadertoy.com/view/WslXDr
 */

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

// #define PI 3.141592

float drawRect() {
    return 0.0;
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    float t = PI*iTime/5.0;

    uv += vec2(0.2*t,0.0);

    vec2 id = floor(5.0*uv+0.5);
    uv = fract(5.0*uv+0.5)-0.5;

    vec3 colorGreen = vec3(63,104,19)/255.;
    vec3 colorBlue = vec3(55.,94.,151.)/255.;
    vec3 colorRed = vec3(255,204,187)/255.;
    vec3 colorYellow = vec3(110,181.,192)/255.;


    float a = PI/4.;

    vec3 col = vec3(0.0);
    float r = 0.5;

    for (float x = -1.; x <= 1.; x += 1.) {
        for (float y = -1.; y <= 1.; y += 1.) {
            vec2 offset = vec2(x,y);

            vec2 id2 = (id+offset);
            vec2 uv2 = (uv-offset);

            float t2 = -2.0*t+id2.x/5.+abs(id2.y)/10.;

            float r2 = 0.5+0.15*sin(4.0*t2);

            float a2 = a + PI/4.*(floor(t2)+smoothstep(0.1,.5,fract(t2)));

            uv2 *= mat2(cos(a2), sin(a2), -sin(a2), cos(a2));

            float multiplier = 0.95;

            float dx = smoothstep(r2,r2*multiplier,abs(uv2.x));
            float dy = smoothstep(r2,r2*multiplier,abs(uv2.y));

            float pct = smoothstep(-.5,.5,sin(2.0*t2));


            vec3 color = mix(colorYellow, colorRed, pct);

            col += (dx * dy)*color;
        }
    }


    return vec4(col,1.0);

}