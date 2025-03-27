/*{
    "CREDIT": "franz\n1024 architecture",
    "DESCRIPTION": "Automatic Plasma colors",
    "TAGS": "LED",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
{"LABEL": "A", "NAME": "mat_A", "TYPE": "color","DEFAULT": [ 0.84, 0.53, 0.18, 1.0 ], "FLAGS": "no_alpha"  }, 
{"LABEL": "B", "NAME": "mat_B", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.91, 1.0 ], "FLAGS": "no_alpha" }, 
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = texCoord *2.-1.;
	uv *= mat_scale;
	float t = mat_time;
	float S = billowedNoise(vec3(uv*vec2(0.15,1.),t))*2.-1.;

	vec3 C = cross((vec3(S,-S,S)),normalize(vec3(sin(t*1.1),cos(t*0.9),-sin(t))));
	float c = (C.r+C.g+C.b)*0.33;

	vec3 col = mix(mat_A.rgb,mat_B.rgb,C.r);

	return vec4(col,1.);
}