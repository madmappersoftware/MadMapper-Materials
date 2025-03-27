/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nadapted from Shadertoy MtBSWy\noriginal by schwenk",
    "DESCRIPTION": "LED gradient rendering",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"


float angle_trunc(float a)
{
    for( int i = 0; i < 32; i++)
    {
        if( a >= 0.0 )
            break;
        else
        	a += PI * 2.0;
    }
    return a;
}

vec4 translate(vec2 translation,vec4 lineSeg)
{
    return vec4(lineSeg.r-translation.x,lineSeg.g-translation.y,lineSeg.b-translation.x,lineSeg.a-translation.y);
}

float lineSegToBrightness(vec4 lineSeg)
{
    float alpha = atan(lineSeg.g,lineSeg.r);
    float beta =  atan(lineSeg.a,lineSeg.b);
    
    float bright = angle_trunc(alpha - beta);
    if(bright > PI) bright = angle_trunc(beta - alpha);
   
    return (bright/PI);
}


vec4 materialColorForPixel( vec2 texCoord )
{
	
   	vec4 lineSegment = vec4(0.5,0.5,0.5+0.5*sin(animation_time),0.5+0.5*cos(animation_time));
	vec2 uv = texCoord; 
    float bright = lineSegToBrightness(translate(uv,lineSegment));
	
	return vec4(vec3(bright),1.0);
}
