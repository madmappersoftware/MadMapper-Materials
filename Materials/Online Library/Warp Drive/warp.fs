/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "stars, tunnel",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
   		{"LABEL": "Warp", "NAME": "warp", "TYPE": "float", "MIN": .02, "MAX": 1.0, "DEFAULT": .25 },  			  
		{"LABEL": "speed", "NAME": "speed", "TYPE": "float", "MIN": .1, "MAX": 1.0, "DEFAULT": 1 },  
		{"LABEL": "Pos/X pos", "NAME": "xpos", "TYPE": "float", "MIN": 0, "MAX": 2.0, "DEFAULT": 1 }, 
		{"LABEL": "Pos/Y pos", "NAME": "ypos", "TYPE": "float", "MIN": 0, "MAX": 2.0, "DEFAULT": 1 }, 

      ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec2 position = vec2( texCoord.x * 2 -  xpos,texCoord.y * 2 -  ypos);
	float angle = atan(position.y, position.x) / (3.14159265359);
	float rad = length(position);
	vec3 color = vec3(1,1,1.0);
	float angleFract = fract(angle*333.);
	float angleRnd = floor(angle*333.);
	float angleRnd1 = fract(angleRnd*fract(angleRnd*.55)*45.1);
	float angleRnd2 = fract(angleRnd*fract(angleRnd*.5)*13.724);
	float t2 = TIME + angleRnd1*111.0;
	float radDist = sqrt(angleRnd2);
	float adist = radDist / rad * warp;
	float dist = (t2*(speed/4)+adist);
	dist = abs(fract(dist) - .001);
	color +=  (2.2 / dist) * cos(sin(TIME)) * adist / radDist / 50;  
	float alpha =  (2.2 / dist) * cos(sin(TIME)) * adist / radDist / 50;  
	
	return vec4(color.r,color.g,color.b,alpha);
}
