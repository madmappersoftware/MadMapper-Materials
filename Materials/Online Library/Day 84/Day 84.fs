/*{
    "CREDIT": "jeyko",
    "DESCRIPTION": "www.shadertoy.com/view/Wssczn",
    "TAGS": "graphics",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Iterations", "NAME": "mat_iterations", "TYPE": "int", "MIN": 10, "MAX": 300, "DEFAULT": 200 }, 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 },
{"LABEL": "Flicker", "NAME": "mat_flicker", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
{"LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
	
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
    "IMPORTED": [
{"NAME": "texture_noise", "PATH": "noise.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

vec3 glow = vec3(0);
#define dmin(a, b) a.x < b.x ? a : b
#define PI acos(-1.)
#define tau (2.*PI)
#define rot(x) mat2(cos(x),-sin(x),sin(x),cos(x))
#define mat_time (mat_time + 3.6)
#define pal(a,b,c,d,e) (a + b*sin(c*d + e))

vec3 att = vec3(1);

float pModPolar(inout vec2 p, float repetitions) {
	float angle = 2.*PI/repetitions;
	float a = atan(p.y, p.x) + angle/2.;
	float r = length(p);
	float c = floor(a/angle);
	a = mod(a,angle) - angle/2.;
	p = vec2(cos(a), sin(a))*r;
	// For an odd number of repetitions, fix cell index of the cell in -x direction
	// (cell index would be e.g. -5 and 5 in the two halves of the cell):
	if (abs(c) >= (repetitions/2.)) c = abs(c);
	return c;
}
#define pmod(p,x) mod(p,x) - 0.5*x
vec4 valueNoise(float t){
	return mix(texture(texture_noise,vec2(floor(t)/256.)),texture(texture_noise,vec2(floor(t) + 1.)/256.), smoothstep(0.,1.,fract(t)));
}

vec2 map(vec3 p){
	vec2 d = vec2(10e7);
    float modD = 3.;
    float idz = floor(p.z/modD);
    p.z = pmod(p.z, modD);
    
    
    vec3 q = p;
    vec3 b = p;
    float idb = pModPolar(q.xy, 3.);
    //b.xy *= rot(idz*0.5);
    pModPolar(b.xy, 3.);
    
    b.x -= 0.8;
    
    vec3 u = p;
    
    //u.xy *= rot(idz);
    float o = pModPolar(u.xy, 5.);
    u.x -= 1.;
    
    q.x -= 0.8;
    
    float dG = -u.x;
    d = dmin(d, vec2(dG, 3.));
    
    //u -= 0.1;
    u.y = abs(u.y);
    u.y -= 0.7;
    dG = length(u.xy) - 0.02;
    d = dmin(d, vec2(dG, 8.));
    
        //glow += 0.2/(0.01 + dG*dG*2.)*att;
	//q = abs(q);
    //q.xy *= rot(-1.5);
    //q.x -= 0.2;
    vec3 z = q;
    z = abs(z) - vec3(0.01,0.4,0.1);
    float dC = max(z.z, max(z.y, z.x));
    d = dmin(d, vec2(dC, 0.));
    z = q;
    z.x += 0.02;
    z = abs(z) ;
    z -= vec3(0.01,0.3,0.02);
    float dCb = max(z.z, max(z.y, z.x));
    d = dmin(d, vec2(dCb, 1.));

   
    z = b;
    z.y -= 0.2;
    z.x += 0.3;
    z.z += modD*0.10;
    z.xy *= rot(0.7 + sin(mat_time*0.2 + idz*0.5));
    z = abs(z);
    z.zx *= rot(-0.1);
    z = abs(z) - vec3(0.01,0.5,0.04);
    float dD = max(z.z,max(z.x, z.y));
    
    d = dmin(d, vec2(dD, 5.));
      
    z = u;
    z.y -= 0.2;
    z.x += 0.3;
    z.z -= modD*0.25;
    
    z = abs(z);
    z.xz *=rot(0.25*PI);
    z.x -= 0.4;
    z.xy *=rot(0.25*PI);
    
    //z.x -= 0.08;
    z = abs(z) - vec3(0.07,0.1,0.04);
    float dDd = max(z.z,max(z.x, z.y));
    //d = dmin(d, vec2(dDd, 5.));
       
    vec4 a = valueNoise((idb + mat_time*30.*mat_flicker + idz*3.));
    
    //vec3 c = max(pal(0.7,1., vec3(3.7,0.3,0.6), 0.6,4.4 + sin(iTime) + sin(idz * idb)*0.2), 0.);
    vec3 c = max(pal(0.7,1., vec3(3.,0.3,0.1), 0.6,4.4 + sin(mat_time) + idz + sin(idz * idb)*0.2), 0.1);
    
    glow += pow(smoothstep(0.,1.,a.z*1.5), 20.)*1.5/(0.005 + dCb*dCb*(90. - a.x*20.))*att*c* pow(smoothstep(1.,0.,length(q.y*1.)), 5.);

    return d;
}
float dith;
vec2 march(vec3 ro, vec3 rd, inout vec3 p, inout float t, inout bool hit){
	vec2 d = vec2(10e7);

    p = ro; t = 0.; hit = false;
    for(int i = 0; i < mat_iterations ; i++){
    	d = map(p);
        d.x *= dith;
        
    //	glow += exp(-d.x*2.);
        if(d.x < 0.002){
        	hit = true;
            break;
        }
        
        t += d.x;
        p = ro + rd*t;
    }
    
    
    return d;
}

vec3 getNormal(vec3 p){
	vec2 t= vec2(0.001,0);
	return normalize(map(p).x - vec3(
    	map(p - t.xyy).x,
    	map(p - t.yxy).x,
    	map(p - t.yyx).x
    ));
}


vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord -0.5;

	uv *= 1. - dot(uv,uv)*0.14;
    
    uv.xy *= rot((mat_time - 3.6)*0.1);
    
    vec3 col = vec3(0);

    dith = mix(0.8,1., texture(texture_noise, 20.*uv*256.).x);
    vec3 ro = vec3(0);
    	ro.xy += mat_offset;
    ro.z += mat_time*1.5;
    
    vec3 rd = normalize(vec3(uv,2.));
    
    vec3 p; float t; bool hit;
    float side = 1.;
    float tA;
    
    for(int i = 0; i < 3; i ++){
    	vec2 d = march(ro, rd, p, t, hit);
    	vec3 n = getNormal(p);
        
        vec3 ld = normalize(vec3(1));
        vec3 h = normalize(ld - rd);
        
        float diff = max(dot(n, ld), 0.);
        float spec = pow(max(dot(n, -h), 0.), 10.);
        float fres = pow(1. - max(dot(n, -rd), 0.), 5.);
        
        if(i == 0){
        	tA = t;
        }    
        if(d.y == 5.){
        	//col += fres*0.1*att*(glow);
            col += fres*0.06*att*(glow);
        }
        if(d.y == 8.){
        	col += fres*0.02*att*(glow);
            //col += fres*20.*att;
        }
        if (d.y == 3.){
        	rd = reflect(rd, n);
            att *= vec3(0.6,0.8,0.8)*0.2;
            col += spec*0.04*att;
            ro = p + n*0.2;
        } else {
        	break;
        }
    }
      
    col += glow*0.001;   
    col = mix(col, vec3(0.4,0.4,0.7)*0.004, pow(smoothstep(0.,1.,tA*0.013), 1.6));
    
    col = mix(col,smoothstep(0.,1.,col), 0.4);
    col *= 18.;
    col = pow(col, vec3(0.4545));
    col *= 1. - dot(uv,uv)*2.;

	return vec4(col,1.);
}