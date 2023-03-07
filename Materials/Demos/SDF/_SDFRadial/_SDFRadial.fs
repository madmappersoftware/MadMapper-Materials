/*{
    "CREDIT": "mm team",
    "CATEGORIES": [
        "Image Control"
    ],
    "INPUTS": [ 
        { "Label": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "Label": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 }
    ],
    "GENERATORS": [
        {"NAME": "time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}}
    ],
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p = texCoord;
    
    // rect radial grid
    p = translate( p, vec2( 0.5, 0.5 ) );
    p = repeatRadial( p, 0.1 );
    p = translate( p, vec2( 0.1, 0.0 ) );
    p = repeat( p, vec2( 0.04, 0.0 ) );
    
    float dist = rectangle( p, vec2( 0.015, 0.01 - 0.035 * length( texCoord - vec2(0.5) ) ) );

    // background
    vec4 col = backgroundColor;
    // fill
    col = fill( col, fillColor, dist );
    // stroke
    col = stroke( col, strokeColor, dist, strokeWidth );

    col.a = 1.0;

    return saturate( col );
}
