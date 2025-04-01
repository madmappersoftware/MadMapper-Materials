/*{
    "DESCRIPTION": "https://www.shadertoy.com/view/XdKXz3",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers",
    "CATEGORIES": [
        "Your category"
    ],
    "TAGS": "converted_from_isf",
    "VSN": "1.0",
    "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 0.5
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
float iTime = TIME;

// relying on hexagonal tiling tuto https://www.shadertoy.com/view/4dKXR3

vec4 materialColorForPixel( vec2 texCoord ) {

    float adj_time = mat_time - mat_offset*4;

    vec2 uv = texCoord;

    uv *= 200;

    uv *= mat_zoom;

    vec2 R = iResolution.xy,
         U = uv = (uv-R/2.)/R.y * 6. *  1.73/2.;          // centered coords

    U *= mat2(1,-1./1.73, 0,2./1.73);                     // conversion to
    vec3 g = vec3(U, 1.-U.x-U.y), g2,                     // hexagonal coordinates
         id = floor(g);                                   // cell id

    g = fract(g);                                         // diamond coords
    g2 = abs(2.*g-1.);                                    // distance to borders

    U = id.xy * mat2(1,.5, 0,1.73/2.);
    float l00 = length(U-uv),                    // screenspace distance to nodes
          l10 = length(U+vec2(1,0)-uv),
          l01 = length(U+vec2(.5,1.73/2.)-uv),
          l11 = length(U+vec2(1.5,1.73/2.)-uv),
            l = min(min(l00, l10), min( l01, l11)); // closest node: l=dist, C=coord
    vec2 C = U+ ( l==l00 ? vec2(0) : l==l10 ? vec2(1,0) : l==l01 ? vec2(.5,1.73/2.) : vec2(1.5,1.73/2.) );

    return sin(length(40.*l-2.*adj_time)) +vec4(0) - vec4(0);
}


