/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader that draws an animated 3D cube with a 4-color gradient, reversible rotation, and manual controls",
	"TAGS": "cube, laser, 3D, gradient, animation",
	"VSN": "1.6",
	"INPUTS": [
		{"LABEL": "Cube Size", "NAME": "cube_size", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Position X", "NAME": "global_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Position Y", "NAME": "global_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Animate Rotation", "NAME": "animate_rotation", "TYPE": "bool", "DEFAULT": true },
		{"LABEL": "Reverse Rotation", "NAME": "reverse_rotation", "TYPE": "bool", "DEFAULT": false },
		{"LABEL": "Rotation Speed", "NAME": "rotation_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Rotation Offset", "NAME": "rotation_offset", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Rotation Shape", "NAME": "rotation_shape", "TYPE": "long", "DEFAULT": "Smooth", "VALUES": ["Smooth", "In", "Out"] },
		{"LABEL": "Manual Rotation X", "NAME": "manual_rotation_x", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Manual Rotation Y", "NAME": "manual_rotation_y", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Manual Rotation Z", "NAME": "manual_rotation_z", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Color 1", "NAME": "color1", "TYPE": "color", "DEFAULT": [1.0, 0.0, 0.0, 1.0] },
		{"LABEL": "Color 2", "NAME": "color2", "TYPE": "color", "DEFAULT": [0.0, 1.0, 0.0, 1.0] },
		{"LABEL": "Color 3", "NAME": "color3", "TYPE": "color", "DEFAULT": [0.0, 0.0, 1.0, 1.0] },
		{"LABEL": "Color 4", "NAME": "color4", "TYPE": "color", "DEFAULT": [1.0, 1.0, 0.0, 1.0] }
	],
	"GENERATORS": [
		{"NAME": "anim_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "speed_curve": 2, "link_speed_to_global_bpm": false }}
	]
}*/

const float pi = 3.14159265359;

// Vertex positions for a cube (8 vertices)
const vec3 cubeVertices[8] = vec3[8](
	vec3(-1.0, -1.0, -1.0), // Bottom-left-back
	vec3( 1.0, -1.0, -1.0), // Bottom-right-back
	vec3( 1.0,  1.0, -1.0), // Top-right-back
	vec3(-1.0,  1.0, -1.0), // Top-left-back
	vec3(-1.0, -1.0,  1.0), // Bottom-left-front
	vec3( 1.0, -1.0,  1.0), // Bottom-right-front
	vec3( 1.0,  1.0,  1.0), // Top-right-front
	vec3(-1.0,  1.0,  1.0)  // Top-left-front
);

// Edge pairs for a cube (12 edges, 24 indices)
const int cubeEdges[24] = int[24](
	0, 1, 1, 2, 2, 3, 3, 0, // Back face
	4, 5, 5, 6, 6, 7, 7, 4, // Front face
	0, 4, 1, 5, 2, 6, 3, 7  // Connecting edges
);

// Function to calculate animation shape
float applyShape(float t, int shape) {
	if (shape == 0) return smoothstep(0.0, 1.0, t);          // Smooth
	else if (shape == 1) return pow(t, 2.0);                // In
	else if (shape == 2) return 1.0 - pow(1.0 - t, 2.0);    // Out
	return t; // Default fallback
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Determine which edge and point within the edge we are drawing
	int edgeIndex = pointNumber / 2; // Each edge has 2 points
	int edgePoint = pointNumber % 2; // First or second point of the edge

	// Ensure we don't draw any spurious lines
	if (edgeIndex >= 12) {
		pos = vec2(0.0);
		color = vec4(0.0);
		shapeNumber = -1;
		return;
	}

	// Get vertex indices for the edge
	int vertexIndex = cubeEdges[edgeIndex * 2 + edgePoint];

	// Retrieve the corresponding vertex
	vec3 vertex = cubeVertices[vertexIndex] * cube_size;

	// Handle animated rotation
	float animProgress = fract(anim_time + rotation_offset / 360.0); // Cyclical progress
	float animFactor = applyShape(animProgress, int(rotation_shape));
	if (reverse_rotation) animFactor = 1.0 - animFactor; // Reverse rotation direction

	// Determine rotation angles
	float animatedRotationX = animFactor * 360.0;
	float animatedRotationY = animFactor * 360.0;
	float animatedRotationZ = animFactor * 360.0;

	// Combine animation and manual rotations
	float radX = (animate_rotation ? animatedRotationX : manual_rotation_x) * pi / 180.0;
	float radY = (animate_rotation ? animatedRotationY : manual_rotation_y) * pi / 180.0;
	float radZ = (animate_rotation ? animatedRotationZ : manual_rotation_z) * pi / 180.0;

	// Rotation matrices
	mat3 rotX = mat3(
		1.0, 0.0, 0.0,
		0.0, cos(radX), -sin(radX),
		0.0, sin(radX), cos(radX)
	);

	mat3 rotY = mat3(
		cos(radY), 0.0, sin(radY),
		0.0, 1.0, 0.0,
		-sin(radY), 0.0, cos(radY)
	);

	mat3 rotZ = mat3(
		cos(radZ), -sin(radZ), 0.0,
		sin(radZ), cos(radZ), 0.0,
		0.0, 0.0, 1.0
	);

	// Combine rotations and apply to vertex
	vertex = rotZ * rotY * rotX * vertex;

	// Apply global position offset
	vertex.xy += vec2(global_x, global_y);

	// Project the 3D vertex to 2D space
	pos = vertex.xy;

	// Assign a 4-color gradient based on edge index
	float gradientFactor = float(edgeIndex) / 12.0; // 0 to 1 across all 12 edges
	if (gradientFactor < 0.333)
		color = mix(color1, color2, gradientFactor * 3.0);
	else if (gradientFactor < 0.666)
		color = mix(color2, color3, (gradientFactor - 0.333) * 3.0);
	else
		color = mix(color3, color4, (gradientFactor - 0.666) * 3.0);

	// Assign the shape number (useful for edge identification)
	shapeNumber = edgeIndex;
}