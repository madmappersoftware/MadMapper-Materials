/*{
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Mad Team, Collectif Scale, Furtive Vision",
  "DESCRIPTION": "V1.3, repeat < 1",
  "VSN": "1.3",
  "TAGS": "line,graphic",
  "INPUTS": [

    { "LABEL": "Global/Line Width", "NAME": "mat_width", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Translation", "NAME": "mat_base_transition", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Rotation", "NAME": "mat_base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },
    // { "LABEL": "Global/Smooth", "NAME": "mat_smoothness", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Repeat", "NAME": "mat_repeat", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.001, "MAX": 16.0 },
    { "LABEL": "Global/Polar Coord", "NAME": "mat_polar_coordinates", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Global/Symetry", "NAME": "mat_symetric", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

    { "LABEL": "Global/Align", "NAME": "mat_align", "TYPE": "long", "VALUES": ["Left","Center","Right"], "DEFAULT": "Center", "FLAGS": "generate_as_define" },

    { "LABEL": "Cells/Cells X", "NAME": "mat_cells_x", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 1 },
    { "LABEL": "Cells/Cells Y", "NAME": "mat_cells_y", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 1 },

    { "LABEL": "Feedback/Left", "NAME": "feedback_l", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. },
    { "LABEL": "Feedback/Right", "NAME": "feedback_r", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. },

    { "LABEL": "Symetry/Symetry X", "NAME": "mat_symetric_x", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Symetry/Symetry Y", "NAME": "mat_symetric_y", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

    { "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Move/Strob", "NAME": "mat_automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Auto Move/Offset", "NAME": "mat_automoveoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Width/Active", "NAME": "mat_autowidthactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Width/Size", "NAME": "mat_autowidthsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Width/Speed", "NAME": "mat_autowidthspeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Width/Reverse", "NAME": "mat_autowidthreverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Width/Strob", "NAME": "mat_autowidthstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Width/Shape", "NAME": "mat_autowidthshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Auto Width/Offset", "NAME": "mat_autowidthoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Rotate/Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Auto Scale/Active", "NAME": "mat_autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Size", "NAME": "mat_autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Speed", "NAME": "mat_autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Scale/Reverse", "NAME": "mat_autoscalereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Scale/Strob", "NAME": "mat_autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Scale/Shape", "NAME": "mat_autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Auto Scale/Offset", "NAME": "mat_autoscaleoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

    { "LABEL": "Color/Front Color", "NAME": "mat_front_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    { "LABEL": "Color/Back Color", "NAME": "mat_back_color", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
    { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
  ],
  "GENERATORS": [
    {
      "NAME": "mat_move_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_automovespeed","reverse": "mat_automovereverse", "strob": "mat_automovestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "mat_width_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autowidthspeed","reverse": "mat_autowidthreverse","strob": "mat_autowidthstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "mat_rot_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "mat_scale_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autoscalespeed","reverse": "mat_autoscalereverse","strob": "mat_autoscalestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    }
  ]
}*/

#ifndef M_PI
  #define M_PI 3.1415926535897932384626433832795
#endif

