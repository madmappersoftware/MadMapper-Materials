/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Orbital - Blue Album / 2004",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },
		{ "LABEL": "Global/Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		
		{ "LABEL": "Protons/Count", "NAME": "protonsCount", "TYPE": "int", "MIN": 0, "MAX": 20, "DEFAULT": 0 },
		{ "LABEL": "Protons/Speed", "NAME": "protonsSpeed", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Protons/Scale", "NAME": "protonsScale", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },

		{ "LABEL": "Ellipses/Count", "NAME": "ellipsesCount", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 3 },
		{ "LABEL": "Ellipses/Iterations", "NAME": "ellipsesIterations", "TYPE": "int", "MIN": 1, "MAX": 100, "DEFAULT": 60 },
		{ "LABEL": "Ellipses/Stroke weight", "NAME": "strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.05 },
		{ "LABEL": "Ellipses/Scale X", "NAME": "scaleX", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.6 },
		{ "LABEL": "Ellipses/Scale Y", "NAME": "scaleY", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },

		{ "LABEL": "Noise/Ellipse noise", "NAME": "ellipseNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
		{ "LABEL": "Noise/Iterations noise", "NAME": "iterationsNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3 },
		{ "LABEL": "Noise/Offset noise", "NAME": "offsetNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3  },

        { "LABEL": "Colors/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.06, 0.14, 0.27, 1.0 ]}
    ],
	"GENERATORS": [
		{ "NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}},
		{ "NAME": "mat_protons_time", "TYPE": "time_base", "PARAMS": {"speed": "protonsSpeed"}},
	]
}*/

#include "MadSDF.glsl"
#include "MadNoise.glsl"
#include "MadCommon.glsl"

const vec2 center = vec2(0.5);
const vec3 ellipseColor = vec3(0.29, 0.38, 0.58);
const float ellipsesRadius = 0.09;

float map(float value, float min1, float max1, float min2, float max2) {
  return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	vec2 uv = (texCoord - center) * scale;
	vec3 color = backgroundColor.rgb;
	float t = mat_animation_time;

	for(int e = 0; e < ellipsesCount; e++) {	
		// Rotation
		uv = rotate(uv, PI/ellipsesCount);
		
		vec2 pos = uv;
		vec2 deformationNoise = vec2(map(noise(vec3(e, t, 0.1)) * ellipseNoise * 2, -1, 1, 0.2, scaleX-0.3),
									 map(noise(vec3(e, t*1.5,0.3)) * ellipseNoise * 2, -1, 1, 0.9, scaleY));
			
		// PROTONS
		for(int p = 0; p < protonsCount; p++) {	
			vec2 posElec = vec2(pos.x + sin(mat_protons_time + p/PI) * ellipsesRadius / deformationNoise.x, 
								pos.y + cos(mat_protons_time + p/PI) * ellipsesRadius / deformationNoise.y);	
			
			float proton = circle(posElec, 0.02 * protonsScale);
	
			color = fill(color, vec3(0.29, 0.38, 0.58) * 2.5, proton);	
		}	
		
		pos *= deformationNoise;
		
		// ELLIPSES
		for(int i = 0; i < ellipsesIterations; i++) {
			// Distortion
			float noiseLength = pow(noise(vec3(uv + t * 0.5 + e * 0.1, i )), 2) * iterationsNoise * 0.2;

			if(i%2 == 0){
				pos += noise(vec3(uv, i + t)) * offsetNoise * 0.02;
			} else {
				pos -= noise(vec3(uv , i + t)) * offsetNoise * 0.02;
			}

			// Ellipse dist
			float ellipseDist = circle(pos, ellipsesRadius) + noiseLength;

			// Ellipse color
			color += stroke(vec3(0), ellipseColor, ellipseDist, strokeWeight * 0.01);	
		}
	}

	return  vec4(color, 1);
}