/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "plasma_man",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
		{
            "NAME": "brightness",
            "LABEL": "Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "contrast",
            "LABEL": "Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "saturation",
            "LABEL": "Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "hue_shift",
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
	float t = animation_time;
	vec2 uv = texCoord*uScale + vec2(2.2);
//	uv = sin(uv *2.34);
	vec2 center = uv*2.0 - 1.0;

	
	float c_1 = length(center*10.0 + vec2(sin(t*0.4),cos(t*0.45)))*0.34;
	float c_2 = distance(vec2(c_1), vec2(sin(t*0.45+uv.x*4.0),cos(t*0.76+uv.y*2.33)));
	float c_3 = length(vec2(sin(c_1+t*0.33),cos(c_2+t*.98)));
	
	float r = sin(c_1* PI * 0.23 + t*0.45)*0.5+0.5;
	float g = cos( c_2 * PI * 1 + t*.89)*0.5+0.5;
	float b = sin(c_3 * PI * 0.4 - t*0.23)*0.5+0.5;

	vec3 P = palette( r, 
					 vec3(0.5, 0.5, 0.5),
					 vec3(0.5, 0.5, 0.5),
					 vec3(2.0, 1.0, 0.0),
					 vec3(0.50, 0.20, 0.25) );
	
	vec3 P2 = palette( g, 
					 vec3(0.5, 0.5, 0.5),
					 vec3(0.5, 0.5, 0.5),
					 vec3(2.0, 1.0, 0.0),
					 vec3(0.50, 0.20, 0.25) );
	
	vec3 P3 = palette( b, 
					 vec3(0.5, 0.5, 0.5),
					 vec3(0.5, 0.5, 0.5),
					 vec3(2.0, 1.0, 0.0),
					 vec3(0.50, 0.20, 0.25) );
	
	vec3 col = (P - P3 * P2);
	
	vec3 out_color = col;
	
		// Apply Hue Shift and saturation
    if (hue_shift > 0.01 || saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+hue_shift));
        hsv.y = max(hsv.y + saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, contrast);

    // Apply brightness
    out_color.rgb += brightness;
	
	return vec4(out_color,1);
}
