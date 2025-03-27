/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Gradient Color effect that allows choose two to five colors and blending between, with many controls and auto-scroll.",
    "TAGS": "color,gradient",
    "VSN": "1.2",
    "INPUTS": [
        { "LABEL": "Color/Color Count", "NAME": "mat_color_count", "TYPE": "int", "DEFAULT": 2, "MIN": 2, "MAX": 5 },
        { "LABEL": "Color/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 3", "NAME": "mat_color3", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
        { "LABEL": "Color/Color 4", "NAME": "mat_color4", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 5", "NAME": "mat_color5", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },

        { "LABEL": "Color/Adjust/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Adjust/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },

        { "LABEL": "Global/Translate", "NAME": "mat_base_translate", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Global/Rotate", "NAME": "mat_base_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
        { "LABEL": "Global/Scale", "NAME": "mat_base_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Global/Blend Curve", "NAME": "mat_curve", "TYPE": "float", "DEFAULT": 1, "MIN": 1, "MAX": 2 },
        { "LABEL": "Global/Mode", "NAME": "mat_gradient_mode", "TYPE": "long", "VALUES": ["Linear","Circular"], "DEFAULT": "Linear" },

        { "LABEL": "Move/Auto Move", "NAME": "mat_anim1_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Move/Speed", "NAME": "mat_anim1_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Move/Reverse", "NAME": "mat_anim1_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Move/Strob", "NAME": "mat_anim1_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },

        { "LABEL": "Scale/Auto Scale", "NAME": "mat_anim2_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Scale/Size", "NAME": "mat_anim2_level", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Scale/Speed", "NAME": "mat_anim2_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Scale/Strob", "NAME": "mat_anim2_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Scale/Shape", "NAME": "mat_anim2_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear"], "DEFAULT": "Smooth" },

        { "LABEL": "Rotate/Auto Rotate", "NAME": "mat_anim3_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Rotate/Size", "NAME": "mat_anim3_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Rotate/Speed", "NAME": "mat_anim3_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Rotate/Strob", "NAME": "mat_anim3_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Rotate/Shape", "NAME": "mat_anim3_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "In" },

        { "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    ],
    "GENERATORS": [
        { "NAME": "mat_anim1", "TYPE": "animator", "PARAMS": {"active": "mat_anim1_active", "speed": "mat_anim1_speed", "reverse": "mat_anim1_reverse", "strob": "mat_anim1_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "In"}},
        { "NAME": "mat_anim2", "TYPE": "animator", "PARAMS": {"active": "mat_anim2_active", "speed": "mat_anim2_speed", "strob": "mat_anim2_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim2_shape"}},
        { "NAME": "mat_anim3", "TYPE": "animator", "PARAMS": {"active": "mat_anim3_active", "speed": "mat_anim3_speed", "strob": "mat_anim3_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim3_shape"}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel(vec2 texCoord)
{
    float angle = (mat_base_rotate + mat_anim3_level*mat_anim3 * 360) * 2*PI / 360;
    float sin_factor = sin(angle);
    float cos_factor = cos(angle);
    vec2 uv = ((texCoord-vec2(0.5)) * mat2(cos_factor, sin_factor, -sin_factor, cos_factor));
    float pos;
    if (mat_gradient_mode==0)
        pos = uv.x;
    else
        pos = -length(uv);
    pos *= pow(mat_base_scale,3) + mat_anim2_level*mat_anim2_level * mat_anim2;
    pos += 0.5 + mat_base_translate + mat_anim1;
    pos = fract(pos);

    vec4 colors[5];
    colors[0] = mat_color1;
    colors[1] = mat_color2;
    colors[2] = mat_color3;
    colors[3] = mat_color4;
    colors[4] = mat_color5;

    float stepSize = 1.0/mat_color_count;
    int stepId = int(pos/stepSize);
    vec4 col = mix(colors[stepId%mat_color_count],colors[(stepId+1)%mat_color_count],pow(pos/stepSize-stepId,pow(mat_curve,6)));

    // Apply contrast
    col.rgb = mix(vec3(0.5), col.rgb, mat_contrast);

    // Apply brightness
    col.rgb += vec3(mat_brightness);

    return col;
}

