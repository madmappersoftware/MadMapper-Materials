/*{
    "CREDIT": "shadertoy MtXcRj",
    "DESCRIPTION": "Labirynth effect",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 

		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 0.12 },
		{ "LABEL": "Move/Automove", "NAME": "mat_auto", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" }, 
		{ "LABEL": "Move/Speed", "NAME": "mat_mspeed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.4 }, 
		{ "LABEL": "Move/Amplitude", "NAME": "mat_amplitude", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_mtime", "TYPE": "time_base", "PARAMS": {"speed": "mat_mspeed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadNoise.glsl"

/////

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

/////

const float PI = 3.14159265359;
	
#define c30 0.86602540378
#define hm  (4.0/3.0)
#define grid 20.0
#define grid2 1.0
#define smooth (1.0 / uv.y  )
#define timeScale 0.1
#define rt (mat_animation_time * timeScale)

vec2 hex(vec2 v){
	float yc = abs(mod(v.x + floor((v.y*hm + 0.5))*(c30 / 2.0),c30) / c30 - 0.5);
	float y = floor(v.y*hm + yc);
	float x = floor(v.x/c30 + 0.5 + fract(y / 2.0))- fract(y / 2.0);
	return vec2(x*c30,y /hm + 1.0/(hm * 4.0));
}

float rand(vec2 v){
    return fract(sin(dot(v,vec2(5.11543,71.3132)))*43758.5453);
}

float rand(vec3 v){
    return fract(cos(dot(v,vec3(13.46543,67.1132,123.546123)))*43758.5453);
}

float layer(vec2 uv){

    vec2 fuv = floor(uv * grid);
    float nos = fract(rand(fuv) + rt);
    float fl = uv.y + (step(nos,0.5) * 2.0 - 1.0) * uv.x;
    float finCol = abs(fract(fl * grid2 * grid) - 0.5);
	finCol = step(0.3,finCol);

	uv = rot(PI*0.25)*uv;

    return finCol;
}	

float retime(in float t)
{
	return (floor(t) + smoothstep(0.,1.,fract(t)) );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord-0.5;

	uv = rot(PI*0.25)*uv;	
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);

	if(mat_auto)
	{
		float t = retime(mat_mtime);
		t = retime(retime(t));
		float x = flowNoise(vec2(t*0.4,1.234),34.567);
		float y = flowNoise(vec2(t*0.4,0.497),7.0007);

		uv += vec2(x,y)*mat_amplitude;
		float a = flowNoise(vec2(t*0.4,0.765),0.5678);
		uv *= rot(a);
	}
	
	// make a color out of it
	vec3 color = vec3(layer(uv));
	
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
	return vec4(color,1.0);
}
