/*{
  "CREDIT": "Mad Team",
  "DESCRIPTION": "Single line pattern with many controls. Like original Line Patterns with added Auto Light",
  "VERSION": "1.0",
  "TAGS": "line,graphic",
  "INPUTS": [
    { "LABEL": "Cells/Cells X", "NAME": "mat_cells_x", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
    { "LABEL": "Cells/Cells Y", "NAME": "mat_cells_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },

    { "LABEL": "Global/Line Width", "NAME": "mat_width", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Translation", "NAME": "mat_base_transition", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Rotation", "NAME": "mat_base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },
    { "LABEL": "Global/Smooth", "NAME": "mat_smoothness", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Repeat", "NAME": "mat_repeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },
    { "LABEL": "Global/Polar Coord", "NAME": "mat_polar_coordinates", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Global/Symetry", "NAME": "mat_symetric", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

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

    { "LABEL": "Auto Light/Active", "NAME": "mat_autolightactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Auto Light/Size", "NAME": "mat_autolightsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Light/Speed", "NAME": "mat_autolightspeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Auto Light/Strob", "NAME": "mat_autolightstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Auto Light/Shape", "NAME": "mat_autolightshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Auto Light/Offset", "NAME": "mat_autolightoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

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
    },
    {
      "NAME": "mat_light_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_autolightspeed","strob": "mat_autolightstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    }
  ]
}*/

#ifndef M_PI
  #define M_PI 3.1415926535897932384626433832795
#endif

