/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "frz / 1024 architecture",
	"DESCRIPTION": "Atmospheric Planes",
	"TAGS": "laser",
	"VSN": "1.3",
	"INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_x", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 7 },
		{"LABEL": "Global/Join Corners", "NAME": "mat_join", "TYPE": "bool",  "DEFAULT": false, "FLAGS":"button" },
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 },  
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
		{"LABEL": "Global/Random Scale", "NAME": "mat_rscale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. }, 
		{"LABEL": "Global/Flicker", "NAME": "mat_flicker", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Global/Autorotate", "NAME": "mat_autorotate", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0  },
		{"LABEL": "Global/Autoshake", "NAME": "mat_autoshake", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },  

		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
	],
	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_x", "value2": 2, }},
	],
	"RENDER_SETTINGS": {
	   "POINT_COUNT": "mat_instance_count"
	}
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;

mat2 rot(float a) {
	float ca=cos(a);
	float sa=sin(a);
	return mat2(ca,sa,-sa,ca);  
}

float sm(in float t)
{
	return floor(t) + smoothstep(0.,1.,fract(t));
}

float parabola( float x, float k )
{
	return pow( 4.0*x*(1.0-x), k );
}
float sinc( float x, float k )
{
	float a = pi*(k*x-1.0);
	return sin(a)/a;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int repeat = int(mat_join)+1;
	int p = pointNumber % (pointNumber);

	float normalizedPos = float(p)/( pointCount);
	float t = sm(sm(sm(mat_time)));	
	
	float nx = billowedNoise(vec2(float(pointNumber / (2*repeat))*0.4056 + 1.2345,t*0.25))*2.-1.;
	float ny = billowedNoise(vec2(float(pointNumber / (2*repeat) )*0.345,t*0.25))*2.-1.;
	float rz = billowedNoise(vec2(0.603 + 2.347,t*0.125))*2.-1.;
	float sz = billowedNoise(vec2(float(pointNumber / (2*repeat) )*0.345,t*0.125));

	int point = pointNumber %(4);
	float bounce = sinc(fract(mat_time-0.7+(pointNumber/4)*0.01),20.5)*float(mat_autoshake);
	bounce *= 0.1;

	float curve = mix(1.,smoothstep(0.,1.2,sz),mat_rscale);

	if(point == 1) nx += mat_scale*curve ;
	if(point == 3) ny += mat_scale*curve ;
	if(point == 3 && pointNumber%8 == 7 && mat_join) ny = mix(ny,ny*-1.,mat_scale);

	pos = vec2(nx+bounce,ny - bounce);

	if(mat_autorotate > 0) pos *= rot( (rz*pi*0.5 + bounce*0.5) * mat_autorotate);

	shapeNumber = pointNumber/2;

	vec3 col = mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos));
	float n = vnoise(vec2(float(pointNumber/2),mat_time*100.));
	n = step(0.5,n);
	n = mix(1.,n,mat_flicker);
	color = vec4(col*n,1.);
}