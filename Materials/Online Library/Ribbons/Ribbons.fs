/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "adapted from shadertoy tsGSzc by avin, ported by frz / 1024 ",
    "DESCRIPTION": "Ribonnish 70s thingie",
    "TAGS": "animation",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Count", "NAME": "mat_count", "TYPE": "float", "MIN": 1.0, "MAX": 200.0, "DEFAULT": 20.0 }, 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.7 }, 
		{ "LABEL": "Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "NoiseScale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1. }, 
		{ "LABEL": "Width", "NAME": "mat_width", "TYPE": "float", "MIN": 0.05, "MAX": 2.0, "DEFAULT": 0.7 }, 
		{ "LABEL": "Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 7.0, "DEFAULT": 0.0 },
		{ "LABEL": "Desync", "NAME": "mat_desync", "TYPE": "float", "MIN": 0.0, "MAX": 1., "DEFAULT": 0.2 }, 

		{ "LABEL": "Color/Hue", "NAME": "mat_hue", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Spread", "NAME": "mat_spread", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.15 },
		{ "LABEL": "Color/Saturation", "NAME": "mat_saturation", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

#define hue(h) clamp( abs( fract(h + vec4(3,2,1,0)/3.) * 6. - 3.) -1. , 0., 1.)
#define rand1(p) fract(sin(p* 78.233)* 43758.5453) 

#define COUNT 100.

#define MOD3 vec3(.1031,.11369,.13787)

vec3 hash33(vec3 p3)
{
	p3 = fract(p3 * MOD3);
    p3 += dot(p3, p3.yxz+19.19);
    return -1.0 + 2.0 * fract(vec3((p3.x + p3.y)*p3.z, (p3.x+p3.z)*p3.y, (p3.y+p3.z)*p3.x));
}
void rotate(in float angle, inout vec2 uv)
{    
    float ca = cos(angle);
    float sa = sin(angle);
    uv *= mat2(ca, -sa, sa, ca);	
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs

	uv -= vec2(0.5);
	uv *= mat_scale;
	uv.x == 0.25;
    float t1 = mat_animation_time;
	
    
    float sm = (1./1024. * 2.);  
    vec3 res = vec3(0.);

   for(float i=0.; i<mat_count;i+=1.){
	float k = i * mat_desync*0.05;
		float t =   floor(t1+k) + smoothstep(0.,1.,fract(t1+k))*smoothstep(0.,1.,fract(t1+k));
        vec2 oiuv = uv;
        vec2 iuv = uv;
        
        iuv.x += rand1(i+mat_count)*.5 - .25;
        
        iuv.x += flowNoise(vec2(i+t*0.2, oiuv.y*mat_noisescale + t), -t*0.1+rand1(i+mat_count))*mat_amp*0.5;

        float angle = rand1(i)*.5+mat_rotation;
        rotate(angle, iuv);        
        
        float perc = i/COUNT;
        
        float width = (perc + .5)*mat_width*0.1 - rand1(i+COUNT*3.)*.04;
        float ism = sm; // + (1. - perc)*.025;
            
   	 	float g = smoothstep(width + ism, width, abs(iuv.x));    
        float gSh = smoothstep(width, width + ism*10., abs(iuv.x));
        
        res = res*clamp(gSh + .5, .0, 1.);
        
        vec3 lineCol = hsv2rgb(vec3(0. + rand1(i+COUNT*2.)*mat_spread+mat_hue, (rand1(i)*.5+.5)*2.*mat_saturation, 1.0)).rgb;
        
        res = mix(res, lineCol,  g);
    }               

    
	
	// make a color out of it
	vec3 color = res;
	

	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
