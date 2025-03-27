/*{
    "CREDIT": "edwy",
    "DESCRIPTION": "Simple hexagon pattern",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#include "MadNoise.glsl"

// shadertoy NscSDB by edwy   |    https://www.shadertoy.com/view/NscSDB
vec2 hex(vec2 point){
    return vec2( (point.x + 1.7321 * point.y) / 3.0, (point.x + 1.7321 * point.y) / 3.0 - 1.1547 * point.y);
}

vec2 point(vec2 hex){
    return vec2(3.0 * (hex.x + hex.y), 1.7321 * (hex.x - hex.y)) / 2.0;
}

float inhex(vec2 p, vec2 c, float s){
    return step(max(abs(c.y - p.y), dot(abs(p - c), normalize(vec2(1.7321, 1.0)))), s / 2.0);
}

vec3 Image(vec2 uv)
{
    vec3 col = vec3(1.); // 0.5 + 0.5*cos(mat_time / 5.0 +uv.xyx+vec3(0,2,4));   
    vec2 nuv = 15.0 * uv * mat_scale;   
    nuv += vec2(mat_time / 350.0);
    
    float s = billowedNoise(vec3(uv,mat_time))*0.9 + 1. ; // 1.69 + 0.006 * sin(mat_time / 5.0);

    vec2 nc = round(hex(nuv));    
    col *= 1.0 - inhex(nuv, point(nc), s) - inhex(nuv, point(nc + vec2(1 ,0)), s) - inhex(nuv, point(nc + vec2(-1, 0)), s) - inhex(nuv, point(nc + vec2(0, 1)), s) - inhex(nuv, point(nc + vec2(0, -1)), s);
    
    return col;
}

const int aa = 2;

vec4 materialColorForPixel( vec2 texCoord )
{
   vec2 uv = texCoord.xy;

   vec3 total = vec3(.0);

	// MSAA
    for(int i = 0; i <aa;i++)
    for(int j = 0; j <aa;j++)
    {
    	/*offset the uv for MSAA*/
    	vec2 uv =uv +  (vec2(float(i),float(j))/float(aa)-.5)/vec2(1024.) - vec2(.5);
    	
        total+= Image(uv);
    }
    total /= float(aa*aa);
	return vec4(total,1.);
}