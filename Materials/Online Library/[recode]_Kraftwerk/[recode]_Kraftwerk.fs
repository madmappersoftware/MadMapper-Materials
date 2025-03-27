/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Kraftwek - Vertigo / 1972",
    "TAGS": "album cover",
    "VERSION": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },
		{ "LABEL": "Global/Speed", "NAME": "speed", "TYPE": "float", "MIN": -2, "MAX": 2, "DEFAULT": 0 },
		{ "LABEL": "Global/Offset ", "NAME": "offset", "TYPE": "point2D", "MAX": [ 2, 2 ], "MIN": [ -2, -2 ], "DEFAULT": [ -0.4, 0.0 ]},
	
		{ "LABEL": "Shape/Count", "NAME": "shapesCount", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 1 },
		{ "LABEL": "Shape/Offset X", "NAME": "shapeOffsetX", "TYPE": "float", "MIN": -0.2, "MAX": 0.2, "DEFAULT": 0 },
		{ "LABEL": "Shape/Offset Y", "NAME": "shapeOffsetY", "TYPE": "float", "MIN": -0.2, "MAX": 0.2, "DEFAULT": 0 },
		{ "LABEL": "Shape/Stroke weight", "NAME": "strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Shape/Amplitude power", "NAME": "amplitudePower", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Shape/Phase", "NAME": "phase", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Shape/Spacing", "NAME": "spacing", "TYPE": "float", "MIN": 0.1, "MAX": 3, "DEFAULT": 0.25 },
		{ "LABEL": "Shape/Noise Variation", "NAME": "noiseVariation", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 1.3 }, 

		{ "LABEL": "Lines/Count", "NAME": "linesCount", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 40 },
		{ "LABEL": "Lines/Speed", "NAME": "mat_lineSpeed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0},
		{ "LABEL": "Lines/Lines width", "NAME": "linesWidth", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.03 },
		{ "LABEL": "Lines/Lines height", "NAME": "linesHeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
		{ "LABEL": "Lines/Lines noise", "NAME": "linesNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0, "FLAGS": "hidden" },
		{ "LABEL": "Lines/Lines size", "NAME": "lineSize", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Lines/Randomize", "NAME": "linesRandom", "TYPE": "event",  "DEFAULT": true },

        { "LABEL": "Colors/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ]}
    ],
	"GENERATORS": [
		{ "NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}},
		{ "NAME": "mat_line_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_lineSpeed"}},
   		{"NAME": "mat_incrementer", "TYPE": "incrementer", "PARAMS": {"increment": "linesRandom"} }
	]
}*/


#include "MadSDF.glsl"
#include "MadNoise.glsl"
#include "MadCommon.glsl"

const vec2 center = vec2(0.5);
const vec3 shapeColor = vec3(0.01, 0.91, 0.99);

float sinusDist(vec2 p, float amp, float phase)
{
	p.x = p.x + p.x * 2.5;
	return mix(sin(p.x + phase) * amp, p.y, 0.4);
}

float random (vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898,78.233)))*43758.5453123);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	float scaleFactor = scale * 80;
	vec2 uv = (texCoord - center - offset) * scaleFactor ;
	vec3 color = backgroundColor.rgb;
	float t = mat_animation_time;

	vec2 domain = uv;

	for(int s=0; s<shapesCount; s++){
		// Shape distance field
		domain.x  *= spacing;

		float amplitude = pow(sin(domain.x * 0.08 + t), 2) * amplitudePower * 25 + noise(vec2(pow(domain.x, 1), t)) * noiseVariation;	
		float shapeDist = sinusDist(domain, amplitude, phase * 2.5) * sinusDist(domain, amplitude, PI);	

		float lines = line(vec2(domain.x/scaleFactor + fract((-mat_line_time + 1.41)/2)*2-1, domain.y/scaleFactor),
						   vec2(-0.5 *lineSize, 0),
						   vec2(0.5 * lineSize, 0), 0.25 * linesWidth);
	
		for(int i=0; i<linesCount; i++)
		{	
			float noiseHeight = ridgedNoise(vec2(i, mat_incrementer + i)) * linesHeight;

			float line = line(domain/scaleFactor,
							  vec2(float(i)/linesCount - 0.5, noiseHeight),
							  vec2(float(i)/linesCount - 0.5, -noiseHeight), 0.05 * linesWidth);

			lines = min(line, lines);
		}

		// Shape color
		color = fill(color, shapeColor, lines);	
		color = stroke(color, shapeColor, shapeDist, strokeWeight * amplitude);	

		domain.x +=  shapeOffsetX * 20;
		domain.y +=  shapeOffsetY * 20;
	}
	
	// Return final color
	return  vec4(color, 1);
}