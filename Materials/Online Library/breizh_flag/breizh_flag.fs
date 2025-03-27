/*{
    "CREDIT": "frz / test mat",
    "DESCRIPTION": "moving bzh flag",
    "TAGS": "test",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "SpeedX", "NAME": "mat_speedX", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "SpeedY", "NAME": "mat_speedY", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 }, 

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_timeX", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedX", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    		{"NAME": "mat_animation_timeY", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedY", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
	],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "bzh.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

	uv.x = uv.x - mat_animation_timeX;
	uv.y = uv.y + mat_animation_timeY;

	uv *= mat_scale;
	
	
	// You can also import static image textures,
	// if the file exists in your material folder and an "IMPORTED" section is declared.
	// Here we use an hypothetic imported texture named "mat_some_texture"
	// whose file is "bzh.jpg" located in your material folder on your drive
	// at "~/Documents/MadMapper/Materials/breizh_flag"	
	 vec3 color = texture(mat_some_texture,uv).rgb;
	
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
