/*{
    "CREDIT": "Derived by Tim Brassey from Shadertoy Synth Waves by Avin",
    "DESCRIPTION": "Hue Waves",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 2.0 }, 
		{"LABEL": "Line Count", "NAME": "mat_count", "TYPE": "float", "MIN": 1.0, "MAX": 50.0, "DEFAULT": 10.0 },
		{"LABEL": "Height", "NAME": "mat_height", "TYPE": "float", "MIN": 1.0, "MAX": 100.0, "DEFAULT": 20.0 },
		{"LABEL": "Ribboning", "NAME": "mat_ribbon", "TYPE": "float", "MIN": 0.001, "MAX": 0.10, "DEFAULT": 0.01 },
		{"LABEL": "Stroke", "NAME": "mat_stroke", "TYPE": "float", "MIN": 0.001, "MAX": 0.05, "DEFAULT": 0.02 },
		{"LABEL": "Activity", "NAME": "mat_activity", "TYPE": "float", "MIN": 0.001, "MAX": 1, "DEFAULT": 0.5 },

    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/


#define COUNT mat_count
#define COL_BLACK vec3(23,32,38) / 255.0 

#define SF 1./min(RENDERSIZE.x,RENDERSIZE.y)
#define SS(l,s) smoothstep(SF,-SF,l-s)
#define hue(h) clamp( abs( fract(h + vec4(3,2,1,0)/3.) * 6. - 3.) -1. , 0., 1.)

// Original noise code from https://www.shadertoy.com/view/4sc3z2
#define MOD3 vec3(.1031,.11369,.13787)

vec3 hash33(vec3 p3)
{
	p3 = fract(p3 * MOD3);
    p3 += dot(p3, p3.yxz+19.19);
    return -1.0 + 2.0 * fract(vec3((p3.x + p3.y)*p3.z, (p3.x+p3.z)*p3.y, (p3.y+p3.z)*p3.x));
}

float simplex_noise(vec3 p)
{
    const float K1 = 0.333333333;
    const float K2 = 0.166666667;
    
    vec3 i = floor(p + (p.x + p.y + p.z) * K1);
    vec3 d0 = p - (i - (i.x + i.y + i.z) * K2);
        
    vec3 e = step(vec3(0.0), d0 - d0.yzx);
	vec3 i1 = e * (1.0 - e.zxy);
	vec3 i2 = 1.0 - e.zxy * (1.0 - e);
    
    vec3 d1 = d0 - (i1 - 1.0 * K2);
    vec3 d2 = d0 - (i2 - 2.0 * K2);
    vec3 d3 = d0 - (1.0 - 3.0 * K2);
    
    vec4 h = max(0.6 - vec4(dot(d0, d0), dot(d1, d1), dot(d2, d2), dot(d3, d3)), 0);
    vec4 n = h * h * h * h * vec4(dot(d0, hash33(i)), dot(d1, hash33(i + i1)), dot(d2, hash33(i + i2)), dot(d3, hash33(i + 1)));
    
 	return dot(vec4(mat_height), n);
}

vec4 materialColorForPixel( vec2 texCoord ) {

  
    vec2 uv = texCoord.xy;
    
    float m = 1;
    float t = TIME * mat_speed;
    vec3 col;
    for(float i=COUNT; i>=0.; i-=1.){
        float edge = simplex_noise(vec3(uv * vec2(3, 0.) + vec2(0, t + i*mat_ribbon), 1.))*mat_activity + (.5/COUNT)*i + .25;
        float mi = SS(edge, uv.y) - SS(edge + mat_stroke, uv.y);        
        m *= SS(edge, uv.y);
        m += mi;        
        
        if(mi > 0){
        	col = (1-hue(i/COUNT).rgb);
        }        
    }           
    
   col = mix(COL_BLACK, col, m);

    
    return vec4(col,1.0);
}







