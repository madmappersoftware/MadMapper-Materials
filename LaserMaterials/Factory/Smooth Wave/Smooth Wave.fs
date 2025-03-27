/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider",
	"DESCRIPTION": "Multi sinusoid scrolling",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"The resolution defines the quantization of your visual"}, 
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0., "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the visual"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Offset Angle", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},
		
		{"LABEL": "Animation/Auto Move", "NAME": "mat_animationShape", "TYPE": "long", "VALUES": ["Smooth","In","Out"], "DEFAULT": "In","FLAGS": "button_grid", "DESCRIPTION":"To adjust the mod of the auto move animation"},
		{"LABEL": "Animation/Amplitude", "NAME": "mat_ampAnim", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To adjust the amplitude of the sinus"},
		{"LABEL": "Animation/Slide Speed", "NAME": "mat_speedUp", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To adjust the speed of the auto move animation"},
		{"LABEL": "Animation/Flow Speed", "NAME": "mat_speedX", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0.0, "MAX": 2., "DESCRIPTION":"To adjust the speed of the animation inside the line"},
		{"LABEL": "Animation/Reverse", "NAME": "mat_animationReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"Reverse animation"},
		{"LABEL": "Animation/Fade", "NAME": "mat_fadeActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Pulse", "VALUES": ["Main","Grad","Osc","Sin 2","Sin 4","Sin 8","Pulse","Pulse2","Pulse3"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color on each shapes, Osc : Oscillate between main and secondary color in time, Sin : Oscillate between main and secondary color with a sin, Pulse is the same as sin but move faster than the animation", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},	
	],

	"GENERATORS": [
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedX","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOutAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Out", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedUp","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
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
	float base_rotation = 45*mat_button_angle+mat_offsetAngle;
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

	int finalPointCount = pointsForPrecision[mat_precision-1];//4 + (mat_precision/10)*(500-4));
	int pointsPerShape = finalPointCount;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMax = (normalizedPos-0.5)*2.;
	float normalizedPosMaxRight = (normalizedPos-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPos+mat_timeOffset)*2-1;

	float speed = 0;

	if(mat_animationShape==0){speed=mat_timeSmoothAnim;}
	if(mat_animationShape==1){speed=mat_timeInAnim;}
	if(mat_animationShape==2){speed=mat_timeOutAnim;}

	pos = vec2(
		normalizedPosMax
		,mat_ampAnim*sin(normalizedPosMaxRight*PI+mat_timeOffset)+((speed-0.5)*4)
	);
	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_offsetGeneral[0];
	pos[1]+=mat_offsetGeneral[1];
	
	float alpha = mat_fadeActivated?sin(PI*(pos[1]+1)*.5):1.;
	color = vec4(mat_mainColor.rgb,alpha);
	
	if(mat_button_grid==1){
		color = vec4(mat_mainColor.rgb,mat_alpha*alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),int(abs(mat_time))%2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),cos(PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),cos(2*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),cos(4*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),sin(PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),cos(2*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),sin(4*PI*(normalizedPos-(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
