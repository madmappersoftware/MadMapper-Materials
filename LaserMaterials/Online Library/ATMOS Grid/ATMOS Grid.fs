/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple beam grid",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/X", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 4 },
		{"LABEL": "Global/Y", "NAME": "mat_y", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 4 },
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.9 }, 
		{"LABEL": "Global/Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 }, 

		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": "mat_y"}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_instance_count",
       "PRESERVE_ORDER": true
    }
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;


void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
	float W = 2./float(mat_x);
	float H = 2./float(mat_y);
	float normalizedPos = float(pointNumber)/(pointCount-1);

	pos.x = -1 + pointNumber%(mat_x)*W + W*0.5;
	pos.y = -1 + (pointNumber/mat_x)*H + H*0.5;;

	pos += dFlowNoise( pos*mat_scale, mat_time ).xy *mat_power*mat_power/20;

	shapeNumber = pointNumber;
	color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1);
}
