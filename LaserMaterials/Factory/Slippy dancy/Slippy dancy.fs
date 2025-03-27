/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "Interstellar looking visual using line pattern with color gradient",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Size", "NAME": "mat_sizeGeneral", "TYPE": "float", "MIN": 0.0, "MAX":1.0, "DEFAULT": 0.7, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_offsetGeneral", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"},  
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid" },
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "FLAGS": "button_grid", "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Shapes Offset", "NAME": "mat_autorotateoffset", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
		
		{"LABEL": "Shapes Manager/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 2, "DESCRIPTION":"To adjust the amount of shapes"},
		{"LABEL": "Shapes Manager/Line Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 1, "DESCRIPTION":"To adjust the size of the lines"},  
		{"LABEL": "Shapes Manager/Line Spread", "NAME": "mat_lineSpread", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.75, "DESCRIPTION":"To adjust the gap betweend the lines"},  
		{"LABEL": "Shapes Manager/Glitch mod", "NAME": "mat_buttonGlitch", "TYPE": "long", "DEFAULT":"Full", "VALUES": ["Full","Holed"], "FLAGS": "button_grid", "DESCRIPTION":"You can adjust the Mod by adding holes in the scan part of the animation"},

		{"LABEL": "Auto Bounce/Active", "NAME": "mat_bounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"To activate the bounce animation of the lines, LIKE A TRAMPOLINE"},
		{"LABEL": "Auto Bounce/Speed", "NAME": "mat_speedBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1, "DESCRIPTION":"To adjust the speed of the bounce animation"},
		{"LABEL": "Auto Bounce/Amplitude", "NAME": "mat_ampBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5, "DESCRIPTION":"To adjust the amplitude of the bounce animation"},
		{"LABEL": "Auto Bounce/Offset", "NAME": "mat_offsetBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0. , "DESCRIPTION":"To adjust the offset of the bounce animation"},
		{"LABEL": "Auto Bounce/Phase", "NAME": "mat_phaseBounce", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0., "DESCRIPTION":"To adjust the phase of the bounce animation so you can configure precisely your animation"},

		{"LABEL": "Auto Side Bounce/Active", "NAME": "mat_sideBounceActivated", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The side bounce animation is pivoting the visual with an oscillation, you can add this animation with the auto rotate animation to create pulsating rotation if the speed of the side bounce and the auto rotate are multiples"},
		{"LABEL": "Auto Side Bounce/Speed", "NAME": "mat_speedOsc", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.2, "DESCRIPTION":"To adjust the side bounce animation speed"},
		{"LABEL": "Auto Side Bounce/Amplitude", "NAME": "mat_ampOsc", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0, "DESCRIPTION":"To adjust the side bounce animation amplitude"},

		{"LABEL": "Auto Rotate/Active", "NAME": "mat_autorotateactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button", "DESCRIPTION":"The auto rotate is an animation which make the whole visual rotatating"},
		{"LABEL": "Auto Rotate/Speed", "NAME": "mat_autorotatespeed", "TYPE": "float", "DEFAULT": 0.4, "MIN": 0.0, "MAX": 3.0, "DESCRIPTION":"To adjust the auto rotate speed"},
		{"LABEL": "Auto Rotate/Reverse", "NAME": "mat_autorotatereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To invert the spin"},
		{"LABEL": "Auto Rotate/Strob", "NAME": "mat_autorotatestrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 , "DESCRIPTION":"The auto rotate strobe will activate and stop the rotation animation according to a duration named strobe"},
		
		{"LABEL": "Color/Grid", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Shape","Osc"],"DESCRIPTION":"You can choose your color Mod. Main : Main Color, Shape : 1 shape main color and 1 shape secondary color, Osc : Oscillate between main and secondary color in time", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
		{"LABEL": "Color/Main", "NAME": "mat_mainColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the main color"},
		{"LABEL": "Color/Secondary", "NAME": "mat_secondaryColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha", "DESCRIPTION":"To choose the secondary color"},
		{"LABEL": "Color/Power", "NAME": "mat_alpha", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1. , "DESCRIPTION":"To adjust the power of the color"},
		
		{"LABEL": "Strobe/Activate", "NAME": "mat_strobeActivated", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button", "DESCRIPTION":"To activate the strobe"},
		{"LABEL": "Strobe/Speed", "NAME": "mat_speedStrobe", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT":0.6, "DESCRIPTION":"To adjust the frequency of the strobe"},
		{"LABEL": "Strobe/Duration", "NAME": "mat_strobeDuration", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25, "DESCRIPTION":"To adjust the duration of the strobe"},
	],

	"GENERATORS": [
		{"NAME": "mat_timeStrobe", "TYPE": "time_base", "PARAMS": {"speed": "mat_strobeMultiplied","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_strobeMultiplied","TYPE": "multiplier","PARAMS": {"value1": "mat_speedStrobe", "value2":15, "value3": 1, "value4": 1}},
		{"NAME": "mat_timeBounce", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedBounce","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOscColor","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOsc", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedOsc","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSize", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedLineSize","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOscRL", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedRL","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_rot_position","TYPE": "time_base","PARAMS": {"speed": "mat_autorotatespeed","reverse": "mat_autorotatereverse","strob": "mat_autorotatestrob", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": 1000
	}
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat3 makeTransformMatrix(float normalizedCellId)
{
	float base_rotation = int(mat_sideBounceActivated)*20*mat_ampOsc*sin(2*PI*mat_timeOsc)+45*mat_button_angle+mat_offsetAngle+normalizedCellId*mat_autorotateoffset*180;
	// Center
	mat3 linePatternsMatrix = mat3(1,0,0,
							  0,1,0,
							  0,0,1);

	// Rotate
	if (mat_autorotateactive || (base_rotation!=0)) {
		float angle = base_rotation * 2*PI / 360;
		if (mat_autorotateactive) {
		angle += fract(0.5 + (mat_rot_position)) * 2*PI;
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


	lineSpreadCustom = mat_lineSpread; //managing shape instances out the bands by adjusting the line spread to the first line or the last line

	if (shapeNumber % 2 != 0) {
		customShapeNumber =-shapeNumber; //impaire
	}
	else{customShapeNumber = (shapeNumber)+1;} //paire
	
	
	pos = vec2(
		normalizedPosInShapeMax*size									//size
		,customShapeNumber*lineSpreadCustom		//shape spread
		*(mat_bounceActivated?(mat_ampBounce*sin(2*PI*(mat_timeBounce+mat_phaseBounce))+mat_offsetBounce):1.)	//bounce
	);

	float normalizedShapeId = float(shapeNumber)/mat_shapeAmount;

	pos*=mat_sizeGeneral;

	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = (vec3(pos,1) * transformMatrix).xy;
	pos = vec2(pos[0]+mat_offsetGeneral[0],pos[1]+mat_offsetGeneral[1]);    

	color = mat_alpha*4.*vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(0.),pow(abs(tan(sin(2*PI*mat_timeBounce)*10*2*PI*normalizedPos)),0.5)));

	if(mat_button_grid==1){
		color *= vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,1.),float(shapeNumber)));
	}
	else if(mat_button_grid==2){
		color *= vec4(mix(vec4(mat_mainColor.rgb,1.),vec4(mat_secondaryColor.rgb,1.),(sin(mat_timeBounce*2*PI)+1)/2));
	}


	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}
}
