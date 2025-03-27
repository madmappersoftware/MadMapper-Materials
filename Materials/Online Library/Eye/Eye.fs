/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Tristan Le Moigne",
    "DESCRIPTION": "Inspired by Inigo Quilez Tutorial",
    "TAGS": "graphics",
    "VSN": "1.0",
    "INPUTS": [
		{ "LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0001, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Pupil Size", "NAME": "mat_pupil_size", "TYPE": "float", "MIN": 0.0001, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Center Size", "NAME": "mat_center_size", "TYPE": "float", "MIN": 0.0001, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Iris Density", "NAME": "mat_iris_density", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Speed", "NAME": "mat_move_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Color/Iris Color 1", "NAME": "mat_iris_color1", "TYPE": "color", "DEFAULT": [0.0, 0.3, 0.4, 1.0] },
		{ "LABEL": "Color/Iris Color 2", "NAME": "mat_iris_color2", "TYPE": "color", "DEFAULT": [0.2, 0.5, 0.4, 1.0] },
		{ "LABEL": "Color/Center Color", "NAME": "mat_iris_center_color", "TYPE": "color", "DEFAULT": [0.9, 0.6, 0.2, 1.0] },
		{ "LABEL": "Color/Back Color", "NAME": "mat_back_color", "TYPE": "color", "DEFAULT": [1,1,1,1] },
		{ "LABEL": "Reflection/Amount", "NAME": "mat_reflection_amount", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Reflection/Size", "NAME": "mat_reflection_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 }, 
		{ "LABEL": "Reflection/Pos", "NAME": "mat_reflection_pos", "TYPE": "point2D", "MIN": [-1.0,-1.0], "MAX": [1.0,1.0], "DEFAULT": [-0.24, 0.2] }, 
     ],
	"GENERATORS": [
		{ "NAME": "mat_move_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_move_speed"}},
	]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

mat2 m = mat2(0.8, 0.6, -0.6, 0.8);

float fbm(vec2 p)
{
	float f = 0.0;
	f += 0.5000 * vnoise(p); p *= m*2.02;
	f += 0.2500 * vnoise(p); p *= m*2.03;
	f += 0.1250 * vnoise(p); p *= m*2.01;
	f += 0.0625 * vnoise(p); p *= m*2.04;
	f /= 0.9375;
	return f;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 p = texCoord * 2.0 - 1.0;
	p /= mat_size;
	
	float r = sqrt(dot(p,p)); // Distance of every pixels from the center of the screen

	if(r<0.8)
	{
		vec3 color = mat_iris_color1.rgb;

		// Blue
		color = mix(color,
					mat_iris_color2.rgb,
					fbm(5.0 * p));
		
		// Yellow
		color = mix(color,
					mat_iris_center_color.rgb,
					1.0 - smoothstep(0.2*mat_center_size, 0.5*mat_center_size, r / mat_pupil_size));
		
		// Domain distortion
		float angle = atan(p.y, p.x); // Angle
		if (angle < 0) angle += 2*PI;
		if (angle > PI) angle = 2*PI-angle;
		angle += 0.05 * fbm(15.0*p); //0.1 = amp 5.0 = freq 	
		angle *= mat_iris_density;

		// Iris
		color = mix(color,
					vec3(1.0),
					smoothstep(0.3, 1.0,fbm(vec2(6.0*r - mat_move_time, 20*angle))));
		
		// Dark nuances
		color *= 1.0 - 0.5*smoothstep(0.4, 0.9, fbm(vec2(10.0*r, 15.0*angle)));
		
		// Volume Effect
		color *= 1.0 - 0.5*smoothstep(0.6, 0.8, r);	
	
		// Pupil
		color *= smoothstep(0.2, 0.25, r / mat_pupil_size);

		// Fake reflections
		float f = 1.0 - smoothstep(0.0, mat_reflection_size, length(p + mat_reflection_pos*vec2(-1,1))); //0.25 = size of reflection // Distance of every pixels from the center of reflection
		color += mat_reflection_amount*vec3(1.0, 0.9, 0.8)*f*0.9; // 0.9 = softers

		// Edges smooth
		return mix(vec4(color,1),
					vec4(mat_back_color.rgb*mat_back_color.a,mat_back_color.a), // Premultipl alpha
					smoothstep(0.7, 0.8, r));
	} else {
		return vec4(mat_back_color.rgb*mat_back_color.a,mat_back_color.a); // Premultipl alpha
	}
}
