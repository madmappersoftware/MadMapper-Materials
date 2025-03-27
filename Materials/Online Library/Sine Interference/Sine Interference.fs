/*{
    "CREDIT": "lolucky, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/dtd3D4",

    "VSN": "1.1",

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
            "LABEL": "Wave/Adjust",
            "NAME": "mat_adjust_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.5,0.5]
        },
        {
            "LABEL": "Wave/Adjust Scale",
            "NAME": "mat_adjust_pos_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Wave Scale",
            "NAME": "mat_wave_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Separation",
            "NAME": "mat_separation",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Wave 1",
            "NAME": "mat_wave1_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Wave 2",
            "NAME": "mat_wave2_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Soften 1",
            "NAME": "mat_soften1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Wave/Soften 2",
            "NAME": "mat_soften2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Wave/Mod 1",
            "NAME": "mat_mod1",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Mod 2",
            "NAME": "mat_mod2",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Wave/Glow",
            "NAME": "mat_glow",
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
            "DEFAULT": 0.5
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
            "LABEL": "Rotation/Animate",
            "NAME": "mat_animate_rotate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotation/Range",
            "NAME": "mat_rotate_range",
            "TYPE": "floatRange",
            "DEFAULT": [0.0,360.0],
            "MIN": -360.0,
            "MAX": 360.0
        },
        {
            "LABEL": "Rotation/Signal",
            "NAME": "mat_rotate_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Square","Inverse Square","Triangle","Sine"],
            "DEFAULT": "Saw"
        },

        {
            "LABEL": "Rotation/Filter",
            "NAME": "mat_rotate_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Rotation/Curve",
            "NAME": "mat_rotate_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotation/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "LABEL": "Rotation/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotation/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Rotation/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotation/Strobe",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Rotation/Restart",
            "NAME": "mat_rotate_restart",
            "TYPE": "event",

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
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Mix"
        },
        {
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Sensitivity",
            "NAME": "mat_back_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 4.;

float mat_rotate_time = fract((mat_rotate_time_source * 0.05  - mat_rotate_offset));

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


float matFilter(float t, long filter_type, float curve) {
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

vec2 flipX(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the X axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.x = 1.-new_coord.x;
    new_coord -= vec2(0.5);
    return new_coord;
}

vec2 flipY(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the Y axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.y = 1.-new_coord.y;
    new_coord -= vec2(0.5);
    return new_coord;
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
    uv *= mat_scale * 20.;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 10.;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    float rotate = getRotateValue();

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}


vec2 wave(vec2 p) {
    // d/dx -cosbx/(ax) = (abxsinbx+acosbx)/(ax)^2
    float a = .5 * pow(mat_soften1,2.);
    float b = 2. * mat_wave_scale;
    float x1 = a * length(p) + 1. * pow(mat_soften2,2.);
    float x2 = length(p) * b - mat_time;
    return - normalize(p) * (b * x1 * sin(x2) + a * cos(x2))/x1/x1;
}

vec3 normal( vec2 p ) {
    vec2 d = mat_wave2_level * wave(p-vec2(5 * mat_separation,0)) + mat_wave1_level * wave(p+vec2(5 * mat_separation,0));
    return normalize(cross(vec3(1 * mat_mod1, 0, d.x), vec3(0, 1 * mat_mod2, d.y)));
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    // vec2 light_pos = 2. * (mat_adjust_pos/2. + vec2(0.5));

    vec2 light_pos = mat_adjust_pos * mat_adjust_pos_scale * 4.;
    light_pos = flipX(light_pos, 1.);
    // light_pos = flipY(light_pos, 1.);

    out_color = vec4(vec3(1., 1., 1.) * dot(
        normal(uv),
        normalize(vec3(light_pos, 1 * mat_glow))),
    1);

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // General way to add transparency to any shader
    if (mat_back_mode == 1) {
        // Differentiate front & back colors using a hard cut w/ threshold
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = mat_back_color;
        } else {
            out_color = mat_front_color;
        }
    } else {
        // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * mat_back_sensitivity + mat_back_thresh);
    }

    return out_color;
}
