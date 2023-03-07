/*{
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 }, 
		{"LABEL": "Global/Point Count", "NAME": "mat_shape_count", "TYPE": "int", "MIN": 1, "MAX": 40, "DEFAULT": 10 },
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
	    {"LABEL": "Connect/Enable", "NAME": "mat_connect", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Connect/Threshold", "NAME": "mat_connect_threshold", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 0.2 }, 
    ],
    "RENDER_SETTINGS": {
        "POINT_COUNT": 800,
        "SKIP_BLACK": false,
        "MAX_SPEED": 2,
        "ANGLE_OPTIMIZATION": false,
        "PRESERVE_ORDER": true
    }
}*/

#include "auto_all.glsl"

vec2 computePosForShape(int shapeNumber) {
	float normalizedShapeId = (mat_shape_count==1)?0:float(shapeNumber) / (mat_shape_count-1);
	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	return mat_scale * (vec3(0,0,1) * transformMatrix).xy;
}

vec4 computeColorForShape(int shapeNumber) {
	float normalizedShapeId = float(shapeNumber) / (mat_shape_count-1);
	return vec4(getLightValue(normalizedShapeId) * mat_color * getColorValue(normalizedShapeId));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	if (pointNumber >= mat_shape_count) {
		if (mat_connect) {
			int connectTestIdx = (pointNumber-mat_shape_count)/2; // 2 points per connection
			// We change first shape each time we rolled over all shapes with second one
			int firstShapeNumber = connectTestIdx/mat_shape_count;
			int secondShapeNumber = connectTestIdx%mat_shape_count;
			if (firstShapeNumber < mat_shape_count && secondShapeNumber < mat_shape_count && firstShapeNumber<secondShapeNumber) {
				float distance = length(computePosForShape(firstShapeNumber)-computePosForShape(secondShapeNumber));
				if (distance < mat_connect_threshold) {
					shapeNumber = mat_shape_count+connectTestIdx;
					if ((pointNumber-mat_shape_count)%2 != 0) {
						pos = computePosForShape(firstShapeNumber);
						color = computeColorForShape(firstShapeNumber);
					} else {
						pos = computePosForShape(secondShapeNumber);
						color = computeColorForShape(secondShapeNumber);
					}
					// Not nice because fading luminosity with lasers gives crappy colors
					//color *= sqrt(1 - distance / mat_connect_threshold);
				} else {
					shapeNumber = -1; // point will be ignored if shape number is negative
					return;
				}
			} else {
				shapeNumber = -1; // point will be ignored if shape number is negative
				return;
			}
		} else {
		    shapeNumber = -1; // point will be ignored if shape number is negative
		    return;
		}
	} else {
		shapeNumber = pointNumber;
		pos = computePosForShape(shapeNumber);
		color = computeColorForShape(shapeNumber);
	}
}
