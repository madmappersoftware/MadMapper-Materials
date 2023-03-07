/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Tutorial about using a Attack-Decay-Release Filter - on audio amplitude actually, but it could be done an INPUT parameter.",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "LABEL": "Audio In Level",
            "NAME": "mat_audio_in_level",
            "TYPE": "float",
            "DEFAULT": 2.0,
            "MIN": 0.0,
            "MAX": 4.0
        },
        {
            "LABEL": "Attack",
            "NAME": "mat_attack",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Decay",
            "NAME": "mat_decay",
            "TYPE": "float",
            "DEFAULT": 0.4,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Release",
            "NAME": "mat_release",
            "TYPE": "float",
            "DEFAULT": 0.6,
            "MIN": 0.0,
            "MAX": 1.0
        },
    ],
	"GENERATORS": [
        {
            "NAME": "mat_audio_amplitude",
            "TYPE": "pass_thru",
            "PARAMS": {"input_value":"/audioin/MadMapper/amplitude"}
        },
        {
            "NAME": "mat_audio_amplitude_decay",
            "TYPE": "adsr",
            "PARAMS": {"input_value":"mat_audio_amplitude","attack":"mat_attack","decay":"mat_decay","release":"mat_release"}
        }
	]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    float dist = (1-mat_audio_amplitude_decay*mat_audio_in_level - texCoord.y) * 30;
    float value = 1-pow(dist,3);
    return vec4(value*2,value,value,1);
}
