/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "FMS_Cat, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#40836.0",
  "VSN": "1.0",
  "INPUTS" : [


    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },

    {   "LABEL": "THR",
        "NAME": "THR",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.5
    },

    {   "LABEL": "LoopFreq",
        "NAME": "LOOPFREQ",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 8.0,
        "DEFAULT": 4.0
    },

    {   "LABEL": "Vignette",
        "NAME": "VIG",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 0.0
    },

    {   "LABEL": "Inside Limit",
        "NAME": "inside_limit",
        "TYPE": "int",
        "MIN": 0,
        "MAX": 75,
        "DEFAULT": 2
    },

    {   "LABEL": "Outside Limit",
        "NAME": "outside_limit",
        "TYPE": "int",
        "MIN": 0,
        "MAX": 75,
        "DEFAULT": 75
    },

    {   "LABEL": "Power",
        "NAME": "power",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 2.0
    },

    {
        "LABEL": "BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

// FMS_Cat
// https://twitter.com/FMS_Cat


float iTime = mat_time - mat_offset * 20;

#define SCALE (RENDERSIZE.y)

float v2random( vec2 co ) {
    return fract( sin( dot( co.xy, vec2( 2.9898, 7.233 ) ) ) * 4838.5453 );
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 p = texCoord - vec2(0.5);
    p *= mat_zoom;

    float r = length( p ) * SCALE * 0.1;
    float layer = floor( r );

    if ( (inside_limit < layer) && (outside_limit > layer) ) {
        float theta = ( atan( p.y, p.x ) + PI ) / 2.0 / PI;
        float vel = 0.05 * ( v2random( vec2( layer, 3.155 ) ) - 0.5 );
        float freq = 1.0 + floor( layer * 2.0 * pow( v2random( vec2( layer, 2.456 ) ), 2.0 ) );

        float phase = fract( ( theta + iTime * vel ) * LOOPFREQ ) * freq;
        float phase0 = floor( phase );
        float phase1 = mod( phase0 + 1.0, freq );
        float phasec = fract( phase );

        float state0 = v2random( vec2( layer, phase0 ) ) < THR ? 0.0 : 1.0;
        float state1 = v2random( vec2( layer, phase1 ) ) < THR ? 0.0 : 1.0;
        float state = mix( state0, state1, smoothstep( 0.0, 0.5 / SCALE * LOOPFREQ * freq / length( p ), phasec ) );

        vec3 col = vec3( state );

        float layerc = mod( r, 1.0 );
        col *= smoothstep( 0.0, 0.0 + 0.4, layerc );
        col *= smoothstep( 0.6, 0.6 - 0.4, layerc );

        col *= power;

        col *= exp( -VIG * length( p ) );

        return vec4( col, 1.0 );
    } else {
        return vec4( 0.0, 0.0, 0.0, 1.0 );
    }


}