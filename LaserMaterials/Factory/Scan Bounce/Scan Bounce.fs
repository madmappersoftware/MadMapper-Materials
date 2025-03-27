/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "A basic scanner",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_sizeGlobal", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},

		{"LABEL": "Shape Manager/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 10 , "DESCRIPTION":"To choose the amount of line in the visual"},
		{"LABEL": "Shape Manager/Line Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.5, "DESCRIPTION":"To adjust the size of the lines"},  
		{"LABEL": "Shape Manager/Line Spread", "NAME": "mat_lineSpread", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"To adjust the gap betweend the lines"},

		{"LABEL": "Auto Bounce/Active", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the bounce animation of the lines, LIKE A TRAMPOLINE"},
		{"LABEL": "Auto Bounce/Speed", "NAME": "mat_speedBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.3, "DESCRIPTION":"To adjust the speed of the bounce animation"},
		{"LABEL": "Auto Bounce/Amplitude", "NAME": "mat_ampBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1., "DESCRIPTION":"To adjust the amplitude of the bounce animation"},
		{"LABEL": "Auto Bounce/Offset", "NAME": "mat_offsetBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0., "DESCRIPTION":"To adjust the offset of the bounce animation"},
		
		{"LABEL": "Color/Grid", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Shape","Osc","Sin 2","Inv 2","Sin 4","Sin 8","Sin 16","Sin 32"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. , "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},
		{"LABEL": "Color/Size", "NAME": "mat_sizeColor", "TYPE": "float", "DEFAULT": .5, "MIN": 0.02, "MAX": 1., "DESCRIPTION":"To adjust the Sin offset "},

		{"LABEL": "Color Oscillator/Active", "NAME": "mat_colorOscActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Color Oscillator/Speed", "NAME": "mat_speedOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Color Oscillator/Amplitude", "NAME": "mat_ampOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 },
		{"LABEL": "Color Oscillator/Phase", "NAME": "mat_phaseOscColor", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0. },

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},	
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_ColorMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOscColor", "value2":10, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeBounce", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedBounce","speed_curve": 3,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_ColorMultiplied","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOsc", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOsc","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSize", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedLineSize","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscRL", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedRL","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_rot_position","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 1000
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId)
{
	float base_rotation = 45*mat_button_angle+mat_offsetAngle+90;
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
	int lastFrameShapeAmount = int(lastFrameUserData.x);

	int pointsPerShape = pointCount / mat_shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;
	if (shapeNumber >= mat_shapeAmount) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2 * normalizedPosInShape - 1;

	float normalizedPos = float(pointNumber)/(pointCount-1);

	float size = mat_size;
	
	float customShapeNumber = 0;
	float lineSpreadCustom = 0;

	/*if(mat_button_lineMod == 0){
		lineSpreadCustom = (mat_lineSpread/(floor(float(mat_shapeAmount==1?2:mat_shapeAmount)/2))); //managing instances out the bands by adjusting the line spread to the first line or the last line
		if (shapeNumber % 2 != 0){
			customShapeNumber = (shapeNumber+1)/2; //impaire
		}
		else{customShapeNumber = -shapeNumber/2;} //paire
	}*/

	lineSpreadCustom =(mat_lineSpread/(mat_shapeAmount % 2 != 0?mat_shapeAmount:mat_shapeAmount-1))-0.000001; //managing shape instances out the bands by adjusting the line spread to the first line or the last line
	if (shapeNumber % 2 != 0) {
		customShapeNumber =-shapeNumber; //impaire
	}
	else{customShapeNumber = (shapeNumber)+1;} //paire

	pos = vec2(
		normalizedPosInShapeMax*size									//size
		
		,customShapeNumber*lineSpreadCustom		//shape spread
		*(mat_bounceActivated?(mat_ampBounce*sin(2*PI*(mat_timeBounce))+mat_offsetBounce):1.)	//bounce
		
	);

	float normalizedShapeId = float(shapeNumber)/mat_shapeAmount;

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = (vec3(pos,1) * transformMatrix).xy;
	pos = vec2(pos[0]+mat_offsetGeneral[0],pos[1]+mat_offsetGeneral[1]);

	pos *= mat_sizeGlobal;
  
	
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
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),float(shapeNumber)/(mat_shapeAmount-0.99999)));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeBounce*2*PI+colorOsc)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(mat_shapeAmount*2*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_secondaryColor.rgb,mat_alpha),vec4(mat_mainColor.rgb,1.),(cos(mat_shapeAmount*2*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(mat_shapeAmount*4*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(mat_shapeAmount*8*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(mat_shapeAmount*16*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(cos(mat_shapeAmount*32*PI*normalizedPos+colorOsc)+1)*sizeColor));
	}
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) {
		if (mat_shapeAmount == lastFrameShapeAmount) {
			pos = mix(pos,lastFramePos,mat_feedback);
		} else {
			lastFramePos = vec2(mat_offsetGeneral);
			pos = mix(pos,lastFramePos,mat_feedback);
		}
	}
}
