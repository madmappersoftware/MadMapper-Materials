/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Breathing Cone",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Side", "NAME": "mat_precision", "TYPE": "int", "MIN": 2, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"To adjust the amount of faces of each shape, 10 is circle"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.6, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},  
		
		{"LABEL": "Animation/Activate", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"To activate the size oscillated animation"}, 
		{"LABEL": "Animation/Speed", "NAME": "mat_offsetGeneral", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT":0.15, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT":0.1, "DESCRIPTION":"To adjust the offset of the animation to avoid the beam in the animation"},
		{"LABEL": "Animation/Shape", "NAME": "mat_animationShape", "TYPE": "long", "VALUES": ["Smooth","In","Out"], "DEFAULT": "Smooth","FLAGS": "button_grid", "DESCRIPTION":"To choose the oscillation mod : -smooth- is sin, -in- is a ramp, -out- is a minus ramp"},

		{"LABEL": "Tilt/Auto Tilt", "NAME": "mat_tiltActivated", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To activate the tilt animation"}, 
		{"LABEL": "Tilt/Reverse", "NAME": "mat_tiltReverse", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To activate the tilt reverse"}, 
		{"LABEL": "Tilt/Speed ", "NAME": "mat_offsetRotate", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT":0.5, "DESCRIPTION":"To adjust the speed of the animation"},

		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Sin 2", "VALUES": ["Main","Osc","Pulse","Sin 2","Sin 4","Pulse x2"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin and Pulse: Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT":0.15 },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. },

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6  },
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },	
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffsetPhase", "TYPE": "time_base", "PARAMS": {"speed": "mat_offsetRotate","reverse": "mat_tiltReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_animationReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOutAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Out", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOffset", "value2": 4, "value3": 1, "value4": 1}}
	],
	"RENDER_SETTINGS": {
		"POINT_COUNT": 500,
	},
	"RASTERISATION_SETTINGS": {
		"REQUIRES_LAST_FRAME": true
	},
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

const int pointsForPrecision[10] = int[10](2,3,4,5,6,7,8,9,10,500);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int finalPointCount = pointsForPrecision[mat_precision-1];

	userData.x = finalPointCount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int pointsPerShape = finalPointCount;

	shapeNumber = pointNumber/pointsPerShape;
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2 * normalizedPosInShape - 1;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);

	float rotate = mat_tiltActivated?mat_timeOffsetPhase:0.;

	pos = vec2(
		sin(PI*normalizedPosInShapeMax+rotate)
		,sin(PI*(normalizedPosInShapeMax+0.5+rotate))
	);

	if(mat_bounceActivated){
		if(mat_animationShape==0){pos*=(mat_timeSmoothAnim*(1.-mat_offset))+mat_offset;}
		if(mat_animationShape==1){pos*=(mat_timeInAnim*(1.-mat_offset))+mat_offset;}
		if(mat_animationShape==2){pos*=(mat_timeOutAnim*(1.-mat_offset))+mat_offset;}
	}

	pos*=mat_size;
	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,1);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*PI)+1)/2));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(4*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(4*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (finalPointCount == lastFramePointCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = vec2(mat_offsetGeneral);
			pos = mix(pos,lastFramePos,feedback);
		}
	}
}
