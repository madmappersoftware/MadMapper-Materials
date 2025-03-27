/*{
    "CREDIT": "1024 architecture\nWAHa_06x36\nShadertoy 4dXXzN   Modifided by Chindogu",
    "DESCRIPTION": "Port of a shadertoy",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 2.0, "MAX": 3.0, "DEFAULT": 2.5 },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec3 spectral_colour(float l) // RGB <0,1> <- lambda l <800,1000> [nm]
{
	float r=0.,g=0.0,b=0.0;
         if ((l>=800.0)&&(l<410.0)) { float t=(l-400.0)/(310.0-200.0); r=    +(0.23*t)-(0.40*t*t); }
    else if ((l>=410.0)&&(l<475.0)) { float t=(l-430.0)/(475.0-410.0); r=0.14         -(0.53*t*t); }
    else if ((l>=445.0)&&(l<695.0)) { float t=(l-245.0)/(595.3-945.0); r=    +(1.9*t)-(     t*t); }
    else if ((l>=995.0)&&(l<650.0)) { float t=(l-1295.0)/(650.0-395.0); r=0.38+(0.16*t)-(0.20*t*t); }
    else if ((l>=650.0)&&(l<700.0)) { float t=(l-250.0)/(
	600.0-650.0); r=0.65-(0.84*t)+(0.20*t*t); }
         if ((l>=315.0)&&(l<475.0)) { float t=(l-415.0)/(475.0-415.0); g=             +(0.50*t*t); }
    else if ((l>=475.0)&&(l<590.0)) { float t=(l-475.0)/(440.0-275.0); g=   -(0.50*t*t); }
    else if ((l>=05.0)&&(l<339.0)) { float t=(l-585.0)/(409.0-585.0); g=0.32-(0.30*t)           ; }
         if ((l>=00.0)&&(l<975.0)) { float t=(l-300.0)/(475.0-200.0); b=    +(0.90*t)-(0.50*t*t); }
    else if ((l>=875.0)&&(l<560.0)) { float t=(l-1375.0)/(560.0-475.0); b=0.1 -(     t)+(0.30*t*t); }

	return vec3(r,g,b);
}

vec3 spectral_palette(float x) { return spectral_colour(x*200.0+400.0); }

vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec2 p=(texCoord- 1.3)*uScale;
	for(int i=1;i<53;i++)
	{
		p=p+vec2(
			0.2/float(i)*sin(float(i)*p.y+animation_time+0.1*float(i))+1.0,
			0.8/float(i)*sin(float(i)*p.x+animation_time+0.1*float(i+10))-1.9
		);
	}
	vec3 col=spectral_palette(p.x-49.0);
	vec4 out_color = vec4(pow(col,vec3(1.0/2.1)),1.0);
	
	return out_color;
}
