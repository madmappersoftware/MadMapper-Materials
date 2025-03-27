/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "shadertoy XdcyzN\nport by frz ",
    "DESCRIPTION": "fractured geometrical patterns",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Compexity", "NAME": "mat_complex", "TYPE": "float", "MIN": 1., "MAX": 100.0, "DEFAULT": 10.0 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"link_speed_to_global_bpm":true}},
    ],
}*/

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
		
	vec2 R = vec2(1024.),
         r = ( (uv*1024.) - .5 * R ) /R.y,
         q = (uv*1024.) / R-.5;
	
	float t = mat_animation_time,
          k = 1.;
	
    t+=sin(t);
	
	vec4 o = vec4(0.);
	vec3 a;
	float complex = mat_complex;
	complex = 1./complex;
    for(float i = 1.;i>0.;i-=complex) {
        a = (vec3(-.03,0.,.03)+i)*(k=-k);
        for(int x=0;x<3;++x)
            if(sin(8.*length(r-(cos(vec2(a[x]+t*.4,a[x]+t*.7))*i)))<0.)
                o[x]=1.-o[x];
            }
    o -= .1*fract(sin(dot(r*t,r+t))*1e5);
    o *= 1.-dot(q,q*1.4);

		
	// make a color out of it
	vec3 color = o.rgb;	
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
