/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "humanaute",
    "DESCRIPTION": "Add texture to control Depth Map And shadows",
    "TAGS": "depth map Fx",
    "VSN": "1.0",
    "INPUTS": [ 
		//{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL":"Image","NAME":"input_image","TYPE":"image", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
		//{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.001, "MAX": 5., "DEFAULT": 2. }, 
		//{"LABEL": "Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": -5., "MAX": 5., "DEFAULT": 0. }, 
		{"LABEL": "Fx", "NAME": "mat_fx", "TYPE": "float", "MIN": 0., "MAX": 1., "DEFAULT": 0. }, 
		
		//{"LABEL": "Move","NAME": "mat_move", "TYPE": "point2D", "DEFAULT":[0.,0.],"MIN":[-4.0,-4.0],"MAX":[4.,4.]},   
		{"LABEL":"Color/Background Color","NAME": "mat_backgroundColor","TYPE": "color","DEFAULT": [ 0.54, 0.93, 0.95,1. ]},
		{"LABEL": "Color/BackGround Disable", "NAME": "mat_background_disable",  "TYPE": "bool", "DEFAULT": false, "FLAGS":"button"  }, 
		{"LABEL":"Color/object Color","NAME": "mat_objectColor","TYPE": "color","DEFAULT": [ 1.0, 0.40, 0.22,1.]},
		{"LABEL": "Lit Position/Pos X", "NAME": "mat_lit_pos_x", "TYPE": "float", "MIN": -10., "MAX": 10., "DEFAULT": 0. }, 
		{"LABEL": "Lit Position/Pos Y", "NAME": "mat_lit_pos_y", "TYPE": "float", "MIN": -10., "MAX": 10., "DEFAULT": 0. }, 
		{"LABEL": "Lit Position/Pos Z", "NAME": "mat_lit_pos_z", "TYPE": "float", "MIN": -5., "MAX": 5., "DEFAULT": 0. }, 
		//{"LABEL": "Rotation/Rot X", "NAME": "mat_rot_x", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0. },
		//{"LABEL": "Rotation/Rot Y", "NAME": "mat_rot_y", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0. },
		//{"LABEL": "Rotation/Rot Z", "NAME": "mat_rot_z", "TYPE": "float", "MIN": 0., "MAX": 2., "DEFAULT": 0. },

],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
		
    ],
	"IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "text test.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

float time = mat_time;
//vec2 mouse = vec2 (mat_move);
vec2 resolution = vec2(1.);

#define PI 3.141592653589793

const int MAX_MARCHING_STEPS = 255;
const float MIN_DIST = 0.0;
const float MAX_DIST = 100.;
const float PRECISION = 0.001;
const float EPSILON = 0.0005;

#include "MadNoise.glsl"

//rotation matrix around X axis
mat3 rotateX(float theta)
{
	float c = cos(theta);
	float s = sin(theta);
	return mat3(
		vec3(1.,0.,0.),
		vec3(0.,c,-s),
		vec3(0.,s,c)
	);
}
//rotation matrix around Y axis
mat3 rotateY(float theta)
{
	float c = cos(theta);
	float s = sin(theta);
	return mat3(
		vec3(c,0.,s),
		vec3(0.,1.,0.),
		vec3(-s,0.,c)
	);
}
//rotation matrix around Z axis
mat3 rotateZ(float theta)
{
	float c = cos(theta);
	float s = sin(theta);
	return mat3(
		vec3(c,-s,0.),
		vec3(s,c,0.),
		vec3(0.,0.,1.)
	);
}
// identity Matrix
mat3 identity()
{
	return mat3(
		vec3(1.,0.,0.),
		vec3(0.,1.,0.),
		vec3(0.,0.,1.)
	);
}
struct Surface
{
	float sd; //signed distance value
	vec3 col ;//color
};

float imageCol(vec4 color)
{
	return (color.r + color.g + color.b);
}
float mat_noise( in vec2 x )
{
	return texture(mat_some_texture, x*.01).x;
}

