/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers",

    "DESCRIPTION": "A flexible texture transformer.  Good for adding motion to static content like images.  Try using this with seamless tile images for infinite scrolls",

    "VSN": "1.2",

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
            "DEFAULT": "Pre Rotate"
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
            "LABEL": "Texture/Texture",
            "NAME": "mat_tex1",
            "TYPE": "image"
        },

        {
            "LABEL": "Texture/Auto Aspect",
            "NAME": "mat_t1_auto_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture/Aspect",
            "NAME": "mat_t1_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture/Scale",
            "NAME": "mat_t1_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Texture/Flip X",
            "NAME": "mat_t1_flip_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture/Flip Y",
            "NAME": "mat_t1_flip_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture/Extend",
            "NAME": "mat_t1_extend",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.75
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Offset Scale",
            "NAME": "mat_shift_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scroll/Strobe",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Restart",
            "NAME": "mat_shift_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Wobble/Animate",
            "NAME": "mat_wobble_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wobble/Type",
            "NAME": "mat_wobble_mode",
            "TYPE": "long",
            "VALUES": ["Circular","Noise 1","Noise 2"],
            "DEFAULT": "Circular"
        },
        {
            "Label": "Wobble/Range",
            "NAME": "mat_wobble_range",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.1
        },
        {
            "LABEL": "Wobble/Speed",
            "NAME": "mat_wobble_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Wobble/BPM Sync",
            "NAME": "mat_wobble_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wobble/Reverse",
            "NAME": "mat_wobble_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Wobble/Offset",
            "NAME": "mat_wobble_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Wobble/Offset Scale",
            "NAME": "mat_wobble_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Wobble/Strobe",
            "NAME": "mat_wobble_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Wobble/Restart",
            "NAME": "mat_wobble_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Rotate/Animate",
            "NAME": "mat_animate_rotate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Range",
            "NAME": "mat_rotate_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,360.0],
            "MIN": -360.0,
            "MAX": 360.0
        },
        {
            "LABEL": "Rotate/Signal",
            "NAME": "mat_rotate_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },

        {
            "LABEL": "Rotate/Filter",
            "NAME": "mat_rotate_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Rotate/Curve",
            "NAME": "mat_rotate_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Strobe",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Rotate/Restart",
            "NAME": "mat_rotate_restart",
            "TYPE": "event",

        },

        {
            "LABEL": "Scale/Animate",
            "NAME": "mat_animate_scale",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scale/Mode",
            "NAME": "mat_scale_mode",
            "TYPE": "long",
            "VALUES": ["Add","Subtract"],
            "DEFAULT": "Add"
        },
        {
            "LABEL": "Scale/Range",
            "NAME": "mat_scale_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,1.0],
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Scale/Signal",
            "NAME": "mat_scale_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },
        {
            "LABEL": "Scale/Filter",
            "NAME": "mat_scale_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Scale/Curve",
            "NAME": "mat_scale_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Scale/Speed",
            "NAME": "mat_scale_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Scale/BPM Sync",
            "NAME": "mat_scale_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scale/Reverse",
            "NAME": "mat_scale_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scale/Offset",
            "NAME": "mat_scale_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Scale/Strobe",
            "NAME": "mat_scale_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Scale/Restart",
            "NAME": "mat_scale_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "FX/Enable",
            "NAME": "mat_t1_fx_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "FX/Cosine Mix",
            "NAME": "mat_t1_cosine_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Cosine Palette",
            "NAME": "mat_t1_cosine_palette",
            "TYPE": "long",
            "DEFAULT": "Rainbow",
            "VALUES": [ "Gray", "Rainbow", "Blue-Brown", "Blue-Pink", "Savanah","Pink-Brown","Pop","Pinky" ]
        },
        {
            "LABEL": "FX/Cosine Cycle",
            "NAME": "mat_t1_cosine_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Cosine Post Multiply",
            "NAME": "mat_t1_cosine_postmult",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "FX/Mono Mix",
            "NAME": "mat_t1_mono_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Mono Color",
            "NAME": "mat_t1_mono_color",
            "TYPE": "color",
            "DEFAULT": [
                0.6,
                0.45,
                0.3,
                1.0
            ]
        },
        {
            "LABEL": "FX/Mono Thresh",
            "NAME": "mat_t1_mono_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "FX/Soft BW Mix",
            "NAME": "mat_t1_bw_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Soft BW Gain",
            "NAME": "mat_t1_bw_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "FX/Brightness",
            "NAME": "mat_t1_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Contrast",
            "NAME": "mat_t1_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "FX/Saturation",
            "NAME": "mat_t1_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Hue",
            "NAME": "mat_t1_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "FX/Invert",
            "NAME": "mat_t1_invert",
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
            "VALUES": ["Before FX", "After FX"],
            "DEFAULT": "After FX",
            "FLAGS": "generate_as_define"
        },




    ],
    "GENERATORS": [
        {
            "NAME": "mat_shift_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shift_speed",
                "speed_curve":2,
                "reverse": "mat_shift_reverse",
                "strob" : "mat_shift_strob",
                "reset": "mat_shift_restart",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_wobble_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_wobble_speed",
                "speed_curve":2,
                "reverse": "mat_wobble_reverse",
                "strob" : "mat_wobble_strob",
                "reset": "mat_wobble_restart",
                "bpm_sync": "mat_wobble_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_rotate_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rotate_speed",
                "speed_curve":2,
                "strob" : "mat_rotate_strob",
                "reverse": "mat_rotate_reverse",
                "bpm_sync": "mat_rotate_bpm_sync",
                "reset": "mat_rotate_restart",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_scale_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scale_speed",
                "speed_curve":2,
                "strob" : "mat_scale_strob",
                "reverse": "mat_scale_reverse",
                "bpm_sync": "mat_scale_bpm_sync",
                "reset": "mat_scale_restart",
                "link_speed_to_global_bpm":true
            }
        },
    ],
    "IMPORTED": [
        {
            "NAME": "noiseLUT",
            "PATH": "noiseLUT.png",
            "GL_TEXTURE_MIN_FILTER": "LINEAR",
            "GL_TEXTURE_MAG_FILTER": "LINEAR",
            "GL_TEXTURE_WRAP": "REPEAT"
        }
    ]

}*/

