/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz cooked it up from the net",
    "DESCRIPTION": "Noise series: 3 tap highlighted voronoi",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.9 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": -0.3 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.7 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec2 hash22(vec2 p) { 
    float n = sin(dot(p,vec2(1, 113))); 
    p = fract(vec2(8.*n, n)*262144.);
   return sin(p*6.2831853 + mat_animation_time*2.); 
}

float Voronoi3Tap(vec2 p){
    
	// Simplex grid stuff.
    //
    vec2 s = floor(p + (p.x + p.y)*.3660254); // Skew the current point.
    p -= s - (s.x + s.y)*.2113249; // Use it to attain the vector to the base vertice (from p).

    // Determine which triangle we're in -- Much easier to visualize than the 3D version. :)
    // The following is equivalent to "float i = step(p.y, p.x)," but slightly faster, I hear.
    float i = p.x<p.y? 0. : 1.;
    
    
    // Vectors to the other two triangle vertices.
    vec2 p1 = p - vec2(i, 1. - i) + .2113249, p2 = p - .5773502; 

    // Add some random gradient offsets to the three vectors above.
    p += hash22(s)*.125;
    p1 += hash22(s +  vec2(i, 1. - i))*.125;
    p2 += hash22(s + 1.)*.125;
    
    // Determine the minimum Euclidean distance. You could try other distance metrics, 
    // if you wanted.
    float d = max(max(dot(p, p), dot(p1, p1)), dot(p2, p2))/.8425;
   
    // That's all there is to it.
    return sqrt(d); 

}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-mat_offset.x,mat_offset.y);
	
//	uv += sin(uv*10.+mat_animation_time)*0.03;
	
    // Take two 3-tap Voronoi samples near one another.
    float c = Voronoi3Tap(uv*5.);
	float c2 = Voronoi3Tap(uv*5. - 10./1024.);

   	vec3 color = vec3(c);
	color += 2.*vec3(.5, .8, 1)*(c2*c2*c2 - c*c*c);
    color += (length(hash22(uv + mat_animation_time))*.06 - .03)*vec3(1, .5, 0);
	color = color.ggg;
  
	
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
