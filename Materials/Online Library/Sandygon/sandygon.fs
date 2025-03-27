/*{
    "CREDIT": "nimitz shadertoy lllGRr\njessifin\nfranz / 1024 architecture",
    "DESCRIPTION": "Sandy-polygonal terrain using Signed Distance Fields",
    "TAGS": "SDF",
    "VSN": "1.0",
    "INPUTS": [ 
	    { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "Scale X", "NAME": "uScaleX", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.35 },
		{ "LABEL": "Scale Y", "NAME": "uScaleY", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.35 },
		{ "LABEL": "Height", "NAME": "uHeight", "TYPE": "float", "MIN": 0.0, "MAX": 2.8, "DEFAULT": 2.0 },	
		{ "LABEL": "Cam/Z", "NAME": "uCamZ", "TYPE": "float", "MIN": 0, "MAX": 80.0, "DEFAULT": 80.0 },
		{ "LABEL": "Cam/Lookat", "NAME": "uTarget", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ 0.0, 0.0 ],
		"DEFAULT": [ 0.7, 0.56 ] },
		{ "LABEL": "Cam/Light", "NAME": "uLight", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ],
		"DEFAULT": [ 0.1, 0.0 ] },
		
		{ "LABEL": "Color/Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 1.0, 0.95, 0.65, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "uColorB", "TYPE": "color", "DEFAULT": [ 1.0, 0.4, 0.1, 1.0 ] },
      ],
	"GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

	
//Triangulator by nimitz (twitter: @stormoid)

#define ITR 16
#define FAR 100.

mat3 rotmat(vec3 axis, float angle)
{
	axis = normalize(axis);
	float s = sin(angle);
	float c = cos(angle);
	float oc = 1.0 - c;
	
	return mat3(oc * axis.x * axis.x + c, oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s, 
	oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s, 
	oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c);
}

float heightmap(in vec2 p)
{
	float N = billowedNoise(p*vec2(uScaleX,uScaleY)*0.2)*uHeight;
//	float N = flowNoise(p*vec2(uScaleX,uScaleY)*0.2, 1.0)*uHeight;
	return N;	
}

//from jessifin (https://www.shadertoy.com/view/lslXDf)
vec3 bary(vec2 a, vec2 b, vec2 c, vec2 p) 
{
    vec2 v0 = b - a, v1 = c - a, v2 = p - a;
    float inv_denom = 1.0 / (v0.x * v1.y - v1.x * v0.y)+1e-9;
    float v = (v2.x * v1.y - v1.x * v2.y) * inv_denom;
    float w = (v0.x * v2.y - v2.x * v0.y) * inv_denom;
    float u = 1.0 - v - w;
    return abs(vec3(u,v,w));
}

/*
	Idea is quite simple, find which (triangular) side of a given tile we're in,
	then get 3 samples and compute height using barycentric coordinates.
*/
float map(vec3 p)
{
    vec3 q = fract(p)-0.5;
    vec3 iq = floor(p);
    vec2 p1 = vec2(iq.x-.5, iq.z+.5);
    vec2 p2 = vec2(iq.x+.5, iq.z-.5);
    
    float d1 = heightmap(p1);
    float d2 = heightmap(p2);
    
    float sw = sign(q.x+q.z); 
    vec2 px = vec2(iq.x+.5*sw, iq.z+.5*sw);
    float dx = heightmap(px);
    vec3 bar = bary(vec2(.5*sw,.5*sw),vec2(-.5,.5),vec2(.5,-.5), q.xz);
    return (bar.x*dx + bar.y*d1 + bar.z*d2 + p.y + 3.);
}

vec3 getRay(vec2 uv){
    uv = (uv * 2.0 - 1.0);
	vec3 proj = normalize(vec3(uv.x, uv.y, 1.0) + vec3(uv.x, uv.y, -1.0) * pow(length(uv), 2.0) * 0.05);	  
	vec2 mouse = uTarget;
	vec3 ray = rotmat(vec3(0.0, -1.0, 0.0), mouse.x * 2.0 - 1.0) * rotmat(vec3(1.0, 0.0, 0.0), 1.5 * (mouse.y * 2.0 - 1.0)) * proj;
    return ray;
}

float march(in vec3 ro, in vec3 rd)
{
	float precis = 0.01;
    float h=precis*2.0;
    float d = 0.;
    for( int i=0; i<ITR; i++ )
    {
        if( abs(h)<precis || d>FAR ) break;
        d += h;
	    float res = map(ro+rd*d);
        h = res;
    }
	return d;
}

vec3 normal(const in vec3 p)
{  
    vec2 e = vec2(-1., 1.)*0.01;
	return normalize(e.yxx*map(p + e.yxx) + e.xxy*map(p + e.xxy) + 
					 e.xyx*map(p + e.xyx) + e.yyy*map(p + e.yyy) );   
}
	
vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *vec2(1,-1); 
    vec3 ro = vec3(uTarget*vec2(1,uCamZ), animation_time*2);
	vec3 ray = getRay(uv);	
	float rz = march(ro,ray);
    vec3 col = vec3(0.);
    
    if ( rz < FAR )
    {
        vec3 pos = (ro+rz*ray) ;
        vec3 nor= normal(pos);
        vec3 ligt = normalize(vec3(uLight.x, 0.05,uLight.y));      
        float dif = clamp(dot( nor, ligt ), 0., 1.);
        float fre = pow(clamp(1.0+dot(nor,ray),0.0,1.0), 2.);

        col = vec3(dif)*uColorA.rgb + uColorA.rgb*0.2;
        col += fre*uColorB.rgb;
    }

	return vec4(col,1.0);
}
