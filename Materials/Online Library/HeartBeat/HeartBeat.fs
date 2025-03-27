/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Heartbeat animation for lights",
    "TAGS": "light_rythm",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Min", "NAME": "mat_min", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Show Curve", "NAME": "mat_show_curve", "TYPE": "bool",  "DEFAULT": 1, "FLAGS":"button" },
		{ "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "DEFAULT": 2., "MIN": 0.0, "MAX": 2. },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/
// based on shadertoy XdBBzG
//based on this formula : https://www.reddit.com/r/Physics/comments/30royq/whats_the_equation_of_a_human_heart_beat/cpw81wj/
// https://www.shadertoy.com/view/XdBBzG

float ecg(float x)
{   
   x = x - ceil(x/1.3 - 0.5)*1.3;   
   return 0.9*(exp((-pow(x + 1.4, 2.)) / (2.*0.02)) + exp((-pow(x - 1.4,2.)) / (2.*0.02))) + 
       (1. - abs(x / 0.1) - x)*exp((-pow(7.*x,2.)) / 2.);
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
	vec2 uv = texCoord;

	float E = ecg(mat_animation_time )*0.666+0.333;

	E = mat_min + (1.-mat_min)*E;

	vec3 color = vec3(E);

	float P = plot(vec2(uv.x,1.-uv.y)*1.1-0.05, mat_min + (1.-mat_min)*(ecg(uv.x + mat_animation_time)*0.66+0.333), 0.01);

	color 		 = mix(color,vec3(1,0,0),P*float(mat_show_curve));

	float line_0 = line(0.02,uv.y*1.1-0.05,0.004);
	float line_1 = line(0.98,uv.y*1.1-0.05,0.004);
	float line_2 = line(0.5,uv.y*1.1-0.05,0.004);
	float l 		 = max(max(line_0,line_1),line_2);
	color 		 =  mix(color,vec3(0.,l,0)*0.25,l*float(mat_show_curve));

	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
