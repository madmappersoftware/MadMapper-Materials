/*{
    "INPUTS": [  
        { "Label": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "Label": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05 },  
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 }
    ],
    "GENERATORS": [
        {"NAME": "time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "bpm_sync": "bpmsync", "speed_curve": 2}}
    ],
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"

float regularCross( vec2 p, vec2 size )
{
    return blend( rectangle( p, size.xy ), rectangle( p, size.yx ) );
}

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p = texCoord;
    
    float cellSize = 0.015 * scale;

    // start with a radial grid of cross
    p = translate( texCoord, vec2( 0.5 ) );    
    p = repeatRadial( p, 0.3, time );
    p = translate( p, vec2( 0.2, 0.0 ) );    
    float dist = regularCross( p, vec2( 0.333, 10.075 ) * cellSize );

    // and substract a larger radial grid of rectangles
    p = translate( texCoord, vec2( 0.5 ) );    
    p = repeatRadial( p, 0.8, time * 4.5 );
    p = translate( p, vec2( 0.4, 0.0 ) );    
    dist = subtract( dist, rectangle( p, vec2( 2 ) * cellSize ) );

    // and finally only keep the intersection of the previous
    // field with a rotating grid of circles
    float circleGridCellSize = 0.025;
    p = translate( texCoord, vec2( 0.5 ) );  
    p = rotate( p, time / 2.0 );
    p = translate( p, vec2( circleGridCellSize * 0.5 ) );  
    p = repeat( p, vec2( circleGridCellSize ) );
    dist = intersect( dist, circle( p, circleGridCellSize * 0.25 ) );   

    // coloring
    vec4 col = fill( backgroundColor, fillColor, dist );
    col = stroke( col, strokeColor, dist, strokeWidth );
    col.a = 1.0;
    return saturate( col );
}
