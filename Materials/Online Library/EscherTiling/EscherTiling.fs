/*{
    "DESCRIPTION": "https://www.shadertoy.com/view/4dVGzd",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "by you",
    "VSN": "1.0",
    "CATEGORIES": [
        "Your category"
    ],
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Adapted by Jason Beyers",
    "INPUTS": [

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
        }


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

float iTime = mat_time - mat_offset;

vec4 materialColorForPixel( vec2 texCoord ) {
    vec2 U = texCoord - vec2(0.5);
    U *= 4. * mat_zoom;

    // U *= 12./iResolution.y;
    // O-=O;

    vec4 O = vec4(0);
    vec2 f = floor(U), u = 2.*fract(U)-1.;  // ceil cause line on some OS
    float b = mod(f.x+f.y,2.), y;

    for(int i=0; i<4; i++)
        u *= mat2(0,-1,1,0),
        // y = 2.*fract(.2*iDate.w+U.x*.01)-1.,
        y = 2.*fract(.2*iTime+U.x*.01)-1.,
        O += smoothstep(.55,.45, length(u-vec2(.5,1.5*y)));


    if (b>0.) O = 1.-O; // try also without :-)

    return O;
}

