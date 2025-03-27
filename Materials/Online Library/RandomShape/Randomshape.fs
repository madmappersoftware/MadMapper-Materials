/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "ported by frZ",
    "DESCRIPTION": "Random shapes generator",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 4.0 }, 
		{ "LABEL": "Morph", "NAME": "mat_morph", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Mode", "NAME": "mat_mode", "TYPE": "long", "VALUES": [ "Fill", "Stroke" ], "DEFAULT": "Fill" },
		{ "LABEL": "Stroke Size", "NAME": "mat_stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.05 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 2.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 1, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],
}*/

	
float rand( inout uint seed )
{
    // integer hash from Hugo Elias
	seed = (seed << 13U) ^ seed;
    seed = seed * (seed * seed * 15731U + 789221U) + 1376312589U;
    uint seed2 = seed * seed;
    return float(seed2&0x7fffffffU)/float(0x7fffffffU);
}

// returns a signed distance field
float Polygon( vec2 uv, uint seed )
{
    float f = abs(uv.y-rand(seed)+.5)-rand(seed)-1.;
    f = max(f,abs(uv.x-rand(seed)+.5)-rand(seed)-.5);
    f = max(f,abs(dot(uv,vec2(1,1)/sqrt(2.))-rand(seed)+.5)-rand(seed)-1.);
    f = max(f,abs(dot(uv,vec2(1,-1)/sqrt(2.))-rand(seed)+.5)-rand(seed)-1.);
    return f;
}

float stroke(float v, float stroke_size)
{
	return (v>0 && v<stroke_size) ? -min(v,stroke_size-v) : 1;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv += vec2(-mat_offset.x,mat_offset.y);
	
	// make a color out of it
	vec3 color = vec3(0.);
	
    uint seed1 = 31713U + uint(mat_animation_time);
    uint seed2 = seed1+1U;

	float value;

	if (mat_morph>0) {
		float mixFactor = fract(mat_animation_time); // From 0-1
		mixFactor = max(mixFactor-(1-mat_morph),0)*1/mat_morph;
		value = mix(Polygon(uv*5.+vec2(-2.5),seed1),Polygon(uv*5.+vec2(-2.5),seed2),mixFactor);
	} else {
		value = Polygon(uv*5.+vec2(-2.5),seed1);
	}

	if (mat_mode == 0) {
	    color.rgb = vec3(value);
	} else {
	    color.rgb = vec3(stroke(value,mat_stroke_size));
	}
		
	float bloom = .01;
    color.rgb = .5+.5*color.rgb/(bloom+abs(color.rgb));
	
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
