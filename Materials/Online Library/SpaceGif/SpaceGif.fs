/*{
  "DESCRIPTION": "https://www.shadertoy.com/view/wdlGRM",
  "CREDIT": "by you",
  "CATEGORIES": [
    "Your category"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS": [

        {
            "LABEL" : "Center",
            "MAX": [1.0,1.0],
            "MIN": [0.0,0.0],
            "DEFAULT":[0.5,0.5],
            "NAME": "center",
            "TYPE": "point2D"
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
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = mat_time - mat_offset*100;

// Space Gif by Martijn Steinrucken aka BigWings - 2019
// Email:countfrolic@gmail.com Twitter:@The_ArtOfCode
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// Original idea from:
// https://boingboing.net/2018/12/20/bend-your-spacetime-continuum.html
//


vec4 materialColorForPixel( vec2 texCoord )
{

    // vec2 uv = (fragCoord.xy-iResolution.xy*.5)/iResolution.y;

    vec2 uv = texCoord - center;

    uv *= mat_zoom;

    uv *= mat2(.707, -.707, .707, .707);

    uv *= 15.;

    vec2 gv = fract(uv)-.5;
  vec2 id = floor(uv);

  float m = 0.;
    float t;
    for(float y=-1.; y<=1.; y++) {
      for(float x=-1.; x<=1.; x++) {
            vec2 offs = vec2(x, y);

            t = -iTime+length(id-offs)*.2;
            float r = mix(.4, 1.5, sin(t)*.5+.5);
        float c = smoothstep(r, r*.9, length(gv+offs));
        m = m*(1.-c) + c*(1.-m);
        }
    }

    return vec4(m);
}


