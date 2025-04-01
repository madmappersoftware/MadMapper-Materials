/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Various Noise Combination",
    "TAGS": "noise",
    "VSN": "1.2",
    "INPUTS": [
        { "LABEL": "Mode", "NAME": "mat_mode", "TYPE": "long", "VALUES": ["Billowed Combo","Billowed Werley","Billowed Combo 2","Full Combo","Billowed fBm","Billowed Combo 3","Billowed Werley 2","Billowed Werley 3","Billowed Werley 4","Ridge Worley fBm","Ridge Combo"], "DEFAULT": "Billowed Combo" },  
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.00001, "MAX": 5.0, "DEFAULT": 1.0 },  
        { "LABEL": "Scale 2", "NAME": "mat_scale2", "TYPE": "float", "MIN": 0.1, "MAX": 50.0, "DEFAULT": 1.5 },  
        { "LABEL": "Scale 3", "NAME": "mat_scale3", "TYPE": "float", "MIN": 0.1, "MAX": 50.0, "DEFAULT": 10.0 },  
        { "LABEL": "Noise Speed", "NAME": "mat_zspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Speed X", "NAME": "mat_xspeed", "TYPE": "float", "MIN": -3.0, "MAX": 3.0, "DEFAULT": 0.0 },  
        { "LABEL": "Speed Y", "NAME": "mat_yspeed", "TYPE": "float", "MIN": -3.0, "MAX": 3.0, "DEFAULT": 0.0 },  
        { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": -1.0, "MAX": 5.0, "DEFAULT": 1.0 },
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "mat_xspeed", "speed_curve": 4, "reverse": "mat_reverse", "link_speed_to_global_bpm":true}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "mat_yspeed", "speed_curve": 4, "reverse": "mat_reverse", "link_speed_to_global_bpm":true}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "mat_zspeed", "speed_curve": 4, "reverse": "mat_reverse", "link_speed_to_global_bpm":true}}
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv     = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * mat_scale * mat_scale + vec2( xtime, ytime ), ztime );
	vec3 uv2	= vec3(1,2,1) + uv * vec3(mat_scale2,mat_scale2,1);
	vec3 uv3	= vec3(1,2,2) + uv * vec3(mat_scale3,mat_scale3,1);
	
	float value;
	float brightness = 0;
	float contrast = 1;
	
    // Simplex Noise
	if (mat_mode == 0) {
		value = 1 - abs((billowedNoise(uv) + billowedNoise(uv2)) * abs(billowedNoise(uv3/vec3(10,10,1))));
		brightness = -40 + mat_brightness * 20;
		contrast = 81 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 1) {
		value = 1 - mix(billowedNoise(uv)*billowedNoise(uv2),worleyNoise(uv),abs(billowedNoise(uv3)));
		brightness = -20.7 + mat_brightness * 20;
		contrast = 43 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 2) {
		value = 1 - billowedNoise(uv) * billowedNoise(uv2) * billowedNoise(uv3);
		brightness = -21.7 + mat_brightness * 22;
		contrast = 43 + (mat_contrast-1) * 43;
	}
	if (mat_mode == 3) {
		value = 1 - fBm(vec2(worleyNoise(uv3),billowedNoise(uv2)));
		brightness = -10 + mat_brightness * 20;
		contrast = 10 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 4) {
		value = 1 - abs(billowedNoise(uv2)*fBm(uv3));
		brightness = -6.8 + mat_brightness * 20;
		contrast = 13 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 5) {
		value = 1 - abs(billowedNoise(uv)) * billowedNoise(uv2) * billowedNoise(uv3*vec3(5,5,1));
		brightness = -28.5 + mat_brightness * 20;
		contrast = 58 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 6) {
		value = 1 - abs(billowedNoise(uv2)*worleyNoise(uv3*vec3(5,5,1)));
		brightness = -6.8 + mat_brightness * 20;
		contrast = 13 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 7) {
		value = 1 - abs(billowedNoise(uv)) * billowedNoise(uv2) * worleyNoise(uv3*vec3(25,25,1));
		brightness = -28.5 + mat_brightness * 20;
		contrast = 58 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 8) {
		value = 1 - worleyNoise(vec2(billowedNoise(uv+worleyNoise(uv3))));
		brightness = -3.6 + mat_brightness * 20;
		contrast = 10 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 9) {
		value = 1.08 - ridgedNoise(vec2(worleyNoise(uv+fBm(uv))));
		brightness = -0.4 + mat_brightness * 20;
		contrast = 9 + (mat_contrast-1) * 20;
	}
	if (mat_mode == 10) {
		value = ridgedMF(vec2(ridgedMF(uv)));
		brightness = -0.2 + mat_brightness * 20;
		contrast = 1 -(mat_contrast-1) * 20;
	}

    // Interpolate Color
    vec3 color  = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, value );
 
     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

	if (mat_mode < 2) {
		color = color-pow(0.5-fBm(uv*5),4);
	}
	if (mat_mode == 2 || mat_mode == 4 || mat_mode == 6) {
		color = saturate(color);
		color = color*4.0;
	}
	
    return vec4( color, 1.0f );
}
