/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Rotating and breathing polygon",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Offset Angle", "NAME": "mat_offsetAngle", "TYPE": "float", "MIN": 0., "MAX": 360, "DEFAULT": 0., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Corners", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT":5, "DESCRIPTION":"To select the amount of corners"}, 
		{"LABEL": "Global/Mod", "NAME": "mat_buttonMod", "TYPE": "long", "DEFAULT":"Polygon", "VALUES": ["Polygon","Beam","Circle"], "DESCRIPTION":"Shape", "FLAGS": "button_grid", "DESCRIPTION":"To select the polygone mod, Polygone : creating a normal polygone with"},
		
		{"LABEL": "Animation/Activate", "NAME": "mat_animeActivated", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"The animation is a pivoting the polygon"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": 0.2, "DESCRIPTION":"To adjust the speed of the rotation"},
		{"LABEL": "Animation/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To reverse the spin"},
		
		{"LABEL": "Breath/Size Breath", "NAME": "mat_breathButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"The breath animation is an oscillator changing the alpha value of the visual"},
		{"LABEL": "Breath/Color Breath", "NAME": "mat_breathActivated", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"The breath animation is an oscillator changing the alpha value of the visual"},
		{"LABEL": "Breath/Speed", "NAME": "mat_speedBreath", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": .1, "DESCRIPTION":"To adjust the speed of the breathing animation"},
		{"LABEL": "Breath/Offset", "NAME": "mat_strobePower", "TYPE": "float", "MIN": 0, "MAX": 1., "DEFAULT": 0.1, "DESCRIPTION":"To adjust the offset of the breathing animation to avoid the full black mod"}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Osc","Pulse","Sin","Sin 2","Pulse x2"], "DESCRIPTION":"Shape", "FLAGS": "button_grid", "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Osc : Oscillates between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin,Pulse is sin impulsion", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Speed", "NAME": "mat_speedColor", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT":0.3, "DESCRIPTION":"To adjust the speed of the color animation"},
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ .0, .0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_autorotatereverse","speed_curve": 3,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeBreath", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier2","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier3","speed_curve": 4,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier3","TYPE": "multiplier","PARAMS": {"value1": "mat_speedColor", "value2": 3, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOffset", "value2": 3, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier2","TYPE": "multiplier","PARAMS": {"value1": "mat_speedBreath", "value2": 5, "value3": 1, "value4": 1}}
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
	float base_rotation =mat_offsetAngle;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);

	// Rotate
	if ((base_rotation!=0)) {
		float angle = base_rotation * 2*PI / 360;
		if (true) {
		angle += fract(0.5 + (0)) * 2*PI;
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

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	// Store amount in user data so we can handle changing amount when applying feedback
	userData.x = mat_amount;
	userData.y = mat_buttonMod;

	int pointsPerPart = pointCount / mat_amount;
	int partNumber = pointNumber / pointsPerPart;

	float normalizedPos;

	if(mat_buttonMod == 0){
		//polygons
		if (pointNumber%pointsPerPart==0) {
			// Make a beam
			shapeNumber = 1000;
			normalizedPos = float(pointNumber)/(pointCount-1);
		} else if (pointNumber%pointsPerPart==1) {
			shapeNumber = partNumber;
			normalizedPos = float(partNumber)/mat_amount;
		} else if (pointNumber%pointsPerPart==2) {
			shapeNumber = partNumber;
			normalizedPos = float(partNumber+1)/mat_amount;
		} else {
			shapeNumber = -1;
			return;
		}
	} else if(mat_buttonMod == 1){
		//beam
		if (pointNumber%pointsPerPart==0) {
			shapeNumber = partNumber;
			normalizedPos = float(pointNumber)/(pointCount-1);
		} else {
			shapeNumber = -1;
			return;
		}
	} else if(mat_buttonMod == 2){
		//cercle beam
		normalizedPos = float(pointNumber)/(pointCount-1);
		if (pointNumber%pointsPerPart==0) {
			shapeNumber = 1000; // Make a beam
		} else {
			shapeNumber = pointNumber / pointsPerPart;
		}
	}

	pos = vec2(
		sin(PI*(normalizedPos-0.5)*2.+(mat_animeActivated?mat_timeOffset:0))*(mat_breathButt?(0.5+sin(PI*mat_timeBreath)*(0.5*(1-mat_strobePower))+mat_strobePower/2.):1.)
		,cos(PI*(normalizedPos-0.5)*2.+(mat_animeActivated?mat_timeOffset:0))*(mat_breathButt?(0.5+sin(PI*mat_timeBreath)*(0.5*(1-mat_strobePower))+mat_strobePower/2.):1.)
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
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeColor*PI)+1)/2));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(fract(2.*normalizedPos+mat_timeColor/8.)*2*PI)));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1.*cos(2.*PI*normalizedPos+mat_timeOffset+mat_timeColor/2.)));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),4.*cos(2.*PI*normalizedPos+mat_timeOffset+mat_timeColor/2.)));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(1.*PI*fract(normalizedPos*2.)+mat_timeColor/1.)));
	}

	if(mat_breathActivated){color=vec4(color[0],color[1],color[2],color[3]*(0.5+sin(PI*mat_timeBreath)*(0.5*(1-mat_strobePower))+mat_strobePower/2.));}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	
	
	float feedback = -pow(mat_feedback-1.,2)+1.;
	if(FRAMEINDEX > 0) {
		vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
		int lastFramePointCount = int(lastFrameUserData.x);
		int lastFrameButtonMode = int(lastFrameUserData.y);

		if (mat_buttonMod == lastFrameButtonMode) {
			vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
			if (mat_amount == lastFramePointCount) {
				pos = mix(pos,lastFramePos,feedback);
			} else {
				vec2 lastFramePos = vec2(mat_shift);
				pos = mix(pos,lastFramePos,feedback);
			}
		}
	}
}
