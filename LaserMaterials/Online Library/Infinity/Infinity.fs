/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader that renders an infinity symbol with customizable rotation, colors, scale, and audio-reactive squishing, now with streamlined rotation control",
	"TAGS": "infinity, laser, 2D, customizable, audio-reactive, rotation",
	"VSN": "1.4",
	"INPUTS": [
		{"LABEL": "Scale", "NAME": "symbol_scale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Position X", "NAME": "global_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Position Y", "NAME": "global_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Enable Rotation", "NAME": "enable_rotation", "TYPE": "bool", "DEFAULT": false },
		{"LABEL": "Rotation Speed", "NAME": "rotation_speed", "TYPE": "float", "MIN": -100.0, "MAX": 100.0, "DEFAULT": 50.0 },
		{"LABEL": "Color 1", "NAME": "color1", "TYPE": "color", "DEFAULT": [0.0, 1.0, 1.0, 1.0] },
		{"LABEL": "Color 2", "NAME": "color2", "TYPE": "color", "DEFAULT": [1.0, 0.0, 1.0, 1.0] },
		{"LABEL": "Audio Reactivity", "NAME": "audio_reactivity", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.0 },
		{"LABEL": "Audio Input", "NAME": "audio_input", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 5.0 }
	],
	"GENERATORS": [
		{"NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "speed_curve": 1, "link_speed_to_global_bpm": false }}
	]
}*/

const float pi = 3.14159265359;

// Parametric equations for an infinity symbol
vec2 infinitySymbol(float t, float squishFactor) {
	float x = sin(t);
	float y = sin(t) * cos(t) * squishFactor; // Apply squishing effect
	return vec2(x, y);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Normalize the point number to the range [0, 1] for parametric t
	float t = float(pointNumber) / float(pointCount - 1) * 2.0 * pi;

	// Compute squish factor based on audio input
	float squishFactor = 1.0 - (audio_input * audio_reactivity / 10.0); // Squishes dynamically based on audio

	// Calculate the infinity symbol's position with squish effect
	vec2 symbolPos = infinitySymbol(t, squishFactor) * symbol_scale;

	// Handle rotation
	float currentRotation = 0.0;
	if (enable_rotation) {
		currentRotation = rotation_time * pi / 180.0; // Rotation controlled by time
	}

	// Apply rotation to the infinity symbol
	mat2 rotMatrix = mat2(
		cos(currentRotation), -sin(currentRotation),
		sin(currentRotation), cos(currentRotation)
	);
	symbolPos = rotMatrix * symbolPos;

	// Add global position offset
	symbolPos += vec2(global_x, global_y);

	// Set the final position
	pos = symbolPos;

	// Assign a gradient color based on the point's normalized position
	float gradientFactor = float(pointNumber) / float(pointCount - 1);
	color = mix(color1, color2, gradientFactor);

	// Assign a unique shape number for the infinity symbol
	shapeNumber = 0;
}