/*{
    "CREDIT": "frz / testing audio input responsivess",
    "DESCRIPTION": "graphing a simple test line\nto display an audio FFT\nand check its responsiveness",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "NAME": "waveformFFT","TYPE":"audioFFT","SIZE":4,"RELEASE": 0.1, "ATTACK": 0.0},
      ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

	float A = texture(waveformFFT,uv).r;
	A *= 0.5;
	float width = 0.02;
	float Y1 = 1. - step(0.5,uv.y+A);
	float Y2 = 1.-step(0.5 + width,uv.y+A);
	
	
	// make a color out of it
	vec3 color = vec3(Y2 - Y1);
	

		
	vec4 final_color = vec4(color*vec3(1,0,0),1.0);	
	return final_color;
}
