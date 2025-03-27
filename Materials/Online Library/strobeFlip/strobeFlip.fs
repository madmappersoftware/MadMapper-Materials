/*{
	    "RESOURCE_TYPE": "Material For MadMapper",
	"CREDIT": "ameisso",
	"DESCRIPTION": "flip",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [
	{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 5.0 },
	{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
	{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" },
	{ "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },

],
	"GENERATORS": [
		{
			"NAME": "animation_time",
			"TYPE": "time_base",
			"PARAMS": {"speed": "speed", "bpm_sync": "bpmsync", "speed_curve": 10, "reverse": "reverse", "link_speed_to_global_bpm":true}
		}
	],
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
	if (int(animation_time*mat_speed) % 2 != 0)
	{
		if (texCoord.x > 0.5)
		{
			return mat_backgroundColor;
		}
		else
		{
			return mat_foregroundColor;
		}
	}
	else
	{
		if (texCoord.x > 0.5)
		{
			return mat_foregroundColor;
		}
		else
		{
			return mat_backgroundColor;
		}
	}
}