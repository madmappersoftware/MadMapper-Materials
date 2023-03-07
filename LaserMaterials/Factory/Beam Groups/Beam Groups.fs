/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Line with Beams",
    "TAGS": "laser,beams,atmos",
    "VSN": "1.0",
    "INPUTS": [
		{ "LABEL": "Global Scale", "NAME": "mat_global_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Count", "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 4 },
		{ "LABEL": "Groups", "NAME": "mat_group_count", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 3 },
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Group Scale", "NAME": "mat_group_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Group Offset", "NAME": "mat_group_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
	    { "LABEL": "Symetry", "NAME": "mat_symetry", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
	    { "LABEL": "Movement", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Noise"], "DEFAULT": "Noise" },

	    { "LABEL": "Auto Scale/Active", "NAME": "mat_auto_scale", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
	    { "LABEL": "Auto Scale/Offset", "NAME": "mat_auto_scale_offfset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

	    { "LABEL": "Noise/Power", "NAME": "mat_noise_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },
	    { "LABEL": "Noise/Mode", "NAME": "mat_noise_mode", "TYPE": "long", "DEFAULT": "Flow", "VALUES": [ "Flow", "Billow", "fBm", "MultiFractal", "White" ] },
	    { "LABEL": "Noise/Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
	    { "LABEL": "Noise/Scale", "NAME": "mat_noise_scale", "TYPE": "float", "MIN": 0.1, "MAX": 10.0, "DEFAULT": 1.0 },
	    { "LABEL": "Noise/Group Offset", "NAME": "mat_noise_group_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

		{ "LABEL": "Colors/Count", "NAME": "mat_color_count", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 5 }, 
		{ "LABEL": "Colors/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
		{ "LABEL": "Colors/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [1,0,0,1] }, 
		{ "LABEL": "Colors/Color 3", "NAME": "mat_color3", "TYPE": "color", "DEFAULT": [0,0,0,1] }, 
		{ "LABEL": "Colors/Color 4", "NAME": "mat_color4", "TYPE": "color", "DEFAULT": [1,0,0,1] }, 
		{ "LABEL": "Colors/Color 5", "NAME": "mat_color5", "TYPE": "color", "DEFAULT": [0.5,0.4,0.3,1] }, 
		{ "LABEL": "Colors/Offset", "NAME": "mat_color_group_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Colors/Auto Cycle", "NAME": "mat_color_auto", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
	    { "LABEL": "Colors/Speed", "NAME": "mat_color_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
    ],
    "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,}},
	    {"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "speed_curve":2,}},
	    {"NAME": "mat_color_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_color_speed", "speed_curve":2,}},
	],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 1024,
       "PRESERVE_ORDER": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

highp float mat_rand(vec2 co)
{
  highp float a = 12.9898;
  highp float b = 78.233;
  highp float c = 43758.5453;
  highp float dt= dot(co.xy ,vec2(a,b));
  highp float sn= mod(dt,3.14);
  return fract(sin(sn) * c);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int beamCount = mat_count * mat_group_count;
	if (mat_symetry) {
		beamCount *= 2;
	}

	int beamNumber = int(pointNumber) % mat_count;
	int groupNumber = int(pointNumber) / mat_count;
	bool isSymetric = false;
	if (mat_symetry && groupNumber >= mat_group_count) {
		isSymetric = true;
		groupNumber -= mat_group_count;
	}

	// Nothing to write
	if (groupNumber >= mat_group_count) {
	    shapeNumber = -1;
		return;
	}	

	// Shape Number
	shapeNumber = pointNumber;

	// Pos
	float position;
	float rescale;
	float scale = max(0,mat_group_scale-0.0001);
	if (mat_auto_scale) {
		float groupScaleOffset = groupNumber*mat_auto_scale_offfset/mat_group_count;
		scale *= flowNoise(vec2(1.23+groupScaleOffset+mat_animation_time/10,10+groupScaleOffset+mat_animation_time/9),0);
	}
	float groupOffset = groupNumber*mat_group_offset/mat_group_count;
	if (mat_automoveshape == 0) { // Smooth
		position = (0.5+0.5*sin(groupOffset*2*PI+mat_animation_time));
		rescale = 1-scale;
	} else if (mat_automoveshape == 1) { // In
		position = fract(mat_animation_time+groupOffset);
		rescale = 1;
	} else { // Noise
		position = 0.5+0.5*1.1*flowNoise(vec2(groupOffset+mat_animation_time/7,groupOffset+mat_animation_time/4.5),0);
		rescale = 1-scale;
	}

	float groupBaseX = position;

	float xInSegment;
	if (mat_count == 1) {
		xInSegment = position;
	} else {
		position *= rescale;
		xInSegment = position + beamNumber * scale / (mat_count-1);
	}
	if (isSymetric) {
		xInSegment = min(1,1 - xInSegment);
	}

	pos = vec2(-0.9+1.8*xInSegment,0);

	// Add noise
    float noiseValue = 0.;

	if (mat_noise_mode == 0) { // Flow
		noiseValue = flowNoise(vec2(groupBaseX,groupNumber*mat_noise_group_offset)*mat_noise_scale * 0.9543 + vec2(0.21,453.2),mat_noise_time/2);
	} else if (mat_noise_mode == 1) { // Billow
		noiseValue = billowedNoise(vec3(vec2(groupBaseX,groupNumber*mat_noise_group_offset)*mat_noise_scale * 0.9543 + vec2(0.1,453.2),mat_noise_time/2))*2.-1.;
	} else if (mat_noise_mode == 2) { // fBm
		noiseValue = fBm(vec3(vec2(groupBaseX,groupNumber*mat_noise_group_offset)*mat_noise_scale * 0.9543 + vec2(0.21,453.2),mat_noise_time/2));
	} else if (mat_noise_mode == 3) { // MultiFractal
		noiseValue = ridgedMF(vec3(vec2(groupBaseX,groupNumber*mat_noise_group_offset)*mat_noise_scale * 0.9543 + vec2(0.21,453.2),mat_noise_time/2))*2.-1.;
	} else if (mat_noise_mode == 4) { // White
		noiseValue = mat_rand(vec2(0.21+groupNumber*mat_noise_group_offset,453.2+mat_noise_time/2))*2.-1.;
	}


	pos.y -= noiseValue * mat_noise_power * mat_noise_power;
	pos *= mat_global_scale;

	// color
	float colorGroupOffset = (mat_color_group_offset * groupNumber) / (mat_group_count+1);
	float colorIndex = colorGroupOffset * mat_color_count;
	if (mat_color_auto) {
		colorIndex += mat_color_time;
	}
	colorIndex = mod(colorIndex,mat_color_count);
	if (colorIndex < 1) {
		if (mat_color_count < 2) {
			color = mix(mat_color1,mat_color1,colorIndex-0);	
		} else {
			color = mix(mat_color1,mat_color2,colorIndex-0);
		}
	} else if (colorIndex < 2) {
		if (mat_color_count < 3) {
			color = mix(mat_color2,mat_color1,colorIndex-1);	
		} else {
			color = mix(mat_color2,mat_color3,colorIndex-1);	
		}
	} else if (colorIndex < 3) {
		if (mat_color_count < 4) {
			color = mix(mat_color3,mat_color1,colorIndex-2);	
		} else {
			color = mix(mat_color3,mat_color4,colorIndex-2);	
		}
	} else if (colorIndex < 4) {
		if (mat_color_count < 5) {
			color = mix(mat_color4,mat_color1,colorIndex-3);	
		} else {
			color = mix(mat_color4,mat_color5,colorIndex-3);	
		}
	} else if (colorIndex < 5) {
		color = mix(mat_color5,mat_color1,colorIndex-4);	
	}
}
