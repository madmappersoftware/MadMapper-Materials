/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple atmospherical line",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Global/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
		{"LABEL": "Global/Max", "NAME": "mat_min", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. }, 
		{"LABEL": "Global/Min", "NAME": "mat_max", "TYPE": "float", "MIN": -1., "MAX": 0.0, "DEFAULT": -1. }, 

		{"LABEL": "Rotation/Initial", "NAME": "mat_rot", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
		{"LABEL": "Rotation/Speed", "NAME": "mat_rotspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Rotation/Automatic", "NAME": "mat_rotauto", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },

		{ "LABEL": "Color/Out", "NAME": "outColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Center", "NAME": "centerColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},


    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2, 
															 "link_speed_to_global_bpm":true}},
        {"NAME": "mat_rottime", "TYPE": "time_base", "PARAMS": {"speed": "mat_rotspeed","speed_curve": 2, 
															 "link_speed_to_global_bpm":true}}

    ],
}*/

#include "MadNoise.glsl"

const float pi = 3.14159265359;

float sinc( float x, float k )
{
    float a = pi*(k*x-1.0);
    return sin(a)/a;
}

float parabola( float x, float k )
{
    return pow( 4.0*x*(1.0-x), k );
}

vec3 hsv2rgb(vec3 c) {
  vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
  vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
  return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

mat2 frot(float a)
{
	return mat2(sin(a),cos(a),sin(-a),cos(a));
}
mat2 rotate(float a) {
	float s = sin(a);
	float c = cos(a);
	return mat2(c, -s, s, c);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float normalizedPos = float(pointNumber)/(pointCount-1);

	float t = mat_time;
	t = floor(t) + smoothstep(0.,1.,fract(t));

	float rt = parabola(fract(t),2.);

	// Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
	float normalizedPosInShape = float(pointNumber)/(pointCount-1) ;
	
	float coeff = sinc(rt,0.5);
	float k = fBm(vec2((normalizedPos-0.5)*mat_scale*10.,t))*mat_amp*2.*coeff;

	k = min(mat_min,k);
	k = max(mat_max,k);
	pos = vec2(normalizedPos*2.-1.,k);

	float rtt = parabola(fract(mat_rottime),2.);;
	float angle = flowNoise(vec2(rtt*0.1,mat_rottime*0.1),0.4567);
	pos *= rotate(mat_rot*pi);
	pos *= rotate(angle*pi*mat_rotauto*2.);
	
	shapeNumber = 1;

	float N = flowNoise(vec2(normalizedPos*4,coeff),t);
	if(N > 0.) N = 1; else N = 0.;
	float hue = parabola(normalizedPos,2.);
	color = vec4(mix(outColor.rgb,centerColor.rgb,vec3(hue)),1);
}
