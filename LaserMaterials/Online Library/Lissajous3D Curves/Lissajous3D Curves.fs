/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader drawing a 3D Lissajous curve with tracing, fading, color animation, wiggling, and projection into 2D",
	"TAGS": "lissajous, laser, generative, customizable, 3D",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Scale (Zoom)",       "NAME": "design_scale", "TYPE": "float", "MIN":0.1, "MAX":2.0, "DEFAULT":1.0 },
		{"LABEL": "Position X",         "NAME": "global_x",     "TYPE": "float", "MIN":-1.0,"MAX":1.0,"DEFAULT":0.0 },
		{"LABEL": "Position Y",         "NAME": "global_y",     "TYPE": "float", "MIN":-1.0,"MAX":1.0,"DEFAULT":0.0 },
		{"LABEL": "Rotate X (3D)",      "NAME":"rotate_x","TYPE":"float","MIN":0.0,"MAX":360.0,"DEFAULT":0.0 },
		{"LABEL": "Rotate Y (3D)",      "NAME":"rotate_y","TYPE":"float","MIN":0.0,"MAX":360.0,"DEFAULT":0.0 },

		{"LABEL": "Freq X (a)",         "NAME": "freq_x",       "TYPE": "float", "MIN":1.0,"MAX":10.0,"DEFAULT":3.0 },
		{"LABEL": "Freq Y (b)",         "NAME": "freq_y",       "TYPE": "float", "MIN":1.0,"MAX":10.0,"DEFAULT":2.0 },
		{"LABEL": "Phase X (δx)",       "NAME": "phase_x",      "TYPE": "float", "MIN":0.0,"MAX":3.1415927,"DEFAULT":1.0 },
		{"LABEL": "Freq Z (c)",         "NAME": "freq_z",       "TYPE": "float", "MIN":1.0,"MAX":10.0,"DEFAULT":4.0 },
		{"LABEL": "Phase Z (δz)",       "NAME": "phase_z",      "TYPE": "float", "MIN":0.0,"MAX":3.1415927,"DEFAULT":0.5 },

		{"LABEL": "Trace Offset",       "NAME": "trace_offset", "TYPE": "float", "MIN":0.0,"MAX":1.0,"DEFAULT":0.0 },
		{"LABEL": "Trace Length",       "NAME": "trace_length", "TYPE": "float", "MIN":0.0,"MAX":1.0,"DEFAULT":1.0 },
		{"LABEL": "Fade Tail",          "NAME": "fade_tail",    "TYPE": "float", "MIN":0.0,"MAX":1.0,"DEFAULT":0.0 },

		{"LABEL": "Color 1",            "NAME":"color1",        "TYPE":"color","DEFAULT":[1.0,0.0,0.0,1.0] },
		{"LABEL": "Color 2",            "NAME":"color2",        "TYPE":"color","DEFAULT":[0.0,0.0,1.0,1.0] },

		{"LABEL": "Animate Color",      "NAME":"animate_color", "TYPE":"bool", "DEFAULT":false },
		{"LABEL": "Color Anim Speed",   "NAME":"color_anim_speed","TYPE":"float","MIN":0.0,"MAX":1.0,"DEFAULT":0.2 },

		{"LABEL": "Animate Trace",      "NAME":"animate_trace","TYPE":"bool","DEFAULT":false },
		{"LABEL": "Trace Speed",        "NAME":"trace_speed","TYPE":"float","MIN":0.0,"MAX":1.0,"DEFAULT":0.1 },

		{"LABEL": "Wiggle Lines",       "NAME":"wiggle","TYPE":"bool","DEFAULT":false },
		{"LABEL": "Wiggle Amount",      "NAME":"wiggle_amount","TYPE":"float","MIN":0.0,"MAX":0.2,"DEFAULT":0.05 }
	],
	"GENERATORS": [
		{"NAME": "time_base", "TYPE": "time_base", "PARAMS": {"speed": 1.0, "speed_curve": 1, "link_speed_to_global_bpm": false}}
	]
}*/

const float pi = 3.14159265359;

// 3D Lissajous:
// x(t) = sin(a*t + δx)
// y(t) = sin(b*t)
// z(t) = sin(c*t + δz)
vec3 lissajous3D(float t, float a, float b, float c, float deltaX, float deltaZ) {
	float x = sin(a * t + deltaX);
	float y = sin(b * t);
	float z = sin(c * t + deltaZ);
	return vec3(x, y, z);
}

// 3D rotation matrices
mat3 rotateX(float angle) {
	float c = cos(angle);
	float s = sin(angle);
	return mat3(
		1.0, 0.0, 0.0,
		0.0, c, -s,
		0.0, s, c
	);
}

mat3 rotateY(float angle) {
	float c = cos(angle);
	float s = sin(angle);
	return mat3(
		c, 0.0, s,
		0.0, 1.0, 0.0,
		-s, 0.0, c
	);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	float currentTime = time_base;

	// Animate trace offset if enabled
	float effectiveTraceOffset = trace_offset;
	if (animate_trace) {
		effectiveTraceOffset = fract(trace_offset + currentTime * trace_speed);
	}

	// Fraction along the curve
	float pointFrac = float(pointNumber) / float(pointCount - 1);
	float sectionPos = fract(pointFrac - effectiveTraceOffset);

	// Check trace window
	if (sectionPos > trace_length) {
		pos = vec2(0.0);
		color = vec4(0.0);
		shapeNumber = 0;
		return;
	}

	// Fade tail
	float alpha = 1.0;
	if (fade_tail > 0.0) {
		float tailStart = trace_length * (1.0 - fade_tail);
		if (sectionPos > tailStart) {
			float tailFrac = (sectionPos - tailStart) / (trace_length - tailStart);
			alpha = 1.0 - tailFrac;
		}
	}

	// Compute 3D Lissajous position
	float t = pointFrac * 2.0 * pi;
	float a = freq_x;
	float b = freq_y;
	float c = freq_z;
	float deltaX = phase_x;
	float deltaZ = phase_z;

	vec3 lPos3D = lissajous3D(t, a, b, c, deltaX, deltaZ);

	// Wiggle
	if (wiggle) {
		float wiggleX = sin((currentTime + pointFrac * 10.0) * 2.0 * pi) * wiggle_amount;
		float wiggleY = cos((currentTime * 0.5 + pointFrac * 7.0) * 2.0 * pi) * wiggle_amount;
		float wiggleZ = sin((currentTime * 0.3 + pointFrac * 5.0) * 2.0 * pi) * wiggle_amount;
		lPos3D += vec3(wiggleX, wiggleY, wiggleZ);
	}

	// 3D Rotation
	float rotXAngle = radians(rotate_x);
	float rotYAngle = radians(rotate_y);
	mat3 rotXMat = rotateX(rotXAngle);
	mat3 rotYMat = rotateY(rotYAngle);
	lPos3D = rotYMat * (rotXMat * lPos3D);

	// Scale and position adjustments
	lPos3D.xy *= design_scale;
	lPos3D.x += global_x;
	lPos3D.y += global_y;

	// Project 3D -> 2D (simple orthographic projection: ignore Z)
	vec2 finalPos = lPos3D.xy;

	// Animate color if enabled
	float gradientFactor = pointFrac;
	if (animate_color) {
		float hueShift = fract(currentTime * color_anim_speed);
		gradientFactor = fract(gradientFactor + hueShift);
	}

	vec4 baseColor = mix(color1, color2, gradientFactor);
	baseColor.a *= alpha;

	pos = finalPos;
	color = baseColor;
	shapeNumber = 0;
}