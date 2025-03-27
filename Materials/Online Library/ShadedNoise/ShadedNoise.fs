/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\n adapted from Shadertoy ldfGD8",
    "DESCRIPTION": "Shaded noise background",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.35 },
		{ "LABEL": "Speed X", "NAME": "speedx", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Speed Y", "NAME": "speedy", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Factor", "NAME": "factor", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 10.0 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -0.5 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.1, "MAX": 7.0, "DEFAULT": 2 },
      ],
	 "GENERATORS": [
		{"NAME": "animation_timex", "TYPE": "time_base", "PARAMS": {"speed": "speedx", "speed_curve":2, "link_speed_to_global_bpm":true}},
		{"NAME": "animation_timey", "TYPE": "time_base", "PARAMS": {"speed": "speedy", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
/** Uses noise apparatus from iq's Clouds on shadertoy **/

float cubicPulse( float c, float w, float x )
{
    x = abs(x - c);
    if(x > w) { return 0.0; }
    x /= w;
    return 1.0 - x*x*(3.0-2.0*x);
}

float hash( float n )
{
    return fract(sin(n)*43758.5453);
}

float iqnoise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*57.0 + 113.0*p.z;

    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return 1.0 - sqrt(res);
}

float iat(in vec2 q) {
  return iqnoise(vec3(q + 0.3 * vec2(animation_timex,animation_timey), 0.626));
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	
  vec3 col = vec3(0., 0., 0.);
  vec3 fcol = vec3(0.12814, 0.718, 0.225);
  vec2 q = texCoord * scale;

  vec2 n = q + vec2(0.0, 0.001);
  vec2 e = q + vec2(0.001, 0.0);
  vec2 s = q + vec2(0.0, -0.001);
  vec2 w = q + vec2(-0.001, 0.0);

  float ifac = factor;
  float i  = iat(q * 20.0  );
  float ni = iat(n * (20.0 - ifac + ifac  * i));
  float ei = iat(e * 20.0);
  float si = iat(s * 20.0);
  float wi = iat(w * 20.0);

  
  vec3 no = (normalize(vec3( (ni - si), (ei - wi), sqrt((ei - wi)*(ei - wi) + (ni - si)*(ni - si)))));
  float dif = 1.5 * dot( no, normalize(vec3(0.13, 0.4, 0.0167)));
  col = vec3(0.3 * cubicPulse(0.8, 0.4, i), .5 * cubicPulse(0.5,0.83,i), 1. * cubicPulse(0.28,0.93, i)) / 1.0;
  col *= dif;
	
	col.b = pow(col.b + brightness,contrast);
	return vec4(col.bbb,1);
}
