/*{
    "INPUTS": [  
        { "Label": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "Label": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05 },  
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2.0 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 }
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}}
    ],
    "IMPORTED": {
        "noiseLUT": { "PATH": "../textures/noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" }
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // cellId usage example
    vec2 p = ( texCoord - vec2( 0.5 ) ) * scale;
    float dist = 1;
    vec2 cellId;

    p = repeat( p, vec2( 0.1 ), cellId );
    
    float size = 0.05 * ( noise( vec3( cellId * 0.5, mat_time ) ) * 0.5 + 0.5 );
    dist = rectangle( p, size );

    vec4 col = fill( backgroundColor, fillColor, dist );
    col = stroke( col, strokeColor, dist, strokeWidth );
    col.a = 1.0;
    return saturate( col );
}
