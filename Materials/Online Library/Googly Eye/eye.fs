/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joe Griffith",
	"DESCRIPTION": "Googly Eyes - add to a pair of Circles, and enjoy!",
    "TAGS": "Comical",
	"VSN": "1.0",
    "INPUTS": [ 
	
		{ "LABEL": "Look", "NAME": "lookUpDown", "TYPE": "point2D", "MAX": [ 0.45, 0.45 ], "MIN": [ -0.45, -0.45 ], "DEFAULT": [ 0.0, 0.0 ] },		       
	
	]		  	
			
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

//////  adapted from 1024 architecture - Hypnotic
//////  adapted from ShaderToy 4s3SRf	
	
vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec3 color = vec3(0.0);    
	vec2 uv = texCoord;
    uv = 2.0 * uv - 1.; 

    vec2 pos = lookUpDown*vec2(1,-1);     
	
		float dist = length(  uv) - (0.49 * 2);
    	float o = 1. - smoothstep(0.0,5.0/(1010.0), dist);
        color = mix(color , vec3(1.)* (1. -mod(2,2.)),o);

		float dist2 = length( pos - uv) - (0.49 * 1);
    	float o2 = 1. - smoothstep(0.0,5.0/(1010.0), dist2);
        color = mix(color , vec3(1.)* (1. -mod(1,2.)),o2);
	
	return vec4(color,1);  
}
