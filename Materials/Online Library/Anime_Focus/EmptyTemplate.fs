/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "shadertoy WsGcRt\nby treize",
    "DESCRIPTION": "anime style focus lines",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
	{"LABEL": "Center", "NAME": "mat_center", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
	{"LABEL": "A", "NAME": "mat_a", "TYPE": "float", "MIN": 0.0, "MAX": 50.0, "DEFAULT": 2.0 }, 
	{"LABEL": "B", "NAME": "mat_b", "TYPE": "float", "MIN": 0.0, "MAX": 50.0, "DEFAULT": 20.0 }, 
    ],
	"GENERATORS": [
    {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/


float focusLine(vec2 uv,float a,float b)
{
    vec2 p=uv;
    float r = length(p);
 	r = mat_center * r - mat_center;
 	float aa = atan(uv.y, uv.x);
 	aa = abs(cos(a * aa + mat_time) + sin(b * aa + mat_time));
 	float d = aa - r;
 	return smoothstep(0.1, 0.4,clamp(d,0.0,1.0));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2. -1;

 	float f = focusLine(uv,mat_a,mat_b);

	vec3 col = vec3(1.);
	col *= f;
	
	return vec4(col,1.);
}