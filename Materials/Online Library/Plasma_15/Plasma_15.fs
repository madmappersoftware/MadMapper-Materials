/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "plum plasmatronic",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 }, 
	{"LABEL": "Character", "NAME": "mat_char", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 

	{"LABEL": "Color/Overshoot", "NAME": "mat_over", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
	{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.13, 0.09, 0.65, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.6, 0.0, 1.0 ], "FLAGS": "no_alpha" }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2. -1.;
    float len;
    
 	for (int i = 0; i < 3; i++) {
            
        uv.x +=  sin(uv.y + mat_time * 0.6)*(0.2 + mat_char);
        uv.y +=  cos(uv.x + mat_time * 0.8 + cos(len * 3.0))*(0.4 + mat_char);
		len = length(vec2(uv.x, uv.y)); 
    }
    
	len = mix(min(1.,max(0.,len)), len, mat_over);

	vec3 col = mix(mat_backgroundColor.rgb,mat_foregroundColor.rgb,vec3(len));
	if(mat_invert)col = 1. - col;

	return vec4(col,1.);
}