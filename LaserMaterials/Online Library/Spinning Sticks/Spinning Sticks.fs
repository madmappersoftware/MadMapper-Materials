/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Sam Nobs",
	"DESCRIPTION": "Spinning sticks on a circle",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 10 }, 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -10.0, "MAX": 10.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.7 },  
		{"LABEL": "Global/Line Length", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Left", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Right", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Global/Sync Spin", "NAME": "mat_sync_spin", "TYPE" : "bool", "DEFAULT" : 	false },
	],
	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": "mat_amount"
	}
}*/

#include "MadCommon.glsl"

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int evenPointCount = (pointCount/2)*2;
	shapeNumber = (pointNumber/2);
	float normalizedPos = float(shapeNumber)/(evenPointCount);
	
	pos = vec2(sin(normalizedPos*4*PI),cos(normalizedPos*4*PI))*mat_radius;

	float t = mat_time;
	if (!mat_sync_spin) {
		t += normalizedPos * 2 * PI;
	}
    if (pointNumber % 2 == 0)
    {
	    pos = pos + vec2(sin(t), cos(t))*mat_amp;
    } else {
	    pos = pos - vec2(sin(t), cos(t))*mat_amp;
	}
	if (pointNumber < evenPointCount)
	{
		// make sure we don't output an excess point if pointCount is uneven
		pos = pos * 0.3;
	}
	color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,pointNumber % 2),1.);
}
