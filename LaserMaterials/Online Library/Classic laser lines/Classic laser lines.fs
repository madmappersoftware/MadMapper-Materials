/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"DESCRIPTION": "Laser Material Function",
	"CREDIT": "Created by Alex",
	"ISFVSN": "2",
	"CATEGORIES": [
		"Laser"
	],
	"INPUTS": [
		{ "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 3 },
		//{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Spread", "NAME": "mat_spread", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
		{
			"LABEL": "Rotation",
			"NAME": "mat_rotation",
			"TYPE": "float",
			"MIN": -360.0,
			"MAX": 360.0,
			"DEFAULT": 0.0
		},
		{
			"LABEL": "Movement Y",
			"NAME": "mat_movementY",
			"TYPE": "float",
			"MIN": -1.0,
			"MAX": 1.0,
			"DEFAULT": 0.0
		},
		{
			"LABEL": "Movement X",
			"NAME": "mat_movementX",
			"TYPE": "float",
			"MIN": -1.0,
			"MAX": 1.0,
			"DEFAULT": 0.0
		},
		{
			"LABEL": "Color/Front Color",
			"NAME": "mat_foregroundColor",
			"TYPE": "color",
			"DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ],
			"FLAGS": "no_alpha"
		},
		{
			"LABEL": "Color/Background Color",
			"NAME": "mat_backgroundColor",
			"TYPE": "color",
			"DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ],
			"FLAGS": "no_alpha"
		}
	],
	"GENERATORS": [
		{ "NAME": "mat_time", "TYPE": "time_base", "PARAMS": { "speed": "mat_speed" } },
		{
			"NAME": "mat_instance_count",
			"TYPE": "multiplier",
			"PARAMS": {
				"value1": "mat_count",
				"value2": 4
			}
		}
	],
	"RENDER_SETTINGS": {
		"POINT_COUNT": "mat_instance_count",
		"PRESERVE_ORDER": true,
		"ENABLE_FRAME_BLENDING": true
	}
}*/



#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat2 ro(float a) { return mat2(cos(a), sin(a), -sin(a), cos(a)); }
float T(float t) { return floor(t) + smoothstep(0., 1., fract(t)); }


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float t = mat_time;
	float indexNorm = float(pointNumber) / (pointCount - 1);
	int global_point = pointNumber % 4;
	int copy = pointNumber / 4;
	int local_point = global_point % 2;

	// Fixed width and position calculation
	float w = mat_scale * 0.5; // Fixed scale
	float copy_width = (0.25 * mat_spread);
	if (mat_count == 1) copy_width = 0.;
	if (global_point < 2)
	{
		pos = vec2(copy_width * copy - copy_width * (mat_count - 1) * 0.5, w - local_point * w * 2.);
		shapeNumber = 1;
		color = mat_backgroundColor;
	}
	else
	{
		pos = vec2(copy_width * copy - copy_width * (mat_count - 1) * 0.5, w - local_point * w * 2.);
		shapeNumber = pointNumber;
		color = mat_foregroundColor;
	}

	// Apply rotation based on the input parameter
	float radians = radians(mat_rotation);
	pos *= ro(radians);

	// Apply movement along Y axis
	pos.y += mat_movementY;

	// Apply movement along X axis
	pos.x += mat_movementX;
}