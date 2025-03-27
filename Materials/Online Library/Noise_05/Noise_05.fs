/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz cooked it up from the net",
    "DESCRIPTION": "Noise series: 3x3 neighbor cell voronoi",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{ "LABEL": "Radius", "NAME": "R", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": -0.3 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.7 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	
	
#define N 3   // tested neighborhood. Make it odd > 3 if R is high 

// rand 1D, rand 2D, signed rand
#define rnd(p)	fract(sin( dot(p, vec2(12.9898, 78.233) )       ) * 31647.5453 )
#define rnd2(p) fract(sin( (p) * mat2(127.1,311.7, 269.5,183.3) ) * 43758.5453 )
#define srnd(p)  ( 2.*rnd(p) -1. )
#define srnd2(p) ( 2.*rnd(p) -1. )	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	
	vec2 U = uv*2.;	
	
    float m=1e9,m2, c=1e2, v,w;
    
    for (int k=0; k<N*N; k++) {          // --- visit 3x3 neighbor tiles 
        vec2 iU = floor(U)+.5,           // tile center 
              g = iU + vec2(k%N,k/N)-1., // neighbor cell
              p =  g+ srnd2(g)*R -U      // vector to jittered cell node
                 +.1*sin(mat_animation_time+vec2(1.6,0)+3.14*srnd(g)),   
              q = p * mat2(1,-1,1,1)*.707;  
       		  c = min(c,length(p));             

        p = abs(p); v = max(p.x,p.y);    
        if (v < m) m2 = m, m = v;        
        else if (v < m2) m2 = v;        
    }
	
	v = m2-m;
	
	/// create wireframe outline
	v = smoothstep(0.,5.,100.*v);
	
	// make a color out of it
	vec3 color = vec3(v);
		
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(!mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
