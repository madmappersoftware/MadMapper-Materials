/*{
    "DESCRIPTION": "From https://www.shadertoy.com/view/XdVXR3",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "FabriceNeyret2, adapted by Jason Beyers",
    "TAGS": "converted_from_isf",
    "VSN": "1.1",
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

// relying on hexagonal tiling tuto https://www.shadertoy.com/view/4dKXR3

vec4 materialColorForPixel( vec2 texCoord ) {

    float adj_time = mat_time - mat_offset*20;

    vec2 U = texCoord - vec2(0.5,0.5);

    U *= 1000;

    U *= mat_zoom;

    U *= mat2(1,-1./1.73, 0,2./1.73) *5./ 1000;  // conversion to

    // U *= mat2(1,-1./1.73, 0,2./1.73) *5./ iResolution.y;  // conversion to
    vec3 g = vec3(U, 1.-U.x-U.y);                         // hexagonal coordinates
    g = fract(g);                  // diamond coords
    if (length(g)>1.) g = 1.-g;    // barycentric coords

    g = sin(g * 6.28* ( mod(adj_time-10.,20.) - 10. ));


    return g.x + g.y + g.z +vec4(0) - vec4(0) ;
}


