/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Teamm",
    "CATEGORIES": [ "Graphic, Tool" ],
    "DESCRIPTION": "Fills part of the media with manual controls & auto",
    "TAGS": "grid,geometry",
    "VSN": "1.0",
    "INPUTS": [
        {
            "NAME": "mat_width",
            "Label": "Width",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_translate",
            "Label": "Translate",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_repeat",
            "Label": "Repeat",
            "TYPE": "int",
            "DEFAULT": 1,
            "MIN": 1,
            "MAX": 40
        },
        {
            "NAME": "mat_smoothness_in",
            "Label": "Smooth In",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_smoothness_out",
            "Label": "Smooth Out",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        { "LABEL": "Auto Move/Active", "NAME": "mat_automoveactive", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Auto Move/Size", "NAME": "mat_automovesize", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Auto Move/Speed", "NAME": "mat_automovespeed", "TYPE": "float", "DEFAULT": 1, "MIN": 0.0, "MAX": 3.0 },
        { "LABEL": "Auto Move/Reverse", "NAME": "mat_automovereverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Auto Move/Shape", "NAME": "mat_automoveshape", "TYPE": "long", "VALUES": ["Smooth","In","Linear","Cut","Noise","Smooth In"], "DEFAULT": "In", "FLAGS": "generate_as_define" },
        { "LABEL": "Color/Front Color", "NAME": "mat_foreground_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "mat_background_color", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
    ],
    "GENERATORS": [
        {
            "NAME": "mat_move_position",
            "TYPE": "time_base",
            "PARAMS": {"speed": "mat_automovespeed","reverse": "mat_automovereverse", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true }
        }
    ]
}*/

#define M_PI 3.1415926535897932384626433832795

#include "MadNoise.glsl"

vec4 materialColorForPixel(vec2 texCoord)
{
    float x = texCoord.x*mat_repeat - mat_translate;
    if (mat_automoveactive) {
        float translateX = 0;
        if (mat_automoveshape == 0) {
            translateX = mat_automovesize * sin(mat_move_position*2*M_PI) / 2;
        } else if (mat_automoveshape == 1) {
            translateX = mat_automovesize * (0.5-fract(mat_move_position));
        } else if (mat_automoveshape == 2) {
            translateX = mat_automovesize * (0.5-abs(mod(mat_move_position*2+1,2)-1));
        } else if (mat_automoveshape == 3) {
            translateX = mat_automovesize * (0.5-step(0.5,fract(mat_move_position)));
        } else if (mat_automoveshape == 4) {
            translateX = mat_automovesize * (0.5-vnoise(vec2(mat_move_position,0)));
        } else if (mat_automoveshape == 5) {
            translateX = mat_automovesize * (-0.5 * sin(-M_PI/2 + fract(mat_move_position)*M_PI));
        }
        x += translateX;
    }
    x= fract(x);
    float value;
    if (x<mat_width) {
        value = 1;
        float normalizedPosInActivePart = x/mat_width;
        if (normalizedPosInActivePart < mat_smoothness_in) {
            value *= normalizedPosInActivePart/mat_smoothness_in;
        }
        if (1-normalizedPosInActivePart < mat_smoothness_out) {
            value *= (1-normalizedPosInActivePart)/mat_smoothness_out;
        }
    } else {
        value = 0;
    }
    return mix(mat_background_color,mat_foreground_color,value);
}
