/*{
    "CREDIT": "1024 architecture\nFranz\nBased on Shadertoy 4dfSDj",
    "DESCRIPTION": "Inspired by Joy Division - Unknown Pleasures / 1979",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Speed_Sperm", "NAME": "mat_speed_sperm", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 }, 

		{ "LABEL": "Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 128, "DEFAULT": 115 }, 
		{ "LABEL": "Azimut", "NAME": "mat_azimut", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.6 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{ "LABEL": "Fat", "NAME": "mat_fat", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Sperm", "NAME": "mat_sperm", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Sperm Scale", "NAME": "mat_sperm_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, 1.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 0.0, -0.02 ] },
		{ "LABEL": "Noise Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.5 }, 
		{ "LABEL": "Noise Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 }, 
		{ "LABEL": "Simple In", "NAME": "mat_simplein", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "Simple Out", "NAME": "mat_simpleout", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Gradient", "NAME": "mat_gradient", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Flip gradient", "NAME": "mat_flipgradient", "TYPE": "bool",  "DEFAULT": 0 ,"FLAGS": "button"},
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_sperm_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_sperm", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_some_texture", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

mat4 CreatePerspectiveMatrix(in float fov, in float aspect, in float near, in float far)
{
    mat4 m = mat4(0.0);
    float angle = (fov / 180.0) * PI;
    float f = 1. / tan( angle * 0.5 );
    m[0][0] = f / aspect;
    m[1][1] = f;
    m[2][2] = (far + near) / (near - far);
    m[2][3] = -1.;
    m[3][2] = (2. * far*near) / (near - far);
    return m;
}

mat4 CamControl( vec3 eye, float pitch)
{
    float cosPitch = cos(pitch);
    float sinPitch = sin(pitch);
    vec3 xaxis = vec3( 1, 0, 0. );
    vec3 yaxis = vec3( 0., cosPitch, sinPitch );
    vec3 zaxis = vec3( 0., -sinPitch, cosPitch );

    // Create a 4x4 view matrix from the right, up, forward and eye position vectors
    mat4 viewMatrix = mat4(
        vec4(       xaxis.x,            yaxis.x,            zaxis.x,      0 ),
        vec4(       xaxis.y,            yaxis.y,            zaxis.y,      0 ),
        vec4(       xaxis.z,            yaxis.z,            zaxis.z,      0 ),
        vec4( -dot( xaxis, eye ), -dot( yaxis, eye ), -dot( zaxis, eye ), 1 )
    );
    return viewMatrix;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

	// modify uv with material inputs
	uv = uv* 2. -1.;
	uv *= mat_scale;

	vec3 color = vec3(1.);
	
    vec2 p = uv;
	p += vec2(mat_offset.x, mat_offset.y);
 
	vec3 eye = vec3(0., -0.25 + mat_azimut, -1.);
    mat4 projmat = CreatePerspectiveMatrix(50., 1.7, 0.1, 10.);
    mat4 viewmat = CamControl(eye, -5. * PI/180.);
    mat4 vpmat = viewmat * projmat;
    
	vec3 col = vec3(0.);
	vec3 acc = vec3(0.);
	float d;
	
    vec4 pos = vec4(0.);
	float lh = -1024.;
	float off = 0.;
	float h = 0.;
	float z = 0.1;
	float zi = 0.05;

	float simpleout = clamp(2. - (uv.x) - mat_simpleout*2, 0., 1.);
	float simplein = clamp(-2. - (uv.x) + mat_simplein*2, -1., 0.);
	float simple = simplein * simpleout;

	for (int i=0; i<mat_amount; ++i)
	{		
        pos = vec4(p.x,
				   simple*mat_noisepower*fBm(vec3(0.5*vec2(eye.x+p.x, z+off)*mat_noisescale,mat_animation_time))-0.5,
				   eye.z+z, 
				   1.); 

  		vec4 P = vpmat*pos;

		float S = fract(P.x * 10. * mat_sperm_scale + i * 0.33 + mat_sperm_time);
		S = mix(pow(S, 2.2), 1., 1.-mat_sperm);
		h = P.y - p.y;

		if (h>lh)
		{
			d = abs(h);
			col = vec3(d < 0.5 ? smoothstep(1., 0.0, d * (200. -mat_fat * 100.)) : 0.);
			float g = (mix(float(i), float(mat_amount-i), mat_flipgradient));
			col *= exp((-0.1 * mat_gradient) * g );
			col *= S;
            acc += col;
			lh = h;
		}

		z += zi;		
	}

	col = sqrt(clamp(acc, 0., 1.));	
	color = col;

	// Apply Tint
	color *= mat_tint.rgb;

	// Apply Invert
	color = mix(color, vec3(1.0)-color, float(mat_invert));
		
	return vec4(color, 1.0);
}