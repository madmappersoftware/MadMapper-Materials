/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Cabling noodles",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 200, "DEFAULT": 100 },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 2. }, 
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.5, "MAX": 4.0, "DEFAULT": 2. },
		{ "LABEL": "Noise", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0., 0. ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed",}},
    ],
}*/

#include "MadNoise.glsl"

const float PI = 3.14159265359;
const float PRECISION  = 0.001;
const float MAX_DISTANCE = 50.;
const vec2 e = vec2(.00035,-.00035);

///// Utility

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

////// Signed Distance Fields
//////  https://iquilezles.org/www/articles/distfunctions/distfunctions.htm

float smin( float a, float b, float k )
{
    float h = max( k-abs(a-b), 0.0 )/k;
    return min( a, b ) - h*h*k*(1.0/4.0);
}

float sdSphere( vec3 p, float r ){
 return length(p)-r;
}

float sdCyl( vec3 p, float r ){
 return length(p.xy)-r;
}

float sdBox( vec3 p, vec3 b )
{
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

///// Scene Creation
vec2 map( vec3 p )
{	
	if(mat_noise > 0.001)p.xyz += dfBm(p*0.05*mat_noise).yzw;
	p.x += sin(p.z*0.1+mat_time);
	p.y += cos(p.z*0.1+0.34+mat_time);
	vec2 d = vec2(sdCyl(p + vec3(0.,1. + cos(mat_time),0),0.02),1.);

	for (int i = 0; i < 30; i++)
{
		
		p.xy *= rot(p.z*0.001 + mat_time *0.01 + i*0.001);
		float k = sdCyl(p + vec3(i*0.03,1. + cos(mat_time + i) +i*0.2,sin(i*0.6)),0.03 + cos(mat_time + i*0.01 + p.z*2.)*0.02 );
		if(k < d.x) d.x = k;
}
	d.x *= 0.6;
//	if(d2.x < d1.x) d1 = d2;
	return d;
}

////// Raymarcher
vec2 intersect( in vec3 ro, in vec3 rd )
{
	float h= PRECISION * 2.0;
    float t = 0.0;
	float maxd = MAX_DISTANCE;
    float sid = -1.0;
    for( int i=0; i<mat_quality; i++ )
    {
        if( abs(h)<PRECISION||t>maxd ) break;
        t += h;
	    vec2 res = map( ro+rd*t );
        h = res.x;
	    sid = res.y;
    }

    if( t>maxd ) sid=-1.0;
    return vec2( t, sid );
}



vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;

	uv = uv*2.-1.;   // center uvs

  	vec3 ro=vec3(0,-mat_offset.y*2.,4.0);
 	vec3 ta=vec3(0);
  
  	ro.xz *= rot(mat_offset.x*PI);

 	float adv = 0.0;
 	ro.z += adv;
 	ro.z += adv;

  	vec3 cz = normalize(ta-ro);
  	vec3 cx = normalize(cross(cz, vec3(0,-1,0)));
  	vec3 cy = normalize(cross(cz, cx));
  	float fov = mat_fov;
  	vec3 rd=normalize(cx*uv.x + cy*uv.y + fov*cz);
	
	// raymarch to get distance to scene (surf.x) and material ID (surf.y)
    vec2 surf = intersect(ro,rd);

	// surface properties
	vec3 col = vec3(0.,0,0);

	if( surf.y > 0)
	{
	    // geometry
	    vec3 pos = ro + surf.x*rd;
			
		if(surf.y == 1) col = vec3(1.);//sphereColor(pos,nor).xyz; 
	}

	return vec4(col,1.0);
}