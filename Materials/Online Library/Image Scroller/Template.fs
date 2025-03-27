/*{
    "NAME": "Image Scroller",
    "AUTHOR": "Pi & Mash",
    "CREDIT": "Pi & Mash",
    "DESCRIPTION": "A simple repeating image scroller.  YOu can change the images in your local folder if required (Documents\MadMapper\Materials\Image Scroller)",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "Label": "Image", "NAME": "mat_img","TYPE": "long", "DEFAULT": "Image 1", "VALUES": [ "Image 1", "Image 2", "Image 3", "Image 4", "Image 5", "Image 6" ] },
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
        { "LABEL": "Direction", "NAME": "mat_direction", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture0", "PATH": "image1.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "mat_some_texture1", "PATH": "image2.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "mat_some_texture2", "PATH": "image3.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "mat_some_texture3", "PATH": "image4.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "mat_some_texture4", "PATH": "image5.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "mat_some_texture5", "PATH": "image6.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	float x = mat_direction.x;
	if(mat_direction.x == 0) x = 0.1;
	float y = mat_direction.y;
	if(mat_direction.y == 0) y = 0.1;
	uv += vec2(-x * mat_animation_time, y * mat_animation_time);
	
	
	// You can also import static image textures,
	// if the file exists in your material folder and an "IMPORTED" section is declared.
	// Here we use an hypothetic imported texture named "mat_some_texture"
	// whose file is "flare.jpg" located in your material folder on your drive
	// at "~/Documents/MadMapper/Materials/template"	
	vec3 tex = texture(mat_some_texture0,uv).rgb;
	
	if(mat_img == 1) tex = texture(mat_some_texture1,uv).rgb;
	if(mat_img == 2) tex = texture(mat_some_texture2,uv).rgb;
	if(mat_img == 3) tex = texture(mat_some_texture3,uv).rgb;
	if(mat_img == 4) tex = texture(mat_some_texture4,uv).rgb;
	if(mat_img == 5) tex = texture(mat_some_texture5,uv).rgb;
		
	// Apply contrast
    tex = mix(vec3(0.5), tex, mat_contrast);
    // Apply brightness
    tex += vec3(mat_brightness);
	// Apply Tint
	tex *= mat_tint.rgb;
	// Apply Invert
	tex = mix(tex, vec3(1.0)-tex,float(mat_invert));
		
	vec4 final_tex = vec4(tex,1.0);	
	return final_tex;
}
