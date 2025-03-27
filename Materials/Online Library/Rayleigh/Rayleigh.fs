/*{
    "CREDIT": "shadertoy 4lBcRm",
    "DESCRIPTION": "Rayleigh light scattering",
    "TAGS": "simulation",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Time_of_day", "NAME": "mat_time", "TYPE": "float", "MIN": 3., "MAX": 7., "DEFAULT": 4.5 }, 
		{ "LABEL": "Blueish", "NAME": "mat_fov", "TYPE": "float", "MIN": 1., "MAX": 90., "DEFAULT": 10. }, 
      ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	
		
vec3 lightPosition0 = vec3(1,0,0);

#define iterScatteringOut  8.
#define iterScatteringIn   8.

//#define eps			0.00001
#define pi				PI
//why would it need pi*4 jQuery32009827033941304277_1521495449851??
#define pit4 			12.566370614359172953850573533118

//atmospheric scattering parameters
#define PLANET_RADIUS     6.36
#define ATMOSPHERE_RADIUS 6.42
#define H0Rayleigh        .13333
#define H0Mie             .02
#define KRayleigh         vec3(.058,.135,.331)
#define KMie              .21
	
//lib.common
#define dd(a) dot(a,a)
#define u2(a) ((a)*2.-1.)
#define u5(a) ((a)*.5+.5)

//carthesian to sphere (and back)
vec3 c2s(vec3 v){float r=length(v)
;return vec3(r, atan(v.x,v.z),acos(v.y/r));}
vec3 s2c(vec3 v)
{return v.x*vec3(sin(v.z)*sin(v.y),cos(v.z),sin(v.z)*cos(v.y));}

//ray sphere intersection
vec2 rsiab(vec3 p, vec3 v){return vec2(dd(v),dot(p,v)*2.00001);}
//2.00001 ommits left shift optimitation issues.
//return closest intersection of ray and sphere
vec3 irs(vec3 p,vec3 v,float r){;vec2 a=rsiab(p,v);
;return p-v*(a.y+sqrt(a.y*a.y-4.*a.x*(dd(p)-r*r)))*.5/a.x;}
//return distance to ray-sphere interection(s).
float irsfd(vec3 p,vec3 v,float r){vec2 a=rsiab(p,v);
;return -.5*(a.y+sqrt(a.y*a.y-4.*a.x*dd(p)-r*r))/a.x;}
vec2 irsfd2(vec3 p,vec3 v,float r){vec2 a=rsiab(p,v);
;return (vec2(-1,1)*sqrt(a.y*a.y-4.*a.x*(dd(p)-r*r))-a.y)*.5/a.x;}

//scat() is an O(n*O(m*2)) complexity nested loop
//scatS() is an inner loop of scat()
//and it is iterated over twice
//,which seems VERY inefficient and optimizable
//as in, it interpolates over 2 loops
// , instead of interpolating within the loop
vec4 scatS(vec3 a,vec3 b,vec4 k,vec2 o)
{vec3 s=(b-a)/iterScatteringOut,u=a+s*.5;vec2 r=vec2(0)
;for(float i=.0;i<iterScatteringOut; ++i
){float h=(length(u)-PLANET_RADIUS)/(ATMOSPHERE_RADIUS-PLANET_RADIUS)
 ;h=max(.0,h);r+=exp(-h/o);u+=s;
}r*=length(s);return r.xxxy*k*pit4;}

//a,b,k,h0,lightDir
vec4 scat(vec3 a,vec3 b,vec4 k,vec2 o,vec3 d)
{;vec3 s=(b-a)/iterScatteringIn,u=a+s*.5;float stepLength=length(s);
;vec4 rr=vec4(0)
;for(float i=.0;i<iterScatteringIn;++i
){float h=(PLANET_RADIUS-length(u))/(ATMOSPHERE_RADIUS-PLANET_RADIUS)
 ;rr+=exp(-scatS(u,irs(u,-d,ATMOSPHERE_RADIUS),k,o)
          -scatS(u,a                          ,k,o)
         )*exp(min(.0,h)/o).xxxy;u+=s;
}return rr*stepLength*k;}

float RayleighPhase(float m){return (3./4.)*(1.+m*m);}

float MiePhase(float m){const float g=-.99,h=g*g
;return (3.*(1.-h)/(2.*(2.+h)))*(1.+m*m)/pow((1.+h-2.*g*m),3./2.);}

vec3 tldg(vec3 l){float t=-.16+mat_time
;return normalize(vec3(l.x*cos(t)+l.y*sin(t)
,l.x*(-sin(t))+l.y *cos(t),.0));}

vec4 scatter(vec2 u){u.y=1.-u.y
;vec3 s=vec3(1,(u.x*2.-1.)*pi,u.y*pi),d=s2c(s);s=vec3(.0,PLANET_RADIUS,.0)
;return 10.*scat(s,s+d*irsfd2(s,d,ATMOSPHERE_RADIUS).y
,vec4(KRayleigh,KMie),vec2(H0Rayleigh,H0Mie)
,tldg(normalize(lightPosition0.xyz)));}	
	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	vec3 sphericalDirection=vec3(1,(uv.x*2.-1.)*pi,uv.y*pi);
	vec3 d = s2c(sphericalDirection);
	vec3 l=normalize(lightPosition0.xyz);//sunDirection
	vec3 tld=tldg(l);
	vec3 s=vec3(.0,PLANET_RADIUS, .0);
	vec3 atmospherePos=s+d*irsfd2(s,d,ATMOSPHERE_RADIUS).y;
	float fov=mat_fov;//looks aas if this was cobbled from many souces. 
	vec2 w=u2((uv+.5)/vec2(1024.))*vec2(1.,-1);    
	vec2 p=w*tan(fov*pi/360.0);
	vec3 rayOrigin=vec3(0); 
	vec3 rayDirection=normalize(vec3(p,-1)-rayOrigin);
	
	vec2 f=c2s(rayDirection).yz/pi;f.x=u5(f.x);
	vec4 c=scatter(f);
	float t=-.16 +mat_time;
	vec3 sunPos=vec3(l.x*cos(t)+l.y*sin(t),-l.x*sin(t)+l.y*cos(t),.0);
	float m=dot(d,-sunPos)    ;  
	vec4 o=vec4(c.xyz*RayleighPhase(m)+c.www*MiePhase(m),1.0);	

	return o;
}
