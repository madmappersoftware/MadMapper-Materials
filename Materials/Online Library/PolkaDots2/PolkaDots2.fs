/*{
    "DESCRIPTION": "https://www.shadertoy.com/view/XldXRN",
    "CREDIT": "Jason Beyers",
    "VSN": "1.0",
    "CATEGORIES": [
        "Your category"
    ],
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
float iTime = mat_time - mat_offset*4;

void mainImage( out vec4 O, vec2 U )
{
    vec2 R = iResolution.xy;
    U = ( U+U - R ) /R.y * 8.;

    float Pi = 3.14159265359,
           t = 16.*iTime,  // iDate.w cause streching bug on some machines (AMD+windows?)
           e = 35./R.y, v;
  //       a = Pi/3.*floor(t/2./Pi);
  //U *= mat2(cos(a),-sin(a), sin(a), cos(a));
    U *= mat2(sin(Pi/3.*ceil(t/2./Pi) + Pi*vec4(.5,1,0,.5)));      // animation ( switch dir )

    U.y /= .866;
    U -= .5;
    v = ceil(U.y);
    U.x += .5*v;                                                   // hexagonal tiling
  //U.x += sin(t) > 0. ? (.5-.5*cos(t)) * (2.*mod(v,2.)-1.) : 0.;
    U.x += sin(t) > 0. ? (1.-cos(t)) * (mod(v,2.)-.5) : 0.;        // animation ( scissor )
  //U.x += (1.-cos(t/2.)) * (mod(v,2.)-.5);                        // variant

    U = 2.*fract(U)-1.;                                            // dots
    U.y *= .866;
    O += smoothstep(e,-e, length(U)-.6) -O;

}


vec4 materialColorForPixel( vec2 texCoord ) {

    vec4 color = vec4(0);
    mainImage(color, gl_FragCoord.xy * mat_zoom);
    return color;
}
