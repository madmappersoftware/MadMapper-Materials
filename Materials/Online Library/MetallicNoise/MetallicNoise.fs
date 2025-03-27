/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "shadertoy lsKXWc by glkt",
    "DESCRIPTION": "Noise metallic finish",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Softness", "NAME": "mat_soft", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
        { "LABEL": "Speed X", "NAME": "mat_sx", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Speed Y", "NAME": "mat_sy", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -0.1 },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
	
	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_sxn_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_sx", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_syn_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_sy", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"

float hash( vec2 p ) {
	float h = dot(p,vec2(127.1,311.7));	
    return fract(sin(h)*43758.5453123);
}

float noise( in vec2 p ) {
    vec2 i = floor( p );
    vec2 f = fract( p );
    vec2 u = f*f*f*(f*(f*6.-15.)+10.);
    float v = mix( mix( hash( i + vec2(0.,0.) ), 
                     hash( i + vec2(1.,0.) ), u.x),
                mix( hash( i + vec2(0.,1.) ), 
                     hash( i + vec2(1.,1.) ), u.x), u.y);
    return pow(abs(cos(v)),10.);
}

float noiseLayer(vec2 uv) {
    float freq = 10.; // noise base frequency / size
    const int iter = 14; // noise iteration / depth
    float lacunarity = 0.7 + mat_soft*0.3; // lacunarity: relative "importance" of smaller octaves
    float v = 0.;
    float sum = 0.;
    for(int i = 0; i < iter; i++) {
        float layerUp = 1. / pow(freq,lacunarity);
    	v += noise(uv*freq) * layerUp;
        sum += layerUp;
		freq *= 2.0896;
    }
    v /= sum;
    return v;
}


vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	uv -= 0.5;
	// modify uv with material inputs
	uv *= mat_scale*0.1;
	//uv += vec2(-mat_offset.x,mat_offset.y);
	
   	uv.x += noiseLayer(uv*0.1+mat_sxn_time*0.01);    
    uv.y += noiseLayer(uv*0.1+mat_syn_time*0.01);
    
    float v = noiseLayer(uv-vec2(mat_offset.x,mat_offset.y)*0.1+vec2(mat_animation_time*0.1,0.));
        
    v = v*v*2.;
	
	// make a color out of it
	vec3 color = vec3( v);
	
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
