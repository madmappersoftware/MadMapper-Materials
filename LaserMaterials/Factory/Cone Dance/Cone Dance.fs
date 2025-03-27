/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Dancing Cones",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Polygone", "NAME": "mat_precision", "TYPE": "int", "MIN": 2, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"To adjust the amount of faces of each shape, 10 is circle"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.6, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 3, "MIN":1,"MAX":8, "DESCRIPTION":"To select the amount of cones"},
		{"LABEL": "Global/Shape Width", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.2, "DESCRIPTION":"To resize the shapes"}, 

		{"LABEL": "Animation/Speed", "NAME": "mat_speedAnimation", "TYPE": "float", "DEFAULT": 0.2, "MIN": -1.0, "MAX": 1.0, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Auto Phase", "NAME": "mat_phased", "TYPE": "bool", "DEFAULT": true, "FLAGS":"button", "DESCRIPTION":"Auto phase is phasing the shapes equaly in the animation"}, 
		{"LABEL": "Animation/Phase", "NAME": "mat_phase", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": 0.0, "DESCRIPTION":"To adjust the phase of the shapes in the animation, unclick Auto phase to have a normal phasing"}, 
		{"LABEL": "Animation/Path", "NAME": "mat_modButt", "TYPE": "long", "DEFAULT":"8", "VALUES": ["X","8","O","Z","/","V","---","/-/","P"], "DESCRIPTION":"Choose your animation path, each symbol kinda represents the path", "FLAGS": "button_grid" },
		
		{"LABEL": "Beam/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"Beam Mod converts the shapes to an amount of beams. The beams are following the path of the original shape with a speed"}, 
		{"LABEL": "Beam/Beam Amount", "NAME": "mat_beamAmount", "TYPE": "int", "MIN": 0, "MAX": 10, "DEFAULT": 5, "DESCRIPTION":"To select the amount of beam equaly spread on the the previous circle"}, 
		{"LABEL": "Beam/Speed", "NAME": "mat_speedTest2", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0.25 },

		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Pulse","Pulse2","Crazy"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin,Pulse is frequency modulation of the sin, Crazy : is a Sin 256", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
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
		{"NAME": "mat_timeSpeed", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier5","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timePath", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier2","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier2","TYPE": "multiplier","PARAMS": {"value1": "mat_speedAnimation", "value2": 2, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier5","TYPE": "multiplier","PARAMS": {"value1": "mat_speedTest2", "value2": 10, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
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
	
	float base_rotation = 45*mat_button_angle+mat_offsetAngle;
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
	int finalPointCount = pointsForPrecision[mat_precision-1]*(mat_precision==10?1:mat_shapeAmount);

	if(mat_beamModeButt){finalPointCount=500;}

	userData.x = finalPointCount+int(mat_beamModeButt)+mat_shapeAmount;

	if (pointNumber >= finalPointCount && !mat_beamModeButt) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int pointsPerShape = finalPointCount / mat_shapeAmount;
	int gapBeam = pointsPerShape/mat_beamAmount;
	shapeNumber = mat_beamModeButt?pointNumber/gapBeam:pointNumber/pointsPerShape;

	if (shapeNumber >= mat_shapeAmount*(mat_beamModeButt?mat_beamAmount:1.) || (pointNumber%gapBeam!=0 && mat_beamModeButt)) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = (normalizedPosInShape-0.5)*2;
	
	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMaxX = (normalizedPos)*2-1;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPos-mat_timeOffset)*2-1;

	float phase = mat_phased?(1./mat_shapeAmount+mat_phase):mat_phase;
	if(mat_modButt==3){phase*=5;}
	if(mat_modButt==5){phase*=4;}
	
	float timePathPhased = mat_timePath - (shapeNumber/int(mat_beamModeButt?mat_beamAmount:1.))*phase;

	float xPath = 0;
	float yPath = fract(timePathPhased);

	if(mat_modButt==0){
		xPath = -(cos(4*PI*(timePathPhased)))*(1-mat_shapeSize);
		if(yPath < 0.25){yPath = -1*(1-mat_shapeSize);}
		else if(yPath < 0.5){yPath =  cos(4*PI*timePathPhased)*(1-mat_shapeSize);}
		else if(yPath < 0.75){yPath = 1*(1-mat_shapeSize);}
		else{yPath = cos(4*PI*(timePathPhased-0.25))*(1-mat_shapeSize);}
	}

	if(mat_modButt==1){
		xPath = sin(4*PI*timePathPhased)*(1-mat_shapeSize);
		yPath = cos(2*PI*timePathPhased)*(1-mat_shapeSize);
	}

	if(mat_modButt==2){
		xPath = sin(2*PI*(timePathPhased+mat_timePath))*(1-mat_shapeSize);
		yPath = sin(2*PI*(timePathPhased+0.25+mat_timePath))*(1-mat_shapeSize);
	}

	if(mat_modButt==3){
		xPath = cos(2*PI*timePathPhased)*(1-mat_shapeSize);
		yPath = cos(2*PI*timePathPhased/5)*(1-mat_shapeSize);
	}

	if(mat_modButt==4){
		xPath = sin(2*PI*timePathPhased)*(1-mat_shapeSize);
		if(yPath < 0.25){yPath = -1*(1-mat_shapeSize);}
		else if(yPath < 0.5){yPath =  cos(4*PI*timePathPhased)*(1-mat_shapeSize);}
		else if(yPath < 0.75){yPath = 1*(1-mat_shapeSize);}
		else{yPath = cos(4*PI*(timePathPhased-0.25))*(1-mat_shapeSize);}
	}

	if(mat_modButt==5){
		xPath = cos(0.5*PI*timePathPhased)*(1-mat_shapeSize);
		yPath = cos(1*PI*(timePathPhased))*(1-mat_shapeSize);
	}

	if(mat_modButt==6){
		xPath = cos(2.*PI*timePathPhased)*(1-mat_shapeSize);
		yPath = 0.;
	}

	if(mat_modButt==7){
		if(yPath < 0.5){
			xPath = cos(2.*PI*timePathPhased)*(1-mat_shapeSize);
			yPath = 0.;
		}
		else{
			xPath = 0.;
			yPath = cos(2.*PI*timePathPhased)*(1-mat_shapeSize);
		}
	}

	if(mat_modButt==8){
		if(yPath < 0.25){
			xPath = cos(2.*PI*timePathPhased)*(1-mat_shapeSize);
			yPath = 0.;
		}
		else if(yPath < 0.50){
			xPath = 0.;
			yPath = sin(2.*PI*timePathPhased)*(1-mat_shapeSize);
		}
		else if(yPath < 0.75){
			xPath = cos(2.*PI*timePathPhased)*(1-mat_shapeSize);
			yPath = 0.;
		}
		else{
			xPath = 0.;
			yPath = sin(2.*PI*timePathPhased)*(1-mat_shapeSize);
		}
	}

	pos = vec2(
		sin(PI*normalizedPosInShapeMax+mat_timeSpeed)*mat_shapeSize + xPath
		,sin(PI*(normalizedPosInShapeMax)+PI/2+mat_timeSpeed)*mat_shapeSize + yPath
	);

	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(shapeNumber)/float(mat_shapeAmount-1)));
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
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMaxX*cos(mat_shapeAmount*2*PI+mat_timeOffset)))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMaxX*(2.+cos(mat_shapeAmount*2*PI+mat_timeOffset))*2.))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (finalPointCount+int(mat_beamModeButt)+mat_shapeAmount == lastFramePointCount) {	
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = vec2(mat_shift);
			pos = mix(pos,lastFramePos,feedback);
		}
	}
}
