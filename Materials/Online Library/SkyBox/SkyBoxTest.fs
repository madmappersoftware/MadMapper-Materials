/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Pan Tilt around a spherical map.\nReplace the included free texture by your own.",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Fov", "NAME": "mat_fov", "TYPE": "float", "MIN": 0.5, "MAX": 4.0, "DEFAULT": 1. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0., 1. ] },
      ],

	"IMPORTED":
	[
		{ "NAME": "texture_graff", "PATH": "graffiti.jpg", 
		"GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
	]
}*/

// texture from : https://hdrihaven.com/hdri/?h=graffiti_shelter

const float mat_PI = 3.14159265359;

flat in vec3 ro; // ray origin
in vec3 rd; // ray direction

vec4 materialColorForPixel (vec2 texCoord)
{
	vec3 nrd = normalize(rd);

	float theta = acos(nrd.y) / -mat_PI;
	float phi = atan( -nrd.z, nrd.x) / -mat_PI * 0.5f;
	vec3 col = texture(texture_graff,vec2(phi,theta)).rgb;

	return vec4(col, 1.0);
}