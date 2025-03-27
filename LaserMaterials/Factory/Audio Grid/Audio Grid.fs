/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "frz / 1024 architecture",
	"DESCRIPTION": "Simple Audio Vector grid",
	"TAGS": "laser",
	"VSN": "1.4",
	"INPUTS": [
		{"LABEL": "Global/X", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 3 },
		{"LABEL": "Global/Y", "NAME": "mat_y", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 15 },
 
		{"LABEL": "Global/Scale", "NAME": "mat_gscale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 },
		{"LABEL": "Global/Rotation", "NAME": "mat_rot", "TYPE": "float", "MIN": 0.0, "MAX": 360, "DEFAULT": 0.0 },  

		{"LABEL": "Connect/Connect", "NAME": "mat_connect", "TYPE": "bool", "DEFAULT": true, "FLAGS":"button" }, 
		{"LABEL": "Connect/Flicker", "NAME": "mat_flick", "TYPE": "float", "DEFAULT": 0.3,"MIN":0.,"MAX":1.0, }, 
		{"LABEL": "Noise/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.3 },
		{"LABEL": "Noise/Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Noise/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.9 }, 

		{"LABEL": "Audio/Power", "NAME": "mat_apower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Audio/Threshold", "NAME": "mat_athresh", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Audio/Scale", "NAME": "mat_ascale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 


		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

		{
			"NAME": "mat_spectrum",
			"TYPE": "audioFFT",
			"SIZE": 12,
			"ATTACK": 0.05,
			"DECAY": 0.0,
			"RELEASE": 0.1
		},

	],

	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": "mat_y"}},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": "mat_instance_count"
	}
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;
mat2 ro(float a){return mat2(cos(a),sin(a),-sin(a),cos(a));}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float nx = -1 + 2 * (pointNumber%(mat_x)) / float(mat_x-1);
	float ny = -1 + 2 * (pointNumber/mat_x) / float(mat_y-1);
	float normalizedPos = float(pointNumber)/(pointCount-1);
	vec2 pos2d = vec2(nx,ny)*mat_gscale;

	vec3 n = dFlowNoise( pos2d*mat_scale, mat_time )*mat_power*0.1;
	pos2d += n.yz;

	if (mat_apower>0) {
		float audio = texture(mat_spectrum, vec2(0.2+(nx*0.5+0.5)*0.7*mat_ascale,0.5)).r*4.;
		audio = pow(audio,2.2)*0.5*mat_apower;
		audio = (max(mat_athresh,audio)-mat_athresh)*(1.+mat_athresh);
		pos2d /= 1+audio;
	}

	pos2d.xy *= ro(mat_rot*2*pi/360);

	int idx = pointNumber;
	if(mat_connect) {
		float shape = billowedNoise(vec3(pos2d*10.+vnoise(pos2d.xx)*10.,mat_time))*mat_flick*30.;
		idx = int(floor(shape));
	}

	pos = pos2d;
	shapeNumber = idx;
	color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1);
}