/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Audio moving laser plane",
    "TAGS": "atmospheric",
    "VSN": "1.0",
    "INPUTS": [ 
{ "NAME": "spectrum","TYPE": "audioFFT","SIZE": 12,"ATTACK": 0.3, "DECAY": 0.0,"RELEASE": 0.5},
{ "NAME": "spectrum_fat","TYPE": "audioFFT","SIZE": 12,"ATTACK": 0., "DECAY": 0.0,"RELEASE": 0.},
{"LABEL": "Pattern", "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 2 }, 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
{"LABEL": "Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0.0, "MAX": 0.999, "DEFAULT": 0.1 },

{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
{"LABEL": "Audio", "NAME": "mat_audio", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
{"LABEL": "Automatic", "NAME": "mat_auto", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" }, 

{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" },

    ],
    "GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_count", "value2" : 8}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_instance_count",
       "PRESERVE_ORDER": false,
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
	int global_point = (pointNumber/mat_count) % 4;
	int copy = global_point / 4;
	int local_point = global_point % 2;

	float audio = IMG_NORM_PIXEL(spectrum,vec2(0.2 ,0)).r*4.;
	float audio_pow = pow(audio,3.3)*2.;

	float audio_fat = IMG_NORM_PIXEL(spectrum_fat,vec2(0.2 ,0)).r*4.;
	float audio_fat_pow = pow(max(0.,audio_fat-0.),4.3)*20.*mat_audio;

	float w = mat_scale*audio_pow*0.5;

	if (global_point < 2) {
		pos = vec2(0.,w - local_point*w*2.);
		shapeNumber = 1;
		color = mat_backgroundColor;
	} else {
		pos = vec2(0.,w - local_point*w*2.);
		shapeNumber = pointNumber;	
		color = mat_foregroundColor;	
	}

	pos.xy *= ro(3.1456*0.5);
	if (mat_auto == true) {
		float tt = T(T(T(mat_time)));
		pos *= ro(tt);
		vec3 n = 0.5*dFlowNoise(vec2(tt*0.2),1.23);
		pos.xy += n.yz*0.1;
	}

	if (pointNumber > 4) {
		float tt = T(T(T(mat_time)));
		vec3 n = dFlowNoise(vec2(tt*10.),pointNumber *0.234);
		pos.xy += n.yz*0.05*audio_fat_pow;
		shapeNumber = (pointNumber/2);
		color = mix(mat_foregroundColor,mat_backgroundColor, vec4(vec3(float(global_point < 2 ? 1 : 0)),1.));
	}

	vec2 lastPos = texture(mm_LastFrameData,vec2(float(pointNumber+0.5)/pointCount,0.2)).rg;

	if (lastPos.x > -1 && lastPos.x < 1) {
		pos = mix(pos,lastPos,mat_feedback);
	}
}
