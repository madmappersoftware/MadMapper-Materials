/*{
    "CREDIT": "104, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3tXXRX",

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
            "NAME": "mat_uv_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "LABEL": "Egg/Enable",
            "NAME": "mat_egg",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Egg/Size",
            "NAME": "mat_big_egg_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Egg/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Egg/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Egg/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Egg/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Egg Field/Size",
            "NAME": "mat_small_egg_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg Field/Span",
            "NAME": "mat_span",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg Field/Fill",
            "NAME": "mat_field_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg Field/Hole",
            "NAME": "mat_hole_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg Field/Shift",
            "NAME": "mat_field_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "Label": "Egg Field/Direction",
            "NAME": "mat_scroll_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Egg Field/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Egg Field/BPM Sync",
            "NAME": "mat_scroll_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Egg Field/Reverse",
            "NAME": "mat_scroll_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Egg Field/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Egg Field/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Distort/Distort",
            "NAME": "mat_distort",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Distort/Distort Offset",
            "NAME": "mat_distort_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "LABEL": "Vignette/Vignette",
            "NAME": "mat_vignette",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Vignette/Level",
            "NAME": "mat_vignette_lvl",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fuzz/Fuzz",
            "NAME": "mat_fuzz",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Background/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.05,
                0.0,
                1.0
            ]
        },
        {
            "LABEL": "Background/Alpha Background",
            "NAME": "mat_alpha_background",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Background/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Simple Mode",
            "NAME": "mat_color_simple_mode",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color/Outline Color",
            "NAME": "mat_outline_color",
            "TYPE": "color",
            "DEFAULT": [
                0.06,
                0.03,
                0.02,
                1.0
            ]
        },
        {
            "LABEL": "Color/Glow Color",
            "NAME": "mat_glow_color",
            "TYPE": "color",
            "DEFAULT": [
                0.2,
                0.1,
                0.2,
                1.0
            ]
        },
        {
            "LABEL": "Color/Yolk Color",
            "NAME": "mat_yolk_color",
            "TYPE": "color",
            "DEFAULT": [
                0.5,
                0.5,
                0.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Tint",
            "NAME": "mat_tint",
            "TYPE": "color",
            "DEFAULT": [
                0.72,
                0.56,
                0.56,
                1.0
            ]
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
        }

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
        {
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

float mat_scroll_time = mat_scroll_time_source - mat_scroll_offset;

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


float bigsize = .8 * mat_big_egg_size;
float smallsize = .09 * mat_small_egg_size;
float span = .1 * mat_span;

// const float PI = 3.14159;
float PI2 = PI*2.;

mat2 rot2D(float r){
    return mat2(cos(r), sin(r), -sin(r), cos(r));
}
float nsin(float x) {
    return cos(x)*.5+.5;
}
vec3 hash32(vec2 p) {
    vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yxz+19.19);
    return fract((p3.xxy+p3.yzz)*p3.zyx);
}
float opUnion( float d1, float d2 ) { return min(d1,d2); }
float opSubtraction( float d1, float d2 ) { return max(-d1,d2); }

float sdShape(vec2 p, float r) {
    return length(p+r*.5)-r;
}
void egg(inout vec2 sd, vec2 uv, float t, float r, float a) {
    if (r <= 0.) return;
    float yolk = sdShape((11./sqrt(2.))*uv*rot2D(a), r);
    float white = sdShape((5.2/sqrt(2.))*uv*rot2D(a), r);
    sd = vec2(opUnion(sd.x, white),
              opUnion(sd.y, yolk));
}

// invisible dots creating some kind of underlying disturbance
float under(vec2 N, float t) {
    const float sz = .001;
    const float range = 1.8;
    vec2 p1 = vec2(sin(-t*1.6666), cos(t)) * range;
    vec2 p2 = vec2(sin(t), sin(t*1.3333)) * range;
    float b = min(length(p1-N)-sz, length(p2-N)-sz);
    return b;
}

void smallEggField(inout vec2 sd, vec2 uv, vec2 uvbig, float t) {

    // vec2 uvsmall = mod(uv+vec2(t*.3,0),span)-span*.5;// uv within small scroll period

    float t_x = 0.3 * t * cos(2.*PI * mat_scroll_angle/360.0);
    float t_y = 0.3 * t * sin(2.*PI * mat_scroll_angle/360.0);

    vec2 field_shift = mat_field_offset;
    field_shift += vec2(0.5);
    field_shift.x = 1.-field_shift.x;
    field_shift -= vec2(0.5);
    field_shift *= 0.5;

    vec2 uvsmall = mod(uv+vec2(t_x + field_shift.x,t_y + field_shift.y),span)-span*.5;// uv within small scroll period


    vec2 uvsmall_q = (uv-uvsmall) / mat_hole_size;// uv of scrolling small egg field
    vec2 sdquant = vec2(1e6);
    // find the dist to the big egg, quantizing big egg's coords to small egg coords
    egg(sdquant, uvsmall_q, t, bigsize, under(uvsmall_q, t));
    egg(sd,uvsmall, t, smallsize * smoothstep(0.,.8,sdquant.x - .5), under(uvbig*10., t));
}

vec3 color(vec2 sd, float fact) {
    // vec3 o;
    // o.rgb = 1.-smoothstep(vec3(0),mat_field_fill*fact*vec3(.06,.03,.02), vec3(sd.x));
    // o.rgb *= vec3(.9,.7,.7)*.8;
    // o.g += .05;
    // if (sd.x < 0.) o -= sd.x*.6;
    // o = clamp(o,o-o,o-o+1.);

    // vec3 ayolk = 1.-smoothstep(vec3(0),fact*vec3(.2,.1,.2),sd.yyy);
    // o.rgb = mix(o.rgb, vec3(.5,.5,0), ayolk);
    // if (sd.y < 0.) o -= sd.y*.1;
    // o = clamp(o,o-o,o-o+1.);
    // return o;

    vec3 o;

    if (mat_color_simple_mode) {

        o.rgb = 1.-smoothstep(vec3(0),mat_field_fill*fact*vec3(0.), vec3(sd.x));
        o.rgb *= mat_tint.rgb;

        if (!mat_alpha_background) {
            o += mat_back_color.rgb;
        }
        o = clamp(o,o-o,o-o+1.);

    } else {

        o.rgb = 1.-smoothstep(vec3(0),mat_field_fill*fact*mat_outline_color.rgb, vec3(sd.x));
        o.rgb *= mat_tint.rgb;

        if (!mat_alpha_background) {
            o += mat_back_color.rgb;
        }
        if (sd.x < 0.) o -= sd.x*.6;
        o = clamp(o,o-o,o-o+1.);

        vec3 ayolk = 1.-smoothstep(vec3(0),fact*mat_glow_color.rgb,sd.yyy);
        o.rgb = mix(o.rgb, mat_yolk_color.rgb, ayolk);
        if (sd.y < 0.) o -= sd.y*.1;
        o = clamp(o,o-o,o-o+1.);
    }

    return o;

}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 1.25;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_uv_offset;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;

    vec2 N = texCoord - vec2(0.5);

    uv.y -= .1;
    vec2 sd = vec2(1e6);
    vec2 uvbig = uv * mat_distort;// uv of the big egg and disturbance layer both
    // big egg
    if (mat_egg) {
        egg(sd, uv, mat_time, bigsize, under(uvbig, mat_time));
    }
    // small eggs
    vec2 sdsmall = vec2(1e6);

    vec2 distort_shift = mat_distort_offset;
    distort_shift += vec2(0.5);
    distort_shift.x = 1.-distort_shift.x;
    distort_shift -= vec2(0.5);

    uvbig += distort_shift;

    smallEggField(sdsmall, uv, uvbig, mat_scroll_time);
    uv -= span * .5;
    smallEggField(sdsmall, uv, uvbig, mat_scroll_time);

    out_color.rgb = mix(color(sd, 2.), color(sdsmall, .2), vec3(step(.1,sd.x)));
    out_color = pow(out_color, out_color-out_color+.5);
    // out_color.rgb += (hash32(gl_FragCoord.xy+mat_time)-.5)*.1;

    if (mat_fuzz) {
        out_color.rgb += (hash32(uv+mat_time)-.5)*.1;
        out_color = clamp(out_color,out_color-out_color,out_color-out_color+1.);
    }

    if (mat_vignette) {
        out_color *= 1.-length(13.*pow(abs(N), vec2(4.))) * mat_vignette_lvl;// vingette
    }

    out_color.a = 1.;

    if (mat_alpha_background) {
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color.a = 0.;
        }
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


    return out_color;
}
