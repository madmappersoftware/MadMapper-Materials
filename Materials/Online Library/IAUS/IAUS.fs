/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "A port of the magic formula from Infinite Axis Utility System",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "power", "NAME": "p", "TYPE": "float", "MIN": -1.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "slope", "NAME": "m", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "exponent", "NAME": "k", "TYPE": "float", "MIN": -3.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "h_shift", "NAME": "c", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "v_shift", "NAME": "b", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 }, 	
      ],

}*/

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	
	//// flip vertical axis b/c opengl is reversed
	uv.y = 1.-uv.y;
	uv.x = pow(uv.x,p);
	
	float val = m * pow( (uv.x - c), k) + b;	
	/// alt version
	//float val2 = k * (1./ (1. + ( pow(1000.,pow(m,-1*uv.x + c))))) + b;
	
	float O = 1. - step(val,uv.y);

	// make a color out of it
	vec3 color = vec3(O);
	
	/// color background
	color.g = uv.y;
	
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
