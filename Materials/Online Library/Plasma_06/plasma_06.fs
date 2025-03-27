/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "did you say plasma ?",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "ColorMix", "NAME": "uMix", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 2 },
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
            "DEFAULT": 0.487
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
	
	float t=animation_time;
	vec2 uv = texCoord * 5.0 * uScale;
    vec2 uv0=uv;
	float i0=1.0;
	float i1=1.0;
	float i2=1.0;
	float i4=0.0;
	for(int s=0;s<uMix;s++)
	{
		vec2 r;
		r=vec2(cos(uv.y*i0-i4+t/i1),sin(uv.x*i0-i4+t/i1))/i2;
        r+=vec2(-r.y,r.x)*0.3;
		uv.xy+=r;
        
		i0*=1.83;
		i1*=1.1;
		i2*=1.6;
		i4+=0.05+0.1*t*i1;
	}
    float r=sin(uv.x-t)*0.5+0.5;
    float b=cos(uv.y+t)*0.5+0.5;
    float g=sin((uv.x+uv.y+cos(t*0.5))*0.5)*0.5+0.5;

	vec3 col = uColorA.rgb * r + uColorB.rgb * g + uColorC.rgb * b;
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