/*
By Jason Beyers - October 2023

This material was originally made to display static images (like midjourney images) in a fun format.  But it can certainly be useful for animated textures too!  Play around with the settings - you can do a LOT with this one :)

FYI: This material has a lot of controls, so it takes a while for Madmapper to load this in a project.  I went a little crazy with the inputs haha.
*/

#define NOISE_TEXTURE_BASED
#include "MadCommon.glsl"
#include "MadNoise.glsl"

// int REGIONS = 4;
// int PHASES = 4;

// Timers
// Most of these are modified in other functions

float mat_shift_time = (mat_shift_time_source - mat_shift_offset * mat_shift_offset_scale * 4.) * 0.05;
float mat_wobble_time = (mat_wobble_time_source - mat_wobble_offset * mat_wobble_offset_scale * 4.);
float mat_rotate_time = fract((mat_rotate_time_source * 0.05  - mat_rotate_offset));
float mat_scale_time = fract(mat_scale_time_source * 0.125 - mat_scale_offset);

// For Cosine Palette FX
const vec3 MAT_ONE3 = vec3(1.);
const vec3 MAT_HALF3 = vec3(0.5);

vec3 mat_palette( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

// Easing & filtering functions

float matEaseInOut(float t, float curve) {
    // Simple ease-in-out function
    if (t < 0.5) {
        return pow(2 * t, curve) / 2.;
    } else {
        return 1. - pow(-2. * t + 2, curve) / 2.;
    }
}

float matEaseIn(float t, float curve) {
    // Simple ease-in function
    return pow(t, curve);
}

float matEaseOut(float t, float curve) {
    // Simple ease-out function
    return 1. - pow(1. - t, curve);
}

float matEaseOutIn(float t, float curve) {
    // Ease-out-in function
    // This gets squirrely with high curve values
    return (pow(t, 3.) - 2. * pow(t, 2.) + t) * curve + (-2. * pow(t,3.) + 3. * pow(t, 2.)) + (pow(t, 3.) - pow(t, 2.)) * curve;
}


float matFilter(float t, int filter_type, float curve) {
    // Apply one of four filters to time-varying variable t (ranging from 0 to 1) with curve

    if (filter_type == 0) { // Ease In
        return matEaseIn(t, curve);
    } else if (filter_type == 1) { // Ease Out
        return matEaseOut(t, curve);
    } else if (filter_type == 2) { // Ease In Out
        return matEaseInOut(t, curve);
    } else { // Ease Out In
        return matEaseOutIn(t, curve);
    }
}

// Various helper functions

float matCircle(in vec2 _st, in float _radius){
    vec2 dist = _st-vec2(0.5);
    return 1.-smoothstep(_radius-(_radius*0.01),
                         _radius+(_radius*0.01),
                         dot(dist,dist)*4.0);
}

float getNoise(vec2 p) {
    p += vec2(0.5);
    p *= 0.1;
    p -= vec2(0.5);
    float value = texture(noiseLUT, p).x * 0.5; // Adjust the texture sampler and scale as needed
    // return clamp(value, 0.2, 0.8);
    return value;
}


vec2 uvNoiseOffset(vec2 uv, float time) {
    vec2 noiseOffset = vec2(getNoise(uv + time), getNoise(uv - time));
    return noiseOffset;
}

// UV transform functions

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

vec2 applyScale(vec2 uv) {
    // Apply UV scale transforms to main output

    if (mat_animate_scale) {
        // scale_time = fract(mat_scale_time);

        float scale_time;

        if (mat_scale_signal == 0) { // Saw
            scale_time = mat_scale_time;
        } else if (mat_scale_signal == 1) { // Inverse Saw
            scale_time = 1. - mat_scale_time;
        } else if (mat_scale_signal == 2) { // Square
            scale_time = floor(mat_scale_time + 0.5);
        } else if (mat_scale_signal == 3) { // Inverse Square
            scale_time = 1. - floor(mat_scale_time + 0.5);
        } else if (mat_scale_signal == 4) { // Triangle
            scale_time = abs(0.5 - mat_scale_time);
        } else { // Sine
            scale_time = 0.5 + 0.5 * sin(2. * PI * mat_scale_time);
        }
        scale_time = matFilter(scale_time, mat_scale_filter, mat_scale_curve);

        scale_time = 1. - scale_time;

        float range_min = mat_scale_range[0];
        float range_max = mat_scale_range[1];
        scale_time = range_min + scale_time * (range_max - range_min);

        if (mat_scale_mode == 0) { // Add
            return uv * mat_scale * (1. + scale_time);
        } else { // Subtract
            return uv * mat_scale / (1. + scale_time);
        }

    } else {
        return uv * mat_scale;
    }
}

vec2 applyUVShift(vec2 uv, float rotate) {
    // Apply UV shift for main output

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 0.5;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    if (mat_shift_animate) {
        float shift_time_x = mat_shift_time * cos(2.*PI * mat_shift_angle/360.0);
        float shift_time_y = mat_shift_time * sin(2.*PI * mat_shift_angle/360.0);
        uv_shift.x -= shift_time_x;
        uv_shift.y -= shift_time_y;
    }
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(rotate) / 360);
    uv_shift -= vec2(0.5);
    uv += uv_shift;
    return uv;
}

