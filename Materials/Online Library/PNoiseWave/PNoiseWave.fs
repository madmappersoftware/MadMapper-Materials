/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "mojovideotech, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#39961.5",
  "VSN": "1.0",
  "INPUTS" : [

    {
        "LABEL":        "Center",
        "NAME" :        "center",
        "TYPE" :        "point2D",
        "DEFAULT" :     [ -0.5, 1.5 ],
        "MAX" :         [ 2.0, 2.0 ],
        "MIN" :         [ -2.0, -2.0 ]
    },
    {
        "LABEL":        "Flip",
        "NAME":         "flip",
        "TYPE":         "bool",
        "FLAGS":        "button",
        "DEFAULT":      "FALSE"
    },


    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL":        "Wave/Scale Y",
        "NAME" :        "mat_wave_scale_y",
        "TYPE" :        "float",
        "DEFAULT" :     1.0,
        "MIN" :         0.0,
        "MAX" :         2.0
    },

    {
        "LABEL":        "Wave/Seed1",
        "NAME" :        "seed1",
        "TYPE" :        "float",
        "DEFAULT" :     13,
        "MIN" :         8,
        "MAX" :         233
    },
    {
        "LABEL":        "Wave/Seed2",
        "NAME" :        "seed2",
        "TYPE" :        "float",
        "DEFAULT" :     59,
        "MIN" :         55,
        "MAX" :         98
    },
    {
        "LABEL":        "Wave/Seed3",
        "NAME" :        "seed3",
        "TYPE" :        "float",
        "DEFAULT" :     2933227,
        "MIN" :         75025,
        "MAX" :         3524578
    },

    {
        "LABEL":        "Wave/Freq1",
        "NAME" :        "freq",
        "TYPE" :        "float",
        "DEFAULT" :     47.0,
        "MIN" :         3.0,
        "MAX" :         73.0
    },
    {
        "LABEL":        "Wave/Freq2",
        "NAME" :        "freq2",
        "TYPE" :        "float",
        "DEFAULT" :     25.0,
        "MIN" :         2.0,
        "MAX" :         41.0
    },
    {
        "LABEL":        "Wave/Freq3",
        "NAME" :        "freq3",
        "TYPE" :        "float",
        "DEFAULT" :     3.0,
        "MIN" :         1.0,
        "MAX" :         11.0
    },
    {
        "LABEL": "Wave/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Wave/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Wave/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Wave/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Wave/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL":        "Color/Rx",
        "NAME" :        "Rx",
        "TYPE" :        "float",
        "DEFAULT" :     0.99,
        "MIN" :         0.0,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Gx",
        "NAME" :        "Gx",
        "TYPE" :        "float",
        "DEFAULT" :     0.55,
        "MIN" :         0.0,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Bx",
        "NAME" :        "Bx",
        "TYPE" :        "float",
        "DEFAULT" :     0.8,
        "MIN" :         0.0,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Offset1",
        "NAME" :        "offset",
        "TYPE":         "float",
        "DEFAULT" :     0.4,
        "MIN" :         0.1,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Offset2",
        "NAME" :        "offset2",
        "TYPE":         "float",
        "DEFAULT" :     0.5,
        "MIN" :         0.1,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Offset3",
        "NAME" :        "offset3",
        "TYPE":         "float",
        "DEFAULT" :     0.25,
        "MIN" :         0.1,
        "MAX" :         1.0
    },
    {
        "LABEL":        "Color/Invert",
        "NAME":         "invert",
        "TYPE":         "bool",
        "FLAGS":        "button",
        "DEFAULT":      "FALSE"
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

float iTime = mat_time - mat_offset * 10;


////////////////////////////////////////////////////////////
// PNoiseWaveFactory  by mojovideotech
//
// based on :
// glslsandbox/e#39961.5
//
// Creative Commons Attribution-NonCommercial-ShareAlike 3.0
////////////////////////////////////////////////////////////


float fade(float t) { return t * t * t * (t * (t * 6.0 - 15.0) + 10.0); }

vec2 smoothify(vec2 x) { return vec2( fade(x.x), fade(x.y)); }

vec2 hash(vec2 co) {
    float m = dot(co, vec2(seed1, seed2));
    return fract( vec2( sin(m), cos(m)) * seed3) * 2.0 - 1.0;
}

float pNoise(vec2 uv) {
    vec2 PT  = floor(uv);
    vec2 pt  = fract(uv);
    vec2 mmpt = smoothify(pt);
    vec4 grads = vec4(dot(hash(PT + vec2(0.0, 1.0)), pt-vec2(0.0, 1.0)),
                    dot(hash(PT + vec2(1.0, 1.0)), pt-vec2(1.0, 1.0)),
                    dot(hash(PT + vec2(0.0, 0.0)), pt-vec2(0.0, 0.0)),
                    dot(hash(PT + vec2(1.0, 0.0)), pt-vec2(1.0, 0.0)));
    return 5.*mix (mix (grads.z, grads.w, mmpt.x), mix (grads.x, grads.y, mmpt.x), mmpt.y);
}

float fbm(vec2 uv) {
    float N = iTime;
    N += 1.0000*pNoise(10.0*uv);
    N += 0.50000*pNoise(20.0*uv);
    N += 0.25000*pNoise(40.0*uv);
    N += 0.12500*pNoise(80.0*uv);
    N += 0.06250*pNoise(160.0*uv);
    N += 0.03125*pNoise(320.0*uv);
    return N;
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = (texCoord - vec2(0.5)) + center;
    vec2 p = (mat_zoom * 0.02) * uv;

    // vec2 p = 50. * scale * ( 2.0 * gl_FragCoord.xy / RENDERSIZE.y - 1.0 ) + center;

    float e, f;
    vec3 g, h, j, k, q;
    // if (flip) { f = gl_FragCoord.x; }
    // else f = gl_FragCoord.y;
    if (flip) {
        f = p.x * 100. * mat_wave_scale_y;
    } else {
        f = p.y * 100. * mat_wave_scale_y;
    }

    e = sin( floor(freq2) * f + fbm( floor(freq) * p) + fbm( floor(freq2) * p) + fbm( floor(freq3) * p));
    g = vec3(e);
    h = mix( g,vec3(1.0, 0.0, 0.0), Rx);
    j = mix( g,vec3(0.0, 1.0, 0.0), Gx);
    k = mix( g,vec3(0.0, 0.0, 1.0), Bx);
    q = vec3( reflect( reflect( offset + h ,offset2 - j ), offset3 + k ));
    if (invert) {
        return vec4( 1.0 - vec3(q), 1.0);
    } else {
        return vec4( q, 1.0);
    }




}