/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Sligthly moving parametrical function for long period movement",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the visual movements"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
	
		{"LABEL": "Animation/Speed", "NAME": "mat_speedAnim", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0.8, "DESCRIPTION":"To adjust the speed of the animation"}, 
		{"LABEL": "Animation/Length", "NAME": "mat_length", "TYPE": "float", "MIN": 0., "MAX": 4., "DEFAULT": 2., "DESCRIPTION":"To adjust the length of the lines" },  
		{"LABEL": "Animation/Animation mod", "NAME": "mat_insideShift", "TYPE": "point2D", "DEFAULT": [-1,0.2],"MIN":[-1.5,-1.5],"MAX":[1.5,1.5], "DESCRIPTION":"The lines are controled by a parametric function, you can adjust the parameters of the function. x axe : frequency, y axe : y amplitude"},  

		{"LABEL": "Length/Length Bounce", "NAME": "mat_lengthBounceButt", "TYPE": "bool", "DEFAULT": true,"FLAGS":"button", "DESCRIPTION":"The length bounce is on oscillator controlling the length of the parametric curve"}, 
		{"LABEL": "Length/Speed Bounce", "NAME": "mat_offsetGeneral", "TYPE": "float", "DEFAULT": 1, "MIN": 0, "MAX": 1., "DESCRIPTION":"To adjust the speed of the length oscillation"},
		{"LABEL": "Length/Offset", "NAME": "mat_offsetLength", "TYPE": "float", "DEFAULT": 1, "MIN": 0, "MAX": 1. , "DESCRIPTION":"To adjust the offset of the length oscillation"},
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Inv 2", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Inv 2","Inv 4","Inv 8"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin & Pulse: Oscillates between main and secondary color with a sin and time offset", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [1, 0, 0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1., "MIN": 0.0, "MAX": 1. },

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6  },
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },	
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeShift", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnim","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},	
		{"NAME": "osc_offset", "TYPE": "animator", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "osc_length", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeParamChange", "TYPE": "animator", "PARAMS": {"speed": "mat_offsetGeneral","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
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

const int pointsForPrecision[10] = int[10](2,3,4,5,6,7,8,12,15,500);

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
	int shapeAmount = 1;

	int pointsPerShape = finalPointCount/shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;

	if (shapeNumber >= shapeAmount) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2*mat_length * (normalizedPosInShape - 0.5);
	if(mat_lengthBounceButt){normalizedPosInShapeMax*=osc_length+mat_offsetLength/5;}
	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);

	float osc = (cos(0.1*PI*(normalizedPosInShapeMax-mat_timeOffset))+1)/2;
	float oscOffseted = (cos(0.1*PI*(normalizedPosInShapeMax-mat_timeOffset))+1)/2;
	float instance = normalizedPosInShapeMax;
	
	float x = instance*osc+(mat_insideShift[0]*sin(PI*instance))*oscOffseted;
	float y = (mat_insideShift[1]*sin(PI*instance))*osc+instance*oscOffseted;
	float shapeNumberOSC = 2*(shapeNumber-0.5);

	//float autoInsideShift = 1/(exp(pow(pow(osc_length,2)-1,20)-1));

	
	//vec2 autoInsideShift = noise();
	float autoShift = sin(PI*mat_timeShift*0.9);
	float autoShift2 = sin((PI+0.5)*mat_timeShift*0.9);
	
	pos = vec2(
		sin((PI+mat_insideShift[0]+autoShift)*normalizedPosInShapeMax)*autoShift
		,sin((PI+mat_insideShift[1]+autoShift2)*normalizedPosInShapeMax)*autoShift2
	);
	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,1.);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*1*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(4*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(4*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	
	float feedback = -pow(mat_feedback-1.,2)+1.;
	
	if(FRAMEINDEX > 0) {
		if (finalPointCount == lastFramePointCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber*lastFramePointCount/finalPointCount,0),0).rg;
			pos = mix(pos,lastFramePos,feedback);
		}
	}

}
