/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "frz / 1024 architecture",
	"DESCRIPTION": "Audio Reactive laser point array",
	"TAGS": "laser",
	"VSN": "1.4",
	"INPUTS": [
		{"LABEL": "Global/X", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 2 },
		{"LABEL": "Global/Y", "NAME": "mat_y", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 2 },

		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Audio", "NAME": "mat_audio", "TYPE": "float", "MIN": 0.0, "MAX": 1.5, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 }, 
		{"LABEL": "Global/Wobble", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{"LABEL": "Global/Wire Length", "NAME": "mat_length", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Circle", "NAME": "mat_circle", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Pointilize", "NAME": "mat_connect", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button" },  

		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

		{
			"NAME": "spectrum",
			"TYPE": "audioFFT",
			"SIZE": 12,
			"ATTACK": 0.05,
			"DECAY": 0.0,
			"RELEASE": 0.1
		},

	],

	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": "mat_y", "value3":2}},
	],

	"RENDER_SETTINGS": {
	   "POINT_COUNT": "mat_instance_count"
	}
}*/

#include "MadNoise.glsl"

mat2 rot(float a)
{
	return mat2(cos(a),sin(a),-sin(a),cos(a));
}

const float pi = 3.14159265359;

float tt(in float t)
{
	return floor(t) + smoothstep(0.,1.,fract(t));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float W = 2./float(mat_x);
	float H = 2./float(mat_y);
	float nx = -1 + (pointNumber/2)%(mat_x)* W + W*0.5;
	float ny = -1 + ((pointNumber/2)/mat_x)* H + H*0.5;
	float normalizedPos = float(pointNumber/2)/((pointCount/2)-1);
	vec2 pos2d = vec2(nx,ny);

	vec3 n = dFlowNoise( pos2d*mat_scale, mat_time )*mat_power;

	float k = IMG_NORM_PIXEL(spectrum,vec2(0.2+normalizedPos*0.03,0)).r*mat_audio*4.;
	k = pow(max(0.05,k-0.3),4.)*10.*mat_length;

	int id = pointNumber%2;	

	float shape = billowedNoise(vec3(pos2d*10.+vnoise(pos2d.xy)*10.,mat_time*4.))*30;

	if(mat_connect&& k > 0.) shapeNumber = int(floor(shape));
	else shapeNumber = pointNumber;

	if(id == 1) 
	{
		pos2d.x += k;
		shapeNumber =  pointNumber-1;
	}

	int idx = pointNumber;
	pos = (pos2d + n.yz*0.1)*max(0.001,mat_scale);

	float rn = (noise(vec2(mat_time,(pointNumber/2)*0.9))*2.-1)*0.5;
	pos *= rot(rn*k);

	pos = mix(pos,vec2(0),k);

	// autorotate
	float t = tt(tt(tt(tt(mat_time*3.14156*0.1))));
	pos *= rot(t + mat_time*0.1);

	float pi = 6.34;
	vec2 pos_circle = normalize(pos.xy)*mat_scale;
	pos = mix(pos,pos_circle,mat_circle);

	vec3 col = mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(fract(normalizedPos)));
	float bright = fract(normalizedPos*8);
	color = vec4(col,1.);
}