/*{
    "INPUTS": [  
        { "NAME": "PALETTE", "Label": "Palette", "TYPE": "long", "DEFAULT": "Palette01", "FLAGS": "generate_as_define", "VALUES": 
            [ "Palette01", "Palette02", "Palette03", "Palette04", "Palette05", "Palette06", "Palette07", "Palette08", "Palette09", "Palette10", "Palette11" ] },        
        { "NAME": "AUDIO", "LABEL": "Audio", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define" },
        { "NAME": "ROTATING", "LABEL": "Rotating", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define" },
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05 },  
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2.0 },  
        { "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float", "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025 },        
        { "NAME": "spectrum_128", "TYPE": "audioFFT", "SIZE": 128, 
            "ATTACK": 0.2, "DECAY": 0.0, "RELEASE": 0.6 }
    ],
    "GENERATORS": [
        {"NAME": "time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}}
    ],
    "IMPORTED": {
        "noiseLUT": { "PATH": "../textures/noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },"gradient1": { "PATH": "../textures/gradientTest01.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette01": { "PATH": "../textures/palette01.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette02": { "PATH": "../textures/palette02.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette03": { "PATH": "../textures/palette03.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette04": { "PATH": "../textures/palette04.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette05": { "PATH": "../textures/palette05.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette06": { "PATH": "../textures/palette06.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette07": { "PATH": "../textures/palette07.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette08": { "PATH": "../textures/palette08.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette09": { "PATH": "../textures/palette09.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette10": { "PATH": "../textures/palette10.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
        "palette11": { "PATH": "../textures/palette11.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" },
    
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // cellId usage example
    vec2 p = ( texCoord - vec2( 0.5 ) );
    float dist = 1;
    vec2 cellId;

    float cellSize = 0.05 * scale;
    p = repeat( p, vec2( cellSize ), cellId );

    float signedNoise = noise( cellId * 10.0 );
    float unsignedNoise = signedNoise * 0.5 + 0.5;
    
#if defined( ROTATING_IS_true )
    p = rotate( p, signedNoise + time * sign( signedNoise ) );
#endif

    float scl = cellSize * unsignedNoise;
#if defined( AUDIO_IS_true )
    scl *= texture( spectrum_128, vec2( signedNoise, 0 ) ).r * 2.0;
#endif

    dist = blend( rectangle( p, scl * vec2( 0.333, 0.075 ) ), rectangle( p, scl * vec2( 0.075, 0.333 ) ) );


    unsignedNoise = 1.0 - unsignedNoise;

#if defined( PALETTE_IS_Palette01 )
    vec4 backgroundColor = texture( palette01, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette01, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette02 )
    vec4 backgroundColor = texture( palette02, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette02, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette03 )
    vec4 backgroundColor = texture( palette03, vec2( 0.2, 0.5f ) );
    vec4 fillColor = texture( palette03, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette04 )
    vec4 backgroundColor = texture( palette04, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette04, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette05 )
    vec4 backgroundColor = texture( palette05, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette05, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette06 )
    vec4 backgroundColor = texture( palette06, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette06, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette07 )
    vec4 backgroundColor = texture( palette07, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette07, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette08 )
    vec4 backgroundColor = texture( palette08, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette08, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette09 )
    vec4 backgroundColor = texture( palette09, vec2( 0.8, 0.5f ) );
    vec4 fillColor = texture( palette09, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette10 )
    vec4 backgroundColor = texture( palette10, vec2( 0.9, 0.5f ) );
    vec4 fillColor = texture( palette10, vec2( unsignedNoise, 0.5f ) );
#elif defined( PALETTE_IS_Palette11 )
    vec4 backgroundColor = texture( palette11, vec2( 0.5, 0.5f ) );
    vec4 fillColor = texture( palette11, vec2( unsignedNoise, 0.5f ) );
#endif

    vec4 col = fill( backgroundColor, fillColor, dist );
    col.a = 1.0;
    return saturate( col );
}
