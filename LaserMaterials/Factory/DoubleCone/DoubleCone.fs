/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Moving Double Cones",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Sides", "NAME": "mat_precision", "TYPE": "int", "MIN": 3, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"To adjust the amount of faces of each shape, 10 is circle"},  
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.3, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Shape Dist", "NAME": "mat_shapeDistance", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 1.25, "DESCRIPTION":"To adjust the space between the circles"},
		
		{"LABEL": "Animation/Speed", "NAME": "mat_offsetGeneral", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT":0.25, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Mods", "NAME": "mat_phaseButt", "TYPE": "long", "VALUES": ["Circle","Bounce","Mirror","Scan","In","Out"], "DEFAULT": "Scan","FLAGS": "button_grid", "DESCRIPTION":"You can choose several presets for the animation"}, 
		{"LABEL": "Animation/Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT":0.0, "DESCRIPTION":"To adjust the offset an offset in the animation"},
		{"LABEL": "Animation/Y Translate", "NAME": "mat_Y", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To allow the visual to oscillate on the y axe"}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Sin 4", "VALUES": ["Main","Osc","Pulse","Sin 2","Sin 4","Pulse x2"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Osc : Oscillate between main and secondary color in time, Sin & Pulse: Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT":0.15, "DESCRIPTION":"To adjust the speed animation of the sin and pulse color"}, 
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
		
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_offsetGeneral", "value2": 0.5, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier2","TYPE": "multiplier","PARAMS": {"value1": "mat_offsetGeneral", "value2": 4, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOffsetColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplierColor","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeAutoShift", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier2","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOutAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Out", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplierColor","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOffset", "value2": 4, "value3": 1, "value4": 1}}
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

mat3 makeTransformMatrix(float normalizedCellId)
{
	
	float base_rotation = 45*mat_button_angle;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);
	if (base_rotation!=0) {
		float angle = base_rotation * 2*PI / 360;

		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		//uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
		linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
								 -sin_factor, cos_factor, 0,
								 0, 0, 1);
	}

	return linePatternsMatrix;
}

const int pointsForPrecision[9] = int[9](6,8,10,12,14,16,18,20,500);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int finalPointCount = pointsForPrecision[mat_precision-2];

	userData.x = finalPointCount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int pointsPerShape = finalPointCount/2;

	shapeNumber = pointNumber/pointsPerShape;
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2 * normalizedPosInShape - 1;

	float normalizedShapeId = float(shapeNumber)/2;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);

	pos = vec2(
		(sin(PI*normalizedPosInShapeMax)+(shapeNumber-0.5)*2*mat_shapeDistance)
		,sin(PI*(normalizedPosInShapeMax+0.5))
	);

	float phase = 0.;
	if(mat_phaseButt==0){phase=1./4;}
	else if(mat_phaseButt==1){phase=1./3;}
	else if(mat_phaseButt==2){phase=1./2;}
	else if(mat_phaseButt==3){phase=1./8;}

	if(mat_phaseButt==4){pos*=(mat_timeInAnim*(1.-mat_offset)+(mat_offset));}
	else if(mat_phaseButt==5){pos*=mat_timeOutAnim*(1.-mat_offset)+(mat_offset);}
	else{pos*=sin(mat_timeAutoShift*PI*phase)*(1.-mat_offset)+(mat_offset);}

	pos[0]+=sin(mat_timeAutoShift*PI/2);
	mat_Y?pos[1]+=sin(mat_timeAutoShift*PI/4)*2:0;

	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	float normalizedPosMaxRightColor = (normalizedPosInShape-mat_timeOffsetColor)*2-1;
	float normalizedPosMaxLeftColor = (normalizedPosInShape+mat_timeOffsetColor)*2-1;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,1);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffsetColor*10.*PI*phase)+1)/2));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(shapeNumber==0?normalizedPosMaxRightColor:normalizedPosMaxLeftColor)+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(4*PI*(shapeNumber==0?normalizedPosMaxRightColor:normalizedPosMaxLeftColor)+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(4*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRightColor:normalizedPosMaxLeftColor)))+1));
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
