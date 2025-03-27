/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "High speed rotating lines pattern, choose your preset",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0., "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the visual"},
		{"LABEL": "Global/Size", "NAME": "mat_sizeGlobal", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},

		{"LABEL": "Shape Manager/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 10 , "DESCRIPTION":"To adjust the amount of lines"},
		{"LABEL": "Shape Manager/Line Mod", "NAME": "mat_button_lineMod", "TYPE": "long", "DEFAULT":"Mirror", "VALUES": ["Center","Mirror","Group"], "FLAGS": "button_grid", "DESCRIPTION":"To choose de line configuration mod, Center : Add an extra line at the center, Mirror : No extra line in the center, Group : Add an offset so the lines are not facing"},
		{"LABEL": "Shape Manager/Line Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0.5, "DESCRIPTION":"To adjust the size of the lines"}, 
		{"LABEL": "Shape Manager/Line Spread", "NAME": "mat_lineSpread", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"To adjust the gap betweend the lines"}, 
		{"LABEL": "Shape Manager/Shape Angle", "NAME": "mat_offsetAngleShape", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},

		{"LABEL": "Animation/Mod", "NAME": "mat_animMod", "TYPE": "long", "DEFAULT":"1", "VALUES": ["1","2","3","4","5","6","7","8","9"], "FLAGS": "button_grid", "DESCRIPTION":"Each Mod has his own set of variables, choose the one you want"},
		
		{"LABEL": "Auto Line Size/Active", "NAME": "mat_lineSizeActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the line sizing animation controled by an oscillator"},
		{"LABEL": "Auto Line Size/Speed", "NAME": "mat_speedLineSizeMulti", "TYPE": "long", "DEFAULT":"x1", "VALUES": ["x0.5","x1","x2"], "FLAGS": "button_grid", "DESCRIPTION":"To multiply the line sizing speed by 0.5, 1 or 2"},
		
		{"LABEL": "Auto Bounce/Active", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the bounce animation which modulate the -line spread- variable"},
		{"LABEL": "Auto Bounce/Speed", "NAME": "mat_speedBounceMulti", "TYPE": "long", "DEFAULT":"x1", "VALUES": ["x0.5","x1","x2"], "FLAGS": "button_grid", "DESCRIPTION":"To multiply the bounce speed by 0.5, 1 or 2"},
		

		{"LABEL": "Color/Grid", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Grad", "VALUES": ["Const","Grad","Osc","Sin 2","Inv 2","Sin 4","Sin 8","Sin 16","Sin 32"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. , "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},
		{"LABEL": "Color/Size", "NAME": "mat_sizeColor", "TYPE": "float", "DEFAULT": .5, "MIN": 0.02, "MAX": 1., "DESCRIPTION":"To adjust the Sin offset "},

		{"LABEL": "Color Oscillator/Active", "NAME": "mat_colorOscActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Color Oscillator/Speed", "NAME": "mat_speedOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.7 },
		{"LABEL": "Color Oscillator/Amplitude", "NAME": "mat_ampOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 },
		{"LABEL": "Color Oscillator/Phase", "NAME": "mat_phaseOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0. },

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
		{"NAME": "mat_timeBounce", "TYPE": "time_base", "PARAMS": {"speed": 1,"speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOscColor","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSideBounce", "TYPE": "time_base", "PARAMS": {"speed": 1,"speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSize", "TYPE": "time_base", "PARAMS": {"speed": 1,"speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_rot_position","TYPE": "time_base","PARAMS": {"speed": 1,"strob": "mat_autorotatestrob", "speed_curve":1, "link_speed_to_global_bpm":false, "max_value":10000 }},
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

mat3 makeTransformMatrix(float normalizedCellId,float timeSideBounce,float mat_ampSideBounce,float timeAutorotate,float autorotateoffset, int shapeNumber)
{
	float base_rotation = 20*mat_ampSideBounce*sin(2*PI*timeSideBounce)+45*mat_button_angle + mat_offsetAngleShape*shapeNumber;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);

	// Rotate
	if (true) {
		float angle = base_rotation * 2*PI / 360;
		angle += fract(0.5 + (timeAutorotate+normalizedCellId*autorotateoffset)) * 2*PI;
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
	float mat_speedGlobal = 0.75;
	float mat_autorotateoffset = 0;
	int mat_speedRotateMulti = 1;
	float rotSpeed = 3.;	

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	float autorotateoffset = mat_autorotateoffset;
	
	float timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
	float ampBounce = 0.8;
	float offsetBounce = 0.8;
	float phaseBounce = 0.0;

	float timeAutorotate = mat_rot_position*rotSpeed * .1 * mat_speedGlobal*1.;

	float timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
	float offsetLineSize = 0.;
	float phaseLineSize = 1;
	float delayLineSize = 0.0;//int(mat_delayLineSize);

	float timeSideBounce = mat_timeSideBounce * 0.2 * mat_speedGlobal;
	float ampSideBounce = 2;

	if(mat_animMod==0){
		mat_speedGlobal = 0.7;
		timeBounce = mat_timeBounce * 0.5 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.5;
		offsetBounce = 0.51;
		phaseBounce = 0.5;

		timeAutorotate = mat_rot_position*rotSpeed * 0.35 * (mat_speedRotateMulti==0?0.5:mat_speedRotateMulti) * mat_speedGlobal;

		timeSize = mat_timeSize * 0.1 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.;
		phaseLineSize = 0.5;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 0.3 * mat_speedGlobal;
		ampSideBounce = 2.*100.;
	}

	if(mat_animMod==1){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 12.396 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}
	
	if(mat_animMod==2){
		timeBounce = mat_timeBounce * 0.5 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.5;
		offsetBounce = 0.51;
		phaseBounce = 0.5;

		timeAutorotate = mat_rot_position*rotSpeed * 0.35 * (mat_speedRotateMulti==0?0.5:mat_speedRotateMulti) * mat_speedGlobal;

		timeSize = mat_timeSize * 0.1 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.;
		phaseLineSize = 0.5;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 0.3 * mat_speedGlobal;
		ampSideBounce = 2.*100.;
	}

	if(mat_animMod==3){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 462.81 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}

	if(mat_animMod==4){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 57.851 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}

	if(mat_animMod==5){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 57.4 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}

	if(mat_animMod==6){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 20.661 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;

	}

	if(mat_animMod==7){
		timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 177.68 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}

	if(mat_animMod==8){
				timeBounce = mat_timeBounce * 0.2 * mat_speedGlobal * (mat_speedBounceMulti==0?0.5:mat_speedBounceMulti);
		ampBounce = 0.8;
		offsetBounce = 0.8;
		phaseBounce = 0.0;

		timeAutorotate = mat_rot_position*rotSpeed * 210.74 * mat_speedGlobal*1.;

		timeSize = mat_timeSize * 0.2 *(mat_speedLineSizeMulti==0?0.5:mat_speedLineSizeMulti) * mat_speedGlobal;
		offsetLineSize = 0.5;
		phaseLineSize = 0.25;
		delayLineSize = 0.;//int(mat_delayLineSize);

		timeSideBounce = mat_timeSideBounce * 1. * mat_speedGlobal;
		ampSideBounce = 2.;
	}

	int shapeAmount = mat_shapeAmount;
	if(mat_button_lineMod==0){shapeAmount=mat_shapeAmount+1;}
	else if(mat_shapeAmount == 0){shapeAmount=1;}
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
	if(mat_button_angle==1){size*=1.5;} //extend line size when 90 ticked
	
	float customShapeNumber = 0;
	float lineSpreadCustom = 0;

	if(mat_button_lineMod == 0){
		lineSpreadCustom =(mat_lineSpread/(floor(float(shapeAmount==1?2:shapeAmount)/2))); //managing instances out the bands by adjusting the line spread to the first line or the last line
		if (shapeNumber % 2 != 0){
			customShapeNumber = (shapeNumber+1)/2; //impaire
		}
		else{customShapeNumber = -shapeNumber/2;} //paire
	}

	else if(mat_button_lineMod == 1){
		lineSpreadCustom = (mat_lineSpread/(shapeAmount % 2 != 0?shapeAmount:shapeAmount-1))-0.000001; //managing shape instances out the bands by adjusting the line spread to the first line or the last line
		//lineSpreadCustom = mat_lineSpread;
		if (shapeNumber % 2 != 0) {
			customShapeNumber =-shapeNumber; //impaire
			//customShapeNumber /= 3.;
		}
		else{customShapeNumber = (shapeNumber)+1;} //paire
	}

	else if(mat_button_lineMod == 2){
		size/=2;
		lineSpreadCustom = (mat_lineSpread/(shapeAmount % 2 != 0?shapeAmount:shapeAmount-1))-0.000001; //managing shape instances out the bands by adjusting the line spread to the first line or the last line
		//lineSpreadCustom = mat_lineSpread;
		if (shapeNumber % 2 != 0) {
			customShapeNumber =-shapeNumber; //impaire
			//customShapeNumber /= 3.;
		}
		else{customShapeNumber = (shapeNumber)+1;} //paire
	}
	
	pos = vec2(
		normalizedPosInShapeMax*size									//size
		*(mat_lineSizeActivated?cos(2*PI*(timeSize+phaseLineSize/2)+(shapeNumber+1)*delayLineSize*PI)+offsetLineSize:1.)  	//line size
		
		+(mat_button_lineMod==2?(shapeNumber % 2 != 0?-0.5:0.5):0.)		//mod line "group" adding +- offsets
		
		,customShapeNumber*lineSpreadCustom		//shape spread
		*(mat_bounceActivated?(ampBounce*sin(2*PI*(timeBounce+phaseBounce))+offsetBounce):1.)	//bounce
		
	);

	pos *= mat_sizeGlobal;

	float normalizedShapeId = float(shapeNumber)/shapeAmount;

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId,timeSideBounce,ampSideBounce,timeAutorotate,autorotateoffset,shapeNumber);
	pos = (vec3(pos,1) * transformMatrix).xy;
	pos = vec2(pos[0]+mat_offsetGeneral[0],pos[1]+mat_offsetGeneral[1]);
  
	
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
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
