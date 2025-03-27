/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Alex Christoffer Rasmussen",
    "DESCRIPTION": "Leukbaars https://www.shadertoy.com/view/lldGzr",
    "TAGS": "Pixel",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "MIN": 1.0, "MAX": 64.0, "DEFAULT": 32.0 }, 
        { "LABEL": "Count", "NAME": "mat_count", "TYPE": "int", "MIN": 0.0, "MAX": 128.0, "DEFAULT": 32.0 },
		{ "LABEL": "Gravity", "NAME": "mat_gravity", "TYPE": "float", "MAX": 2.0, "MIN": -2.0, "DEFAULT": -0.72 },
      ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	
	
float seed = 0.32; //-----------------------------------starting seed

float gravity = -0.72; //---------------------------------set gravity

vec4 materialColorForPixel( vec2 texCoord )
{
	float newTIME = TIME*mat_speed;
	float res = mat_size;
	float particles = mat_count;
	float gravity = mat_gravity;
	
	vec2 uv = texCoord-vec2(0.5);
   	float clr = 0.0;  
    float timecycle = (newTIME)-floor(newTIME); 
    seed = (seed+floor(newTIME));
    
    //testing
    float invres=1.0/res;
    float invparticles = 1.0/particles;

    
    for( float i=0.0; i<particles; i+=1.0 )
    {
		seed+=i+tan(seed);
        vec2 tPos = (vec2(cos(seed),sin(seed)))*i*invparticles;
        
        vec2 pPos = vec2(0.0,0.0);
        pPos.x=((tPos.x) * timecycle);
		pPos.y = -gravity*(timecycle*timecycle)+tPos.y*timecycle+pPos.y;
        
        //pPos = floor(pPos*res)*invres; //------comment this out for smooth version 

    	vec2 p1 = pPos;
    	vec4 r1 = vec4(vec2(step(p1,uv)),1.0-vec2(step(p1+invres,uv)));
    	float px1 = r1.x*r1.y*r1.z*r1.w;
        float px2 = smoothstep(0.0,200.0,(1.0/distance(uv, pPos+.015)));//added glow
        px1=max(px1,px2);
        
	    clr += px1*(sin(newTIME*20.0+i)+1.0);
    }
    
	vec4 fragColor = vec4(clr*(1.0-timecycle))*vec4(4, 0.5, 0.1,1.0);
	return fragColor;
}
