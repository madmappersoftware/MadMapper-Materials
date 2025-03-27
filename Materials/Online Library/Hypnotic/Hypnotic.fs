/*{
    "CREDIT": "1024 architecture",
    "TAGS": "graphic",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0, "MAX" : 10.0, "DEFAULT": 2.0 },
		{ "LABEL": "Offset_X", "NAME": "offsetx", "TYPE": "float", "MIN" : 0, "MAX" : 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Offset_Y", "NAME": "offsety", "TYPE": "float", "MIN" : 0, "MAX" : 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Blur", "NAME": "blur", "TYPE": "float", "MIN": 1.0, "MAX": 1000.0, "DEFAULT": 100.0 },
		{ "LABEL": "Iterations", "NAME": "iterations", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 20 },
		{ "LABEL": "Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	  	   "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

//////  adapted from ShaderToy 4s3SRf	
	
vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec3 color = vec3(1.0);
    
	vec2 uv = texCoord;
    uv = 2.0 * uv - 1.;
	uv *= iterations*0.055;
 
    vec2 pos = vec2(.0);
	
	float ofx = offsetx *0.5;
	float ofy = offsety *0.5;
    
    for(float i = iterations; i > 0.; --i)
    {
        pos = vec2(sin(animation_time+(iterations - i)*0.4)*ofx, cos(animation_time+(iterations - i)*0.4)*ofy);
    	float dist = length( pos - uv) - (0.05 * i);
    	float o = 1. - smoothstep(0.0,5.0/(1010.0-blur), dist);
        color = mix(color , vec3(1.)* (1. -mod(i,2.)),o);
    }
	
	if(!invert){color = vec3(1.)-color;}
	return vec4(color,1.0);
}
