/*{
    "CREDIT": "frz / 1024",
    "DESCRIPTION": "Clapping Bar audio ",
    "TAGS": "Laser graphics",
    "VSN": "1.0",
    "INPUTS": [ 
{ "NAME": "spectrum","TYPE": "audioFFT","SIZE": 6,"ATTACK": 0.1, "DECAY": 0.0,"RELEASE": 0.2},
{"LABEL": "Count", "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 3 }, 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
{"LABEL": "Audio", "NAME": "mat_audio", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 

{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
{"LABEL": "Spread", "NAME": "mat_spread", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
{"LABEL": "Automatic", "NAME": "mat_auto", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" }, 

{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" },

    ],
    "GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_count", "value2" : 4}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_instance_count",
       "PRESERVE_ORDER": true,
       "ENABLE_FRAME_BLENDING": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat2 ro(float a){return mat2(cos(a),sin(a),-sin(a),cos(a));}
float T(float t){return floor(t) + smoothstep(0.,1.,fract(t));}


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float t = mat_time;
    float indexNorm = float(pointNumber)/(pointCount-1);
	int global_point = pointNumber % 4;
	int copy = pointNumber / 4;
	int local_point = global_point % 2;

	float audio = IMG_NORM_PIXEL(spectrum,vec2(0.17+(copy)*0.05 + (fract(mat_time)*0.1-0.05),0)).r*4.;
	float audio_pow = pow(audio,3.3)*2.*mat_audio;

	float audio_w = IMG_NORM_PIXEL(spectrum,vec2(0.2,0)).r;
	float audio_w_pow = pow(audio_w,3.3)*4.*mat_audio;

	float w = mat_scale*audio_pow;
	float copy_width = (0.25*mat_spread)*mix(1.,audio_w,float(1.));
	if(mat_count == 1)copy_width = 0.;
	if(global_point < 2){

		pos = vec2(copy_width*copy - copy_width*(mat_count-1)*0.5,w - local_point*w*2.);
		shapeNumber = 1;
		color = mat_backgroundColor;
		}
	else{
			pos = vec2(copy_width*copy - copy_width*(mat_count-1)*0.5,w - local_point*w*2.);
			shapeNumber = pointNumber;	
			color = mat_foregroundColor;	
		}

	if(mat_auto == true){
			pos *= ro(T(T(T(mat_time))));
	}
}
