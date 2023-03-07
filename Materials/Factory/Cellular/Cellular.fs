/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Cellular noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.00001, "MAX": 20.0, "DEFAULT": 2.0 },  
        { "LABEL": "Noise Speed", "NAME": "zspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Speed X", "NAME": "xspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Speed Y", "NAME": "yspeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 },  
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 5.0, "DEFAULT": 1 },
        { "LABEL": "Color/Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": 0 },  
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed", "speed_curve": 4, "link_speed_to_global_bpm":true}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed", "speed_curve": 4, "reverse": "reverse", "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": {
        "noiseLUT": { "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" }
    }
}*/

/*
Glsl Material

Copyright (c) 2016, Garage Cube, All rights reserved.
Copyright (c) 2016, Simon Geilfus, All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that
the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and
 the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
 the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.
*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"


vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv = vec3( texCoord * scale + vec2( xtime, ytime ), ztime);

    // Simplex Noise
    float n = 2 * worleyNoise( uv );

    if (invert) n = 1-n;

    // Interpolate Color
    vec3 color  = mix( backgroundColor.rgb, foregroundColor.rgb, n );
 
     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
