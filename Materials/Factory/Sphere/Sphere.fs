/*{
    "CREDIT": "MadTeam",
    "DESCRIPTION": "Sphere shading and repetitions",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
	{ "LABEL": "Repetitions", "NAME": "mat_repeat", "TYPE": "int", "MIN": 1, "MAX": 8, "DEFAULT": 1 }, 
    { "LABEL": "Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.1, "MAX": 1, "DEFAULT": 0.45 },
	{ "LABEL": "Randomize", "NAME": "mat_randomize", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 1. },
	{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ -0.5, -1. ] },

	{ "LABEL": "Noise/Amount", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 0.3 },
	{ "LABEL": "Noise/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1 },
	{ "LABEL": "Noise/Scale", "NAME": "mat_nscale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

	{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button" }, 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" },  
    { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
      ],
	 "GENERATORS": [
    {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": false, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = fract(texCoord*mat_repeat);
	vec2 seed = floor(texCoord*mat_repeat+1.);
	vec2 cell = dFlowNoise(seed*0.345,1.234).yz;
	uv = (uv*2.-1.)/mat_radius;
	
	cell = mix(vec2(1.),cell,mat_randomize*(clamp(mat_repeat-1,0,1)));

	float depth = 2.0 * sqrt(abs(1.-(dot(uv, uv))));	
	float n = flowNoise(uv*cos(depth)*mat_nscale+cell,mat_animation_time)*mat_noise;
	float G = (dot(mat_offset*vec2(1.,-1.)*cell, uv) + depth  + n) / (1.0 + length(mat_offset*cell))*0.5+0.5;

    // Apply brightness
    G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0.,1.);

	if (mat_invert) G = 1 - G;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, G );
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
