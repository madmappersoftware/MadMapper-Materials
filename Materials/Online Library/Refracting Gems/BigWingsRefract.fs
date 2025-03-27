// followed BigWing's tutorial on refraction
// https://www.shadertoy.com/view/sls3WN

// This material is intended to Render To Texture,
// with so FXAA surface fx applied to it, or a Denoiser (at minimum samples)

/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "A Refracting background",
    "TAGS": "texture",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Quality", "NAME": "mat_iterations", "TYPE": "int", "MIN": 10, "MAX": 200, "DEFAULT": 100 },
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },

{"LABEL": "Density", "NAME": "mat_dens", "TYPE": "float", "MIN": -1., "MAX": 1.0, "DEFAULT": -0.2 },
{"LABEL": "Abberation", "NAME": "mat_abb", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.2 },
{"LABEL": "FOV", "NAME": "mat_fov", "TYPE": "float", "MIN": 1.5, "MAX": 5.0, "DEFAULT": 4.0 },
{"LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.02, -0.2 ] }, 

{"LABEL": "Color/Ambiant Color", "NAME": "mat_ambColor", "TYPE": "color", "DEFAULT": [ 0.13, 0.1, 0.1, 1.0 ], "FLAGS": "no_alpha" },
{"LABEL": "Color/Colorize", "NAME": "mat_color", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
	"IMPORTED":
	[
		{ "NAME": "texture_graff", "PATH": "studio.exr", 
		"GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
	]
}*/

#define MAX_DIST 10.
#define SURF_DIST .001
#define PI 3.14156

#define S smoothstep
#define T mat_time

mat2 Rot(float a) {
    float s=sin(a), c=cos(a);
    return mat2(c, -s, s, c);
}

float sdBox(vec3 p, vec3 s) {
    p = abs(p)-s;
	return length(max(p, 0.))+min(max(p.x, max(p.y, p.z)), 0.);
}
float sdRoundBox( vec3 p, vec3 b, float r )
{
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
}
float sdOctahedron( vec3 p, float s)
{
  p = abs(p);
  return (p.x+p.y+p.z-s)*0.57735027;
}

//float sdOctahedron( vec3 p, float s)
//{
//  p = abs(p);
//  float m = p.x+p.y+p.z-s;
//  vec3 q;
//       if( 3.0*p.x < m ) q = p.xyz;
//  else if( 3.0*p.y < m ) q = p.yzx;
//  else if( 3.0*p.z < m ) q = p.zxy;
//  else return m*0.57735027;
//    
//  float k = clamp(0.5*(q.z-q.y+s),0.0,s); 
//  return length(vec3(q.x,q.y-s+k,q.z-k)); 
//}

float GetDist(vec3 p) {
//	vec3 c = vec3(3.,0.,0.);
//	vec3 q = mod(p+0.5*c,c)-0.5*c;
//	p = abs(q)-0.5;
	p.xz *= Rot(PI*0.25-T+2.37);
	p.yx *= Rot(PI*0.25+T+0.56);
	p = abs(abs(p+0.2))-0.5;
	float d = sdOctahedron(p, 1.2);
	p.xy *= Rot(PI*0.25+T);
	p.yz *= Rot(PI*0.25-T);
	d = min(d,sdOctahedron(p-0.3, 1.2));
    
    return d*0.9;
}

float RayMarch(vec3 ro, vec3 rd, float side) {
	float dO=0.;
    
    for(int i=0; i<mat_iterations; i++) {
    	vec3 p = ro + rd*dO;
        float dS = GetDist(p)*side;
        dO += dS;
        if(dO>MAX_DIST || abs(dS)<SURF_DIST) break;
    }
    
    return dO;
}

vec3 GetNormal(vec3 p) {
	float d = GetDist(p);
    vec2 e = vec2(.0002, 0);
    
    vec3 n = d - vec3(
        GetDist(p-e.xyy),
        GetDist(p-e.yxy),
        GetDist(p-e.yyx));
    
    return normalize(n);
}

vec3 GetRayDir(vec2 uv, vec3 p, vec3 l, float z) {
    vec3 f = normalize(l-p),
        r = normalize(cross(vec3(0,1,0), f)),
        u = cross(f,r),
        c = f*z,
        i = c + uv.x*r + uv.y*u,
        d = normalize(i);
    return d;
}

vec2 getSphUv(in vec3 r)
{
	float theta = acos(r.y) / -PI;
	float phi = atan( -r.z, r.x) / -PI * 0.5f;

	return vec2(phi,theta);
}

vec4 materialColorForPixel( vec2 texCoord )
{

   vec2 uv = texCoord *2. -1.;
	vec2 m = mat_offset;

    vec3 ro = vec3(0, 3, -3);
    ro.yz *= Rot(-m.y*3.14+1.);
    ro.xz *= Rot(-m.x*6.2831);
    
    vec3 rd = GetRayDir(uv, ro, vec3(0,0.,0), mat_fov);
    vec3 col = vec3(0.2,0.2,0.3)* (1. - length(uv)); // texture(texture_graff, getSphUv(rd)).rgb;
//	col += (rd*0.5+0.5)*0.1;
//	col += texture(texture_graff, getSphUv(rd)).rgb * (1. - length(uv)*0.75);
    float d = RayMarch(ro, rd, 1.); // outside of object
    
    float IOR = 1.45; // index of refraction
    
    if(d<MAX_DIST) {
        vec3 p = ro + rd * d; // 3d hit position
        vec3 n = GetNormal(p); // normal of surface... orientation
        vec3 r = reflect(rd, n);
        
        vec3 rdIn = refract(rd, n, 1./IOR); // ray dir when entering
        
        vec3 pEnter = p - n*SURF_DIST*3.;
        float dIn = RayMarch(pEnter, rdIn, -1.); // inside the object
        
        vec3 pExit = pEnter + rdIn * dIn; // 3d position of exit
        vec3 nExit = -GetNormal(pExit); 
        
        vec3 rdOut = vec3(0.);// refract(rdIn, nExit, IOR);
		vec3 reflTex = vec3(0.);

   		float abb = .05*mat_abb;
        
        // red
        rdOut = refract(rdIn, nExit, IOR-abb);
        if(dot(rdOut, rdOut)==0.) rdOut = reflect(rdIn, nExit);
        reflTex.r = texture(texture_graff, getSphUv(rdOut)).r;
        
        // green
        rdOut = refract(rdIn, nExit, IOR);
        if(dot(rdOut, rdOut)==0.) rdOut = reflect(rdIn, nExit);
        reflTex.g =  texture(texture_graff, getSphUv(rdOut)).g;
        
        // blue
        rdOut = refract(rdIn, nExit, IOR+abb);
        if(dot(rdOut, rdOut)==0.) rdOut = reflect(rdIn, nExit);
        reflTex.b =  texture(texture_graff, getSphUv(rdOut)).b;
        
        float dens = mat_dens;
        float optDist = exp(-dIn*dens);
        
        reflTex = reflTex*optDist;

		float fresnel = pow(1.+dot(rd,n),5.);
		vec3 rfl = texture(texture_graff, getSphUv(reflect(rd,n))).rgb;
	
		col = reflTex;
		col += mat_ambColor.rgb;
		
		col = mix(col, rfl, fresnel);

		col += fract(rdOut)*mat_color;
		col *= vec3(dot(vec3(0.25,0.6,0.15),(vec3(step(vec3(0.5), fract(rdOut*1.))))));
    }
    
	return vec4(col,1.);
}