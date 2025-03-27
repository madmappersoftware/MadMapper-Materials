/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Frank Stella - Ifafa II / 1967",
    "TAGS": "painting",
    "VSN": "1.0",
    "INPUTS": [  
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 7},
		{ "LABEL": "Global/Triangles count", "NAME": "triangles_count", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 19},
		{ "LABEL": "Global/Rotation", "NAME": "global_rotation", "TYPE": "float", "MIN": -6.2831, "MAX": 6.2831, "DEFAULT": 0},
		{ "LABEL": "Global/Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 5, "DEFAULT": 0},
		{ "LABEL": "Global/Global offset ", "NAME": "global_offset", "TYPE": "point2D", "MAX": [ 1, 1 ], "MIN": [ -1, -1 ], "DEFAULT": [ 0, 0 ]},
		{ "LABEL": "Global/Noise amplitude", "NAME": "noise_amplitude", "TYPE": "float", "MIN": 0, "MAX": 5, "DEFAULT": 0},
		{ "LABEL": "Global/Negative", "NAME": "is_negative", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button"},
        { "LABEL": "Global/Background", "NAME": "background_color", "TYPE": "color", "DEFAULT": [ 0.95, 0.95, 0.87, 1.0 ] },

		{ "LABEL": "Global/Emboitement", "NAME": "emboitement", "TYPE": "float", "MIN": 0.05, "MAX": 1, "DEFAULT": 0.05},

		{ "LABEL": "Right/Right offset ", "NAME": "right_offset", "TYPE": "point2D", "MAX": [ 5,5 ], "MIN": [ -5, -5 ], "DEFAULT": [ -0.9, -1.9 ]},
		{ "LABEL": "Right/Right rotation", "NAME": "right_rotation", "TYPE": "float", "MIN": -6.2831, "MAX": 6.2831, "DEFAULT": 0},
		{ "LABEL": "Right/Right color", "NAME": "right_color", "TYPE": "color", "DEFAULT": [ 0, 0, 0, 1.0 ] },  

		{ "LABEL": "Left/Left offset ", "NAME": "left_offset", "TYPE": "point2D", "MAX": [ 5.0, 5.0 ], "MIN": [ -5.0, -5.0 ], "DEFAULT": [ 0.75, -1.8 ]},
		{ "LABEL": "Left/Left rotation", "NAME": "left_rotation", "TYPE": "float", "MIN": -6.2831, "MAX": 6.2831, "DEFAULT": -1.05},
        { "LABEL": "Left/Left color", "NAME": "left_color", "TYPE": "color", "DEFAULT": [ 0.45, 0, 0, 1.0 ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}},
    ] 
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	float t = mat_animation_time;
	vec2 center = vec2(0.5);
	vec4 color = background_color;
	float negative = 0.0;

	vec2 uv = (texCoord - center + global_offset) * scale ;
	uv = rotate(uv, PI + global_rotation);
	float noise = vnoise(uv + t) * noise_amplitude;
	
	// Make triangles	
	for (int i = triangles_count; i > 0; i--) {	
		// Make triangles distance field
		vec2 offsetRight = vec2(0 + right_offset.x , emboitement * i + right_offset.y);

		float triangleRightDist = triangle(rotate(uv,right_rotation) + noise, 
										   offsetRight,  0.098 * i + 0.5);
		float triangleLeftDist = triangle(rotate(uv, left_rotation) - noise, vec2(0, emboitement * i) + left_offset,  
										  0.098 * i + 0.5);

		// Make triangles color
		vec4 triangle1Color = mix(right_color + noise, background_color, i % 2);
		vec4 triangle2Color = mix(left_color + noise, background_color, i % 2);
		
		// Return color 
		color = fill(color, triangle1Color, triangleRightDist);
		color = fill(color, triangle2Color, triangleLeftDist);
	}
	
	// Check for negative
	vec4 finalColor = is_negative ? vec4(1.0 - color.rgb, 1.0) : color;	

	// Return final color
	return finalColor;
}
