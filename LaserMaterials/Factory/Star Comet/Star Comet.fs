/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "This visual is a small definition range of a parametrical function to simultate a random path",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},

		{"LABEL": "Animation/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the speed of the animation which is following a parametric function"},
		{"LABEL": "Animation/Reverse", "NAME": "mat_animationReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To reverse the animation"},
		{"LABEL": "Animation/Path", "NAME": "mat_morph3", "TYPE": "point2D", "MIN": [0.,0.], "MAX": [1.,1.], "DEFAULT": [0.33,0.2] , "DESCRIPTION":"To adjust the parameters of the parametric function which will change the path of the snake"},
		{"LABEL": "Animation/Line Width", "NAME": "mat_lineWidth", "TYPE": "float", "DEFAULT": .1, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To adjust the length of the lines"}, 
		{"LABEL": "Animation/Beam Mod", "NAME": "mat_beamModButt", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To transform the line as beam"}, 
		
		{"LABEL": "Auto Shuffle/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the auto shuffle mod"},
		{"LABEL": "Auto Shuffle/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"To adjust the auto shuffle speed"},
		{"LABEL": "Auto Shuffle/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"The auto shuffle mod will spin the parametrical function"},
		
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Grad", "VALUES": ["Main","Grad","Osc","Head","Tail","Snake","Pulse","Pulse2","Pulse4"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color between main and secondary color on each shapes, Osc : Oscillate between main and secondary color in time, Head/Tail/Snake: Color the ends, Pulse : Color impulsions ", "FLAGS": "button_grid" },
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
		
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_animationReverse","speed_curve": 4,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeRotate","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","strob": "mat_autorotatestrob", "speed_curve":3, "link_speed_to_global_bpm":true, "max_value":10000 }},
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_speedOffset", "value2": 2, "value3": 1, "value4": 1}}
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
	mat3 linePatternsMatrix = mat3(1,0,0,0,1,0,0,0,1);
	if (base_rotation!=0) {
		float angle = base_rotation * 2*PI / 360;
		float sin_factor = sin(angle);
		float cos_factor = cos(angle);
		linePatternsMatrix *= mat3(cos_factor, sin_factor,0,-sin_factor,cos_factor,0,0,0,1);
	}

	return linePatternsMatrix;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;

	float normalizedPos = -float(pointNumber)/(pointCount-2)*pow(mat_lineWidth,2)*1.;
	
	if(mat_beamModButt){
		if(pointNumber==0){
			shapeNumber=0;
		}
		else{shapeNumber=-1;}
	}

	float normalizedPosMax = normalizedPos*2+mat_timeOffset;

	pos = vec2(
		sin(PI*(normalizedPosMax-(mat_autorotateactive?mat_timeRotate:0)))*sin((mat_morph3[0]*3+1.)*2*PI*normalizedPosMax),
		sin(PI*(normalizedPosMax+0.5-(mat_autorotateactive?mat_timeRotate:0)))*sin((mat_morph3[1]*3+1.)*2*PI*(normalizedPosMax+0.5))
	);

	pos *= mat_shapeSize;
	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
  	pos = (vec3(pos,1) * transformMatrix).xy;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,mat_alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),-normalizedPos/pow(mat_lineWidth,2)));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),(sin(mat_timeOffset*2*PI)+1)/2));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),float(pointNumber)/(pointCount-2)*5.));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),-(float(pointNumber)/(pointCount-2)*5.)+1.));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),1.-cos(2*PI*(normalizedPos)/pow(mat_lineWidth,2))));
	}
	else if(mat_button_grid==6){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(1*2*PI*(normalizedPos+mat_timeOffset))+1));
	}
	else if(mat_button_grid==7){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(1*4*PI*(normalizedPos+mat_timeOffset))+1));
	}
	else if(mat_button_grid==8){
		color = vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,mat_alpha),sin(1*8*PI*(normalizedPos+mat_timeOffset))+1));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
