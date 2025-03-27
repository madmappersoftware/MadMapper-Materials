/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "This a sinus that can be splitted into pieces",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_sizeGeneral", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},

		{"LABEL": "Shapes/Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 2, "MIN":1,"MAX":10, "DESCRIPTION":"To choose the amount of time the shape will be splitted"},
		{"LABEL": "Shapes/Width", "NAME": "mat_shapeLength", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"To adjust the size of every shapes"},
		
		{"LABEL": "Animation/Amplitude", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.2, "DESCRIPTION":"To adjust the amplitude of the animation"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "DEFAULT": 0.15, "MIN": .0, "MAX": 1.0, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Double Deck", "NAME": "mat_doubleDeckButt", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"The double deck is using two sinus and only one without"},
		{"LABEL": "Animation/Frequence", "NAME": "mat_freq", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0., "MAX": 5., "DESCRIPTION":"To adjust the frequency of the main sinus of the animation"},

		{"LABEL": "Beam Mode/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"The beam mod converts the lines into beams"}, 
		{"LABEL": "Beam Mode/Beam Amount", "NAME": "mat_beamAmount", "TYPE": "int", "DEFAULT": 2, "MIN":0,"MAX":8, "DESCRIPTION":"To choose the amount of beams"},
		
		{"LABEL": "Color/Grid", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"1:1", "VALUES": ["Main","Grad","Shape","1:1","2:2","White","Sin 2","Sin 4","Sin 8","Inv 2","Inv 4","Inv 8"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Shape is like -grad- but shape by shape, 1:1 : Oscillate at every shape, 2:2 : Oscillate at every two shape, Sin : Oscillate between main and secondary color with a sin, Inv of colors in loop", "FLAGS": "button_grid" },
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
		{"NAME": "mat_timeRotate","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
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


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int finalPointCount = 500; 

	// Write last point count in userData so we disable feedback when changing point count
	userData.x = finalPointCount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMaxX = (normalizedPos)*2-1;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset)*2-1;

	float shapeSize = 1.-mat_shapeLength;

	float shapeNumberCalc = fract(normalizedPos*mat_shapeAmount);

	int pointsPerShape = int(float(finalPointCount / mat_shapeAmount)*mat_shapeLength);

	int gapBeam = pointsPerShape/mat_beamAmount;

	float lengthShape = ((1./float(mat_shapeAmount))*mat_shapeLength)/mat_beamAmount;

	int gapBeamBis = int(float(finalPointCount)*lengthShape);

	if(shapeNumberCalc>shapeSize/2. && shapeNumberCalc<1.-shapeSize/2.){
		if(!mat_beamModeButt){
			shapeNumber=int(normalizedPos*mat_shapeAmount);
		}
		if(mat_beamModeButt){
			if(pointNumber%gapBeamBis==0){
				shapeNumber=int(normalizedPos*100*mat_shapeAmount)*(mat_beamAmount);
			}
			else{
				shapeNumber=-1;
			}
		}
	}
	else{shapeNumber=-1;}

	pos = vec2(
		normalizedPosMaxX
		,sin(normalizedPosMaxY*mat_freq*PI)
	);
	if(mat_doubleDeckButt){pos[1]*=sin(2*normalizedPosMaxY*mat_freq*PI)*1.3;}
	pos[1]*=mat_size;
	pos*=mat_sizeGeneral;

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
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),floor(normalizedPos*mat_shapeAmount)/(mat_shapeAmount-(mat_shapeAmount==1?0:1))));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),((shapeNumber/(mat_beamModeButt?100:1))%2==0)?0.:1.));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),((shapeNumber/2)%2==0)?0.:1.));
	}
	else if(mat_button_grid==5){
		color = vec4(vec4(1.));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==9){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==10){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4*PI*(normalizedPos-normalizedPosMaxY))+1));
	}
	else if(mat_button_grid==11){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*8*PI*(normalizedPos-normalizedPosMaxY))+1));
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
