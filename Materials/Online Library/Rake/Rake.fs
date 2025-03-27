/*{
    "CREDIT": "Mad Team",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "animspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.5 },

		{ "LABEL": "Width", "NAME": "size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 4 },
        { "LABEL": "Thickness", "NAME": "thickness", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0.1 },
        { "LABEL": "Base Rotation", "NAME": "base_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 360, "DEFAULT": 0 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false },

        { "LABEL": "Effects/Squirl", "NAME": "squirl", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },
        { "LABEL": "Effects/Move", "NAME": "mat_move", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },
        { "LABEL": "Effects/Rotate", "NAME": "mat_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.0 },

		{ "LABEL": "Cells/Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 16, "DEFAULT": 1 },
        { "LABEL": "Cells/Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 16, "DEFAULT": 1 },

        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
    ],
    "GENERATORS": [
        {"NAME": "anim_position", "TYPE": "time_base", "PARAMS": {"speed": "animspeed","bpm_sync": "bpmsync", "speed_curve": 3}},
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

// A single iteration of Bob Jenkins' One-At-A-Time hashing algorithm.
int hash( int x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    x ^= ( x >> 11u );
    x += ( x << 15u );
    return x;
}

float animatedRand(int seed, float anim_pos) {
    int randInt1 = hash(seed+int(anim_pos));
    int randInt2 = hash(seed+1+int(anim_pos));
	return mix(randInt1,randInt2,fract(anim_pos));
}

float animatedRandCell(vec2 cellId, int seed, float anim_pos) {
	int fullSeed = int(cellId.x*133+cellId.y)+seed*139;
    float rand1 = (hash(fullSeed+int(anim_pos))%1001) / 1000.0;
    float rand2 = (hash(fullSeed+1+int(anim_pos))%1001) / 1000.0;
	return mix(rand1,rand2,fract(anim_pos));
}

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 cellId;
    vec2 p          = texCoord;
    vec2 cellSize   = vec2( 1.0 / cells_x, 1.0 / cells_y );
    p               = repeat( p, cellSize, cellId );

    vec2 radius = cellSize * 0.5;
    float luma = 1.0;

    radius *= size;

	p = rotate(p, base_rotation*PI/180 + PI/2);
	p.y += squirl*cos(abs(p.x*cells_y)*PI)*(animatedRandCell(cellId,4,anim_position*4)-0.5);
	p = translate(p, mat_move * vec2(0,(sin(anim_position*2*PI+cellId.x*133+cellId.y))/(cells_y*2)));
	p = rotate(p, mat_rotate * animatedRandCell(cellId,3,anim_position) * 4 * PI);
	
	float dist = rectangle( p, vec2(radius.x,thickness*thickness) );

    vec4 backColor = vec4(backgroundColor.rgb * brightness, 1);
    vec4 frontColor = vec4(foregroundColor.rgb * brightness, 1);

    vec4 col = luma * fill( backColor, frontColor, dist );

    col.a = 1.0;
    return col;
}
