/*{
    "CREDIT": "Pierre Guilluy",
    "TAGS": "graphic,psychedelic,trippy",
    "INPUTS": [
        { "LABEL": "Style/Stroke Size", "NAME": "stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },

        { "LABEL": "Shape/Zoom", "NAME": "shape_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.75 },

        { "LABEL": "Anim/Speed", "NAME": "speed_size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Reverse", "NAME": "reverse_size", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		
		{ "LABEL": "Shape/Center", "NAME": "center", "TYPE": "point2D", "MAX": [ 1.5, 1.5 ], "MIN": [ -0.5, -0.5 ], "DEFAULT": [ 0.5, 0.5 ] },
    ],
    "GENERATORS": [
        {"NAME": "animation_time_size", "TYPE": "time_base", "PARAMS": {"speed": "speed_size", "reverse": "reverse_size", "strob": 0, "speed_curve":3, "link_speed_to_global_bpm":true }},
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 vectorToCenter = vec2(center.x - texCoord.x, (1.0 - center.y) - texCoord.y);
	float distToCenter = sqrt(sqrt(pow(vectorToCenter.x, 2) + pow(vectorToCenter.y, 2)));
	float value = step(-(stroke_size*2-1), sin(animation_time_size * 5 + (distToCenter * (500 - shape_size * 500)) + (texCoord.x * 10))); // step will transform the sin to a square signal
	
    vec4 col = vec4(value, value, value, 1);	
    return col;
}
