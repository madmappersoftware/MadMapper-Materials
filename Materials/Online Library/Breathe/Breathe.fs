/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Breathing animation for lights",
    "TAGS": "light_rythm",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Min", "NAME": "mat_min", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Show Curve", "NAME": "mat_show_curve", "TYPE": "bool",  "DEFAULT": 1, "FLAGS":"button" },
        { "LABEL": "Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

float breathe(float t){

	float b = sin(t)*0.5+0.5;
	b = smoothstep(0.,1.,b);
	b= pow(b,mat_contrast);
	return b;
}	

float plot(vec2 st, float pct,float w){
  return  smoothstep( pct-w, pct, st.y) - 
          smoothstep( pct, pct+w, st.y);
}

float line(float y,float axis,float width){
	return step(y-width/2,axis) - step(y+ width/2.,axis);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs

	float b = breathe(mat_animation_time);	
	b = mat_min + (1.-mat_min)*b;

	// make a color out of it
	vec3 color = vec3(b);
	float p = plot(vec2(uv.x,(1.-uv.y)*1.1-0.05),mat_min + (1.-mat_min)*breathe(uv.x + mat_animation_time),0.02);

	color   		 = mix(color,vec3(p,0,0),p*float(mat_show_curve));
	
	float line_0 = line(0.02,uv.y*1.1-0.05,0.004);
	float line_1 = line(0.98,uv.y*1.1-0.05,0.004);
	float line_2 = line(0.5,uv.y*1.1-0.05,0.004);
	float l 		 = max(max(line_0,line_1),line_2);
	color 		 =  mix(color,vec3(0.,l,0)*0.25,l*float(mat_show_curve));

	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
