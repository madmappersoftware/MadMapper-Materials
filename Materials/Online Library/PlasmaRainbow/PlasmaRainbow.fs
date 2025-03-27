/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Plasma effect",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Cycle", "NAME": "mat_cycle", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		
		{ "Label": "Mode", "NAME": "mat_mode","TYPE": "long", "DEFAULT": "Sine", "VALUES": [ "Billow", "Worley","Mixed","Sine" ] },
		
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

vec3 hue2rgb(in float hue)
{
    // Converting pure HUE to RGB (see http://www.chilliant.com/rgb2hsv.html)
  
    vec3 out_rgb = vec3(      abs(hue * 6. - 3.) - 1.  ,
    				     2. - abs(hue * 6. - 2.)       ,
        			     2. - abs(hue * 6. - 4.)       );    
    return out_rgb;
}

#define t mat_animation_time

vec4 materialColorForPixel( vec2 texCoord )
{
	
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	
	vec3 col = vec3(0.);

	if(mat_mode == 0){ col = hue2rgb( fract(mat_cycle + billowedNoise(vec3(uv,t)) )   ) ; }
	if(mat_mode == 1){ col = hue2rgb( fract(mat_cycle + worleyNoise(vec3(uv,t)))); }
	if(mat_mode == 2){ col = hue2rgb( fract(mat_cycle + mix( worleyNoise(vec3(uv,t)), billowedNoise(vec3(uv,t)),0.5  ) )); }

	if(mat_mode == 3){
    col = vec3( hue2rgb( fract( uv.x - (t * 1.)  + sin( uv.y + t) + mat_cycle)  ).r,
                     hue2rgb( fract( uv.x - (t * 1.1) + sin( uv.y + t) + mat_cycle)  ).g,
                     hue2rgb( fract( uv.x - (t * 1.2) + sin( uv.y + t) + mat_cycle)  ).b                     
                   );
	} 
	
	// make a color out of it
	vec3 color = col;

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
