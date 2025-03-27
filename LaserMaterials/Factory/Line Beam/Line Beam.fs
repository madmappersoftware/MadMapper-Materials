/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "This is a line pattern that can transfor into a beam pattern",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.6 , "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the lines teleportations"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"},  
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Offset Angle", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},
		{"LABEL": "Global/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 3, "MIN":1,"MAX":10, "DESCRIPTION":"To select the amount of lines"},
		{"LABEL": "Global/Line Size", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"To adjust the shapes size"}, 
		{"LABEL": "Global/Shape Angle", "NAME": "mat_shapeAngle", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},
		
		{"LABEL": "Animation/Mod", "NAME": "mat_buttonMod", "TYPE": "long", "DEFAULT":"Right", "VALUES": ["Left","Smooth","Right"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the animation pattern"}, 
		{"LABEL": "Animation/Speed", "NAME": "mat_speedAnimation", "TYPE": "float", "DEFAULT": 0.2, "MIN": .0, "MAX": 1.0, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Auto Phase", "NAME": "mat_phased", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"The auto phase function will put all the lines at an equal distance in the animation path, adjust the phase manualy by unable it and use the -phase- variable"},  
		{"LABEL": "Animation/Phase", "NAME": "mat_phase", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": 0.,"FLAGS": "button", "DESCRIPTION":"To adjust phase of every shapes in the animation path, to have a equidistant phase : put -phase- at 0 and activate the -auto phase- button"}, 
		
		{"LABEL": "Beam/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"The beam function will convert a line into a serie of beams"},
		{"LABEL": "Beam/Beam Amount", "NAME": "mat_beamAmount", "TYPE": "int", "MIN": 0, "MAX": 10, "DEFAULT": 5, "DESCRIPTION":"To adjust the amount of beam"}, 
		{"LABEL": "Beam/Speed", "NAME": "mat_speedBeam", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the speed of the beam animation"},

		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Fast","Fast 2","Fast 3"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin, Fast: color translation at different speed", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
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
		{"NAME": "mat_timeBeamMultiplied", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier5","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timePath", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier2","TYPE": "multiplier","PARAMS": {"value1": "mat_speedAnimation", "value2": 2, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier5","TYPE": "multiplier","PARAMS": {"value1": "mat_speedBeam", "value2": 10, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier05","TYPE": "multiplier","PARAMS": {"value1": "mat_speedAnimation", "value2": 0.5, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOutAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Out", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier05","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
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


mat3 makeTransformMatrix(float normalizedCellId,int shapeNumber)
{
	float base_rotation = 45*mat_button_angle+90+mat_offsetAngle+mat_shapeAngle*shapeNumber;
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

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	userData.x = mat_shapeAmount+int(mat_beamModeButt)+mat_beamAmount;

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFrameShapeAmount = int(lastFrameUserData.x);

	int pointsPerShape = pointCount / mat_shapeAmount;
	int gapBeam = pointsPerShape/mat_beamAmount;
	shapeNumber = mat_beamModeButt?pointNumber/gapBeam:pointNumber/pointsPerShape;

	if (shapeNumber >= mat_shapeAmount*(mat_beamModeButt?mat_beamAmount:1.) || (pointNumber%gapBeam!=0 && mat_beamModeButt)) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = (normalizedPosInShape-0.5)*2;
	
	float normalizedPos = float(pointNumber)/(pointCount-1);
	float normalizedPosMaxX = (normalizedPos)*2-1;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPos-mat_timeOffset)*2-1;

	float directionTime = 0;
	if(mat_buttonMod == 0){directionTime = mat_timeOutAnim;}
	if(mat_buttonMod == 1){directionTime = mat_timeSmoothAnim;}
	if(mat_buttonMod == 2){directionTime = mat_timeInAnim;}
	
	float mat_timePathPhased = directionTime - (shapeNumber/int(mat_beamModeButt?mat_beamAmount:1.))*(mat_phased?(1./mat_shapeAmount+mat_phase):mat_phase);

	float xPath = 0;
	float yPath = (fract(mat_timePathPhased)-0.5)*2;

	pos = vec2(
		sin(PI*normalizedPosInShapeMax+mat_timeBeamMultiplied)*mat_shapeSize + xPath
		,yPath
	);
	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos,shapeNumber);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*1*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (mat_shapeAmount+int(mat_beamModeButt)+mat_beamAmount == lastFrameShapeAmount) {
			pos = mix(pos,lastFramePos,mat_feedback);
		} else {
			lastFramePos = vec2(mat_shift);
			pos = mix(pos,lastFramePos,mat_feedback);
		}
	}
}
