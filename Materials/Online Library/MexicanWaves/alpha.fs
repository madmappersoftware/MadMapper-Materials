/*{
    "CREDIT": "luiscript",
    "DESCRIPTION": "Sine waves that represents Low, Mid and Hi frequencies",
    "TAGS": "lines, waves, audio, sine, reactive",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Lo", "NAME": "low", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },
		{ "LABEL": "Mid", "NAME": "mid", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },
		{ "LABEL": "Hi", "NAME": "hi", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },
      ],
}

demo: https://www.youtube.com/watch?v=upAHYU8vw6A

*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"
 
vec4 materialColorForPixel( vec2 texCoord ) {
	float offset = 0.5;
	vec3 color = vec3(0.0);
	
	vec2 uLow = texCoord - offset + 0.25;
	uLow.y += sin( 10.5*uLow.x ) * 0.25 * low;
	float fTemp = abs( 0.2 / uLow.y / 100);
	
	vec2 uMid = texCoord - offset;
	uMid.y += sin( 15.5*uMid.x ) * 0.25 * mid;
	float fTemp2 = abs( 0.2 / uMid.y / 100);

	vec2 uHi = texCoord - offset - 0.25;
	uHi.y += sin( 30.5*uHi.x ) * 0.25 * hi;
	float fTemp3 = abs( 0.2 / uHi.y / 100);

	float r = 0.02744509;
	float g = 0.4274;
	float b = 0.2902;
	
	vec3 verde = vec3(r,g,b);
	
	color += vec3( pow(fTemp,  0.9) * verde.x, pow(fTemp, 0.9) * verde.y, 0.0);
	color += vec3( pow(fTemp2, 1.0) * 1., pow(fTemp2, 1.0) * 1., pow(fTemp2, 1.0) * 1.);
	color += vec3( pow(fTemp3, 1.0) * 1., 0., 0.);
    
	return vec4( color, 1.0 );

}
