/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mad-matt",
    "DESCRIPTION": "Fast draft on how we could drive a 3D LED setup with a shape, controlling its position & size / splitting the Z into planes where we could map each plane of LED",
    "TAGS": "LED,3D",
    "VSN": "1.0",
	"PREVIEW_ASPECT_RATIO": 16,
    "INPUTS": [ 
		{ "LABEL": "Plane Count", "NAME": "mat_plane_count", "TYPE": "int", "MIN": 1, "MAX": 128, "DEFAULT": 12 },

        { "LABEL": "Noise Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.5 },	
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Depth Scale", "NAME": "mat_depth_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Rotate", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{ "LABEL": "Rot Axis", "NAME": "mat_rot_axis", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -2.0, "MAX": 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.1 },
        { "LABEL": "Color/Saturation", "NAME": "saturation", "TYPE": "float", "MIN": -1.0, "MAX": 3.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Hue", "NAME": "hue_shift", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
      ],
	 "GENERATORS": [
		 {"NAME": "noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
     "RASTERISATION_SETTINGS": {
	    "DEFAULT_RENDER_TO_TEXTURE": true,
	    "DEFAULT_WIDTH": 144,
	    "DEFAULT_HEIGHT": 12
	 },
}*/

#include "MadCommon.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	float pixelDist = 1.0 / mat_plane_count;

	// 3D position for this pixel in (-1,-1,-1) - (1,1,1)
	float zPlane = floor(texCoord.x*mat_plane_count);
	vec3 pos3D = vec3(-1+2*(texCoord.x*mat_plane_count - zPlane),-1+2*texCoord.y,-1.+2.*zPlane/(mat_plane_count-1));

	// Rotate
	vec3 axis;
    if (mat_rot_axis < 1) {
		axis = mix(vec3(1,0,0),vec3(0,1,0),mat_rot_axis-0);
    } else if (mat_rot_axis < 2) {
		axis = mix(vec3(0,1,0),vec3(0,0,1),mat_rot_axis-1);
    } else {
		axis = mix(vec3(0,0,1),vec3(1,0,0),mat_rot_axis-2);
    }
    axis = normalize(axis);
	float s = sin(mat_rotation*2*PI/360);
	float c = cos(mat_rotation*2*PI/360);
    float oc = 1.0 - c;
    mat4 rotMatrix = 
           mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
	vec4 pos4D = vec4(pos3D,1) * rotMatrix;
	pos3D = pos4D.xyz / pos4D.w;

	vec2 uv = pos3D.xy*mat_scale;
	float t = noise_time + pos3D.z*mat_scale*mat_depth_scale*2;
    
    float v1 = sin(uv.x*5.0 + t);
    float v2 = sin(5.0*(uv.x*sin(t / 2.0) + uv.y*cos(t/3.0)) + t);
    float cx = uv.x + sin(t / 5.0)*5.0;
    float cy = uv.y + sin(t / 3.0)*5.0;
    float v3 = sin(sqrt(100.0*(cx*cx + cy*cy)) + t);
	
	float vf = v1 + v2 + v3;
	
	float r = sin(vf * PI)*0.5+0.5;
	float g = cos(vf * PI + 4.0)*0.5+0.5;
	float b = cos(vf * PI + sin(t) + 2.8)*0.5+0.5;
	
	vec3 out_color = vec3(r,g,b);
	
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
	
	return vec4(out_color,1);
}

