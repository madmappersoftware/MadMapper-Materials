/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#61343.0",
  "VSN": "1.1",
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
        "LABEL": "Cross Height",
        "NAME": "mat_cross_height",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.2,
        "DEFAULT": 0.1
    },
    {
        "LABEL": "Cross Width",
        "NAME": "mat_cross_width",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.4
    },

    {
        "LABEL": "Circle Radius",
        "NAME": "mat_circle_radius",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.5,
        "DEFAULT": 0.25
    },

    {
        "LABEL": "Cross Stroke",
        "NAME": "mat_circle_stroke",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.4,
        "DEFAULT": 0.2
    },



    {
        "LABEL": "Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.5
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
    {
        "LABEL": "Color/Front Color",
        "NAME": "mat_front_color",
        "TYPE": "color",
        "DEFAULT": [
            .941,
            .965,
            .941,
            1.0
        ]
    },
    {
        "LABEL": "Color/Back Color",
        "NAME": "mat_back_color",
        "TYPE": "color",
        "DEFAULT": [
            .133,
            .137,
            .137,
            1.0
        ]
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

/*
 * Original shader from: https://www.shadertoy.com/view/3lyXzz
 */

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

// --------[ Original ShaderToy begins here ]---------- //
/*
@lsdlive
CC-BY-NC-SA

Motion Graphics #003

Checkout the ISF port: https://github.com/theotime/isf_shaders/blob/master/shaders/motiongraphics_003.fs

With the help of: https://thebookofshaders.com/examples/?chapter=motionToolKit
With the help of: https://patriciogonzalezvivo.github.io/PixelSpiritDeck/

*/


// #define mat_cross_height .1
// #define mat_cross_width .4
// #define mat_circle_radius .25
// #define mat_circle_stroke .2

// #define AA 5.
#define AA 5. / 1920.

#define pi 3.141592
// #define iTime (speed*(bpm/60.)*iTime)

// https://lospec.com/palette-list/1bit-monitor-glow
//vec3 col1 = vec3(.133, .137, .137);
//vec3 col2 = vec3(.941, .965, .941);

mat2 r2d(float a) {
    float c = cos(a), s = sin(a);
    return mat2(c, s, -s, c);
}

float fill(float d) {
    // return 1. - smoothstep(0., AA / iResolution.x, d);
    return 1. - smoothstep(0., AA, d);
}

// inspired by Pixel Spirit Deck: https://patriciogonzalezvivo.github.io/PixelSpiritDeck/
// + https://www.shadertoy.com/view/tsSXRz
float stroke(float d, float width) {
    // return 1. - smoothstep(0., AA / iResolution.x, abs(d) - width * .5);

    return 1. - smoothstep(0., AA, abs(d) - width * .5);
}

float bridge(float mask, float sdf, float w) {
    mask *= 1. - stroke(sdf, w * 2.);
    return mask + stroke(sdf, w);
}

float circle(vec2 p, float radius) {
  return length(p) - radius;
}

float rect(vec2 p, vec2 size) {
  vec2 d = abs(p) - size;
  return min(max(d.x, d.y), 0.0) + length(max(d,0.0));
}

float easeInOutQuad(float t) {
    if ((t *= 2.) < 1.) {
        return .5 * t * t;
    } else {
        return -.5 * ((t - 1.) * (t - 3.) - 1.);
    }
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    float t1 = fract(iTime * .25);// sliding
    float t2 = fract(iTime);// rotation
    t2 = easeInOutQuad(t2);

    uv *= r2d(pi * .25);
    vec2 uv1 = fract((uv + t1) * 4.) - .5;
    vec2 uv2 = fract(((uv-vec2(t1, 0)) * 4.)+.5) - .5;

    // layer1 - cross
    float mask = fill(rect(uv1 * r2d(t2 * pi), vec2(mat_cross_height, mat_cross_width)));
    mask += fill(rect(uv1 * r2d(t2 * pi), vec2(mat_cross_width, mat_cross_height)));

    // layer2 - circle
    mask = bridge(mask, circle(uv2, mat_circle_radius), mat_circle_stroke) ;

    mask = clamp(mask, 0., 1.);
    return mix(mat_back_color, mat_front_color, mask);


}