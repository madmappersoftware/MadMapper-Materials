/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "A cross bouncing with a modifier which creates movement and overpassing lasers",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.6, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Length", "NAME": "mat_length", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"To adjust the length of the line"},  
		
		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0, "MAX": 1., "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Mirror", "NAME": "mat_twoLinesButton", "TYPE": "bool", "DEFAULT": true,"FLAGS":"button", "DESCRIPTION":"To add one line to complete the cross"},
		{"LABEL": "Animation/Offset", "NAME": "mat_bounceOffset", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. , "DESCRIPTION":"To add an offset in the formula to avoid the beam for example"}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Sin 2","DEFAULT":"Sin 2", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Pulse","Pulse2","BiPulse"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Sin : Oscillate between main and secondary color with a sinus and time phasing, Pulse : Sending color impulses in the visual", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeFlow", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffsetMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_speedOffsetMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOffset", "value2":1, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
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
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);
	if (base_rotation!=0) {
		float angle = base_rotation * 2*PI / 360;

		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
								 -sin_factor, cos_factor, 0,
								 0, 0, 1);
	}

	return linePatternsMatrix;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	int shapeAmount = int(mat_twoLinesButton)+1;
	int pointsPerShape = pointCount/shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;

	if (shapeNumber >= shapeAmount) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 3.*mat_length * (normalizedPosInShape - 0.5);
	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	float normalizedPos = float(pointNumber)/(pointCount-1);
	float offset = 10;

	float osc = cos(0.1*PI*(normalizedPosInShapeMax-mat_timeOffset))*(0.5-mat_bounceOffset/2)+0.5+mat_bounceOffset/2;
	float oscOffseted = cos(0.1*PI*(normalizedPosInShapeMax-mat_timeOffset))*(0.5-mat_bounceOffset/2)+0.5+mat_bounceOffset/2;
	float instance = normalizedPosInShapeMax;
	
	float x = instance*osc+(sin(PI*instance))*oscOffseted;
	float y = (sin(PI*instance))*osc+instance*oscOffseted;
	float shapeNumberOSC = 2*(shapeNumber-0.5);

	pos = vec2(
		x * shapeNumberOSC*(sin(1*PI*mat_timeFlow))
		,y *(sin(1*(PI+1)*.4*mat_timeFlow))
	);
	pos*=mat_size*.64;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_offsetGeneral[0];
	pos[1]+=mat_offsetGeneral[1];

	float normalizedPosMax = sin(abs(PI*normalizedPosInShapeMax)+mat_timeOffset)*0.5+0.5;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-fract(normalizedPos*(int(mat_twoLinesButton)+1))));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*1*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax = sin(abs(2.*PI*normalizedPosInShapeMax)+mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax = sin(abs(4.*PI*normalizedPosInShapeMax)+mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(PI*normalizedPosInShapeMax+mat_timeOffset*10.))*0.5+0.5));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(2.*PI*normalizedPosInShapeMax+mat_timeOffset*20.))*0.5+0.5));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(2.*PI*normalizedPosInShapeMax+sin(mat_timeOffset)*40.))*0.5+0.5));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,feedback);
}
