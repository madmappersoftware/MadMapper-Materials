/*{
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Plane Deform.",
    "VSN": "1.0",
    "TAGS": "geometry",
    "INPUTS": [
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 },
        { "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
        { "Label": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "Label": "Rotate/Rotate", "NAME": "rotation_active", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "Label": "Rotate/Rotation", "NAME": "rotation_speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },  
        { "Label": "Color/Fill", "NAME": "fillColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        { "NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 3, "link_speed_to_global_bpm":true} },
        { "NAME": "rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "rotation_speed", "speed_curve": 3, "link_speed_to_global_bpm":true} }
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // cellId usage example
    vec2 p = ( texCoord - vec2( 0.5 ) ) * 4.0f;
    float dist = 1;
    vec2 cellId;

    // plane deformation
    vec2 p2;
    if (rotation_active) {
        p2 = mat2( cos( rotation_time ), -sin( rotation_time ), sin( rotation_time ), cos( rotation_time ) ) * p.xy;
    } else {
        p2 = p.xy;
    }
    p.xy = vec2( p2.x, 1.0 ) / abs( p2.y ) + vec2( 0, animation_time );

    // distance field
    p = repeat( p, vec2( 0.1 * scale ), cellId );
    float size = 0.1* scale * ( noise( vec3( cellId * 0.5, abs( p2.y ) + animation_time * 0.25 ) ) );
    dist = rectangle( p, size * vec2( 0.1, 1 ) );

    // coloring
    vec3 color = fill( backgroundColor.rgb, fillColor.rgb, dist );
    color += glow( vec3(0.5), dist, 0.02 );
    color += glow( vec3(0.25), dist, 0.04 );

    color *= vec3( smoothstep( 0.0, 0.5, abs( p2.y ) ) );
    color = clamp(color,0,1);

    // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1 );
}
