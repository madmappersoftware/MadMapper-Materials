/*{
    "CREDIT": "1024 architecture\nadpated from KodeLife",
    "TAGS": "graphic",
    "INPUTS": [ 
        { "LABEL": "speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "center_X", "NAME": "centerX", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "center_Y", "NAME": "centerY", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "offset_X", "NAME": "offsetX", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "offset_Y", "NAME": "offsetY", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1.0 },
      ],
	 "GENERATORS": [
         {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	float time = animation_time;
	vec2 uv = abs(-1 + 2.0*texCoord + vec2(centerX,centerY)) + vec2(offsetX,offsetY);
	float l = length(uv);
	float a = atan(uv.y,uv.x);
	float col = 0.0;
	col += 1.5 * sin(time + 13.0 * a + uv.y *20.0);
	col += cos(0.9 * uv.x * a * 60.0 + l * 5.0 - time * 2.0);
	col *= 1.2 - l;
	return vec4(vec3(col),1.0);
}
