/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "The broken line is fractured in n points which follow a sinus path, those points are connected each other by a segment",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"Global size"}, 
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Fracture", "NAME": "mat_points", "TYPE": "int", "DEFAULT":4, "MIN":2,"MAX":10, "DESCRIPTION":"To choose the amount of time the line is broken"},
		{"LABEL": "Global/Width", "NAME": "mat_shapeSize", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"To adjust the width of the broken line"}, 
		
		{"LABEL": "Animation/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": .5, "DESCRIPTION":"To adjust the amplitude of the animation"},
		{"LABEL": "Animation/Speed", "NAME": "mat_speedFlow", "TYPE": "float", "DEFAULT": 0.2, "MIN": .0, "MAX": 1., "DESCRIPTION":"To adjust the speed of the animation"},
		{"LABEL": "Animation/Frequence", "NAME": "mat_freqFunc", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT": .3, "DESCRIPTION":"To adjust the frequency of the animation"},
		
		{"LABEL": "Amplitude Oscillator/Activate", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The amplitude oscillator will multiply the animation with an oscillator to ad organic movement of pulsation"},
		{"LABEL": "Amplitude Oscillator/Speed", "NAME": "mat_speedAnimation", "TYPE": "float", "DEFAULT": 0.5, "MIN": .0, "MAX": 1.0, "DESCRIPTION":"To adjust the speed of the animation amplitude oscillator"},

		{"LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"The auto rotate is an animation which make the whole visual rotatating"},
		{"LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotateReverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To invert the rotation"},
		{"LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"To adjust the auto rotate speed"},
		{"LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"The auto rotate strobe will activate and stop the rotation animation according to a duration named strobe"},

		{"LABEL": "Style Adjustement/Beam", "NAME": "mat_alphaBeam", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1. },
		{"LABEL": "Style Adjustement/Segment", "NAME": "mat_alphaSegment", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To adjust the intensity of the segments. If -Gradient- is On, the segment will have a gaussian gradient"},
		{"LABEL": "Style Adjustement/Gradient", "NAME": "mat_gradientActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To adjust the intensity of the beams."},
		{"LABEL": "Style Adjustement/End Locking", "NAME": "mat_lockActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To lock the ends of the line."},

		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Beam","Inv Beam"], "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Beam : Beams are main color and segments are in secondary, Segment : Beams are seconday color and segments are in main", "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, .0, .0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha, main color if color mod -main- choosen"},

		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_multiplierRotate","TYPE": "multiplier","PARAMS": {"value1": "mat_autorotatespeed", "value2": 20, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeRotate","TYPE": "time_base","PARAMS": {"speed": "mat_multiplierRotate","reverse": "mat_autorotateReverse","strob": "mat_autorotatestrob", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timePath", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeFuncOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplierSpeedFlow","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedAnimation","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplierSpeedFlow","TYPE": "multiplier","PARAMS": {"value1": "mat_speedFlow", "value2": 5, "value3": 1, "value4": 1}}
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
	
	float base_rotation = 45*mat_button_angle+mat_offsetAngle+(mat_autorotateactive?mat_timeRotate:0.);
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

float lineFunction(float start, float end, float normalizedPos){
	return (end - start)*normalizedPos+start;
}

float func(float x){
	float exteFix = mat_lockActivated?sin(PI*x):1.;
	return exteFix*mat_amp*cos(mat_freqFunc*4.*PI*((x-0.5)*2)+mat_timeFuncOffset)*(mat_bounceActivated?sin(2*PI*+mat_timePath):1.);
}

const int pointsAmount[10] = int[10](500,500,500,495,500,500,492,490,496,495);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int finalPointCount = pointsAmount[mat_points-1];

	userData.x = finalPointCount;
	
	float normalizedPos = float(pointNumber)/(finalPointCount-1);
	float normalizedPosMax = (normalizedPos-0.5)*2;

	int points = mat_points-1;

	int i = int(normalizedPos*float(points));

	float start = func(i/float(points));
	float end = func((i+1)/float(points));

	float yPath = (end-start)*fract(normalizedPos*points)+start;

	float gradient = !mat_gradientActivated?mat_alphaSegment:(0.5*cos(2*PI*fract(normalizedPos*points)))+(mat_alphaSegment-0.25)*2;

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,gradient*mat_alpha);
	}
	if(mat_button_grid==1){
		color = vec4(mat_secondaryColor.rgb,gradient*mat_alpha);
	}
	if(mat_button_grid==2){
		color = vec4(mat_mainColor.rgb,gradient*mat_alpha);
	}
	
	if(pointNumber%(int((finalPointCount)/points))==0 || pointNumber==finalPointCount-1){
		shapeNumber = 1;
		if(mat_button_grid==0){
			color =  vec4(mat_mainColor.rgb,mat_alphaBeam);
		}
		if(mat_button_grid==1){
			color = vec4(mat_mainColor.rgb,mat_alphaBeam);
		}
		if(mat_button_grid==2){
			color = vec4(mat_secondaryColor.rgb,mat_alphaBeam);
		}
	}
	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	float xPath = normalizedPosMax;

	pos = vec2(xPath, yPath);

	pos[0]*=mat_shapeSize;
	pos*=mat_size;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;
	
	if(FRAMEINDEX > 0) {
		if (finalPointCount == lastFramePointCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber*lastFramePointCount/finalPointCount,0),0).rg;
			pos = mix(pos,lastFramePos,feedback);
		}
	}
}
