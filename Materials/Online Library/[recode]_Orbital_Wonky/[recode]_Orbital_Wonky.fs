/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Orbital - Wonky Album / 2012",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1.4 },

		{ "LABEL": "Circles/Count", "NAME": "circlesCount", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 11 },
		{ "LABEL": "Circles/Radius", "NAME": "circlesRadius", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.37 },
		{ "LABEL": "Circles/Stroke weight", "NAME": "strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.18 },

		{ "LABEL": "Noise/Noise uv", "NAME": "noiseUV", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.08 },
		{ "LABEL": "Noise/Noise freq", "NAME": "noiseFrequency", "TYPE": "float", "MIN": 0, "MAX": 50, "DEFAULT": 0 },
		{ "LABEL": "Noise/Noise circle", "NAME": "noiseCircle", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.15 },
		{ "LABEL": "Noise/Noise stroke", "NAME": "noiseStroke", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
		{ "LABEL": "Noise/Noise offset", "NAME": "noiseOffset", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3 },
		{ "LABEL": "Noise/Speed", "NAME": "noiseSpeed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0 },

		{ "LABEL": "Color/Opacity", "NAME": "colorOpacity", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.17 },
		{ "LABEL": "Color/Pow", "NAME": "colorPow", "TYPE": "float", "MIN": 1, "MAX": 2, "DEFAULT": 1.17 },
		{ "LABEL": "Color/Pow Intensity", "NAME": "colorPowIntensity", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0.9 },
		{ "LABEL": "Color/Smooth min", "NAME": "colorSmoothMin", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Color/Smooth max", "NAME": "colorSmoothMax", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Color/Map min", "NAME": "colorMapMin", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Color/Map max", "NAME": "colorMapMax", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Color/Saturation", "NAME": "colorSaturation", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.8 },
		{ "LABEL": "Color/Value", "NAME": "colorValue", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Color/Fract", "NAME": "colorFract", "TYPE": "float", "MIN": 0, "MAX": 0.5, "DEFAULT": 0 }
    ],
	"GENERATORS": [
		{ "NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "noiseSpeed"}},
	]
}*/

#include "MadSDF.glsl"
#include "MadNoise.glsl"
#include "MadCommon.glsl"

const vec2 center = vec2(0.5);

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	vec2 uv = (texCoord - center) * scale;
	vec4 color;

	for (int i = 0; i < circlesCount; i++) {	
		// All noises
		vec2 noiseUV = uv * mix(1, flowNoise(uv * noiseFrequency, mat_noise_time), noiseUV);
		float noiseCircle = pow(noise(uv + mat_noise_time * 0.5 + i * noiseOffset * 0.1), 2) * noiseCircle;
		float noiseStroke =  mix(1, noise(uv + mat_noise_time), noiseStroke) * strokeWeight;

		// Circle dist
		float circleDist = circle(noiseUV, circlesRadius) - noiseCircle;

		// Circle color
		float hue = fract((i)/(circlesCount+0.1)) + colorFract;
		hue = pow(hue * colorPowIntensity,colorPow);

		vec4 circleColor = vec4(hsv2rgb(vec3(hue, colorSaturation, colorValue)), 0) * colorOpacity;
		color += stroke(vec4(0), circleColor, circleDist, noiseStroke);
	}

	return  1.-color;
}