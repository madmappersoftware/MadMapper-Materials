/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Line Pattern, use feedback to smooth the line teleportation",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.85, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the animation"},
		{"LABEL": "Global/Size", "NAME": "mat_sizeMax", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Offset Angle", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},

		{"LABEL": "Shape Manager/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 10, "DESCRIPTION":"To select the amount of lines"},
		{"LABEL": "Shape Manager/Line Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"To adjust the animation speed"}, 
		{"LABEL": "Shape Manager/Shape Rotate", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 90.0, "MIN": 0.0, "MAX": 90.0, "DESCRIPTION":"To adjust the offset angle according to the line number"},
		
		{"LABEL": "Animation/Offset", "NAME": "mat_offsetBounce", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.1, "DESCRIPTION":"To adjust the location of the animation. 0 : the animation will be concentrated at the center, 1 : the animation is concentrated in the outside"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedBounce", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3, "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Animation/Auto Offset Oscillator", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The auto zoom animation will oscillate the trigger zone of the animation, Activate it here"},
		
		{"LABEL": "Auto Line Size/Active", "NAME": "mat_lineSizeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"The line sizing mod"},
		{"LABEL": "Auto Line Size/Speed", "NAME": "mat_speedLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Auto Line Size/Offset", "NAME": "mat_offsetLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0. },
		{"LABEL": "Auto Line Size/Phase", "NAME": "mat_phaseLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 0. },
		{"LABEL": "Auto Line Size/Delay", "NAME": "mat_delayLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.5 },

		{"LABEL": "Auto translation/Active", "NAME": "mat_RLactivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{"LABEL": "Auto translation/Amplitude", "NAME": "mat_ampRL", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Auto translation/Speed", "NAME": "mat_speedRL", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Auto translation/Offset", "NAME": "mat_offsetRL", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0. },
		{"LABEL": "Auto translation/Phase", "NAME": "mat_phaseRL", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },

		{"LABEL": "Color/Grid", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Osc", "VALUES": ["Const","Grad","Osc","Sin 2","Inv 2","Sin 4","Sin 8","Sin 16","Sin 32"], "DESCRIPTION":"Shape", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ .0, .0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. },
		{"LABEL": "Color/Size", "NAME": "mat_sizeColor", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.02, "MAX": 1. },

		{"LABEL": "Color Oscillator/Active", "NAME": "mat_colorOscActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Color Oscillator/Speed", "NAME": "mat_speedOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.7 },
		{"LABEL": "Color Oscillator/Amplitude", "NAME": "mat_ampOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 },
		{"LABEL": "Color Oscillator/Phase", "NAME": "mat_phaseOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0. },

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6  },
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },	
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
		{"NAME": "mat_timeBounce", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedBounce","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOscColor","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOsc", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOsc","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSize", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedLineSize","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscRL", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedRL","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_rot_position","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","strob": "mat_autorotatestrob", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
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
	float base_rotation = shapeNumber*mat_autorotateoffset + 45*mat_button_angle + mat_offsetAngle;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);

	// Rotate
	if ((base_rotation!=0)) {
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
	userData.x = mat_shapeAmount;

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFrameShapeCount = int(lastFrameUserData.x);
	
	int shapeAmount = mat_shapeAmount;

	int pointsPerShape = pointCount / shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;
	if (shapeNumber >= shapeAmount) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2 * normalizedPosInShape - 1;

	float normalizedPos = float(pointNumber)/(pointCount-1);

	float size = mat_size;
	
	float customShapeNumber = 0;
	float lineSpreadCustom = 0;

	
	lineSpreadCustom =(1./(shapeAmount % 2 != 0?shapeAmount:shapeAmount-1))-0.000001; //managing shape instances out the bands by adjusting the line spread to the first line or the last line
	if (shapeNumber % 2 != 0) {
		customShapeNumber =-shapeNumber; //impaire
	}
	else{customShapeNumber = (shapeNumber)+1;} //paire
	
	vec2 mat_phase = vec2(0.);
	if(fract(mat_timeBounce)<0.25){
		mat_phase[0]=-1.;
		mat_phase[1]=1.;
	}
	else if(fract(mat_timeBounce)<0.50){
		mat_phase[0]=-1.;
		mat_phase[1]=-1.;
	}
	else if(fract(mat_timeBounce)<0.75){
		mat_phase[0]=1.;
		mat_phase[1]=-1.;
	}
	else{
		mat_phase[0]=1.;
		mat_phase[1]=1.;
	}

	mat_phase[0] = mix(mat_phase[0],0.,-mat_offsetBounce+(mat_bounceActivated?(1.-abs(-mat_offsetBounce))*sin(-PI*mat_timeBounce*0.25):0));
	mat_phase[1] = mix(mat_phase[1],0.,-mat_offsetBounce+(mat_bounceActivated?(1.-abs(-mat_offsetBounce))*sin(-PI*mat_timeBounce*0.25):0));
	
	pos = vec2(
		normalizedPosInShapeMax*size									//size
		*(mat_lineSizeActivated?cos(2*PI*(mat_timeSize+mat_phaseLineSize)+(shapeNumber+1)*mat_delayLineSize)+mat_offsetLineSize:1.)  	//line size
		+(mat_RLactivated?sin(2*PI*(mat_timeOscRL+mat_phaseRL))*mat_ampRL+mat_offsetRL:0.)			//X translate
		
		,customShapeNumber*lineSpreadCustom		//shape spread
		*(bool(shapeNumber)?-mat_phase[0]/2.:mat_phase[1]/2.)
								//bounce
		
	);

	float normalizedShapeId = float(shapeNumber)/shapeAmount;


  
	
	float colorOsc = (2*mat_ampOscColor*sin(2*PI*mat_timeOscColor+2*PI*shapeNumber*mat_phaseOscColor))*int(mat_colorOscActivated)*(mat_button_grid-2);
		
	float sizeColor;
	if(mat_sizeColor == 0.01){
		if(((pointNumber%pointsPerShape)%(int(float(pointsPerShape)/3)))==0){
			color = vec4(mat_mainColor.rgb,mat_alpha);
		}
		else{color = vec4(0.);}
	}
	else{
	sizeColor = 1/mat_sizeColor-1;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),float(shapeNumber)/(shapeAmount-0.99999)));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeBounce*2*PI+colorOsc)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(shapeAmount*2*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_secondaryColor.rgb,mat_alpha),vec4(mat_mainColor.rgb,1.),(cos(shapeAmount*2*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(shapeAmount*4*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(shapeAmount*8*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(shapeAmount*16*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(shapeAmount*32*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	}


	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId,shapeNumber);
	pos = (vec3(pos,1) * transformMatrix).xy;

	
	pos*=mat_sizeMax;
	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (mat_shapeAmount == lastFrameShapeCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = vec2(mat_shift);
			pos = mix(pos,lastFramePos,feedback);
		}
	}
}

