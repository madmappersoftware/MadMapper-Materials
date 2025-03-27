/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Matt",
	"DESCRIPTION": "Animated groups of beams",
	"TAGS": "atmos,beam",
	"VSN": "1.0",
	"INPUTS": [
			{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.8 }, 
			{"LABEL": "Global/Groups", "NAME": "mat_groups", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 2 }, 
			{"LABEL": "Global/Points", "NAME": "mat_points", "TYPE": "int", "MIN": 4, "MAX": 10, "DEFAULT": 4 }, 
			{"LABEL": "Global/Rotate", "NAME": "mat_rotate", "TYPE": "float", "MIN": 0, "MAX": 180, "DEFAULT": 0 }, 
			{"LABEL": "Global/Group Dist", "NAME": "mat_group_dist", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.0 }, 
			{"LABEL": "Global/Point Dist", "NAME": "mat_point_dist", "TYPE": "float", "MIN": 0.01, "MAX": 0.5, "DEFAULT": 0.15 }, 
			{"LABEL": "Global/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
			{"LABEL": "Global/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 

			{ "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
			{ "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
			{ "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
			{ "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
			{ "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Noise"], "DEFAULT": "Noise" },
			{ "LABEL": "Auto Move/Offset", "NAME": "mat_automoveoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

			{ "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
			{ "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2.0 },
			{ "LABEL": "Auto Rotate/Duration", "NAME": "mat_autorotateduration", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1.0 },

			{ "LABEL": "Auto Light/Active", "NAME": "mat_autolightactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
			{ "LABEL": "Auto Light/Speed", "NAME": "mat_autolightspeed", "TYPE": "float", "DEFAULT": 2.5, "MIN": 0.0, "MAX": 3.0 },
			{ "LABEL": "Auto Light/Shape", "NAME": "mat_autolightshape", "TYPE": "long", "VALUES": ["Cut","Smooth","Out","Noise"], "DEFAULT": "Cut" },
			{ "LABEL": "Auto Light/Offset", "NAME": "mat_autolightoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
	],
	"GENERATORS": [
		{
			"NAME": "mat_move_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_automovespeed","reverse": "mat_automovereverse", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
		{
			"NAME": "mat_rot_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_autorotatespeed","bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
		{
			"NAME": "mat_light_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_autolightspeed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 4000,
	   "PRESERVE_ORDER": true,
	   "ENABLE_FRAME_BLENDING": false
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int groupNumber = pointNumber/mat_points;
	if (groupNumber >= mat_groups) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}	

	float normalizedGroupNumber = 0;
	if (mat_groups>1) {
		normalizedGroupNumber = float(groupNumber)/(mat_groups-1);
	}

	int poinIdx = pointNumber % mat_points;
	float normalizedPointIdx = float(poinIdx)/(mat_points-1);	

	shapeNumber = pointNumber;

	// Position in group
	pos = vec2(0,mat_point_dist*(-0.5+normalizedPointIdx));

	// Rotate if needed
	if (mat_rotate>0) {
		float angle = mat_rotate*PI/180;
		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		//uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
		mat3 rotMat = mat3(cos_factor, sin_factor, 0,
						   -sin_factor, cos_factor, 0,
						   0, 0, 1);
		pos = (rotMat * vec3(pos,0)).xy;
	}

	// Offset groups
	pos.y += mat_group_dist*(-0.5+normalizedGroupNumber);

	float normalizedGroupIdx = float(groupNumber)/mat_groups;	
	
	// Move
	if (mat_automoveactive) {
		// "Smooth"=0,"In"=1,"Noise"=2
		if (mat_automoveshape == 0) {
			pos.x += mat_automovesize * sin((mat_move_position/10+normalizedGroupIdx*mat_automoveoffset)*2*PI);
		} else if (mat_automoveshape == 1) {
			pos.x += mat_automovesize * 2 * (0.5-mod((mat_move_position/5+normalizedGroupIdx*mat_automoveoffset),1));
		} else {
			pos.x += mat_automovesize * noise(vec2((mat_move_position/5+normalizedGroupIdx*mat_automoveoffset*10),0));
		}
	}

	// Rotate
	if (mat_autorotateactive) {
		float rotPosition = mat_rot_position / 10;
		float rotValue = floor(rotPosition);
		if (fract(rotPosition) > 1-mat_autorotateduration) {
			float normalizedTransitionTime = (fract(rotPosition) - (1-mat_autorotateduration)) / mat_autorotateduration;
			if (normalizedTransitionTime < 0.5) {
				normalizedTransitionTime = 0.5*pow(normalizedTransitionTime*2,3);
			} else {
				normalizedTransitionTime = 1-0.5*pow((1-normalizedTransitionTime)*2,3);
			}
			//normalizedTransitionTime = 0.5 + 0.5*cos(PI + normalizedTransitionTime*PI);
			rotValue += normalizedTransitionTime;
		}

		float angle = rotValue * PI;

		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		//uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
		mat3 rotMat = mat3(cos_factor, sin_factor, 0,
						   -sin_factor, cos_factor, 0,
						   0, 0, 1);
		pos = (vec3(pos,1) * rotMat).xy;
  }

	// Light
	float lightValue = 1;
	if (mat_autolightactive) {
		// "cue"=0,Smooth"=1,"Out"=2,"Noise"=3
		if (mat_autolightshape == 0) {
			lightValue = fract((mat_light_position*2+normalizedGroupIdx*mat_autolightoffset))<0.5?1:0;
		} else if (mat_autolightshape == 1) {
			lightValue = 0.5+0.5*sin((mat_light_position*2+normalizedGroupIdx*mat_autolightoffset)*2*PI);
		} else if (mat_autolightshape == 2) {
			lightValue = 1-fract((mat_light_position*2+normalizedGroupIdx*mat_autolightoffset));
		} else {
			lightValue = 0.5+0.5*noise(vec2((mat_light_position*2+normalizedGroupIdx*mat_autolightoffset*10),0));
		}
	}

	pos = mat_scale * pos;
	color = lightValue * vec4(mix(mat_color1,mat_color2,normalizedGroupNumber));
}
