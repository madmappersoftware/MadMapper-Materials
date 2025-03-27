/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "The broken line is fractured in n points which follow a sinus path, those points are connected each other by a segment",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},  
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Segments", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 5, "MIN":2,"MAX":5, "DESCRIPTION":"To adjust the amount of lines"},
		{"LABEL": "Global/Width", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"To adjust the width of the visual"},  
		
		{"LABEL": "Animation/Ramp Mod", "NAME": "mat_buttonRamp", "TYPE": "long", "DEFAULT":"Grad", "VALUES": ["Grad","Flat","Curve"], "FLAGS": "button_grid", "DESCRIPTION":"To select the ramp mod, grad : Linear function and a step between every points, Flat : constant function and a step between every points, Curve : Linear function without a step"}, 
		{"LABEL": "Animation/Amplitude", "NAME": "mat_ampAnimation", "TYPE": "float", "DEFAULT": 1., "MIN": 0., "MAX": 1., "DESCRIPTION":"To adjust the amplitude of the visual"},
    	{"LABEL": "Animation/Speed", "NAME": "mat_speedAnimation", "TYPE": "float", "DEFAULT": 0.1, "MIN": -1., "MAX": 1., "DESCRIPTION":"To adjust the speed of the animation"},
    	{"LABEL": "Animation/Frequency", "NAME": "mat_freqAnim", "TYPE": "float", "DEFAULT": 1.25, "MIN": 0., "MAX":3., "DESCRIPTION":"The animation is a sinusoid, adjust here the frequency"},
    	
		{"LABEL": "Beam/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To activate the Beam Mod which transforms every function into beams"},
		{"LABEL": "Beam/Beam Amount", "NAME": "mat_beamAmount", "TYPE": "int", "MIN": 0, "MAX": 10, "DEFAULT": 5, "DESCRIPTION":"To adjust the amount of beams per shape"},

        {"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. , "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},
		
		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
        {"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier5","reverse": "mat_animationReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOffset05", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier05","reverse": "mat_animationReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSpeed", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier5","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timePath", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier2","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier05","TYPE": "multiplier","PARAMS": {"value1": "mat_speedAnimation", "value2": 1, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier5","TYPE": "multiplier","PARAMS": {"value1": "mat_speedAnimation", "value2": 5, "value3": 1, "value4": 1}},
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
	float base_rotation = mat_offsetAngle+45*mat_button_angle;
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

float lineFunction(float start, float end, float normalizedPos){
	return (start - end)*normalizedPos+start;
}

float func(float x){
	return sin(mat_freqAnim*PI*x+mat_timeOffset);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{

	int finalPointCount = mat_beamModeButt?(500/int(mat_beamAmount*mat_shapeAmount))*int(mat_beamAmount*mat_shapeAmount):500;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	userData.x = mat_shapeAmount+int(mat_beamModeButt);

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFrameShapeAmount = int(lastFrameUserData.x);

	
	int pointsPerShape = finalPointCount / mat_shapeAmount;
	int gapBeam = pointsPerShape/mat_beamAmount;
	shapeNumber = mat_beamModeButt?pointNumber/gapBeam:1;

	if (shapeNumber >= mat_shapeAmount*(mat_beamModeButt?pointNumber:1.)) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	if (mat_beamModeButt && pointNumber%gapBeam!=1){
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = (normalizedPosInShape-0.5)*2;
	
    float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMax = (normalizedPos-0.5)*2;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset05)*2-1;
	float normalizedPosMinY = (normalizedPos-mat_timeOffset05)*2-1;
	
	float xPath = 0;

	int mat_points = mat_shapeAmount;

	float yPath = 0;

	int i = int(normalizedPos*float(mat_points));

	float start = func(i/float(mat_points));
	float end = func((i+1)/float(mat_points));
	
	float phase = 0.2;
	if(mat_buttonRamp==1){phase = 0;}
	if(mat_buttonRamp==2){phase = 1;}

	yPath = (end-start)*fract(normalizedPos*phase*mat_shapeAmount)+start;
		
	pos = vec2(
		normalizedPosMax * mat_shapeSize
		,yPath
	);
	pos[1]*=mat_ampAnimation;
	pos*=mat_size;
	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),floor(normalizedPos*mat_shapeAmount)/mat_shapeAmount));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*1*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (mat_shapeAmount+int(mat_beamModeButt) == lastFrameShapeAmount) {
			pos = mix(pos,lastFramePos,mat_feedback);
		} else {
			lastFramePos = vec2(mat_shift);
			pos = mix(pos,lastFramePos,mat_feedback);
		}
	}
}
