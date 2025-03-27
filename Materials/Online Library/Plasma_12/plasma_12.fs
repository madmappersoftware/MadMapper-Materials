/*{
    "CREDIT": "raymarched plasma from shadertoy llsSR7",
    "DESCRIPTION": "electromagnetic plasma drive",
    "TAGS": "plasma",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "_Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.5 },
		{ "LABEL": "_Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{ "LABEL": "_Deformation", "NAME": "uDeform", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 },
		{ "LABEL": "_Z", "NAME": "uZ", "TYPE": "float", "MIN": -.25, "MAX": 0.25, "DEFAULT": 0.0 },
		{ "LABEL": "_Complexity", "NAME": "RM_ITERS", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 5 },
		{ "LABEL": "_Noise Power", "NAME": "uNoisePower", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 4.0 },
		{ "LABEL": "_Noise Scale", "NAME": "uNoiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0.25 },
		{
            "NAME": "brightness",
            "LABEL": "_Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.4
        },
        {
            "NAME": "contrast",
            "LABEL": "_Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.7
        },
        {
            "NAME": "saturation",
            "LABEL": "_Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "hue_shift",
            "LABEL": "_Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
		{ "LABEL": "Color/_Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 1.0, 0.1, 0.1, 1.0 ] },
		{ "LABEL": "Color/_Color B", "NAME": "uColorB", "TYPE": "color", "DEFAULT": [ 0.6, 0.6, 0.6, 1.0 ] },
      ],
	 "GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/


#include "MadCommon.glsl"
#include "MadNoise.glsl"
#define RM_FACTOR   0.9


float plasma(vec3 r) {
	float mx = r.x + animation_time / 0.130;
	mx += 20.0 * sin((r.y + mx) / 20.0 + animation_time / 0.810);
	float my = r.y - animation_time / 0.200;
	my += 30.0 * cos(r.x / 23.0 + animation_time / 0.710);
	my += billowedNoise(r*uNoiseScale)*uNoisePower;
	return r.z - (sin(mx / 7.0) * 2.25 + sin(my / 3.0) * 2.25 + 5.5);
}

float scene(vec3 r) {
	return plasma(r);
}

float raymarch(vec3 pos, vec3 dir) {
	float dist = 0.0;
	float dscene;

	for (int i = 0; i < RM_ITERS; i++) {
		dscene = scene(pos + dist * dir);
		if (abs(dscene) < 0.1)
			break;
		dist += RM_FACTOR * dscene;
	}

	return dist;
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	
	float c, s;
	float vfov = 3.14159 / 2.3;

	vec3 cam = vec3(10.0, 10.0, 20.0);

	vec2 uv = texCoord - 0.5;
	uv.y *= 1.0;
	uv *= uScale;

	vec3 dir = vec3(0.0, 0.0, -1.0);

	float xrot = vfov * length(uv);

	c = cos(xrot);
	s = sin(xrot);
	dir = mat3(1.0, 0.0, 0.0,
	           0.0,   c,  -s,
	           0.0,   s,   c) * dir;

	c = normalize(uv).x;
	s = normalize(uv).y;
	dir = mat3(  c,  -s, 0.0,
	             s,   c, 0.0,
	           0.0, 0.0, 1.0) * dir;

	c = cos(0.7);
	s = sin(uZ* PI * 2);
	dir = mat3(  c, 0.0,   s,
	           0.0, 1.0, 0.0,
	            -s, 0.0,   c) * dir;

	float dist = raymarch(cam, dir);
	vec3 pos = cam + dist * dir;

	vec3 plasma;
	plasma.rgb = mix(
		uColorA.rgb,
		mix(
			vec3(0.0, 0.0, 0.0),
			uColorB.rgb,
			pos.z / 10.0
		),
		1.0 / (dist / 20.0)
	);
		
	vec3 out_color = plasma;
	
	// Apply Hue Shift and saturation
    if (hue_shift > 0.01 || saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+hue_shift));
        hsv.y = max(hsv.y + saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, contrast);

    // Apply brightness
    out_color.rgb += brightness;
	
	return vec4(out_color,1.0);
	
}
