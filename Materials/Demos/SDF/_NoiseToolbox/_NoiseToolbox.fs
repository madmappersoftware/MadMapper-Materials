/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mm team",
    "CATEGORIES": [
        "Image Control"
    ],
    "INPUTS": [
        {
            "NAME": "NOISE_TYPE",
            "Label": "Noise Type",
            "TYPE": "long",
            "VALUES": [
                "VALUE", 
                "SIMPLEX", 
                "WORLEY",
                "FLOW",
                "BILLOWED",
                "RIDGED",
                "FBM",
                "BILLOWY_TUBULENCE",
                "RIDGED_MF"
            ],
            "DEFAULT": "VALUE",
            "FLAGS": "generate_as_define"
        },
        {
            "Label": "Back Color",
            "NAME": "backgroundColor",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },
        {
            "Label": "Front Color",
            "NAME": "foregroundColor",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "Label": "Scale",
            "NAME": "scale",
            "TYPE": "float",
            "MIN": 0.00001,
            "MAX": 100.0,
            "DEFAULT": 0.5
        },
        {
            "Label": "ScaleX",
            "NAME": "xscale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "ScaleY",
            "NAME": "yscale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Octaves",
            "NAME": "octaves",
            "TYPE": "int",
            "MIN": 2,
            "MAX": 10,
            "DEFAULT": 3
        },
        {
            "Label": "Lacunarity",
            "NAME": "lacunarity",
            "TYPE": "float",
            "MIN": -5.0,
            "MAX": 5.0,
            "DEFAULT": 2.0
        },
        {
            "Label": "Gain",
            "NAME": "gain",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "Label": "Ridge Offset",
            "NAME": "ridgeOffset",
            "TYPE": "float",
            "MIN": -2.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "TEX_BASED",
            "LABEL": "LUT Based",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define"
        },
        {
            "NAME": "ANIMATED",
            "LABEL": "Animated",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define"
        },
        {
            "Label": "Anim Speed X",
            "NAME": "xspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Anim Speed Y",
            "NAME": "yspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Anim Speed Z",
            "NAME": "zspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.05
        }
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed"}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed"}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed"}}
    ],
    "IMPORTED": {
        "noiseLUT": {
            "PATH": "noiseLUT.png",
            "GL_TEXTURE_MIN_FILTER": "LINEAR",
            "GL_TEXTURE_MAG_FILTER": "LINEAR",
            "GL_TEXTURE_WRAP": "REPEAT",
        }
    }
}*/

#ifdef TEX_BASED_IS_true
    #define NOISE_TEXTURE_BASED
#endif

#include "MadNoise.glsl"

//TODO: Fix derivatives sums...

vec4 getIntegerAndFractionalCoords( vec2 uv )
{
    vec2 iuv = floor( uv );
    return vec4( iuv, uv - iuv );
}

vec4 materialColorForPixel( vec2 texCoord )
{

    #if defined( ANIMATED_IS_true )
    vec3 uv = vec3( texCoord * scale * vec2( xscale, yscale ) + vec2( xtime, ytime ), ztime );
    #else
    vec2 uv = texCoord * scale * vec2( xscale, yscale );
    #endif

    float n = 0.0f;

    // Value Noise
    #if defined( NOISE_TYPE_IS_VALUE )
        n = vnoise( uv * 2.0f );

    // Simplex Noise
    #elif defined( NOISE_TYPE_IS_SIMPLEX )
        n = noise( uv ) * 0.5f + 0.5f;
        
    #elif defined( NOISE_TYPE_IS_WORLEY )
        n = worleyNoise( uv );
        
    #elif defined( NOISE_TYPE_IS_FLOW )
        #if defined( ANIMATED_IS_true )
            n = flowNoise( uv.xy + vec2( xtime, ytime ), ztime ) * 0.5f + 0.5f;
        #else
            n = flowNoise( uv, 0.0f ) * 0.5f + 0.5f;
        #endif
        
    #elif defined( NOISE_TYPE_IS_BILLOWED )
        n = billowedNoise( uv );
        
    #elif defined( NOISE_TYPE_IS_RIDGED )
        n = ridgedNoise( uv );
        
    #elif defined( NOISE_TYPE_IS_FBM )
        n = fBm( uv, octaves, lacunarity, gain ) * 0.5f + 0.5f;
        
    #elif defined( NOISE_TYPE_IS_BILLOWY_TUBULENCE )
        n = billowyTurbulence( uv, octaves, lacunarity, gain );
        
    #elif defined( NOISE_TYPE_IS_RIDGED_MF )
        n = ridgedMF( uv, ridgeOffset, octaves, lacunarity, gain );
        
    #endif

    return vec4( mix( backgroundColor.rgb, foregroundColor.rgb, n ), 1.0f );
}
