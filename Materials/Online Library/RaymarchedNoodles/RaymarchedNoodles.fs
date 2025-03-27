/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy wtl3zM\nby  omeometo",
    "DESCRIPTION": "graphic noodling",
    "TAGS": "raymarch",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 4.0 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
	
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
	

vec3 rand3(vec3 p){
    vec3 q=vec3(
        p.x*41.839+p.y*91.793+p.z*23.418,
        p.x*54.941+p.y*73.694+p.z*48.932,
        p.x*14.023+p.y*10.089+p.z*83.573
    );
	return fract(sin(q+0.25)*2434.2434)*2.0-1.0;
}

vec3 smth3(vec3 f){
	return f*f*f*(10.+f*(-15.+f*6.));
}
vec3 dsmth3(vec3 f){
	return 30.*(f*f*(1.+f*(-2.+f)));
}


void noisewithgrad3(out float val, out vec3 grad, in vec3 p){
	vec3 i=floor(p);
    vec3 f=fract(p);
    vec3 u=smth3(f);
    vec3 du=dsmth3(f);
    
    vec3 g000=rand3(i+vec3(0.0, 0.0, 0.0));
    vec3 g001=rand3(i+vec3(0.0, 0.0, 1.0));
    vec3 g010=rand3(i+vec3(0.0, 1.0, 0.0));
    vec3 g011=rand3(i+vec3(0.0, 1.0, 1.0));
    vec3 g100=rand3(i+vec3(1.0, 0.0, 0.0));
    vec3 g101=rand3(i+vec3(1.0, 0.0, 1.0));
    vec3 g110=rand3(i+vec3(1.0, 1.0, 0.0));
    vec3 g111=rand3(i+vec3(1.0, 1.0, 1.0));
    float a000=dot(g000, f-vec3(0.0, 0.0, 0.0));
    float a001=dot(g001, f-vec3(0.0, 0.0, 1.0));
    float a010=dot(g010, f-vec3(0.0, 1.0, 0.0));
    float a011=dot(g011, f-vec3(0.0, 1.0, 1.0));
    float a100=dot(g100, f-vec3(1.0, 0.0, 0.0));
    float a101=dot(g101, f-vec3(1.0, 0.0, 1.0));
    float a110=dot(g110, f-vec3(1.0, 1.0, 0.0));
    float a111=dot(g111, f-vec3(1.0, 1.0, 1.0));
   
    val = mix(
        mix(mix(a000, a001, u.z), mix(a010, a011, u.z), u.y), 
        mix(mix(a100, a101, u.z), mix(a110, a111, u.z), u.y), u.x); 
    grad = mix(
        mix(mix(g000, g001, u.z), mix(g010, g011, u.z), u.y), 
        mix(mix(g100, g101, u.z), mix(g110, g111, u.z), u.y), u.x); 
    grad += du * vec3(
    	mix(mix(a100-a000, a101-a001, u.z), mix(a110-a010, a111-a011, u.z), u.y),
    	mix(mix(a010-a000, a011-a001, u.z), mix(a110-a100, a111-a101, u.z), u.x),
    	mix(mix(a001-a000, a011-a010, u.y), mix(a101-a100, a111-a110, u.y), u.x)
    );
}


float dist(out vec3 grad, in vec3 p){
	//float val1=sqrt(dot(p,p))-1.0;
    //vec3 g1=normalize(p);
    float val1, val2;
    vec3 g1, g2;
    float r=2.0;
    noisewithgrad3(val1, g1, p*r+mat_animation_time*vec3(0.0, 1.0, 0.0));
    noisewithgrad3(val2, g2, p*r-mat_animation_time*vec3(0.0, 1.0, 0.0)-0.5);
    g1*=r;
    g2*=r;
    
	float a=dot(g1,g1), b=dot(g1,g2);
    float c=dot(g2,g1), d=dot(g2,g2);
    float det=a*d-b*c;
    float x=(d*val1-b*val2)/det;
    float y=(-c*val1+a*val2)/det;
    vec3 g=g1*x+g2*y;
    grad=normalize(g);
    return min(0.1, sqrt(dot(g,g))-0.01)*0.5;
//	return sqrt(val1*val1+val2*val2)*0.5-0.007;
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	
  vec3 pos = vec3(0.0, 0.0, 2.0); //vec3(cos(iTime)*3.0, 0.0, sin(mat_animation_time)*3.0);
    vec3 eye = vec3(0.0, 0.0, -1.0); //-normalize(pos);
    vec3 up = vec3(0.0, 1.0, 0.0);
    vec3 right = cross(eye, up);
    float angle = 0.5;
    vec3 ray = eye + (right * uv.x + up * uv.y) * angle;
    ray = normalize(ray);
    
	float col = 0.0;
    float col_cur = 1.0;
    for(int i=0;i<64;i++){
        vec3 grad;
    	float d = dist(grad, pos);
        if(d<1e-3){
        	float eps=1e-2;

            vec3 normal=grad;
			vec3 op_ray_dir = -normalize(ray);
			col=col_cur * max(dot(normal, op_ray_dir), 0.5);
            //col=col_cur;
            break;
        }
        pos += d * ray;
        col_cur -=1./64.;
    }
	
	// make a color out of it
	vec3 color = vec3(col);
	
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
