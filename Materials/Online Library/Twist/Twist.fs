/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joseph Fiola, Matthew DiVito, Shadertoy user fb39ca4. Converted by zerbzman",
    "DESCRIPTION": "Converted and added speed and color controls.",
    "TAGS": "Generator",
  	"VSN": "1.1",
  	"INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0.25 },
		{ "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Zoom", "NAME": "mat_zoom", "TYPE": "float", "MIN": 0.125, "MAX": 3, "DEFAULT": 3. },
		{ "LABEL": "Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0. },
		{ "LABEL": "Twist", "NAME": "mat_twist", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.51 },
		{ "LABEL": "Iterations", "NAME": "mat_iterations", "TYPE": "float", "MIN": 0, "MAX": 60, "DEFAULT": 30. },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1. },
    		{ "LABEL": "LinesOffset", "NAME": "mat_linesOffset", "TYPE": "float", "MIN": -10, "MAX": 10, "DEFAULT": 1. },
		{ "LABEL": "Fade", "NAME": "mat_fade", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1. },
		{ "LABEL": "Stripes", "NAME": "mat_stripes", "TYPE": "bool", "DEFAULT": true },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
		{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "" },
		{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "" },  
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
		{ "LABEL": "Position", "NAME": "mat_pos", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ 0.0, 0.0 ], "DEFAULT": [ 0.5, 0.5 ] },
  	],
	"GENERATORS": [
		{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve":2,"strob": 0.0,"bpm_sync": "bpmsync","link_speed_to_global_bpm": true,}},
	],
}*/

const float PI = 3.14159265;

vec2 rotate(vec2 v, float a) {
	float sinA = sin(a);
	float cosA = cos(a);
	return vec2(v.x * cosA - v.y * sinA, v.y * cosA + v.x * sinA); 	
}

float square(vec2 uv, float d) {
	return max(abs(uv.x), abs(uv.y)) - d;	
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord;
	uv -= vec2(mat_pos);

	uv *= mat_zoom;
	
	uv = rotate(uv, mat_rotation * PI);
	
	float blurAmount = -5.0 / RENDERSIZE.y * (mat_zoom * 0.5);
    
	float time = mat_time;
	time = mod(time, 1.0);
	time += mat_twist;
	
	float G = 0.0;
	for (int i = 0; i < 60; i++) {
		
		float n = float(i);
		float size = 1.0 - n / mat_iterations;
		float rotateAmount = (n * 0.5 + 0.25) * PI * 2.0; 
		G = mix(G, 1.0, smoothstep(0.0, blurAmount, square(rotate(uv, -rotateAmount * time), size)));
		
		float blackOffset = mix(mat_linesOffset / 4.0, mat_linesOffset / 2.0, n / (mat_iterations * mat_offset)) / (mat_iterations * mat_offset);
		G = mix(G, 0.0, smoothstep(0.0, blurAmount, square(rotate( uv, -(rotateAmount + PI / 2.0) * time), size - blackOffset)));
    	
    	
    	if (mat_stripes) {
		G = (G * -1.0 + 1.0) * mat_fade;
       	} else {
			G = G * mat_fade;
       	}
	}

	// Apply brightness
	G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0., 1.);

	if (mat_invert) G = 1 - G;

	vec4 color = mix( mat_backgroundColor, mat_foregroundColor, G );

	return color;
}