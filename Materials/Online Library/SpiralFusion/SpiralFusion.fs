/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Henry Florence",
    "DESCRIPTION": "Based on the shader from Enttec ELM. Uses alpha channel, so can be easily layered.",
    "TAGS": "color pixelmapping",
    "VSN": "0.9",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Force", "NAME": "mat_force", "TYPE": "float", "MIN": 1.0, "MAX": 10.0, "DEFAULT": 5.0 },
		{"LABEL": "Complexity", "NAME": "mat_complexity", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 5 },
		{"LABEL": "Zoom", "NAME": "mat_zoom", "TYPE": "float", "MIN": 0.1, "MAX": 10.0, "DEFAULT": 1.0 },
		{"LABEL": "X", "NAME": "mat_x", "TYPE": "float", "MIN": -1.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Y", "NAME": "mat_y", "TYPE": "float", "MIN": -1.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Colour", "NAME": "mat_colour2", "TYPE": "color", "DEFAULT": [1.0,1.0,1.0,1.0] },
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/
#define PI 3.14159265359


vec2 rotate2D (vec2 _st, float _angle) {
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    return _st;
}

vec4 materialColorForPixel( vec2 p )
{
	p.x -= mat_x;
	p.y -= mat_y;
    p *= mat_zoom;

    float power = pow(mat_force/5.5, 3.);
    vec4 color = mat_colour2;
    float r = length(p)*1.;
    float w = 1.5;
    float time = -mat_time *1.5;
    float t = sin(r*3.0+time*PI*.35)*2.5/power;
    p *= t;
    p = rotate2D(p,(r*PI*0.8*mat_complexity-time*.6));
    color *= smoothstep(-w,.0,p.x)*smoothstep(w,.0,p.x);
    color *= distance(vec2(0.), vec2(cos(r*4.3+time*PI*0.25)));
    
	return color;
}