vec2 applyUVWobble(vec2 uv) {
    // Apply UV wobble for main output

    float tile_pos_time;

    if (mat_wobble_animate) {
        float wobble_time = mat_wobble_time;
        vec2 wobble;

        float range_min = 0.;
        float range_max = mat_wobble_range;

        if (mat_wobble_mode == 0) { // Circular

           wobble_time *= 4.;
            float power = 0.5;
            float range = range_min + 1. * (range_max - range_min);
            wobble = 0.25 * power * range * vec2(sin(wobble_time), cos(wobble_time));

        } else if (mat_wobble_mode == 1) { // Noise 1
            wobble_time /= 32.;
            wobble = uvNoiseOffset(vec2(0.5), wobble_time);
            wobble -= vec2(0.5);
            wobble *= 2.;
            wobble.x = range_min + (wobble.x + 0.5) * (range_max - range_min);
            wobble.y = range_min + (wobble.y + 0.5) * (range_max - range_min);
        } else {
            wobble_time /= 1.5;
            range_max /= 32.;
            wobble = dnoise(vec2(wobble_time, 0.)).yz;
            wobble -= vec2(0.5);
            wobble *= 2.;
            wobble.x = range_min + (wobble.x + 0.5) * (range_max - range_min);
            wobble.y = range_min + (wobble.y + 0.5) * (range_max - range_min);
        }
        return uv + wobble;
    } else {
        return uv;
    }
}

