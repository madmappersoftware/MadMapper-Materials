/*{
    "CREDIT": "1024 architecture",
    "TAGS": "graphic",
    "INPUTS": [ 
        { "LABEL": "speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "center_X", "NAME": "centerX", "TYPE": "float", "MIN": -1.0, "MAX": 0.0, "DEFAULT": 0.0 },
		{ "LABEL": "center_Y", "NAME": "centerY", "TYPE": "float", "MIN": 0.1, "MAX": 0.5, "DEFAULT": 0.2 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -0.5 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.1, "MAX": 7.0, "DEFAULT": 2 },
      ],
	 "GENERATORS": [
         {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv  = texCoord + vec2(centerX,centerY);
	float a = atan(uv.y,uv.x);
	float l = length(uv);
	vec2 uv_2 = sin(uv + noise(uv));
	float p  = length(uv_2);
	l = (sin(l) / tan(a));
	float f = a*cos(l+animation_time);
	float m = tan( cos( sin(animation_time)+tan(uv.x*uv.y)));
	float luma =  m * f * l * p;
	
    luma = pow(luma + brightness, contrast);

	
	return vec4(luma,luma,luma,1);
}
