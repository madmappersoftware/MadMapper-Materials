/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader that generates random lines or points with customizable line count, rotation, colors, scale, audio-reactive scattering, and a seed slider for new patterns",
	"TAGS": "random, laser, 2D, customizable, audio-reactive, rotation",
	"VSN": "1.9",
	"INPUTS": [
		{"LABEL": "Scale", "NAME": "design_scale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Position X", "NAME": "global_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Position Y", "NAME": "global_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Enable Rotation", "NAME": "enable_rotation", "TYPE": "bool", "DEFAULT": false },
		{"LABEL": "Rotation Speed", "NAME": "rotation_speed", "TYPE": "float", "MIN": -100.0, "MAX": 100.0, "DEFAULT": 50.0 },
		{"LABEL": "Line Count", "NAME": "line_count", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 12 },
		{"LABEL": "Points Only", "NAME": "points_only", "TYPE": "bool", "DEFAULT": false },
		{"LABEL": "Seed", "NAME": "seed", "TYPE": "float", "MIN": 0.0, "MAX": 1000.0, "DEFAULT": 0.0 },
		{"LABEL": "Color 1", "NAME": "color1", "TYPE": "color", "DEFAULT": [0.0, 1.0, 1.0, 1.0] },
		{"LABEL": "Color 2", "NAME": "color2", "TYPE": "color", "DEFAULT": [1.0, 0.0, 1.0, 1.0] },
		{"LABEL": "Audio Reactivity", "NAME": "audio_reactivity", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{"LABEL": "Audio Input", "NAME": "audio_input", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 5.0 }
	],
	"GENERATORS": [
		{"NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "speed_curve": 1, "link_speed_to_global_bpm": false }}
	]
}*/

const float pi = 3.14159265359;

// Random function for generating values
float random(float x) {
	return fract(sin(x) * 43758.5453123);
}

// Generate random positions with audio scattering
vec2 randomPosition(int seed, float squishFactor, float baseSeed, float audioScatter) {
	float angle = random(float(seed) + baseSeed) * 2.0 * pi; // Full 360-degree randomness
	float radius = (random(float(seed + 1) + baseSeed) * 0.3 + audioScatter) * (1.0 + squishFactor); // Scatter radius dynamically with audio input
	return vec2(cos(angle), sin(angle)) * radius;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Limit total points to user-specified line count
	int maxPoints = line_count; // Line count controlled by slider
	if (pointNumber >= maxPoints) return;

	// Calculate audio scatter factor
	float audioScatter = audio_input * audio_reactivity / 10.0;

	// Audio-based squish factor (controls shape spread)
	float squishFactor = 1.0 - audioScatter;

	// Generate position with seed offset and scatter
	vec2 randPos = randomPosition(pointNumber, squishFactor, seed, audioScatter);

	// Rotation (optional)
	float currentRotation = 0.0;
	if (enable_rotation) {
		currentRotation = rotation_time * pi / 180.0; // Time-based rotation
	}
	mat2 rotMatrix = mat2(
		cos(currentRotation), -sin(currentRotation),
		sin(currentRotation), cos(currentRotation)
	);
	randPos = rotMatrix * randPos;

	// Scale and global position adjustment
	randPos = randPos * design_scale + vec2(global_x, global_y);

	// Final position logic
	if (points_only) {
		// For "Points Only," directly assign the calculated position
		pos = randPos;
	} else {
		// For lines, alternate between the origin (0,0) and the calculated position
		if (pointNumber % 2 == 0) {
			pos = vec2(0.0); // Origin for the start of the line
		} else {
			pos = randPos; // End point of the line
		}
	}

	// Assign color gradient
	float gradientFactor = float(pointNumber) / float(maxPoints - 1);
	color = mix(color1, color2, gradientFactor);

	// Assign a unique shape number
	shapeNumber = 0;
}