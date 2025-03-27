/*{
    "CREDIT": "afl_ext",
    "DESCRIPTION": "5D Noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 }, 
{"LABEL": "Drag", "NAME": "mat_seed", "TYPE": "float", "MIN": 0.0, "MAX": 100.0, "DEFAULT": 20.0 },
{"LABEL": "Use Threshold", "NAME": "mat_use", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" },
{"LABEL": "Threshold", "NAME": "mat_thresh", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
{ "LABEL": "UV/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

/*
5D wave noise
afl_ext 2018
public domain
*/

#define EULER 2.7182818284590452353602874

float hash(float p){
    return fract(4768.1232345456 * sin(p));
}

float wave(vec4 uv, vec4 emitter, float speed, float phase, float timeshift){
    float dst = distance(uv, emitter);
    return pow(EULER, sin(dst * phase - timeshift * speed)) / EULER;
}
vec4 wavedrag(vec4 uv, vec4 emitter){
    return normalize(uv - emitter);
}
float seedWaves = 0.0;
vec4 randWaves(){
    float x = hash(seedWaves);
    seedWaves += 1.0;
    float y = hash(seedWaves);
    seedWaves += 1.0;
    float z = hash(seedWaves);
    seedWaves += 1.0;
    float w = hash(seedWaves);
    seedWaves += 1.0;
    return vec4(x,y,z,w) * 2.0 - 1.0;
}

float getwaves5d(vec4 position, float dragmult, float timeshift){
    float iter = 0.0;
    float phase = 6.0;
    float speed = 2.0;
    float weight = 1.0;
    float w = 0.0;
    float ws = 0.0;
    for(int i=0;i<20;i++){
        vec4 p = randWaves() * 30.0;
        float res = wave(position, p, speed, phase, 0.0 + timeshift);
        float res2 = wave(position, p, speed, phase, 0.006 + timeshift);
        position -= wavedrag(position, p) * (res - res2) * weight * dragmult;
        w += res * weight;
        iter += 12.0;
        ws += weight;
        weight = mix(weight, 0.0, 0.2);
        phase *= 1.2;
        speed *= 1.02;
    }
    return w / ws;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2.-1.;
	uv *= mat_scale;
	vec2 mouse = mat_offset;
    float c =  getwaves5d(vec4( uv, mouse.x * 10.0, mouse.y * 10.0),mat_seed, mat_time  );

	if(mat_use) c = smoothstep(mat_thresh-0.02,mat_thresh+0.02,c);
	return vec4(vec3(c),1.);
}