/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Jean Schneider /1024",
	"DESCRIPTION": "This is a multiple circle instancing with a drop animation",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Polygone", "NAME": "mat_precision", "TYPE": "int", "MIN": 2, "MAX": 10, "DEFAULT": 10, "DESCRIPTION":"To adjust the amount of faces of each shape, 10 is circle"},  
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0.7 , "DESCRIPTION":"Mixing the actual frame with the previous frame to smooth the parameter modifications"},
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 1, "DESCRIPTION":"Global size"},
		{"LABEL": "Global/Shift", "NAME": "mat_shift", "TYPE": "point2D", "MIN": [-1.,-1.], "MAX": [1.,1.], "DEFAULT": 0., "DESCRIPTION":"To adjust the position of your visual"}, 
		{"LABEL": "Global/Angle", "NAME": "mat_button_angle", "TYPE": "long", "DEFAULT":"0", "VALUES": ["0","45","90"], "FLAGS": "button_grid", "DESCRIPTION":"To choose the output angle"},
		{"LABEL": "Global/Angle Offset", "NAME": "mat_offsetAngle", "TYPE": "float", "DEFAULT":0., "MIN": 0., "MAX": 360., "DESCRIPTION":"To adjust the output angle"},
		{"LABEL": "Global/Shape Amount", "NAME": "mat_shapeAmount", "TYPE": "int", "MIN": 1, "MAX": 5, "DEFAULT": 4, "DESCRIPTION":"To select the amount of cones"},
		
		{"LABEL": "Auto Move/X Translate", "NAME": "mat_bounceX", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"To allow the auto move to go right and left with an oscillator"}, 
		{"LABEL": "Auto Move/Y Translate", "NAME": "mat_bounceY", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button", "DESCRIPTION":"To allow the auto move to go top to bottom with an oscillator"}, 
		{"LABEL": "Auto Move/Speed", "NAME": "mat_offsetGeneral", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT":0.25, "DESCRIPTION":"To adjust the auto move speed"}, 
		
		{"LABEL": "Drop Animation/Activate", "NAME": "mat_dropActivated", "TYPE": "bool", "DEFAULT": true,"FLAGS": "button", "DESCRIPTION":"The animation drop is an oscillator on the circle size delayed by the circle number"}, 
		{"LABEL": "Drop Animation/Speed Drop", "NAME": "mat_speedDrop", "TYPE": "float", "MIN": 0., "MAX": 1, "DEFAULT":0.25, "DESCRIPTION":"To adjust the animation drop speed"}, 
		{"LABEL": "Drop Animation/Mods", "NAME": "mat_phaseButt", "TYPE": "long", "VALUES": ["Smooth","Out","In"], "DEFAULT": "Smooth","FLAGS": "button_grid", "DESCRIPTION":"To change the animation drop style"}, 
					
		{"LABEL": "Color/Mod", "NAME": "mat_button_grid", "TYPE": "long", "DEFAULT":"Main", "VALUES": ["Main","Osc","Grad","Pulse","Drop","Inv Grad"], "DESCRIPTION":"Shape", "FLAGS": "button_grid" , "DESCRIPTION":"You can choose your color Mod. Main : Main Color, Grad : Gradient color per shape between main and secondary color, Osc : Oscillate between main and secondary color in time, Pulse is a sinus with a phase, Drop: Drop style color mod", "FLAGS": "button_grid" , "FLAGS": "button_grid" },
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
		{"NAME": "mat_timeColor", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier2","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeBounceShape", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeAutoShift", "TYPE": "time_base", "PARAMS": {"speed": "mat_multiplier3","reverse": "mat_animationReverse","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeSmoothAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_speedDrop","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Smooth", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeInAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier05In","reverse": "mat_animationReverse","speed_curve": 1, "shape":"In", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_timeOutAnim", "TYPE": "animator", "PARAMS": {"speed": "mat_multiplier05Out","reverse": "mat_animationReverse","speed_curve": 1, "shape":"Out", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_multiplier05In","TYPE": "multiplier","PARAMS": {"value1": "mat_speedDrop", "value2":2, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier05Out","TYPE": "multiplier","PARAMS": {"value1": "mat_speedDrop", "value2":2, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier","TYPE": "multiplier","PARAMS": {"value1": "mat_speedDrop", "value2": 8, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier2","TYPE": "multiplier","PARAMS": {"value1": "mat_speedDrop", "value2": 4, "value3": 1, "value4": 1}},
		{"NAME": "mat_multiplier3","TYPE": "multiplier","PARAMS": {"value1": "mat_offsetGeneral", "value2": 4, "value3": 1, "value4": 1}}
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
	int finalPointCount = pointsForPrecision[mat_precision-1]*(mat_precision==10?1:mat_shapeAmount);

	userData.x = finalPointCount+mat_shapeAmount;

	if (pointNumber >= finalPointCount) {
		shapeNumber = -1;
		return;
	}

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
	int lastFramePointCount = int(lastFrameUserData.x);

	int pointsPerShape = finalPointCount/mat_shapeAmount;

	shapeNumber = pointNumber/pointsPerShape;

	if (shapeNumber >= mat_shapeAmount) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}
	
	float normalizedPosInShape = float(pointNumber % pointsPerShape)/(pointsPerShape-1);
	float normalizedPosInShapeMax = 2 * normalizedPosInShape - 1;

	float normalizedPos = float(pointNumber)/(finalPointCount-1);

	pos = vec2(
		sin(PI*normalizedPosInShapeMax)*(1./mat_shapeAmount)*(shapeNumber+.5*(mat_dropActivated?sin(PI*mat_timeBounceShape+shapeNumber/4):1.))
		,sin(PI*(normalizedPosInShapeMax+0.5))*(1./mat_shapeAmount)*(shapeNumber+.5*(mat_dropActivated?sin(PI*mat_timeBounceShape+shapeNumber/4):1.))
	);

	float phase = 0.;
	
	if(mat_phaseButt==0){phase=1./8;}

	if(mat_phaseButt==1){pos*=mat_timeInAnim;}
	else if(mat_phaseButt==2){pos*=mat_timeOutAnim;}

	pos[0]+=mat_bounceX?sin(mat_timeAutoShift*PI/2):0.;
	pos[1]+=mat_bounceY?sin(.5*mat_timeAutoShift*PI/2):0.;

	pos*=mat_size*0.5;

	mat3 transformMatrix = makeTransformMatrix(normalizedPos);
	pos = (vec3(pos,1) * transformMatrix).xy;

	pos[0]+=mat_shift[0];
	pos[1]+=mat_shift[1];

	float normalizedPosMaxRight = (normalizedPosInShape-mat_timeOffset)*2-1;
	float normalizedPosMaxLeft = (normalizedPosInShape+mat_timeOffset)*2-1;

	float alpha = sin(PI*mat_timeColor-PI*shapeNumber/mat_shapeAmount);

	if(mat_button_grid==0){
		color = vec4(mat_mainColor.rgb,alpha);
	}
	else if(mat_button_grid==1){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),ceil((sin(mat_timeColor*PI*0.5)))*2.));
	}
	else if(mat_button_grid==2){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),1.-floor(normalizedPos*mat_shapeAmount)/(mat_shapeAmount-1)));
	}
	else if(mat_button_grid==3){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),cos(2*PI*(shapeNumber==0?normalizedPosMaxRight:normalizedPosMaxLeft)+PI)+1));
	}
	else if(mat_button_grid==4){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),(floor((2.*sin((mat_timeColor+1.)*PI)+1.)*mat_shapeAmount))/(mat_shapeAmount)));
	}
	else if(mat_button_grid==5){
		color = vec4(mix(vec4(mat_mainColor.rgb,alpha),vec4(mat_secondaryColor.rgb,mat_alpha*alpha),floor(normalizedPos*mat_shapeAmount)/(mat_shapeAmount-1)));
	}
	if(mat_strobeActivated){color=vec4(color[0],color[1],color[2],color[3]*(fract(mat_timeStrobe)<mat_strobeDuration?1.:0.));}

	float feedback = -pow(mat_feedback-1.,2)+1.;

	if(FRAMEINDEX > 0) {
		if (finalPointCount+mat_shapeAmount == lastFramePointCount) {
			pos = mix(pos,lastFramePos,feedback);
		} else {
			vec2 lastFramePos = vec2(mat_bounceX?sin(mat_timeAutoShift*PI/2)/2.:0.,mat_bounceY?sin(.5*mat_timeAutoShift*PI/2)/2.:0.);
			pos = mix(pos,lastFramePos,feedback);
		}
	}
}
