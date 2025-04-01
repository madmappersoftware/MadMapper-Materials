/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
	"DESCRIPTION": "Draft of a shape border effect. To be continued",
    "TAGS": ["Graphic","Borders","Quad","Circle","Triangle"],
    "VSN": "0.1",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        { "LABEL": "Aspect Ratio", "NAME": "mat_aspect_ratio", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 10.0 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Thickness", "NAME": "thickness", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.001, "MAX": 1.0 },
        { "LABEL": "Radius", "NAME": "shape_radius", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Repeat Cent", "NAME": "repeat_cent", "TYPE": "int", "MIN" : 1.0, "MAX" : 16.0, "DEFAULT": 1.0 },
        { "LABEL": "Inner Radius", "NAME": "mat_inner_radius", "TYPE": "float", "DEFAULT": 0.8, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Repeat Fade", "NAME": "repeat_fade", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Animate", "NAME": "animactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Anim/Speed", "NAME": "animspeed", "TYPE": "float", "DEFAULT": 0.8, "MIN": -3.0, "MAX": 3.0 },
        { "LABEL": "Anim/Curve", "NAME": "animcurve", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.00001, "MAX": 1.0 },
        { "LABEL": "Anim/Repeat", "NAME": "animrepeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 100 },
        { "LABEL": "Anim/Offset", "NAME": "animoffset", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4.0 },
        { "LABEL": "Anim/Strob", "NAME": "animstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "AutoRadius/Auto Radius", "NAME": "autoradiusactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "AutoRadius/Size", "NAME": "autoradiussize", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "AutoRadius/Speed", "NAME": "autoradiusspeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2.0 },
        { "LABEL": "AutoRadius/Strob", "NAME": "autoradiusstrob", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "AutoRadius/Shape", "NAME": "autoradiusshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" }, 
        { "LABEL": "AutoThickness/Auto Thickness", "NAME": "autothinessactive", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "AutoThickness/Size", "NAME": "autothinesssize", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.2 },
        { "LABEL": "AutoThickness/Speed", "NAME": "autothinessspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.8 },
        { "LABEL": "AutoThickness/Shape", "NAME": "autothinessshape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
    ],
    "GENERATORS": [
        {"NAME": "anim_time", "TYPE": "time_base", "PARAMS": {"speed": "animspeed","bpm_sync": "bpmsync", "speed_curve": 3,"strob": "animstrob"}},
        {"NAME": "auto_thickness_time", "TYPE": "time_base", "PARAMS": {"speed": "autothinessspeed","bpm_sync": "bpmsync", "speed_curve": 3}},
        {"NAME": "auto_thickness_value", "TYPE": "shaper", "PARAMS": {"active": "autothinessactive", "input_value": "auto_thickness_time", "shape": "autothinessshape"}},
        {"NAME": "auto_radius_time", "TYPE": "time_base", "PARAMS": {"speed": "autoradiusspeed","strob": "autoradiusstrob", "bpm_sync": "bpmsync", "speed_curve": 3}},
        {"NAME": "auto_radius_value", "TYPE": "shaper", "PARAMS": {"active": "autoradiusactive", "input_value": "auto_radius_time","shape": "autoradiusshape"}},
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
    vec2 uv = texCoord - vec2(0.5);
	
	vec2 aspect_ratio = vec2(1);
	if (mat_aspect_ratio>1) aspect_ratio = vec2(1,1/mat_aspect_ratio);
	if (mat_aspect_ratio<1) aspect_ratio = vec2(mat_aspect_ratio,1);
	
    float inner_radius = (mat_inner_radius*(1-auto_radius_value*autoradiussize));

	float finalThickness = thickness*thickness+autothinesssize*auto_thickness_value;

	vec4 outColor = vec4(0);
	
	vec2 vecFromCenter = texCoord-vec2(0.5,0.5);
	float angle = 2 * PI+atan(vecFromCenter.y,vecFromCenter.x);
	
	float luma = 1;
	for (int i=0; i<=repeat_cent-1; i++) {
		float ratio = pow(1-(float(i)/repeat_cent) * (1-inner_radius), 2);
		float itRadius = 0.5 * shape_radius * ratio;
		float itThickness = (1-inner_radius)*finalThickness * (0.05 + 0.95 * ratio);
		
		float animFactor = 1;
		if (animactive) {
			// Animate
			float animAngle = 2*PI*(fract(anim_time/animrepeat) + animoffset * (float(i)/repeat_cent));
			float angleDist = mod(4*PI+(angle-animAngle)*animrepeat,2*PI);
			animFactor = 0.5 + (0.5 * sin(angleDist)) * (1/animcurve);
		}

		#if defined(shape_IS_Square)
			vec2 squareRadius = itRadius * aspect_ratio;
			// Refactoring needed for cleaner code but solves the aspect ratio for rectangle
			// (same space between inner repetitions)
			if (mat_aspect_ratio>1) squareRadius.y -= (1-ratio)*(1-aspect_ratio.y)/2;
			if (mat_aspect_ratio<1) squareRadius.x -= (1-ratio)*(1-aspect_ratio.x)/2;
			outColor += luma * animFactor * vec4(stroke(vec3(0,0,0),vec3(1,1,1),rectangle( uv, squareRadius ),itThickness), 1);
		#elif defined(shape_IS_Circle)
			outColor += luma * animFactor * vec4(stroke(vec3(0,0,0),vec3(1,1,1),circle( uv, itRadius ),itThickness), 1);
	    #elif defined(shape_IS_Hexagon)
			outColor += luma * animFactor * vec4(stroke(vec3(0,0,0),vec3(1,1,1),hexagon( uv*vec2(1.15,1), itRadius ),itThickness), 1);
	    #else
			outColor += luma * animFactor * vec4(stroke(vec3(0,0,0),vec3(1,1,1),mm_triangle( uv, itRadius ),itThickness), 1);
	    #endif
		luma *= repeat_fade;
	}
	
	outColor.a = 1;
    return outColor;
}
