/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "frz / 1024 architecture",
	"DESCRIPTION": "Simple vector grid of crosses",
	"TAGS": "laser",
	"VSN": "1.0",
	"INPUTS": [
		{"LABEL": "Global/X", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 3 },
		{"LABEL": "Global/Y", "NAME": "mat_y", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 3 },
		{"LABEL": "Global/Width", "NAME": "mat_width", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },  
		{"LABEL": "Noise/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.3 },  
		{"LABEL": "Noise/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.9 }, 
		{"LABEL": "Noise/Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 
		{"LABEL": "Global/Connect", "NAME": "mat_connect", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button" },

		{"LABEL": "Color/Flicker", "NAME": "mat_flicker", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Color/Speed", "NAME": "mat_fspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.3 },    
	],

	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_ftime", "TYPE": "time_base", "PARAMS": {"speed": "mat_fspeed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": "mat_y","value3" : 4}},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": "mat_instance_count",
	   "PRESERVE_ORDER": true,
	}
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int currentPoint = (pointNumber%(pointCount/2))/2;
	float W = 2./float(mat_x);
	float H = 2./float(mat_y);
	float nx = -1 + currentPoint%(mat_x)* W + W*0.5;
	float ny = -1 + (currentPoint/mat_x)* H + H*0.5;
	vec2 pos2d = vec2(nx,ny);

	vec3 n = dFlowNoise( pos2d*mat_scale, mat_time )*mat_power;
	shapeNumber = pointNumber/2;

	float sq = mat_width*0.1;

	if(pointNumber < pointCount/2)
	{
		int crossIdx = pointNumber%2;
		if(crossIdx == 0) pos2d.x -= sq;
		if(crossIdx == 1) pos2d.x += sq;
	} else {
		int crossIdx = pointNumber%2;
		if(crossIdx == 0) pos2d.y -= sq;
		if(crossIdx == 1) pos2d.y += sq;
	}
	
	pos = pos2d + n.yz*0.1;

	float nCol = billowedNoise(vec2( (pointNumber/2)%(pointCount/4),mat_ftime));
	nCol = mix(1.,nCol*nCol*nCol,mat_flicker);
	color = vec4(vec3(nCol),1);
}
