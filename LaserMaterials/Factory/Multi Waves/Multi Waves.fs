/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Using a multiple scrolling sinus",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"}, 
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid","DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 3,"DESCRIPTION":"To choose the amount of shapes"},
		{"LABEL": "Global/Lines Spread", "NAME": "mat_spread", "TYPE": "float", "MIN": 0, "MAX": 1., "DEFAULT": 0.03,"DESCRIPTION":"To adjust the distance between the waves"},
		
		{"LABEL": "Auto Slide/Speed", "NAME": "mat_freqSpawn", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.3 ,"DESCRIPTION":"The auto slide is translating on the y axis the visual, adjust here the speed"},
		{"LABEL": "Auto Slide/Reverse", "NAME": "mat_slideReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button","DESCRIPTION":"The auto slide is translating on the y axis the visual, change the direction here"},
		
		{"LABEL": "Animation/Amplitude", "NAME": "mat_ampAnimation", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.17,"DESCRIPTION":"To adjust the amplitide of the waves"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedAnimation", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0.0, "MAX": 1,"DESCRIPTION":"To adjust the speed of the waves"},
		{"LABEL": "Animation/Reverse", "NAME": "mat_animationReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button","DESCRIPTION":"To choose the direction of the waves"},
		{"LABEL": "Animation/Adjustements", "NAME": "mat_adjustement", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [1.,1.], "DEFAULT": [0.5,0.],"DESCRIPTION":"To adjust somes parameters of the sinusoid, x : frequence, y : adding other sinus"},
		{"LABEL": "Animation/Tilte", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. }, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Sin 2", "VALUES": ["Main","Grad","1:1","Sin 2","Sin 4","Sin 8","Inv 2","Inv 4","Inv 8"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, 1:1 is an alternation between 1 waves of main colors and 1 secondary, Sin & Inv : Oscillate between main and secondary color with a sin", "FLAGS": "button_grid" },
		{ "LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{ "LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{ "LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},	
	],
	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_up", "TYPE": "time_base", "PARAMS": {"speed": "mat_freqSpawn","reverse": "mat_slideReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		//{"NAME": "mat_up", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_up2", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_upColor", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_upMultipled","TYPE": "multiplier","PARAMS": {"value1": "mat_up", "value2": 0.1}},
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
	int pointsPerShape = finalPointCount/mat_shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2. * normalizedPosInShape - 1.;
	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	float normalizedPosMaxX = (normalizedPosInShape)*2-1;
	float normalizedPosMaxY = (normalizedPosInShape+mat_timeOffset)*2-1;
	float normalizedPosMaxYColor = (normalizedPosInShape+2*mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPosInShape-mat_timeOffset)*2-1;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);

	float offsetCalc = 0;

	float mat_upLoop = (2+(mat_spread)*12)*(fract(mat_up-(1./mat_shapeAmount)*shapeNumber)-0.5);
	float y = mat_upLoop+mat_ampAnimation*(sin(PI*normalizedPosMaxY)*mat_adjustement[1]*4.+sin(mat_adjustement[0]*5.*PI*normalizedPosMaxY))+sin(0.5*PI*normalizedPosMinY)*.1;

	pos = vec2(
		mix(normalizedPosInShapeMax,y,mat_rotation)*(1.+mat_adjustement[1]/1.5)
		,mix(y,normalizedPosInShapeMax,mat_rotation)*(1.+mat_adjustement[1]/1.5)
	);

	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];
	
	float normalizedPosMax = sin(abs(PI*normalizedPosInShapeMax)-3.*mat_timeOffset)*0.5+0.5;	

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(PI*normalizedPos)));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),shapeNumber%2==0?0:1));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),normalizedPosMax));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(2.*PI*normalizedPosInShapeMax)-3.*mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(abs(4.*PI*normalizedPosInShapeMax)+10.*mat_timeOffset)*0.5+0.5));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(PI*(normalizedPos-normalizedPosMaxYColor+1))));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(2*PI*(normalizedPos-normalizedPosMaxYColor+1))));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(4*PI*(normalizedPos-normalizedPosMaxYColor+1))));
	}
	color[3]=color[3]*sin(PI*(y+1)*.5);
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	//if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
