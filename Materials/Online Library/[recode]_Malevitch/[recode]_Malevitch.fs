/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inspired by Kasimir Malevitch - Carre noir sur fond blanc / 1915",
    "TAGS": "painting",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.4 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.7 },
		{ "LABEL": "Noise Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0., "MAX": 5.0, "DEFAULT": 0.05 },
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.2 },
		{ "LABEL": "Wiggle","NAME": "mat_wiggle", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1 }, 
		{ "LABEL": "Morph", "NAME": "mat_morph", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
		{ "LABEL": "Separation", "NAME": "mat_separation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
		{ "LABEL": "Schmolick", "NAME": "mat_schmolick", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
		{ "LABEL": "Cosine", "NAME": "mat_cosine", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
		{ "LABEL": "InvertSDF", "NAME": "mat_SDFinvert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_cracks", "PATH": "special_cracks.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uvSDF = (texCoord*2.0)-1.0;
	vec2 uv = uvSDF;
	float t = mat_animation_time;

	uv += dfBm(vec2(t, sin(t))).rg * 0.02 * mat_wiggle;

	float noise_scale = 0.05;
	uv.x += fBm(vec3(uv * 2. * mat_noisescale + 0.234 + t + cos(uv), fBm(uv * mat_noisescale))) * mat_mix_noise * noise_scale * 2;
	uv.y += fBm(uv * mat_noisescale + sin(uv * mat_noisescale) + 4.543 + t) * mat_mix_noise * noise_scale * 2;
	
	vec3 color = vec3(1.);

	uvSDF = uv;
	uvSDF = mix(sin(uvSDF), cos(uvSDF), mat_cosine);
	uvSDF = repeatRadial(uvSDF, PI*(1.- mat_schmolick));	
	float radius = mix(mat_scale, mat_scale * 1.3, mat_cosine);
	float distRounded = roundedRectangle(uvSDF,vec2(0., -mat_separation), vec2(radius), mat_morph * radius + 0.001);
	float distRounded2 = roundedRectangle(uvSDF,vec2(0., mat_separation), vec2(radius), mat_morph * radius + 0.001);
	float dist  = smoothBlend( distRounded, distRounded2, mat_separation );
	
	vec3 col = fill( vec3(1.-float(mat_SDFinvert)), vec3(float(mat_SDFinvert)), dist);
	float result = col.r;
	color = col;

	vec3 tex = texture(mat_cracks,texCoord).bgg;
	tex = pow(tex, vec3(mix(0.3, 2.2, pow(vnoise(-uv * 5.), 2.2))));

	float use = vnoise(texCoord * 1.1 + 2.23);
	
	color += mix((1.-tex) * 0.7, -(1-tex) * 0.1, color.r);
	color *= vec3(1, 0.98, 0.94);
	color.rgb -= vec3(pow(use, 2.2) * 0.2) * result;

	// Apply Invert
	color = mix(color, vec3(1.0)-color, float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}