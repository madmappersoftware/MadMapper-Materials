/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Should map wall bricks in many situations.",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Mortar Color", "NAME": "mortarColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Brick Color", "NAME": "brickColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },  
        { "LABEL": "Mortar Width", "NAME": "mortarWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 },
        { "LABEL": "Step", "NAME": "stepper", "TYPE": "int", "MIN" : 1, "MAX" : 5, "DEFAULT": 2 },
        { "LABEL": "Offset", "NAME": "offset", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Random/Random Colors", "NAME": "RANDOM_COLORS", "TYPE": "bool", "DEFAULT": true, "FLAGS": "generate_as_define,button" },
        { "LABEL": "Random/Speed", "NAME": "random_speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 4.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
    ],
    "GENERATORS": [
        { "NAME": "random_time", "TYPE": "time_base", "PARAMS": {"speed": "random_speed", "link_speed_to_global_bpm":true} }
    ]
}*/

#define SDF_ANTIALIASING_NONE 

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 cellId;
    vec2 p          = texCoord;
    vec2 brickSize  = vec2( 0.1, 0.075 ) * scale;
    vec2 offsetVec  = vec2( 0 );
    if(mod(texCoord.y / brickSize.y, stepper) < 1){
        p.x += offset * brickSize.x;
    }
    p               = repeat( p, brickSize, offsetVec, vec2( 2 ), cellId );
    float dist      = rectangle( p, brickSize * 0.5 - vec2( mortarWidth ) );

    // coloring 
#if ! defined( RANDOM_COLORS_IS_true )
    vec4 fillColor = brickColor;
#else
    vec4 fillColor = brickColor * billowedNoise( vec3(cellId, random_time) );
#endif
    vec3 color = fill( vec3(0), fillColor.rgb, dist );
    color = stroke( color, mortarColor.rgb, dist, mortarWidth );

    // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += brightness;

    return vec4(color, 1.0);

}
