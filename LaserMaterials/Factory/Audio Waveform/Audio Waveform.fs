/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Laser audio waveform display",
    "TAGS": "atmospheric",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 1.0 },
{"LABEL": "Audio", "NAME": "mat_audio", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
{"LABEL": "Y Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": -.50, "MAX": 0.5, "DEFAULT": 0.0 },
{"LABEL": "Rotation", "NAME": "mat_rot", "TYPE": "float", "MIN": 0.0, "MAX": 180, "DEFAULT": 0.0 },

{"LABEL": "Points", "NAME": "mat_points", "TYPE": "bool",  "DEFAULT": false, "FLAGS":"button" }, 

{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" },

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
       "POINT_COUNT": 50,
       "PRESERVE_ORDER": true,
       "ENABLE_FRAME_BLENDING": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat2 ro(float a){return mat2(cos(a),sin(a),-sin(a),cos(a));}


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float t = mat_time;
    float indexNorm = float(pointNumber)/(pointCount-1);

    pos = vec2(indexNorm *2.-1.,0.);

	float k = IMG_NORM_PIXEL(spectrum,vec2(fract(indexNorm + mat_time),0.5)).r*2.;
	k = pow(k,3.4)*mat_audio;
	float d = max(0.,1. - length(pos*2.));
	pos.y += k*d;

	pos.y += mat_offset;

	pos.xy *= ro(mat_rot*3.1459/180.);
    color = vec4( (1. - abs(indexNorm *2.-1.)) * mix(mat_backgroundColor.rgb,mat_foregroundColor.rgb,vec3(k*10.)), 1.) ;
	shapeNumber = 0;
	if(mat_points == true) shapeNumber = pointNumber;   
}
