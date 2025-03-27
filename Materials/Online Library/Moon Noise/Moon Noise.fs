/*
	Based on Voronoise by iq :https://www.shadertoy.com/view/Xd23Dh
	and Gabor 4: normalized  by FabriceNeyret2 : https://www.shadertoy.com/view/XlsGDs
*/

/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Moonish noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
	{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 3.0 }, 

	{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" }, 
    { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },

    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#define PI 3.14159265358979

float hash( in vec3 p ) 
{
    return fract(sin(p.x*15.32758341+p.y*39.786792357+p.z*59.4583127+7.5312) * 43758.236237153)-.5;
}

vec3 hash3( in vec3 p )
{
    return vec3(hash(p),hash(p+1.5),hash(p+2.5));
}

float gavoronoi3b(in vec3 p)
{    
    vec3 ip = floor(p);
    vec3 fp = fract(p);
    float f = 2.*PI;																																										;//frequency
    float v = .8;//cell variability <1.
    float va = 0.0;
    float wt = 0.0;
    for (int i=-1; i<=1; i++) 
	for (int j=-1; j<=1; j++)
    for (int k=-1; k<=1; k++)     
	{		
        vec3 o = vec3(i, j, k)-.5;       		
        vec3 pp = fp +o  - v*hash3(ip - o);
        float d = length(pp);
        float w = exp(-d*4.);
        wt +=w;
        va +=sin(sqrt(d)*f)*w;
	}    
    return va/wt;
}

float fbmabs( vec3 p ) {
	
	float f=1.2;
   
	float r = 0.0;	
    for(int i = 0;i<5;i++){	
		r += abs(gavoronoi3b( p*f ))/f;       
	    f *=2.3;
	}
	return r/2.;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord * 2. -1.;
	uv *= mat_scale;
	float n = fbmabs(vec3(uv,mat_time));

    n += mat_brightness;
    n = mix(0.5, n, mat_contrast);

	if (mat_invert) n = 1 - n;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, n );
	return vec4(color,1.);
}