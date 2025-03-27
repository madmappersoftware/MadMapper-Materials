/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture",
    "DESCRIPTION": "Square Array.",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [  
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.0 },
        { "LABEL": "Iterations", "NAME": "iterations", "TYPE": "float", "MIN": 1.0, "MAX": 40.0, "DEFAULT": 6.0 },
        { "LABEL": "Glow", "NAME": "glowpower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Noise Color", "NAME": "nc", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Glow Color", "NAME": "glowColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "noiseLUT", "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#define NOISE_TEXTURE_BASED
#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{

    // first grid
    vec2 posId;
    vec2 pos = ( texCoord  ) * 1.0f;
    vec3 seed;

    pos = repeat(pos, vec2( 1.0 / iterations) ,posId);
    seed = vec3(posId.yx,animation_time);

    vec2 rpos = rotate(pos,rotation);
    float rec = rectangle(rpos + (worleyNoise(vec3(animation_time,posId))-vec2(0.12)*0.10 ),  1.0 / iterations );
    float rec2 = rectangle(-rpos + 0.21*billowedNoise(vec3(posId.yx,animation_time)), vec2(0.5,vnoise(vec3(posId,animation_time))*(1.0 / iterations)*0.7) );
    float rec3 = rectangle(-rpos + vec2(vnoise(seed)) , vec2(flowNoise(seed.xy,seed.z)) );

    float finalShape = subtract(rec,rec2);
    float noiseColor = noise(vec3(posId,animation_time));

    vec4 color = fill( backgroundColor, fillColor, finalShape )*(1.0 - (noiseColor*nc));

    /// 2nd grid
    vec2 lineId;
    vec2 linePos = ( texCoord  ) * 1.0f;

    linePos = repeat(pos, vec2( 0.80 / iterations) ,lineId);
    float lineYnoise = billowedNoise(seed)*0.25;
    float lines = line( linePos, vec2(-1,lineYnoise), vec2(1,lineYnoise), 0.02 );
    vec4 linesColor = fill( backgroundColor, fillColor, lines );

    finalShape = subtract(finalShape,lines);
    color += glow( glowColor, finalShape, 0.5*glowpower )*linesColor;

    return saturate( color );
}
