/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Orbital - Blue Album / 2004",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },
		{ "LABEL": "Global/Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },
		
		{ "LABEL": "Protons/Count", "NAME": "protonsCount", "TYPE": "int", "MIN": 0, "MAX": 20, "DEFAULT": 3 },
		{ "LABEL": "Protons/Speed", "NAME": "protonsSpeed", "TYPE": "float", "MIN": -2, "MAX": 2, "DEFAULT": 0.5 },

		{ "LABEL": "Ellipses/Level", "NAME": "ellipsesLevel", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Ellipses/Count", "NAME": "ellipsesCount", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 3 },
		{ "LABEL": "Ellipses/Iterations", "NAME": "ellipsesIterations", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 2 },
		{ "LABEL": "Ellipses/Scale X", "NAME": "scaleX", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.6 },
		{ "LABEL": "Ellipses/Scale Y", "NAME": "scaleY", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },

		{ "LABEL": "Noise/Ellipse noise", "NAME": "ellipseNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
		{ "LABEL": "Noise/Iterations noise", "NAME": "iterationsNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3 },
		{ "LABEL": "Noise/Offset noise", "NAME": "offsetNoise", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3  },
    ],
	"GENERATORS": [
		{ "NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve": 2}},
		{ "NAME": "mat_protons_time", "TYPE": "time_base", "PARAMS": {"speed": "protonsSpeed", "speed_curve": 2}},
	],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 8192
    }
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


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float t = mat_animation_time;

	// Protons
	int totalProtonCount = ellipsesCount * protonsCount;
	int pointsForProtons = totalProtonCount;
	if (pointNumber < pointsForProtons) {
		int ellipseNum = pointNumber / protonsCount;
		int protonNum = pointNumber % protonsCount;
		shapeNumber = pointNumber;
		color = vec4(1);

		vec2 deformationNoise = vec2(map(noise(vec3(ellipseNum, t, 0.1)) * ellipseNoise * 2, -1, 1, 0.2, scaleX-0.3),
										map(noise(vec3(ellipseNum, t*1.5,0.3)) * ellipseNoise * 2, -1, 1, 0.9, scaleY));

		vec2 posElec = vec2(sin(mat_protons_time + protonNum/PI) * ellipsesRadius / deformationNoise.x, 
							cos(mat_protons_time + protonNum/PI) * ellipsesRadius / deformationNoise.y);

		pos = rotate(posElec,ellipseNum*PI/ellipsesCount);
		pos *= 2 / (scale+0.000001);

		return;
	}

	// Ellipses
	int pointsForEllipses = pointCount - pointsForProtons;
	int pointsPerEllipseIteration = pointsForEllipses / (ellipsesCount * ellipsesIterations);
	int ellipseItNum = (pointNumber - pointsForProtons) / pointsPerEllipseIteration;
	if (ellipseItNum >= ellipsesCount*ellipsesIterations) {
		shapeNumber = -1;
		return;
	}
    shapeNumber = totalProtonCount + ellipseItNum;
	int ellipseNum = ellipseItNum / ellipsesIterations;
	int ellipseIteration = ellipseItNum - ellipseNum * ellipsesIterations;
	
	int pointIdxInIteration = pointNumber - pointsForProtons - ellipseItNum * pointsPerEllipseIteration;
    float normalizedPosInShape = float(pointIdxInIteration) / (pointsPerEllipseIteration-1);

	vec2 deformationNoise = vec2(map(noise(vec3(ellipseNum, t, 0.1)) * ellipseNoise * 2, -1, 1, 0.2, scaleX-0.3),
									map(noise(vec3(ellipseNum, t*1.5,0.3)) * ellipseNoise * 2, -1, 1, 0.9, scaleY));


	vec2 origPos = vec2(sin(normalizedPosInShape*2*PI) * ellipsesRadius / deformationNoise.x, 
			   cos(normalizedPosInShape*2*PI) * ellipsesRadius / deformationNoise.y);
	origPos = rotate(origPos,ellipseNum*PI/ellipsesCount);

	// Distortion
	float noiseLength = pow(noise(vec3(origPos + t * 0.5 + ellipseNum * 0.1, ellipseIteration*1.2 )), 2) * iterationsNoise * 0.2;
	pos = origPos / (1 + vec2(noiseLength));

	if(ellipseNum%2 == 0){
		pos += noise(vec3(origPos, ellipseIteration*5 + t)) * offsetNoise * 0.02;
	} else {
		pos -= noise(vec3(origPos , ellipseIteration*5 + t)) * offsetNoise * 0.02;
	}

	pos *= 2 / (scale+0.000001);

	color = vec4(ellipseColor*ellipsesLevel,1);
}

//vec4 materialColorForPixel( vec2 texCoord )
//{
//	// Set up
//	vec2 uv = (texCoord - center) * scale;
//	vec3 color = backgroundColor.rgb;
//	float t = mat_animation_time;
//
//	for(int e = 0; e < ellipsesCount; e++) {	
//		// Rotation
//		uv = rotate(uv, PI/ellipsesCount);
//		
//		vec2 pos = uv;
//		vec2 deformationNoise = vec2(map(noise(vec3(e, t, 0.1)) * ellipseNoise * 2, -1, 1, 0.2, scaleX-0.3),
//									 map(noise(vec3(e, t*1.5,0.3)) * ellipseNoise * 2, -1, 1, 0.9, scaleY));
//			
//		// PROTONS
//		for(int p = 0; p < protonsCount; p++) {	
//			vec2 posElec = vec2(pos.x + sin(mat_protons_time + p/PI) * ellipsesRadius / deformationNoise.x, 
//								pos.y + cos(mat_protons_time + p/PI) * ellipsesRadius / deformationNoise.y);	
//			
//			float proton = circle(posElec, 0.02 * protonsScale);
//	
//			color = fill(color, vec3(0.29, 0.38, 0.58) * 2.5, proton);	
//		}	
//		
//		pos *= deformationNoise;
//		
//		// ELLIPSES
//		for(int i = 0; i < ellipsesIterations; i++) {
//			// Distortion
//			float noiseLength = pow(noise(vec3(uv + t * 0.5 + e * 0.1, i )), 2) * iterationsNoise * 0.2;
//
//			if(i%2 == 0){
//				pos += noise(vec3(uv, i + t)) * offsetNoise * 0.02;
//			} else {
//				pos -= noise(vec3(uv , i + t)) * offsetNoise * 0.02;
//			}
//
//			// Ellipse dist
//			float ellipseDist = circle(pos, ellipsesRadius) + noiseLength;
//
//			// Ellipse color
//			color += stroke(vec3(0), ellipseColor, ellipseDist, strokeWeight * 0.01);	
//		}
//	}
//
//	return  vec4(color, 1);
//}