float getRotateValue() {
    // Return the rotation value (0-1) for main output

    float rotate;
    float rotate_time;

    if (mat_animate_rotate) {

        if (mat_rotate_signal == 0) { // Saw
            rotate_time = mat_rotate_time;
        } else if (mat_rotate_signal == 1) { // Inverse Saw
            rotate_time = 1. - mat_rotate_time;
        } else if (mat_rotate_signal == 2) { // Square
            rotate_time = floor(mat_rotate_time + 0.5);
        } else if (mat_rotate_signal == 3) { // Inverse Square
            rotate_time = 1. - floor(mat_rotate_time + 0.5);
        } else if (mat_rotate_signal == 4) { // Triangle
            rotate_time = abs(0.5 - mat_rotate_time);
        } else { // Sine
            rotate_time = 0.5 + 0.5 * sin(2. * PI * mat_rotate_time);
        }

        rotate_time = 1. - rotate_time;
        // rotate_time = matEaseInOut(rotate_time, mat_rotate_curve);
        rotate_time = matFilter(rotate_time, mat_rotate_filter, mat_rotate_curve);
        float range_min = mat_rotate_range[0] / 360.0;
        float range_max = mat_rotate_range[1] / 360.0;
        rotate_time = range_min + rotate_time * (range_max - range_min);

        rotate = (rotate_time + mat_rotate / 360.) * 360.;
    } else {
        rotate = mat_rotate;
    }
    return rotate;
}

vec2 transformUV(vec2 uv) {
    // UV transforms for the main output

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv = applyScale(uv) * 0.5;

    uv = mirrorUV(uv);

    float rotate = getRotateValue();

    // XY shift pre rotate
    if (mat_shift_type == 0) {
        uv = applyUVShift(uv, -1 * rotate);
        uv = applyUVWobble(uv);
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv = applyUVShift(uv, rotate);
        uv = applyUVWobble(uv);
    }
    return uv;
}



vec2 applyGenericUV(vec2 uv, float scale, float aspect, float rotate, vec2 offset, bool flip_x, bool flip_y) {
    // Manipulate UV coordinates in a general way

    uv -= vec2(0.5);
    uv *= scale;
    uv.x *= aspect;
    uv += vec2(0.5);
    if (flip_x) {
        uv.x = 1. - uv.x;
    }
    if (flip_y) {
        uv.y = 1. - uv.y;
    }
    vec2 uv_shift = offset;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv -= vec2(0.5);
    uv.x /= aspect;
    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*rotate / 360);
    uv -= vec2(0.5);
    uv.x *= aspect;
    uv += vec2(0.5);
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(rotate) / 360);
    uv_shift -= vec2(0.5);
    uv += uv_shift;
    return uv;
}

// FX functions 

vec4 applyColorControls(vec4 color, float brightness, float contrast, float saturation, float hue, bool invert) {
    // Apply color controls FX to the provided vec4 color

    // Apply invert
    if (invert) color.rgb=1-color.rgb;

    // Apply Hue Shift and saturation
    if (hue > 0.01 || saturation != 0) {
        vec3 hsv = rgb2hsv(color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+hue));
        hsv.y = max(hsv.y + saturation, 0);
        color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(color.rgb, LumCoeff));
    color.rgb = mix(AvgLumin, color.rgb, contrast);

    // Apply brightness
    color.rgb += brightness;

    return color;
}

vec4 applyCosinePalette(vec4 color, float fx_mix, int palette, float cycle, bool postmult) {
    // Apply cosine palette FX to the provided vec4 color

    if (fx_mix > 0.0) {
        // original by iquilez https://iquilezles.org/www/articles/palettes/palettes.htm
        vec4 original = color;
        float lum = luma(original.rgb);
        lum = fract(lum + cycle);
        float lumO = lum;

        if(palette == 0) // gray
        { color.rgb = mat_palette(lum,MAT_ONE3,MAT_ONE3,MAT_ONE3,MAT_ONE3);}
        else if(palette == 1) // rainbow
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.,0.33,0.67));}
        else if(palette == 2) // blue-brown
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.,0.1,0.2));}
        else if(palette == 3) // blue-pink
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,MAT_ONE3,vec3(0.3,0.2,0.2));}
        else if(palette == 4) // savanah
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(1.,1.,0.5),vec3(0.8,0.9,0.3));}
        else if(palette == 5) // pink-brown
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(1.,0.7,0.4),vec3(0.0,0.15,0.2));}
        else if(palette == 6) // pop
        { color.rgb = mat_palette(lum,MAT_HALF3,MAT_HALF3,vec3(2.,1.,0.),vec3(0.5,0.2,0.25));}
        else if(palette == 7) // pinky
        { color.rgb = mat_palette(lum,vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.,1.,1.),vec3(0.,0.25,0.25));}

        if(postmult == true) {
            color.rgb *= vec3(lumO);
        }

        color.rgb = mix(color.rgb,original.rgb,1.-fx_mix);
    }

    return color;
}

