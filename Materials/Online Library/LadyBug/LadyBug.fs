/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy 3sfXWn",
    "DESCRIPTION": "Ladybug sliding effect,\nfrom Shadertoy",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 40.0, "DEFAULT": 8.0 }, 
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

        { "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

float circ(vec2 _st,float radius){
    return 1.-smoothstep(radius-0.05,radius,length(vec2(0.5) - _st));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);

	
	// make a color out of it
    vec3 color = vec3(0.);
	
    float t = mat_animation_time*2.;
    float x_offset = -cos(mod(t,PI));
    float y_offset = -cos(mod(t-PI/2.,PI));
    vec2 i = step(1.,mod(uv,2.));
    float x_f = step(0.,x_offset)*(x_offset*(1.+2.*(i.y-1.)));
    float y_f = step(0.,y_offset)*(y_offset*(1.+2.*(i.x-1.)));

    float N = worleyNoise(vec3(x_f,y_f,t))*0.4;

    float   col = 1.-circ(fract(uv+vec2(x_f,y_f)),0.3+N*mat_mix_noise);
    color = vec3(col);
	

	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
