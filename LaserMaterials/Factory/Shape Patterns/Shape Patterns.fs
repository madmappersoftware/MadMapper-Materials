/*{
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Shape Patterns with an image as timeline",
    "TAGS": "atmospheric,bpm,beam",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 }, 
		{"LABEL": "Global/Smooth", "NAME": "mat_smooth", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.9 }, 
		{"LABEL": "Global/Shapes", "NAME": "mat_possible_shapes", "TYPE": "long", "DEFAULT": "All", "VALUES": ["All","Circle","Square","Triangle"] },
		{"LABEL": "Global/Min Shapes", "NAME": "mat_min_shapes", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 2 }, 
		{"LABEL": "Global/Max Shapes", "NAME": "mat_max_shapes", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 3 }, 
		{"LABEL": "Global/Draw Beam", "NAME": "mat_draw_beam", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
	    {"LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
	    {"LABEL": "Global/Pattern", "NAME": "mat_pattern", "TYPE": "int", "DEFAULT": 0, "MIN":0, "MAX": 16 },
		{"LABEL": "Auto Move/Speed", "NAME": "mat_translate_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Auto Move/Width", "NAME": "mat_translate_width", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Auto Move/Height", "NAME": "mat_translate_height", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Effects/Auto Rotate", "NAME": "mat_auto_rotate", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" }, 
		{"LABEL": "Effects/Auto Flicker", "NAME": "mat_auto_flicker", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" }, 
		{"LABEL": "Effects/Auto Scale", "NAME": "mat_scale_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
    ],
    "IMPORTED": [
        {"NAME": "patterns", "PATH": "Patterns.png", "GL_TEXTURE_MIN_FILTER": "NEAREST", "GL_TEXTURE_MAG_FILTER": "NEAREST", "GL_TEXTURE_WRAP": "REPEAT"}
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","bpm_sync": "mat_bpmsync", "speed_curve": 2,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": 4000,
       "PRESERVE_ORDER": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

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

vec2 generateTrianglePoint(vec2 center, vec2 size, float posInShape)
{
	if (posInShape < 1./3) {
		float posOnEdge = posInShape * 3;
		return mix(vec2(-size.x,-size.y),vec2(size.x,-size.y),posOnEdge);
	}
	if (posInShape < 2./3) {
		float posOnEdge = (posInShape-1./3) * 3;
		return mix(vec2(size.x,-size.y),vec2(0,size.y),posOnEdge);
	}
	float posOnEdge = (posInShape-2./3) * 3;
	return mix(vec2(0,size.y),vec2(-size.x,-size.y),posOnEdge);
}

vec2 generateCirclePoint(vec2 center, float radius, float posInShape, out float alpha)
{
	// Disable overlap / soft-edge blend at the moment, causes issues in low luminosity
	#define OVERLAP_SIZE 0.0
	float posWithOverlap = posInShape * (1+OVERLAP_SIZE);
	if (posWithOverlap<OVERLAP_SIZE) {
		alpha = posWithOverlap/OVERLAP_SIZE;
	} else if (posWithOverlap>1) {
		alpha = 1-(posWithOverlap-1)/OVERLAP_SIZE;
	} else {
		alpha = 1;
	}
	return center + radius * vec2(cos(3.14159*2*posWithOverlap),sin(3.14159*2*posWithOverlap));
}

vec3 generatePointsPoint(vec2 center, vec2 radius, float posInShape)
{
	int pointNumber = int(posInShape * 20);
	return vec3(center + radius * vec2(cos(3.14159*2*pointNumber/20),sin(3.14159*2*pointNumber/20)), pointNumber);
}

#define PATTERN_SPEED 128

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	if (pointNumber < 16) {
		int patternNumber = pointNumber - 4000;
		float patternValue = IMG_NORM_PIXEL(patterns,vec2(fract(mat_time/PATTERN_SPEED),((mat_pattern+patternNumber)%16)/16.f)).r;
		vec2 lastFrameData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0).rg;
		float outRandomValue = lastFrameData.y;
		if (lastFrameData.x < patternValue) {
			// When pattern value goes from 0 to 1
			outRandomValue = fract(mat_time*123.45);
		}
		userData = vec4(patternValue,outRandomValue,0,0);
	}
	
	int shapeToDraw = int(3 * texelFetch(mm_LastFrameData,ivec2((mat_pattern+0)%16,2),0).y);
	float pattern_scale = mix(1,0.1 + 0.8*texelFetch(mm_LastFrameData,ivec2((mat_pattern+1)%16,2),0).y,mat_scale_level);
	float move = texelFetch(mm_LastFrameData,ivec2((mat_pattern+2)%16,2),0).y;
	float rotate = int(4*texelFetch(mm_LastFrameData,ivec2((mat_pattern+6)%16,2),0).y);
	int shapeCount = mat_min_shapes + int(1.2 * (mat_max_shapes-mat_min_shapes) * texelFetch(mm_LastFrameData,ivec2((mat_pattern+5)%16,2),0).y);
	shapeCount = int(pow(2,shapeCount-1));
	float patternValueForBlackout = IMG_NORM_PIXEL(patterns,vec2(fract(mat_time/PATTERN_SPEED),((mat_pattern+8)%16)/16.)).r;
	float patternValueForCenter = IMG_NORM_PIXEL(patterns,vec2(fract(mat_time/PATTERN_SPEED),((mat_pattern+9)%16)/16.)).r;

	bool ignorePoint = false;

	#define pointsPerShape 250
	int internalShapeNumber = pointNumber / pointsPerShape;
	shapeNumber = internalShapeNumber;

	if (internalShapeNumber >= shapeCount) {
		// ignore point but write position anyway, so fading does just clone the shape
		internalShapeNumber = shapeNumber%shapeCount;
		ignorePoint = true;
	}

	// Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
	float normalizedPosInShape = float(pointNumber%pointsPerShape)/(pointsPerShape-1);

	if (mat_draw_beam) {
		int pointNumber = int(normalizedPosInShape*12);
		shapeNumber = shapeNumber * 40 + pointNumber;
		normalizedPosInShape = pointNumber/12.;
	}

	if (mat_possible_shapes == 1) {
		shapeToDraw = 0;
	} else if (mat_possible_shapes == 2) {
		shapeToDraw = 1;
	} else if (mat_possible_shapes == 3) {
		shapeToDraw = 2;
	}

	float shapePointAlpha = 1;
	if (shapeToDraw == 0) {
		pos = generateCirclePoint(vec2(0),1,normalizedPosInShape,shapePointAlpha);
	} else if (shapeToDraw == 1) {
		pos = generateSquarePoint(vec2(0),vec2(1),normalizedPosInShape);
	} else if (shapeToDraw == 2) {
		pos = generateTrianglePoint(vec2(0),vec2(1),normalizedPosInShape);
	}
	
	if (mat_auto_rotate) {
		float angle = (PI/2)*int(rotate);
		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		//uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
		mat3 rotMat = mat3(cos_factor, sin_factor, 0,
		                   -sin_factor, cos_factor, 0,
		                   0, 0, 1);
		pos = (rotMat * vec3(pos,0)).xy;
	}

	pos = mat_scale * 0.5 * pos * pattern_scale;
	vec2 translation = vec2(mat_translate_width,mat_translate_height) * curlNoise(vec2(move,mat_translate_speed*mat_time/16)).xy/(10 * (0.6+mat_scale));
	if (internalShapeNumber < shapeCount && internalShapeNumber % 2 == 1) {
		translation *= -1;
	}
	// Dump code version, it's late
	if (ignorePoint) {
		shapeNumber = -1;
	} else {
		float shapeDistance = 2. / (shapeCount-1);
		translation *= (1 - (internalShapeNumber/2) * shapeDistance);
	}
	pos += translation;
	color = mat_color * shapePointAlpha;

	if (mat_auto_flicker && patternValueForBlackout>0.5) {
		color = vec4(0);
	}

	vec2 lastPos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	if (lastPos.x > -1 && lastPos.x < 1) {
		pos = mix(lastPos,pos,1-mat_smooth);
		vec4 lastColor = texelFetch(mm_LastFrameData,ivec2(pointNumber,1),0);
		color.rgb = min(color.rgb,mix(lastColor.rgb,color.rgb,1-mat_smooth));
	}

	if (patternValueForCenter>0) {
		pos *= 0.01;
	}

	// if (int(FRAMEINDEX)%3 != 0) {
	// 	color = vec4(0);
	// }
}
