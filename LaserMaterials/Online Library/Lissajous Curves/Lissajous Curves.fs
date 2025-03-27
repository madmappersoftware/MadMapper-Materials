/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader drawing Lissajous curves with animateable color, tracing speed, and wiggling lines.",
	"TAGS": "lissajous, laser, generative, customizable, animation",
	"VSN": "1.2",
	"INPUTS": [
		{"LABEL": "Scale (Zoom)",       "NAME": "design_scale", "TYPE": "float", "MIN":0.1, "MAX":2.0, "DEFAULT":1.0 },
		{"LABEL": "Position X",         "NAME": "global_x",     "TYPE": "float", "MIN":-1.0,"MAX":1.0,"DEFAULT":0.0 },
		{"LABEL": "Position Y",         "NAME": "global_y",     "TYPE": "float", "MIN":-1.0,"MAX":1.0,"DEFAULT":0.0 },

		{"LABEL": "Freq X (a)",         "NAME": "freq_x",       "TYPE": "float", "MIN":1.0,"MAX":10.0,"DEFAULT":3.0 },
		{"LABEL": "Freq Y (b)",         "NAME": "freq_y",       "TYPE": "float", "MIN":1.0,"MAX":10.0,"DEFAULT":2.0 },
		{"LABEL": "Phase (δ)",          "NAME": "phase",        "TYPE": "float", "MIN":0.0,"MAX":3.1415927,"DEFAULT":1.0 },

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

// Lissajous parametric equations:
// x(t) = sin(a * t + δ)
// y(t) = sin(b * t)
vec2 lissajous(float t, float a, float b, float delta) {
	float x = sin(a * t + delta);
	float y = sin(b * t);
	return vec2(x, y);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Time from generator
	float currentTime = time_base; 

	// If animate_trace is enabled, shift trace_offset over time
	float effectiveTraceOffset = trace_offset;
	if (animate_trace) {
		effectiveTraceOffset = fract(trace_offset + currentTime * trace_speed);
	}

	// Fraction along the curve
	float pointFrac = float(pointNumber) / float(pointCount - 1);

	// Apply trace offset and length logic
	float sectionPos = fract(pointFrac - effectiveTraceOffset);

	// If outside the trace window, skip
	if (sectionPos > trace_length) {
		pos = vec2(0.0);
		color = vec4(0.0);
		shapeNumber = 0;
		return;
	}

	// Fade tail logic
	float alpha = 1.0;
	if (fade_tail > 0.0) {
		float tailStart = trace_length * (1.0 - fade_tail);
		if (sectionPos > tailStart) {
			float tailFrac = (sectionPos - tailStart) / (trace_length - tailStart);
			alpha = 1.0 - tailFrac;
		}
	}

	// Compute Lissajous position
	float t = pointFrac * 2.0 * pi;
	float a = freq_x;
	float b = freq_y;
	float delta = phase;

	vec2 lPos = lissajous(t, a, b, delta);

	// Wiggle lines if enabled
	if (wiggle) {
		// Add a small sinusoidal variation based on time and pointFrac
		float wiggleX = sin((currentTime + pointFrac * 10.0) * 2.0 * pi) * wiggle_amount;
		float wiggleY = cos((currentTime * 0.5 + pointFrac * 7.0) * 2.0 * pi) * wiggle_amount;
		lPos += vec2(wiggleX, wiggleY);
	}

	// Scale and position adjustments
	lPos = lPos * design_scale + vec2(global_x, global_y);

	// Color gradient
	float gradientFactor = pointFrac;

	// If animate_color is enabled, shift colors over time
	vec4 baseColor1 = color1;
	vec4 baseColor2 = color2;
	if (animate_color) {
		// Shift hue over time (simple approach: rotate gradientFactor)
		float hueShift = fract(currentTime * color_anim_speed);
		gradientFactor = fract(gradientFactor + hueShift);
	}

	vec4 baseColor = mix(baseColor1, baseColor2, gradientFactor);
	baseColor.a *= alpha;

	// Assign outputs
	pos = lPos;
	color = baseColor;
	shapeNumber = 0;
}