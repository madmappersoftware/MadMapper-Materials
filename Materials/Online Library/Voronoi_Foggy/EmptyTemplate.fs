/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Adapted from jasondecode\nShadertoy XlXBWj",
    "DESCRIPTION": "voronoise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
	{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 

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

#include "MadCommon.glsl"

#define MAX_MARCH_STEPS 16
#define MARCH_STEP_SIZE 0.2

#define NOISE_AMPLITUDE 0.75

#define FBM_ITERATIONS 3
#define FBM_AMPLITUDE_GAIN 0.8
#define FBM_FREQUENCY_GAIN 1.9

#define FOV45 0.82842693331417825056778150945139

vec3 UvToWorld(vec2 uv) { return normalize(vec3((uv-0.5) * vec2(1024.), -1024. / FOV45)); }
vec3 Hash3( vec3 p ) { return fract(sin(vec3( dot(p,vec3(127.1,311.7,786.6)), dot(p,vec3(269.5,183.3,455.8)), dot(p,vec3(419.2,371.9,948.6))))*43758.5453); }
float Voronoi(vec3 p)
{
	vec3 n = floor(p);
	vec3 f = fract(p);

	float shortestDistance = 1.0;
	for (int x = -1; x < 1; x++) {
		for (int y = -1; y < 1; y++) {
			for (int z = -1; z < 1; z++) {
				vec3 o = vec3(x,y,z);
				vec3 r = (o - f) + 1.0 + sin(Hash3(n + o)*50.0)*0.2;
				float d = dot(r,r);
				if (d < shortestDistance) {
					shortestDistance = d;
				}
			}
		}
	}
	return shortestDistance;
}

float FractalVoronoi(vec3 p)
{
	float n = 0.0;
	float f = 0.5, a = 0.5;
	mat2 m = mat2(0.8, 0.6, -0.6, 0.8);
	for (int i = 0; i < FBM_ITERATIONS; i++) {
		n += Voronoi(p * f) * a;
		f *= FBM_FREQUENCY_GAIN;
		a *= FBM_AMPLITUDE_GAIN;
		p.xy = m * p.xy;
	}
	return n;
}

vec2 March(vec3 origin, vec3 direction)
{
	float depth = MARCH_STEP_SIZE;
	float d = 0.0;
	for (int i = 0; i < MAX_MARCH_STEPS; i++) {
		vec3 p = origin + direction * depth + vec3(0.,0.,mat_time);
		d = FractalVoronoi(p) * NOISE_AMPLITUDE;
		depth += max(MARCH_STEP_SIZE, d);
	}
	return vec2(depth, d);
}


mat3 rotation(float angle, vec3 axis)
{
	float s = sin(-angle);
	float c = cos(-angle);
	float oc = 0.15 - c;
	vec3 sa = axis * s;
	vec3 oca = axis * oc;
	return mat3(	
		oca.x * axis + vec3(	c,	-sa.z,	sa.y),
		oca.y * axis + vec3( sa.z,	c,		-sa.x),		
		oca.z * axis + vec3(-sa.y,	sa.x,	c));	
}

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = texCoord * 2. -1.;
	uv *= mat_scale;
	uv += 0.5;

	vec3 direction = UvToWorld(uv);
	vec3 origin = vec3(0.0, 0, 0.0);
	vec2 data = March(origin, direction);

	float G = data.y * data.x * 0.7;


    // Apply brightness
    G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0.,1.);

	if (mat_invert) G = 1 - G;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, G );


	vec4 final_color = vec4(color,1.0);	

	return final_color;
}