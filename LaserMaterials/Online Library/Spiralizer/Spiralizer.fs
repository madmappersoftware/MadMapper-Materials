/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader that generates spirographic patterns dynamically with full customization controls",
	"TAGS": "spirograph, laser, generative, customizable",
	"VSN": "1.2",
	"INPUTS": [
		{"LABEL": "Scale (Zoom)", "NAME": "design_scale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.425 },
		{"LABEL": "Position X", "NAME": "global_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Position Y", "NAME": "global_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Inner Radius", "NAME": "inner_radius", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.506 },
		{"LABEL": "Outer Radius", "NAME": "outer_radius", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.100 },
		{"LABEL": "Pen Offset", "NAME": "pen_offset", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.855 },
		{"LABEL": "Rotation Speed", "NAME": "rotation_speed", "TYPE": "float", "MIN": -100.0, "MAX": 100.0, "DEFAULT": 21.368 },
		{"LABEL": "Color 1", "NAME": "color1", "TYPE": "color", "DEFAULT": [1.0, 0.0, 0.5, 1.0] },
		{"LABEL": "Color 2", "NAME": "color2", "TYPE": "color", "DEFAULT": [0.0, 0.5, 1.0, 1.0] }
	],
	"GENERATORS": [
		{"NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "speed_curve": 1, "link_speed_to_global_bpm": false }}
	]
}*/

const float pi = 3.14159265359;

// Generate spirograph coordinates
vec2 spirographPosition(float t, float R, float r, float p) {
	float x = (R - r) * cos(t) + p * cos((R - r) / r * t);
	float y = (R - r) * sin(t) - p * sin((R - r) / r * t);
	return vec2(x, y);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Normalize point count for the spirographic pattern
	float t = float(pointNumber) / float(pointCount) * 2.0 * pi;

	// Get spirograph parameters
	float R = inner_radius;       // Inner (fixed) circle radius
	float r = outer_radius;       // Rolling (moving) circle radius
	float p = pen_offset;         // Pen offset from the center of the rolling circle

	// Calculate base spirograph position
	vec2 spiroPos = spirographPosition(t, R, r, p);

	// Apply rotation based on time
	float rotation = rotation_time * pi / 180.0;
	mat2 rotMatrix = mat2(cos(rotation), -sin(rotation), sin(rotation), cos(rotation));
	spiroPos = rotMatrix * spiroPos;

	// Scale, position offset, and zoom
	spiroPos = spiroPos * design_scale + vec2(global_x, global_y);

	// Assign position
	pos = spiroPos;

	// Gradient color between two user-defined colors
	float gradientFactor = float(pointNumber) / float(pointCount);
	color = mix(color1, color2, gradientFactor);

	// Assign shape number (unused here)
	shapeNumber = 0;
}