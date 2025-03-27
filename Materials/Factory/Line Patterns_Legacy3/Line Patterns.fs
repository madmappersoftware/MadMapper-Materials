/*{
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Mad Team",
  "DESCRIPTION": "Single line pattern with many controls.",
  "VSN": "1.0",
  "TAGS": "line,graphic",
  "INPUTS": [
    { "LABEL": "Global/Line Width", "NAME": "width", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Translation", "NAME": "base_transition", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Rotation", "NAME": "base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },
    { "LABEL": "Global/Smooth", "NAME": "smoothness", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Repeat", "NAME": "repeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },
    { "LABEL": "Global/BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Move/Auto Move", "NAME": "automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "Move/Size", "NAME": "automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Move/Speed", "NAME": "automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Move/Reverse", "NAME": "automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Move/Strob", "NAME": "automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Move/Shape", "NAME": "automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Width/Auto Width", "NAME": "autowidthactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Width/Size", "NAME": "autowidthsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Width/Speed", "NAME": "autowidthspeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Width/Reverse", "NAME": "autowidthreverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Width/Strob", "NAME": "autowidthstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Width/Shape", "NAME": "autowidthshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Rotate/Auto Rotate", "NAME": "autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Rotate/Speed", "NAME": "autorotatespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Rotate/Reverse", "NAME": "autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Rotate/Strob", "NAME": "autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Auto Scale", "NAME": "autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Scale/Size", "NAME": "autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Speed", "NAME": "autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Scale/Reverse", "NAME": "autoscalereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Scale/Strob", "NAME": "autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Shape", "NAME": "autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
    { "LABEL": "Color/Front Color", "NAME": "front_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    { "LABEL": "Color/Back Color", "NAME": "back_color", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
    { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
  ],
  "GENERATORS": [
    {
      "NAME": "move_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "automovespeed","reverse": "automovereverse", "strob": "automovestrob", "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "width_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "autowidthspeed","reverse": "autowidthreverse","strob": "autowidthstrob", "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "rot_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "autorotatespeed","reverse": "autorotatereverse","strob": "autorotatestrob", "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "scale_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "autoscalespeed","strob": "autoscalestrob", "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
    }
  ]
}*/

in mat3 linePatternsMatrix;
in float widthValue;

vec4 materialColorForPixel(vec2 texCoord)
{
    // Set a minimum smoothness so we avoid aliasing when rotated
    float minSmoothness = 0.001 * repeat;
    float finalSmoothness = minSmoothness + smoothness/2;
    float halfFinalWidth = width * widthValue / 2;

    vec2 uv = (vec3(texCoord,1) * linePatternsMatrix).xy;

    float dist=fract(uv.x*repeat + halfFinalWidth) - halfFinalWidth;

    // Be sure we can fill the screen with lines (when width==1) even with this min smoothness (that avoids aliasing on edges)
    dist *= (1-2*minSmoothness);

    float value;
    if (dist>0)
      value=1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist);
    else
      value=1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);

    vec3 col =mix(back_color.rgb,front_color.rgb,value);

    // Apply contrast
    col.rgb = mix(vec3(0.5), col.rgb, contrast);

    // Apply brightness
    col.rgb += vec3(brightness);

    return vec4(col,1);
}
