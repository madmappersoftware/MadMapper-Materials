/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Shows how to use Last Frame within a material\nRequires MM v5.1 minimum",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 1.0, "MAX": 10.0, "DEFAULT": 5.0 }, 
{"LABEL": "Width", "NAME": "mat_w", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 0.1 }, 

{"LABEL": "Feedback/Amount", "NAME": "mat_f_amount", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.76 },
{"LABEL": "Feedback/Zoom", "NAME": "mat_f_zoom", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -0.3 }, 
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],

"RASTERISATION_SETTINGS": 
{ "DEFAULT_RENDER_TO_TEXTURE": true,
 "DEFAULT_WIDTH": 512,
 "DEFAULT_HEIGHT": 512,
 "DEFAULT_PIXEL_FORMAT": "PF_U8_BGRA",
 "REQUIRES_LAST_FRAME": true
},
}*/

#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2. -1.;
	uv *= mat_scale;

	float n_1 = vnoise(vec2(mat_time,2.345));
	float n_2 = vnoise(vec2(mat_time,7.005));
	vec2 n = vec2(n_1,n_2);

	uv += n;

	float w = mat_w;
	float k = 1. - step(w,fract(uv.x));
	float m = 1. - step(w,fract(uv.y));

	float val = k + m;
	
	vec3 col = vec3(val);
	vec3 prev = IMG_NORM_PIXEL(mm_LastFrame,texCoord*(1. +mat_f_zoom)).rgb;
	col = mix(col,prev*vec3(1.,0.,0),vec3(mat_f_amount));
	
	return vec4(col,1.);
}