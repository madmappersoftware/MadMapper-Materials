/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nfranz",
    "DESCRIPTION": "fBm noise in a fBm noise in a fBm noise in a ...",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [
	    { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0, "MAX": 5.0, "DEFAULT": 2.0 },
		{ "LABEL": "Noise 1", "NAME": "uN1", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Noise 2", "NAME": "uN2", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Noise 3", "NAME": "uN3", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 1.0 },

		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.7 },
	  ],
	  	 "GENERATORS": [
         {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
float pattern(in vec2 p){
	
	 vec2 q = vec2( fBm( p + vec2(0.0,animation_time) ),
                   fBm( p + vec2(4.2,1.3*animation_time) ) );
	
	 vec2 r = vec2( fBm( p + q + vec2(1.7,6.2) ),
                     fBm( p + q + vec2(animation_time,2.8) ) );
	
	vec2 s = vec2( fBm( p + q + vec2(animation_time,9.2) ),
                     fBm( p + q + vec2(animation_time,3.8) ) );
	
	return fBm(p*uN1 + r*uN2 + s*uN3);
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord*vec2(uScale);
	float p = pattern(uv);
	
	//////// brightness + contrast
	// Apply contrast
    p = mix(0.5, p, contrast);
    // Apply brightness
    p += brightness;
	
	return vec4(vec3(p),1);
}
