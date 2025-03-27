/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Tater",
    "DESCRIPTION": "A faster squarish Voronoi noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
	{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 

	{ "LABEL": "Color/Cycle", "NAME": "mat_cycle", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
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

// Voronoi by Tater
// https://www.shadertoy.com/view/fslcDS

const float mat_pi = 3.1415926535;
#define rot(a) mat2(cos(a),sin(a),-sin(a),cos(a))
#define h13(n) fract((n)*vec3(12.9898,78.233,45.6114)*43758.5453123)
vec2 vor(vec2 v, vec3 p, vec3 s){
    p = abs(fract(p-s)-0.5);
    float a = max(p.x,max(p.y,p.z));
    float b = min(v.x,a);
    float c = max(v.x,min(v.y,a));
    return vec2(b,c);
}

float vorMap(vec3 p){
    vec2 v = vec2(5.0);
    v = vor(v,p,h13(0.96));
    p.xy*=rot(1.2);
    v = vor(v,p,h13(0.55));
    p.yz*=rot(2.);
    v = vor(v,p,h13(0.718));
    p.zx*=rot(2.7);
    v = vor(v,p,h13(0.3));
    return v.y+v.x; 
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2. - 1.;
	uv *= mat_scale;

	float G = vorMap(vec3(uv,mat_time));

	G = fract(G  + mat_cycle*0.9999);

    // Apply brightness
    G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0.,1.);

	if (mat_invert) G = 1 - G;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, G );


	return vec4(color,1.);
}