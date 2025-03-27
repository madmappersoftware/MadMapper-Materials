/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Neon Flickering Effect",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "NAME": "MODE", "Label": "Type", "TYPE": "long", "DEFAULT": "Quiet", "FLAGS": "generate_as_define", "VALUES": [ "Quiet", "Nervous",  ] },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{ "LABEL": "Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 16, "DEFAULT": 1 },
        { "LABEL": "Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 16, "DEFAULT": 1 }, 
        { "LABEL": "Threshold", "NAME": "mat_thresh", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.65 },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 1, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

   	vec2 cellId;
    vec2 p          = texCoord;
    vec2 cellSize   = vec2( 1.0 / cells_x, 1.0 / cells_y );
    p               = repeat( p, cellSize, cellId );

	float N = 0.;
	// use MadNoise libray

#if defined( MODE_IS_Quiet )
     N = billowedNoise(vec3(cellId,mat_animation_time));;
#elif defined( MODE_IS_Nervous )
     N = ridgedNoise(vec3(cellId,mat_animation_time));
#endif

	N = step(mat_thresh,N);

	// make a color out of it
	vec3 color = vec3( N);
	

	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
