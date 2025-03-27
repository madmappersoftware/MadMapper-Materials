/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Do something cool",
	"DESCRIPTION": "describe your material here",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Strob", "NAME": "mat_strob", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Scale", "NAME": "mat_global_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.8 }, 

		{"LABEL": "Count/Min", "NAME": "mat_min_count", "TYPE": "int", "MIN": 0, "MAX": 40, "DEFAULT": 0 }, 
		{"LABEL": "Count/Max", "NAME": "mat_max_count", "TYPE": "int", "MIN": 0, "MAX": 40, "DEFAULT": 3 }, 

		{"LABEL": "Shape/Min Length", "NAME": "mat_min_length", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.01 }, 
		{"LABEL": "Shape/Max Length", "NAME": "mat_max_length", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 }, 
		{"LABEL": "Shape/Scale", "NAME": "mat_shape_scale", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.2 }, 
	],
	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"strob": "mat_strob"} },
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 400,
	   "PRESERVE_ORDER": false,
	   "ENABLE_FRAME_BLENDING": false
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec2 hash( vec2 p )                       // rand in [0,1]
{
	p = vec2(dot(p,vec2(127.1,311.7)),
			 dot(p,vec2(269.5,183.3)));
	return fract(sin(p+20.)*53758.5453123);
}

// 2d noise functions from https://www.shadertoy.com/view/XslGRr
float mat_noise( vec2 x )
{
	vec2 p = floor(x);
	vec2 f = fract(x);
	f = f*f*(3.0-2.0*f);
	vec2 uv = (p+vec2(37.0,17.0)) + f;
	vec2 rg = hash( uv/256.0 ).yx;
	return mix( rg.x, rg.y, 0.5 );
}

float mat_noise( float x, int y )
{
	return mat_noise(vec2(x,y));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int shapeCount = int(round(mix(mat_min_count,mat_max_count,fract(mat_noise(mat_time,0)))));

	// We'll make 20 points per shape
	int pointsPerShape = pointCount / shapeCount;
	shapeNumber = pointNumber/pointsPerShape;
	if (shapeNumber >= shapeCount) {
		shapeNumber = -1; // Points with a negative shape number are ignored
		return;
	}

	float normalizedPosInShape = (pointNumber%pointsPerShape)/float(pointsPerShape-1);
	float shapeLength = mix(mat_min_length,mat_max_length,fract(mat_noise(mat_time,shapeNumber)));

	vec2 shapeCenterPos = fract(hash(vec2(mat_time,shapeNumber)));
	vec2 noiseAtCenter = dFlowNoise(vec2(mat_time*100+0.5*shapeLength,mat_time*50+11*shapeNumber),0).yz;
	vec2 noiseAtThisPos = dFlowNoise(vec2(mat_time*100+normalizedPosInShape*shapeLength,mat_time*50+11.1*shapeNumber),0).yz;
	pos = mat_global_scale * (-1 + 2 * (shapeCenterPos + mat_shape_scale*mat_shape_scale*(noiseAtThisPos-noiseAtCenter)));

	color = vec4(1);
}
