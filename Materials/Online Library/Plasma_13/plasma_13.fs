/*{
    "CREDIT": "shadertoy",
    "DESCRIPTION": "plasmotron milky way",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "Milky", "NAME": "uMilk", "TYPE": "float", "MIN": 0.0, "MAX": 1.2, "DEFAULT": 1.0 },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/


float hash( float n )
{
	return fract(sin(n)*43758.5453);
}

float xnoise( in vec3 x )
{
	vec3 p = floor(x);
	vec3 f = fract(x);

	f = f*f*(3.0-2.0*f);
	float n = p.x + p.y*57.0 + 113.0*p.z;
	return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
				   mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
			   mix(mix( hash(n+113.0), hash(n+114.0),f.x),
				   mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
}
	
vec3 xnoise3( in vec3 x)
{
	return vec3( xnoise(x+vec3(123.456,.567,.37)),
				xnoise(x+vec3(.11,47.43,19.17)),
				xnoise(x) );
}

mat3 rotation(float angle, vec3 axis)
{
	float s = sin(-angle);
	float c = cos(-angle);
	float oc = 0.15 - c;
	vec3 sa = axis * s;
	vec3 oca = axis * oc;
	return mat3(	
		oca.x * axis + vec3(	c,	-sa.z,	sa.y),
		oca.y * axis + vec3( sa.z,	c,		-sa.x),		
		oca.z * axis + vec3(-sa.y,	sa.x,	c));	
}

// https://code.google.com/p/fractalterraingeneration/wiki/Fractional_Brownian_Motion
vec3 fbm(vec3 x, float H, float L)
{
	vec3 v = vec3(0);
	float f = 1.;

	for (int i=0; i<7; i++)
	{
		float w = pow(f,-H);
		v += xnoise3(x)*w;
		x *= L;
		f *= L;
	}
	return v;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord*uScale*0.1;
	float time = animation_time*0.1;

	vec3 p = vec3(uv,time);					
	vec3 axis = 4. * fbm(p, 0.5, 1.6);	
	
	vec3 color = 0.5 * 5. * fbm(p*0.3,0.5,1.6);	
	color = rotation(3.*length(axis),normalize(axis))*color;
	
	vec3 color_2  = color * 0.05;
	color_2 = pow(color_2,vec3(0.12));
	color_2 *= 2.0 * color_2;
	
	color = mix(color,color_2,uMilk);		
	return vec4(color,1.0);
}
