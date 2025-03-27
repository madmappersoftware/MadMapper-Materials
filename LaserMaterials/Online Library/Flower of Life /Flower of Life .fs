/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Ctrl-Z",
	"DESCRIPTION": "Flower of Life",
	"TAGS": "sacred geometry, flower of life, laser",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Global/Circle Radius", "NAME": "mat_circle_radius", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.01, "MAX": 0.5 },
		{"LABEL": "Global/Circle Count", "NAME": "mat_circle_count", "TYPE": "int", "DEFAULT": 7, "MIN": 1, "MAX": 37 },
		{"LABEL": "Global/XY", "NAME": "mat_xyPos", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the center position of your visual"}, 
		{"LABEL": "Global/Overlap", "NAME": "mat_overlap_factor", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 2.0 },
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] },
		{"LABEL": "Noise/Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Noise/Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
		{"LABEL": "Noise/Speed", "NAME": "mat_noisespeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }
	],
	"GENERATORS": [
		{"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noisespeed", "speed_curve":2, "bpm_sync": false, "link_speed_to_global_bpm":true}},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 4000
	}
}*/

#include "auto_all.glsl"

// Function to calculate circle position based on the hexagonal pattern
vec2 calculateCirclePosition(int shapeNumber, float radius, float overlapFactor) {
    if (shapeNumber == 0) {
        return vec2(0.0, 0.0); // Center circle position
    }
    
    int layer = 1;
    int layerStartIndex = 1;

    // Calculate the layer in which the current circle resides
    while (shapeNumber >= layerStartIndex + 6 * layer) {
        layerStartIndex += 6 * layer;
        layer++;
    }

    int indexInLayer = shapeNumber - layerStartIndex;
    float angle = float(indexInLayer) / float(layer * 6) * 2.0 * PI;

    // Adjust the distance from the center based on the overlap factor
    float distanceFromCenter = radius * layer * sqrt(3.0) * overlapFactor;

    return vec2(cos(angle), sin(angle)) * distanceFromCenter;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    int totalShapes = mat_circle_count + 1; // Adding 1 for the outer circle
    int pointsPerShape = pointCount / totalShapes;
    shapeNumber = pointNumber / pointsPerShape;

    if (shapeNumber >= totalShapes) {
        shapeNumber = -1; // point will be ignored if shape number is negative
        return;
    }

    float normalizedPosInShape = float(pointNumber - shapeNumber * pointsPerShape) / (pointsPerShape - 1);

    if (shapeNumber < mat_circle_count) {
        // Calculate base position of the inner circles in the grid with overlap control
        vec2 basePos = calculateCirclePosition(shapeNumber, mat_circle_radius, mat_overlap_factor);

        // Calculate the position of the current point along the circle's circumference
        float angle = normalizedPosInShape * 2.0 * PI;
        pos = basePos + mat_circle_radius * vec2(cos(angle), sin(angle));
    } else {
        // Calculate the outer circle's radius
        float outerRadius = mat_circle_radius * mat_overlap_factor * (1.0 + sqrt(12.0) * sqrt(mat_circle_count*0.1));
        
        // Calculate the position of the outer circle's points
        float angle = normalizedPosInShape * 2.0 * PI;
        pos = outerRadius * vec2(cos(angle), sin(angle));
    }

    // Apply auto-rotate and auto-move using functions from auto_all.glsl
    mat3 transformMatrix = makeTransformMatrix(normalizedPosInShape);
    pos = (transformMatrix * vec3(pos, 1.0)).xy;

    // Apply auto-move if active
    if (mat_automoveactive) {
        float moveX = mat_automovesize * cos(mat_move_position);
        float moveY = mat_automovesize * sin(mat_move_position);
        pos += vec2(moveX, moveY);
    }

    // Translate the entire pattern to the center specified by mat_center_x and mat_center_y
    pos += vec2(mat_xyPos[0], mat_xyPos[1]);

    // Apply noise if enabled
    if (mat_noisepower > 0) {
        pos += mat_noisepower * noise(mat_noisescale * pos + vec2(0, mat_noise_time));
    }

    // Set the color
    color = vec4(getLightValue(normalizedPosInShape) * mat_color * getColorValue(normalizedPosInShape));
}