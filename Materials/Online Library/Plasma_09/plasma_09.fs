/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "captain plassssma",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 5.0 },
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
            "DEFAULT": 1.1
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
		{ "LABEL": "Color/Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 0.0, 0.6, 0.9, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "uColorB", "TYPE": "color", "DEFAULT": [ 0.1, 0.2, 0.7, 1.0 ] },
		{ "LABEL": "Color/Color C", "NAME": "uColorC", "TYPE": "color", "DEFAULT": [ 0.3, 0.2, 0.0, 1.0 ] },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord * uScale;
	vec2 center = (texCoord-0.5)*2.0;
	float t = animation_time;
	
	uv *= billowedNoise(vec3(center,t));
	
	float c_1 = sin(uv.x + t)*0.5+0.5;
	float c_2 = cos(distance(vec2(c_1,uv.x),center+t))*0.5+0.5;
	float c_3 = sin(c_2+uv.y + t*0.45)*0.5+0.5;
	
	vec3 col = uColorA.rgb*c_1 + uColorB.rgb*c_2 + uColorC.rgb*c_3;
	
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
	
	return vec4(out_color,1.0);
	return vec4(c_1,c_2,c_3,1);
}
