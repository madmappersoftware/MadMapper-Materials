/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "Dynamic moving triangles with advanced customization options",
	"TAGS": "triangles, animation, symmetry",
	"VSN": "1.1",
	"INPUTS": [
		{"LABEL": "Global/Number of Triangles", "NAME": "tri_count", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 10 },
		{"LABEL": "Global/Triangle Size", "NAME": "tri_size", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Global/Speed", "NAME": "tri_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 
		{"LABEL": "Global/Rotation", "NAME": "tri_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Position/X Offset", "NAME": "tri_x_offset", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Position/Y Offset", "NAME": "tri_y_offset", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Symmetry/Mirror", "NAME": "tri_symmetry", "TYPE": "bool", "DEFAULT": false },

		{"LABEL": "Noise/Type", "NAME": "tri_noise_type", "TYPE": "long", "DEFAULT": "Flow", "VALUES": [ "Flow", "Billow","White" ] },
		{"LABEL": "Noise/Speed", "NAME": "tri_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{"LABEL": "Noise/Scale", "NAME": "tri_noise_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{"LABEL": "Noise/Intensity", "NAME": "tri_noise_intensity", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },

		{"LABEL": "Color/Bottom Left", "NAME": "tri_color1", "TYPE": "color", "DEFAULT": [ 0.2, 0.6, 1.0, 1.0 ] },
		{"LABEL": "Color/Top Left", "NAME": "tri_color2", "TYPE": "color", "DEFAULT": [ 1.0, 0.2, 0.2, 1.0 ] },
		{"LABEL": "Color/Top Right", "NAME": "tri_color3", "TYPE": "color", "DEFAULT": [ 0.2, 1.0, 0.6, 1.0 ] },
		{"LABEL": "Color/Bottom Right", "NAME": "tri_color4", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 0.2, 1.0 ] }
	],
	"GENERATORS": [
		{"NAME": "tri_time", "TYPE": "time_base", "PARAMS": {"speed": "tri_speed", "speed_curve": 2, "link_speed_to_global_bpm": true }},
		{"NAME": "tri_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "tri_noise_speed", "speed_curve": 2, "link_speed_to_global_bpm": true }},
		{"NAME": "tri_point_count", "TYPE": "multiplier", "PARAMS": {"value1": "tri_count", "value2": 3 }}
	],
	"RENDER_SETTINGS": {
		"POINT_COUNT": "tri_point_count",
		"MAX_SPEED": 4,
		"ANGLE_OPTIMIZATION": false,
		"PRESERVE_ORDER": true,
		"SKIP_BLACK": false
	}
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	// Triangle and vertex calculation
	int triIndex = pointNumber / 3;
	int vertexIndex = pointNumber % 3;

	// Calculate base triangle layout
	float angleOffset = float(vertexIndex) * 120.0 * pi / 180.0; // 120 degrees apart
	vec2 basePos = vec2(-0.5 + (float(triIndex) / float(tri_count)) * 2.0, -0.5 + (triIndex % 2) * 0.5);
	vec2 localPos = vec2(cos(angleOffset), sin(angleOffset)) * tri_size;

	// Apply rotation
	float rotationRad = tri_rotation * pi / 180.0;
	localPos = mat2(cos(rotationRad), -sin(rotationRad), sin(rotationRad), cos(rotationRad)) * localPos;

	// Apply offsets
	vec2 adjustedPos = basePos + localPos + vec2(tri_x_offset, tri_y_offset);

	// Add animation and noise
	vec2 animatedPos = adjustedPos + vec2(0.0, tri_time * 0.5);

	vec3 noise = vec3(0.0);
	if (tri_noise_type == 0) noise = dFlowNoise(animatedPos * tri_noise_scale, tri_noise_time);
	if (tri_noise_type == 1) noise = dBillowedNoise(vec3(animatedPos * tri_noise_scale, tri_noise_time)).xyz;
	if (tri_noise_type == 2) noise = dRidgedMF(vec3(animatedPos * tri_noise_scale, tri_noise_time)).xyz;

	animatedPos += noise.xy * tri_noise_intensity;

	// Apply symmetry
	if (tri_symmetry && animatedPos.x > 0.0)
		animatedPos.x = -animatedPos.x;

	// Assign output position
	pos = animatedPos;

	// Assign gradient colors
	float gradientFactor = float(vertexIndex) / 2.0;
	if (gradientFactor < 0.5)
		color = mix(tri_color1, tri_color2, gradientFactor * 2.0);
	else
		color = mix(tri_color3, tri_color4, (gradientFactor - 0.5) * 2.0);

	shapeNumber = triIndex;
}
