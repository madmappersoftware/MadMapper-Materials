/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Raymarched surface",
    "TAGS": "raymarch",
    "VSN": "1.1",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 100, "DEFAULT": 10 },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
		
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.5, "MAX": 20.0, "DEFAULT": 5. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.2, -0.2 ] },

	{ "LABEL": "Render/Fresnel", "NAME": "mat_fresnel", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },
	{ "LABEL": "Render/AO", "NAME": "mat_ao", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. },
	{ "LABEL": "Render/Noise", "NAME": "mat_rnoise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. }, 

	{ "LABEL": "AA/Enable AA", "NAME": "mat_AA", "TYPE": "bool",  "DEFAULT": false, "FLAGS":"button" }, 
	{ "LABEL": "AA/Show Samples", "NAME": "mat_showAA", "TYPE": "bool",  "DEFAULT": false,"FLAGS":"button" }, 

        { "Label": "Color/Color A", "NAME": "mat_cA", "TYPE": "color", "DEFAULT": [ 0.0, 0.04, 0.07, 1.0 ], "FLAGS":"no_alpha" },  
        { "Label": "Color/Color B", "NAME": "mat_cB", "TYPE": "color", "DEFAULT": [ 0.66, 0.74, 0.77, 1.0 ], "FLAGS":"no_alpha" },
		{ "Label": "Color/Color Fresnel", "NAME": "mat_cC", "TYPE": "color", "DEFAULT": [ 1.0, 0.39, 0.0, 1.0 ], "FLAGS":"no_alpha" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed",}},
    ],
}*/

#include "MadNoise.glsl"

const float PI = 3.14159265359;
const float PRECISION  = 0.001;
const float MAX_DISTANCE = 200.;
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

float sdBox( vec3 p, vec3 b )
{
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}
float sdRoundBox( vec3 p, vec3 b, float r )
{
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
}

///// Scene Creation
vec2 map( vec3 p )
{	

	vec3 c = vec3(1.,1,0);
	vec3 q = mod(p+0.5*c,c)-0.5*c;

	vec3 cellA = floor(p-vec3(0.5));
	q.xz *= rot(mat_time +cellA.x);
	q.yz *= rot(mat_time+ 0.25 + cellA.y );

	float s = billowedNoise(vec3(p.xy,mat_time))*mat_rnoise;
	vec2 d1 = vec2(sdRoundBox(q+vec3(0.,0.,0),vec3(0.35-s*0.25),0.02),2.);
	p -= vec3(0.5,0.5,0);
	vec3 cellB = floor(p-vec3(0.5));

	vec3 r = mod(p-0.5*c,c)-0.5*c;
	r.yz *= rot(mat_time+ 0.5 +cellB.x);
	r.xz *= rot(mat_time+ 0.75 +cellB.y);
	vec2 d2 =  vec2(sdRoundBox(r+vec3(0.,0.,0),vec3(0.35-s*0.25),0.02),1.);
	
	if(d2.x < d1.x) d1 = d2;
	return d1;
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


vec3 calcNormal(in vec3 pos)
{
 return normalize(	e.xyy*map(pos+e.xyy).x+
					e.yyx*map(pos+e.yyx).x+
					e.yxy*map(pos+e.yxy).x+
					e.xxx*map(pos+e.xxx).x);
}

float calcAO( in vec3 pos, in vec3 nor , in float d)
{
  return clamp(map(pos+nor*d).x/d,0.,1.) ;  
}

vec3 sphereColor(in vec3 p , in vec3 nor){

	return vec3(  abs(nor)*0.5 + vec3(0.,0.4,0.) );
}

vec3 boxColor(in vec3 p , in vec3 nor){

	return vec3(  abs(nor)*0.5 + vec3(0.8,0.,0.) );
}

vec3 render(in vec2 uv)
{

	 	vec3 ro=vec3(0,-mat_offset.y*10.,4.0);
 	vec3 ta=vec3(0);
  
  	ro.xz *= rot(mat_offset.x*PI*0.45);

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
	    vec3 nor = calcNormal(pos);
	
		// fresnel
		float fr = pow(1.+dot(nor,rd),4.);

		vec3 lp = vec3(5,5,5);
		vec3 ld = normalize(lp);
		float sp = abs(dot(nor,ld));

		col = vec3(sp);
	
		// Ambiant Occlusion
		float ao = calcAO(pos,nor,0.05);
		float aol = calcAO(pos,nor,0.2);
		col *= vec3(mix(1.,ao,mat_ao));
		col *= vec3(mix(1.,aol,0.5*mat_ao));
		col += vec3(fr*mat_fresnel)*mat_cC.rgb;
	}

	vec3 final_color = mix(mat_cA.rgb,mat_cB.rgb,col);

	return final_color;

}

vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;

	uv = uv*2.-1.;   // center uvs

	vec3 final_color = render(uv);

	if ( fwidth(length(final_color)) > .05 && mat_AA)
	{
		for(int k =0; k<9; k++)
		{
			final_color += render(uv +(vec2(k%3-1,k/3-1)/3.)*0.002);
		}
		final_color /= 9.;
		if(mat_showAA)final_color.r ++;
	}

	return vec4(final_color,1.0);
}