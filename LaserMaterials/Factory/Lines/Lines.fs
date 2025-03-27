/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Matt",
	"DESCRIPTION": "describe your material here",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Global/Count", "NAME": "mat_shape_count", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 50 }, 
		{"LABEL": "Global/V Spacing", "NAME": "mat_v_spacing", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 }, 
		{"LABEL": "Global/Translation Y", "NAME": "mat_base_transitiony", "TYPE": "float", "DEFAULT": -1.0, "MIN": -1.0, "MAX": 1.0 },
		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
	    { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Global/Rotation", "NAME": "mat_base_rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },

		{"LABEL": "Noise/Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Noise/Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
		{"LABEL": "Noise/Speed", "NAME": "mat_noisespeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },

	    { "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
	    { "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
	    { "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Auto Move/Strob", "NAME": "mat_automovestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "In" },
	    { "LABEL": "Auto Move/Offset", "NAME": "mat_automoveoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },

	    { "LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 3.0 },
	    { "LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Rotate/Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },

	    { "LABEL": "Auto Light/Active", "NAME": "mat_autolightactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Auto Light/Size", "NAME": "mat_autolightsize", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Light/Speed", "NAME": "mat_autolightspeed", "TYPE": "float", "DEFAULT": 3.0, "MIN": 0.0, "MAX": 3.0 },
	    { "LABEL": "Auto Light/Strob", "NAME": "mat_autolightstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Light/Shape", "NAME": "mat_autolightshape", "TYPE": "long", "VALUES": ["Smooth","In","Cut"], "DEFAULT": "Cut" },
	    { "LABEL": "Auto Light/Offset", "NAME": "mat_autolightoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

	    { "LABEL": "Auto Color/Active", "NAME": "mat_autocoloractive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	    { "LABEL": "Auto Color/Speed", "NAME": "mat_autocolorspeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 3.0 },
	    { "LABEL": "Auto Color/Strob", "NAME": "mat_autocolorstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Auto Color/Shape", "NAME": "mat_autocolorshape", "TYPE": "long", "VALUES": ["Smooth","In","Cut"], "DEFAULT": "In" },
	    { "LABEL": "Auto Color/Offset", "NAME": "mat_autocoloroffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

		{"LABEL": "Fading/Top", "NAME": "mat_fade_top", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0, "FLAGS": "collapsed" },
		{"LABEL": "Fading/Bottom", "NAME": "mat_fade_bottom", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0, "FLAGS": "collapsed" },
		{"LABEL": "Fading/Left", "NAME": "mat_fade_left", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0, "FLAGS": "collapsed" },
		{"LABEL": "Fading/Right", "NAME": "mat_fade_right", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0, "FLAGS": "collapsed" },
	],
	"GENERATORS": [
		{
			"NAME": "mat_noise_time",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_noisespeed", "speed_curve":2, "bpm_sync": false, "link_speed_to_global_bpm":true}},
		{
			"NAME": "mat_move_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_automovespeed","reverse": "mat_automovereverse", "strob": "mat_automovestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
		{
			"NAME": "mat_rot_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
		{
			"NAME": "mat_light_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_autolightspeed","strob": "mat_autolightstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
		{
			"NAME": "mat_color_position",
			"TYPE": "time_base",
			"PARAMS": {"speed": "mat_autocolorspeed","strob": "mat_autocolorstrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
		},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 4000
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId)
{
    // Center
    mat3 linePatternsMatrix = mat3(1,0,0,
                              0,1,0,
                              0,0,1);

    // Rotate
    if (mat_autorotateactive || (mat_base_rotation!=0)) {
      float angle = mat_base_rotation * 2*PI / 360;
      if (mat_autorotateactive) {
        angle += fract(0.5 + (mat_rot_position+normalizedCellId*mat_autorotateoffset)) * 2*PI;
      }
      float sin_factor = sin(angle);
      float cos_factor = cos(angle);
      //uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
      linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
                                 -sin_factor, cos_factor, 0,
                                 0, 0, 1);
    }

    return linePatternsMatrix;
}

float getLightValue(float normalizedCellId)
{
    // Light
    float light = 1;
    if (mat_autolightactive) {
      // "Smooth","In","Cut"
      if (mat_autolightshape == 0) {
        light -= mat_autolightsize * (1+sin((mat_light_position+normalizedCellId*mat_autolightoffset)*2*PI))/2;
      } else if (mat_autolightshape == 1) {
        light -= mat_autolightsize * (mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      } else {
        light -= mat_autolightsize * step(0.5,mod((mat_light_position+normalizedCellId*mat_autolightoffset),1));
      }
    }
    return light;
}

vec3 mat_hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

vec4 getColorValue(float normalizedCellId)
{
    // Color
    vec4 color = vec4(1);
    if (mat_autocoloractive) {
      // "Smooth","In","Cut"
      float hue;
      if (mat_autocolorshape == 0) {
        hue = (1+sin((mat_color_position+normalizedCellId*mat_autocoloroffset)*2*PI))/2;
      } else if (mat_autocolorshape == 1) {
        hue = (mod((mat_color_position+normalizedCellId*mat_autocoloroffset),1));
      } else {
        hue = step(0.5,mod((mat_color_position+normalizedCellId*mat_autocoloroffset),1));
      }
      color = vec4(mat_hsv2rgb(vec3(hue,1.,1.)),1);
    }
    return color;
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

	// 2 points per line
	int lineNumber = shapeNumber;

	pos = vec2(-1 + 2*normalizedPosInShape,mat_v_spacing*(-1+2*normalizedShapeId+mat_base_transitiony));

	// Move
	float translateX = 0;
	if (mat_automoveactive) {
		if (mat_automoveactive) {
			// "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
			if (mat_automoveshape == 0) {
				translateX = mat_automovesize * sin((mat_move_position+normalizedShapeId*mat_automoveoffset)*2*PI) / 2;
			} else if (mat_automoveshape == 1) {
				translateX = mat_automovesize * fract(mat_move_position+normalizedShapeId*mat_automoveoffset);
			} else if (mat_automoveshape == 2) {
				translateX = mat_automovesize * (0.5-abs(mod((mat_move_position+normalizedShapeId*mat_automoveoffset)*2+1,2)-1));
			} else if (mat_automoveshape == 3) {
				translateX = mat_automovesize * (0.5-step(0.5,mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)));
			} else if (mat_automoveshape == 4) {
				translateX = mat_automovesize * (0.5*noise(vec2((mat_move_position+normalizedShapeId*mat_automoveoffset*99.5),0)));
			} else {
				translateX = mat_automovesize * (-0.5 * sin(-PI/2 + mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)*PI));
			}
		}
	}
	pos.y = -1 + 2*(fract((pos.y+1)/2+translateX));

	if (mat_noisepower > 0) {
		pos.y += mat_noisepower * noise(mat_noisescale * vec2(normalizedPosInShape,normalizedShapeId) + vec2(0,mat_noise_time));
	}

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = (vec3(pos,1) * transformMatrix).xy;

	color = vec4(getLightValue(normalizedShapeId) * mat_color * getColorValue(normalizedShapeId)); 

	if (pos.x < -1+mat_fade_left*2) color -= ((-1+mat_fade_left*2)-pos.x)/(mat_fade_left*2);
	if (pos.x > 1-mat_fade_right*2) color -= (pos.x-(1-mat_fade_right*2))/(mat_fade_right*2);
	if (pos.y < -1+mat_fade_bottom*2) color -= ((-1+mat_fade_bottom*2)-pos.y)/(mat_fade_bottom*2);
	if (pos.y > 1-mat_fade_top*2) color -= (pos.y-(1-mat_fade_top*2))/(mat_fade_top*2);
}
