/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "Dynamic moving triangles with configurable noise",
	"TAGS": "triangles, animation",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Number of Triangles", "NAME": "tri_count", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 10 },
		{"LABEL": "Global/Triangle Size", "NAME": "tri_size", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Global/Speed", "NAME": "tri_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 
		{"LABEL": "Noise/Type", "NAME": "tri_noise_type", "TYPE": "long", "DEFAULT": "Flow", "VALUES": [ "Flow", "Billow","White" ] },
		{"LABEL": "Noise/Speed", "NAME": "tri_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{"LABEL": "Noise/Scale", "NAME": "tri_noise_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{"LABEL": "Noise/Intensity", "NAME": "tri_noise_intensity", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
		{"LABEL": "Color/Base", "NAME": "tri_base_color", "TYPE": "color", "DEFAULT": [ 0.2, 0.6, 1.0, 1.0 ] },
		{"LABEL": "Color/Tip", "NAME": "tri_tip_color", "TYPE": "color", "DEFAULT": [ 1.0, 0.2, 0.2, 1.0 ] }
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

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	// Normalize the current point index
	int triIndex = pointNumber / 3; // Each triangle has 3 points
	int vertexIndex = pointNumber % 3;

	// Define triangle layout (equilateral)
	float angleOffset = float(vertexIndex) * 120.0 * 3.14159265359 / 180.0; // Each vertex is 120 degrees apart
	vec2 basePos = vec2(-0.5 + (float(triIndex) / float(tri_count)) * 2.0, -0.5 + (triIndex % 2) * 0.5);
	vec2 localPos = vec2(cos(angleOffset), sin(angleOffset)) * tri_size;

	// Add animation and noise
	vec2 animatedPos = basePos + localPos + vec2(0.0, tri_time * 0.5); // Vertical movement

	vec3 noise = vec3(0.0);
	if (tri_noise_type == 0) noise = dFlowNoise(animatedPos * tri_noise_scale, tri_noise_time);
	if (tri_noise_type == 1) noise = dBillowedNoise(vec3(animatedPos * tri_noise_scale, tri_noise_time)).xyz;
	if (tri_noise_type == 2) noise = dRidgedMF(vec3(animatedPos * tri_noise_scale, tri_noise_time)).xyz;

	animatedPos += noise.xy * tri_noise_intensity;

	// Assign output position and color
	pos = animatedPos;
	color = mix(tri_base_color, tri_tip_color, float(vertexIndex) / 2.0);
	shapeNumber = triIndex;
}
