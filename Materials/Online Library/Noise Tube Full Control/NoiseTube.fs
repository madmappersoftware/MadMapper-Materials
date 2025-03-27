/*{
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Freely combine 1-3 different noises with fine control",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Global/Noise Speed", "NAME": "zspeed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
        { "LABEL": "Global/Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Global/Speed X", "NAME": "xspeed", "TYPE": "float", "MIN": -3.0, "MAX": 3.0, "DEFAULT": 0.0 },  
        { "LABEL": "Global/Speed Y", "NAME": "yspeed", "TYPE": "float", "MIN": -3.0, "MAX": 3.0, "DEFAULT": 0.0 }, 
		
		{ "LABEL": "Noise 1/Noise 1", "NAME": "mat_noise1_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },  
        { "LABEL": "Noise 1/Noise Type", "NAME": "mat_noise_type", "TYPE": "long", "VALUES": ["Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "Billowed" },  
        { "LABEL": "Noise 1/Scale", "NAME": "mat_noise1_scale", "TYPE": "float", "MIN": 0.00001, "MAX": 10.0, "DEFAULT": 1.0 },  
        { "LABEL": "Noise 1/Invert", "NAME": "mat_noise1_invert", "TYPE": "bool", "DEFAULT": true },  
        
		{ "LABEL": "Noise 2/Noise 2", "NAME": "mat_noise2_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },  
        { "LABEL": "Noise 2/Noise Type", "NAME": "mat_noise2_type", "TYPE": "long", "VALUES": ["Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "fBm" },  
        { "LABEL": "Noise 2/Scale", "NAME": "mat_noise2_scale", "TYPE": "float", "MIN": 0.00001, "MAX": 10.0, "DEFAULT": 2.0 },  
        { "LABEL": "Noise 2/Invert", "NAME": "mat_noise2_invert", "TYPE": "bool", "DEFAULT": false },  
        { "LABEL": "Noise 2/Mode", "NAME": "mat_noise2_combi", "TYPE": "long", "VALUES": ["Combine","Multiply","Mix","Subtract","Multiply Combination"], "DEFAULT": "Multiply Combination" },  
        { "LABEL": "Noise 2/Control", "NAME": "mat_noise2_input", "TYPE": "float", "MIN": 0, "MAX": 4.0, "DEFAULT": 1.5 },  
        
		{ "LABEL": "Noise 3/Noise 3", "NAME": "mat_noise3_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },  
        { "LABEL": "Noise 3/Noise Type", "NAME": "mat_noise3_type", "TYPE": "long", "VALUES": ["Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "Billowed" },  
        { "LABEL": "Noise 3/Invert", "NAME": "mat_noise3_invert", "TYPE": "bool", "DEFAULT": false },  
        { "LABEL": "Noise 3/Scale", "NAME": "mat_noise3_scale", "TYPE": "float", "MIN": 0.00001, "MAX": 10.0, "DEFAULT": 2.0 },  
        { "LABEL": "Noise 3/Mode", "NAME": "mat_noise3_combi", "TYPE": "long", "VALUES": ["Combine","Multiply","Mix","Subtract","Multiply Combination"], "DEFAULT": "Combine" },  
        { "LABEL": "Noise 3/Control", "NAME": "mat_noise3_input", "TYPE": "float", "MIN": 0, "MAX": 4.0, "DEFAULT": 1.0 },  
        
		{ "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -10.0, "MAX": 3.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Burn", "NAME": "mat_burn", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.5 },
    ],
    "GENERATORS": [
        {"NAME": "mat_xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "mat_ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "mat_ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed", "speed_curve": 4, "reverse": "reverse", "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * mat_noise1_scale * mat_noise1_scale + vec2( mat_xtime, mat_ytime ), mat_ztime );

    // Noise
	float n = 1;
	if (mat_noise1_active) {
		if (mat_noise_type==0) {
			n = billowedNoise( uv );
		} else if (mat_noise_type==1) {
			n = noise( uv );
		} else if (mat_noise_type==2) {
			n = worleyNoise( uv );
		} else if (mat_noise_type==3) {
			n = fBm( uv );
		} else if (mat_noise_type==4) {
			n = ridgedNoise( uv );
		} else if (mat_noise_type==5) {
			n = billowyTurbulence( uv );
		} else {
			n = ridgedMF( uv );
		}
		if (mat_noise1_invert) n = 1-5*n;
	}

	if (mat_noise2_active) {
		uv = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * mat_noise2_scale * mat_noise2_scale + vec2( mat_xtime, mat_ytime ), mat_ztime );
		
		uv.xy += 1;

		float noise2_value;
		if (mat_noise1_active && (mat_noise2_combi == 0 || mat_noise2_combi == 4)) {
			uv.z = n*mat_noise2_input*mat_noise2_input;
		}
		
		if (mat_noise2_type==0) {
			noise2_value = billowedNoise( uv );
		} else if (mat_noise2_type==1) {
			noise2_value = noise( uv );
		} else if (mat_noise2_type==2) {
			noise2_value = worleyNoise( uv );
		} else if (mat_noise2_type==3) {
			noise2_value = fBm( uv );
		} else if (mat_noise2_type==4) {
			noise2_value = ridgedNoise( uv );
		} else if (mat_noise2_type==5) {
			noise2_value = billowyTurbulence( uv );
		} else {
			noise2_value = ridgedMF( uv );
		}
		if (mat_noise2_invert) noise2_value = 1-5*noise2_value;
		
		if (mat_noise2_combi == 0) {
			n = noise2_value;
		} else if (mat_noise2_combi == 1) {
			n = mix(n,n*noise2_value,mat_noise2_input);
		} else if (mat_noise2_combi == 2) {
			n = mix(n,(n + noise2_value) / 2,mat_noise2_input);
		} else if (mat_noise2_combi == 3) {
			n = mix(n,n - abs(noise2_value),mat_noise2_input);
		} else if (mat_noise2_combi == 4) {
			n *= max(0,noise2_value);
		} 
	}

	if (mat_noise3_active) {
		uv = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * mat_noise3_scale * mat_noise3_scale + vec2( mat_xtime, mat_ytime ), mat_ztime );

		uv.xy += 2;
		
		float noise3_value;
		if ((mat_noise1_active || mat_noise2_active) && (mat_noise3_combi == 0 || mat_noise3_combi == 4)) {
			uv.z = n*mat_noise3_input*mat_noise3_input;
		}
		
		if (mat_noise3_type==0) {
			noise3_value = billowedNoise( uv );
		} else if (mat_noise3_type==1) {
			noise3_value = noise( uv );
		} else if (mat_noise3_type==2) {
			noise3_value = worleyNoise( uv );
		} else if (mat_noise3_type==3) {
			noise3_value = fBm( uv );
		} else if (mat_noise3_type==4) {
			noise3_value = ridgedNoise( uv );
		} else if (mat_noise3_type==5) {
			noise3_value = billowyTurbulence( uv );
		} else {
			noise3_value = ridgedMF( uv );
		}
		if (mat_noise3_invert) noise3_value = 1-5*noise3_value;
		
		if (mat_noise3_combi == 0) {
			n = noise3_value;
		} else if (mat_noise3_combi == 1) {
			n = mix(n,n*noise3_value,mat_noise3_input);
		} else if (mat_noise3_combi == 2) {
			n = mix(n,(n + noise3_value) / 2,mat_noise3_input);
		} else if (mat_noise3_combi == 3) {
			n = mix(n,n - abs(noise3_value),mat_noise3_input);
		} else if (mat_noise3_combi == 4) {
			n *= max(0,noise3_value);
		} 
	}

	n = max(0,n);
	
    // Interpolate Color
    vec3 color  = mix( backgroundColor.rgb, foregroundColor.rgb, n );
	
     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

 	color += mat_burn * mat_burn * vec3(pow(n,2));
	
    return vec4( color, 1.0f );
}
