/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Distortion of a square, with mulitple iterations",
    "TAGS": "noise,square,curious",
    "VSN": "1.1",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.5, "MAX": 2.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Distortion", "NAME": "mat_noise_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },		
		{ "LABEL": "Iterations", "NAME": "iterations", "TYPE": "int", "MIN": 1.0, "MAX": 12.0, "DEFAULT": 1 },			
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "Offset/Top Offset", "NAME": "Offset_top", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Offset/Bottom Offset", "NAME": "Offset_bottom", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Offset/Left Offset", "NAME": "Offset_left", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Offset/Right Offset", "NAME": "Offset_right", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}}, ],		
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec2 uv;	

vec3 HardRectangle(vec2 position,float width, float height){
	
float left = step(position.x+Offset_left,uv.x);
float bottom = step(1-height-position.y+Offset_bottom,1-uv.y);
float right = step(1-width-position.x+Offset_right,1-uv.x);
float top = step(position.y+Offset_top,uv.y);
	
return vec3(left*bottom*right*top);

}


float drawLine (vec2 p1, vec2 p2, vec2 uv, float a)
{
    float r = 0.;
    float one_px = 1. / 100; //not really one px
    
    // get dist between points
    float d = distance(p1, p2);
    
    // get dist between current pixel and p1
    float duv = distance(p1, uv);

    //if point is on line, according to dist, it should match current uv 
    r = 1.-floor(1.-(a*one_px)+distance (mix(p1, p2, clamp(duv/d, 0., 1.)),  uv));
        
    return r;
}
	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	
	// use MadNoise libray
	float K = noise(vec3(uv,mat_animation_time));
	K *= (mat_noise_amp/3.5);

	float positionX=0;	
	float positionY=0;
	float width = mat_scale;	
	float height = mat_scale;	
	
	
	vec3 color = HardRectangle(vec2(positionX+(K*1),positionY+(K*1)),width-(K*2),height-(K*2));
	color = mix(color, vec3(1.0)-color,float(1));
 
	for(float i = 0; i <iterations+2; ++i)
    {
	 color += HardRectangle(vec2(positionX+(i/10)+(K*1),positionY+(i/10)+(K*1)),width-(i/5)-(K*2),height-(i/5)-(K*2));
		color = mix(color, vec3(1.0)-color,float(1));	
	}
	
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
