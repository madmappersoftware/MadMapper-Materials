/*{
    "CREDIT": "1024 architecture\nadapted from Tekf",
    "DESCRIPTION": "Parametric Starfield",
    "TAGS": "space",
    "VSN": "1.1",
    "INPUTS": [
		{ "LABEL": "Amount", "NAME": "uAmount", "TYPE": "int", "MIN": 0, "MAX": 25, "DEFAULT": 15 },
		{ "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.7 },
    	{ "LABEL": "BPM Sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Gamma", "NAME": "uGamma", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 2.2 },
        { "LABEL": "Regularity", "NAME": "uRegularity", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },  
  		{ "LABEL": "Trail Length", "NAME": "uTrail", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1 },  
		{ "LABEL": "Position", "NAME": "uPos", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
	  ],
	  "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve": 2, "bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// make ray from uv and center on screen
	vec3 ray;
	ray.xy = ((texCoord-0.5)*2.0)+uPos*vec2(-1,1);
	ray.z = 1.0;
	vec3 stp = ray/max(abs(ray.x),abs(ray.y));
	vec3 pos = 2.0*stp+.5;

	// animate
	float offset = animation_time;	
	float speed2 = uSpeed;
	float speed = max(0,speed2+(uTrail-0.5));
	
	vec3 col = vec3(0);
	
	for ( int i=0; i < uAmount; i++ )
	{
		float z = billowedNoise(ivec2(pos.xy*uRegularity));
		z = fract(z-offset);
		float d = 44.0*z-pos.z;
		float w = pow(max(0.0,1.0-8.0*length(fract(pos.xy)-.5)),2.0);
		vec3 c = max(vec3(0),vec3(1.0-abs(d+speed2*.5)/speed,1.0-abs(d)/speed,1.0-abs(d-speed2*.5)/speed));
		col += 1.5*(1.0-z)*c*w;
		pos += stp;
	}
	return vec4(pow( col, vec3(1.0/uGamma) ),1.0);
}
