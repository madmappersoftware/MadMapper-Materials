/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Tutorial about using a Attack-Decay-Release Filter - on audio amplitude actually, but it could be done an INPUT parameter.",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "LABEL": "Attack",
            "NAME": "attack",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Decay",
            "NAME": "decay",
            "TYPE": "float",
            "DEFAULT": 0.4,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Release",
            "NAME": "release",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
    ],
	"GENERATORS": [
        {
            "NAME": "audio_amplitude",
            "TYPE": "pass_thru",
            "PARAMS": {"input_value":"/audioin/MadMapper/amplitude"}
        },
        {
            "NAME": "audio_amplitude_decay",
            "TYPE": "adsr",
            "PARAMS": {"input_value":"audio_amplitude","attack":"attack","decay":"decay","release":"release"}
        }
	]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    return vec4(vec3(audio_amplitude_decay),1);
}
