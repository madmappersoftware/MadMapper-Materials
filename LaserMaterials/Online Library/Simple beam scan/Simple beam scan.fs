/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "CZ / frz / 1024 architecture ",
    "DESCRIPTION": "Simple beam scan",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/X", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 4 },
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Global/Loop", "NAME": "loop", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }, 
		{ "LABEL": "Global/Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 1.0, 0.0 ] },
		{ "LABEL": "Noise/Noise", "NAME": "noises", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },		
		{ "Label": "Noise/Type", "NAME": "mat_type","TYPE": "long", "DEFAULT": "Billowed", "VALUES": [ "Billowed", "Worley","Ridged","Billowy Turbulence","Flow Noise","Noise"] },
		{ "LABEL": "Noise/Time", "NAME": "times", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Color/Color", "NAME": "mat_Color", "TYPE": "color", "DEFAULT": [ 0.0, 1.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],


    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": "mat_y"}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_instance_count"
    }
}*/


#include "MadNoise.glsl"


const float pi = 3.14159265359;


void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
	
	float W = 2./float(mat_x);
	
	if(loop){
		float t = 0.;
		if(reverse){t = mod(-mat_time *mat_x, 2.);}
		else{t = mod(mat_time *mat_x, 2.);}

		pos.x = (-1 + pointNumber%(mat_x)*W + W*0.5 *t)*mat_offset.x;
		}
	else{
		if(mat_x==1){
			pos.x = -1 + pointNumber%(mat_x)*W + W *0.5*mat_offset.x+1.;
			pos.y = mat_offset.y ;
		}
		else{
			pos.x = (-1 + pointNumber%(mat_x)*W + W )*mat_offset.x;
			pos.y = mat_offset.y;
		}
	}
		
	float n = 0.;	
	if (mat_type==0) n = billowedNoise(vec2(pos.x));
	if (mat_type==1) n = worleyNoise(vec2(pos.x));
	if (mat_type==2) n = ridgedMF(vec2(pos.x));
	if (mat_type==3) n = billowyTurbulence(vec2(pos.x));
	if (mat_type==4) n = flowNoise(vec2(pos.x), mat_time);
	if (mat_type==5) n = noise(vec2(pos.x));

	if(noises) {
		if(times){pos.y = (mat_offset.y*n)*sin(mat_time);}
		else{pos.y = mat_offset.y*n;}
	}			
	else {
		if(times){pos.y = (mat_offset.y)*sin(mat_time);}
		else{pos.y = mat_offset.y;}
	}
		
	shapeNumber = pointNumber;
	color = vec4(mat_Color);
}


