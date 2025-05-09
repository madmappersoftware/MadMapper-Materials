/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy Md33zB\nby zifnab",
    "DESCRIPTION": "Imagination test",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.05 }, 
		{ "LABEL": "inkColor", "NAME": "inkColor", "TYPE": "color", "DEFAULT": [ 0.01, 0.01, 0.1, 1.0 ] },
		{ "LABEL": "paperColor", "NAME": "paperColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.98, 0.94, 1.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"

/*
 A noise function mirrored and thresholded to maximize the value at the center of the screen
 Combined with a second layer of noise to produce an ink on paper effect
*/

const float shadeContrast = 0.55;

//3D simplex noise from: https://www.shadertoy.com/view/XsX3zB
const float F3 =  0.3333333;
const float G3 =  0.1666667;

vec3 random3(vec3 c) {
    float j = 4096.0*sin(dot(c,vec3(17.0, 59.4, 15.0)));
    vec3 r;
    r.z = fract(512.0*j);
    j *= .125;
    r.x = fract(512.0*j);
    j *= .125;
    r.y = fract(512.0*j);
    return r-0.5;
}

float simplex3d(vec3 p) {
	 vec3 s = floor(p + dot(p, vec3(F3)));
	 vec3 x = p - s + dot(s, vec3(G3));
	 
	 vec3 e = step(vec3(0.0), x - x.yzx);
	 vec3 i1 = e*(1.0 - e.zxy);
	 vec3 i2 = 1.0 - e.zxy*(1.0 - e);
	 
	 vec3 x1 = x - i1 + G3;
	 vec3 x2 = x - i2 + 2.0*G3;
	 vec3 x3 = x - 1.0 + 3.0*G3;
	 
	 vec4 w, d;
	 
	 w.x = dot(x, x);
	 w.y = dot(x1, x1);
	 w.z = dot(x2, x2);
	 w.w = dot(x3, x3);
	 
	 w = max(0.6 - w, 0.0);
	 
	 d.x = dot(random3(s), x);
	 d.y = dot(random3(s + i1), x1);
	 d.z = dot(random3(s + i2), x2);
	 d.w = dot(random3(s + 1.0), x3);
	 
	 w *= w;
	 w *= w;
	 d *= w;
	 
	 return dot(d, vec4(52.0));
}

float fbm(vec3 p)
{
	float f = 0.0;	
	float frequency = 1.0;
	float amplitude = 0.5;
	for (int i = 0; i < 5; i++)
	{
		f += simplex3d(p * frequency) * amplitude;
		amplitude *= 0.5;
		frequency *= 2.0 + float(i) / 100.0;
	}
	return min(f, 1.0);
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

    vec2 coord = 1.0 - uv * 2.0;
    uv.x = 1.0 - abs(1.0 - uv.x * 2.0);

    vec3 p = vec3(uv, mat_animation_time);
    
    //Sample a noise function
    float blot = fbm(p * 3.0 + 8.0);
    float shade = fbm(p * 2.0 + 16.0);
    
    //Threshold
    blot = (blot + (sqrt(uv.x) - abs(0.5 - uv.y)));
    blot = smoothstep(0.65, 0.71, blot) * max(1.0 - shade * shadeContrast, 0.0);
    
	
	// make a color out of it
	vec3 color = mix(paperColor.rgb, inkColor.rgb, blot);
	color *= 1.0 - pow(max(length(coord) - 0.5, 0.0), 5.0);

	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
