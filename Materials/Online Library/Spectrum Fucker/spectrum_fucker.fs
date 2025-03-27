/*{
    "CREDIT": "hexler",
    "DESCRIPTION": "Audio effect based on spectrum: https://vimeo.com/51993089.",
    "TAGS": "audio,reactive,spectrum",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Audio Level", "NAME": "mat_audio_level", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Speed", "NAME": "mat_animspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN" : 0.0, "MAX" : 3.0, "DEFAULT": 1.0 },
        { "LABEL": "Squirl", "NAME": "mat_squirl", "TYPE": "float", "MIN" : 0.0, "MAX" : 3.0, "DEFAULT": 1.0 },
        { "LABEL": "Distance", "NAME": "mat_distance", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 5.0 },
        { "NAME": "spectrum", "TYPE": "audioFFT", "SIZE": 3, "ATTACK": 0.0, "DECAY": 0.0, "RELEASE": 0.1 },
      ],
    "GENERATORS": [
        {"NAME": "anim_position", "TYPE": "time_base", "PARAMS": {"speed": "mat_animspeed","bpm_sync":true, "speed_curve": 3}},
    ]
}*/

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = 2*mat_scale*mat_scale*(texCoord-vec2(0.5));
	vec2 spec = vec2(IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r,IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r) * mat_audio_level * mat_audio_level;
	float col = 0.0;
	for (float i=0; i<30; i++) {
		uv.x += sin(i*mat_distance*mat_distance + spec.x * 30*fract(anim_position) + anim_position + uv.y*mat_squirl) * spec.y;
		col += abs(.066/uv.x) * spec.y;
	}
	return vec4(col,col,col,1);
}
