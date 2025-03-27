/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Matt",
	"DESCRIPTION": "describe your material here",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Global/Cube", "NAME": "mat_cube", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Global/Line", "NAME": "mat_line", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Global/Lissajous", "NAME": "mat_lissajous", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Global/Shape Count", "NAME": "mat_shape_count", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 2}, 
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
	],
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

vec3 generate3DCirclePoint(vec3 center, vec3 radius, float posInShape)
{
	return center + radius * vec3(cos(3.14159*2*posInShape),sin(3.14159*2*posInShape),0);
}

vec3 generate3DLinePoint(vec3 start, vec3 end, float posInShape)
{
	return mix(start,end,posInShape);
}

vec3 generate3DCubePoint(vec3 center, vec3 size, float posInShape, out int segmentNumber)
{
	// A Cube will be decomposed in 2 squares (each with 4 lines) & 4 lines
	// We have a total of 12 lines to draw
	int lineNumber = int(posInShape*12-0.00001);
	if (lineNumber < 4) {
		segmentNumber = 0;
		return vec3(generateSquarePoint(center.xy,size.xy/2,fract(posInShape*3)),center.z-size.z/2);
	}
	else if (lineNumber < 8) {
		segmentNumber = 1;
		return vec3(generateSquarePoint(center.xy,size.xy/2,fract(posInShape*3)),center.z+size.z/2);
	}
	else if (lineNumber == 8) {
		segmentNumber = 2;
		vec2 pos2D = center.xy-size.xy/2;
		return generate3DLinePoint(vec3(pos2D,center.z-size.z/2), vec3(pos2D,center.z+size.z/2),fract(posInShape*12));
	}
	else if (lineNumber == 9) {
		segmentNumber = 3;
		vec2 pos2D = center.xy+vec2(size.x,-size.y)/2;
		return generate3DLinePoint(vec3(pos2D,center.z+size.z/2), vec3(pos2D,center.z-size.z/2),fract(posInShape*12));
	}
	else if (lineNumber == 10) {
		segmentNumber = 4;
		vec2 pos2D = center.xy+size.xy/2;
		return generate3DLinePoint(vec3(pos2D,center.z-size.z/2), vec3(pos2D,center.z+size.z/2),fract(posInShape*12));
	}
	else if (lineNumber == 11) {
		segmentNumber = 5;
		vec2 pos2D = center.xy+vec2(-size.x,size.y)/2;
		return generate3DLinePoint(vec3(pos2D,center.z+size.z/2), vec3(pos2D,center.z-size.z/2),fract(posInShape*12));
	} else {
		segmentNumber = -1;
		return vec3(0);
	}
}

vec3 lissajous3d(float t, float a, float b, float c, float d, float e)
{
	return vec3(sin(a*t), sin(b*t+d), sin(c*t+e));
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

	vec3 circle3DPos3D = generate3DCirclePoint(vec3(0),vec3(1,1,0)*0.49*mat_size,normalizedPosInShape);

	int cubeSegmentNumber = 0;
	vec3 pos3D = circle3DPos3D;
	if (mat_cube > 0) {
		vec3 cube3DPos3D = generate3DCubePoint(vec3(0),vec3(1.6)*0.49*mat_size,normalizedPosInShape,cubeSegmentNumber);
		pos3D = mix(pos3D,cube3DPos3D,mat_cube);
	}
	if (mat_line > 0) {
		vec3 line3DPos3D = generate3DLinePoint(vec3(-0.49*mat_size,0,0),vec3(0.49*mat_size,0,0),normalizedPosInShape);
		pos3D = mix(pos3D,line3DPos3D,mat_line);
	}
	if (mat_lissajous > 0) {
		vec3 lissajou3DPos3D = lissajous3d(normalizedPosInShape*2,PI,3*PI,6*PI,0,0)*mat_size;
		pos3D = mix(pos3D,lissajou3DPos3D,mat_lissajous);
	}

	vec4 pos4D = vec4(pos3D,1) * make3dTransformMatrix(normalizedShapeId);
	pos3D = pos4D.xyz / pos4D.w;

	pos = mat_scale * pos3D.xy * (2-pos3D.z);
	color = vec4(getLightValue(normalizedShapeId) * mat_color * getColorValue(normalizedShapeId));
	shapeNumber = shapeNumber*100 + cubeSegmentNumber;
}
