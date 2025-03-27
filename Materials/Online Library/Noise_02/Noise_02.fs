/*{
    "CREDIT": "frz cooked it up from the net",
    "DESCRIPTION": "Noise series: rounded voronoi",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 10.0, "DEFAULT": 4.0 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		
		{ "Label": "Extra Shape", "NAME": "mat_shape","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Circle" ] },
		
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.3 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.6 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 1, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"
	
vec2 hash22(vec2 p) { 

    float n = sin(dot(p, vec2(41, 289)));
    p = fract(vec2(262144, 32768)*n); 
    return sin( p*6.2831853 + mat_animation_time )*.25 + .5;     
}

float Voronoi(in vec2 p){
    
	vec2 g = floor(p), o; p -= g; // Cell ID, offset variable, and relative cell postion.
	vec3 d = vec3(1); // 1.4, etc. "d.z" holds the distance comparison value.
    
	for(int y=-1; y<=1; y++){
		for(int x=-1; x<=1; x++){
            
			o = vec2(x, y); // Grid cell ID offset.
            o += hash22(g + o) - p; // Random offset.
			
            // Regular squared Euclidean distance.
            d.z = dot(o, o); 
            // Adding some radial variance as we sweep around the circle. It's an old
            // trick to draw flowers and so forth. Three petals is reminiscent of a
            // triangle, which translates roughly to a blocky appearance.
            d.z *= cos(atan(o.y, o.x)*3. - 3.14159/2.)*.333 + .667;
            //d.z *= (1. -  tri(atan(o.y, o.x)*3./6.283 + .25)*.5); // More linear looking.
            
            d.y = max(d.x, min(d.y, d.z)); // Second order distance.
            d.x = min(d.x, d.z); // First order distance.                    
		}
	}

    return d.y*.5 + (d.y-d.x)*.5; // Range: [0, 1]   
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	
 	float m = Voronoi(uv);	
	// make a color out of it
	vec3 color = vec3(m);
	
	
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
