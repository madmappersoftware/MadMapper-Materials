/*{
  "INPUTS": [
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

    { "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Rotate/Axis", "NAME": "mat_autorotateaxis", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },

    { "LABEL": "Auto Scale/Active", "NAME": "mat_autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Size", "NAME": "mat_autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Speed", "NAME": "mat_autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Scale/Reverse", "NAME": "mat_autoscalereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Strob", "NAME": "mat_autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Shape", "NAME": "mat_autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Step Noise","Smooth In"], "DEFAULT": "Smooth" },
  ],
  "GENERATORS": [
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
  ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

float retime(in float t)
{
  return (floor(t) + smoothstep(0.,1.,fract(t)) );
}

mat4 make3dTransformMatrix()
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
        scale = mat_autoscalesize * (1+sin((mat_scale_position)*2*PI))/2;
      } else if (mat_autoscaleshape == 1) {
        scale = mat_autoscalesize * (mod((mat_scale_position),1));
      } else if (mat_autoscaleshape == 2) {
        scale = mat_autoscalesize * abs(mod((mat_scale_position)*2+1,2)-1);
      } else if (mat_autoscaleshape  == 3) {
        scale = mat_autoscalesize * step(0.5,mod((mat_scale_position),1));
      } else if (mat_autoscaleshape == 4) {
        scale = mat_autoscalesize * (0.5+0.5*noise(vec2((mat_scale_position*10),0)));
      } else if (mat_autoscaleshape == 5) {
        float pos = retime(retime(retime(mat_scale_position)));
        float noiseValue = 0.5+0.5 * noise(vec2(pos,0));
        scale = mat_autoscalesize * noiseValue;
      } else {
        scale = mat_autoscalesize * (-0.5 * sin(-PI/2 + mod(mat_scale_position,1)*PI));
      }
      scale = 1-scale;
      linePatternsMatrix *= mat4(scale,0,0,0,
                                 0,scale,0,0,
                                 0,0,scale,0,
                                 0,0,0,1);
    }

    // Rotate
    if (mat_autorotateactive) {
      float angle = fract(mat_rot_position) * 2*PI;

      // mat_autorotateaxis from 0-3
      // 0 => Y axis
      // 1 => Z axis
      // 2 => X axis
      // 3 => Y axis again

      vec3 axis;

      float axisValue = mod(mat_autorotateaxis,3);

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
