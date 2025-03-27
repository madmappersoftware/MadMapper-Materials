/*{
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "I'm here for the plasma",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{
            "NAME": "ubrightness",
            "LABEL": "Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "ucontrast",
            "LABEL": "Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "usaturation",
            "LABEL": "Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "uhue_shift",
            "LABEL": "Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec3 palette( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	
	float t=animation_time;
	vec2 uv = texCoord * uScale;
	float A = 0.0;
	
	for(int i =0; i < 8;i++){
		vec2 r = vec2(0);
		r = sin(t*0.5 + i*uv.yx + i*t*0.4)*0.1;
		r *= vec2(A + uv.yx);
		A -= 0.143 ;
		A *= cos(A + t*0.45)+ 0.2;
		uv += r;		
	}
	
	float r = sin(uv.x* PI - t*0.4)*0.5+0.5;
	float g = sin(uv.y* PI * 4 - t*0.2)*0.5+0.5;
	
	vec3 P = palette( r, 
					 vec3(0.5, 0.5, 0.5),
					 vec3(0.5, 0.5, 0.5),
					 vec3(1.0, 1.0, 1.0),
					 vec3(0.6, 0.9, 0.25) );
	
	vec3 P2 = palette( g, 
					 vec3(0.5, 0.5, 0.5),
					 vec3(0.5, 0.5, 0.5),
					 vec3(1.0, 1.0, 1.0),
					 vec3(0.6, 0.9, 0.25) );
	
	vec3 col = mix(P,P2,g);
	
	vec3 out_color = col;
	
		// Apply Hue Shift and saturation
    if (uhue_shift > 0.01 || usaturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+uhue_shift));
        hsv.y = max(hsv.y + usaturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, ucontrast);

    // Apply brightness
    out_color.rgb += ubrightness;
	
	return vec4(out_color,1);
}
