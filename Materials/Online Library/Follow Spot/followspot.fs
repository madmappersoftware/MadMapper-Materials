/*{
    "CREDIT": "Joe Griffith",
	"DESCRIPTION": "Digital Follow Spot",
    "TAGS": "Theatrical",
	"VSN": "1.0",
    "INPUTS": [ 
		
		{ "LABEL": "Pan - Tilt", "NAME": "panTilt", "TYPE": "point2D", "MAX": [ 0.69, 0.69 ], "MIN": [ -0.69, -0.69 ], "DEFAULT": [ 0.0, 0.0 ] },		
        { "LABEL": "Diffusion", "NAME": "diffusion", "TYPE": "float", "MIN": 0.0, "MAX": 1000.0, "DEFAULT": 100.0 },
		{ "LABEL": "Iris", "NAME": "iris", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.22 },
		{ "LABEL": "Douser", "NAME": "douser", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Color", "NAME": "gelColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
	  		]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

//////  adapted from 1024 architecture - Hypnotic
//////  adapted from ShaderToy 4s3SRf	
	
vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec3 color = vec3(1.0);    
	vec2 uv = texCoord;
    uv = 2.0 * uv - 1.; //calibrate telrad
	
    vec2 pos = panTilt*vec2(1,-1);      //assign position and invert controls to match ui
	float dist = length( pos - uv) - (iris);
    float o = 1. - smoothstep(0.0,5.0/(1111.0-diffusion), dist);    //add diffusion for soft edge
    color = mix(color , vec3(1.)* (1. -mod(1,2.)),o);
	color = vec3(gelColor.x,gelColor.y,gelColor.z)-color;   //set gel color
	
	if (iris == 0){color = vec3(0,0,0);}   //insure complete blackout when iris is in
	
	return vec4(color,douser);   //dimming
}
