/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "leon, adapted by Jason Beyers",

    "DESCRIPTION": "Anaglyph generator. From https:\/\/www.shadertoy.com\/view\/wdc3zX",

    "VSN": "1.1",

    "INPUTS": [


        {
            "LABEL": "Anaglyph/Camera",
            "NAME": "mat_camera_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },



        {
            "LABEL": "Anaglyph/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        { "Label": "Anaglyph/Count", "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 5 },

        { "Label": "Anaglyph/Range", "NAME": "mat_range", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
        { "Label": "Anaglyph/Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 },
        { "Label": "Anaglyph/Blend", "NAME": "mat_blend", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.5 },
        { "Label": "Anaglyph/Balance", "NAME": "mat_balance", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.5 },
        { "Label": "Anaglyph/Falloff", "NAME": "mat_falloff", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.9 },
        { "Label": "Anaglyph/Grain", "NAME": "mat_grain", "TYPE": "float", "MIN": 0.0, "MAX": 0.1, "DEFAULT": 0.01 },
        { "Label": "Anaglyph/Divergence", "NAME": "mat_divergence", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0.1 },
        { "Label": "Anaglyph/Field Of View", "NAME": "mat_fieldOfView", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.5 },


        {
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.25
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
            "NAME": "mat_alpha",
            "LABEL": "Color/Alpha",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_hue_shift",
            "LABEL": "Color/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
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

float mat_time = mat_time_source - mat_offset;


// Anaglyph Quick Sketch
// An example on how to render stereoscopic anaglyph image
// It will be the theme of https://2019.cookie.paris/
// And the content of the 3rd issue of https://fanzine.cookie.paris/
// Leon Denise 2019.09.20
// Licensed under hippie love conspiracy

// Using code from
// Inigo Quilez
// Morgan McGuire

float mat_random(vec2 p) { return fract(1e4 * sin(17.0 * p.x + p.y * 0.1) * (0.1 + abs(sin(p.y * 13.0 + p.x)))); }

mat2 mat_rot(float a) { float c=cos(a),s=sin(a); return mat2(c,-s,s,c); }
float mat_smoothmin (float a, float b, float r) { float h = clamp(.5+.5*(b-a)/r, 0., 1.); return mix(b, a, h)-r*h*(1.-h); }

vec3 mat_look (vec3 eye, vec3 target, vec2 anchor, float fov) {
    vec3 forward = normalize(target-eye);
    vec3 right = normalize(cross(forward, vec3(0,1,0)));
    vec3 up = normalize(cross(right, forward));
    return normalize(forward * fov + right * anchor.x + up * anchor.y);
}

vec3 mat_camera (vec3 eye) {
    // vec2 mouse = mat_camera_pos.xy/RENDERSIZE.xy*2.-1.;

    vec2 mouse = mat_camera_pos;
    if (true) {
        eye.yz *= mat_rot(mouse.y*3.1415);
        eye.xz *= mat_rot(mouse.x*3.1415);
    } else {
        eye.yz *= mat_rot(-3.1415/4.);
        eye.xz *= mat_rot(-3.1415/2.);
    }
    return eye;
}

float mat_geometry (vec3 pos) {
    pos = mat_camera(pos);
    float a = 1.0;
    float scene = 1.;
    float t = mat_time*0.2;
    float wave = 1.0+0.2*sin(t*8.-length(pos)*2.);
    t = floor(t)+pow(fract(t),.5);
    for (int i = mat_count; i > 0; --i) {
        pos.xy *= mat_rot(cos(t)*mat_balance/a+a*2.+t);
        pos.zy *= mat_rot(sin(t)*mat_balance/a+a*2.+t);
        pos.zx *= mat_rot(sin(t)*mat_balance/a+a*2.+t);
        pos = abs(pos)-mat_range*a*wave;
        scene = mat_smoothmin(scene, length(pos)-mat_radius*a, mat_blend*a);
        a /= mat_falloff;
    }
    return scene;
}

float mat_raymarch ( vec3 eye, vec3 ray ) {
    float dither = mat_random(ray.xy+fract(mat_time));
    float total = dither;
    const int count = 30;
    for (int index = count; index > 0; --index) {
        float dist = mat_geometry(eye+ray*total);
        dist *= 0.9+.1*dither;
        total += dist;
        if (dist < 0.001 * total)
            return float(index)/float(count);
    }
    return 0.;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec3 eyeLeft = vec3(-mat_divergence,0,5.);
    vec3 eyeRight = vec3(mat_divergence,0,5.);
    vec3 rayLeft = mat_look(eyeLeft, vec3(0), uv, mat_fieldOfView);
    vec3 rayRight = mat_look(eyeRight, vec3(0), uv, mat_fieldOfView);
    float red = mat_raymarch(eyeLeft, rayLeft);
    float cyan = mat_raymarch(eyeRight, rayRight);
    float a = mat_alpha;
    if (red > 0. || cyan > 0.) {
        a = 1.;
    }
    out_color = vec4(red,vec2(cyan),a);

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply Hue Shift and saturation
    if (mat_hue_shift > 0.01 || mat_saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+mat_hue_shift));
        hsv.y = max(hsv.y + mat_saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;


    return out_color;
}
