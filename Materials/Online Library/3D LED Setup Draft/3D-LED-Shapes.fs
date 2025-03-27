/*{
    "CREDIT": "mad-matt",
    "DESCRIPTION": "Fast draft on how we could drive a 3D LED setup with a shape, controlling its position & size / splitting the Z into planes where we could map each plane of LED",
    "TAGS": "LED,3D",
    "VSN": "1.0",
	"PREVIEW_ASPECT_RATIO": 16,
    "INPUTS": [ 
		{ "LABEL": "Plane Count", "NAME": "mat_plane_count", "TYPE": "int", "MIN": 2, "MAX": 128, "DEFAULT": 16 },

		{ "LABEL": "Sphere/Sphere", "NAME": "mat_sphere_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button"},
		{ "LABEL": "Sphere/Sphere X", "NAME": "mat_sphere_x", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Sphere/Sphere Y", "NAME": "mat_sphere_y", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Sphere/Sphere Z", "NAME": "mat_sphere_z", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Sphere/Sphere Radius", "NAME": "mat_sphere_radius", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.25 },
		{ "LABEL": "Sphere/Smooth", "NAME": "mat_sphere_smooth", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },

		{ "LABEL": "Cube/Cube", "NAME": "mat_cube_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button"},
		{ "LABEL": "Cube/Cube X", "NAME": "mat_cube_x", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Cube/Cube Y", "NAME": "mat_cube_y", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Cube/Cube Z", "NAME": "mat_cube_z", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Cube/Cube Radius", "NAME": "mat_cube_radius", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },

		{ "LABEL": "Noise/Noise", "NAME": "mat_noise_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button"},
		{ "LABEL": "Noise/Scale", "NAME": "mat_noise_scale", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
		{ "LABEL": "Noise/Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": -2, "MAX": 2, "DEFAULT": 0 },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
    "GENERATORS": [
        {"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "speed_curve": 3, "link_speed_to_global_bpm":true}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "Simplex4DNoise.glsl"

float sdf_sphere (vec3 pos, vec3 center, float radius)
{
    return distance(pos,center) - radius;
}

float vmax(vec3 v)
{
	 return max(max(v.x, v.y), v.z);
}
 
float sdf_box(vec3 p, vec3 c, vec3 s)
{
	return vmax(abs(p-c) - s);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	
	// 3D position for this pixel in (-1,-1,-1) - (1,1,1)
	float zPlane = floor(uv.x*mat_plane_count);
	vec3 pos3D = vec3(-1+2*(uv.x*mat_plane_count - zPlane),-1+2*uv.y,-1.+2.*zPlane/(mat_plane_count-1));
	
	vec3 color = vec3(0);
	
	if (mat_sphere_active) {
		vec3 sphere_center = vec3(mat_sphere_x,mat_sphere_y,mat_sphere_z);

		color += mix(fill( vec3(0), vec3(1), sdf_sphere(pos3D,sphere_center,mat_sphere_radius) ),
					 vec3(0.1-pow(sdf_sphere(pos3D,sphere_center,mat_sphere_radius),1)),
					 mat_sphere_smooth);
	}
	
	if (mat_cube_active) {
		vec3 cube_center = vec3(mat_cube_x,mat_cube_y,mat_cube_z);
		color += fill( vec3(0), vec3(1), sdf_box(pos3D,cube_center,vec3(mat_cube_radius)) );
	}
	
	if (mat_noise_active) {
	    // Simplex Noise
		color += snoise( vec4(pos3D * mat_noise_scale,mat_noise_time) );
	}
	
	
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

