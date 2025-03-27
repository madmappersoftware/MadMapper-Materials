/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Cornelius // ProjectileObjects",
	"DESCRIPTION": "A laser shader that draws a 3D cube",
	"TAGS": "cube, laser, 3D",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Cube Size", "NAME": "cube_size", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Rotation X", "NAME": "rotation_x", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Rotation Y", "NAME": "rotation_y", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Rotation Z", "NAME": "rotation_z", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Color", "NAME": "line_color", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] }
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

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
	// Determine which edge and point within the edge we are drawing
	int edgeIndex = pointNumber / 2; // Each edge has 2 points
	int edgePoint = pointNumber % 2; // First or second point of the edge

	// Get vertex indices for the edge
	int vertexIndex = cubeEdges[edgeIndex * 2 + edgePoint];

	// Retrieve the corresponding vertex
	vec3 vertex = cubeVertices[vertexIndex] * cube_size;

	// Apply rotation to the vertex
	float radX = rotation_x * pi / 180.0;
	float radY = rotation_y * pi / 180.0;
	float radZ = rotation_z * pi / 180.0;

	// Rotation around X-axis
	mat3 rotX = mat3(
		1.0, 0.0, 0.0,
		0.0, cos(radX), -sin(radX),
		0.0, sin(radX), cos(radX)
	);

	// Rotation around Y-axis
	mat3 rotY = mat3(
		cos(radY), 0.0, sin(radY),
		0.0, 1.0, 0.0,
		-sin(radY), 0.0, cos(radY)
	);

	// Rotation around Z-axis
	mat3 rotZ = mat3(
		cos(radZ), -sin(radZ), 0.0,
		sin(radZ), cos(radZ), 0.0,
		0.0, 0.0, 1.0
	);

	// Combine rotations and apply to vertex
	vertex = rotZ * rotY * rotX * vertex;

	// Project the 3D vertex to 2D space
	pos = vertex.xy;

	// Set the color of the edge
	color = line_color;

	// Assign the shape number for identification (e.g., edges of the cube)
	shapeNumber = edgeIndex;
}