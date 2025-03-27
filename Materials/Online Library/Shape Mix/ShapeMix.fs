/*{
    "CREDIT": "Matt Beghin",
    "TAGS": "graphic,shape,texture",
	"DESCRIPTION": "Some experiments mixing different shapes. Cool slow texturing.",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Circle"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        { "LABEL": "Mixing", "NAME": "mixing", "TYPE": "long", "VALUES": ["Nice", "Invert","Merge","Max"], "DEFAULT": "Nice", "FLAGS": "generate_as_define" },
        { "LABEL": "Speed", "NAME": "animspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.5 },
        { "LABEL": "Thickness", "NAME": "thickness", "TYPE": "float", "MIN" : 0.0, "MAX" : 0.2, "DEFAULT": 0.035 },
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN" : 0.001, "MAX" : 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Random Size", "NAME": "randomsize", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
      ],
    "GENERATORS": [
        {"NAME": "anim_position", "TYPE": "time_base", "PARAMS": {"speed": "animspeed","bpm_sync":true, "speed_curve": 3}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

// Unperfect but fast hash
int hash( int x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    return x;
}

float animatedRand(int seed, float anim_pos) {
    int randInt1 = hash(seed+int(anim_pos));
    int randInt2 = hash(seed+1+int(anim_pos));
	return mix(randInt1%1001,randInt2%1001,fract(anim_pos)) / 1000.0;
}

float animatedRandCell(vec2 cellId, int seed, float anim_pos) {
	int fullSeed = int(cellId.x*133+cellId.y)+seed*139;
    int rand1 = hash(fullSeed+int(anim_pos));
    int rand2 = hash(fullSeed+1+int(anim_pos));
	return mix(rand1%1001,rand2%1001,fract(anim_pos)) / 1000.0;
}

float mm_mat_shape(vec2 texCoord, float size, float radius, int seed)
{
	vec2 pos = vec2(-0.5+animatedRand(seed,anim_position),-0.5+animatedRand(seed+1,anim_position));
	vec2 cellId;
	vec2 uv = repeat(texCoord-vec2(0.5)+size*pos,vec2(size),cellId);
	float finalRadius = radius;
	if (randomsize) finalRadius *= animatedRandCell(cellId,seed,anim_position);
	#if defined(shape_IS_Square)
		return rectangle(uv, size*finalRadius);
	#else // defined(shape_IS_Circle)
		return circle(uv, size*finalRadius);
	#endif
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float dist1 = mm_mat_shape(texCoord,1,scale*0.4,10);
	float dist2 = mm_mat_shape(texCoord,0.1,scale*0.4,20);
	float size3 = 0.1+0.3*animatedRand(33,anim_position);
	float dist3 = mm_mat_shape(texCoord,size3,scale*0.4,30);

	#if defined(mixing_IS_Nice)
		float dist = dist1*dist2/dist3;
	#elif defined(mixing_IS_Invert)
		float dist = dist1*dist2*dist3;
	#elif defined(mixing_IS_Merge)
		float dist = min(dist1,min(dist2,dist3));
	#else // defined(mixing_IS_Max)
		float dist = max(dist1,max(dist2,dist3));
	#endif
	return vec4(stroke(vec3(0),vec3(1),dist,thickness*thickness),1);
}
