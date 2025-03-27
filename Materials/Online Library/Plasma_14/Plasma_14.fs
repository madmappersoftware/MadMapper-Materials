/*{
    "CREDIT": "original by Viktor Korsun",
    "DESCRIPTION": "yet another plasma",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.5 }, 

		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
		{ "LABEL": "Color/Desaturate", "NAME": "mat_desaturate", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Color/Rotation", "NAME": "mat_crot", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
    		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    		{ "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
    
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	

mat3 rotation(float angle, vec3 axis)
{
	float s = sin(-angle);
	float c = cos(-angle);
	float oc = 0.15 - c;
	vec3 sa = axis * s;
	vec3 oca = axis * oc;
	return mat3(	
		oca.x * axis + vec3(	c,	-sa.z,	sa.y),
		oca.y * axis + vec3( sa.z,	c,		-sa.x),		
		oca.z * axis + vec3(-sa.y,	sa.x,	c));	
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float t = mat_animation_time;
	// get texture coordinates
	vec2 uv = texCoord*2.-1.;
	// modify uv with material inputs
	uv *= mat_scale;
	
	vec2 p = uv;
	
	// main code, *original shader by: 'Plasma' by Viktor Korsun (2011)
	float x = p.x;
	float y = p.y;
	float mov0 = x+y+cos(sin(t)*2.0)*100.+sin(x/100.)*1000.;
	float mov1 = y / 0.9 +  t;
	float mov2 = x / 0.2;
	float c1 = abs(sin(mov1+t)/2.+mov2/2.-mov1-mov2+t);
	float c2 = abs(sin(c1+sin(mov0/1000.+t)+sin(y/40.+t)+sin((x+y)/100.)*3.));
	float c3 = abs(sin(c2+cos(mov1+mov2+c2)+cos(mov2)+sin(x/1000.)));

	float L = luma(vec3(c1,c2,c3));

	// make a color out of it
	vec3 color = mix(vec3(c1,c2,c3),vec3(L),vec3(mat_desaturate));
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);

	// Apply Invert
	color = mix(color,color * rotation(mat_crot*PI,vec3(1.)), vec3(mat_crot));
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}