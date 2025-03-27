/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Beam Wave -Jean Schneider /1024 mod, circuit bent style",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": 1, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Beam Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 10, "DESCRIPTION":"To choose the amount of beam"},
		
		{"LABEL": "Animation/Animation", "NAME": "mat_animationX", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the animation which is here a sinus with a phase"},
		{"LABEL": "Animation/Amplitude", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"To adjust the amplitude of the animation which is here a sinus with a phase"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0, "MAX": 1., "DESCRIPTION":"To adjust the speed of the animation which changes the phase speed inside the sinus"},
    	{"LABEL": "Animation/Length", "NAME": "mat_Length", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 1., "DESCRIPTION":"To adjust the size of the sinus"}, 
		{"LABEL": "Animation/Adjustements", "NAME": "mat_adjustement", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [2.,2.], "DEFAULT": [0.7,0.45], "DESCRIPTION":"The animation is controled by a sin function, you can adjust the parameters of the function. x axe : amplitude of a sinus, y axe : amplitude of a different sinus"},
		{"LABEL": "Animation/Vibration", "NAME": "mat_ampVibration", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0., "DESCRIPTION":"To add a global vibration on the animation"},
		
		{"LABEL": "Bounce/Activate Bounce", "NAME": "mat_animationY", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The bounce animation will add some up and down organic movement according to the -animation/amplitude- variable and the -bounce/height-. If the animation is off, the bounce will only depends on the -Heigth variable-"},
		{"LABEL": "Bounce/Speed", "NAME": "mat_speedY", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0, "MAX": 1. , "DESCRIPTION":"To adjust the speed of the bounce"},
    	{"LABEL": "Bounce/Height", "NAME": "mat_sizeBounce", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": 0.5, "DESCRIPTION":"The bounce animation will add some up and down organic movement according to the -animation/amplitude- variable and the -bounce/height-. If the animation is off, the bounce will only depends on the -Heigth variable-"}, 
		
        {"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","1:1","Pulse","Pulse2","Inv"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin, 1:1 : alternates for each beam, Loop : Pulse of colors in loop", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. , "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
    ],

    "GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeVibration", "TYPE": "time_base", "PARAMS": {"speed": "mat_ampVibration","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeY", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedY","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
],
	"RENDER_SETTINGS": {
		"POINT_COUNT": "mat_amount",
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
      linePatternsMatrix *= mat3(sin_factor, cos_factor, 0,
                                 -sin_factor, cos_factor, 0,
                                 0, 0, 1);
    }

    return linePatternsMatrix;
}


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	int mat_shapeAmount = mat_amount;

    float normalizedPos = float(pointNumber)/(pointCount-(mat_amount!=1?1:0));
	float normalizedPosMaxX = mat_amount!=1?(normalizedPos)*2-1:0;
	float normalizedPosMaxY = (normalizedPos-mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPos+mat_timeOffset)*2-1;

	int shapeSize = int((1)*mat_amount);
	shapeSize = mat_amount-1-mat_shapeAmount;

	int shapeNumberCalc = int(normalizedPos*mat_shapeAmount*mat_amount)%mat_amount;

	if(shapeNumberCalc<shapeSize/2 || shapeNumberCalc>mat_amount-shapeSize/2){shapeNumber=-1;}
	else{shapeNumber=int(normalizedPos*mat_shapeAmount);}

	pos = vec2(
		normalizedPosMaxX
		,mat_animationX?
		mat_shapeSize*(tan(PI*normalizedPosMaxY)*mat_adjustement[1]+sin(mat_adjustement[0]*PI*normalizedPosMaxY))
		+sin(1.2*PI*mat_timeVibration*50)*mat_ampVibration
		:0

	);
	pos[1] *= (1/(1+mat_adjustement[1]))*0.9;
	pos[1] += mat_animationY?sin(2*PI*mat_timeY)*((mat_size-(mat_animationX?mat_shapeSize:0.)))*mat_sizeBounce:0;
	
	pos[0] *= mat_Length;
	pos *= mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
  	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0] += mat_offsetGeneral[0];
	pos[1] += mat_offsetGeneral[1];

	float normalizedColorPulse = (normalizedPos-mat_timeOffset*.02)*2-1;
	float normalizedColorInvPulse = (normalizedPos+mat_timeOffset*.01)*2-1;
	float normalizedColorPulseDouble = (normalizedPos-mat_timeOffset*.04)*2-1;
	
	if(mat_button_grid==0){
    	color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeY*1*PI)+1)/2));
	}
	else if(mat_button_grid==3){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_amount*mat_shapeAmount*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==4){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2.*mat_amount*mat_shapeAmount*PI*normalizedPosMaxX+PI)+1));
	}
	else if(mat_button_grid==5){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),shapeNumber%2));
	}
	else if(mat_button_grid==6){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2.*PI*(normalizedPos-normalizedColorPulse))+1));
	}
	else if(mat_button_grid==7){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*4.*PI*(normalizedPos-normalizedColorPulseDouble))+1));
	}
	else if(mat_button_grid==8){
    	color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2.*PI*(normalizedPos-normalizedColorPulseDouble*4.))+1));
	}
	
	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
	
}
