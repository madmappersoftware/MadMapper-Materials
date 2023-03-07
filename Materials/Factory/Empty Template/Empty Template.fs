/*{
    "CREDIT": "Do something cool",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

vec4 materialColorForPixel( vec2 texCoord )
{
	return vec4(0.,texCoord,1.);
}