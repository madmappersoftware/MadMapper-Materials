/*{
    "CREDIT": "shadertoy MdGGRW",
    "DESCRIPTION": "Explosion effect",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Amount", "NAME": "mat_amount", "TYPE": "float", "MIN": 0.2, "MAX": 2.0, "DEFAULT": 1. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

	
vec4 cellColor = vec4(0.0,0.0,0.0,0.0);
vec3 cellPosition = vec3(0.0,0.0,0.0);
float cellRandom = 0.0, onOffRandom = 0.0;


float random (vec3 i){
	return fract(sin(dot(i.xyz,vec3(4154895.34636,8616.15646,26968.489)))*968423.156);
}

vec4 getColorFromFloat (float i){
    i *= 2000.0;
    return vec4(normalize(vec3(abs(sin(i+radians(45.0))),abs(sin(i+radians(90.0))),abs(sin(i)))),1.0);
}

vec3 getPositionFromFloat (float i){
    i *= 2000.0;
    return vec3(normalize(vec3(abs(sin(i+radians(45.0))),abs(sin(i+radians(90.0))),abs(sin(i)))))-vec3(0.5,0.5,0.5);
}

float map(vec3 p){
    //p *= 1.0;
    cellRandom = random(floor((p*0.5)+0.0*vec3(0.5,0.5,0.5)));
    onOffRandom = random(vec3(5.0,2.0,200.0)+floor((p*0.5)+0.0*vec3(0.5,0.5,0.5)));
    cellColor = getColorFromFloat(cellRandom);
    cellPosition = getPositionFromFloat(cellRandom);
    p.x = mod(p.x, 2.0);
    p.y = mod(p.y, 2.0);
    p.z = mod(p.z, 2.0);
    p += 1.0*cellPosition.xyz;
    p += p.xyz*sin(10.0*mat_animation_time+onOffRandom*300.0)*			0.05;
    p += p.yzx*cos(10.0*mat_animation_time+onOffRandom*300.0+1561.355)*	0.05;
    if(onOffRandom>0.5){
    	return length(p-vec3(1.0,1.0,1.0)) - 0.2*cellRandom+0.02*(sin(mat_animation_time*20.0*onOffRandom+cellRandom*2000.0));
    } else {
        return 1.0;
    }
}

float trace(vec3 o, vec3 r){
    float t = 0.5;
    const int maxSteps = 48;
    for (int i = 0; i < maxSteps; i++){ 
        vec3 p = o + r * t;
        float d = map(p);
        t += d*0.35;
    }
    return t;
}
	
vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord - vec2(0.5,1.);
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	

    vec3 r = normalize(vec3(uv, 1.0));
    r = r*0.1+cross(r,vec3(0.0,1.0,-1.0));
    vec3 o;
    o.z = 8.5*mat_animation_time;
    o += vec3(0.52,0.5,-3.0);
    
    
    float t = trace(o,r*mat_amount);
    float fog = 1.0 / (1.0 + t * t * 0.1);
    vec3 fc = vec3(fog);
    
	vec4 fragColor = vec4(fc*vec3(28.0,10.0+-1.0*length(uv+vec2(0.0,1.0)),6.4)*0.6/length(uv+vec2(0.0,1.0))*1.0,1.0);

	vec3 color = fragColor.rgb;
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
