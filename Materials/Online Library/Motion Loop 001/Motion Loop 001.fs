/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "lsdlive, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/tlcXD8 ",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Pattern/Base Radius",
            "NAME": "mat_base_radius",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/Width",
            "NAME": "mat_width",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "Label": "Animation/Strob",
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
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },





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

float mat_time = (mat_time_source - mat_offset * 8.) * 0.5;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}


/*
@lsdlive
CC-BY-NC-SA

Motion Loop #001

Checkout the ISF port: https://github.com/theotime/isf_shaders/blob/master/shaders/motiongraphics_001.fs

With the help of https://thebookofshaders.com/examples/?chapter=motionToolKit
With the help of Flopine, FabriceNeyret2, Pixel Spirit Deck.

*/


// https://lospec.com/palette-list/1bit-monitor-glow

float ring_base_sz = .0125 * pow(mat_base_radius, 2.);
float ring_base_width = .1 * mat_width;

float AA = 3.;

vec2 RESOLUTION = vec2(1024.);



mat2 r2d(float a) {
    float c = cos(a), s = sin(a);
    return mat2(c, s, -s, c);
}

float fill(float d) {
    return 1. - smoothstep(0., AA / RESOLUTION.x, d);
}

// inspired by Pixel Spirit Deck: https://patriciogonzalezvivo.github.io/PixelSpiritDeck/
// + https://www.shadertoy.com/view/tsSXRz
float stroke(float d, float width) {
    return 1. - smoothstep(0., AA / RESOLUTION.x, abs(d) - width * .5);
}

float flip(float value, float percent) {
    return mix(value, 1. - value, percent);
}

float circle(vec2 p, float radius) {
  return length(p) - radius;
}

// https://thebookofshaders.com/edit.php?log=160909064320
float easeInOutExpo(float t) {
    if (t == 0. || t == 1.) {
        return t;
    }
    if ((t *= 2.) < 1.) {
        return .5 * exp2(10. * (t - 1.));
    } else {
        return .5 * (-exp2(-10. * (t - 1.)) + 2.);
    }
}

// not used, but can be
float easeInOutQuad(float t) {
    if ((t *= 2.) < 1.) {
        return .5 * t * t;
    } else {
        return -.5 * ((t - 1.) * (t - 3.) - 1.);
    }
}

// not used, but can be
float easeInOutCubic(float t) {
    if ((t *= 2.) < 1.) {
        return .5 * t * t * t;
    } else {
        return .5 * ((t -= 2.) * t * t + 2.);
    }
}

// https://thebookofshaders.com/edit.php?log=160909064528
/*float ring(vec2 p, float radius, float width) {
    return abs(length(p) - radius * 0.5) - width;
}*/


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;


    // rotation animation
    float t = easeInOutExpo(fract(mat_time));
    uv *= r2d((PI / 2.) * (floor(mat_time) + t));

    // ring size animation
    float offs = .5;
    t = easeInOutExpo(fract(mat_time + offs));
    float anim_sz = .125 + .125 * sin(PI * .75 + PI * (floor(mat_time + offs) + t));

    // old solution for anti-aliasing
    /*
    float ring = ring(uv, ring_base_sz + anim_sz, ring_base_width);
    float eps = abs(ring); // sharpen around the ring
    float sdf = (2. * smoothstep(-eps, eps, uv.x) - 1.) * ring;
    float mask = smoothedge(sdf, 3.); // cut sdf + AA
    */

    // Better solution from pixel spirit deck
    // pixelspiritdeck.com
    float mask = flip(stroke(circle(uv, ring_base_sz + anim_sz), ring_base_width), fill(uv.x));

    // vec3 col = mix(col1, col2, mask);




    out_color = mix(mat_back_color, mat_front_color, mask);

    return out_color;
}
