/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Audio Reactive Beamish thing",
    "TAGS": "atmospheric",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Rotation", "NAME": "mat_rot", "TYPE": "float", "MIN": 0.0, "MAX": 180.0, "DEFAULT": 0.0 }, 
        {"LABEL": "Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 8, "MAX": 80, "DEFAULT": 18 }, 
		{"LABEL": "Wobble", "NAME": "mat_wobble", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Audio", "NAME": "mat_audio", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
        {"LABEL": "Freq. Spread", "NAME": "mat_freq", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. }, 

		{ "LABEL": "Color/Bottom", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Top", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

        {
            "NAME": "spectrum",
            "TYPE": "audioFFT",
            "SIZE": 49,
            "ATTACK": 0.3,
            "DECAY": 0.0,
            "RELEASE": 0.2
        },
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_precision",
       "PRESERVE_ORDER": true,
       "ENABLE_FRAME_BLENDING": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat2 rot(float a)
{
	return mat2(cos(a),sin(a),-sin(a),cos(a));
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float t = mat_time;
    float indexNorm = float(pointNumber)/(pointCount-1);

    // lissajous
    pos = vec2(indexNorm*0.01);

	vec3 n = dFlowNoise(vec2(mat_time),0.34);
	pos += n.yz*0.1*mat_wobble;

	vec3 audio = dFlowNoise(vec2(mat_time,(pointNumber/2)*345.678),0.34)*0.1;
	float k = IMG_NORM_PIXEL(spectrum,vec2(0.17+mat_freq*0.6*indexNorm,0)).r*4.;
	k = pow(max(0.04,k-0.1),4.)*20.;

	pos.x += audio.z*k*10.*mat_audio;
	pos.y += (k)*0.1*mat_freq;

	pos.xy *= rot(mat_rot*3.1459/180);
	if(pointNumber >= 4 && pointNumber <= 80) pos.y += audio.y*k*1.;
	
    color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,indexNorm),1.);
    shapeNumber = int(floor(k*3.));
}
