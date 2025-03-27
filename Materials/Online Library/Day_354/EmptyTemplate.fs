/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "jeyko / shadertoy wdVBDG",
    "DESCRIPTION": "graphical beauty",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
{"LABEL": "Complexity", "NAME": "mat_complex", "TYPE": "int", "MIN": 1, "MAX": 256, "DEFAULT": 128 },
{"LABEL": "Outline", "NAME": "mat_outline", "TYPE": "bool",  "DEFAULT": false, "FLAGS":"button" },  
{"LABEL": "Distort", "NAME": "mat_distort", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

float df;

#define pal(a,b,c,d,e) (a + (b)*sin((c)*(d) + e))

vec3 add(float da, float db, vec3 cola, vec3 colb, float method){
    vec3 colo = vec3(0);
    
    float aa = smoothstep(df,0.,db);
    
    if (method == 0.){
        colo = mix(cola,colb,aa);
    }
    return colo;
}

mat2 rot(float angle){
    return mat2(cos(angle), -sin(angle), sin(angle), cos(angle));
}
#define xor(a,b,c) min(max(a,-(b)), max(-(a) + c,b))


vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p = (texCoord *2. - 1.)*0.7;
    vec2 uv = p;
    
    float env = pow(abs(sin(mat_time*0.5)),0.3)*sign(sin(mat_time*0.5 ))*0.5 + 0.5 ;   
    
    p += vec2(sin(mat_time + sin(mat_time*0.4))*0.04,sin(mat_time*0.6 - sin(mat_time*0.4))*0.04);
    
    p *= 1. + env;
    df = dFdx(p.x);
    
    vec3 col = vec3(0.01,0.4,0.91);
    
    col += sin(p.xyy + sin(mat_time + length(p))*0.4)*0.5;
      
    float d = 10e4;  
    vec3 colo = vec3(0);

  #define TP(P) (floor(T) + pow(fract(T),P))

    float T = mat_time - 14.;
    for(float i = 0.; i < mat_complex; i++){
        float env = sin(TP(5. + sin(i)*2.));
        env = pow(abs(env),4.)*sign(env);
        T += mat_distort;
        p.x += 0. + env*0.004;
        p *= rot(sin(TP(2.)*0.001)*0.41);
        float ld = length(p) - .2 - sin(i*1.4 + sin(T)*0.2)*0.4;
        vec3 c = pal(0.5,.5,vec3(3,2.1,1.5),1.,i + p.x);
        
        if(sin(i *0.4) < -0.4){
            ld = xor(ld,-(p.y) - .5*sin(i + T*0.13 ),.4);
            
        }
        if(sin(i *0.4) > 0.1){
            ld = abs(ld - 0.1*sin(i + TP(5.)));
        }
            
        colo = add(d,ld,colo,c,0.);
        
        
        d = xor(d,ld,-0. - sin(i)*0.1);
        
        if(mat_outline)d = min(d,ld);
    }

    col = add(10.,d + 0.01,col,colo,0.);       
      
    col = mix(1. - col,col, env);    
    col *= smoothstep(1.,0.,dot(uv,uv)*0.7);
    
    col = mix(col,smoothstep(0.,1.,col),0.6);
    
    col = pow(col,vec3(0.454545));

	return vec4(col,1.);
}