/*{
    "CREDIT": "1024 architecture\nadapted from Inigo Quilez",
    "DESCRIPTION": "Infini-tunnel ",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.7 },
        { "LABEL": "Point Size", "NAME": "point_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
		{ "LABEL": "Ring Points", "NAME": "RING_POINTS", "TYPE": "int", "MIN": 1, "MAX": 1024, "DEFAULT": 128 },
		{ "LABEL": "Tunnel Layers", "NAME": "TUNNEL_LAYERS", "TYPE": "int", "MIN": 1, "MAX": 128, "DEFAULT": 96 },
      	{ "LABEL": "Color/Color A", "NAME": "color_a", "TYPE": "color", "DEFAULT": [ 0.2, 0.2, 0.2, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "color_b", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
	  
	  	{ "LABEL": "Cam/Orbit", "NAME": "uCam", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ],
		"DEFAULT": [ 0.0, 0.0 ] },
		
	  ],
	 "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", 
		"PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
	//Constants
#define TAU 6.2831853071795865

	
uniform vec2 iResolution = vec2(1024);	

//Square of x
float sq(float x)
{
	return x*x;   
}

//Angular repeat
vec2 AngRep(vec2 uv, float angle)
{
    vec2 polar = vec2(atan(uv.y, uv.x), length(uv));
    polar.x = mod(polar.x + angle / 2.0, angle) - angle / 2.0; 

    return polar.y * vec2(cos(polar.x), sin(polar.x));
}

//Signed distance to circle
float sdCircle(vec2 uv, float r)
{
    return length(uv) - r;
}

//Mix a shape defined by a distance field 'sd' with a 'target' color using the 'fill' color.
vec3 MixShape(float sd, vec3 fill, vec3 target)
{
    float blend = smoothstep(0.0,1.0/iResolution.y, sd);
    return mix(fill, target, blend);
}

//Tunnel/Camera path
vec2 TunnelPath(float x)
{
    vec2 offs = vec2(0, 0);
    
 //   offs.x = 0.2 * sin(TAU * x * 0.5) + 0.4 * sin(TAU * x * 0.2 + 0.3);
 //  offs.y = 0.3 * cos(TAU * x * 0.3) + 0.2 * cos(TAU * x * 0.1);

   offs *= smoothstep(1.0,4.0, x);
    
    return offs;
}


vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec2 res = vec2(1);
	vec2 uv = texCoord;  
    uv -= res/2.0;
    
    vec3 color = vec3(0);
    
    float repAngle = TAU / float(RING_POINTS);
    float pointSize = point_size*point_size*0.1;
    
    float camZ = animation_time;
    vec2 camOffs = uCam*vec2(0.5,-0.5);//TunnelPath(camZ);
    
    for(int i = 1;i <= TUNNEL_LAYERS;i++)
    {
        float pz = 1.0 - (float(i) / float(TUNNEL_LAYERS)) ;
        
        //Scroll the points towards the screen
        pz -= mod(camZ, 4.0 / float(TUNNEL_LAYERS));
        
        //Layer x/y offset
        vec2 offs = TunnelPath(camZ + pz) - camOffs;
        
        //Radius of the current ring
        float ringRad = 0.15 * (1.0 / sq(pz * 0.8 + 0.4));
        
        //Only draw points when uv is close to the ring.
        if(abs(length(uv + offs) - ringRad) < pointSize * 1.5) 
        {
            //Angular repeated uv coords
            vec2 aruv = AngRep(uv + offs, repAngle);

            //Distance to the nearest point
            float pdist = sdCircle(aruv - vec2(ringRad, 0), pointSize);

            //Stripes
            vec3 ptColor = (mod(float(i / 2), 2.0) == 0.0) ? color_a.xyz : color_b.xyz;
            
            //Distance fade
            float shade = (1.0-pz);

            color = MixShape(pdist, ptColor * shade, color);
        }
    }
    
	return vec4(color, 1.0);
	
}



