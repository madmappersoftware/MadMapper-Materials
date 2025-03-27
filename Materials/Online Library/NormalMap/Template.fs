/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "normals from a height map\non the fly using different techniques",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 40.0, "DEFAULT": 10.0 }, 
		{ "LABEL": "NormalScale", "NAME": "mat_normal_scale", "TYPE": "float", "MIN": 0.1, "MAX": 100.0, "DEFAULT": 10.0 }, 
		{ "LABEL": "ShowNormals", "NAME": "mat_show_normals", "TYPE": "bool",  "DEFAULT": false , 	 "FLAG":"generate_as_define"},
		{ "LABEL": "InvertNormals", "NAME": "mat_invert_normals", "TYPE": "bool",  "DEFAULT": false ,"FLAG":"generate_as_define"},
		{ "LABEL": "HighQuality", "NAME": "mat_HQ", "TYPE": "bool",  "DEFAULT": false ,"FLAG":"generate_as_define"},
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	
	float nscale = mat_normal_scale;
	
	// generate some noise values
	float N = billowedNoise(vec3(uv,mat_animation_time));

if(mat_invert_normals)N = 1.0 - N;	

	// try with partial derivative returned by noise function
	// not good for now
	/*
	vec4 K = dBillowedNoise(vec3(uv,mat_animation_time));	
	vec3 norm = K.xyz;
	norm.z = 1.0;
	norm.x = (norm.x *0.5) + 0.5;
	norm.y = (norm.y *0.5) + 0.5;
	norm = normalize(norm);
	*/
		
	vec3 normal = vec3(1.);
	
if(!mat_HQ){
	
	// using GLSL partial derivative function
	normal.x = dFdx(N)*nscale;
	normal.y = dFdy(N)*nscale;
	normal.x = (normal.x *0.5) + 0.5; // mad
	normal.y = (normal.y *0.5) + 0.5; // mad
	normal = normalize(normal);
}else{
	
	// using Sobel 
	// same result but quality is better since noise is generated 9x times
	// should be faster using texel fetches from a render target
	
	float scale = nscale *4.;
	float texel = 1. / 1024.; // texel size
	float n0 = billowedNoise(vec3(uv + vec2(-texel,-texel),mat_animation_time));
	float n1 = billowedNoise(vec3(uv + vec2(0,-texel),mat_animation_time));
	float n2 = billowedNoise(vec3(uv + vec2(texel,-texel),mat_animation_time));
	float n3 = billowedNoise(vec3(uv + vec2(-texel,0),mat_animation_time));
	float n4 = billowedNoise(vec3(uv ,mat_animation_time));
	float n5 = billowedNoise(vec3(uv + vec2(texel,0),mat_animation_time));
	float n6 = billowedNoise(vec3(uv + vec2(-texel,texel),mat_animation_time));
	float n7 = billowedNoise(vec3(uv + vec2(0,texel),mat_animation_time));
	float n8 = billowedNoise(vec3(uv + vec2(texel,texel),mat_animation_time));
	
	normal.x = scale * -(n2-n0+2*(n5-n3)+n8-n6);
	normal.y = scale * -(n6-n0+2*(n7-n1)+n8-n2);
	normal.x = (normal.x *0.5) + 0.5;
	normal.y = (normal.y *0.5) + 0.5;

	normal = normalize(normal);	
}
			
if(!mat_show_normals)normal = normal.zzz;		
	vec4 color = vec4(normal,1.0);	
	return color;
}
