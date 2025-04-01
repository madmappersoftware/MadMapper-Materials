/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Eyes landscape using distance fields",
    "TAGS": "SDF",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 }, 
        { "LABEL": "Noise Scale", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{ "LABEL": "Noise Frequency", "NAME": "mat_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "Noise Fill", "NAME": "mat_fill", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Outline", "NAME": "mat_outline", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	
float logn(float a,float b) { return log(a)/log(b); }
#define clamps(x) clamp(x,0.,1.)
vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;

	// use MadNoise libray
	float N = billowedNoise(vec3(uv*mat_freq,mat_animation_time));
	float M = worleyNoise(vec3(uv*mat_freq,mat_animation_time));

	// make a color out of it
	vec3 color = vec3( N);
	
	vec2 center = vec2( 0.5f )+vec2(N,M)*0.01*mat_noise;
	vec2 center2 = vec2( 0.5f )+vec2(N,M)*0.03*mat_noise;
	vec2 domain = repeat( uv-center, vec2( 0.2f ) );
	vec2 domain2 = repeat( uv-center2, vec2( 0.2f ) );
	
	float distFieldA = circle( domain, 0.025f+N*0.1*mat_noise );
	float distFieldB = circle( domain2, 0.01f+M*0.03*mat_noise );

	// Create the resulting distance field
	float distField = subtract( distFieldA, distFieldB );
	distField -= N*0.5*mat_fill;

	// and render it
	vec3 circleColor = vec3( 1.0f );
	vec3 backgroundColor = vec3( 0.0f );
	color = fill( backgroundColor, circleColor, distField );
	color += vec3( smoothstep(0.99,1.,sin(abs(distField*50.))))*mat_outline;

	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
