/*{
    "CREDIT": "shadertoy XstyD8 + frz",
    "DESCRIPTION": "Sphere Lit, ambiant occlusion style",
    "TAGS": "AO",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Radius", "NAME": "mat_radius", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
}*/
	
#define unsigned(t) ((t + 1.0) / 2.0) // in = [-1,1] out = [ 0,1]
#define   signed(t) (2.0 * t - 1.0)   // in = [ 0,1] out = [-1,1]
#define   invert(t) (1.0 - t)
#define map(v)  (v - middle) / radius // Map the vector to the sphere

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	
	float radius = mat_radius*1024.; // Radius is 1/3 of the smallest canvas dimension
    vec2  middle = vec2(512.), // Middle of the canvas
      mousePixel = map(mat_offset.xy*512.), // Mapped to sphere-relative coordinates
      drawnPixel = map(uv * 1024.); // Mapped to sphere-relative coordinates
	
	float      z = 3.0 * sqrt(abs(invert(dot(drawnPixel, drawnPixel))));	
	vec4 col = vec4(unsigned((dot(mousePixel, drawnPixel) + z) / (1.0 + length(mousePixel))));
	
	// make a color out of it
	vec3 color = col.rgb;
	
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
