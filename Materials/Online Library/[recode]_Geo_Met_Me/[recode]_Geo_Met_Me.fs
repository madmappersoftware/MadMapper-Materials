/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Inspired by Geo Met Me - Square Spiral / 2009",
    "VSN": "1.0",
    "TAGS": "painting",
    "INPUTS": [
        { "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN":0, "MAX": 10, "DEFAULT": 1.2},
		{ "LABEL": "Global/Rotation", "NAME": "rotation", "TYPE": "float", "MIN": -1.571, "MAX": 1.571, "DEFAULT": 0.06},
		
		{ "LABEL": "Shape/Shapes count", "NAME": "shapesCount", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 20 },
		{ "LABEL": "Shape/Edges count", "NAME": "edgesCount", "TYPE": "int", "MIN": 2, "MAX": 10, "DEFAULT": 4 },
		{ "LABEL": "Shape/Stroke weight", "NAME": "strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 0.1, "DEFAULT": 0.002 },

		{ "LABEL": "Noise/Noise power", "NAME": "noisePower", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0},
		{ "LABEL": "Noise/Noise speed", "NAME": "noiseSpeed", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0},
		
        { "LABEL": "Colors/Start gradient", "NAME": "startGradient", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Colors/End gradient", "NAME": "endGradient", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Colors/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Colors/Fill", "NAME": "isShapeFilled", "TYPE": "bool",  "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Colors/Gradient", "NAME": "isGradient", "TYPE": "bool",  "DEFAULT": true, "FLAGS": "button" }
	],
	"GENERATORS": [
		{ "NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "noiseSpeed"}},
	]
}*/


#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"

float Shape(vec2 p, float radius, int edges)
{
	// Angle and radius from the current pixel
	float a = atan(p.x,p.y)+PI;
	float r = PI*2/float(edges);
	
	// Shaping function that modulate the distance
	return cos(floor(.5+a/r)*r-a) * length(p) - radius;
}

vec4 materialColorForPixel(vec2 texCoord)
{
	// Init variables
	vec4 color = backgroundColor;
	vec4 shapeColor = vec4(0);
	vec2 center = vec2(0.5);	
	vec2 uv = (texCoord - center) * scale;
	uv = rotate(uv, -0.06);
	float noiseTime = mat_noise_time * 2;

	float size = 0.5;
	float rotationAngle = rotation;
	float factor = 1 / (abs(sin(rotationAngle)) + abs(cos(rotationAngle)));
		
	for(int i = 1; i < shapesCount + 1; i++)
	{	
		// Noise uv
		float noiseX = billowedNoise(vec2(noiseTime, i))*2-1;
		float noiseY = billowedNoise(vec2(noiseTime, i*2.784))*2-1;

		vec2 noise = vec2(noiseX, noiseY);
		uv += noise * noisePower;

		// Rotate uv
		uv = rotate(uv, rotationAngle);

		// Scale
        size *= factor;

		// Create shape
		float squareDist = Shape(uv, size, edgesCount);

		// Color shape
		float lerpValue = float(i)/float(shapesCount);
		
		if(isGradient){
			shapeColor = mix(startGradient, endGradient, lerpValue);				
		} else {
			shapeColor = mix(startGradient, endGradient, i%2);
		}	
	
		if(isShapeFilled){
			color = fill(color, shapeColor, squareDist);  				
		} else {
			color = stroke(color, shapeColor, squareDist, strokeWeight); 
		}		
	}	
	
	return color ;
}
