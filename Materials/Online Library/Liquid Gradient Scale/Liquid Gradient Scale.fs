/*{
    "CREDIT": "Matt Beghin, Furtive Vision, Collectif Scale",
    "DESCRIPTION": "Gradient Color with deformation",
    "TAGS": "color, gradient, liquid, Collectif Scale",
    "VSN": "1.2",
    "INPUTS": [
        { "LABEL": "Color/Color Count", "NAME": "mat_color_count", "TYPE": "int", "DEFAULT": 2, "MIN": 1, "MAX": 5 },
        { "LABEL": "Color/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 3", "NAME": "mat_color3", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
        { "LABEL": "Color/Color 4", "NAME": "mat_color4", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 5", "NAME": "mat_color5", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },

        { "LABEL": "Color/Adjust/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Adjust/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },

        {"LABEL": "Deform/Active", "NAME": "deform_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        {"LABEL": "Deform/Speed", "NAME": "deform_speed", "TYPE": "float", "DEFAULT": 1.0, "MIN": -2.0, "MAX": 2.0} ,
        {"LABEL": "Deform/Power_x","NAME": "deform_power_x","TYPE": "float","DEFAULT": 0.0,"MIN": 0.0,"MAX": 1.0} ,
        {"LABEL": "Deform/Power_y","NAME": "deform_power_y","TYPE": "float","DEFAULT": 0.0,"MIN": 0.0,"MAX": 1.0},
        {"LABEL": "Deform/Power_xy","NAME": "deform_power_xy","TYPE": "float","DEFAULT": 0.25,"MIN": 0.0,"MAX": 1.0},
        {"LABEL": "Deform/Scale_x","NAME": "deform_scale_x","TYPE": "float","DEFAULT": 1.0,"MIN": 0.0,"MAX": 10.0},
        {"LABEL": "Deform/Scale_y","NAME": "deform_scale_y","TYPE": "float","DEFAULT": 1.0,"MIN": 0.0,"MAX": 10.0},
        {"LABEL": "Deform/Scale_xy","NAME": "deform_scale_xy","TYPE": "float","DEFAULT": 1.0,"MIN": 0.0,"MAX": 10.0},

        { "LABEL": "Global/Translate", "NAME": "mat_base_translate", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Global/Rotate", "NAME": "mat_base_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
        { "LABEL": "Global/Scale", "NAME": "mat_base_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Global/Blend Curve", "NAME": "mat_curve", "TYPE": "float", "DEFAULT": 1, "MIN": 1, "MAX": 2 },
        { "LABEL": "Global/Mode", "NAME": "mat_gradient_mode", "TYPE": "long", "VALUES": ["Linear","Circular"], "DEFAULT": "Linear" },

        { "LABEL": "Move/Active", "NAME": "mat_anim1_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Move/Speed", "NAME": "mat_anim1_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Move/Reverse", "NAME": "mat_anim1_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Move/Strob", "NAME": "mat_anim1_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },

        { "LABEL": "Scale/Active", "NAME": "mat_anim2_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Scale/Size", "NAME": "mat_anim2_level", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Scale/Speed", "NAME": "mat_anim2_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Scale/Strob", "NAME": "mat_anim2_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Scale/Shape", "NAME": "mat_anim2_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "Smooth" },

        { "LABEL": "Rotate/Active", "NAME": "mat_anim3_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Rotate/Size", "NAME": "mat_anim3_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Rotate/Speed", "NAME": "mat_anim3_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Rotate/Strob", "NAME": "mat_anim3_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Rotate/Shape", "NAME": "mat_anim3_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "In" },

        { "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    ],
    "GENERATORS": [
        { "NAME": "mat_anim1", "TYPE": "animator", "PARAMS": {"active": "mat_anim1_active", "speed": "mat_anim1_speed", "reverse": "mat_anim1_reverse", "strob": "mat_anim1_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "In"}},
        { "NAME": "mat_anim2", "TYPE": "animator", "PARAMS": {"active": "mat_anim2_active", "speed": "mat_anim2_speed", "strob": "mat_anim2_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim2_shape"}},
        { "NAME": "mat_anim3", "TYPE": "animator", "PARAMS": {"active": "mat_anim3_active", "speed": "mat_anim3_speed", "strob": "mat_anim3_strob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "shape": "mat_anim3_shape"}},
        { "NAME": "deform_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "deform_speed", "reverse": false, "strob": 0, "speed_curve":2, "link_speed_to_global_bpm":true} },

    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"


vec2 deformUV(vec2 uv)
{
    float t = deform_noise_time;
    float K1 = 0.;
    float K2 = 0.;
    float NX = 0.;
    float NY = 0.;

    if(deform_power_x > 0.001) NX = billowedNoise(vec2(uv.x*deform_scale_x + 1.2345,t))-0.5;
    if(deform_power_y > 0.001) NY = billowedNoise(vec2(uv.y*deform_scale_y,t))-0.5;
    if(deform_power_xy > 0.001){
        K1 = flowNoise(vec2(0.5)+(uv-vec2(0.5))*deform_scale_xy,t);
        K2 = flowNoise(uv*deform_scale_xy + 23.4321,t);
    }

    vec2 distorted = uv + (vec2(NX,NY))*vec2(deform_power_x,deform_power_y)*0.2 + (vec2(K1,K2))*deform_power_xy;
    return distorted;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    float angle = (mat_base_rotate + mat_anim3_level*mat_anim3 * 360) * 2*PI / 360;
    float sin_factor = sin(angle);
    float cos_factor = cos(angle);
    vec2 uv = ((texCoord-vec2(0.5)) * mat2(cos_factor, sin_factor, -sin_factor, cos_factor));

    if (deform_active) uv = deformUV(uv);

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

