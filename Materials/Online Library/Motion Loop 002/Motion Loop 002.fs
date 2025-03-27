/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "lsdlive, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wt3SRl",

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
            "LABEL": "Pattern/Blink",
            "NAME": "mat_blink",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },




        {
            "LABEL": "Pattern/Stroke 1",
            "NAME": "mat_stroke1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Pattern/Stroke 2",
            "NAME": "mat_stroke2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Stroke 3",
            "NAME": "mat_stroke3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Stroke 4",
            "NAME": "mat_stroke4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Stroke 5",
            "NAME": "mat_stroke5",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Stroke 6",
            "NAME": "mat_stroke6",
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

Motion Loop #002

Checkout the ISF port: https://github.com/theotime/isf_shaders/blob/master/shaders/motiongraphics_002.fs

With the help of https://thebookofshaders.com/examples/?chapter=motionToolKit
Inspired by: https://thebookofshaders.com/edit.php?log=160909064609

*/


// float blink_factor (10.*.125)

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

float pulse(float begin, float end, float t) {
  return step(begin, t) - step(end, t);
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

    float mask;

    float blink_factor = 0.;

    if (mat_blink) {
        blink_factor = 0.125;
    }

    float t1 = fract(.125 + mat_time * .25); // for blinking rings
    float t2 = easeInOutExpo(fract(mat_time));// for easing ring

    // blinking rings
    if (uv.x < 0.) {
        if (uv.y < 0. && pulse(0., .25 - blink_factor, t1) == 1.) {
            mask = fill(circle(uv, .35));
            mask += stroke(circle(uv, .3725), .005 * mat_stroke5);
        } else if(uv.y > 0. && pulse(.25, .5 - blink_factor, t1) == 1.) {
            mask = fill(circle(uv, .35));
            mask += stroke(circle(uv, .3725), .005 * mat_stroke5);
        }
    } else {
        if (uv.y > 0. && pulse(.5, .75 - blink_factor, t1) == 1.) {
            mask = fill(circle(uv, .35));
            mask += stroke(circle(uv, .3725), .005 * mat_stroke5);
        } else if (uv.y < 0. && pulse(.75, 1. - blink_factor, t1) == 1.) {
            mask = fill(circle(uv, .35));
            mask += stroke(circle(uv, .3725), .005 * mat_stroke5);
        }
    }

    // opposite blinking rings
    if (uv.x < 0.) {
        if (uv.y < 0. && pulse(.5, .75 - blink_factor, t1) == 1.) {
            mask = stroke(circle(uv, .25), .05* mat_stroke3);
            mask += stroke(circle(uv, .2), .005* mat_stroke4);
        } else if(uv.y > 0. && pulse(.75, 1. - blink_factor, t1) == 1.) {
            mask = stroke(circle(uv, .25), .05* mat_stroke3);
            mask += stroke(circle(uv, .2), .005* mat_stroke4);
        }
    } else {
        if (uv.y > 0. && pulse(0., .25 - blink_factor, t1) == 1.) {
            mask = stroke(circle(uv, .25), .05 * mat_stroke3);
            mask += stroke(circle(uv, .2), .005* mat_stroke4);
        } else if (uv.y < 0. && pulse(.25, .5 - blink_factor, t1) == 1.) {
            mask = stroke(circle(uv, .25), .05  * mat_stroke3);
            mask += stroke(circle(uv, .2), .005* mat_stroke4);
        }
    }


    // easing ring
    vec2 uv2 = uv * r2d(-PI / 2. * (floor(mat_time) + t2));
    if(uv2.x < 0. && uv2.y < 0.)
        mask -= 2. * stroke(circle(uv2, .15), .05 * mat_stroke6);
    mask += stroke(circle(uv, .15), .05 * mat_stroke6);


    // outer rings + central circle
    mask -= fill(circle(uv, .09));
    mask += stroke(circle(uv, .4), .01 * mat_stroke1);
    mask += stroke(circle(uv, .43), .01 * mat_stroke2);

    mask = clamp(mask, 0., 1.);


    out_color = mix(mat_back_color, mat_front_color, mask);

    return out_color;
}
