/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple Isoline on a billowed noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Amount", "NAME": "mat_amount", "TYPE": "float", "MIN": 1.0, "MAX": 10.0, "DEFAULT": 4.0 }, 
		{"LABEL": "Width", "NAME": "mat_width", "TYPE": "float", "MIN": 1.0, "MAX": 20.0, "DEFAULT": 1. },

    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],

}*/
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord.xy *2.-1.;
	float v1 = billowedNoise(vec3(uv *mat_scale,mat_time));

	v1 = fract(v1*mat_amount);
	v1 = smoothstep(1.,0., (0.5/mat_width)*abs(v1)/fwidth(v1));

	vec3 col = vec3(v1,v1,v1);

	return vec4(col,1.);
}