/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Put your name here\nEnjoy shading !",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Floats/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Floats/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1,  "DEFAULT": 2.0 }, 
        { "LABEL": "Floats/Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Floats/Ranged", "NAME": "mat_ranged", "TYPE": "floatRange", "DEFAULT": [0.0,1.0], "MIN": 0.0, "MAX": 1.0},

		{ "LABEL": "Int/Iterations", "NAME": "mat_iter", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 5 }, 

		{ "LABEL": "2d/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Buttons/Checkbox", "NAME": "mat_check", "TYPE": "bool", "DEFAULT": 0, },
		{ "LABEL": "Buttons/Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "Buttons/Event (true for 1 frame)", "NAME": "mat_event", "TYPE": "event",  },
		{ "LABEL": "Buttons/Grid", "NAME": "mat_block","TYPE": "long", "DEFAULT": "White", "VALUES": [ "White","Red" ,"Blue","4","5","6"],"FLAGS":"button_grid" },
		

		{ "LABEL": "Counter/Increment", "NAME": "mat_plus_one", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button,trigger" },
		{ "LABEL": "Counter/Decrement", "NAME": "mat_minus_one", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button,trigger" },

		{ "LABEL": "Dropdown", "NAME": "mat_shape","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Circle" ] },
      { "LABEL": "Curve", "NAME": "mat_curve", "TYPE": "curve", "DEFAULT": [0.0,0.0,0.2,0.,0.3,0.9,0.4,0.0,], "MIN": [0,0], "MAX": [1,1], "INTERPOLATION": "catmull_rom", "DISPLAY": "linear" },
		
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
      { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },

      ],

	 "GENERATORS": [
    {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    {"NAME": "mat_incrementer","TYPE": "incrementer","PARAMS": {"increment": "mat_plus_one", "decrement": "mat_minus_one"}
}
    ],
	  "IMPORTED": [
        {"NAME": "mat_texture", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ],
	"RASTERISATION_SETTINGS": {
    	"DEFAULT_RENDER_TO_TEXTURE": true,
    	"DEFAULT_WIDTH": 1024,
    	"DEFAULT_HEIGHT": 1024,
    	"DEFAULT_PIXEL_FORMAT": "PF_U8_RGBA",
    	"REQUIRES_LAST_FRAME": true
},
	"RENDER_SETTINGS" : {
		"CLAMP_MATERIAL_OUTPUT" : true,
},
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
	vec3 color = vec3(  K+K)* step(0.5, texCoord.x);
	
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

	// Button Grid
	float block = step(0.9,uv.x)-step(1.,uv.x);
	vec3 b_col = vec3(0.);

	if(mat_block==0) b_col = vec3(1.);
	if(mat_block==1) b_col = vec3(1.,0.,0.);
	if(mat_block==2) b_col = vec3(0.,1.,1.);
	if(mat_block==3) b_col = vec3(1.);
	if(mat_block==4) b_col = vec3(1.);
	if(mat_block==5) b_col = vec3(1.);

	color += block*b_col ;

	// Event
	if(mat_event)color += texture(mat_texture,texCoord.xy).rgb*4.;

	// Incrementer
	float I = mat_incrementer*0.1;
	float Iline = step(0+I,1.-uv.y)- step(0.02+I,1.-uv.y);

	color += vec3(Iline);
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));

	// Get previous frame for feedback
	vec3 lastFrame = texture(mm_LastFrame,texCoord.xy*0.99).rgb;
	color += lastFrame *0.9 * (1.-step(0.5, texCoord.x));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
