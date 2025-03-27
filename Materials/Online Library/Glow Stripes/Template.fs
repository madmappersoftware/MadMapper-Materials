/*{
    "NAME": "Glow Stripes",
    "AUTHOR": "Pi & Mash",
    "CREDIT": "Pi & Mash",
    "DESCRIPTION": "A combination of MadTeam's Dunes and base template with extra whistles and bells",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 20.0, "DEFAULT": 10.0 }, 
		
		
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		
		{ "LABEL": "Blob Number", "NAME": "mat_blob_number", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 10 },
		{ "LABEL": "Blob Size", "NAME": "mat_blob_size", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 0.25 }, 
		{ "LABEL": "Stripe Size", "NAME": "mat_stripe_size", "TYPE": "float", "MIN": 0.01, "MAX": 0.30, "DEFAULT": 0.15 },
		
		{ "LABEL": "Noise", "NAME": "mat_noise", "TYPE": "point2D", "MAX": [ 40.0, 20.0 ], "MIN": [ 20.0, 0.0 ], "DEFAULT": [ 37.0, 17.0 ] },
		
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		
		{ "Label": "Effect", "NAME": "mat_effect2","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Billow", "Noise", "Stripes", "Clipped"] },
		{ "Label": "Color Effect", "NAME": "mat_effect","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Sin", "Cos" ] },
		
		{ "Label": "Extra Shape", "NAME": "mat_shape","TYPE": "long", "DEFAULT": "Nothing", "VALUES": [ "Nothing", "Circle" ] },
		 
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
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



vec2 hash(vec2 p)                       // rand in [-1,1]
{
  p = vec2(dot(p,vec2(127.1,311.7)),
           dot(p,vec2(269.5,183.3)));
  return -1. + 2.*fract(sin(p+20.)*53758.5453123);
}
// 2d noise functions from https://www.shadertoy.com/view/XslGRr
float mat_noise2(in vec2 x)
{
  vec2 p = floor(x);
  vec2 f = fract(x);
  f = f*f*(3.0-2.0*f);
  vec2 uv = (p+vec2(mat_noise.x,mat_noise.y)) + f;
  vec2 rg = hash( uv/256.0 ).yx;
  return 0.5*mix( rg.x, rg.y, 0.5 );
}

	
float rnd(int i, int j)
{
  return mat_noise2(vec2(i, j));
}

float Stripes (vec2 uv, float d, float freq, float time)
{
  float hv = 0.;
  for (int i=0; i<mat_blob_number; i++) 
  {
    vec2 pos = vec2(rnd(i,0), rnd(i,1));
    vec2 dir = (mat_stripe_size+d)*vec2(rnd(i,2),rnd(i,3)) - d;
    hv += mat_blob_size * sin(dot(uv-pos, freq*dir) * 6. + time);
  }
  return hv;
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;	
	
    float h = Stripes(uv, -0.05, 10, - 3.5*mat_animation_time);
	
	uv += vec2(-mat_offset.x,mat_offset.y);
	
	// use MadNoise libray
	float N = billowedNoise(vec3(uv,mat_animation_time));
	float M = worleyNoise(vec3(uv,mat_animation_time));
	float K = mix(N,M,mat_mix_noise);
	
	float bandSize = K;
	if(mat_effect2 == 1){
		bandSize = N;
	}
	if(mat_effect2 == 1){
		bandSize = M;
	}
	if(mat_effect2 == 3){
		bandSize = h;
	}
	if(mat_effect2 == 4){
		bandSize = clamp(h, 0.0, 1.0);
	}
	
	// make a color out of it
    vec3 color =  vec3( uv * bandSize,  K);
	if(mat_effect == 1) {
		color =  vec3( abs(sin(uv+ mat_animation_time))*bandSize, K);
	}
	if(mat_effect == 2) {
		color =  vec3( cos(uv+ mat_animation_time) * bandSize, K);
	}
	
	// You can also import static image textures,
	// if the file exists in your material folder and an "IMPORTED" section is declared.
	// Here we use an hypothetic imported texture named "mat_some_texture"
	// whose file is "flare.jpg" located in your material folder on your drive
	// at "~/Documents/MadMapper/Materials/template"	
	// vec3 tex = texture(mat_some_texture,uv).rgb;
	
	// extra shape using MadMapper's Signed Distance Field 2d library
	if (mat_shape==1) {
		float radius = 0.1;
		float dist = 1.0 - circle( (texCoord*2.0)-1.0, radius );
		color += vec3(dist);
	}
	

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