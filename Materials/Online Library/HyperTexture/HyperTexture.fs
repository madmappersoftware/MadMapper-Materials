/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy MlB3Wt\nby FabriceNeyret2",
    "DESCRIPTION": "warning: SLOW",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Iterations", "NAME": "mat_iterations", "TYPE": "int", "MIN": 50, "MAX": 200, "DEFAULT": 100 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"


// a variant from https://www.shadertoy.com/view/ltj3Dc

#define SHADED 0
#define FOG 0
#define NOISE 3 // Perlin, Worley, Trabeculum
#define VARIANT 2

const vec3 skyColor = 0.*vec3(.7,.8,1.); const float skyTrsp = .5;
const vec3 sunColor = vec3(1.,.7,.1)*10.;   
const vec3 lightDir = vec3(.94,.24,.24); // normalize(vec3(.8,.2,-.2));
const vec3 ambient  = vec3(.2,0.,0.), 
           diffuse  = vec3(.8);


// --- noise functions from https://www.shadertoy.com/view/XslGRr
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

const mat3 m = mat3( 0.00,  0.80,  0.60,
           		    -0.80,  0.36, -0.48,
             		-0.60, -0.48,  0.64 );

float mat_hash( float n ) {
    return fract(sin(n)*43758.5453);
}

float mat_noise( in vec3 x ) { // in [0,1]
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*f*(3.-2.*f);

    float n = p.x + p.y*57. + 113.*p.z;

    float res = mix(mix(mix( mat_hash(n+  0.), mat_hash(n+  1.),f.x),
                        mix( mat_hash(n+ 57.), mat_hash(n+ 58.),f.x),f.y),
                    mix(mix( mat_hash(n+113.), mat_hash(n+114.),f.x),
                        mix( mat_hash(n+170.), mat_hash(n+171.),f.x),f.y),f.z);
    return res;
}

float mat_fbm( vec3 p ) { // in [0,1]
    float f;
    f  = 0.5000*mat_noise( p ); p = m*p*2.02;
    f += 0.2500*mat_noise( p ); p = m*p*2.03;
    f += 0.1250*mat_noise( p ); p = m*p*2.01;
    f += 0.0625*mat_noise( p );
    return f;
}
// --- End of: Created by inigo quilez --------------------

// more 3D noise
vec3 mat_hash13( float n ) {
    return fract(sin(n+vec3(0.,12.345,124))*43758.5453);
}
float mat_hash31( vec3 n ) {
    return mat_hash(n.x+10.*n.y+100.*n.z);
}
vec3 mat_hash33( vec3 n ) {
    return mat_hash13(n.x+10.*n.y+100.*n.z);
}

vec4 mat_worley( vec3 p ) {
    vec4 d = vec4(1e15);
    vec3 ip = floor(p);
    for (float i=-1.; i<2.; i++)
   	 	for (float j=-1.; j<2.; j++)
            for (float k=-1.; k<2.; k++) {
                vec3 p0 = ip+vec3(i,j,k),
                      c = mat_hash33(p0)+p0-p;
                float d0 = dot(c,c);
                if      (d0<d.x) { d.yzw=d.xyz; d.x=d0; }
                else if (d0<d.y) { d.zw =d.yz ; d.y=d0; }
                else if (d0<d.z) { d.w  =d.z  ; d.z=d0; }
                else if (d0<d.w) {              d.w=d0; }   
            }
    return sqrt(d);
}


float mat_i_grad=.2/2., mat_i_scale = 5., mat_i_thresh=.5; // default value possibly overloaded below.

// my noise
float mat_tweaknoise( vec3 p , bool step) {
    float d1 = smoothstep(mat_i_grad/2.,-mat_i_grad/2.,length(p)-.5),
          d2 = smoothstep(mat_i_grad/1.,-mat_i_grad/1.,abs(p.z)-.5),
          d=d1;
#if NOISE==1 // 3D Perlin noise
    float v = mat_fbm(mat_i_scale*p);
#elif NOISE==2 // Worley noise
    float v = (.9-mat_i_scale*mat_worley(mat_i_scale*p).x);
#elif NOISE>=3 // trabeculum 3D
  #if VARIANT==0
    d = (1.-d1)*d2; 
  #elif VARIANT==2
    d=d2;
  #endif
    if (d<0.5) return 0.;
    mat_i_grad=.8, mat_i_scale = 10., mat_i_thresh=.5+.5*(cos(.5*mat_animation_time)+.36*cos(.5*3.*mat_animation_time))/1.36;
    vec4 w=mat_i_scale*mat_worley(mat_i_scale*p-vec3(0.,0.,3.*mat_animation_time)); 
    float v=1.-1./(1./(w.z-w.x)+1./(w.a-w.x)); // formula (c) Fabrice NEYRET - BSD3:mention author.
#endif
    
    return (true)? smoothstep(mat_i_thresh-mat_i_grad/2.,mat_i_thresh+mat_i_grad/2.,v*d) : v*d;
}

// Cheap computation of normals+Lambert using directional derivative (see https://www.shadertoy.com/view/Xl23Wy )
// still, we need an estimate of slope amplitude to avoid artifacts (see mat_i_grad+mat_i_scale).
float shadedNormal( vec3 p, float v ) {
    float epsL = 0.01;
#if 1// centered directional derivative
    float dx = (mat_tweaknoise(p+epsL*lightDir,false)-mat_tweaknoise(p-epsL*lightDir,false))/(2.*epsL);
#else // cheap directional derivative
    float dx = (mat_tweaknoise(p+epsL*lightDir,false)-v)/epsL;
#endif
    return clamp(-dx*mat_i_grad/mat_i_scale/v, 0.,1.); // Lambert shading
    
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

    
    vec2 mouse=mat_offset;
    if (mouse.x+mouse.y==0.) mouse.xy=vec2(0.5);

    //camera
    float theta = (mouse.x*2. - 1.)*PI;
    float phi = (mouse.y - .5)*PI;
 // camera shake 
    float t=3.*mat_animation_time,B=.07; theta += B*cos(t); phi += B*sin(t);


    vec3 cameraPos =vec3(sin(theta)*cos(phi),sin(phi),cos(theta)*cos(phi));   
    vec3 cameraTarget = vec3(0.);
    vec3 ww = normalize( cameraPos - cameraTarget );
    vec3 uu = normalize(cross( vec3(0.,1.,0.), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec2 q = 2.*(uv -vec2(.9,.5));
    vec3 rayDir = normalize( q.x*uu + q.y*vv -1.5*ww );

    // ray-trace volume
    vec3 col=vec3(0.);
 	float transp=1., epsC=.01/2.;
    float l = .5;
    vec3 p=cameraPos+l*rayDir, p_=p;
    
    for (int i=0; i<mat_iterations; i++) { 
        float Aloc = mat_tweaknoise(p*mat_scale,true); // density field
        if (Aloc>0.01) {
            

            float a = 2.*PI*float(i)/200.; vec3 c = .5+.5*cos(a+vec3(0.,2.*PI/3.,-2.*PI/3.)+mat_animation_time);

 	        col += transp*c*Aloc;
            //if (c.r>1.) { fragColor = vec4(0.,0.,1.,1.); return; }
            col = clamp(col,0.,1.); // anomaly :-(
   		    transp *= 1.-Aloc;
	        if (transp<.001) break;
        }
 
        p += epsC*rayDir;
    }           

	
	// make a color out of it
	vec3 color = col+ transp*skyColor;

	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
