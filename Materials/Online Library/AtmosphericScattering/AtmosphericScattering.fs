/*{
    "CREDIT": "ported by frz / \noriginal shadertoy WtBXWw by bearworks",
    "DESCRIPTION": "Out-door light scattering",
    "TAGS": "physical",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Rayleigh", "NAME": "mat_rayleigh", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Rayleigh Att.", "NAME": "mat_rayleighatt", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Mie", "NAME": "mat_mie", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.15 }, 
		{ "LABEL": "Mie Att.", "NAME": "mat_mieatt", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.12 }, 		
		{ "LABEL": "Sun", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ 0.0, 0.0 ], "DEFAULT": [ 0.5, 0.3 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

#define fov tan(radians(60.0))

#define cameraheight 5e1 //50.

#define Gamma 2.2

#define Rayleigh mat_rayleigh
#define Mie mat_mie
#define RayleighAtt mat_rayleighatt
#define MieAtt mat_mieatt

//float g = -0.84;
float g = -0.97;
//float g = -0.9;

#if 1
vec3 _betaR = vec3(1.95e-2, 1.1e-1, 2.94e-1); 
vec3 _betaM = vec3(4e-2, 4e-2, 4e-2);
#else
vec3 _betaR = vec3(6.95e-2, 1.18e-1, 2.44e-1); 
vec3 _betaM = vec3(4e-2, 4e-2, 4e-2);
#endif


const float ts= (cameraheight / 2.5e5);

vec3 Ds = normalize(vec3(0., 0., -1.)); //sun 

vec3 ACESFilm( vec3 x )
{
    float tA = 2.51;
    float tB = 0.03;
    float tC = 2.43;
    float tD = 0.59;
    float tE = 0.14;
    return clamp((x*(tA*x+tB))/(x*(tC*x+tD)+tE),0.0,1.0);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	uv.y = 1-uv.y;

	float AR = 1.;//iResolution.x/iResolution.y;
    float M = 1.0; //canvas.innerWidth/M //canvas.innerHeight/M --res

	vec2 uvMouse = vec2(mat_offset.x,mat_offset.y);
   
	Ds = normalize(vec3(uvMouse.x-0.25, uvMouse.y-0.25, (fov/-2.0)));
    
	vec3 O = vec3(0., cameraheight, 0.);
	vec3 D = normalize(vec3(uv, -(fov*M)));

	// make a color out of it
	vec3 color = vec3( 0.);

   	float L1 =  O.y / D.y;
	vec3 O1 = O + D * L1;
    	vec3 D1 = vec3(1.);
    	D1 = normalize(D);


      // optical depth -> zenithAngle
      float sR = RayleighAtt / D.y ;
      float sM = MieAtt / D.y ;

  	  float cosine = clamp(dot(D,Ds),0.0,1.0);
      vec3 extinction = exp(-(_betaR * sR + _betaM * sM));

       // scattering phase
      float g2 = g * g;
      float fcos2 = cosine * cosine;
      float miePhase = Mie * pow(1. + g2 + 2. * g * cosine, -1.5) * (1. - g2) / (2. + g2);
        //g = 0;
      float rayleighPhase = Rayleigh;

      vec3 inScatter = (1. + fcos2) * vec3(rayleighPhase + _betaM / _betaR * miePhase);

      color = inScatter*(1.0-extinction); // *vec3(1.6,1.4,1.0)

       // sun
      color += 0.47*vec3(1.6,1.4,1.0)*pow( cosine, 350.0 ) * extinction;
      // sun haze
      color += 0.4*vec3(0.8,0.9,1.0)*pow( cosine, 2.0 )* extinction;
    
	  color = ACESFilm(color);
    
      color = pow(color, vec3(Gamma));
	
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
