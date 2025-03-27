/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Radial Spectrum.",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [  
        { "LABEL": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "LABEL": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.001, "MAX": 0.1, "DEFAULT": 0.01 },  
        { "LABEL": "Strength", "NAME": "strength", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.8 },  
        { "LABEL": "Radius", "NAME": "radius", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.25 },  
        { "LABEL": "Repeat", "NAME": "repeat_count", "TYPE": "float", "MIN": 2.0, "MAX": 1000, "DEFAULT": 128 },  
        { "LABEL": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.001 },        
        { "NAME": "spectrum_128", "TYPE": "audioFFT", "SIZE": 128, "ATTACK": 0.2, "DECAY": 0.0, "RELEASE": 0.6 },
        { "Label": "Rotate/Rotate", "NAME": "rotation_active", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },  
        { "Label": "Rotate/Speed", "NAME": "rotation_speed", "TYPE": "float", "MIN": 0, "MAX": 3, "DEFAULT": 0.0 },
        { "LABEL": "Rotate/Reverse", "NAME": "rotation_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
    ],
    "GENERATORS": [
        {"NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "reverse": "rotation_reverse", "speed_curve": 3, "link_speed_to_global_bpm":true}}
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // cellId usage example
    vec2 p = texCoord;
    float dist = 1;
    vec2 cellId;

    p = translate( p, vec2( 0.5 ) );
    if (rotation_active) p = rotate( p, rotation_time );
    p = repeatRadial( p, ( PI * 2.0 ) / repeat_count, cellId );
    p = translate( p, vec2( radius, 0.0 ) );
    
    float size = texture( spectrum_128, vec2( noise( cellId * scale ), 0 ) ).r * 0.01;
    dist = rectangle( p, vec2( size * 15.0 * strength, size * 0.5 ) );

    vec4 col = fill( backgroundColor, fillColor, dist );
    col = stroke( col, strokeColor, dist, strokeWidth );
    col.a = 1.0;
    return saturate( col );
}
