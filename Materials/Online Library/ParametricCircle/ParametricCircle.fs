/*{
    "CREDIT": "for Tij",
    "DESCRIPTION": "moving circle",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.01, "MAX": 4.0, "DEFAULT": 4.0 }, 
	    { "LABEL": "Off_scale", "NAME": "mat_off_scale", "TYPE": "float", "MIN": 0.01, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{ "LABEL": "Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.001, "MAX": 0.99, "DEFAULT": 0.2 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },

      ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord*2.-1.;
	uv *= mat_scale;
//	uv += vec2(-mat_offset.x,mat_offset.y)*vec2(mat_scale)*vec2(mat_off_scale);

	
	// make a color out of it
	vec3 color = vec3( 0.);
	
	float radius = mat_radius;
	float dist = 1.0 - circle( uv+mat_offset*mat_off_scale, radius );
	
	float A = 1. - smoothstep(1. - radius,0.,dist);

	color += vec3(A);

		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
