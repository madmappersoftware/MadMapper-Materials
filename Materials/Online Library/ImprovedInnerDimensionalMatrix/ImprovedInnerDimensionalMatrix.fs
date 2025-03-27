/*{
	    "RESOURCE_TYPE": "Material For MadMapper",
	"CREDIT": "mojovideotec, Sam Nobs",
	"CATEGORIES" : [ "generator"
  ],
  "DESCRIPTION" : "",
  "VSN": "1.0",
  "INPUTS" : [
	{
     	"NAME" :		"seed1",
     	"TYPE" : 		"float",
    	"DEFAULT" :		155,
     	"MIN" : 		34,
      	"MAX" :			233
	},
    {
      	"NAME" :		"seed2",
      	"TYPE" :		"float",
      	"DEFAULT" :		649,
      	"MIN" : 		89,
      	"MAX" :			987	
	},
	{
		"NAME" : 		"scale",
		"TYPE" : 		"float",
		"DEFAULT" : 	1.0,
		"MIN" : 		0.25,
		"MAX" : 		2.0
	},
	{
		"NAME" : 		"rate",
		"TYPE" : 		"float",
		"DEFAULT" : 	1.0,
		"MIN" : 		0.1,
		"MAX" : 		3.0
	},
	{
		"NAME" : 		"zoom",
		"TYPE" : 		"float",
		"DEFAULT" : 	0.175,
		"MIN" : 		-1.0,
		"MAX" : 		1.0
	},
	{
		"NAME" : 		"line",
		"TYPE" : 		"float",
		"DEFAULT" : 	0.367,
		"MIN" : 		0.0,
		"MAX" : 		0.5
	},
	{
		"NAME" : 		"flash",
		"TYPE" : 		"float",
		"DEFAULT" : 	7.5,
		"MIN" : 		0.5,
		"MAX" : 		10.0
	},
	{
   		"NAME" : 		"mirror",
     	"TYPE" : 		"bool",
     	"DEFAULT" : 	false
   	},
	{
	  	"LABEL": "Color",
	  	"NAME": "mat_color",
	  	"TYPE": "color",
	  	"DEFAULT": [ 0.3, 0.0, 1.0, 1.0 ],
	  	"FLAGS": "no_alpha"
	},
  ],
	"GENERATORS": [
		{"NAME": "mat_rate_time", "TYPE": "time_base", "PARAMS": {"speed": "rate","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_rate_zoom", "TYPE": "time_base", "PARAMS": {"speed": "zoom","speed_curve": 1,"link_speed_to_global_bpm":false}},
		{"NAME": "mat_rate_flash", "TYPE": "time_base", "PARAMS": {"speed": "flash","speed_curve": 1,"link_speed_to_global_bpm":false}},

	],
}
*/


////////////////////////////////////////////////////////////////////
// InnerDimensionalMatrix  by mojovideotech
//
// based on :
// The Universe Within - by Martijn Steinrucken aka BigWings 2018
// shadertoy.com/\lscczl
// glslsandbox.com\/e#47584.1
//
// Converted to MadMapper and improved by Sam Nobs
//
//   Improvements/changes: 
//     - create time base for rate, zoom and flash so animation doesn't jump when the parameters are changed
//     - remove color cycle and color flag, instead add color chooser
//
//
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0
////////////////////////////////////////////////////////////////////

#include "MadCommon.glsl"


#ifdef GL_ES
precision highp float;
#endif

#define S(a, b, t) smoothstep(a, b, t)

float N1(float n) {
	return fract(sin(n) * 43758.5453123);
}

float N11(float p) {
	float fl = floor(p);
	float fc = fract(p);
	return mix(N1(fl), N1(fl + 1.0), fc);
} 

float N21(vec2 p) { return fract(sin(p.x * floor(seed1) + p.y * floor(seed2)) * floor(seed2+seed1)); }

vec2 N22(vec2 p) { return vec2(N21(p), N21(p + floor(seed2))); }

float L(vec2 p, vec2 a, vec2 b) {
	vec2 pa = p-a, ba = b-a;
	float t = clamp(dot(pa, ba)/dot(ba, ba), 0.0, 1.0);
	float d = length(pa - ba * t);
	float m = S(0.02, 0.0, d);
	d = length(a-b);
	float f = S(1.0, 0.8, d);
	m *= f;
	m += m*S(0.05, 0.06, abs(d - 0.5)) * 2.0;
	return m;
}

vec2 GetPos(vec2 p, vec2 o) {
	p += o;
	vec2 n = N22(p)*mat_rate_time;
	p = sin(n) * line;
	return o+p;
}

float G(vec2 uv) {
	vec2 id = floor(uv);
	uv = fract(uv) - 0.5;
	vec2 g = GetPos(id, vec2(0));
	float m = 0.0;
	for(float y=-1.0; y<=1.0; y++) {
		for(float x=-1.0; x<=1.0; x++) {
			vec2 offs = vec2(x, y);
			vec2 p = GetPos(id, offs);
			m+=L(uv, g, p);
			vec2 a = p-uv;
			float f = 0.003/dot(a, a);
			f *= pow( sin(N21(id+offs) * 6.2831 + (mat_rate_flash)) * 0.4 + 0.6, flash);
			m += f;
		}
	}
	m += L(uv, GetPos(id, vec2(-1, 0)), GetPos(id, vec2(0, -1)));
	m += L(uv, GetPos(id, vec2(0, -1)), GetPos(id, vec2(1, 0)));
	m += L(uv, GetPos(id, vec2(1, 0)), GetPos(id, vec2(0, 1)));
	m += L(uv, GetPos(id, vec2(0, 1)), GetPos(id, vec2(-1, 0)));
	return m;
}

vec4 materialColorForPixel(vec2 texCoord)
{
	vec2 uv = (2.25 - scale) * ( texCoord - vec2(0.5,0.5)) * 2;
	if (mirror) { if(uv.x<0.0) uv.x = abs(uv.x); }
	float m = 0.0;
	vec3 col;
	for(float i=0.0; i<1.0; i+=0.2) {
		float z = fract(i+mat_rate_zoom);
		float s = mix(10.0, 0.5, z);
		float f = S(0.0, 0.4, z) * S(1.0, 0.8, z);
		m += G(uv * s + (N11(i)*100.0) * i) * f;
	}
	col = mat_color.rgb;
	col *= m; 
	return vec4( col, 1.0 );
}