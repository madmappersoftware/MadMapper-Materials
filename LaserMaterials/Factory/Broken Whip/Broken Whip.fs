/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Ths visual acts like a whip, adjust the whip in the animation section",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Break Points", "NAME": "mat_amount", "TYPE": "int", "MIN": 2, "MAX": 20, "DEFAULT": 5, "DESCRIPTION":"To choose the amount of time the whip is broken"},

		{"LABEL": "Animation Whip/Amplitude", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": .4, "DESCRIPTION":"To adjust the amplitude of the animation"},
		{"LABEL": "Animation Whip/Power", "NAME": "mat_speedOffset", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0, "MAX": 1.0, "DESCRIPTION":"To adjust the power of the whip"}, 
		{"LABEL": "Animation Whip/Frequence", "NAME": "mat_speedAnim", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1, "DESCRIPTION":"To adjust the frequency of a whips animation"}, 
		{"LABEL": "Animation Whip/Roughness", "NAME": "mat_roughness", "TYPE": "float", "DEFAULT": 0.6, "MIN": 0, "MAX": 1, "DESCRIPTION":"To adjust the time of a whip animation"}, 
		{"LABEL": "Animation Whip/Adjustements", "NAME": "mat_adjustement", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [10.,2.], "DEFAULT": [2.3,0.6], "DESCRIPTION":"To adjust the whip animation"}, 
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Pulse", "VALUES": ["Main","Grad","Osc","Pulse","Pulse2","OscOsc"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color, Osc : Oscillate between main and secondary color in time, OscOsc : Oscillate between main and secondary color with pulse, Pulse : Color oscillation in space", "FLAGS": "button_grid" },
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
		{"NAME": "spawn", "TYPE": "time_base", "PARAMS": {"speed": 10,"reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeFeedback", "TYPE": "time_base", "PARAMS": {"speed": 0.1,"reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeWhipAnim", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnim","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplierRoughness","TYPE": "multiplier","PARAMS": {"value1": "mat_roughness", "value2": 0.05,}}
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
		linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
								 -sin_factor, cos_factor, 0,
								 0, 0, 1);
	}

	return linePatternsMatrix;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int mat_shapeAmount = mat_amount;

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;

	float normalizedPos = float(pointNumber)/(pointCount-1);
	float normalizedPosMaxX = (normalizedPos)*2-1;
	float normalizedPosMaxY = (normalizedPos+mat_timeOffset)*2-1;
	float normalizedPosMinY = (normalizedPos-mat_timeOffset)*2-1;
	float normalizedPosMinYColor = (normalizedPos-(mat_timeWhipAnim/4.)*2.)*2-1;
	float normalizedColorMaxY = (normalizedPos+(PI/2*mat_timeFeedback/2)-0.5)*2-1;
	float normalizedColorMinY = (normalizedPos-(PI/2*mat_timeFeedback/2)+0.5)*2-1;


	pos = vec2(
		normalizedPosMaxX
		,sin(PI*normalizedPosMaxY)*mat_adjustement[1]+sin(mat_adjustement[0]*PI*normalizedPosMaxY)

	);
	pos[1] *= (1/(1+mat_adjustement[1]))*0.9;
	pos[1] += sin(0.5*PI*normalizedPosMinY)*0.1;
	pos[1] *= mat_shapeSize;
	pos*=mat_size;
	pos[0]+=mat_offsetGeneral[0];
	pos[1]+=mat_offsetGeneral[1];	

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;


	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1-normalizedPos));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),((cos(25.*PI*(mat_timeWhipAnim/4.)))+1)/2));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(2*PI*mat_timeFeedback*5.)*cos(25.*PI*(mat_timeWhipAnim/4.))));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(9.*2*PI*(normalizedPos-normalizedPosMinYColor-0.5*sin(50*PI*(mat_timeWhipAnim/4.))*0.05+0.90+mat_multiplierRoughness))+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),cos(mat_shapeAmount*2*PI*(normalizedPos-normalizedPosMinYColor-2.*sin(50*PI*(mat_timeWhipAnim/4.))*0.05+0.90+mat_multiplierRoughness))+1));
	}


	float feedback = sin(50*PI*(mat_timeWhipAnim/4.))*0.05+0.90+mat_multiplierRoughness;
	feedback = feedback*(-1/pow(spawn+1,2)+1);

	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,feedback);
}
