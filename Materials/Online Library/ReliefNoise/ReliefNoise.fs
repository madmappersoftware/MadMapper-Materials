/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "multi-scale noise\n+flow deformation",
    "TAGS": "Noise",
    "VSN": "1.0",
    "INPUTS": [ 

		{ "Label": "Noise Type", "NAME": "mat_type","TYPE": "long", "DEFAULT": "Ridged", "VALUES": [ "Ridged", "Worley","Billow","fBm" ] },
		
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Flow", "NAME": "mat_flow", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
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
	
vec4 materialColorForPixel( vec2 texCoord )
{
	float t = mat_animation_time;
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	
	float f = 0.0;

    mat2 m = mat2( 1.6,  1.2, -1.2,  1.6 );
    float flow = sin(t*0.1+5.0)*1.0 + mat_flow*4.;


	if(mat_type == 0){
		f  = 0.5000*ridgedMF( vec3(uv,t) ); uv = m*uv;
		f += 0.2500*ridgedMF( uv +f*flow); uv = m*uv;
		f += 0.1250*ridgedMF( uv +f*flow); uv = m*uv;
	}

	if(mat_type == 1){
		f  = 0.5000*worleyNoise( vec3(uv,t) ); uv = m*uv;
		f += 0.2500*worleyNoise( uv +f*flow*2.); uv = m*uv;
		f += 0.1250*worleyNoise( uv +f*flow*2.); uv = m*uv;
	}

	if(mat_type == 2){
		f  = 0.5000*billowedNoise( vec3(uv,t) ); uv = m*uv;
		f += 0.2500*billowedNoise( uv +f*flow); uv = m*uv;
		f += 0.1250*billowedNoise( uv +f*flow); uv = m*uv;
	}

	if(mat_type == 3){
		f  = 0.5000*abs(fBm( vec3(uv,t))); uv = m*uv;
		f += 0.2500*abs(fBm( uv +f*flow)); uv = m*uv;
		f += 0.1250*abs(fBm( uv +f*flow)); uv = m*uv;
	}

	// make a color out of it
	vec3 color = vec3(f);
	
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
