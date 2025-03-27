/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Victor Vasarely - Desillusion / 1960",
    "TAGS": "painting",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 6},
		{ "LABEL": "Global/Circles count", "NAME": "circles_count", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 22},
		{ "LABEL": "Global/Smooth", "NAME": "smoothValue", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1},

		{ "LABEL": "Global/Position ", "NAME": "position", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ]},
		{ "LABEL": "Global/Amplitude ", "NAME": "amplitude", "TYPE": "point2D", "MAX": [ 0.1, 0.1 ], "MIN": [ -0.1, -0.1 ], "DEFAULT": [ 0.0, 0.02 ]},
		{ "LABEL": "Global/Offset first circle ", "NAME": "offsetFirstCircle", "TYPE": "point2D", "MAX": [ 0.25,  0.25 ], "MIN": [ -0.25, -0.25 ], "DEFAULT": [ 0.0, 0.0 ]},		

		{ "LABEL": "Color/Noise Power", "NAME": "noisePower", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 0},
		{ "LABEL": "Color/Frisson", "NAME": "frisson", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0},
        { "LABEL": "Color/Odd circle", "NAME": "odd_color", "TYPE": "color", "DEFAULT": [ 0.3, 0.8, 1.0, 1.0 ] },
        { "LABEL": "Color/Even circle", "NAME": "even_color", "TYPE": "color", "DEFAULT": [ 1.0, 0.44, 0.4, 1.0 ] },  
        { "LABEL": "Color/Background", "NAME": "background_color", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
		{ "LABEL": "Color/Odd bright", "NAME": "odd_brightness", "TYPE": "float", "MIN": -0.1, "MAX": 0.1, "DEFAULT": -0.035},
		{ "LABEL": "Color/Even brigh", "NAME": "even_brightness", "TYPE": "float", "MIN": -0.1, "MAX": 0.1, "DEFAULT": -0.05}
    ]
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	vec2 offset;
	vec2 center = vec2(0.5);
	vec2 uv = (texCoord - center) * scale;
	vec3 color = background_color.rgb;
	int circlesCount = circles_count - 1;
	
	// Make circles	
	for (int i = circles_count; i > 0; i--) {	
		// Make circles distance field
		float mappedI = -cos((2 * PI/circlesCount) * i) * circlesCount/2 + circlesCount/2;	
	
		offset = mappedI * amplitude * smoothValue + position; 
		offset += offsetFirstCircle * (circles_count - i);
		
		float circleDist = circle(uv, offset, 0.1 * i );
		
		// Make circles color
		vec3 evenColor = even_color.rgb + (circlesCount - i) * even_brightness; // 0.04
		vec3 oddColor = odd_color.rgb + i * odd_brightness; // 0.015
		vec3 circleColor = mix(oddColor, evenColor, i % 2);

		float noise = billowedNoise(vec3(noisePower, 2.234, i * 0.798));

		// Return color 
		color = fill(color, circleColor, circleDist + noise * frisson);
	}
	
	// Return final color
	return vec4(color, 1);
}
