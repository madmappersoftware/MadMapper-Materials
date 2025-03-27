/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Domain Warping",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Z", "NAME": "Z", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 1.0 },  
     
	 
	 
	 ],
}*/
//Domain Warping 
//
// Modded version of 'Alien Spawning Pool' by @christinacoffin 
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
//
// hacked up version of https://www.shadertoy.com/view/4s23zz#
// See http://www.iquilezles.org/www/articles/warp/warp.htm for details
// 
//




#include "MadCommon.glsl"
#include "MadNoise.glsl"
	

const mat2 mtx = mat2( 0.0280,  0.60, -0.60,  0.480 );

highp float rand(vec2 co)
{
    highp float a = 12.9898;
    highp float b = 78.233;
    highp float c = 43758.5453;
    highp float dt= dot(co.xy ,vec2(a,b));
    highp float sn= mod(dt,3.14);
    return fract(sin(sn) * c);
}

float fbm4( vec2 p )
{
	
    float f = 0.000001;
 float noise_2 = fBm(p) *2	;
   f += 0.006000*(-1.0+2.0*noise_2);// 
	p = mtx*p*23.02;
    f += 0.9500*(-1.0+2.0*noise_2);//
	p = mtx*p*23.03;
    f += 0.50*(8*noise_2);// 
	p = mtx*p*23.01;
	f += 0.0225*(-5.0+2.0)*noise_2;

    return f/01.375;
}

float fbm6( vec2 p )
{
    float f = 0.0;

float noise_2 = billowedNoise(p)*0.0013;

	
    f += 0.0500000*noise_2;
	p = mtx*p*2.02;
    f += 0.250000*noise_2; 
	p = mtx*p*2.03;
    f += 0.073125000*noise_2;
	p = mtx*p*2.01;
    f += 0.062500*noise_2; 
	p = mtx*p*2.04;
    f += .0231250*noise_2;
	p = mtx*p*2.01;
    f += 0.015625*noise_2;

    return f/0.996875;
}

float func( vec2 q, out vec2 o, out vec2 n )
{
    float ql = length( q );
    q.x += 0.015*sin(0.11*TIME+ql*14.0);
    q.y += 0.035*cos(0.13*TIME+ql*14.0);
    q *= Z + 0.2*cos(0.05*TIME);

    q = (q+1.0)*0.5;

    o.x = 0.5 + 0.5*fbm4( vec2(2.0*q*vec2(1.0,1.0)          )  );
    o.y = 0.5 + 0.5*fbm4( vec2(2.0*q*vec2(1.0,1.0)+vec2(5.2))  );

    float ol = length( o*o );
    o.x += 0.003*sin(0.911*TIME*ol)/ol;
    o.y += 0.002*sin(0.913*TIME*ol)/ol;


    n.x = fbm6( vec2(4.0*o*vec2(1.0,1.0)+vec2(9.2))  );
    n.y = fbm6( vec2(4.0*o*vec2(1.0,1.0)+vec2(5.7))  );

    vec2 p = 11.0*q + 3.0*n;

    float f = 0.5 + 0.85*fbm4( p );

    f = mix( f, f*f*f*-3.5, -f*abs(n.x) );

    float g = 0.5+0.5*sin(1.0*p.x)*sin(1.0*p.y);
    f *= 1.0-0.5*pow( g, 7.0 );

    return f;
}

float funcs( in vec2 q )
{
    vec2 t1, t2;
    return func(q,t1,t2);
}

	
	

vec4 materialColorForPixel( vec2 texCoord )
{

	
	float noise_value = rand(texCoord.xy*TIME);
		vec2 p = texCoord.xy / 1;
	vec2 q = (-1 + 2.0*texCoord.xy) /1;
    vec2 o, n;
    float f = func(q, o, n);
    vec3 col = vec3(-0.91620);  
	col = mix( vec3(0.5,0.1,0.4), col, f ); 
    col = mix( vec3(0.2,0.1,0.4), col * vec3(0.13,0.05,0.05), f );
    col = mix( col, vec3(0.19,0.9,0.9), dot(n,n)*n.x*1.357 );
    col = mix( col, vec3(0.5,0.2,0.2), 0.5*o.y*o.y );
	col += 0.05*mix( col, vec3(0.9,0.9,0.9), dot(n,n) );
    col = mix( col, vec3(0.0,0.2,0.4), 0.5*smoothstep(1.02,1.3,abs(n.y)+abs(n.x*n.x)) );
    col *= f*(9.92+(1.1*sin(TIME)));//animate glowy translucent underbits
   
	col = mix( col, vec3(-1.0,0.2,0.4), 0.5*smoothstep(2.02,1.3,abs(n.y)+abs(n.x*n.x)) );   
	col = mix( col, vec3(0.40,0.92,0.4), 0.5*smoothstep(0.602,1.93,abs(n.y)+abs(n.x*n.x)) );    
    
    vec2 ex = -1.* vec2( 2.0 / 1, 0.0 );
    vec2 ey = -1.*vec2( 0.0, 2.0 / 1 );
	vec3 nor = normalize( vec3( funcs(q+ex) - f, ex.x, funcs(q+ey) - f ) );
    vec3 lig = normalize( vec3( 0.19, -0.2, -0.4 ) );
    float dif = clamp( 0.03+0.7*dot( nor, lig ), 0.0, 1.0 );

    vec3 bdrf;
    bdrf  = vec3(0.85,0.90,0.95)*(nor.y*0.5+0.5);
    bdrf += vec3(0.15,0.10,0.05)*dif;
    col *= bdrf/f;
    col = vec3(0.8)-col;
    col = col*col;
    col *= vec3(0.8,1.15,1.2);    
	col *= 0.45 + 0.5 * sqrt(16.0*p.x*p.y*p.y*(2.0-p.x)*(1.0-p.y)) * vec3(1.,0.3,0.);

    col = clamp(col,0.0,1.0);
	
	return vec4(col,1);
}
