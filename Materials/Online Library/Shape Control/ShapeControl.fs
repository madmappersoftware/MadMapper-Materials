/*{
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Custom shape with a few settings + bling bling auto mode (bpm synched & audio reactive)",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Shape", "NAME": "mat_shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon"], "DEFAULT": "Circle", "FLAGS": "generate_as_define" },
        { "LABEL": "Repeat", "NAME": "mat_repeat", "TYPE": "int", "MIN": 1, "MAX": 16.0, "DEFAULT": 1 },  
        { "LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },  
        { "LABEL": "Stroke", "NAME": "mat_stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },  
        { "LABEL": "Smoothness", "NAME": "mat_smoothness", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 1 },  
        { "LABEL": "Inner Repeat", "NAME": "mat_inner_repeat", "TYPE": "float", "MIN": 1, "MAX": 8.0, "DEFAULT": 1 },  
        { "LABEL": "BlingBling/Auto", "NAME": "mat_blingbling_auto", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },  
        { "LABEL": "BlingBling/Speed", "NAME": "mat_blingbling_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
	    { "LABEL": "BlingBling/BPM Sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "BlingBling/Bass To Size", "NAME": "mat_blingbling_bass2size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
    	{ "NAME": "mat_spectrum", "TYPE": "audioFFT", "SIZE": 3, "ATTACK": 0.0, "DECAY": 0.0, "RELEASE": 0.5},
      ],
    "GENERATORS": [
        {"NAME": "mat_blingbling_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_blingbling_speed", "bpm_sync": "mat_bpm_sync", "speed_curve":3, "link_speed_to_global_bpm":true}},
	  ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 p = repeat(texCoord,vec2(1.0/mat_repeat)) * mat_repeat;
	
	float size = mat_size / 2;
	float smoothness = mat_smoothness;
	float stroke_size = mat_stroke_size;
	float innerRepeatShape = mat_inner_repeat - 1;
	
	if (mat_blingbling_auto) {
		// Animate Size
		size *= mix(01-fract(mat_blingbling_time/8),min(1,2*IMG_NORM_PIXEL(mat_spectrum,vec2(0.17,0)).r),mat_blingbling_bass2size);
		// Animate Stroke
		stroke_size *= 0.2 + 0.8*(0.5+0.5*sin(mat_blingbling_time*PI/4));
		// Animate Smoothness
		smoothness *= (.5+0.5*sin(mat_blingbling_time*PI/16)) * (1-stroke_size);
		// Animate Inner Repeat
		innerRepeatShape *= 0.5*(1+sin(mat_blingbling_time*PI/12)) * stroke_size;
	}
	
    #if defined(mat_shape_IS_Square)
        float shapeDist = rectangle(p, vec2(size) );
    #elif defined(mat_shape_IS_Circle)
        float shapeDist = circle(p, size );
    #elif defined(mat_shape_IS_Hexagon)
        float shapeDist = hexagon(p, size );
    #else
        p.x *= 0.43333;
        p.x /= size;
        p.y = -0.375 * (p.y-0.16666);
        p.y /= size;
        float shapeDist = triangle( p, 0.5 );
		stroke_size /= 2;
    #endif
				
	float value = 0;
	if (shapeDist<0 && shapeDist>-stroke_size) {
		float normalizedPosInShape = shapeDist/stroke_size;
		//shapeDist = stroke_size * -fract(normalizedPosInShape * pow(mat_repeat,2)); 
		shapeDist *= cos(-PI/2 - normalizedPosInShape * pow(1+innerRepeatShape,2) * 2* PI);

		float minDist = 2 * min(-shapeDist,stroke_size+shapeDist) * 1/stroke_size;
		value = minDist * 1/smoothness;
	}
	
	return vec4(vec3(value),1);
}
