/*{
    "CREDIT": "shadertoy lslXRS",
    "DESCRIPTION": "Lava pond",
    "TAGS": "texture",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.5 }, 
		{ "LABEL": "Iterations", "NAME": "mat_iterations", "TYPE": "float", "MIN": 2., "MAX": 8.0, "DEFAULT": 4.0 }, 
			
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.5 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },

      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "noise.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
//#include "MadNoise.glsl"
#include "MadSDF.glsl"	

float mat_hash21(in vec2 n){ return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453); }
mat2 mat_makem2(in float theta){float c = cos(theta);float s = sin(theta);return mat2(c,-s,s,c);}
float mat_noise( in vec2 x ){return texture(mat_some_texture, x*.01).x;}

vec2 mat_gradn(vec2 p)
{
	float ep = .09;
	float gradx = mat_noise(vec2(p.x+ep,p.y))-mat_noise(vec2(p.x-ep,p.y));
	float grady = mat_noise(vec2(p.x,p.y+ep))-mat_noise(vec2(p.x,p.y-ep));
	return vec2(gradx,grady);
}

float mat_flow(in vec2 p)
{
	float z=2.;
	float rz = 0.;
	vec2 bp = p;
	for (float i= 1.;i < mat_iterations;i++ )
	{
		//primary flow speed
		p += mat_animation_time*.6;
		
		//secondary flow speed (speed of the perceived flow)
		bp += mat_animation_time*1.9;
		
		//displacement field (try changing time multiplier)
		vec2 gr = mat_gradn(i*p*.34+mat_animation_time*1.);
		
		//rotation of the displacement field
		gr*=mat_makem2(mat_animation_time*6.-(0.05*p.x+0.03*p.y)*40.);
		
		//displace the system
		p += gr*.5;
		
		//add noise octave
		rz+= (sin(mat_noise(p)*7.)*0.5+0.5)/z;
		
		//blend factor (blending displaced system with base system)
		//you could call this advection factor (.5 being low, .95 being high)
		p = mix(bp,p,.77);
		
		//intensity scaling
		z *= 1.4;
		//octave scaling
		p *= 2.;
		bp *= 1.9;
	}
	return rz;	
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;

	float rz = mat_flow(uv*3.);	
	vec3 color = vec3(.2,0.07,0.01)/rz;

	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;

		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
