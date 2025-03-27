/*{
    "CREDIT": "Adapeted from Inigo Quilez",
    "DESCRIPTION": "XOR shift Noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 1., "MAX": 100.0, "DEFAULT": 20.0 }, 
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

const uint k = 1103515245U; 

vec3 hash( uvec3 x )
{
    x = ((x>>8U)^x.yzx)*k;
    x = ((x>>8U)^x.yzx)*k;
    x = ((x>>8U)^x.yzx)*k;
    
    return vec3(x)*(1.0/float(0xffffffffU));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord;
	uv *= mat_scale;
	vec3 color = hash(uvec3(uv*10,mat_animation_time*1000));
			
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
