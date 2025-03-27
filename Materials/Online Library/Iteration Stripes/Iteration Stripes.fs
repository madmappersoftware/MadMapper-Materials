/*
{

    "CREDIT": "Derived by Tim Brassey from Shadertoy's Iteration Stripes by iq",
    "DESCRIPTION": "Iteration Stripes",
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
     "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": "0c7bf5fe9462d5bffbd11126e82908e39be3ce56220d900f633d58fb432e56f5.png"
        }
    },
    "INPUTS": [

		{"LABEL": "Speed", "NAME": "mat_one", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Shadow", "NAME": "mat_two", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{"LABEL": "Zoom", "NAME": "mat_three", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

    ]
}

*/


#define AA 2

float hash( in float n )
{
    return fract(sin(n)*43758.5453);
}
float noise( in vec2 p )
{
    vec2 i = floor(p);
    vec2 f = fract(p);
    f = f*f*(3.0-2.0*f);
    float n = i.x + i.y*57.0;
    return mix(mix( hash(n+ 0.0), hash(n+ 1.0),f.x),
               mix( hash(n+57.0), hash(n+58.0),f.x),f.y);
}

vec2 map( in vec2 p, in float time )
{
    for( int i=0; i<4; i++ )
    {
    	float a = (noise(p*1.5)*6.2831 + time);
		p += 0.1*vec2( cos(a), sin(a) );
    }
    return p;
}

float height( in vec2 p, in vec2 q )
{
    float h = dot(p-q,p-q);
    h += 0.005*IMG_NORM_PIXEL(iChannel0,mod(9.75*(p+q),1.0)).x;
    return h;
}

vec4 materialColorForPixel( vec2 texCoord ) {



    float time = mat_one*TIME;
    
    vec3 tot = vec3(0.0);
	#if AA>1
    for( int m=0; m<AA; m++ )
    for( int n=0; n<AA; n++ )
    {
        vec2 o = vec2(float(m),float(n)) / float(AA) - 0.5;
        vec2 p = (mat_three*(texCoord.xy));
		#else    
        vec2 p = (mat_three*texCoord.xy);
		#endif
        // deformation
        vec2 q = map(p,time*mat_one);
        // color
        float w = 10.0*q.x;
        float u = floor(w);
        float f = fract(w);
        vec3  col = vec3(0.7,0.55,0.5) + 0.3*cos(3.0*u+vec3(0.0,1.5,2.0));
        
        // filtered drop-shadow
        float sha = smoothstep(0.0,mat_two,f)-smoothstep(1.0-fwidth(w),1.0,f);
        
        // normal
 		vec2  eps = vec2(2.0,0.0);
		float l2c = height(q,p);
        float l2x = height(map(p+eps.xy,time),p) - l2c;
        float l2y = height(map(p+eps.yx,time),p) - l2c;
        vec3  nor = normalize( vec3( l2x, eps.x, l2y ) );
            
        // lighting
        col *= 0.3+0.7*sha;
        col *= 0.8+0.2*vec3(1.0,0.9,0.3)*dot(nor,vec3(0.7,0.3,0.7));
        col += 0.3*pow(nor.y,20.0)*sha;
        col *= 7.5*l2c;
        tot += col;
	#if AA>1
    }
    tot /= float(AA*AA);
	#endif
	return vec4(tot, 1.0 );
}


