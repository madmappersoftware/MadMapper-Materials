// SaturdayShader Week 23 : Discspin
// by Joseph Fiola (http://www.joefiola.com)
// 2016-01-23

// Based on "The Power of Sin" by antonOTI - https://www.shadertoy.com/view/XdlSzB



/*{
  "CREDIT": "Joseph Fiola",
  "DESCRIPTION": "Based on 'The Power of Sin' by antonOTI - https://www.shadertoy.com/view/XdlSzB",
  "CATEGORIES": [
    "Generator"
  ],
  "INPUTS": [
    {
      "NAME": "mirror",
      "TYPE": "bool"
    },
    {
      "NAME": "pattern",
      "TYPE": "bool",
      "DEFAULT": true
    },
    {
      "NAME": "iteration",
      "TYPE": "float",
      "DEFAULT": 35,
      "MIN": 0,
      "MAX": 35
    },
    { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MAX": 4.0, "MIN": 0.0, "DEFAULT": 1 },
    { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
    {
      "NAME": "radius",
      "TYPE": "float",
      "DEFAULT": 0.8,
      "MIN": 0,
      "MAX": 2
    },
    {
      "NAME": "centerRadius",
      "TYPE": "float",
      "DEFAULT": 1,
      "MIN": -1,
      "MAX": 0
    },
    {
      "NAME": "lineThickness",
      "TYPE": "float",
      "DEFAULT": 0.07,
      "MIN": 0.01,
      "MAX": 1
    },
    {
      "NAME": "smoothEdge",
      "TYPE": "float",
      "DEFAULT": 0.03,
      "MIN": 0.01,
      "MAX": 1
    },
    {
      "NAME": "yOffset",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": -1,
      "MAX": 0
    },
    {
      "NAME": "xOffset",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": -1,
      "MAX": 0
    },
    {
      "NAME": "startValue",
      "TYPE": "float",
      "DEFAULT": -1.5,
      "MIN": -2,
      "MAX": 2
    },
    {
      "NAME": "endValue",
      "TYPE": "float",
      "DEFAULT": 1.5,
      "MIN": -2,
      "MAX": 2
    },
    {
      "NAME": "rotate",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": -1,
      "MAX": 1
    },
    {
      "NAME": "pos",
      "TYPE": "point2D",
      "DEFAULT": [
        0.5,
        0.5
      ],
      "MIN": [
        0,
        0
      ],
      "MAX": [
        1,
        1
      ]
    }
  ],
  "GENERATORS": [
    {"NAME": "mat_anim_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "bpm_sync": false, "speed_curve":3, "link_speed_to_global_bpm":true}}
  ]
}*/


#define NB 35.
#define MODE1
#define PI 3.14159265358979323846

float circle(vec2 center , float radius,float thickness,float la,float ha)
{
	float f = length(center);
	
	float a = atan(center.y,center.x) ;
	return(smoothstep(f,f+0.01,radius) * smoothstep(radius - thickness,radius - thickness+0.01,f) * step(la,a) *smoothstep(a-smoothEdge,a+smoothEdge,ha));
}

float cable(vec2 p,float dx,float dy,float r,float thick,float la,float ha)
{
	p.x-= dx;
	p.y -= dy;
	return (circle(p,r,thick,la,ha));
}

//rotation function
vec2 rot(vec2 uv,float a){
	return vec2(uv.x*cos(a)-uv.y*sin(a),uv.y*cos(a)+uv.x*sin(a));
}

vec4 materialColorForPixel(vec2 texCoord)
{
	
	vec2 uv = texCoord;
	uv -= vec2(pos - 0.5);

	vec2 p = -1. + 2. * uv;
	
	p=rot(p,rotate * PI);

	
	
	vec2 ap = vec2(0.0);
	if (mirror){
		ap = p * vec2(1.,-1.);
	} else {
		ap = p * vec2(-1.,-1.);
	}



	float f = 0.;
	for(float i = 0.; i < NB; ++i)
	{
		if (i > iteration) break;
		
		if (pattern) f *= -1.; // invert values every iteration to create interesting patterns when line thickness overlaps
		
		float divi = i/iteration * centerRadius;
		f += cable(p,xOffset,yOffset,radius - divi,lineThickness,0.,(sin(mat_anim_time - divi*5.)*startValue+endValue) * 3.14);
		f += cable(ap,xOffset,yOffset,radius - divi,lineThickness,0.,(sin(mat_anim_time - divi*5.)*startValue+endValue) * 3.14);
	}
	vec3 col = mix(vec3(0.,0.,0.),vec3(1.,1.,1.),f);
	
	return vec4(col,1.0);
}

