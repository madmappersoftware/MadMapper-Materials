/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Vibration looking visual using feedback to smooth everything",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the visual"},
		{"LABEL": "Global/Size", "NAME": "mat_amp", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 1., "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},

		{"LABEL": "Animation/Amplitude", "NAME": "mat_sizeAmp", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.15, "DESCRIPTION":"To adjust the amplitude of the line"}, 
		{"LABEL": "Animation/Length", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 1., "DESCRIPTION":"To adjust the length of the lines"}, 
		{"LABEL": "Animation/Mod", "NAME": "mat_mod", "TYPE": "long", "DEFAULT":"R/L", "VALUES": ["Static","R/L","Right"], "DESCRIPTION":"To choose the vibration direction Mod", "FLAGS": "button_grid" }, 
		{"LABEL": "Animation/Speed", "NAME": "mat_speedUp", "TYPE": "float", "DEFAULT": 0.12, "MIN": 0, "MAX": 0.5, "DESCRIPTION":"To adjust the speed of the animation"},
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"BiPulse", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Inv 2","Pulse","Pulse 2","BiPulse"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin & Pulse: Oscillates between main and secondary color with a sin and time offset, BiPulse is following the -L/R Mod- variable", "FLAGS": "button_grid" },
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
		
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_speedUp", "value2": 10, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_up", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeRotate","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","strob": "mat_autorotatestrob", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
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
	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;

	int finalPointCount = pointsForPrecision[mat_precision-1];

	shapeNumber = pointNumber/finalPointCount;
	
	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMax = 2 * normalizedPos - 1;

	float oscMod = 0;
	float normalizedPosMaxRight = 0;
	if(mat_mod==0){
		normalizedPosMaxRight = (normalizedPos-0.5)*2;
	}
	if(mat_mod==2){
		normalizedPosMaxRight = (normalizedPos-mat_timeOffset)*2-1;
	}
	if(mat_mod==1){
		normalizedPosMaxRight = (normalizedPos-cos(PI*mat_timeOffset)*0.25)*2-1;
	}
	float normalizedPosMaxLeft = (normalizedPos+mat_timeOffset)*2-1;

	float vibration = sin(normalizedPosMaxRight*(((int(mat_up*1000))%100)/10)*PI);

	pos = vec2(
		normalizedPosMax
		,mat_sizeAmp*vibration
	);
	pos[0]*=mat_size;
	pos*=mat_amp;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	normalizedPosMax =  2 * normalizedPos - 1;
	
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
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(1.*PI*normalizedPosMax)-mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax = sin(abs(2.*PI*normalizedPosMax)-mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax = sin(abs(PI*normalizedPosMax)+mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),vibration+1.));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(0.25*PI*vibration+mat_timeOffset*10.))*0.5+0.5));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(abs(.5*PI*normalizedPosMax-(cos(PI*mat_timeOffset)-0.5)*0.5))));
	}

	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