#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId) {
    // Center
  mat3 linePatternsMatrix = mat3(1, 0, -0.5, 0, 1, -0.5, 0, 0, 1);

    // Rotate
  if(mat_autorotateactive || (mat_base_rotation != 0)) {
    float angle = mat_base_rotation * 2 * M_PI / 360;
    if(mat_autorotateactive) {
      angle += fract(0.5 + (mat_rot_position + normalizedCellId * mat_autorotateoffset)) * 2 * M_PI;
    }
    float sin_factor = sin(angle);
    float cos_factor = cos(angle);
      //uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
    linePatternsMatrix *= mat3(cos_factor, sin_factor, 0, -sin_factor, cos_factor, 0, 0, 0, 1);
  }

    // Scale
  if(mat_autoscaleactive) {
    float scale;
      #if defined(mat_autoscaleshape_IS_Smooth)
    scale = mat_autoscalesize * (1 + sin((mat_scale_position + normalizedCellId * mat_autoscaleoffset) * 2 * M_PI)) / 2;
      #elif defined(mat_autoscaleshape_IS_In)
    scale = mat_autoscalesize * (mod((mat_scale_position + normalizedCellId * mat_autoscaleoffset), 1));
      #elif defined(mat_autoscaleshape_IS_Linear)
    scale = mat_autoscalesize * abs(mod((mat_scale_position + normalizedCellId * mat_autoscaleoffset) * 2 + 1, 2) - 1);
      #elif defined(mat_autoscaleshape_IS_Cut)
    scale = mat_autoscalesize * step(0.5, mod((mat_scale_position + normalizedCellId * mat_autoscaleoffset), 1));
      #elif defined(mat_autoscaleshape_IS_Noise)
    scale = mat_autoscalesize * vnoise(vec2((mat_scale_position + normalizedCellId * mat_autoscaleoffset * 10), normalizedCellId * mat_autoscaleoffset * 10));
      #endif
    scale = 0.5 / (1 - scale + 0.01);
    linePatternsMatrix *= mat3(scale, 0, 0, 0, scale, 0, 0, 0, 1);
  }

  // Move
  float translateX = 0;
  float mat_move_position = mat_move_position * mat_repeat;
  if(mat_automoveactive) {
      #if defined(mat_automoveshape_IS_Smooth)
    translateX = mat_automovesize * (1 - mat_width) * sin((mat_move_position + normalizedCellId * mat_automoveoffset) * 2 * M_PI) / 2;
      #elif defined(mat_automoveshape_IS_In)
    translateX = mat_automovesize * (0.5 - mod((mat_move_position + normalizedCellId * mat_automoveoffset), 1));
      #elif defined(mat_automoveshape_IS_Linear)
    translateX = mat_automovesize * (1 - mat_width) * (0.5 - abs(mod((mat_move_position + normalizedCellId * mat_automoveoffset) * 2 + 1, 2) - 1));
      #elif defined(mat_automoveshape_IS_Cut)
    translateX = mat_automovesize * (0.5 - step(0.5, mod((mat_move_position + normalizedCellId * mat_automoveoffset), 1)));
      #elif defined(mat_automoveshape_IS_Noise)
    translateX = mat_automovesize * (0.5 - vnoise(vec2((mat_move_position + normalizedCellId * mat_automoveoffset * 10), 0)));
      #elif defined(mat_automoveshape_IS_Smooth_In)
    translateX = mat_automovesize * (-0.5 * sin(-M_PI / 2 + mod((mat_move_position + normalizedCellId * mat_automoveoffset), 1) * M_PI));
      #endif
    translateX /= mat_repeat;
  }
  translateX -= mat_base_transition;

  // if( mat_align ){
    #if defined(mat_align_IS_Left) 
  translateX -= 0.5 + mat_width * 0.5;
    #elif defined(mat_align_IS_Right) 
  translateX += 0.5 + mat_width * 0.5;
    #endif

  // }

  linePatternsMatrix *= mat3(1, 0, translateX, 0, 1, 0, 0, 0, 1);

  return linePatternsMatrix;
}

