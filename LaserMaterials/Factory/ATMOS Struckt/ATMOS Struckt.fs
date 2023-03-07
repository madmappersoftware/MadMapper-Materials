/*{
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Labirynth effect",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [
        {
            "NAME": "spectrum",
            "TYPE": "audioFFT",
            "SIZE": 3,
            "ATTACK": 0.0,
            "DECAY": 0.0,
            "RELEASE": 0.3
        },

		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.15, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Auto Move/Active", "NAME": "mat_auto", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" }, 
		{ "LABEL": "Auto Move/Speed", "NAME": "mat_mspeed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.4 }, 
		{ "LABEL": "Auto Move/Amplitude", "NAME": "mat_amplitude", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 }, 

		{ "LABEL": "Audio Reactive/Active", "NAME": "mat_audio_level", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0 }, 

		{ "LABEL": "Color/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS":"no_alpha" },
      ],
	 "GENERATORS": [
 		{"NAME": "mat_mtime", "TYPE": "time_base", "PARAMS": {"speed": "mat_mspeed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 333
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

float retime(in float t)
{
	return (floor(t) + smoothstep(0.,1.,fract(t)) );
}

const vec3 positions[37] = vec3[](
	vec3(0,0,0),

	vec3(0,2,1),
	vec3(0,4,1),

	vec3(1,0,2),
	vec3(1,5,2),

	vec3(2,0,3),
	vec3(2,2,3),

	vec3(3,0,4),
	vec3(3,3,4),

	vec3(4,0,5),
	vec3(4,2,5),

	vec3(4,4,6),
	vec3(4,6,6),

	vec3(5,5,7),
	vec3(5,7,7),

	vec3(6,4,8),
	vec3(6,6,8),

	vec3(7,0,9),
	vec3(7,3,9),

	vec3(7,5,10),
	vec3(7,7,10),

	vec3(0,1,11),
	vec3(1,1,11),

	vec3(5,1,12),
	vec3(7,1,12),

	vec3(4,2,13),
	vec3(6,2,13), // 26

	vec3(1,3,14), // 27
	vec3(7,3,14),

	vec3(2,4,15),
	vec3(8,4,15),

	vec3(0,5,16),
	vec3(3,5,16),

	vec3(0,6,17),
	vec3(4,6,17),

	vec3(0,7,18),
	vec3(7,7,18)
);

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	// We copy the pathes stored in "positions" 3x3 times
	#define REPEAT_COUNT 3

	if (pointNumber > 37 * REPEAT_COUNT * REPEAT_COUNT) {
		shapeNumber = -1; // point will be ignored if shape number is negative
		return;
	}

	int copyNumber = pointNumber / 37;
	int copyXOffset = copyNumber % REPEAT_COUNT;
	int copyYOffset = copyNumber / REPEAT_COUNT;
	pointNumber %= 37; 

	vec3 constData = positions[pointNumber];
	shapeNumber = int(constData.z);

	pos = vec2(-1.5,-1.5) + constData.xy/7;
	pos += vec2(copyXOffset*8./7,copyYOffset*8./7);
	
	pos = rot(PI*0.25)*pos.xy;
	pos /= mat_scale/4;
	pos += vec2(mat_offset.x,mat_offset.y);

	float alteredTime = retime(retime(retime(mat_mtime)));

	if (mat_auto)
	{
		float t = alteredTime;
		float x = flowNoise(vec2(t*0.2,1.234),34.567);
		float y = flowNoise(vec2(t*0.2,0.497),7.0007);

		pos += vec2(x,y)*mat_amplitude*3;
		float a = flowNoise(vec2(t*0.4,0.765),0.5678);
		pos *= rot(a);
	}
	
	if (mat_audio_level>0) {
	    float audioVal = IMG_NORM_PIXEL(spectrum,vec2(0,0)).r * mat_audio_level * mat_audio_level - 0.1;
		pos *= 1-0.3*max(0,audioVal);
	}

    color = mat_color;
}
