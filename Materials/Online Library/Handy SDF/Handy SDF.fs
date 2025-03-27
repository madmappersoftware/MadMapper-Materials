/*{
    "CREDIT": "dean_the_coder, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/csd3zX",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
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
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },


        {
            "LABEL": "Hand/Control",
            "NAME": "mat_position",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },

        {
            "LABEL": "Hand/Light",
            "NAME": "mat_light",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Hand/Gain",
            "NAME": "mat_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Hand/Background",
            "NAME": "mat_back_level",
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
            "Label": "Animation/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },




        {
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Saturation",
            "NAME": "mat_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "mat_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
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
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}


// 'Handy SDF' dean_the_coder (Twitter: @deanthecoder)
// https://www.shadertoy.com/view/csd3zX
//
// Based on my Terminator 2 shader: https://www.shadertoy.com/view/mdt3D7
//
// Most component SDFs based on iq's awesome examples.
//
// Processed by 'GLSL Shader Shrinker'
// (https://github.com/deanthecoder/GLSLShaderShrinker)

// License: Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License


#define I0  0
#define sat(x)  clamp(x, 0., 1.)
#define S(a, b, c)  smoothstep(a, b, c)
#define S01(a)  S(0., 1., a)

float min2(vec2 v) { return min(v.x, v.y); }

float max3(vec3 v) { return max(v.x, max(v.y, v.z)); }

float dot3(vec3 v) { return dot(v, v); }

float sum2(vec2 v) { return dot(v, vec2(1)); }

float smin(float a, float b, float k) {
    float h = sat(.5 + .5 * (b - a) / k);
    return mix(b, a, h) - k * h * (1. - h);
}

mat2 rot(float a) {
    float c = cos(a),
          s = sin(a);
    return mat2(c, s, -s, c);
}

vec3 bend(vec3 p) {
    float c = cos(-.3 * p.x),
          s = sin(-.3 * p.x);
    p.xz *= mat2(c, s, -s, c);
    return p;
}

float box(vec3 p, vec3 b) {
    vec3 q = abs(p) - b;
    return length(max(q, 0.)) + min(max3(q), 0.);
}

float cyl(vec3 p) {
    vec2 d = abs(vec2(length(p.yz), p.x)) - vec2(.06, .15);
    return min(max(d.x, d.y), 0.) + length(max(d, 0.));
}

float cap(vec3 p, float h, float r) {
    r *= 1. - p.x / h * .14;
    p.x -= clamp(p.x, 0., h);
    return length(p) - r;
}

float tri(vec3 p, vec3 a, vec3 c) {
    const vec3 b = vec3(.06, 0, 0);
    vec3 ba = b - a,
         pa = p - a,
         cb = c - b,
         pb = p - b,
         ac = a - c,
         pc = p - c,
         n = cross(ba, ac);
    return sqrt((sign(dot(cross(ba, n), pa)) + sign(dot(cross(cb, n), pb)) + sign(dot(cross(ac, n), pc)) < 2.) ? min(min(dot3(ba * sat(dot(ba, pa) / dot3(ba)) - pa), dot3(cb * sat(dot(cb, pb) / dot3(cb)) - pb)), dot3(ac * sat(dot(ac, pc) / dot3(ac)) - pc)) : dot(n, pa) * dot(n, pa) / dot3(n));
}

// Simple articulated bone.
float bone(inout vec3 p, mat2 rot, float h, float r) {
    p.xz *= rot;
    float d = cap(p, h, r);
    p.x -= h;
    return d;
}

vec3 rayDir(vec3 ro, vec3 lookAt, vec2 uv) {
    vec3 f = normalize(lookAt - ro),
         r = normalize(cross(vec3(0, 1, 0), f));
    return normalize(f + r * uv.x + cross(f, r) * uv.y);
}

// SDF of the hand.
float sdf(vec3 p) {
    float d, l,
          curl = 1. - mat_position.y * 2.;
    p.xy *= rot(1.8 - mat_position.x * 2.);
    vec3 r,
         q = p + vec3(.25, -.1, .07);
    q.xy *= mat2(.49757, .86742, -.86742, .49757);
    d = bone(q, mat2(.98007, -.19867, .19867, .98007), .42, .04 - .07 * S(.1, .5, q.x));
    q.yz *= mat2(.16997, -.98545, .98545, .16997);
    d = smin(d, bone(q, rot(.3 * curl - .5), .26, -.025), .04);
    float h = cyl(q);
    d = smin(d, bone(q, mat2(.995, -.09983, .09983, .995), .22, -.03 - .065 * S(.1, .25, q.x) * S(.05, -.08, q.z)), .02);
    p = bend(p);
    r = vec3(.37, .47 - S(.1, -.4, p.x) * .15, .12);
    d = smin(d, box(p, r - .12), .16);
    p.x -= r.x;
    curl = S01(curl * (1. + step(p.y, 0.) * .3));
    d = smin(d, tri(p, vec3(0, r.y - .12, 0), vec3(0, .12 - r.y, 0)), .05);
    d -= .12;
    p.xz *= rot(-.2 - curl * .63);
    l = 1. + step(0., p.y) * .1 + step(.25, p.y) * .2;
    p.y = -abs(p.y);
    p.xz -= .05;
    q = p;
    q.y += r.y * .5 - .12;
    q.xy *= mat2(.9998, -.02, .02, .9998);
    mat2 r1 = rot(-.7 * curl),
         r2 = rot(-1.4 * curl);
    d = smin(d, bone(q, r1, .32 * l, .105), .06);
    d = smin(d, bone(q, r2, .17 * l, .09), .01);
    d = smin(d, bone(q, r1, .13 * l, .08), .01);
    q = p;
    q.y += r.y - .12;
    q.z += .07;
    q.xy *= mat2(.995, -.09983, .09983, .995);
    d = smin(d, bone(q, r1, .19 * l, .105), .06);
    d = smin(d, bone(q, r2, .13 * l, .09), .01);
    d = smin(d, bone(q, mat2(1, 0, 0, 1), .12 * l, .08), .01);
    return min(h, d) - .01;
}


// Calculate a normal.
vec3 N(vec3 p, float t) {
    float h = t * .1;
    vec3 n = vec3(0);
    for (int i = int(I0); i < 4; i++) {
        vec3 e = .005773 * (2. * vec3(((i + 3) >> 1) & 1, (i >> 1) & 1, i & 1) - 1.);
        n += e * sdf(p + e * h);
    }

    return normalize(n) * mat_gain;
}

// Simple ambient occlusion.
float ao(vec3 p, vec3 n) {
    const vec2 h = vec2(.1, 1);
    vec2 ao;
    for (int i = int(I0); i < 2; i++)
        ao[i] = sdf(h[i] * n + p);

    return sat(min2(ao / h)) * mat_light;
}

// Set the color.
vec3 lights(vec3 p, vec3 rd, vec3 n) {
    vec3 l,
         ld = normalize(vec3(0, -10, -6) - p);
    float _ao = ao(p, n), fre = S(1., .7, 1. + dot(rd, n));
    l = sat(vec3(dot(ld, n), dot(-ld.xz, n.xz), n.y));
    l.xy = .1 + .9 * l.xy;
    l *= .1 + .9 * _ao;
    l.x += pow(sat(dot(normalize(ld - rd), n)), 1e2);
    return sum2(l.xy) * vec3(1, 1.11, 1.5) * fre;
}

// Simple ray marhcing loop.
vec3 march(vec3 p, vec3 rd) {
    float d = 0.;
    for (float i = min(mat_time, 0.); i < 64.; i++) {
        // Background.
        if (d > 64.)
            return vec3(length(rd.xy) * .2) * mat_back_level;

        float h = sdf(p);
        if (abs(h) < 1e-4) break;
        d += h;
        p += h * rd;
    }

    return pow(max(vec3(0), lights(p, rd, N(p, d))), vec3(.4545));
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    uv += vec2(0.5);
    uv.y = 1. - uv.y;
    uv -= vec2(0.5);

    float t = 0.0;

    float st = S(0., 15., t);
    vec3 lookAt = vec3(0, 0.3, 0),
         ro = vec3(.5, .001, -2);
    out_color = vec4(march(ro, rayDir(ro, lookAt, uv)), 0);
    out_color.a = 1.;


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

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

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    return out_color;
}
