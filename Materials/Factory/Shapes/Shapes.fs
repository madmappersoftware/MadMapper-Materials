/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Shape animations, repeated and offsetted, very usefull for mapping.",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Cells/Cells X", "NAME": "mat_cells_x", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Cells Y", "NAME": "mat_cells_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },

        { "LABEL": "Global/Shape", "NAME": "mat_shape", "TYPE": "long", "VALUES": ["Square", "Triangle","Circle","Hexagon","Rounded Rectangle"], "DEFAULT": "Square", "FLAGS": "generate_as_define" },
        { "LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1 },
        { "LABEL": "Global/Stroke Size", "NAME": "mat_stroke_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1 },
        { "LABEL": "Global/Polar Coord", "NAME": "mat_polar_coordinates", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Auto Size/Active", "NAME": "mat_animate_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Auto Size/Speed", "NAME": "mat_speed_size", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Reverse", "NAME": "mat_reverse_size", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Auto Size/Decay", "NAME": "mat_decay_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Size/Release", "NAME": "mat_release_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Shape", "NAME": "mat_animshape_size", "TYPE": "long", "VALUES": ["In","Smooth","Cut"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
        { "LABEL": "Auto Size/Offset", "NAME": "mat_offset_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Size/Random Off.", "NAME": "mat_random_offset_size", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Auto Luma/Active", "NAME": "mat_animate_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Auto Luma/Speed", "NAME": "mat_speed_luma", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Reverse", "NAME": "mat_reverse_luma", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Auto Luma/Decay", "NAME": "mat_decay_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Auto Luma/Release", "NAME": "mat_release_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Shape", "NAME": "mat_animshape_luma", "TYPE": "long", "VALUES": ["In","Smooth","Cut"], "DEFAULT": "Smooth", "FLAGS": "generate_as_define" },
        { "LABEL": "Auto Luma/Offset", "NAME": "mat_offset_luma", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Auto Luma/Random Off.", "NAME": "mat_random_offset_luma", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "Label": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "Label": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
    ],
    "GENERATORS": [
        {"NAME": "mat_animation_time_size", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_size", "reverse": "mat_reverse_size", "strob": 0, "bpm_sync": "mat_bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
        {"NAME": "mat_animation_time_luma", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_luma", "reverse": "mat_reverse_luma", "strob": 0, "bpm_sync": "mat_bpmsync", "speed_curve":3, "link_speed_to_global_bpm":true }},
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

    vec2 cellSize   = vec2( 1.0 / mat_cells_x, 1.0 / mat_cells_y );
    p               = repeat( p, cellSize, cellId );

    if (mat_polar_coordinates) {
        vec2 x = 2 * (p/cellSize);
        p.x = length(x) - 0.5;
        p.y = atan(x.y, x.x) * 0.5 / PI;
        p *= cellSize;
    }

    vec2 radius = cellSize * 0.5;
    float luma = 1.0;

    if (mat_animate_size) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (mat_random_offset_size)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*mat_cells_x) / (mat_cells_x*mat_cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = mat_animation_time_size;
        if (mat_reverse_size) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - mat_offset_size * normalizedCellId); // Get full value on beat
        if (adjusted_bpm_pos < mat_decay_size) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - mat_decay_size) * 1 / (1 - mat_decay_size);
            if (adjusted_bpm_pos < mat_release_size) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / mat_release_size;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

        #if defined(mat_animshape_size_IS_Smooth)
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
        #elif defined(mat_animshape_size_IS_In)
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
        #else // defined(mat_animshape_size_IS_Cut)
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
        #endif

        radius *= adjusted_bpm_pos;
    }

    if (mat_animate_luma) {
        float normalizedCellId; // Value from 0.0 to 1.0
        if (mat_random_offset_luma)
            normalizedCellId = hash(cellId * 0.5);
        else
            normalizedCellId = (cellId.x + cellId.y*mat_cells_x) / (mat_cells_x*mat_cells_y); // Number from 0.0 to 1.0

        // When restarting media, animation_time_size is zero on first frame, so it might show full size for one frame
        // before going back to 0 because of the fract(...)
        float animation_time = mat_animation_time_luma;
        if (mat_reverse_luma) animation_time-=0.000001;

        float adjusted_bpm_pos = fract(animation_time - mat_offset_luma * normalizedCellId); // Get full value on beat
        if (adjusted_bpm_pos < mat_decay_luma) {
            adjusted_bpm_pos = 1;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            adjusted_bpm_pos = (adjusted_bpm_pos - mat_decay_luma) * 1 / (1 - mat_decay_luma);
            if (adjusted_bpm_pos < mat_release_luma) {
                adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / mat_release_luma;
            } else {
                adjusted_bpm_pos = 0;
            }
        }

        #if defined(mat_animshape_luma_IS_Smooth)
            adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
        #elif defined(mat_animshape_luma_IS_In)
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
        #else // defined(mat_animshape_luma_IS_Cut)
            adjusted_bpm_pos = step(0.5,adjusted_bpm_pos);
        #endif

        luma = adjusted_bpm_pos;
    }

    radius *= mat_size;

    // * 1.01 so SDF_ANTIALIASING_MEDIUM doesn't let some back around when size = 1
    radius *= 1.01;
    float stroke_size = mat_stroke_size * 1.01;

    #if defined(mat_shape_IS_Square)
        float dist = rectangle( p, radius );
        float strokeWidth = stroke_size * 0.5 / min(mat_cells_x,mat_cells_y);
    #elif defined(mat_shape_IS_Circle)
        p.y *= float(mat_cells_y) / mat_cells_x;
        float dist = circle( p, radius.x );
        float strokeWidth = stroke_size * 0.5 / min(mat_cells_x,mat_cells_y);
    #elif defined(mat_shape_IS_Hexagon)
        p.y *= float(mat_cells_y) / mat_cells_x;
        p.x *= 1.15;
        float dist = hexagon( p, radius.x );
        float strokeWidth = stroke_size * 0.5 / min(mat_cells_x,mat_cells_y);
    #elif defined(mat_shape_IS_Rounded_Rectangle)
        float dist = roundedRectangle( p, radius, radius.x/3 );
        float strokeWidth = stroke_size * 0.5 / min(mat_cells_x,mat_cells_y);
    #else
        p.x *= 0.43333;
        p.x /= radius.x;
        p.y = -0.375 * (p.y-0.16666 / mat_cells_y);
        p.y /= radius.y;
        float dist = triangle( p, 0.5 );
        float strokeWidth = stroke_size * 0.25;
    #endif

    vec4 col = stroke( mat_backgroundColor, vec4(luma * mat_foregroundColor.rgb,mat_foregroundColor.a), dist, strokeWidth );

    return col;
}
