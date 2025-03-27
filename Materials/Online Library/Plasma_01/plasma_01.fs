/*{
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "First iteration of the Plasma serie",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.8 },
		{
            "NAME": "ubrightness",
            "LABEL": "Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.1
        },
        {
            "NAME": "ucontrast",
            "LABEL": "Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.1
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
            "DEFAULT": 0.186
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

vec3 palette( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec2 uv = texCoord*uScale;
	float t = animation_time;
	
	float color_1,color_2,color_3;

	color_1 = sin (dot(uv *10, vec2(sin(t*0.78),cos(t*0.34)) + sin(t*0.238)*0.1)) *0.15;
	color_2 = cos( length((uv-0.5)*2.0  )*0.4 );
	color_3 = sin(uv.y*1 - color_2*uv.x);
	float r = sin(PI * color_1 + uv.x + t)*0.5+0.5;
	float g = cos(PI * color_2 + uv.y - t)*0.5+0.5;
	float b = sin((PI * color_3 + uv.x + t))*0.5+0.5;
	
	
	vec3 col = uColorA.rgb*r + uColorB.rgb*g + uColorC.rgb*b;
	
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

	return vec4(out_color,1.0);
}
