/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "A noisy edged square",
    "TAGS": "noise,square",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 1.0, "MAX": 8.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Noise Amp", "NAME": "mat_noise_amp", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },		
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
	float N = billowedNoise(vec3(uv,mat_animation_time));
	float M = worleyNoise(vec3(uv,mat_animation_time));
	float K = mix(N,M,mat_mix_noise);
	K *= mat_noise_amp;

	
	float positionX=0;	
	float positionY=0;
	float width = mat_scale;	
	float height = mat_scale;	
	
	vec3 color = HardRectangle(vec2(positionX+(K*1),positionY+(K*1)),width-(K*2),height-(K*2));
	
	
	
	
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