Surface sdFloor(vec3 p,vec3 col,mat3 transform)
{
	p = p * transform;
	p.x += 3.;
	p.y += 3;
	vec4 srcPixel = IMG_NORM_PIXEL(input_image,p.xy*.165);
	float n = imageCol(srcPixel);

	
	//float n = mat_noise(p.xy*25.+mat_offset*10.);
	//n += fBm(p.xy*.2+mat_offset)*fBm((1-p.xy)*2.+mat_offset);
	//n += .5+ .5*sin(p.x*2.)*cos(mat_time*2.+.5*p.x); //function derform
	float d = (4. + p.z) - n * mat_fx;
	
	d *= .3; // supprime artefacts !!!
	return Surface(d,col);
}
Surface sdFloor1(vec3 p,vec3 col)
{
	p.y += 0.0;
	//float n = mat_noise(p.xy*25.+mat_offset*10.);
	//n += noise(p.xy*.5+mat_offset)*fBm((1-p.xy)*2.+mat_offset);
	 //n += .5+ .5*sin(p.x*2.)*cos(mat_time*2.+.5*p.x); //function derform
	float d = (3.99 + p.z) ;
	
	//d *= .5; // supprime artefacts !!!
	return Surface(d,col);
}
Surface opUnion(Surface obj1,Surface obj2)
{
	if (obj2.sd < obj1.sd) return obj2;
	return obj1;
}
Surface scene(vec3 p)
{
	vec3 floorColor = vec3(mat_objectColor);
	if(mat_background_disable)
	{
	vec3 floorColor = vec3(mat_objectColor);
	Surface co = sdFloor(p-vec3(0.,0.,0.),floorColor,identity());
	return co;
	}
	else
	{
	vec3 floorColor = vec3(mat_objectColor);
	Surface co = sdFloor(p,floorColor,rotateX(0.));
	co = opUnion(co,sdFloor1(p,vec3(mat_backgroundColor)));
	return co;	
	}
}
Surface rayMarch(vec3 ro,vec3 rd)
{
	float depth = MIN_DIST;
	Surface co; //closest object
		
	for (int i = 0; i < MAX_MARCHING_STEPS ; i++)
	{
		vec3 p = ro + depth * rd;
		co =  scene(p);
		depth += co.sd;
		if (co.sd < PRECISION || depth > MAX_DIST) break;
	}
		co.sd = depth;

		return co;
}
vec3 calcNormal(vec3 p)
{
	vec2 e = vec2(1.,-1.)*EPSILON ;  //epsilon
	
	return normalize(
		e.xyy * scene(p + e.xyy).sd +
		e.yyx * scene(p + e.yyx).sd +
		e.yxy * scene(p + e.yxy).sd +
		e.xxx * scene(p + e.xxx).sd);
}
float softShadow(vec3 ro,vec3 rd,float mint,float tmax)
{
	float res = 1.;
	float t = mint;

	for(int i = 0; i < 16; i++)
		{
			float h = scene(ro + rd * t).sd;
			res = min(res,8.0*h/t);
			t += clamp(h,0.02,0.10);
			if (h < 0.001 || t > tmax) break;
		}
	return clamp(res,0.,1.);
}
vec4 materialColorForPixel( vec2 texCoord )
{
		
		vec2 uv = vec2 (texCoord);
		float n = fBm(uv);
		uv -= .5;
		vec3 backgroundColor = vec3(mat_backgroundColor);
		vec3 col = vec3(0.);

		vec3 ro = vec3(0.,0.,2.); // ray origin represents camera position

		vec3 rd = normalize(vec3(uv,-1.)); //ray direction
		
		Surface co = rayMarch(ro,rd); //distance to sphere

		if (co.sd > MAX_DIST)
			{
				col = backgroundColor; // ray didn't hit anything
			}
		else
			{
				vec3 p = ro + rd * co.sd; //point on sphere we discovered from ray marching
				vec3 normal = calcNormal(p);

				//light
				vec3 lightPosition = vec3(mat_lit_pos_x,-mat_lit_pos_y,mat_lit_pos_z);
				vec3 lightDirection = normalize(lightPosition - p);
						
				
				float dif = clamp(dot(normal,lightDirection),0.,1.) +.5; //diffuse reflection clamp between zero and one
				
				float softShadow = clamp(softShadow(p,lightDirection,0.02,2.5),0.1,1.);
							

				col = dif * co.col * softShadow;
			}

		
		//col = mix(col,backgroundColor,1. - exp(-0.0002 * co.sd * co.sd * co.sd)); //fog
		//col = pow(col,vec3(1./2.2)); // gamma correction
 
		return vec4(col,1.);
}