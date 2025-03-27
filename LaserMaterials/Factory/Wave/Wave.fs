/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "A multiple sinus going up and down",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		
		
		{"LABEL": "Animation/Amplitude", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.8, "DESCRIPTION":"To adjust the amplitude of the multi sinus animation"},	 
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0, "MAX": 1.0, "DESCRIPTION":"To adjust the speed of the offset in the multi sinus animation"},
		{"LABEL": "Animation/Crush", "NAME": "mat_sizeShape", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"To crush the visual"},
		{"LABEL": "Animation/Adjustements", "NAME": "mat_adjustement", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [10.,2.], "DEFAULT": [2.3,0.6],"DESCRIPTION":"To adjust somes parameters of the sinusoid, x : frequence, y : adding other sinus"},
		
		{"LABEL": "Shape Splitter/Activate", "NAME": "mat_shapeButton", "TYPE": "bool", "DEFAULT": false,"FLAGS":"button", "DESCRIPTION":"The shape splitter mod will devide the wave into pieces"}, 
		{"LABEL": "Shape Splitter/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 5, "MIN":0,"MAX":10, "DESCRIPTION":"To adjust the amount of shapes"},
		{"LABEL": "Shape Splitter/Shape Width", "NAME": "mat_shapeLength", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"To adjust the size of the shapes"}, 
		{"LABEL": "Shape Splitter/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To transform each shape into beams."}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Sin 4", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Inv 2","Inv 4","Inv 8"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin, Inv : Sin but moving backward", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
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

const int pointsForPrecision[10] = int[10](2,3,4,5,6,7,8,9,10,500);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int finalPointCount = pointsForPrecision[mat_precision-1];

	// Write last point count in userData so we disable feedback when changing point count
	userData.x = finalPointCount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int shapeAmount = mat_shapeButton?mat_shapeAmount:1;
	if(!mat_shapeButton && mat_beamModeButt){shapeAmount=1;}

	float shapeLength = mat_shapeButton?mat_shapeLength:1;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMaxX = (normalizedPos)*2-1;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPos-mat_timeOffset)*2-1;

	int shapeSize = int((1-shapeLength)*finalPointCount);
	
	int shapeNumberCalc = int(normalizedPos*shapeAmount*finalPointCount)%finalPointCount;

	if(shapeNumberCalc<shapeSize/2 || shapeNumberCalc>finalPointCount-shapeSize/2){shapeNumber=-1;}
	else{shapeNumber=int(normalizedPos*shapeAmount);}

	int pointPerShape = int(finalPointCount/mat_shapeAmount);
	
	if(mat_shapeLength==0 || (mat_shapeButton && mat_beamModeButt)){
		if(pointNumber%pointPerShape==pointPerShape/2){
			shapeNumber=int(normalizedPos*shapeAmount);
		}
		else{shapeNumber=-1;};
	}
	
	if(shapeLength==1. && !mat_beamModeButt){shapeNumber=1;}

	pos = vec2(
		normalizedPosMaxX
		,sin(PI*normalizedPosMaxY)*mat_adjustement[1]+sin(mat_adjustement[0]*PI*normalizedPosMaxY)

	);
	pos[1] *= mat_shapeSize*((1/(1+mat_adjustement[1]))*0.9);
	pos[1] += sin(0.5*PI*normalizedPosMinY)*0.1;
	pos*=mat_size;
	pos[0]*=mat_sizeShape;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_offsetGeneral[0];
	pos[1]+=mat_offsetGeneral[1];	

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*2*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(shapeAmount*2*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(shapeAmount*4*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(shapeAmount*8*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(shapeAmount*2*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(shapeAmount*8*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;
	
	if(FRAMEINDEX > 0) {
		if (finalPointCount == lastFramePointCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = vec2(mat_offsetGeneral);
			pos = mix(vec2(mat_offsetGeneral),lastFramePos,feedback);
		}
	}
}
