/*{
    "INPUTS": [  
        { "Label": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "Label": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 }
    ]
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p = texCoord - vec2( 0.5 );
   // vec2 Pi = floor(p);
    //vec2 Pf = p - Pi;

    float dist = 1;
    // grid
    float cellSize = 0.5 * scale;
    p = repeat( p, vec2( cellSize ) );

    // cross
    dist = blend(
            rectangle( p, cellSize * vec2( 0.333, 0.075 ) ), 
            rectangle( p, cellSize * vec2( 0.075, 0.333 ) ) 
            );

    // circle
   // dist = circle( p, cellSize * 0.25 );

    // background
    vec4 col = backgroundColor;
    // fill
    col = fill( col, fillColor, dist );
    // stroke
    col = stroke( col, strokeColor, dist, strokeWidth );

    col.a = 1.0;

    return saturate( col );
}
