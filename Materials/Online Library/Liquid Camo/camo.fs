/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Liquid Camoflauge",
    "TAGS": "liquid,camo",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": .5 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 2, "MAX": 5, "DEFAULT": 3.0 },         
		{ "LABEL": "Distortion", "NAME": "mat_noise_amp", "TYPE": "float", "MIN": 0.0, "MAX": 100.0, "DEFAULT": 15.0 },		
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },	
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
	
	uv *= (mat_scale);
	
	// use MadNoise libray
	float K = noise(vec3(uv,mat_animation_time));
	K *= (mat_noise_amp/3.5)+1;

	float positionX=K;	
	float positionY=K;
	float width = mat_scale;	
	float height = mat_scale;	
		
	vec3 color = HardRectangle(vec2(0.25+K+K,0),width-.5-K-K-K,height);
	

if (color.rgb == vec3(1)) {

	color = vec3(0.37,.26,.22);	
}
	color -= HardRectangle(vec2(.4+K,0),width-1.8,height);
	
if (color.rgb == vec3(0)) {
		color = vec3(.25,.32,.23);	
}	

if (color.r <= 0)  {
if (color.g <= 0)  {
if (color.b <= 0)  {

	color = vec3(.61,.60,.45);
}
}
}
	color -= HardRectangle(vec2(1.6+K,0),width-2,height);
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));	
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
