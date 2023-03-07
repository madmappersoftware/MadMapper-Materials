/*{
    "INPUTS": [  
        { "Label": "Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "Label": "Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ] },  
        { "Label": "Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05 },  
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 }
    ],
    "GENERATORS": [
        {"NAME": "time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}}
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
    vec2 p = texCoord;
    float dist = 1;
    vec2 cellId;

    p = translate( p, vec2( 0.5 ) );
    p = rotate( p, time );
    p = repeatRadial( p, ( PI * 2.0 ) / 35.0 * scale, cellId );
    p = translate( p, vec2( 0.25, 0.0 ) );
    
    float size = 0.015 * scale * ( noise( vec3( cellId * 0.5, time ) ) * 0.5 + 0.5 );
    dist = rectangle( p, vec2( size * 5.0, size ) );

    vec4 col = fill( backgroundColor, fillColor, dist );
    col = stroke( col, strokeColor, dist, strokeWidth );
    col.a = 1.0;
    return saturate( col );
}
