/*{
"INPUTS": [
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Global/Rotation", "NAME": "mat_base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },

    { "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Move/Strob", "NAME": "mat_automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "In" },
    { "LABEL": "Auto Move/Offset", "NAME": "mat_automoveoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Rotate/Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Light/Active", "NAME": "mat_autolightactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Light/Size", "NAME": "mat_autolightsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Light/Speed", "NAME": "mat_autolightspeed", "TYPE": "float", "DEFAULT": 3.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Light/Strob", "NAME": "mat_autolightstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Light/Shape", "NAME": "mat_autolightshape", "TYPE": "long", "VALUES": ["Smooth","In","Cut"], "DEFAULT": "Cut" },
    { "LABEL": "Auto Light/Offset", "NAME": "mat_autolightoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Color/Active", "NAME": "mat_autocoloractive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Color/Speed", "NAME": "mat_autocolorspeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Color/Strob", "NAME": "mat_autocolorstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Color/Shape", "NAME": "mat_autocolorshape", "TYPE": "long", "VALUES": ["Smooth","In","Cut"], "DEFAULT": "In" },
    { "LABEL": "Auto Color/Offset", "NAME": "mat_autocoloroffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
  ],
  "GENERATORS": [
    {
      "NAME": "mat_move_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_automovespeed","reverse": "mat_automovereverse", "strob": "mat_automovestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_rot_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_light_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autolightspeed","strob": "mat_autolightstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_color_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autocolorspeed","strob": "mat_autocolorstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
  ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId)
{
    // Center
    mat3 linePatternsMatrix = mat3(1,0,0,
                              0,1,0,
                              0,0,1);

    // Rotate
    if (mat_autorotateactive || (mat_base_rotation!=0)) {
      float angle = mat_base_rotation * 2*PI / 360;
      if (mat_autorotateactive) {
        angle += fract(0.5 + (mat_rot_position+normalizedCellId*mat_autorotateoffset)) * 2*PI;
      }
      float sin_factor = sin(angle);
      float cos_factor = cos(angle);
      //uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
      linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
                                 -sin_factor, cos_factor, 0,
                                 0, 0, 1);
    }

    return linePatternsMatrix;
}

float getLightValue(float normalizedCellId)
{
    // Light
    float light = 1;
    if (mat_autolightactive) {
      // "Smooth","In","Cut"
      if (mat_autolightshape == 0) {
        light -= mat_autolightsize * (1+sin((mat_light_position+normalizedCellId*mat_autolightoffset)*2*PI))/2;
      } else if (mat_autolightshape == 1) {
        light -= mat_autolightsize * (mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      } else {
        light -= mat_autolightsize * step(0.5,mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      }
    }
    return light;
}

vec3 mat_hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

vec4 getColorValue(float normalizedCellId)
{
    // Color
    vec4 color = vec4(1);
    if (mat_autocoloractive) {
      // "Smooth","In","Cut"
      float hue;
      if (mat_autocolorshape == 0) {
        hue = (1+sin((mat_color_position+normalizedCellId*mat_autocoloroffset)*2*PI))/2;
      } else if (mat_autocolorshape == 1) {
        hue = (mod((mat_color_position+normalizedCellId*mat_autocoloroffset),1));
      } else {
        hue = step(0.5,mod((mat_color_position+normalizedCellId*mat_autocoloroffset),1));
      }
      color = vec4(mat_hsv2rgb(vec3(hue,1.,1.)),1);
    }
    return color;
}

