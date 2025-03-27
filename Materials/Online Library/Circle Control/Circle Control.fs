/*{
  "CREDIT": "Matt Beghin",
  "TAGS": "graphic,circle",
  "DESCRIPTION": "Control a circle with many options: translate, scale, luma can be adjusted and controlled with animations or noise etc.",
  "VSN": "1.0",
  "INPUTS": [
    { "LABEL": "Global/Size", "NAME": "circle_size", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Global/X", "NAME": "circle_x", "TYPE": "float", "DEFAULT": 0.0, "MIN": -1.0, "MAX": 1.0 },
    { "LABEL": "Global/Y", "NAME": "circle_y", "TYPE": "float", "DEFAULT": 0.0, "MIN": -1.0, "MAX": 1.0 },
    { "LABEL": "Global/Smooth", "NAME": "smoothness", "TYPE": "float", "DEFAULT": 0.25, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Repeat X", "NAME": "repeat_x", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },
    { "LABEL": "Global/Repeat Y", "NAME": "repeat_y", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },
    { "LABEL": "Global/BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
    { "LABEL": "MoveX/Auto Move X", "NAME": "automovexactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "MoveX/Size", "NAME": "automovexsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "MoveX/Noise", "NAME": "automovexnoise", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "MoveX/Speed", "NAME": "automovexspeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "MoveX/Reverse", "NAME": "automovexreverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "MoveX/Strob", "NAME": "automovexstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "MoveX/Shape", "NAME": "automovexshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
    { "LABEL": "MoveY/Auto Move Y", "NAME": "automoveyactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "MoveY/Size", "NAME": "automoveysize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "MoveY/Noise", "NAME": "automoveynoise", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "MoveY/Speed", "NAME": "automoveyspeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "MoveY/Reverse", "NAME": "automoveyreverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "MoveY/Strob", "NAME": "automoveystrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "MoveY/Shape", "NAME": "automoveyshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
    { "LABEL": "Luma/Auto Luma", "NAME": "autolumaactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Luma/Size", "NAME": "autolumasize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Luma/Speed", "NAME": "autolumaspeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Luma/Reverse", "NAME": "autolumareverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Luma/Strob", "NAME": "autolumastrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Luma/Shape", "NAME": "autolumashape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
    { "LABEL": "Scale/Auto Scale", "NAME": "autoscaleactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Scale/Size", "NAME": "autoscalesize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Speed", "NAME": "autoscalespeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Scale/Reverse", "NAME": "autoscalereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Scale/Strob", "NAME": "autoscalestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Scale/Shape", "NAME": "autoscaleshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
    { "LABEL": "Color/Front Color", "NAME": "front_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
    { "LABEL": "Color/Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": false },
  ],
  "GENERATORS": [
    {
      "NAME": "movex_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "automovexspeed","reverse": "automovexreverse", "strob": "automovexstrob", "speed_curve":3, "bpm_sync": "bpmsync", "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "movey_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "automoveyspeed","reverse": "automoveyreverse", "strob": "automoveystrob", "speed_curve":3, "bpm_sync": "bpmsync", "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "luma_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "autolumaspeed","reverse": "autolumareverse","strob": "autolumastrob", "speed_curve":3, "bpm_sync": "bpmsync", "link_speed_to_global_bpm":true }
    },
    {
      "NAME": "scale_position",
      "TYPE": "time_base",
      "PARAMS": {"speed": "autoscalespeed","reverse": "autoscalereverse","strob": "autoscalestrob", "speed_curve":3, "bpm_sync": "bpmsync", "link_speed_to_global_bpm":true }
    }
  ],
  "IMPORTED": [
      {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"}
  ]
}*/

in mat3 linePatternsMatrix;
in float lumaValue;

vec4 materialColorForPixel(vec2 texCoord)
{
    // Set a minimum smoothness so we avoid aliasing when rotated
    float finalSmoothness = smoothness;
    float halfFinalWidth = circle_size/2;
    float value = 0;

	for (int x=-1; x<=1; x++) {
		for (int y=-1; y<=1; y++) {
			vec2 uv = (vec3(fract(vec2(repeat_x,repeat_y)*texCoord)+vec2(x,y),1) * linePatternsMatrix).xy;
			float dist=length(uv);
			if (dist>0)
			  value=max(value,1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist));
			else
			  value=max(value,1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist));
		}
	}

    value *= lumaValue;

    vec3 col =value*front_color.rgb;

    // Apply contrast
    col.rgb = mix(vec3(0.5), col.rgb, contrast);

    // Apply brightness
    col.rgb += vec3(brightness);

    if (invert)
      return vec4(1-value*col,1);
    else
      return vec4(value*col,1);
}
