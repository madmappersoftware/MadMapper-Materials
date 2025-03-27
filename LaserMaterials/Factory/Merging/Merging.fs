/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "This a bouncing line visual with a merge when bouncing",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7, "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"}, 
		{"LABEL": "Global/Size", "NAME": "mat_sizeGeneral", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1., "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},  
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Line Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0.5, "DESCRIPTION":"To adjust line size"},
		{"LABEL": "Global/Line Spread", "NAME": "mat_lineSpread", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.75, "DESCRIPTION":"To adjust the distance between the lines"},
		{"LABEL": "Global/Shape Angle", "NAME": "mat_shapeAngle", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360., "DESCRIPTION":"To adjust the offset angle according to the line number"},
		
		{"LABEL": "Lines/Amount", "NAME": "mat_button_AnimMod", "TYPE": "long", "DEFAULT":"2-1", "VALUES": ["1-1","2-1","2-2","3-2","3-3","4-3"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the animation Mod : the animation will swap from n to n-1 or from n to n. You can multiply it with -single double triple- variable"},
		{"LABEL": "Lines/Multiplied", "NAME": "mat_button_speedMod", "TYPE": "long", "DEFAULT":"Single", "VALUES": ["Single","Double","Triple"], "FLAGS": "button_grid", "DESCRIPTION":"You can multiply the amount of lines here"},
		{"LABEL": "Lines/Merge", "NAME": "mat_merge", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"You can choose to merge the lines or to bounce it if unabled"},

		{"LABEL": "Auto Slide/Active", "NAME": "mat_offsetActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the auto slide animation which act like a bounce inside de visual"},
		{"LABEL": "Auto Slide/Power", "NAME": "mat_ampOscOffset", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5, "DESCRIPTION":"To adjust the power of the slide animation"},
		{"LABEL": "Auto Slide/Speed", "NAME": "mat_speedOffset", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1., "DESCRIPTION":"To adjust the speed of the slide animation"},
		
		{"LABEL": "Bounce/Active", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the bounce animation of the lines, LIKE A TRAMPOLINE"},
		{"LABEL": "Bounce/Speed", "NAME": "mat_speedBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1, "DESCRIPTION":"To adjust the speed of the bounce animation"},
		{"LABEL": "Bounce/Amplitude", "NAME": "mat_ampBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5, "DESCRIPTION":"To adjust the amplitude of the bounce animation"},
		{"LABEL": "Bounce/Offset", "NAME": "mat_offsetBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0. , "DESCRIPTION":"To adjust the offset of the bounce animation"},
		{"LABEL": "Bounce/Phase", "NAME": "mat_phaseBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0., "DESCRIPTION":"To adjust the phase of the bounce animation so you can configure precisely your animation"},

		{"LABEL": "Side Bounce/Active", "NAME": "mat_sideBounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The side bounce animation is pivoting the visual with an oscillation, you can add this animation with the auto rotate animation to create pulsating rotation if the speed of the side bounce and the auto rotate are multiples"},
		{"LABEL": "Side Bounce/Speed", "NAME": "mat_speedOsc", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1, "DESCRIPTION":"To adjust the side bounce animation speed"},
		{"LABEL": "Side Bounce/Amplitude", "NAME": "mat_ampOsc", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0, "DESCRIPTION":"To adjust the side bounce animation amplitude"},


		{"LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"The auto rotate is an animation which make the whole visual rotatating"},
		{"LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2., "DESCRIPTION":"To adjust the auto rotate speed"},
		{"LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To invert the spin"},
		{"LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"The auto rotate strobe will activate and stop the rotation animation according to a duration named strobe"},
		{"LABEL": "Auto Rotate/Shapes Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0, "DESCRIPTION":"the shape offset is adjusting the rotation offset according to the shape number"},
	
		{"LABEL": "Auto Line Size/Active", "NAME": "mat_lineSizeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the line sizing animation controled by an oscillator"},
		{"LABEL": "Auto Line Size/Speed", "NAME": "mat_speedLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.2, "DESCRIPTION":"To adjust the line sizing animation speed"},
		{"LABEL": "Auto Line Size/Offset", "NAME": "mat_offsetLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0., "DESCRIPTION":"To adjust the line sizing animation offset"},
		{"LABEL": "Auto Line Size/Phase", "NAME": "mat_phaseLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 0., "DESCRIPTION":"To adjust the line sizing animation phase"},
		{"LABEL": "Auto Line Size/Delay", "NAME": "mat_delayLineSize", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 0., "DESCRIPTION":"To adjust the line sizing animation delay between the different shapes"},

		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Alpha", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1., "DESCRIPTION":"To choose the secondary color alpha"},
		
		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},	
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeBounce", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedBounce","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOsc", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOsc","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSize", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedLineSize","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscOffset", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOffset","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_rot_position","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
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
	float base_rotation = int(mat_sideBounceActivated)*20*mat_ampOsc*sin(2*PI*mat_timeOsc)+45*mat_button_angle+mat_offsetAngle+mat_shapeAngle*shapeNumber;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);

	// Rotate
	if (mat_autorotateactive || (base_rotation!=0)) {
	  float angle = base_rotation * 2*PI / 360;
	  if (mat_autorotateactive) {
		angle += fract(0.5 + (mat_rot_position+normalizedCellId*mat_autorotateoffset)) * 2*PI;
	  }
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
	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	int pointsPerShape = pointCount / 2;

	shapeNumber = pointNumber/pointsPerShape;
	if (shapeNumber >= 2) {
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


	lineSpreadCustom = mat_lineSpread; //managing shape instances out the bands by adjusting the line spread to the first line or the last line

	if (shapeNumber % 2 != 0) {
		customShapeNumber =-shapeNumber; //impaire
	}
	else{customShapeNumber = (shapeNumber)+1;} //paire
	
	
	pos = vec2(
		normalizedPosInShapeMax*size									//size
		*(mat_lineSizeActivated?cos(2*PI*(mat_timeSize+mat_phaseLineSize)+(shapeNumber+1)*mat_delayLineSize)+mat_offsetLineSize:1.)  	//line size
		//+(mat_RLactivated?sin(2*PI*(mat_timeOscRL+mat_phaseRL))*mat_ampRL+mat_offsetRL:0.)			//X translate
		
		,customShapeNumber*lineSpreadCustom		//shape spread
		*(mat_bounceActivated?(mat_ampBounce*sin(2*PI*(mat_timeBounce+mat_phaseBounce))/1+mat_offsetBounce):1.)	//bounce
		
	);

	float normalizedShapeId = float(shapeNumber)/2;

	pos*=mat_sizeGeneral;

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId,shapeNumber);
	pos = (vec3(pos,1) * transformMatrix).xy;
	pos = vec2(pos[0]+mat_offsetGeneral[0],pos[1]+mat_offsetGeneral[1]);
	
	float freqTan = (mat_button_AnimMod+1)*(mat_button_speedMod+1);

	float bounce = pow(abs(sin(PI*mat_timeOscOffset)*mat_ampOscOffset*7.*int(mat_offsetActivated)+tan(freqTan*PI*normalizedPos)),((mat_merge?-sin(2*PI*(mat_timeBounce+mat_phaseBounce)):-1)+mat_offsetBounce)*10);
	
	color = vec4(mix(vec4(mat_secondaryColor.rgb,mat_alpha),vec4(mat_mainColor.rgb,1.),bounce));
		
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
	if(FRAMEINDEX > 0) pos = mix(pos,lastFramePos,mat_feedback);
}
