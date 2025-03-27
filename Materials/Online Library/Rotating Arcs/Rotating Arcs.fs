/*{
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Tools for mapping circles: animated arcs or cenetered circle shapes",
    "VSN": "1.1",
    "TAGS": "graphic,circle",
    "INPUTS": [
        {
          "NAME": "mat_speed",
          "LABEL": "AutoMove/Speed",
          "TYPE": "float",
          "DEFAULT": 0.5,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "thickness",
          "Label": "Thickness",
          "TYPE": "float",
          "DEFAULT": 0.5,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "base_length",
          "Label": "Length",
          "TYPE": "float",
          "DEFAULT": 0.25,
          "MIN": 0.001,
          "MAX": 1.0
        },
        {
          "Label": "Min Radius",
          "NAME": "min_radius",
          "TYPE": "float",
          "DEFAULT": 0.25,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "Label": "Max Radius",
          "NAME": "radius_amplitude",
          "TYPE": "float",
          "DEFAULT": 1.0,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "Label": "Repeat",
          "NAME": "shape_repeat",
          "TYPE": "int",
          "MIN" : 1.0,
          "MAX" : 32.0,
          "DEFAULT": 8.0
        },
        {
          "Label": "Cent Smooth.",
          "NAME": "cent_smoothness",
          "TYPE": "float",
          "DEFAULT": 0.02,
          "MIN": 0.0001,
          "MAX": 1.0
        },
        { "LABEL": "Animate/Anim Length", "NAME": "animate_length", "TYPE": "bool", "DEFAULT": true },
        { "LABEL": "Animate/Anim Luma", "NAME": "animate_luma", "TYPE": "bool", "DEFAULT": false },
    ],
    "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel(vec2 texCoord)
{
	vec2 uv = texCoord - vec2(0.5);

	float max_radius = min_radius + radius_amplitude * (1-min_radius);
	
	float dist = length(uv) * 2;
	if (dist < min_radius) return vec4(0,0,0,1);
	if (dist > max_radius) return vec4(0,0,0,1);
		
	float normalizedDist = (dist-min_radius) / (max_radius-min_radius);
	int arcId = int(normalizedDist*shape_repeat);
	
	float distInArc = mod(normalizedDist,1./shape_repeat) * shape_repeat;
	if (distInArc > thickness) return vec4(0,0,0,1);
	distInArc /= thickness; // Make value from 0-1

	float arcLuma = 1;
	if (animate_luma) arcLuma *= ridgedNoise(vec3(arcId,0.25,mat_animation_time));

	float arcLength = base_length;
	if (animate_length) arcLength *= ridgedNoise(vec3(arcId,0.75,mat_animation_time));
	
	float pixelLuma = 0;
	
	float pixelAngle = mod(2*PI+atan(uv.y,uv.x),2*PI);
	float arcAngle1 = ridgedNoise(vec3(arcId,0.5,mat_animation_time-arcLength)) * 4*PI;
	float arcAngle2 = ridgedNoise(vec3(arcId,0.5,mat_animation_time)) * 4*PI;

	if (arcAngle2 > arcAngle1) {
		while (pixelAngle < arcAngle1) pixelAngle += 2*PI;
		if (pixelAngle < arcAngle2)
			pixelLuma = smoothstep(arcAngle1,arcAngle2,pixelAngle);
	} else {
		while (pixelAngle < arcAngle2) pixelAngle += 2*PI;
		if (pixelAngle < arcAngle1)
			pixelLuma = smoothstep(arcAngle1,arcAngle2,pixelAngle);
	}

	// Antialiasing on edges
	pixelLuma *= min(1,(1-abs(0.5-distInArc)*2) / cent_smoothness);
	
	vec4 outColor = vec4(vec3(arcLuma*pixelLuma),1);
    return outColor;
}
