/*{
    "CREDIT": "matt.beghin",
    "DESCRIPTION": "Vector version of Shapes material",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon"], "DEFAULT": "Circle" },
        { "LABEL": "Size", "NAME": "size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.8 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Cells/Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 2 },
        { "LABEL": "Cells/Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 2 },
        { "LABEL": "Cells/Translate X", "NAME": "translate_x", "TYPE": "float", "MIN": -0.5, "MAX": 0.5, "DEFAULT": 0 },
        { "LABEL": "Cells/Translate Y", "NAME": "translate_y", "TYPE": "float", "MIN": -0.5, "MAX": 0.5, "DEFAULT": 0 },

        { "LABEL": "Auto Size/Active", "NAME": "animate_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Auto Size/Speed", "NAME": "speed_size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Reverse", "NAME": "reverse_size", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Auto Size/Decay", "NAME": "decay_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Size/Release", "NAME": "release_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Shape", "NAME": "animshape_size", "TYPE": "long", "VALUES": ["Smooth","Out","In","Cut"], "DEFAULT": "Out" },
        { "LABEL": "Auto Size/Offset", "NAME": "offset_size", "TYPE": "float", "MIN": 0.0, "MAX": 16.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Random Off.", "NAME": "random_offset_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },

        { "LABEL": "Auto Luma/Active", "NAME": "animate_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Auto Luma/Speed", "NAME": "speed_luma", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Reverse", "NAME": "reverse_luma", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Auto Luma/Decay", "NAME": "decay_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Luma/Release", "NAME": "release_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Shape", "NAME": "animshape_luma", "TYPE": "long", "VALUES": ["Smooth","Out","In","Cut"], "DEFAULT": "Smooth" },
        { "LABEL": "Auto Luma/Offset", "NAME": "offset_luma", "TYPE": "float", "MIN": 0.0, "MAX": 16.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Repeat", "NAME": "repeat_luma", "TYPE": "float", "MIN": 1.0, "MAX": 32.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Random Off.", "NAME": "random_offset_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Auto Move/Active", "NAME": "auto_move", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Auto Move/Level", "NAME": "level_move", "TYPE": "point2D", "MIN": [0.0,0.0], "MAX": [1.0,1.0], "DEFAULT": [0.0,1.0] },
        { "LABEL": "Auto Move/Speed", "NAME": "speed_move", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Move/Temper", "NAME": "temper_move", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Move/Decay", "NAME": "decay_move", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Move/Release", "NAME": "release_move", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Move/Shape", "NAME": "animshape_move", "TYPE": "long", "VALUES": ["Smooth","Out","In","Noise"], "DEFAULT": "Out" },
        { "LABEL": "Auto Move/Offset", "NAME": "offset_move", "TYPE": "float", "MIN": 0.0, "MAX": 16.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Move/Repeat", "NAME": "repeat_move", "TYPE": "float", "MIN": 1.0, "MAX": 32.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Move/Random Off.", "NAME": "random_offset_move", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },

        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    ],
    "GENERATORS": [
        {"NAME": "animation_time_size", "TYPE": "time_base", "PARAMS": {"speed": "speed_size", "reverse": "reverse_size", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
        {"NAME": "animation_time_luma", "TYPE": "time_base", "PARAMS": {"speed": "speed_luma", "reverse": "reverse_luma", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
        {"NAME": "animation_time_move", "TYPE": "time_base", "PARAMS": {"speed": "speed_move", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 16000
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

// procedural white noise   
float hash( vec2 p ) {
    return fract(sin(dot(p,vec2(127.1,311.7)))*43758.5453);
}

vec2 generateSquarePoint(vec2 center, vec2 size, float posInShape, float nextPointPosInShape)
{
	if (posInShape < 0.25) {
		if (nextPointPosInShape > 0.25) { // Be sure to end on angle point
			return center + vec2((-1+2*1)*size.x,-size.y);
		}
		float posOnEdge = posInShape * 4;
		return center + vec2((-1+2*posOnEdge)*size.x,-size.y);
	}
	if (posInShape < 0.5) {
		if (nextPointPosInShape > 0.5) { // Be sure to end on angle point
			return center + vec2(size.x,(-1+2*1)*size.y);
		}
		float posOnEdge = (posInShape-0.25) * 4;
		return center + vec2(size.x,(-1+2*posOnEdge)*size.y);
	}
	if (posInShape < 0.75) {
		if (nextPointPosInShape > 0.75) { // Be sure to end on angle point
			return center + vec2((1-2*1)*size.x,size.y);
		}
		float posOnEdge = (posInShape-0.5) * 4;
		return center + vec2((1-2*posOnEdge)*size.x,size.y);
	}
	if (nextPointPosInShape < posInShape) { // Be sure to end on angle point
		return center + vec2(-size.x,(1-2*1)*size.y);
	}
	float posOnEdge = (posInShape-0.75) * 4;
	return center + vec2(-size.x,(1-2*posOnEdge)*size.y);
}

vec2 generateTrianglePoint(vec2 center, vec2 size, float posInShape, float nextPointPosInShape)
{
    if (posInShape < 1./3) {
		if (nextPointPosInShape > 1./3) { // Be sure to end on angle point
			return center + vec2(0,size.y);
		}
        return center + mix(vec2(-size.x,-size.y),vec2(0,size.y),posInShape*3);
    } else if (posInShape < 2./3) {
		if (nextPointPosInShape > 2./3) { // Be sure to end on angle point
			return center + vec2(size.x,-size.y);
		}
        return center + mix(vec2(0,size.y),vec2(size.x,-size.y),(posInShape-1./3)*3);
    } else {
		if (nextPointPosInShape < posInShape) { // Be sure to end on angle point
			return center + vec2(-size.x,-size.y);
		}
        return center + mix(vec2(size.x,-size.y),vec2(-size.x,-size.y),(posInShape-2./3)*3);
    }
}

vec2 generateCirclePoint(vec2 center, vec2 size, float posInShape, float nextPointPosInShape)
{
	return center + size * vec2(cos(3.14159*2*posInShape),sin(3.14159*2*posInShape));
}

vec2 generatePolygonPoint(vec2 center, vec2 size, float posInShape, float nextPointPosInShape, int polygonSteps)
{
	int segmentIndex = int(posInShape*polygonSteps);
	int nextPointSegmentIndex = int(nextPointPosInShape*polygonSteps);

    float angle1 = segmentIndex *  2*3.141592654 / polygonSteps;
    vec2 pos1 = vec2(cos(angle1), sin(angle1));
    float angle2 = (segmentIndex+1) *  2*3.141592654 / polygonSteps;
    vec2 pos2 = vec2(cos(angle2), sin(angle2));

	if (segmentIndex != nextPointSegmentIndex) { // Be sure to end on angle point
		return center + size * pos2;
	}

	return center + size * mix(pos1,pos2,fract(posInShape*polygonSteps));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int shapeCount = cells_x * cells_y;
	int pointsPerShape = pointCount / shapeCount;
	shapeNumber = pointNumber / pointsPerShape;
	if (shapeNumber >= shapeCount) {
		shapeNumber = -1;
		return;
	}
	float normalizedPosInShape = float(pointNumber%pointsPerShape) / (pointsPerShape-1);
	float nextPointNormalizedPosInShape = float((pointNumber+1)%pointsPerShape) / (pointsPerShape-1);
	vec2 cellId = vec2(shapeNumber % cells_x, shapeNumber / cells_x);

    float animationSizeValue = 1.;
    if (animate_size) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (random_offset_size)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = animation_time_size;
        if (reverse_size) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - offset_size * normalizedCellId); // Get full value on beat
        if (adjusted_bpm_pos < decay_size) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - decay_size) * 1 / (1 - decay_size);
            if (adjusted_bpm_pos < release_size) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release_size;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

		if (animshape_size == 0) {
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
		} else if (animshape_size == 1) {
            adjusted_bpm_pos = adjusted_bpm_pos;
		} else if (animshape_size == 2) {
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
		} else if (animshape_size == 3) {
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
		}

        animationSizeValue = adjusted_bpm_pos;
    }

    float animationLumaValue = 1.0;
    if (animate_luma) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (random_offset_luma)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = animation_time_luma;
        if (reverse_luma) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - offset_luma * normalizedCellId * repeat_luma); // Get full value on beat
        if (adjusted_bpm_pos < decay_luma) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - decay_luma) * 1 / (1 - decay_luma);
            if (adjusted_bpm_pos < release_luma) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release_luma;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

		if (animshape_luma == 0) {
            adjusted_bpm_pos = (1+cos(adjusted_bpm_pos*2*PI))/2;
		} else if (animshape_luma == 1) {
            adjusted_bpm_pos = adjusted_bpm_pos;
		} else if (animshape_luma == 2) {
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
		} else if (animshape_luma == 3) {
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
		}

        animationLumaValue = adjusted_bpm_pos;
    }

    vec2 animationMoveValue = vec2(0);
    if (auto_move) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (random_offset_move)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

		float animation_time = animation_time_move * (0.5 + temper_move * 0.5 * hash(cellId * 0.5));

        float adjusted_bpm_pos = fract(animation_time - offset_move * normalizedCellId * repeat_move); // Get full value on beat
        if (adjusted_bpm_pos < decay_move) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - decay_move) * 1 / (1 - decay_move);
            if (adjusted_bpm_pos < release_move) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release_move;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

		if (animshape_move == 0) {
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
            animationMoveValue = level_move * 2*adjusted_bpm_pos;
		} else if (animshape_move == 1) {
            adjusted_bpm_pos = adjusted_bpm_pos;
            animationMoveValue = level_move * 2*adjusted_bpm_pos;
		} else if (animshape_move == 2) {
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
            animationMoveValue = level_move * 2*adjusted_bpm_pos;
        } else if (animshape_move == 3) {
            animationMoveValue = level_move * vec2(vnoise(vec2(animation_time,0)),vnoise(vec2(animation_time,11)));
        }
    }

	vec2 cellSize = vec2(2. / cells_x, 2. / cells_y);
	vec2 shapeSize = size * animationSizeValue * cellSize / 2;

	if (shapeSize.x < 0.001 && shapeSize.y < 0.001) {
		shapeNumber = -1;
		return;
	}
	if (animationLumaValue < 0.001) {
		shapeNumber = -1;
		return;
	}

	vec2 shapeCenter = vec2(-1) + cellSize * (cellId+0.5) + animationMoveValue;
	if (shapeCenter.x>1) shapeCenter.x-=2;
	if (shapeCenter.y>1) shapeCenter.y-=2;
	if (shapeCenter.x<-1) shapeCenter.x+=2;
	if (shapeCenter.y<-1) shapeCenter.y+=2;

	shapeCenter += vec2(translate_x,translate_y);

	if (shape == 0) {
		pos = generateSquarePoint(shapeCenter,shapeSize,normalizedPosInShape,nextPointNormalizedPosInShape);
	} else if (shape == 1) {
		pos = generateTrianglePoint(shapeCenter,shapeSize,normalizedPosInShape,nextPointNormalizedPosInShape);
	} else if (shape == 2) {
		pos = generateCirclePoint(shapeCenter,shapeSize,normalizedPosInShape,nextPointNormalizedPosInShape);
	} else if (shape == 3) {
		pos = generatePolygonPoint(shapeCenter,shapeSize,normalizedPosInShape,nextPointNormalizedPosInShape,6);
	}

    color = animationLumaValue * vec4(foregroundColor);
}
