/*{
	"CREDIT": "by mojovideotech",
  "TAGS" : [
    "2d",
    "rotation",
    "circular"
  ],
  "DESCRIPTION" : "based on https:\/\/www.shadertoy.com\/view\/Mts3zM by nimitz.",
  "INPUTS" : [
  { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MAX": 4.0, "MIN": 0.0, "DEFAULT": 0.5 },
  { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
	{
		"NAME": 	"loops",
		"TYPE": 	"float",
		"DEFAULT":	16.0,
		"MIN": 		6.0,
		"MAX": 		60.0
	},
	{
      "NAME": "mirror",
      "TYPE": "bool",
      "DEFAULT": "TRUE"
    },
    {
      "NAME": "rotate",
      "TYPE": "bool",
      "DEFAULT": "TRUE"
    },
    {
      "NAME": "offset",
      "TYPE": "bool",
      "DEFAULT": "TRUE"
    },
    {
      "NAME": "noise",
      "TYPE": "bool",
      "DEFAULT": "FALSE"
    }
  ],
    "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "bpm_sync": false, "speed_curve":2, "link_speed_to_global_bpm":true}}
    ]
}
*/

////////////////////////////////////////////////////////////
// SatisSpiraling  by mojovideotech
//
// based on :
// Overly satisfying  by nimitz
// shadertoy.com\/view\/Mts3zM
//
// Creative Commons Attribution-NonCommercial-ShareAlike 3.0
////////////////////////////////////////////////////////////


#define 	pi   	3.141592653589793 	// pi
#define 	PALETTE vec3(.0, 1.4, 2.)+1.5


float w = 1;
float T = mat_animation_time;

mat2 mm2(in float a){float c = cos(a), s = sin(a);return mat2(c,-s,s,c);}

float tri(in float x){return abs(fract(x)-.5);}

vec2 tri2(in vec2 p){return vec2(tri(p.x+tri(p.y*2.)),tri(p.y+tri(p.x*2.)));}

mat2 m2 = mat2( 0.970,  0.242, -0.242,  0.970 );

float triangleNoise(in vec2 p) {
    float z=1.5;
    float z2=1.5;
	float rz = 0.;
    vec2 bp = p;
	for (float i=0.; i<=3.; i++ ) {
        vec2 dg = tri2(bp*2.)*.8;
        dg *= mm2(T*.3);
        p += dg/z2;
        bp *= 1.6;
        z2 *= .6;
		z *= 1.8;
		p *= 1.2;
        p*= m2;
        rz+= (tri(p.x+tri(p.y)))/z;
	}
	return rz;
}

vec4 materialColorForPixel(vec2 texCoord) {
 	vec2 p = texCoord*2.-1.;
    p*= 1.05;
    vec2 bp = p;
   	if (rotate) p *= mm2(T*.25);
    float lp = length(p);
    float id = floor(lp*loops+.5)/loops;
    if (offset) p *= mm2(id*11.);
    if (mirror) p.y = abs(p.y);
    vec2 plr = vec2(lp, atan(p.y, p.x));
    float rz = 1.-pow(abs(sin(plr.x*pi*loops))*1.25/pow(w,0.25),2.5);
    float enp = plr.y+sin((T+id*5.5))*1.52-1.5;
    rz *= smoothstep(0., 0.05, enp);
    rz *= smoothstep(0.,.022*w/plr.x, enp)*step(id,1.);
    if (mirror) rz *= smoothstep(-0.01,.02*w/plr.x,pi-plr.y);
    if (noise) rz *= (triangleNoise(p/(w*w))*0.9+0.4);
    vec3 col = (sin(PALETTE+id*5.+T)*0.5+0.5)*rz;
   	if (noise) col += smoothstep(.4,1.,rz)*0.15, col *= smoothstep(.2,1.,rz)+1.;
	else	col *= smoothstep(.8,1.15,rz)*.7+.8;
  
	return vec4(col,1.0);
}
