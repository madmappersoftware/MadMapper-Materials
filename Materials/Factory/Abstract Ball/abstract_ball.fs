/*{
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Mad Team",
  "DESCRIPTION": "Ball moving from left to right - distorted with a noise",
  "TAGS": "graphic",
  "VSN": "1.0",
  "PREVIEW_ASPECT_RATIO": 1,
  "INPUTS": [
    { "LABEL": "Global/Line Width", "NAME": "mat_width", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Smooth", "NAME": "mat_smoothness", "TYPE": "float", "DEFAULT": 0.25, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
    { "LABEL": "Noise/Noise", "NAME": "mat_autonoiseactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Noise/Amount", "NAME": "mat_autonoisesize", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Noise/Speed", "NAME": "mat_autonoisespeed", "TYPE": "float", "DEFAULT": 0.6, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Noise/Strob", "NAME": "mat_autonoisestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Move/Auto Move", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 0.6, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Move/Strob", "NAME": "mat_automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Auto Scale", "NAME": "mat_autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Scale/Size", "NAME": "mat_autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Speed", "NAME": "mat_autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Scale/Strob", "NAME": "mat_autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Shape", "NAME": "mat_autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "Label": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "Label": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
  ],
  "GENERATORS": [
    {
      "NAME": "mat_noise_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autonoisespeed","strob": "mat_autonoisestrob", "bpm_sync": "mat_bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true}
    },
    {
      "NAME": "mat_move_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_automovespeed","strob": "mat_automovestrob", "bpm_sync": "mat_bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true}
    },
    {
      "NAME": "mat_scale_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autoscalespeed","strob": "mat_autoscalestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true}
    }
  ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

in mat3 mat_linePatternsMatrix;
in float mat_currentMoveSpeed;
in float mat_noiseAmountNow;

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = (vec3(texCoord,1) * mat_linePatternsMatrix).xy;
    float n = mat_noiseAmountNow * (vnoise(texCoord+mat_noise_position*4)-0.5);

    float dist=(length(uv*vec2(1-0.6*mat_currentMoveSpeed,1+mat_currentMoveSpeed)))/((2-mat_currentMoveSpeed));
    dist *= (1+n);

    float value=1-smoothstep(mat_width-mat_smoothness,mat_width+0.00001,dist);

    // Apply contrast
    value = mix(0.5, value, mat_contrast);

    // Apply brightness
    value += mat_brightness;

    return vec4(vec3(value),1);
}