vec4 materialColorForPixel(vec2 texCoord) {
  int cellId = int(texCoord.x * mat_cells_x) + int(texCoord.y * mat_cells_y) * mat_cells_x;
  float normalizedCellId = cellId / float(mat_cells_x * mat_cells_y);

  float mat_widthValue;

  if(mat_autowidthactive) {
      // Width
      #if defined(mat_autowidthshape_IS_Smooth)
    mat_widthValue = mat_autowidthsize * (1 + sin((mat_width_position + normalizedCellId * mat_autowidthoffset) * 2 * M_PI)) / 2;
      #elif defined(mat_autowidthshape_IS_In)
    mat_widthValue = mat_autowidthsize * (mod((mat_width_position + normalizedCellId * mat_autowidthoffset), 1));
      #elif defined(mat_autowidthshape_IS_Linear)
    mat_widthValue = mat_autowidthsize * abs(mod((mat_width_position + normalizedCellId * mat_autowidthoffset) * 2 + 1, 2) - 1);
      #elif defined(mat_autowidthshape_IS_Cut)
    mat_widthValue = mat_autowidthsize * step(0.5, mod((mat_width_position + normalizedCellId * mat_autowidthoffset), 1));
      #elif defined(mat_autowidthshape_IS_Noise)
    mat_widthValue = mat_autowidthsize * vnoise(vec2((mat_width_position + normalizedCellId * mat_autowidthoffset * 10), 2));
      #endif
  } else {
    mat_widthValue = 1;
  }

  mat3 mat_linePatternsMatrix = makeTransformMatrix(normalizedCellId);

  vec2 p = texCoord;
  vec2 cellSize = vec2(1.0 / mat_cells_x, 1.0 / mat_cells_y);
  p = mod(p, cellSize) / cellSize;
  if(mat_polar_coordinates) {
    vec2 x = p - vec2(0.5);
    p.x = length(x);
    p.y = atan(x.y, x.x) * 0.5 / M_PI + 0.5;
  }

    // Set a minimum smoothness so we avoid aliasing when rotated
  float minSmoothness = 0.000001 * mat_repeat;
  float finalSmoothness = minSmoothness + 1. * mat_width * mat_widthValue / 2;
  // float finalSmoothness = minSmoothness + mat_smoothness * mat_width * mat_widthValue / 2;
  float halfFinalWidth = mat_width * mat_widthValue / 2;

	// feedback
  vec2 finalFeedback = vec2(feedback_l / mat_repeat, feedback_r / mat_repeat);

  if(mat_automovereverse) {
    finalFeedback.xy = finalFeedback.yx;
  }

  vec2 uv = (vec3(p, 1) * mat_linePatternsMatrix).xy;
  float dist = (fract((uv.x + halfFinalWidth) * mat_repeat) / mat_repeat) - halfFinalWidth;

  // Be sure we can fill the screen with lines (when width==1) even with this min smoothness (that avoids aliasing on edges)
  dist *= (1 - 2 * minSmoothness);

  float value;

  // float leftDist = fract(-uv.x - halfFinalWidth);
  float leftDist = (fract(-(uv.x + halfFinalWidth) * mat_repeat) / mat_repeat);
  value = (1. - smoothstep(halfFinalWidth, halfFinalWidth + finalFeedback.y, dist)); // right feedback
  value += 1. - smoothstep(0., 0. + finalFeedback.x, leftDist); //leftfeedback

  if(mat_symetric_x) {
    vec2 sym_uv = (vec3(1. - p.x, p.y, 1.) * mat_linePatternsMatrix).xy;
    float dist = (fract((sym_uv.x + halfFinalWidth) * mat_repeat) / mat_repeat) - halfFinalWidth;

    dist *= (1 - 2 * minSmoothness);

    float rightDist = (fract(-(sym_uv.x + halfFinalWidth) * mat_repeat) / mat_repeat);

    value += (1. - smoothstep(halfFinalWidth, halfFinalWidth + finalFeedback.y, dist)); // right feedback
    value += 1. - smoothstep(0., 0. + finalFeedback.x, rightDist); //leftfeedback
  }

  if(mat_symetric_y) {
    vec2 sym_uv = (vec3(p.x, 1. - p.y, 1.) * mat_linePatternsMatrix).xy;
    float dist = (fract((sym_uv.x + halfFinalWidth) * mat_repeat) / mat_repeat) - halfFinalWidth;

    dist *= (1 - 2 * minSmoothness);

    float rightDist = (fract(-(sym_uv.x + halfFinalWidth) * mat_repeat) / mat_repeat);

    value += (1. - smoothstep(halfFinalWidth, halfFinalWidth + finalFeedback.y, dist)); // right feedback
    value += 1. - smoothstep(0., 0. + finalFeedback.x, rightDist); //leftfeedback
  }

  vec4 col = mix(mat_back_color, mat_front_color, value);

    // Apply contrast
  col.rgb = mix(vec3(0.5), col.rgb, mat_contrast);

    // Apply brightness
  col.rgb += vec3(mat_brightness);

  return col;
}
