/*{
    "CREDIT": "Put your name here\nEnjoy shading !",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		
		{ "Label": "Extra Shape", "NAME": "mat_shape","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Circle" ] },

        {"LABEL": "White Border", "NAME": "mat_curve", "TYPE": "curve", "DEFAULT": [0.0,0.0,0.33,0.2,0.66,0.8,0.75,0.4,1.0,0.0], "MIN": [0,0], "MAX": [1,1], "INTERPOLATION": "catmull_rom", "DISPLAY": "linear" },
		
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },


      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);



	
	// use MadNoise libray
	float N = billowedNoise(vec3(uv,mat_time));
	float M = worleyNoise(vec3(uv,mat_time));
	float K = mix(N,M,mat_mix_noise);
	
	// make a color out of it
	vec3 color = vec3( abs(sin(uv+ mat_time))*(1.0-K), K);
	
	// You can also import static image textures,
	// if the file exists in your material folder and an "IMPORTED" section is declared.
	// Here we use an hypothetic imported texture named "mat_some_texture"
	// whose file is "flare.jpg" located in your material folder on your drive
	// at "~/Documents/MadMapper/Materials/template"	
	// vec3 tex = texture(mat_some_texture,uv).rgb;
	
	// extra shape using MadMapper's Signed Distance Field 2d library
	if (mat_shape==1) {
		float radius = 0.1;
		float dist = 1.0 - circle( (texCoord*2.0)-1.0, radius );
		color += vec3(dist);
	}

	float def = IMG_NORM_PIXEL(mat_curve,vec2(fract(mat_time),0.5)).r;
	float k = 1. - step(def,uv.x);
	color += k;
	
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
