/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / freely lifted from mu6k",
    "DESCRIPTION": "Yet another volume plasma",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
{"LABEL": "HighQuality", "NAME": "mat_hq", "TYPE": "bool", "DEFAULT": false, "FLAGS" : "button" }, 
{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
{ "LABEL": "Color/Color_A", "NAME": "mat_A", "TYPE": "color", "DEFAULT": [ 1.0, 0.3, 0.3, 1.0 ] },
{ "LABEL": "Color/Color_B", "NAME": "mat_B", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 0.0, 1.0 ] },
{"LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
	"IMPORTED": [
{"NAME": "mat_tex", "PATH": "noise.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/


//2D texture based 4 component 1D, 2D, 3D noise
vec4 noise(float p){return texture(mat_tex,vec2(p*float(1.0/256.0),.0));}
vec4 noise(vec2 p){return texture(mat_tex,p*vec2(1.0/256.0));}
vec4 noise(vec3 p){float m = mod(p.z,1.0);float s = p.z-m; float sprev = s-1.0;if (mod(s,2.0)==1.0) { s--; sprev++; m = 1.0-m; };return mix(texture(mat_tex,p.xy*vec2(1.0/256.0) + noise(sprev).yz*21.421),texture(mat_tex,p.xy*vec2(1.0/256.0) + noise(s).yz*14.751),m);}
vec4 noise(vec4 p){float m = mod(p.w,1.0);float s = p.w-m; float sprev = s-1.0;if (mod(s,2.0)==1.0) { s--; sprev++; m = 1.0-m; };return mix(noise(p.xyz+noise(sprev).wyx*3531.123420),	noise(p.xyz+noise(s).wyx*4521.5314),	m);}

//functions that build rotation matrixes
mat2 rotate_2D(float a){float sa = sin(a); float ca = cos(a); return mat2(ca,sa,-sa,ca);}
mat3 rotate_x(float a){float sa = sin(a); float ca = cos(a); return mat3(1.,.0,.0,    .0,ca,sa,   .0,-sa,ca);}
mat3 rotate_y(float a){float sa = sin(a); float ca = cos(a); return mat3(ca,.0,sa,    .0,1.,.0,   -sa,.0,ca);}
mat3 rotate_z(float a){float sa = sin(a); float ca = cos(a); return mat3(ca,sa,.0,    -sa,ca,.0,  .0,.0,1.);}

const float toffs = -152.0;
float t;

//density function
float density(vec3 p)
{
	vec4 d = noise(p*.25)*noise(p.xz*.044)*noise(p.xy*.067)*noise(p.yz*.21);
	float fd = dot(d,vec4(1.4));
	fd = fd*fd*fd*fd*fd;
	
	return max(.0,fd);
}

vec4 materialColorForPixel( vec2 texCoord )
{

    t = mat_time*0.1 + toffs;
    
    vec2 uv = texCoord *2. -1.;
	uv *= mat_scale;
	vec2 m =mat_offset;
	//rotation matrix for the camera
	mat3 rotmat = rotate_y((t-toffs)*.07+m.x+0.2)*rotate_x((t-toffs)*.031+m.y+0.2);
	//p is ray position
	vec3 p = vec3(.0,.0,-30.0); p*=rotmat;
	p += vec3(sin(t),cos(t),sin(t*.25)*29.0+t*7.0-22.0-4.0/((t-toffs)*.01+0.01));
	//d is ray direction
	vec3 d = normalize(vec3(uv*(sin(t*.17)*.2+0.8),1.0-length(uv)*.2));
	d*=rotmat;
	
	//some accumulators
	float a = .0;
	float ai = .0;
	vec3 color = vec3(.0);


	for (int i=0; i<50 + int(mat_hq)*50; i++)
	{
		//move forward
		p+=d*.3;

		vec3 n = noise(p.xz*.25+vec2(t*.1)).xyz*12.0*noise(p.zy*.1+vec2(t*.1)).xyz;

		float de = density(p+n);
		a += de; 
		a = min(1.0,a); 
		vec4 c2 = noise(p.yz*.03).xyzw;
		c2.xyz = c2.xxx;
		vec3 c = c2.xyz*1.7;
		
		float occ = min((de-density(p+vec3(0.7+n))),1.0);

		color += max(.0,occ)*(1.0-a)*c;

		if (a>1.0) break; 
	}

	color *= 4.*pow(mat_contrast,4.);
	color = min(color,vec3(1.));
	color = mix(mat_A.rgb,mat_B.rgb,color);

	return vec4(color,1.);
}