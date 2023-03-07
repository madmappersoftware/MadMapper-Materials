/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Audio Reactive Color Spectrum",
    "TAGS": "audioreactive",
    "VSN": "1.0",
    "INPUTS": [ 

		{ "LABEL": "Width", "NAME": "mat_width", "TYPE": "int", "MIN": 2, "MAX": 512, "DEFAULT": 16 },
		{ "LABEL": "Strength", "NAME": "mat_strength", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 1. },
		{ "LABEL": "Attack", "NAME": "mat_attack", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Decay", "NAME": "mat_decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Release", "NAME": "mat_release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
        {
            "NAME": "spectrum",
            "TYPE": "audioFFT",
            "SIZE": "mat_width",
            "ATTACK": "mat_attack",
            "DECAY": "mat_decay",
            "RELEASE": "mat_release"
        },
		{ "LABEL": "Color/Saturation", "NAME": "mat_saturation", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Width", "NAME": "mat_color_width", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Offset", "NAME": "mat_color_offset", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs

	float audio = texture(spectrum,vec2(uv.x,0.5)).r;
	// make a color out of it
	
	vec3 color = hsv2rgb(vec3(
		fract(uv.x-0.5)*mat_color_width+mat_color_offset,
		mat_saturation,
		audio*10.*mat_strength));


	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
