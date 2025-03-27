/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz cooked it up from the net",
    "DESCRIPTION": "Noise series: 4x4 voronoi kernel",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 3.0 }, 
        { "LABEL": "Complexity", "NAME": "mat_complex", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 5 },
		{ "LABEL": "Direction", "NAME": "mat_direction", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, -1.0 ] },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"
	
vec2 m = vec2(.7,.8);

float hash( in vec2 p ) 
{
    return fract(sin(p.x*15.32+p.y*5.78) * 43758.236237153);
}


vec2 hash2(vec2 p)
{
	return vec2(hash(p*.754),hash(1.5743*p.yx+4.5891))-.5;
}

vec2 hash2b( vec2 p )
{
    vec2 q = vec2( dot(p,vec2(127.1,311.7)), 
				   dot(p,vec2(269.5,183.3)) );
	return fract(sin(q)*43758.5453)-.5;
}


mat2 m2= mat2(.8,.6,-.6,.8);

// Gabor/Voronoi mix 4x4 kernel (clean but slower)
float gavoronoi4(in vec2 p)
{    
    vec2 ip = floor(p);
    vec2 fp = fract(p);
    vec2 dir = m;// vec2(.9,.7);
    float f = 2.*PI;																																										;//frequency
    float v = 1.;//cell variability <1.
    float dv = .7;//direction variability <1.
    float va = 0.0;
   	float wt = 0.0;
    for (int i=-2; i<=1; i++) 
	for (int j=-2; j<=1; j++) 
	{		
        vec2 o = vec2(i, j);
        vec2 h = hash2(ip - o);
        vec2 pp = fp +o  -v*h;
        float d = dot(pp, pp);
        float w = exp(-d*2.);
        wt +=w;
      	h= dv*h+dir;//h=normalize(h+dir);
        va +=cos(dot(pp,h)*f)*w;
	}    
    return va/wt;
}

float noiseg( vec2 p)
{   
    return gavoronoi4(p);
}
	
float fbm( vec2 p ) {
	
	float f=1.;
   
	float r = 0.0;	
    for(int i = 0;i<mat_complex;i++){	
		r += noiseg( p*f )/f;       
	    f *=2.;
        p+=vec2(.01,-.05)*r+.2*m*mat_animation_time/(.1-f);
	}
	return r;
}	

float map(vec2 p){
		return fbm(p)+1.;
}	
	
vec3 nor(in vec2 p)
{
	const vec2 e = vec2(0.002, 0.0);
	return -normalize(vec3(
		map(p + e.xy) - map(p - e.xy),
		map(p + e.yx) - map(p - e.yx),
		.15));
}	
	
	
vec4 materialColorForPixel( vec2 texCoord )
{
	m = mat_direction;
	// get texture coordinates
	vec2 uv = texCoord*vec2(-1);
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	

	vec2 p = uv;
   	p += .2*m*mat_animation_time;
    vec3 light = normalize(vec3(3., 2., -1.));
	float r;
    r = (dot(nor(p), light),0.25);
	
	// make a color out of it
	vec3 color = vec3( r);
	
	float k=map(p)*.8+.15;
	
	/// colorize
	color = clamp(vec3(r*k*k, r*k, r*sqrt(k)),0.,1.);	
//	color = vec3(dot(color,color));
	
	/// B+W
	color = color.rrr*1.2;

	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color.rbg,1.0);
	
	return final_color;
}
