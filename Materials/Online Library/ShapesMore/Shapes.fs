/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Shape animations, repeated and offsetted, very usefull for mapping.",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon","Rounded Rectangle"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        { "LABEL": "Size", "NAME": "size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1 },
        { "LABEL": "Stroke Size", "NAME": "stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Cells/Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 1 },
        { "LABEL": "Cells/Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 1 },

        { "LABEL": "Anim/Animate Size", "NAME": "animate_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Anim/Speed", "NAME": "speed_size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Reverse", "NAME": "reverse_size", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Anim/Decay", "NAME": "decay_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Anim/Release", "NAME": "release_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Shape", "NAME": "animshape_size", "TYPE": "long", "VALUES": ["Out","In","Smooth","Cut"], "DEFAULT": "Out", "FLAGS": "generate_as_define" },
        { "LABEL": "Anim/Offset", "NAME": "offset_size", "TYPE": "float", "MIN": 0.0, "MAX": 16.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim/Random Off.", "NAME": "random_offset_size", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "AnimLuma/Animate Luma", "NAME": "animate_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "AnimLuma/Speed", "NAME": "speed_luma", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "AnimLuma/Reverse", "NAME": "reverse_luma", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "AnimLuma/Decay", "NAME": "decay_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "AnimLuma/Release", "NAME": "release_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "AnimLuma/Shape", "NAME": "animshape_luma", "TYPE": "long", "VALUES": ["Out","In","Smooth","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
        { "LABEL": "AnimLuma/Offset", "NAME": "offset_luma", "TYPE": "float", "MIN": 0.0, "MAX": 16.0, "DEFAULT": 1.0 },
        { "LABEL": "AnimLuma/Repeat", "NAME": "repeat_luma", "TYPE": "float", "MIN": 1.0, "MAX": 32.0, "DEFAULT": 1.0 },
        { "LABEL": "AnimLuma/Random Off.", "NAME": "random_offset_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
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
    vec2 cellId;
    vec2 p          = texCoord;
    vec2 cellSize   = vec2( 1.0 / cells_x, 1.0 / cells_y );
    p               = repeat( p, cellSize, cellId );

    vec2 radius = cellSize * 0.5;
    float luma = 1.0;

    if (animate_size) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (random_offset_size)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = animation_time_size;
        if (reverse_size) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - offset_size * normalizedCellId); // Get full value on beat
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
        float normalizedCellId; // Value from 0.0 to 1.0
        if (random_offset_luma)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = animation_time_luma;
        if (reverse_luma) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - offset_luma * normalizedCellId * repeat_luma); // Get full value on beat
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

    radius *= size;

    #if defined(shape_IS_Square)
        float dist = rectangle( p, radius );
        float strokeWidth = stroke_size * 0.5 / min(cells_x,cells_y);
    #elif defined(shape_IS_Circle)
        p.y *= float(cells_y) / cells_x;
        float dist = circle( p, radius.x );
        float strokeWidth = stroke_size * 0.5 / min(cells_x,cells_y);
    #elif defined(shape_IS_Hexagon)
        p.y *= float(cells_y) / cells_x;
        p.x *= 1.15;
        float dist = hexagon( p, radius.x );
        float strokeWidth = stroke_size * 0.5 / min(cells_x,cells_y);
    #elif defined(shape_IS_Rounded_Rectangle)
        float dist = roundedRectangle( p, radius, radius.x/3 );
        float strokeWidth = stroke_size * 0.5 / min(cells_x,cells_y);
    #else
        p.x *= 0.43333;
        p.x /= radius.x;
        p.y = -0.375 * (p.y-0.16666 / cells_y);
        p.y /= radius.y;
        float dist = triangle( p, 0.5 );
        float strokeWidth = stroke_size * 0.25;
    #endif

    vec4 backColor = vec4(backgroundColor.rgb * brightness, 1);
    vec4 frontColor = vec4(foregroundColor.rgb * brightness, 1);

    vec4 col = luma * stroke( backColor, frontColor, dist, strokeWidth );

    col.a = 1.0;
    return col;
}
