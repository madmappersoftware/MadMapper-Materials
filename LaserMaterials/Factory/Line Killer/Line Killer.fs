/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple vector moving lines",
    "TAGS": "laser",
    "VSN": "1.2",
    "INPUTS": [
		{"LABEL": "Global/Lines", "NAME": "mat_line_count", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 7 },
		{"LABEL": "Global/Precision", "NAME": "mat_line_definition", "TYPE": "int", "MIN": 2, "MAX": 300, "DEFAULT": 10 },
		
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. }, 
		{ "Label": "Noise/Type", "NAME": "mat_type","TYPE": "long", "DEFAULT": "Flow", "VALUES": [ "Flow", "Billow","White" ] },
		{"LABEL": "Noise/Speed", "NAME": "mat_nspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.3 },  
		{"LABEL": "Noise/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.9 }, 
		{"LABEL": "Noise/Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 
		{"LABEL": "Noise/Restrict to Y", "NAME": "mat_restrict", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button" },  

		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
        {"NAME": "mat_ntime", "TYPE": "time_base", "PARAMS": {"speed": "mat_nspeed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_line_definition", "value2": "mat_line_count"}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_instance_count",
       "MAX_SPEED": 4,
       "ANGLE_OPTIMIZATION": false,
       "PRESERVE_ORDER": true,
       "SKIP_BLACK": false
    }
}*/
#include "MadNoise.glsl"

const float pi = 3.14159265359;

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float normalizedPos = float(pointNumber)/(pointCount-1);

	int lineNumber = int(pointNumber/mat_line_definition);
	int linePointNumber = pointNumber%mat_line_definition;
	float lineVerticalSpacing = 2./(mat_line_count+1);
	float linePointSpacing = 1./float(mat_line_definition);

	float nx = -0.8 + 1.8 * linePointNumber * linePointSpacing;
	float ny = -1 + 2./(mat_line_count+1) + lineNumber*lineVerticalSpacing;

	vec2 pos2d = vec2(nx,ny);

	pos2d.y += mat_time;
	pos2d.y = -1+2*fract((1+pos2d.y)/2);

	vec3 n = vec3(0.);

	if (mat_type==0) n = dFlowNoise( pos2d*mat_scale, mat_ntime )*mat_power;
	if (mat_type==1) n = ((dBillowedNoise( vec3(pos2d*mat_scale, mat_ntime) ).xyz)*2.-1.5)*mat_power;
	if (mat_type==2) n = ((dRidgedMF( vec3(pos2d*mat_scale, mat_ntime) ).xyz)*2.-1.5)*mat_power;

	if(mat_restrict) n.y = 0.;

	pos = pos2d + n.yz*0.1;
	shapeNumber = lineNumber;
	color = mix(mat_leftColor,mat_rightColor,normalizedPos);
}
