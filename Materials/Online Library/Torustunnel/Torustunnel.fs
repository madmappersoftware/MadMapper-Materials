/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy tlsGz2\nby Caneta",
    "DESCRIPTION": "abstract tunnel",
    "TAGS": "tunnel",
    "VSN": "1.1",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
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
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

float tr = 50.0;
#define P(t,tt) vec3(cos(ti+t) * tr, sin(ti) * 0.1, sin(ti+tt) * tr);

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-1,-1)+vec2(-mat_offset.x,mat_offset.y);
	
    vec2 p =uv;
    float ti = mat_animation_time*0.1;
    vec3 ro = P(0.,0.);
    vec3 ta = P(sin(ti), cos(ti));
    vec3 fo = normalize(ta-ro);
    vec3 ri = normalize(cross(vec3(cos(ti*0.5),sin(ti*0.5),0.), fo));
    vec3 up = normalize(cross(fo,ri));
    vec3 ray = mat3(ri,up,fo) * normalize(vec3(p, 1.5));
    
    float t = 0.0;
    vec3 col = vec3(0.);
    float a = 1.0;
    for(int i=0;i<100;i++) {
        vec3 pos = ro + ray * t;
    	float d = -length(vec2(length(pos.xz) - tr, pos.y)) + 5.0;
        if (d < 0.001) {
    		vec2 uv = vec2(atan(pos.z, pos.x), atan(pos.y, length(pos.xz) - tr)) / (acos(-1.)*2.) + 0.5;
            float c = smoothstep(0.05, 0.00, abs(fract(uv.y * 5.0) - 0.5));
            c = mix(c, 1.0, smoothstep(0.1, 0.00, abs(fract(uv.x * 10.0) - 0.5)));
            col += mix(vec3(c), mix(vec3(7., 4., 2.), vec3(2., 4., 7.), sin(ti) * 0.5 + 0.5), 1.0-exp(-t * 0.005)) * a;
            a *= 0.25;
            t = 0.002;
            ro = pos;
            ray = reflect(ray, normalize((normalize(vec3(pos.x, 0., pos.z)) * tr) - pos));
        }
     t += d;
	}
	// make a color out of it
	vec3 color = col;
	
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
