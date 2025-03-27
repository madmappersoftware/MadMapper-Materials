/*{
    "CREDIT": "frz / 1024\nadapted from shadertoy lsXXzN\nby Fabrice Neyret",
    "DESCRIPTION": "Digital Counter",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.15, -0.48 ] },
		{ "LABEL": "Number", "NAME": "mat_number", "TYPE": "float", "MIN": 0, "MAX": 99.999, "DEFAULT": 1. }, 
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
}*/

// by fabrice neyret
// cf new version here: https://www.shadertoy.com/view/MdKGRw
float segment(vec2 uv, bool On) {
	return (On) ?  (1.-smoothstep(0.08,0.09+float(On)*0.02,abs(uv.x)))*
			       (1.-smoothstep(0.46,0.47+float(On)*0.02,abs(uv.y)+abs(uv.x)))
		        : 0.;
}

float digit(vec2 uv,int num) {
	float seg= 0.;
    seg += segment(uv.yx+vec2(-1., 0.),num!=-1 && num!=1 && num!=4                    );
	seg += segment(uv.xy+vec2(-.5,-.5),num!=-1 && num!=1 && num!=2 && num!=3 && num!=7);
	seg += segment(uv.xy+vec2( .5,-.5),num!=-1 && num!=5 && num!=6                    );
   	seg += segment(uv.yx+vec2( 0., 0.),num!=-1 && num!=0 && num!=1 && num!=7          );
	seg += segment(uv.xy+vec2(-.5, .5),num==0 || num==2 || num==6 || num==8           );
	seg += segment(uv.xy+vec2( .5, .5),num!=-1 && num!=2                              );
    seg += segment(uv.yx+vec2( 1., 0.),num!=-1 && num!=1 && num!=4 && num!=7          );	
	return seg;
}

float showNum(vec2 uv,int nr, bool zeroTrim) { // nr: 2 digits + sgn . zeroTrim: trim leading "0"
	if (abs(uv.x)>2.*1.5 || abs(uv.y)>1.2) return 0.;

	if (nr<0) {
		nr = -nr;
		if (uv.x>1.5) {
			uv.x -= 2.;
			return segment(uv.yx,true); // minus sign.
		}
	}
	
	if (uv.x>0.) {
		nr /= 10; if (nr==0 && zeroTrim) nr = -1;
		uv -= vec2(.75,0.);
	} else {
		uv += vec2(.75,0.); 
		nr = int(mod(float(nr),10.));
	}

	return digit(uv,nr);
}

float dots(vec2 uv, int dot) { // dot: bit 0 = bottom dot; bit 1 = top dot
	float point0 = float(dot/2),
		  point1 = float(dot)-2.*point0; 
	uv.y -= .5;	float l0 = 1.-point0+length(uv); if (l0<.13) return (1.-smoothstep(.11,.13,l0));
	uv.y += 1.;	float l1 = 1.-point1+length(uv); if (l1<.13) return (1.-smoothstep(.11,.13,l1));
	return 0.;
}
//    ... end of digits adapted from Andre

#define STEPX .875
#define STEPY 1.5
float _offset=0.; // auto-increment useful for successive "display" call

// 2digit int + sign
bool display(vec2 pos, float scale, float offset, int number, int dot, vec2 uv) { // dot: draw separator

	uv = (uv-pos)/scale*2.; 
    uv.x = .5-uv.x + STEPX*offset;
	uv.y -= 1.;
	
	float seg = showNum(uv,number,false);
	offset += 2.;
	
	if (dot>0) {
		uv.x += STEPX*offset; 
		offset += 2.;
	}

//	FragColor += vec4(seg);  // change color here
	_offset = offset;
	return (seg>0.);
}

// 2.2 float + sign
bool display(vec2 pos, float scale, float offset, float val,vec2 uv) { // dot: draw separator
	if (display( pos, scale, 0., int(val), 1,uv)) return true;
    if (display( pos, scale, _offset, int(fract(abs(val))*100.), 0,uv)) return true;
	return false;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= 2.;
	uv += vec2(-mat_offset.x,mat_offset.y-1.);
	uv.y *= -1;

	// display counters
	vec2 pos ; 
	float scale = 0.5;
	vec3 color = vec3( 0.);
	
	pos = vec2(0.3,0.25);   
	if (display( pos, scale, 0., (mat_number),uv)) { color = vec3(1.); }

	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}