/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Chosen noise passing through optional two other chosen noises.",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Noise 1", "NAME": "mat_noise_type", "TYPE": "long", "VALUES": ["Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "Ridged MF" },  
        { "LABEL": "Noise 2", "NAME": "mat_noise2_type", "TYPE": "long", "VALUES": ["None","Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "Billowy Turbulence" },  
        { "LABEL": "Noise 3", "NAME": "mat_noise3_type", "TYPE": "long", "VALUES": ["None","Billowed","Simplex","Werley","fBm","Ridge","Billowy Turbulence","Ridged MF"], "DEFAULT": "None" },  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.00001, "MAX": 20.0, "DEFAULT": 2.0 },  
        { "LABEL": "Noise Speed", "NAME": "zspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Speed X", "NAME": "xspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Speed Y", "NAME": "yspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.17, 0.35, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -10.0, "MAX": 3.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Burn", "NAME": "mat_burn", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": false },  
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed", "speed_curve": 4, "reverse": "reverse", "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv     = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * scale * scale + vec2( xtime, ytime ), ztime );

    // Noise
	float n;
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

	if (mat_noise2_type!=0) {
		if (mat_noise2_type==1) {
			n = billowedNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise2_type==2) {
			n = noise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise2_type==3) {
			n = worleyNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise2_type==4) {
			n = fBm( vec3(uv.xy,n+ztime) );
		} else if (mat_noise2_type==5) {
			n = ridgedNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise2_type==6) {
			n = billowyTurbulence( vec3(uv.xy,n+ztime) );
		} else {
			n = ridgedMF( vec3(uv.xy,n+ztime) );
		}
	}

	if (mat_noise3_type!=0) {
		if (mat_noise3_type==1) {
			n = billowedNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise3_type==2) {
			n = noise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise3_type==3) {
			n = worleyNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise3_type==4) {
			n = fBm( vec3(uv.xy,n+ztime) );
		} else if (mat_noise3_type==5) {
			n = ridgedNoise( vec3(uv.xy,n+ztime) );
		} else if (mat_noise3_type==6) {
			n = billowyTurbulence( vec3(uv.xy,n+ztime) );
		} else {
			n = ridgedMF( vec3(uv.xy,n+ztime) );
		}
	}

    if (invert) n = 1 - n;

	n = max(0,n);
	
    // Interpolate Color
    vec3 color  = mix( backgroundColor.rgb, foregroundColor.rgb, n );
	
     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

 	color += mat_burn * vec3(pow(n,2));
	
    return vec4( color, 1.0f );
}
