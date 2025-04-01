/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Shape animations, repeated and offsetted, very usefull for mapping.",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon","Rounded Rectangle"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        { "LABEL": "Stroke Size", "NAME": "stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 },
        { "LABEL": "Pixelate", "NAME": "mat_pixelate", "TYPE": "float", "MIN": 0.001, "MAX": 1.0, "DEFAULT": 0.3 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Anim/Animate Size", "NAME": "animate_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Anim/Speed", "NAME": "speed_size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Shape", "NAME": "animshape_size", "TYPE": "long", "VALUES": ["Out","In","Smooth"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
        { "LABEL": "Anim/Reverse", "NAME": "reverse_size", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Anim/Decay", "NAME": "decay_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Anim/Release", "NAME": "release_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

        { "LABEL": "AnimLuma/Animate Luma", "NAME": "animate_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "AnimLuma/Speed", "NAME": "speed_luma", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "AnimLuma/Shape", "NAME": "animshape_luma", "TYPE": "long", "VALUES": ["Out","In","Smooth","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
        { "LABEL": "AnimLuma/Reverse", "NAME": "reverse_luma", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "AnimLuma/Decay", "NAME": "decay_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "AnimLuma/Release", "NAME": "release_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

        { "Label": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time_size", "TYPE": "time_base", "PARAMS": {"speed": "speed_size", "reverse": "reverse_size", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
        {"NAME": "animation_time_luma", "TYPE": "time_base", "PARAMS": {"speed": "speed_luma", "reverse": "reverse_luma", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

// procedural white noise   
float hash( vec2 p ) {
    return fract(sin(dot(p,vec2(127.1,311.7)))*43758.5453);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float pixelatePow = mat_pixelate * mat_pixelate * mat_pixelate;
    vec2 p = (floor((texCoord-vec2(0.5))/pixelatePow)+vec2(0.5)) * pixelatePow;

    vec2 radius = vec2(0.5);
    float luma = 1.0;

    if (animate_size) {
        float adjusted_bpm_pos = fract(animation_time_size); // Get full value on beat
        if (adjusted_bpm_pos < decay_size) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - decay_size) * 1 / (1 - decay_size);
            if (adjusted_bpm_pos < release_size) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release_size;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

        #if defined(animshape_size_IS_Smooth)
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
        #elif defined(animshape_size_IS_Out)
            adjusted_bpm_pos = adjusted_bpm_pos;
        #elif defined(animshape_size_IS_In)
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
        #else // defined(animshape_size_IS_Cut)
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
        #endif

        radius *= adjusted_bpm_pos;
    }

    if (animate_luma) {
        float adjusted_bpm_pos = fract(animation_time_luma); // Get full value on beat
        if (adjusted_bpm_pos < decay_luma) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - decay_luma) * 1 / (1 - decay_luma);
            if (adjusted_bpm_pos < release_luma) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release_luma;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

        #if defined(animshape_luma_IS_Smooth)
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
        #elif defined(animshape_luma_IS_Out)
            adjusted_bpm_pos = adjusted_bpm_pos;
        #elif defined(animshape_luma_IS_In)
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
        #else // defined(animshape_luma_IS_Cut)
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
        #endif

        luma = adjusted_bpm_pos;
    }

    #if defined(shape_IS_Square)
        float dist = rectangle( p, radius );
        float strokeWidth = stroke_size * 0.5;
    #elif defined(shape_IS_Circle)
        float dist = circle( p, radius.x );
        float strokeWidth = stroke_size * 0.5;
    #elif defined(shape_IS_Hexagon)
        p.x *= 1.15;
        float dist = hexagon( p, radius.x );
        float strokeWidth = stroke_size * 0.5;
    #elif defined(shape_IS_Rounded_Rectangle)
        float dist = roundedRectangle( p, radius, radius.x/3 );
        float strokeWidth = stroke_size * 0.5;
    #else
        p.x *= 0.43333;
        p.x /= radius.x;
        p.y = -0.375 * (p.y-0.16666);
        p.y /= radius.y;
        float dist = triangle( p, 0.5 );
        float strokeWidth = stroke_size * 0.25;
    #endif

    vec4 backColor = vec4(mat_backgroundColor.rgb * brightness, 1);
    vec4 frontColor = vec4(mat_foregroundColor.rgb * brightness, 1);

	float stroke_dist = 4 * smoothstep(-strokeWidth,strokeWidth,dist) * smoothstep(-strokeWidth,strokeWidth,-dist);
	
    vec4 col = vec4(luma * mix(backColor,frontColor,stroke_dist));

    col.a = 1.0;
    return col;
}
