/*{
    "CREDIT": "extracted from shadertoy",
    "DESCRIPTION": "Rotating noise pattern",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Color/Color A", "NAME": "mat_cA", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.27, 1.0 ] ,"FLAGS":"no_alpha"},
		{ "LABEL": "Color/Color B", "NAME": "mat_cB", "TYPE": "color", "DEFAULT": [ 0.86, 1.0, 0.0, 1.0 ] ,"FLAGS":"no_alpha"},
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
	"IMPORTED": [
        {"NAME": "mat_tex_noise", "PATH": "noise.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
		{"NAME": "mat_tex_panther", "PATH": "panther.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#define fma(a,b,c) (a*b+c)
vec2 rotate( in vec2 p, in float angle )
{
	vec2 cossin = vec2(cos(angle), sin(angle));
	return( vec2(fma(p.x, cossin.x, p.y * cossin.y), fma(p.x, -cossin.y, p.y * cossin.x)) );
}

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = texCoord *2.-1.;
	uv *= mat_scale;

    vec2 p = rotate(uv, texture(mat_tex_noise, uv).r - 0.5f) * 0.1f;
    float tex = texture(mat_tex_panther, rotate((uv + p) * 0.125f, mat_time * 0.1f), 0.0f).r;
    
    vec3 T = vec3(uv, abs(sin(tex * 3.14 + mat_time)));
  	vec3 G = exp2(-1.618*T*T);         
    float N = G.x*G.y*G.z;
	float C = (1.0f - N) * N;
	C = pow(C + 0.7,3.2);
	
	vec3 col = mix(mat_cA.rgb,mat_cB.rgb, vec3(C));
	return vec4(col,1.);
}