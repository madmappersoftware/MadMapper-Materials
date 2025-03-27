/*{
    "CREDIT": "Do something cool",
    "DESCRIPTION": "Raymarching template",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 100, "DEFAULT": 100 },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.5, "MAX": 4.0, "DEFAULT": 1. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0., 1. ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed",}},
    ],
}*/
flat in vec3 vro; // ray origin
in vec3 vrd; // ray direction
in vec3 noisePos1;
in vec3 noisePos2;
in vec3 noisePos3;

const float PI = 3.14159265359;
const float PRECISION  = 0.001;
const float MAX_DISTANCE = 20.;
const vec2 e = vec2(.00035,-.00035)*10.;

int mat_id = -1;

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
float map( vec3 p )
{	
	float d = sdRoundBox(p,vec3(1.),0.01);


//	d = max(d,-sdBox(p + (noisePos1*2.-1.), vec3(0.8)));
//	d = max(d,-sdBox(p + (noisePos2*2.-1.), vec3(0.8)));
//	d = max(d,-sdBox(p + (noisePos3*2.-1.), vec3(0.8)));

	d = min(d,sdBox(p + (noisePos1*2.-1.), vec3(0.8)));
	d = min(d,sdBox(p + (noisePos2*2.-1.), vec3(0.8)));
	d = min(d,sdBox(p + (noisePos3*2.-1.), vec3(0.8)));
	mat_id = 1;

	return d*0.7;
}

////// Raymarcher
float intersect( in vec3 ro, in vec3 rd )
{
	float h= PRECISION * 2.0;
    float t = 0.0;
	float maxd = MAX_DISTANCE;
    for( int i=0; i<mat_quality; i++ )
    {
        if( abs(h)<PRECISION||t>maxd ) break;
        t += h;
	    h = map( ro+rd*t );
    }

    if( t>maxd ) mat_id = -1;
    return t;
}

vec3 calcNormal(in vec3 pos)
{
 return normalize(	e.xyy*map(pos+e.xyy)+
					e.yyx*map(pos+e.yyx)+
					e.yxy*map(pos+e.yxy)+
					e.xxx*map(pos+e.xxx));
}

float calcAO( in vec3 pos, in vec3 nor , in float d)
{
  return clamp(map(pos+nor*d)/d,0.,1.) ;  
}
float calcSoftshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax, int technique )
{
	float res = 1.0;
    float t = mint;
    float ph = 1e10; // big, such that y = 0 on the first iteration

	float test = 0.1 + 0.4;
    for( int i=0; i<32; i++ )
    {
		float h = map( ro + rd*t );

        // traditional technique
        if( technique==0 )
        {
        	res = min( res, 10.0*h/t );
        }
        // improved technique
        else
        {
            // use this if you are getting artifact on the first iteration, or unroll the
            // first iteration out of the loop
            //float y = (i==0) ? 0.0 : h*h/(2.0*ph); 

            float y = h*h/(2.0*ph);
            float d = sqrt(h*h-y*y);
            res = min( res, 10.0*d/max(0.0,t-y) );
            ph = h;
        }
        
        t += h;
        
        if( res<0.0001 || t>tmax ) break;
        
    }
    res = clamp( res, 0.0, 1.0 );
    return res*res*(3.0-2.0*res);
}

vec3 boxColor(in vec3 p , in vec3 nor){

	return vec3(  nor*0.5+0.5  );
}

vec4 materialColorForPixel( vec2 texCoord )
{	

	float laser = 0.;
	vec2 uv = texCoord;
	uv = uv*2.-1.;   // center uvs

  	vec3 ro= vro;
  	vec3 rd= normalize(vrd);
	
	float stop = 0.34;
	// distance to scene
    float dist = intersect(ro,rd);

	// surface properties
	vec3 col = vec3(0.,0,0);

	if( mat_id > 0)
	{
	    // Geometry
	    vec3 pos = ro + dist * rd;
	    vec3 nor = calcNormal(pos);

		vec3 L = normalize(vec3(1.,-1.,2.));
		float diffuse = max(0.,dot(nor,L));
		
		// Albedo	 
		col = vec3(diffuse) + (nor*0.5+0.5)*0.5;  
	
		// Shadow
		vec3 lightdir = normalize(vec3(-1.,1,-1));
		float shadow = calcSoftshadow( pos, -lightdir, 0.1, 10., 1 );
		col *= mix(shadow,1.,0.5);

		// Ambiant Occlusion
		float ao = calcAO(pos,nor,0.1);
		col *= mix(1.,ao,0.5);

		laser = abs(length(fwidth(nor)*2.));
		laser += abs(length(fwidth(pos)*2.));

		// fresnel
		float fr = pow(1.+dot(nor,rd),4.);
		col += vec3(fr)*0.5;

	}
//	laser += abs(length(fwidth(col)));

	laser = step(0.1,laser);
	return vec4(col.r, laser,0,1.0);
}