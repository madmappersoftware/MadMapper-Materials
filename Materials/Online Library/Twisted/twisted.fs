/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Twisted lines",
    "TAGS": "noise,lines",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": .5 }, 
		{ "LABEL": "Thickness", "NAME": "mat_scale", "TYPE": "float", "MIN": 1.825, "MAX": 2.5, "DEFAULT": 2.0 },         
		{ "LABEL": "Distortion", "NAME": "mat_noise_amp", "TYPE": "float", "MIN": 0.0, "MAX": 7.0, "DEFAULT": 5.0 },		
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },		
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},  
   ],
		
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec2 uv;
	

vec3 HardRectangle(vec2 position,float width, float height){
	
float left = step(position.x,uv.x);
float bottom = step(1-height-position.y,1-uv.y);
float right = step(1-width-position.x,1-uv.x);
float top = step(position.y,uv.y);
	
return vec3(left*bottom*right*top);

}
		

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	
	// use MadNoise libray
	float K = billowedNoise(vec3(uv,mat_animation_time));
	K *= (mat_noise_amp/3.5);

	float positionX=K;	
	float positionY=K;
	float width = mat_scale;	
	float height = mat_scale;	
		
	vec3 color = HardRectangle(vec2(.1+K,0),width-1.8,height);
	
	color += HardRectangle(vec2(.4+K,0),width-1.8,height);
	color += HardRectangle(vec2(.7+K,0),width-1.8,height);
	color += HardRectangle(vec2(1-K,0),width-1.8,height);
	color += HardRectangle(vec2(1.3-K,0),width-1.8,height);
	color += HardRectangle(vec2(1.6-K,0),width-1.8,height);

	color -= HardRectangle(vec2(.5+K,0),width-1.8,height);
	color -= HardRectangle(vec2(.8+K,0),width-1.8,height);
	color -= HardRectangle(vec2(1.1-K,0),width-1.8,height);
	color -= HardRectangle(vec2(1.4-K,0),width-1.8,height);
	color -= HardRectangle(vec2(1.7-K,0),width-1.8,height);

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
