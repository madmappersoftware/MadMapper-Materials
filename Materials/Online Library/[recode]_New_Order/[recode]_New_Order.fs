/*{
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inspired by New Order - Blue Monday, Brotherhood Album / 1986",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 6.0, "DEFAULT": 1.0 }, 
        { "LABEL": "Mix Radius", "NAME": "mat_mix", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
		{ "LABEL": "Mix Pos", "NAME": "mat_pos", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
		{ "LABEL": "Mix Smooth", "NAME": "mat_smooth", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },

		{ "LABEL": "C rotation", "NAME": "mat_crot", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. },

      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

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

float strokeDist2( float dist, float width, float smoothness )
{
    float aa = length( vec2( dFdx( -dist ), dFdy( -dist ) ) )*(1.+smoothness);
    return 1.0f - ( smoothstep( aa, -aa, -dist ) + smoothstep( -aa, aa, -dist - width ) );
}
vec3 stroke2( vec3 a, vec3 b, float dist, float width , float smoothness) 
{
    return mix( a, b, strokeDist2( dist, width, smoothness ) );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float t = mat_animation_time;

	// get texture coordinates
	vec2 uv = texCoord*2.-1.;

	// modify uv with material inputs
	uv *= mat_scale;

	float n[6];
	float s[6];
	vec2  p[6];

	for(int i = 0; i<7; i++){
		n[i] = flowNoise(vec2(i*0.33,2.34),t);
		s[i] = flowNoise(vec2(i*0.53,435.87),t+0.34);
		p[i] = dFlowNoise(vec2(i*0.43,34.11),t+0.48).yz;
	}

	float p_n = mat_mix * 0.5;
	float s_p = mat_smooth *100. ;
	float p_p = mat_pos*0.1;

	vec2 domain = uv;
	float C0 = circle(domain+ p[0]*p_p, 0.125f +n[0]*p_n);
	float C1 = circle(domain+ p[1]*p_p, 0.265f +n[1]*p_n);
	float C2 = circle(domain+ p[2]*p_p, 0.35f  +n[2]*p_n);
	float C3 = circle(domain+ p[3]*p_p, 0.56f  +n[3]*p_n);
	float C4 = circle(domain+ p[4]*p_p, 0.625f +n[4]*p_n);
	float C5 = circle(domain+ p[5]*p_p, 0.795f +n[5]*p_n);

	vec3 col_0 = vec3(.9,.29,.13);
	vec3 col_1 = vec3(.5,.29,.19);
	vec3 col_2 = vec3(.27,.27,.26);
	vec3 col_3 = vec3(.06,.33,.20);
	vec3 col_4 = vec3(.0,.68,.82);
	vec3	 col_5 = vec3(.13,.34,.58);

	vec3 color = vec3(0.);
	vec3 circleColor = vec3( 1.0f );

	color = stroke2(color, col_0, C0, 0.5,1.   + (s[0]*0.5+0.5)*s_p);
	color = stroke2(color, col_1, C1, 0.07,8. + (s[1]*0.5+0.5)*s_p);
	color = stroke2(color, col_2, C2, 0.06,6.  + (s[2]*0.5+0.5)*s_p);
	color = stroke2(color, col_3, C3, 0.08,8.  + (s[3]*0.5+0.5)*s_p);
	color = stroke2(color, col_4, C4, 0.02,0.  + (s[4]*0.5+0.5)*s_p);
	color = stroke2(color, col_5, C5, 0.11, 8. + (s[5]*0.5+0.5)*s_p);

	vec3 c_h = rgb2hsv(color);
	c_h.x = fract(c_h.x + mat_crot);	
	color = hsv2rgb(c_h);
	
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}