/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "no more plasma",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 2.0 },
		{
            "NAME": "brightness",
            "LABEL": "D/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "contrast",
            "LABEL": "D/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "saturation",
            "LABEL": "D/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "hue_shift",
            "LABEL": "D/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
		{ "LABEL": "Color/Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 0.9, 0.6, 0.1, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "uColorB", "TYPE": "color", "DEFAULT": [ 0.7, 0.1, 0.0, 1.0 ] },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	
	float t=animation_time;
	vec2 uv = texCoord * uScale;
	vec2 center = uv*2.0-1.0;
	center += vec2(sin(t*0.4),cos(t*0.76))*sin(t*0.34);
	float A = 0;
	
	for(int i = 0;i< 6;i++){
		vec2 r = cos(vec2(t+center.x,t+center.y+0.1))*0.2;
		uv += sin(r);
		uv *= sin(distance(center,uv))*0.5+0.5;
//		uv += reflect(uv,texCoord)*0.4;		
	}
	
	float a = sin(uv.x)*0.5+0.5;
	float b = sin(uv.y)*0.5+0.5;
	
	vec3 col = uColorA.rgb*a + uColorB.rgb*b;
		
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
