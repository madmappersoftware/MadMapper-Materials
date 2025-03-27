/*{
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "who wants some plasma instead",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.5 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 },
		{ "LABEL": "Deformation", "NAME": "uDeform", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.9 },
		{ "LABEL": "Complexity", "NAME": "uComplexity", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 15 },
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
		{ "LABEL": "Color/Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 0.2, 0.9, 0.3, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "uColorB", "TYPE": "color", "DEFAULT": [ 0.0, 0.6, 0.1, 1.0 ] },
		{ "LABEL": "Color/Color C", "NAME": "uColorC", "TYPE": "color", "DEFAULT": [ 0.3, 0.2, 0.0, 1.0 ] },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
float m(vec3 p) 
{ 
	p.z+=5.*animation_time; 
    return length(.2*sin(p.x-p.y)+cos(p/3.))-.8;
    //return length(.2*sin(p.x-p.y)+cos(p/3.)-.1*sin(1.5*p.x))-.8;
}

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = (texCoord -0.5) * uScale;
	vec3 d= vec3(uv,0);
	vec3 o=d + vec3(curlNoise(uv,animation_time)*uDeform,1);
    for(int i=0;i<uComplexity;i++) o+=m(o)*d;
    vec3 c = abs(m(o+d)*vec3(.3,.15,.1)+m(o*.5)*vec3(.1,.05,0))*(8.-o.x/2.);
	
	vec3 col = uColorA.rgb*c.x + uColorB.rgb*c.y + uColorC.rgb*c.z;	
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
}
