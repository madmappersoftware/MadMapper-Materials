/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "The flower visual is a parametric function",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"},  
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the visual"},
		{"LABEL": "Global/Size", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": .7, "DESCRIPTION":"Global size"},  
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 8, "MIN":0,"MAX":16, "DESCRIPTION":"To select the amount of cones"},
		{"LABEL": "Global/Shape Width", "NAME": "mat_shapeLength", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"To resize the shapes"},  
		
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Reverse", "NAME": "mat_animationReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To invert the animation"},
		{"LABEL": "Animation/Beam Mode", "NAME": "mat_beamModeButt", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To convert every shapes as a beam"}, 
		
		{"LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The auto rotate is an animation which make the whole visual rotatating"},
		{"LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 3.0, "DESCRIPTION":"To adjust the auto rotate speed"},
		{"LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To invert the spin"},
		{"LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"The auto rotate strobe will activate and stop the rotation animation according to a duration named strobe"},
		
		{"LABEL": "Custom Shape/Petals", "NAME": "mat_petal", "TYPE": "int", "DEFAULT": 1., "MIN": 1., "MAX": 5.,"DESCRIPTION":"To multiply the frequencies"}, 
		{"LABEL": "Custom Shape/Adjustements", "NAME": "mat_morph2", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [1.,1.], "DEFAULT": 0.,"DESCRIPTION":"To spin the 3D object"}, 
		{"LABEL": "Custom Shape/Chaos", "NAME": "mat_morph3", "TYPE": "point2D", "MIN": [0.001,0.001], "MAX": [2.,2.], "DEFAULT": [1.,1.],"DESCRIPTION":"To multiply frequences in the parametric formula"},
		{"LABEL": "Custom Shape/Line Width", "NAME": "mat_lineWidth", "TYPE": "float", "DEFAULT": 1., "MIN": 0.0, "MAX": 10.,"DESCRIPTION":"To adjust the definition of the parametric function"}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Pair", "VALUES": ["Main","Grad","Osc","Pair","Odd","Shape","Sin 2","Sin 4","Sin 8","Inv 2","Inv 4","Inv 8"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color on each shapes, Osc : Oscillate between main and secondary color in time, Pair/Odd : color configuration for shapes, Shape: gradient color per shape, Sin : Oscillate between main and secondary color with a sin, Inv : like sin but backward", "FLAGS": "button_grid" },
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
		{"NAME": "mat_timeRotate","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
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

const int pointsForPrecision[10] = int[10](1,2,3,4,5,6,7,8,9,500);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int finalPointCount = pointsForPrecision[mat_precision-1]*((mat_precision==10)?1:(mat_shapeAmount));//4 + (mat_precision/10)*(500-4));

	// Write last point count in userData so we disable feedback when changing point count
	userData.x = finalPointCount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);


	float normalizedPos = float(pointNumber)/(finalPointCount-2)*mat_lineWidth;

	int shapeSize = int((1-mat_shapeLength)*finalPointCount);
	if(mat_beamModeButt){shapeSize = finalPointCount-1-mat_shapeAmount;}

	int shapeNumberCalc = int(normalizedPos*mat_shapeAmount*finalPointCount)%finalPointCount;

	if(shapeNumberCalc<shapeSize){shapeNumber=-1;}
	else{shapeNumber=int(normalizedPos*mat_shapeAmount);}
	
	if(mat_shapeLength==1. && !mat_beamModeButt){shapeNumber=1;}

	float normalizedPosMax = normalizedPos*2+mat_timeOffset;

	pos = vec2(
		sin(PI*(normalizedPosMax-(mat_autorotateactive?mat_timeRotate:0)))*sin((mat_morph3[0]+mat_petal-1)*2*PI*normalizedPosMax-mat_morph2[0]*PI),
		sin(PI*(normalizedPosMax+0.5-(mat_autorotateactive?mat_timeRotate:0)))*sin((mat_morph3[1]+mat_petal-1)*2*PI*(normalizedPosMax+0.5-mat_morph2[1]/2.))
	);
	pos *= mat_shapeSize;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_offsetGeneral[0];
	pos[1]+=mat_offsetGeneral[1];

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1.-fract(normalizedPos*mat_shapeAmount)));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*2*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(shapeNumber%2==0)?0.:1.));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),((shapeNumber/2)%2==0)?0.:1.));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),floor(normalizedPos*mat_shapeAmount)/(mat_shapeAmount-(mat_shapeAmount==1?0:1))));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*2*PI*normalizedPos)+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*4*PI*normalizedPos)+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*8*PI*(normalizedPos))+1));
	}
	else if(mat_button_grid==9){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*2*PI*(normalizedPos+mat_timeOffset))+1));
	}
	else if(mat_button_grid==10){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*4*PI*(normalizedPos+mat_timeOffset))+1));
	}
	else if(mat_button_grid==11){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(mat_shapeAmount*8*PI*(normalizedPos+mat_timeOffset))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
