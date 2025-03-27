/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Matt",
	"DESCRIPTION": "describe your material here",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Global/Shape", "NAME": "mat_shape", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Global/Shape Count", "NAME": "mat_shape_count", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 2 }, 
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
	],
	"RENDER_SETTINGS": {
		"POINT_COUNT": 4000,
		"PRESERVE_ORDER": true
	}
}*/

#include "auto_all.glsl"

vec2 generateSquarePoint(vec2 center, vec2 size, float posInShape)
{
	if (posInShape < 0.25) {
		float posOnEdge = posInShape * 4;
		return center + vec2((-1+2*posOnEdge)*size.x,-size.y);
	}
	if (posInShape < 0.5) {
		float posOnEdge = (posInShape-0.25) * 4;
		return center + vec2(size.x,(-1+2*posOnEdge)*size.y);
	}
	if (posInShape < 0.75) {
		float posOnEdge = (posInShape-0.5) * 4;
		return center + vec2((1-2*posOnEdge)*size.x,size.y);
	}
	float posOnEdge = (posInShape-0.75) * 4;
	return center + vec2(-size.x,(1-2*posOnEdge)*size.y);
}

vec2 generateCirclePoint(vec2 center, float radius, float posInShape)
{
	return center + radius * vec2(cos(3.14159*2*posInShape),sin(3.14159*2*posInShape));
}

vec3 generate3DCirclePoint(vec3 center, vec3 radius, float posInShape)
{
	return center + radius * vec3(cos(3.14159*2*posInShape),sin(3.14159*2*posInShape),0);
}

vec2 generateLinePoint(vec2 start, vec2 end, float posInShape)
{
	return mix(start,end,posInShape);
}

vec3 generate3DLinePoint(vec3 start, vec3 end, float posInShape)
{
	return mix(start,end,posInShape);
}

vec3 generate3DCubePoint(vec3 center, vec3 size, float posInShape, out int segmentNumber)
{
	// A Cube will be decomposed in 2 squares (each with 4 lines) & 4 lines
	// We have a total of 12 lines to draw
	int lineNumber = int(posInShape*12);
	if (lineNumber < 4) {
		segmentNumber = 0;
		return vec3(generateSquarePoint(center.xy,size.xy/2,fract(posInShape*3)),center.z-size.z/2);
	}
	if (lineNumber < 8) {
		segmentNumber = 1;
		return vec3(generateSquarePoint(center.xy,size.xy/2,fract(posInShape*3)),center.z+size.z/2);
	}
	if (lineNumber == 8) {
		segmentNumber = 2;
		vec2 pos2D = center.xy-size.xy/2;
		return generate3DLinePoint(vec3(pos2D,center.z-size.z/2), vec3(pos2D,center.z+size.z/2),fract(posInShape*12));
	}
	if (lineNumber == 9) {
		segmentNumber = 3;
		vec2 pos2D = center.xy+vec2(size.x,-size.y)/2;
		return generate3DLinePoint(vec3(pos2D,center.z+size.z/2), vec3(pos2D,center.z-size.z/2),fract(posInShape*12));
	}
	if (lineNumber == 10) {
		segmentNumber = 4;
		vec2 pos2D = center.xy+size.xy/2;
		return generate3DLinePoint(vec3(pos2D,center.z-size.z/2), vec3(pos2D,center.z+size.z/2),fract(posInShape*12));
	}
	if (lineNumber == 11) {
		segmentNumber = 5;
		vec2 pos2D = center.xy+vec2(-size.x,size.y)/2;
		return generate3DLinePoint(vec3(pos2D,center.z+size.z/2), vec3(pos2D,center.z-size.z/2),fract(posInShape*12));
	}
}

vec2 lissajous(float t, float a, float b, float d)
{
	return vec2(sin(a*t+d), sin(b*t));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int pointsPerShape = pointCount / mat_shape_count;
	shapeNumber = pointNumber / pointsPerShape;
	if (shapeNumber >= mat_shape_count) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}
	float normalizedShapeId = float(shapeNumber)/mat_shape_count;

	// Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
	float normalizedPosInShape = float(pointNumber-shapeNumber*pointsPerShape)/(pointsPerShape-1);

	if (mat_shape < 1) {
		vec2 circle = generateCirclePoint(vec2(0),1,normalizedPosInShape);
		vec2 square = generateSquarePoint(vec2(0),vec2(1),normalizedPosInShape);
		pos = mix(circle,square,mat_shape);
	} else if (mat_shape < 2) {
		vec2 square = generateSquarePoint(vec2(0),vec2(1),normalizedPosInShape);
		vec2 line = generateLinePoint(vec2(-1,0.0),vec2(1,0.0),normalizedPosInShape);
		pos = mix(square,line,mat_shape-1);
	} else if (mat_shape < 3) {
		vec2 line = generateLinePoint(vec2(-1,0.0),vec2(1,0.0),normalizedPosInShape);
		vec2 lis = lissajous(normalizedPosInShape*2,PI,6*PI,TIME);
		pos = mix(line,lis,mat_shape-2);
	} else {
		vec2 lis = lissajous(normalizedPosInShape*2,PI,6*PI,TIME);
		vec2 circle = generateCirclePoint(vec2(0),1,normalizedPosInShape);
		pos = mix(lis,circle,mat_shape-3);
	}
	
	pos *= pow(mat_size,2);

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = mat_scale * (vec3(pos,1) * transformMatrix).xy;
	color = vec4(getLightValue(normalizedShapeId) * mat_color * getColorValue(normalizedShapeId));
}
