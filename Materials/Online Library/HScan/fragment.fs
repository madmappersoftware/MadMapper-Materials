/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "TAGS": "graphic,bpm",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "mat_animspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.25 },
        { "LABEL": "Thickness", "NAME": "mat_thickness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1, "DEFAULT": 0.5 },
        { "LABEL": "Smoothness", "NAME": "mat_smoothness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1, "DEFAULT": 1.0 },
        { "LABEL": "Dither", "NAME": "mat_dither", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
      ],
    "GENERATORS": [
        {"NAME": "anim_position", "TYPE": "time_base", "PARAMS": {"speed": "mat_animspeed","bpm_sync":true, "speed_curve": 3}},
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

float mat_shape(vec2 texCoord, float size, int seed)
{
	float posX = animatedRand(seed,anim_position);
	float width = animatedRand(seed+12,anim_position) * size;
	return rectangle(repeat(texCoord+vec2(posX,0),vec2(1)), vec2(width*0.5,1));
}

float mat_rand(vec2 co)
{
    float a = 12.9898;
    float b = 78.233;
    float c = 43758.5453;
    float dt= dot(co.xy ,vec2(a,b));
    float sn= mod(dt,3.14);
    return fract(sin(sn) * c);
}

float mat_compute_dither(float luma, vec2 uv)
{
    // Points
    vec2 randIn = uv;
    randIn.x += fract(TIME);
    return mat_rand(randIn) < luma ? 1 : 0;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float shape1 = mat_shape(texCoord,mat_thickness*1,0);
	float shape2 = mat_shape(texCoord,mat_thickness*1,10);
	float shape3 = mat_shape(texCoord,mat_thickness*2,14);
	float value = 0;

	if (shape1 < 0)
		value += min((0 - shape1/(mat_thickness/4) * (1/(0.00001+mat_smoothness))), 1) * 1 / 1;
	if (shape2 < 0)
		value += min((0 - shape2/(mat_thickness/4) * (1/(0.00001+mat_smoothness))), 1) * 1 / 2;
	if (shape3 < 0)
		value += min((0 - shape3/(mat_thickness/2) * (1/(0.00001+mat_smoothness))), 1) * 1 / 7;
		
	if (mat_dither) value = mat_compute_dither(value*value, texCoord);
	
	return vec4(vec3(value),1);
}