vec4 applyColorMonochrome(vec4 color, float fx_mix, vec4 mono_color, float thresh) {
    // Apply color monochrome FX to the provided vec4 color

    if (fx_mix > 0.0) {
        vec4 original = color;
        const vec4  lumcoeff = vec4(0.2126, 0.7152, 0.0722, 0.0);
        float       luminance = dot(original,lumcoeff);
        color = (luminance >= thresh)
            ? mix(mono_color, vec4(1,1,1,1), (luminance-thresh)*2.0)
            : mix(vec4(0,0,0,1), mono_color, luminance*2.0);
        color.rgb = mix(color.rgb, original.rgb,1.-fx_mix);
    }
    return color;
}

vec4 applySoftBW(vec4 color, float fx_mix, float gain) {
    // Apply soft black & white FX to the provided vec4 color
    // This FX can do things that basic saturation & contrast cannot

    if (fx_mix > 0.0) {
        vec4 original = color;
        vec3 raw_color = color.rgb;
        float col_mag = (dot(vec3(1.0), raw_color.rgb) / 3.0 * gain);
        col_mag = smoothstep(0.0, 1.0, col_mag);
        col_mag = smoothstep(0.0, 1.0, col_mag);
        raw_color = vec3(1.0) * col_mag;
        color.rgb = raw_color;
        color.rgb = mix(color.rgb, original.rgb,1.-fx_mix);
    }
    return color;
}

float distToLine(vec2 pt1, vec2 pt2, vec2 testPt) {
    // Helper function for applySoftBorder

    vec2 lineDir = pt2 - pt1;
    vec2 perpDir = vec2(lineDir.y, -lineDir.x);
    vec2 dirToPt1 = pt1 - testPt;
    return abs(dot(normalize(perpDir), dirToPt1));
}

// Texture sampling functions

vec4 getT1Color(vec2 uv) {
    // Sample texture 2

    float aspect = mat_t1_aspect;

    if (mat_t1_auto_aspect) {
        vec2 t1_size = IMG_SIZE(mat_tex1);
        aspect = t1_size.y / t1_size.x;
    }

    uv = applyGenericUV(uv, mat_t1_scale, aspect, 0.0, vec2(0.), mat_t1_flip_x, mat_t1_flip_y);
    vec4 color = IMG_NORM_PIXEL(mat_tex1, uv);

    if (!mat_t1_extend) {
        if ((uv.x > 1.) || (uv.x < 0.) || (uv.y > 1.) || (uv.y < 0.)) {
            color = vec4(0.);
        }
    }
    return color;
}

vec4 applyT1FX(vec4 color) {
    if (mat_t1_fx_enable) {
        color = applyCosinePalette(color, mat_t1_cosine_mix, mat_t1_cosine_palette, mat_t1_cosine_cycle, mat_t1_cosine_postmult);
        color = applyColorMonochrome(color, mat_t1_mono_mix, mat_t1_mono_color, mat_t1_mono_thresh);
        color = applySoftBW(color, mat_t1_bw_mix, mat_t1_bw_gain);
        color = applyColorControls(color, mat_t1_brightness, mat_t1_contrast, mat_t1_saturation, mat_t1_hue_shift, mat_t1_invert);
    }
    return color;
}




vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    vec2 uv_orig = uv;
    // Global UV transforms
    uv = transformUV(uv);

    uv += vec2(0.5);
    out_color = getT1Color(uv);

    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity) - mat_luma_threshold * 4. - 1.;
    }

    out_color = applyT1FX(out_color);

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity);
    }

    return out_color;
}
