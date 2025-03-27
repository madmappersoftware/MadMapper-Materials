/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Matt",
	"DESCRIPTION": "Beam group animated with an image as timeline",
	"TAGS": "atmospheric,bpm,beam",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Global/Smooth", "NAME": "mat_smooth", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 }, 
		{"LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Global/Pattern", "NAME": "mat_pattern", "TYPE": "int", "DEFAULT": 0, "MIN":0, "MAX": 15 },

		{"LABEL": "Vertical Move/Speed", "NAME": "mat_v_move_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Vertical Move/Size", "NAME": "mat_v_move_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 
		{"LABEL": "Vertical Move/Shape", "NAME": "mat_v_move_shape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "Smooth" },
		{"LABEL": "Vertical Move/Offset", "NAME": "mat_v_move_offset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },

		{"LABEL": "Color/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] },
	],
	"IMPORTED": [
		{"NAME": "patterns", "PATH": "Patterns.png", "GL_TEXTURE_MIN_FILTER": "NEAREST", "GL_TEXTURE_MAG_FILTER": "NEAREST", "GL_TEXTURE_WRAP": "REPEAT"}
	],
	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","bpm_sync": "mat_bpmsync", "speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_v_move_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_v_move_speed","bpm_sync": "mat_bpmsync", "speed_curve": 3,"link_speed_to_global_bpm":true}},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 16
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

#define POINT_COUNT 8
#define PATTERN_SPEED 128

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int patternNumber = pointNumber;

	float patternValue = IMG_NORM_PIXEL(patterns,vec2(fract(mat_time/PATTERN_SPEED),((mat_pattern+patternNumber)%16)/16.f)).r;
	vec4 lastFrameData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);

	// R = pattern value
	// G = new random value each time we reach a pattern on pixel
	// B = value increased bby 1/255.f each time we reach a pattern on pixel
	// A = 1 when we reached a new 
	float randomValue;
	float increasingValue;
	float switchedOnValue;
	if (lastFrameData.r < patternValue) {
		// When pattern value goes from 0 to 1
		randomValue = fract(mat_time*123.45);
		increasingValue = fract(lastFrameData.b+1/255.);
		switchedOnValue = 1;
	} else {
		randomValue = lastFrameData.g; // Don't change
		increasingValue = lastFrameData.b; // Don't change
		switchedOnValue = 0;
	}

	userData = vec4(patternValue,randomValue,increasingValue,switchedOnValue);
	
	if (pointNumber >= POINT_COUNT) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float animTime = mat_time/8;

	float random1 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+0)%16,2),0).g;
	float random2 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+1)%16,2),0).g;
	float random3 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+2)%16,2),0).g;
	float random4 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+3)%16,2),0).g;
	float random5 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+4)%16,2),0).g;
	float value5Now = IMG_NORM_PIXEL(patterns,vec2(fract(mat_time/PATTERN_SPEED),((mat_pattern+4)%16)/16.f)).r;
	float value5Before = texelFetch(mm_LastFrameData,ivec2((mat_pattern+4)%16,2),0).r;
	float switchedOn6 = texelFetch(mm_LastFrameData,ivec2((mat_pattern+6)%16,2),0).a;

	float beamGroupCenter = curlNoise(vec2((0.2+random2)*animTime+random1,0)).x/6;
	float beamGroupWidth = 0.5*(0.2+random3);
	if (random2>0.3) {
		beamGroupWidth *= -1;
	}
	
	pos.x = beamGroupCenter + beamGroupWidth * (-0.5+pointNumber/7.) * mat_scale;

	float translateY;
	if (mat_v_move_shape == 0) {
		translateY = mat_v_move_size * sin((mat_v_move_time/6+pointNumber*mat_v_move_offset/POINT_COUNT)*2*PI) / 2;
	} else if (mat_v_move_shape == 1) {
		translateY = mat_v_move_size * fract(mat_v_move_time/6+pointNumber*mat_v_move_offset/POINT_COUNT);
	} else if (mat_v_move_shape == 2) {
		translateY = mat_v_move_size * (0.5-abs(mod((mat_v_move_time/6+pointNumber*mat_v_move_offset/POINT_COUNT)*2+1,2)-1));
	} else if (mat_v_move_shape == 3) {
		translateY = mat_v_move_size * (0.5-step(0.5,mod((mat_v_move_time/6+pointNumber*mat_v_move_offset/POINT_COUNT),1)));
	} else if (mat_v_move_shape == 4) {
		translateY = mat_v_move_size * (0.5*noise(vec2((mat_v_move_time/6+pointNumber*mat_v_move_offset*99.5/POINT_COUNT),0)));
	} else {
		translateY = mat_v_move_size * (-0.5 * sin(-PI/2 + mod((mat_v_move_time/6+pointNumber*mat_v_move_offset/POINT_COUNT),1)*PI));
	}

	pos.y = translateY;
	color = mat_color;
	if (random4 < 0.4) {
		color *= (int(animTime*pow(2,4+2*random5))%2==0?1:0);
	}
	shapeNumber = pointNumber;
	
	if (switchedOn6>0.5) {
		pos = vec2(beamGroupCenter,0) * mat_scale;
	} else if (value5Now > 0.2) {
		pos *= -1;
	} else if (value5Before < 0.2) {
		vec2 lastPos = texture(mm_LastFrameData,vec2(float(pointNumber+0.5)/pointCount,0)).rg;
		if (lastPos.x > -1 && lastPos.x < 1) {
			pos = mix(lastPos,pos,1-mat_smooth);
		}
	}
}
