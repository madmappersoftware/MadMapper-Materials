/*
  Laser Material Documentation

  A bit like Materials in MadMapper, Laser Materials are glsl shader, compiled by and executed by the graphics card. A Material actually writes color in pixels in a texture (an image) to be displayed in MadMapper surfaces. A Laser Material is also writing to a texture, but it's not actually writing colors, it's writing one or multiple 2D pathes that include color. The Laser Material output is a list of pathes made of a list of sample points, each including a color and eventually user data.

  The Laser Material code consists of a function called laserMaterialFunc that takes as input the point number for which it will write data, and the total point count it should output. By default MadMapper will request 8192 samples but this can be changed in "RENDER_SETTINGS" / "POINT_COUNT" (documented below). For each sample, we provide 2D position, rgb color, shape number & eventually user data. The shape number allows MadMapper to know when you want to start a new path (for instance if you want to draw two circles, you don't want them connected).

  The output of the laserMaterialFunc is written to a texture. This is what the texture will contain the output of the laserMaterialFunc in this way:

    Line 0:
      r = x position of the sample point (-1 to 1)
      g = y position of the sample point (-1 to 1)
      b = shape number of this sample point (0,1,2,3...): each time shape number is different from previous sample, MadMapper will start a new shape
      a = 0
    Line 1:
      rgba = color (clamp in 0-1)
    Line 2:
      rgba = user data

  The Laser Material can access data generated at previous frame (the output texture of previous engine frame), which allows creating more logic in the pathes generation. This is passed as a sampler2D uniform named mm_LastFrameData.

*/

/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple vector template",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Speed_Sperm", "NAME": "mat_speed_sperm", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 }, 

		{ "LABEL": "Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 8 }, 
		{ "LABEL": "Azimut", "NAME": "mat_azimut", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.6 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Sperm", "NAME": "mat_sperm", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Sperm Scale", "NAME": "mat_sperm_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, 1.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Noise Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.5 }, 
		{ "LABEL": "Noise Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 }, 
		{ "LABEL": "Simple In", "NAME": "mat_simplein", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{ "LABEL": "Simple Out", "NAME": "mat_simpleout", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Gradient", "NAME": "mat_gradient", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Color/Flip gradient", "NAME": "mat_flipgradient", "TYPE": "bool",  "DEFAULT": 0 ,"FLAGS": "button"},
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_sperm_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_sperm", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4096
    }
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

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float normalizedPos = float(pointNumber)/pointCount;
    shapeNumber = int(normalizedPos * mat_amount);
	float normalizedPosInShape = -1+2*fract(normalizedPos * mat_amount);

    float px = normalizedPosInShape*2 + mat_offset.x;

	vec3 eye = vec3(0., -0.25 + mat_azimut, -1.);
    mat4 projmat = CreatePerspectiveMatrix(50., 1.7, 0.1, 10.);
    mat4 viewmat = CamControl(eye, -5. * PI/180.);
    mat4 vpmat = viewmat * projmat;
    
	vec3 col = vec3(0.);
	vec3 acc = vec3(0.);
	float d;
	
	float lh = -1024.;
	float off = 0.;
	float h = 0.;
	float z = 0.1;
	float zi = 0.05;

	float simpleout = clamp(2. - (px-mat_offset.x) - mat_simpleout*2, 0., 1.);
	float simplein = clamp(-2. - (px-mat_offset.x) + mat_simplein*2, -1., 0.);
	float simple = simplein * simpleout;

	z += zi * shapeNumber;
    vec4 P = vec4(px,
				simple*mat_noisepower*fBm(vec3(0.5*vec2(eye.x+ px, z+off)*mat_noisescale,mat_animation_time)),
				eye.z+z, 
				1.); 
	P = vpmat * P;
	pos = P.xy * mat_scale /2 + vec2(0,0.5) + mat_offset;

	float S = fract(P.x * 10. * mat_sperm_scale + shapeNumber * 0.33 + mat_sperm_time);
	S = mix(pow(S, 2.2), 1., 1.-mat_sperm);

	col = vec3(mix(1-mat_gradient,1,shapeNumber/float(mat_amount-1)));
	if (mat_flipgradient) {
		col = vec3(1)-col;
	}
	col *= S;	

	// Apply Tint
	col *= mat_tint.rgb;

	color = vec4(col, 1.0);
}
