/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joseph Fiola, converted by zerbzman",
    "DESCRIPTION": "Converted from Joseph Fiola's work which was based on shader by Shadertoy user smb02dunnal entitled \"Electro-Prim's\" - https://www.shadertoy.com/view/Mll3WS. I added speed and color controls.",
    "TAGS": "Generator",
    "VSN": "1.1",
    "INPUTS": [ 
		{ "LABEL": "Shape", "NAME": "mat_shape", "TYPE": "long", "VALUES": [0,1], "LABELS": ["triangle", "square"], "DEFAULT": 0 },
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1. },
		{ "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Zoom", "NAME": "mat_zoom", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 1. },
		{ "LABEL": "Rotate", "NAME": "mat_rotate", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0. },
		{ "LABEL": "Twist", "NAME": "mat_twist", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.02 },
		{ "LABEL": "Tunnel", "NAME": "mat_tunnel", "TYPE": "float", "MIN": 0.25, "MAX": 1.75, "DEFAULT": 1.1 },
		{ "LABEL": "Thickness", "NAME": "mat_thickness", "TYPE": "float", "MIN": 0, "MAX": 0.2, "DEFAULT": 0.003 },
		{ "LABEL": "Amplitude", "NAME": "mat_amplitude", "TYPE": "float", "MIN": 0, "MAX": 100, "DEFAULT": 2. },
		{ "LABEL": "Frequency", "NAME": "mat_frequency", "TYPE": "float", "MIN": 0, "MAX": 50, "DEFAULT": 2. },
		{ "LABEL": "Band", "NAME": "mat_band", "TYPE": "float", "MIN": -0.5, "MAX": 1, "DEFAULT": 0. },
		{ "LABEL": "Position", "NAME": "mat_pos", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ 0.0, 0.0 ], "DEFAULT": [ 0.5, 0.5 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
		{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "" },
		{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "" },  
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve":2,"strob": 0.0,"bpm_sync": "bpmsync","link_speed_to_global_bpm": true,}},
    ],
}*/


precision mediump float;

const float PI = 3.14159265359;
const float TWO_PI = 6.28318530718;

float electro(vec2 uv, float d, float f, float o, float a, float b)
{
    
    float theta = atan(uv.y,uv.x);
    
	float amp = smoothstep(0.0, 1.0, (sin(theta + mat_time * PI)*0.5+0.5)-b)*a;
	float phase = d + sin(theta * f + o + mat_time * PI) * amp;
    
    return sin(clamp(phase, 0.0, PI*2.0) + PI/2.0) + 1.0005;
}

mat2 rotate2d(float _angle){
    return mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle));
}

vec4 materialColorForPixel( vec2 texCoord )
{
   const float radius = 0.1;
    
	vec2 uv = texCoord;
	uv -= vec2(mat_pos);

	uv = rotate2d(PI) * uv; // fix the rotation
       
	uv = rotate2d(mat_rotate * -TWO_PI) * uv;
	uv *= mat_zoom;
   
	float grey = 0.0;
	float alpha = 1.0;

	for(int i = 0; i < 20; i++) {
	
   		float d = 0.0;
		
		//triangle
		if (mat_shape == 0) {
			float root2 = sqrt(2.0);
			d = dot(uv, vec2(0.0,-1.0));
    			d = max(d, dot(uv, vec2(-root2,1.0)));
    			d = max(d, dot(uv, vec2( root2,1.0)));
		}
		
		//square
		if (mat_shape == 1) {
	    		d = max(abs(uv).x, abs(uv).y);
		}

		grey += 1.0 - smoothstep(0.0, mat_thickness, electro(uv, d/radius, mat_frequency, 0.0 * PI, mat_amplitude, mat_band));
    		grey += 1.0 - smoothstep(0.0, mat_thickness, electro(uv, d/radius, mat_frequency, 0.5 * PI, mat_amplitude, mat_band));
    		grey += 1.0 - smoothstep(0.0, mat_thickness, electro(uv, d/radius, mat_frequency, 1.0 * PI, mat_amplitude, mat_band));
   	
    	
	    	//tunnel
	    	uv *= mat_tunnel;
	    	
	    	//twist
	    	uv = rotate2d(mat_twist * -TWO_PI) * uv;
   	}

	// Apply brightness
	grey += mat_brightness;

    // Apply contrast
    grey = mix(0.5, grey, mat_contrast);
	grey = clamp(grey, 0., 1.);

	if (mat_invert) grey = 1 - grey;

	vec4 color = mix( mat_backgroundColor, mat_foregroundColor, grey );
   	
	return color;
}