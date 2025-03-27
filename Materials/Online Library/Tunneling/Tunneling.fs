/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "graphical tunnel",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Loop", "NAME": "mat_loop", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
	"IMPORTED": [
    	{"NAME": "mat_tex", "PATH": "zebra.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 p = texCoord * 2. -1.;
	p *= 4.*mat_scale;
    
    float a = atan( p.y, p.x );
    float r = sqrt( dot(p,p) );
    float t = mat_time * 0.2;
    a += sin(0.5*r-0.5*t );

   	vec2 uv;
    uv.x = t + 1.0/(r );
    uv.y = mat_loop*a/3.14159265359;

	vec3 col = texture(mat_tex,uv).rgb;
	col *= r*0.2;

	return vec4(col,1.);
}