#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId)
{
    // Center
    mat3 linePatternsMatrix = mat3(1,0,-0.5,
                              0,1,-0.5,
                              0,0,1);

    // Rotate
    if (mat_autorotateactive || (mat_base_rotation!=0)) {
      float angle = mat_base_rotation * 2*M_PI / 360;
      if (mat_autorotateactive) {
        angle += fract(0.5 + (mat_rot_position+normalizedCellId*mat_autorotateoffset)) * 2*M_PI;
      }
      float sin_factor = sin(angle);
      float cos_factor = cos(angle);
      //uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
      linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
                                 -sin_factor, cos_factor, 0,
                                 0, 0, 1);
    }

    // Scale
    if (mat_autoscaleactive) {
      float scale;
      #if defined(mat_autoscaleshape_IS_Smooth)
        scale = mat_autoscalesize * (1+sin((mat_scale_position+normalizedCellId*mat_autoscaleoffset)*2*M_PI))/2;
      #elif defined(mat_autoscaleshape_IS_In)
        scale = mat_autoscalesize * (mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset),1));
      #elif defined(mat_autoscaleshape_IS_Linear)
        scale = mat_autoscalesize * abs(mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset)*2+1,2)-1);
      #elif defined(mat_autoscaleshape_IS_Cut)
        scale = mat_autoscalesize * step(0.5,mod((mat_scale_position+normalizedCellId*mat_autoscaleoffset),1));
      #elif defined(mat_autoscaleshape_IS_Noise)
        scale = mat_autoscalesize * vnoise(vec2((mat_scale_position+normalizedCellId*mat_autoscaleoffset*10),normalizedCellId*mat_autoscaleoffset*10));
      #endif
      scale = 0.5/(1-scale + 0.01);
      linePatternsMatrix *= mat3(scale,0,0,
                                 0,scale,0,
                                 0,0,1);
    }

    // Move
    float translateX = 0;
    if (mat_automoveactive) {
      #if defined(mat_automoveshape_IS_Smooth)
        translateX = mat_automovesize * (1-mat_width) * sin((mat_move_position+normalizedCellId*mat_automoveoffset/mat_repeat)*2*M_PI) / 2;
      #elif defined(mat_automoveshape_IS_In)
        translateX = mat_automovesize * (0.5-mod((mat_move_position+normalizedCellId*mat_automoveoffset/mat_repeat),1));
      #elif defined(mat_automoveshape_IS_Linear)
        translateX = mat_automovesize * (1-mat_width) * (0.5-abs(mod((mat_move_position+normalizedCellId*mat_automoveoffset/mat_repeat)*2+1,2)-1));
      #elif defined(mat_automoveshape_IS_Cut)
        translateX = mat_automovesize * (0.5-step(0.5,mod((mat_move_position+normalizedCellId*mat_automoveoffset/mat_repeat),1)));
      #elif defined(mat_automoveshape_IS_Noise)
        translateX = mat_automovesize * (0.5-vnoise(vec2((mat_move_position+normalizedCellId*mat_automoveoffset*10/mat_repeat),0)));
      #elif defined(mat_automoveshape_IS_Smooth_In)
        translateX = mat_automovesize * (-0.5 * sin(-M_PI/2 + mod((mat_move_position+normalizedCellId*mat_automoveoffset/mat_repeat),1)*M_PI));
      #endif
    }
    translateX += mat_base_transition;

    linePatternsMatrix *= mat3(1,0,translateX,
                               0,1,0,
                               0,0,1);

    return linePatternsMatrix;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    int cellId = int(texCoord.x*mat_cells_x) + int(texCoord.y*mat_cells_y) * mat_cells_x;
    float normalizedCellId = cellId / float(mat_cells_x*mat_cells_y);

    float mat_widthValue;

    if (mat_autowidthactive) {
      // Width
      #if defined(mat_autowidthshape_IS_Smooth)
        mat_widthValue = mat_autowidthsize * (1+sin((mat_width_position+normalizedCellId*mat_autowidthoffset)*2*M_PI))/2;
      #elif defined(mat_autowidthshape_IS_In)
        mat_widthValue = mat_autowidthsize * (mod((mat_width_position+normalizedCellId*mat_autowidthoffset),1));
      #elif defined(mat_autowidthshape_IS_Linear)
        mat_widthValue = mat_autowidthsize * abs(mod((mat_width_position+normalizedCellId*mat_autowidthoffset)*2+1,2)-1);
      #elif defined(mat_autowidthshape_IS_Cut)
        mat_widthValue = mat_autowidthsize * step(0.5,mod((mat_width_position+normalizedCellId*mat_autowidthoffset),1));
      #elif defined(mat_autowidthshape_IS_Noise)
        mat_widthValue = mat_autowidthsize * vnoise(vec2((mat_width_position+normalizedCellId*mat_autowidthoffset*10),2));
      #endif
    } else {
      mat_widthValue = 1;
    }

    mat3 mat_linePatternsMatrix = makeTransformMatrix(normalizedCellId);

    vec2 p          = texCoord;
    vec2 cellSize   = vec2( 1.0 / mat_cells_x, 1.0 / mat_cells_y );
    p               = mod(p, cellSize) / cellSize;
    if (mat_polar_coordinates)  {
      vec2 x = p - vec2(0.5);
      p.x = length(x);
      p.y = atan(x.y, x.x) * 0.5 / M_PI + 0.5;
    }

    // Set a minimum smoothness so we avoid aliasing when rotated
    float minSmoothness = 0.001 * mat_repeat;
    float finalSmoothness = minSmoothness + mat_smoothness * mat_width * mat_widthValue/2;
    float halfFinalWidth = mat_width * mat_widthValue / 2;

    vec2 uv = (vec3(p,1) * mat_linePatternsMatrix).xy;
    float dist=fract(uv.x*mat_repeat + halfFinalWidth) - halfFinalWidth;

    // Be sure we can fill the screen with lines (when width==1) even with this min smoothness (that avoids aliasing on edges)
    dist *= (1-2*minSmoothness);

    float value;
    if (dist>0) {
      value=1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist);
    } else {
      value=1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);
    }

    if (mat_symetric) {
      vec2 uv = (vec3(1-p.x,p.y,1) * mat_linePatternsMatrix).xy;
      float dist=fract(uv.x*mat_repeat + halfFinalWidth) - halfFinalWidth;

      // Be sure we can fill the screen with lines (when mat_width==1) even with this min smoothness (that avoids aliasing on edges)
      dist *= (1-2*minSmoothness);

      if (dist>0) {
        value += 1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist);
      } else {
        value += 1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);
      }
    }

    float lightValue;
    if (mat_autolightactive) {
      // Light
      #if defined(mat_autolightshape_IS_Smooth)
        lightValue = 1 - mat_autolightsize * (1+sin((mat_light_position+normalizedCellId*mat_autolightoffset)*2*M_PI))/2;
      #elif defined(mat_autolightshape_IS_In)
        lightValue = 1 - mat_autolightsize * (mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      #elif defined(mat_autolightshape_IS_Linear)
        lightValue = 1 - mat_autolightsize * abs(mod((mat_light_position+normalizedCellId*mat_autolightoffset)*2+1,2)-1);
      #elif defined(mat_autolightshape_IS_Cut)
        lightValue = 1 - mat_autolightsize * step(0.5,mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      #elif defined(mat_autolightshape_IS_Noise)
        lightValue = 1 - mat_autolightsize * vnoise(vec2((mat_light_position+normalizedCellId*mat_autolightoffset*10),2));
      #endif
    } else {
      lightValue = 1;
    }

    vec4 col = mix(mat_back_color, mat_front_color, value);
    col.rgb *=  lightValue;

    // Apply contrast
    col.rgb = mix(vec3(0.5), col.rgb, mat_contrast);

    // Apply brightness
    col.rgb += vec3(mat_brightness);

    return col;
}
