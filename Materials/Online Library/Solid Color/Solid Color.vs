/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": [ "Color" ],
    "DESCRIPTION": "Solid Color working in RGB or HSV mode.",
    "TAGS": "geometry,shape,line",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Color/Base Color", "NAME": "mat_base_color", "TYPE": "color", "DEFAULT": [ 1, 1, 1, 1.0 ] },
        { "LABEL": "Color/Hue", "NAME": "mat_hue", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Saturation", "NAME": "mat_saturation", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Value", "NAME": "mat_value", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },

        { "LABEL": "Hue/Animate Hue", "NAME": "mat_anim1_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Hue/Size", "NAME": "mat_anim1_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Hue/Speed", "NAME": "mat_anim1_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Hue/Strob", "NAME": "mat_anim1_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Hue/Shape", "NAME": "mat_anim1_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "In" },

        { "LABEL": "Saturation/Animate Saturation", "NAME": "mat_anim2_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Saturation/Size", "NAME": "mat_anim2_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Saturation/Speed", "NAME": "mat_anim2_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Saturation/Strob", "NAME": "mat_anim2_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Saturation/Shape", "NAME": "mat_anim2_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth" },

        { "LABEL": "Value/Animate Value", "NAME": "mat_anim3_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Value/Size", "NAME": "mat_anim3_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Value/Speed", "NAME": "mat_anim3_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Value/Strob", "NAME": "mat_anim3_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Value/Shape", "NAME": "mat_anim3_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth" },

        { "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false },
    ],
    "GENERATORS": [
        { "NAME": "mat_anim1", "TYPE": "animator", "PARAMS": {"active": "mat_anim1_active", "speed": "mat_anim1_speed", "strob": "mat_anim1_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim1_shape"}},
        { "NAME": "mat_anim2", "TYPE": "animator", "PARAMS": {"active": "mat_anim2_active", "speed": "mat_anim2_speed", "strob": "mat_anim2_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim2_shape"}},
        { "NAME": "mat_anim3", "TYPE": "animator", "PARAMS": {"active": "mat_anim3_active", "speed": "mat_anim3_speed", "strob": "mat_anim3_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim3_shape"}},
    ]
}*/

#include "MadCommon.glsl"

out vec4 finalColor;

void materialVsFunc(vec2 uv) {
    vec3 baseRgb = mat_base_color.rgb;
    vec3 hsv = rgb2hsv(baseRgb);
    hsv[0] += mat_hue;
    hsv[1] += mat_saturation;
    hsv[2] += mat_value;
    hsv[0] = fract(hsv[0] + mat_anim1_level*mat_anim1);
    hsv[1] *= 1-mat_anim2_level*mat_anim2;
    hsv[2] *= 1-mat_anim3_level*mat_anim3;
    finalColor = vec4(hsv2rgb(hsv),1);
}
