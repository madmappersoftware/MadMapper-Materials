/*{
    "CREDIT": "1024 architecture",
    "TAGS": "graphic",
    "INPUTS": [
	    { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "int", "MIN": 2, "MAX": 20, "DEFAULT": 6 },
        { "LABEL": "Shade", "NAME": "shade", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Frequency", "NAME": "freq", "TYPE": "float", "MIN": 0.10, "MAX": 10.0, "DEFAULT": 2.0 },
		{ "LABEL": "Ridge", "NAME": "ridge", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Offset", "NAME": "offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "Label": "Speed/Speed", "NAME": "speed_1", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Color/Top Color", "NAME": "uColorTop", "TYPE": "color", "DEFAULT": [ 0.9, 0.9, 0.9, 1.0 ] },
		{ "LABEL": "Color/Bottom Color", "NAME": "uColorBottom", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": -0.1 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 2.0 },
      ],
	"GENERATORS": [
		{"NAME": "time_1", "TYPE": "time_base", "PARAMS": {"speed": "speed_1", "speed_curve": 2, "link_speed_to_global_bpm":true}},
    ],	  
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec4 getColor(in vec2 uv, in vec4 params, in vec3 topColor, in vec3 bottomColor)
{
	float time =  params.y;
	float value = vnoise(vec2(uv.x * params.x + time*0.01,time*0.1))*2.0 -1.0;
	value += cos((uv.x * params.x * 2.0 + time)*freq ) * 0.25;
	value += billowedNoise(vec3(uv*freq,time));
	value += ridgedNoise(vec2(uv.x * params.x * 4.0 + time * 0.10,time*0.1))*ridge ;
	value = (value + 1.7) / 3.4;

	float height = uv.y * params.z;
	float alpha_W = smoothstep(height, height+0.03, value);

	float colorHeight = height * shade;
	float colorAlpha = 1.0 - smoothstep(colorHeight, colorHeight + 1.0, value);
	vec3 color = mix(topColor, bottomColor, colorAlpha);	
	return vec4(color, alpha_W);
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec2 uv = vec2(1,1) -texCoord;
	uv.x += time_1;

	vec3 color = mix(vec3(0.3, 0.2, 0.2), vec3(0.5, 0.6, 1.0), uv.y);
	vec4 curve;

	for(int i = 0; i< iterations; i++){
	curve = getColor(uv, 
		vec4(offset + float(i), time_1+ float(i),float(i)*offset, float(i)*0.1),
		uColorBottom.rgb, uColorTop.rgb);
	color = mix(color, curve.rgb, curve.a);
	}

		//////// brightness + contrast
	// Apply contrast
    color = mix(vec3(0.5), color, contrast);
    // Apply brightness
    color += vec3(brightness);
	
	return vec4(color,1);

}

// adapted from ShaderToy 4dX3RH
