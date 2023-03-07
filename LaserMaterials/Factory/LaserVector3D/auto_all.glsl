/*{
  "INPUTS": [
    { "LABEL": "Global/Translation", "NAME": "mat_base_transition", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Rotation", "NAME": "mat_base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

    { "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Move/Strob", "NAME": "mat_automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "Smooth" },
    { "LABEL": "Auto Move/Offset", "NAME": "mat_automoveoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Rotate/Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Rotate/Axis", "NAME": "mat_autorotateaxis", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Rotate/Axis Offset", "NAME": "mat_autorotateaxisoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 3.0 },

    { "LABEL": "Auto Scale/Active", "NAME": "mat_autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Size", "NAME": "mat_autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Speed", "NAME": "mat_autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Scale/Reverse", "NAME": "mat_autoscalereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Strob", "NAME": "mat_autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Shape", "NAME": "mat_autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Step Noise","Smooth In"], "DEFAULT": "Smooth" },
    { "LABEL": "Auto Scale/Offset", "NAME": "mat_autoscaleoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

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
      "NAME": "mat_scale_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autoscalespeed","reverse": "mat_autoscalereverse","strob": "mat_autoscalestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
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

float retime(in float t)
{
  return (floor(t) + smoothstep(0.,1.,fract(t)) );
}

mat4 make3dTransformMatrix(float normalizedCellId)
{
    // Center
    mat4 linePatternsMatrix = mat4(1,0,0,0,
                                   0,1,0,0,
                                   0,0,1,0,
                                   0,0,0,1);

    // Scale
    if (mat_autoscaleactive) {
      float scale;
      // "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
      if (mat_autoscaleshape == 0) {
        scale = mat_autoscalesize * (1+sin((mat_scale_position+normalizedCellId*mat_autoscaleoffset)*2*PI))/2;
      } else if (mat_autoscaleshape == 1) {
        scale = mat_autoscalesize * (mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset),1));
      } else if (mat_autoscaleshape == 2) {
        scale = mat_autoscalesize * abs(mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset)*2+1,2)-1);
      } else if (mat_autoscaleshape  == 3) {
        scale = mat_autoscalesize * step(0.5,mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset),1));
      } else if (mat_autoscaleshape == 4) {
        scale = mat_autoscalesize * (0.5+0.5*noise(vec2((mat_scale_position+normalizedCellId*mat_autoscaleoffset*10),normalizedCellId*mat_autoscaleoffset*10)));
      } else if (mat_autoscaleshape == 5) {
        float pos = retime(retime(retime(mat_scale_position)))+normalizedCellId*mat_autoscaleoffset*10;
        float noiseValue = 0.5+0.5 * noise(vec2(pos,normalizedCellId*mat_autoscaleoffset*10));
        scale = mat_autoscalesize * noiseValue;
      } else {
        scale = mat_autoscalesize * (-0.5 * sin(-PI/2 + mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset),1)*PI));
      }
      scale = 1-scale;
      linePatternsMatrix *= mat4(scale,0,0,0,
                                 0,scale,0,0,
                                 0,0,scale,0,
                                 0,0,0,1);
    }

    // Move
    float translateX = 0;
    if (mat_automoveactive) {
      // "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
      if (mat_automoveshape == 0) {
        translateX = mat_automovesize * sin((mat_move_position+normalizedCellId*mat_automoveoffset)*2*PI) / 2;
      } else if (mat_automoveshape == 1) {
        translateX = mat_automovesize * (0.5-mod((mat_move_position+normalizedCellId*mat_automoveoffset),1));
      } else if (mat_automoveshape == 2) {
        translateX = mat_automovesize * (0.5-abs(mod((mat_move_position+normalizedCellId*mat_automoveoffset)*2+1,2)-1));
      } else if (mat_automoveshape == 3) {
        translateX = mat_automovesize * (0.5-step(0.5,mod((mat_move_position+normalizedCellId*mat_automoveoffset),1)));
      } else if (mat_automoveshape == 4) {
        translateX = mat_automovesize * (0.5*noise(vec2((mat_move_position+normalizedCellId*mat_automoveoffset*99.5),0)));
      } else {
        translateX = mat_automovesize * (-0.5 * sin(-PI/2 + mod((mat_move_position+normalizedCellId*mat_automoveoffset),1)*PI));
      }
    }
    translateX += mat_base_transition;
    linePatternsMatrix *= mat4(1,0,0,translateX,
                               0,1,0,0,
                               0,0,1,0,
                               0,0,0,1);


    // Rotate
    if (mat_autorotateactive || (mat_base_rotation!=0)) {
      float angle = mat_base_rotation * 2*PI / 360;
      if (mat_autorotateactive) {
        angle += fract(mat_rot_position+normalizedCellId*mat_autorotateoffset) * 2*PI;
      }

      // mat_autorotateaxis from 0-3
      // 0 => Y axis
      // 1 => Z axis
      // 2 => X axis
      // 3 => Y axis again

      vec3 axis;

      float axisValue = mod(mat_autorotateaxis + normalizedCellId*mat_autorotateaxisoffset,3);

      if (axisValue < 1) {
        axis = mix(vec3(1,0,0),vec3(0,1,0),axisValue-0);
      } else if (axisValue < 2) {
        axis = mix(vec3(0,1,0),vec3(0,0,1),axisValue-1);
      } else {
        axis = mix(vec3(0,0,1),vec3(1,0,0),axisValue-2);
      }

      axis = normalize(axis);
      float s = sin(angle);
      float c = cos(angle);
      float oc = 1.0 - c;
      
      linePatternsMatrix *= 
             mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                  oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                  oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                  0.0,                                0.0,                                0.0,                                1.0);
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
