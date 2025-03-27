/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "simple hue slider color",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Hue", "NAME": "mat_hue", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
{"LABEL": "Min", "NAME": "mat_min", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
{"LABEL": "Max", "NAME": "mat_max", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.6 },
    ],
}*/

#include "MadCommon.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	float range = mat_max - mat_min;
	vec3 col = hsv2rgb(vec3(mat_hue*range + mat_min,1,1));
	return vec4(col,1.);
}