/*{
    "CREDIT": "Jason Beyers",

    "DESCRIPTION": "Line animation with non-looping options",

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
            "LABEL": "Line/Width",
            "NAME": "mat_width",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },

        {
            "LABEL": "Line/Smooth",
            "NAME": "mat_smooth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Position/Translate",
            "NAME": "mat_pos",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Position/Align",
            "NAME": "mat_align",
            "TYPE": "long",
            "VALUES": ["Center","Edge"],
            "DEFAULT": "Center"
        },

        {
            "LABEL": "Position/Animate",
            "NAME": "mat_pos_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "LABEL": "Position/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Position/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Position/BPM Phase Sync",
            "NAME": "mat_bpm_phase_sync",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Position/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Position/Time Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Position/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Position/Signal",
            "NAME": "mat_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Triangle","Sine"],
            "DEFAULT": "Saw"
        },

        {
            "Label": "Position/Signal Offset",
            "NAME": "mat_signal_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Position/Filter",
            "NAME": "mat_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Position/Curve",
            "NAME": "mat_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Position/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Position/Loop",
            "NAME": "mat_loop",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "Label": "Position/Cycles",
            "NAME": "mat_cycles",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 1
        },



        {
            "LABEL": "Width/Animate",
            "NAME": "mat_width_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "Label": "Width/Range",
            "NAME": "mat_width_range",
            "TYPE": "floatRange",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": [0.0,1.0]
        },
        {
            "LABEL": "Width/Speed",
            "NAME": "mat_width_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Width/BPM Sync",
            "NAME": "mat_width_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Width/BPM Phase Sync",
            "NAME": "mat_width_bpm_phase_sync",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Width/Reverse",
            "NAME": "mat_width_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Width/Time Offset",
            "NAME": "mat_width_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Width/Strob",
            "NAME": "mat_width_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Width/Signal",
            "NAME": "mat_width_signal",
            "TYPE": "long",
            "VALUES": ["Saw","Inverse Saw","Triangle","Sine"],
            "DEFAULT": "Sine"
        },

        {
            "Label": "Width/Signal Offset",
            "NAME": "mat_width_signal_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Width/Filter",
            "NAME": "mat_width_filter",
            "TYPE": "long",
            "VALUES": ["Ease In","Ease Out","Ease In Out","Ease Out In"],
            "DEFAULT": "Ease In Out"
        },
        {
            "LABEL": "Width/Curve",
            "NAME": "mat_width_curve",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Width/Restart",
            "NAME": "mat_width_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Width/Loop",
            "NAME": "mat_width_loop",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "Label": "Width/Cycles",
            "NAME": "mat_width_cycles",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 1
        },


        {
            "LABEL": "Color/Color",
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
            "LABEL": "Color/Alpha",
            "NAME": "mat_alpha",
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
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "bpm_phase_sync": "mat_bpm_phase_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_width_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_width_speed",
                "speed_curve":2,
                "reverse": "mat_width_reverse",
                "strob" : "mat_width_strob",
                "reset": "mat_width_restart",
                "bpm_sync": "mat_width_bpm_sync",
                "bpm_phase_sync": "mat_width_bpm_phase_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
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

float apply_lfo(float time, bool loop, int cycles, long signal_type, float signal_offset, long filter_type, float curve) {

    float pos = time;

    if (loop) {
        pos = fract(pos);
    } else {
        if (pos > cycles) {
            pos = 1.;
        }
    }

    // Signal offset
    // Set to 0.5 with Ease In Out + curve>1 for some fun movement
    pos += signal_offset;

    pos = fract(pos);

    if (signal_type == 0) { // Saw
        pos = pos; // do nothing
    } else if (signal_type == 1) { // Inverse Saw
        pos = 1. - pos;
    } else if (signal_type == 2) { // Triangle
        pos = 2. * abs(0.5 - pos);
        pos = 1. - pos;
    } else { // Sine
        if (!loop) {
            pos -= 0.25;
        }
        pos = 0.5 + 0.5 * sin(2. * PI*(pos));
    }

    pos = fract(pos);

    // Filter
    pos = matFilter(pos, filter_type, curve);

    return pos;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    float mat_time = mat_time_source * 0.5 + mat_offset;
    float mat_width_time = mat_width_time_source * 0.5 + mat_width_offset;

    float width = mat_width;
    if (mat_width_animate) {

        width = apply_lfo(mat_width_time, mat_width_loop, mat_width_cycles, mat_width_signal, mat_width_signal_offset, mat_width_filter, mat_width_curve);

        float range_min = mat_width_range[0];
        float range_max = mat_width_range[1];

        width = range_min + width * (range_max - range_min);
    }

    float minSmoothness = 0.001;
    float widthValue = 1.0;
    vec2 p = uv;
    float finalSmoothness = minSmoothness + mat_smooth * width * widthValue/2;
    float halfFinalWidth = width * widthValue / 2;

    float pos = 0.;

    if (mat_pos_animate) {
        pos = apply_lfo(mat_time, mat_loop, mat_cycles, mat_signal, mat_signal_offset, mat_filter, mat_curve);
    }

    // Start pos
    pos += mat_pos + 0.5;

    pos = fract(pos);

    // Align
    if (mat_align == 1) { // Edge
        pos += halfFinalWidth;
    }

    pos = fract(pos);

    vec2 uv2 = (vec3(p,1)).xy;
    float dist=fract(uv2.x + halfFinalWidth - pos) - halfFinalWidth;

    // Be sure we can fill the screen with lines (when width==1) even with this min smoothness (that avoids aliasing on edges)
    dist *= (1-2*minSmoothness);

    float value;
    if (dist>0) {
      value=1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist);
    } else {
      value=1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);
    }

    out_color = vec4(value);

    // Apply invert
    if (mat_invert) out_color=1-out_color;

    out_color *= mat_front_color;

    if (!mat_alpha) {
        out_color.a = 1.;
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    return out_color;
}
