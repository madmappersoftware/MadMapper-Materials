/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Stroboscope - with BPM sync",
    "VSN": "1.0",
    "TAGS": "graphic",
	"INPUTS": [
        { "Label": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "Label": "Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Decay", "NAME": "decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Release", "NAME": "release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Out","In","Smooth"], "DEFAULT": "Out" },
        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
	],
    "GENERATORS": [
        {
            "NAME": "strob_position",
            "TYPE": "time_base",
            "PARAMS": {"speed": "mat_speed", "strob": 0, "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true}
        }
    ]
}*/

in float adjusted_bpm_pos;

vec4 materialColorForPixel(vec2 texCoord)
{
	vec4 backColor = vec4(backgroundColor.rgb * brightness, backgroundColor.a);
	vec4 frontColor = vec4(foregroundColor.rgb * brightness, foregroundColor.a);
	return mix(backColor,frontColor,adjusted_bpm_pos);
}

