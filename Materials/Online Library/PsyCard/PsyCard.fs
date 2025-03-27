/*{
    "CREDIT": "mad-matt",
    "DESCRIPTION": "Test Card as Material",
    "TAGS": "graphics",
    "VSN": "1.0",
	"CATEGORIES": ["TestCards"],
    "INPUTS": [ 
	    	{ "LABEL": "Count", "NAME": "mat_divisions", "TYPE": "int", "DEFAULT": 64, "MIN": 1, "MAX": 256 },
		{ "LABEL": "Scale/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "Scale/Speed", "NAME": "mat_scale_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
        { "LABEL": "Noise/Noise", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Noise/Noise Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Noise/Noise Scale", "NAME": "mat_noise_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
	    { "LABEL": "Color/Back Color", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0] },
	    { "LABEL": "Color/Front Color", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_scale_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_scale_speed", "speed_curve":2, "bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
        {"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "speed_curve":2, "bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 uv1 = texCoord * vec2(mat_divisions);
    float squareIndex1 = floor(uv1.x)+floor(uv1.y);
	vec2 noiseValue = dBillowedNoise(vec3(texCoord*mat_noise_scale,mat_noise_time)).xy;
    vec2 uv2 = (vec2(0.5) + (texCoord-vec2(0.5)+ mat_noise*0.01*noiseValue) * (1+0.1*mat_scale*sin(mat_scale_time))) * vec2(mat_divisions);
    float squareIndex2 = floor(uv2.x)+floor(uv2.y);
    float valueForSquare1 = mod(1+squareIndex1,2);
    float valueForSquare2 = mod(1+squareIndex2,2);
    return mix(mat_color1,mat_color2,min(valueForSquare1,valueForSquare2));
}
