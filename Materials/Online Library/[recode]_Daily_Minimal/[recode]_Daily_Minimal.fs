/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Daily Minimal - NO 574",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },
		{ "LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0 },
		{ "LABEL": "Global/Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },

		{ "LABEL": "Shape/Count", "NAME": "mat_circlesCount", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 20 },
		{ "LABEL": "Shape/Edges", "NAME": "mat_edges", "TYPE": "int", "MIN": 3, "MAX": 50, "DEFAULT": 50 },
		{ "LABEL": "Shape/Stroke Weight", "NAME": "mat_strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },

		{ "LABEL": "Noise/Noise Power", "NAME": "mat_noisePower", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },
		{ "LABEL": "Noise/Noise Freq", "NAME": "mat_noiseFrequency", "TYPE": "float", "MIN": 0, "MAX": 20, "DEFAULT": 10 },
		{ "LABEL": "Noise/Noise Offset", "NAME": "mat_noiseOffset", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
		{ "LABEL": "Noise/Noise Position", "NAME": "mat_noisePosition", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.6 },

 		{ "LABEL": "Graphic/Background", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.08, 0.08, 0.08, 1.0 ] }
    ],
	"GENERATORS": [
		{ "NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"}}
	]
}*/

#include "MadSDF.glsl"
#include "MadNoise.glsl"
#include "MadCommon.glsl"

const vec2 center = vec2(0.5);
const float circleRadius = 0.2;

float Shape(vec2 p, float radius, int edges)
{
	float pi = 3.14159;

	// Angle and radius from the current pixel
	float a = atan(p.x,p.y)+pi;
	float r = pi*2/float(edges);
	
	// Shaping function that modulate the distance
	return cos(floor(.5+a/r)*r-a) * length(p) - radius;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	vec3 color = mat_backgroundColor.rgb;
	vec2 uv = (vec2(texCoord.x, 1.0 - texCoord.y) - center) * mat_scale;
	uv = rotate(uv, mat_rotation * 2 * PI);

	float noiseIntensity = mat_noisePower * 0.05;
	float noisePosition = smoothstep(0.3, 0.7, uv.y + mat_noisePosition);

	for (int i = 0; i < mat_circlesCount; i++) {	
		float noiseCircle = noise(vec3(uv * mat_noiseFrequency, i * mat_noiseOffset + mat_time));
		noiseCircle *= noiseIntensity * noisePosition;

		float circleDist = Shape(uv, circleRadius , mat_edges) + noiseCircle;

		color = stroke(color, vec3(1), circleDist, mat_strokeWeight * 0.006); // 0.0006
	}

	return  vec4(color, 1);
}