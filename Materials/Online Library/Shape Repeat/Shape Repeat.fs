/*{
    "CREDIT": "Matt Beghin",
    "TAGS": ["Graphic"],
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        {
          "NAME": "bpmsync",
          "LABEL": "BPM Sync",
          "TYPE": "bool",
          "DEFAULT": false
        },
        {
          "NAME": "thickness",
          "Label": "Thickness",
          "TYPE": "float",
          "DEFAULT": 0.125,
          "MIN": 0.001,
          "MAX": 0.5
        },
        {
          "Label": "Radius",
          "NAME": "shape_radius",
          "TYPE": "float",
          "DEFAULT": 1.0,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "Label": "Repeat",
          "NAME": "shape_repeat",
          "TYPE": "int",
          "MIN" : 1.0,
          "MAX" : 16.0,
          "DEFAULT": 3.0
        },
        {
          "Label": "Repeat Cent",
          "NAME": "repeat_cent",
          "TYPE": "int",
          "MIN" : 1.0,
          "MAX" : 16.0,
          "DEFAULT": 16.0
        },
        {
          "NAME": "automoveactive",
          "LABEL": "AutoMove/Auto Move",
          "TYPE": "bool",
          "DEFAULT": true,
          "FLAGS": "button"
        },
        {
          "NAME": "automovesize",
          "LABEL": "AutoMove/Size",
          "TYPE": "float",
          "DEFAULT": 0.3,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "automovespeed",
          "LABEL": "AutoMove/Speed",
          "TYPE": "float",
          "DEFAULT": 0.8,
          "MIN": 0.0,
          "MAX": 3.0
        },
        {
          "NAME": "automovestrob",
          "LABEL": "AutoMove/Strob",
          "TYPE": "float",
          "DEFAULT": 0.0,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "autoradiusactive",
          "LABEL": "AutoRadius/Auto Radius",
          "TYPE": "bool",
          "DEFAULT": false,
          "FLAGS": "button"
        },
        {
          "NAME": "autoradiussize",
          "LABEL": "AutoRadius/Size",
          "TYPE": "float",
          "DEFAULT": 0.3,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "autoradiusspeed",
          "LABEL": "AutoRadius/Speed",
          "TYPE": "float",
          "DEFAULT": 0.5,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "NAME": "autoradiusstrob",
          "LABEL": "AutoRadius/Strob",
          "TYPE": "float",
          "DEFAULT": 0.0,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "autoradiusshape",
          "LABEL": "AutoRadius/Shape",
          "TYPE": "long",
          "VALUES": ["Smooth","In","Out","Linear","Cut"],
          "DEFAULT": "Smooth",
          "FLAGS": "generate_as_define"
        }, 
        {
          "NAME": "autothinessactive",
          "LABEL": "AutoThickness/Auto Thickness",
          "TYPE": "bool",
          "DEFAULT": false,
          "FLAGS": "button"
        },
        {
          "NAME": "autothinesssize",
          "Label": "AutoThickness/Size",
          "TYPE": "float",
          "MIN" : 0.0,
          "MAX" : 1.0,
          "DEFAULT": 0.2
        },
        {
          "NAME": "autothinessspeed",
          "Label": "AutoThickness/Speed",
          "TYPE": "float",
          "MIN" : 0.0,
          "MAX" : 2.0,
          "DEFAULT": 0.8
        },
        {
          "NAME": "autothinessshape",
          "LABEL": "AutoThickness/Shape",
          "TYPE": "long",
          "VALUES": ["Smooth","In","Out","Linear","Cut"],
          "DEFAULT": "Smooth",
          "FLAGS": "generate_as_define"
        },
    ],
    "GENERATORS": [
        {"NAME": "auto_thickness_time", "TYPE": "time_base", "PARAMS": {"speed": "autothinessspeed","bpm_sync": "bpmsync", "speed_curve": 3}},
        {"NAME": "auto_thickness_value", "TYPE": "shaper", "PARAMS": {"active": "autothinessactive", "input_value": "auto_thickness_time", "shape": "autothinessshape"}},
        {"NAME": "auto_radius_time", "TYPE": "time_base", "PARAMS": {"speed": "autoradiusspeed","strob": "autoradiusstrob", "bpm_sync": "bpmsync", "speed_curve": 3}},
        {"NAME": "auto_radius_value", "TYPE": "shaper", "PARAMS": {"active": "autoradiusactive", "input_value": "auto_radius_time","shape": "autoradiusshape"}},
        {"NAME": "auto_move_time", "TYPE": "time_base", "PARAMS": {"speed": "automovespeed","strob": "automovestrob", "bpm_sync": "bpmsync", "speed_curve": 3}},
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"


// A single iteration of Bob Jenkins' One-At-A-Time hashing algorithm.
int hash( int x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    x ^= ( x >> 11u );
    x += ( x << 15u );
    return x;
}

float mm_triangle(vec2 uv, float radius ) {
	vec2 modUv = uv;
	modUv.x *= 0.43333;
	modUv.x /= radius;
	modUv.y = -0.375 * (modUv.y-0.16666);
	modUv.y /= radius;
	return triangle(modUv,radius);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 cellId;
    vec2 uv = repeat(texCoord,vec2(1.0/shape_repeat),cellId) * shape_repeat;
	
    float radius = (0.5*shape_radius*(1-auto_radius_value*autoradiussize));

	float finalThickness = thickness*thickness+autothinesssize*auto_thickness_value;

	float cellNum = cellId.x * shape_repeat + cellId.y;
	vec2 moveCenter = vec2(0);
	if (automoveactive) {
		moveCenter = automovesize*0.5*vec2(cos(auto_move_time+cellNum),cos(auto_move_time*1.2+cellNum*33));
	}
	
	vec4 outColor = vec4(0);
	
	for (int i=repeat_cent-1; i>=0; i--) {
		float ratio = pow(1 - float(i)/repeat_cent , 2);
		float itRadius = radius * ratio;
		float itThickness = finalThickness * (0.05 + 0.95 * ratio);
		vec2 itCenter = moveCenter * (1 - ratio);
		#if defined(shape_IS_Square)
			outColor += vec4(stroke(vec3(0,0,0),vec3(1,1,1),rectangle( uv-itCenter, itRadius ),itThickness), 1);
		#elif defined(shape_IS_Circle)
			outColor += vec4(stroke(vec3(0,0,0),vec3(1,1,1),circle( uv-itCenter, itRadius ),itThickness), 1);
	    #elif defined(shape_IS_Hexagon)
			outColor += vec4(stroke(vec3(0,0,0),vec3(1,1,1),hexagon( uv*vec2(1.15,1)-itCenter, itRadius ),itThickness), 1);
	    #else
			outColor += vec4(stroke(vec3(0,0,0),vec3(1,1,1),mm_triangle( uv-itCenter, itRadius ),itThickness), 1);
	    #endif
	}

    return outColor;
}
