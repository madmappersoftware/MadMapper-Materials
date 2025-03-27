/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Bonobo - Linked / 2019",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 200, "DEFAULT": 100 },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0. }, 
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.5, "MAX": 4.0, "DEFAULT": 2.8 },
		{ "LABEL": "Orbit", "NAME": "mat_orbit", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 0. },
		{ "LABEL": "Noise Power", "NAME": "mat_noisePower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Extra Noise", "NAME": "mat_extra", "TYPE": "bool",  "DEFAULT": 0 ,"FLAGS":"button"},
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.4, -0.34 ] },
		{ "LABEL": "Light Pos", "NAME": "mat_light", "TYPE": "point2D", "MAX": [ 10.0, 10.0 ], "MIN": [ -10.0, -10.0 ], "DEFAULT": [ -10., 10. ] },
		{ "LABEL": "Rock_1", "NAME": "mat_r1", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0., 0. ] },
		{ "LABEL": "Rock_2", "NAME": "mat_r2", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0., 0. ] },

		{ "LABEL": "Light/Softness", "NAME": "mat_shad_soft", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.9 },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed",}},
    ],
   	 "IMPORTED": [
    		{"NAME": "texture_rock", "PATH": "rocky.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
		{"NAME": "texture_crater", "PATH": "moon.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

const float PI = 3.14159265359;
const float PRECISION  = 0.0001;
const float MAX_DISTANCE = 40.;

vec3 blue_1 = vec3(18,91,123)/255.;
vec3 blue_2 = vec3(92,159,178)/255.;
vec3 rock_1 = vec3(137,61,71)/255.;

#include "MadNoise.glsl"

///// Utility
float uniformNoise(vec2 uv){
	return fract(sin(dot(uv,vec2(12.2365454,89.15648))) * 458796.12157 );
}

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

mat4 rotation3d(vec3 axis, float angle) {
  axis = normalize(axis);
  float s = sin(angle);
  float c = cos(angle);
  float oc = 1.0 - c;

  return mat4(
	oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
    oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
    oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
	0.0,                                0.0,                                0.0,                                1.0
	);
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

///// Scene Creation
vec2 map( vec3 p )
{	
	p = (rotation3d(vec3(0.,1.,1.),mat_orbit) * vec4(p,1.)).xyz;

	vec3 p1 = p;
	p1 = (rotation3d(vec3(1.,0.,0.),-0.35) * vec4(p1,1.)).xyz;
	
	float limit = 1. - clamp(length(p)*0.05,0.,1.);

	float n = fBm(p*0.3 + mat_time) *mat_noisePower * limit; // large rock
	float n_2 = ridgedMF(p ) * limit;
	float n_3 = ridgedMF(p1 *vec3(1.,4.,1.) + mat_time) *mat_noisePower * limit;

	float d1 = sdBox(p1+vec3(0.,mat_r1),vec3(1.)) + n*0.4 +n_2*0.2 + n_3 *0.2;
	float d2 = sdBox(p1+vec3(2.,-0.75,-2.5) + vec3(0.,mat_r2),vec3(1.5,1.5,2.5)) - n*.7 + n_2*0.4 ;

	float d = smin(d1,d2,0.3);

	vec3 p2 = p;
	p2 = (rotation3d(vec3(1.,0.1,0.),-0.32) * vec4(p1,1.)).xyz;
	float d3 = sdBox(p2+vec3(0.4,0.3,-0.85),vec3(0.65)) + n*0.4 +n_2*0.2 ;

	d = smin(d,d3,0.01);

	float moon = sdSphere(p - vec3(-10.,-4.,4),0.3);

	if(moon < d) return vec2(moon,2.);

	return vec2(d,1.);
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

vec3 calcNormal( in vec3 pos )
{
    vec3  eps = vec3(PRECISION,0.0,0.0);
    vec3 nor;
    nor.x = map(pos+eps.xyy).x - map(pos-eps.xyy).x;
    nor.y = map(pos+eps.yxy).x - map(pos-eps.yxy).x;
    nor.z = map(pos+eps.yyx).x - map(pos-eps.yyx).x;

    return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;

    for( int i=0; i<5; i++ )
    {
        float hr = 0.01 + 0.12*float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        occ += -(dd-hr)*sca;
        sca *= 0.95;
    }

    return clamp( 1.0 - 3.0*occ, 0.0, 1.0 );    
}


vec3 rockColor(in vec3 p , in vec3 nor){
	vec3 tex = texture(texture_rock,p.yz*0.75).rgb;
	tex = pow(tex,vec3(2.2));

	return vec3(  abs((nor)*0.1).x + tex*rock_1 +rock_1 +(tex-0.5)*0.2 );
}

vec3 moonColor(in vec3 p , in vec3 nor){
	float tex = texture(texture_crater,p.yz*2. +vec2(0.,0.5)).r;

	return vec3( 1.5 )*mix(tex,1.,0.5);
}


float softshadow( in vec3 ro, in vec3 rd, float mint, float maxt, float k )
{
    float res = 1.0;

    for( float t=mint; t<maxt; )
    {
        float h = map(ro + rd*t).x;
        if( h<0.001 )
            return 0.0;
        res = min( res, k*h/t );
        t += h;
    }

    return res;
}

// Cheap curvature: https://www.shadertoy.com/view/Xts3WM
float curve(in vec3 p){
    const float eps = 0.0225, amp = 7.5, ampInit = 0.525;

    vec2 e = vec2(-1., 1.)*eps; //0.05->3.5 - 0.04->5.5 - 0.03->10.->0.1->1.
    
    float t1 = map(p + e.yxx).x, t2 = map(p + e.xxy).x;
    float t3 = map(p + e.xyx).x, t4 = map(p + e.yyy).x;
    
    return clamp((t1 + t2 + t3 + t4 - 4.*map(p).x)*amp + ampInit, 0., 1.);
}

vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;

	uv = uv*2.-1.;  

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

    // geometry
    vec3 pos = ro + surf.x*rd;
    vec3 nor = calcNormal(pos);
	
	// surface properties
	vec3 col = mix(blue_1,blue_2,texCoord.y*2.+uniformNoise(uv)*0.02);

	// shadow
	vec3 lp = vec3(mat_light,1);
	vec3 ld = (pos - lp);
	float sha =  softshadow( pos, normalize(ld), mat_shad_soft*0.1, length(ld), 1. );	
	 
	if(surf.y == 2) col = mix(col,moonColor(pos,nor).xyz,sha); 

	sha = mix(1,sha,0.85); 

	if(surf.y == 1){ 
		col = rockColor(pos,nor).xyz *sha;

		// extra fresnel , unrealistic but subtle adds
    		float fre = clamp(dot(nor, rd) + 1., .0, 1.) * 0.2;
		col += vec3(fre);
	}

	// as in https://www.shadertoy.com/view/ldtGWj
    float crv = curve(pos); // Curve value, to darken the crevices.
    crv = smoothstep(0., 1., crv)*.5 + crv*.25 + .25; // Tweaking the curve value a bit.
	crv = mix(crv,1.,0.5);

	// Ambiant Occlusion
	float ao = calcAO(pos,nor);

	col *= vec3(ao*crv);

	return vec4(col,1.0);
}