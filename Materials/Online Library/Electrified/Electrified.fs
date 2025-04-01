/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Brandon Fogerty, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#42209.4",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Electrified/Noise 1",
            "NAME": "mat_noise1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Electrified/Noise 2",
            "NAME": "mat_noise2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Electrified/Noise 3",
            "NAME": "mat_noise3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Electrified/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 40.0,
            "DEFAULT": 27.0
        },
        {
            "LABEL": "Electrified/Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Electrified/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Electrified/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Electrified/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },


        {
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Tint",
            "NAME": "mat_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },

        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        }


    ],
    "GENERATORS": [
        {
            "NAME": "mat_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "reverse": "mat_reverse",
                "strob" : "mat_strob",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 4.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// Lightning
// By: Brandon Fogerty
// bfogerty at gmail dot com
// xdpixel.com


// EVEN MORE MODS BY 27

float Hash( vec2 p)
{
     vec3 p2 = vec3(p.xy,1.0);
    return fract(sin(dot(p2,vec3(37.1,61.7, 12.4)))*3758.5453123);
}

float noise(in vec2 p)
{
    vec2 i = floor(p);
     vec2 f = fract(p);
     f *= f * (3.0-2.0*f);

    return mix(mix(Hash(i + vec2(0.,0.)), Hash(i + vec2(1.,0.)),f.x),
               mix(Hash(i + vec2(0.,1.)), Hash(i + vec2(1.,1.)),f.x),
               f.y);
}

float fbm(vec2 p)
{
     float v = -0.27;
     v += noise(p*1.0)*.5 * mat_noise1;
     v -= (mat_noise1 - 1.) * 0.25;
     v += noise(p*2.)*.25 * mat_noise2;
     v -= (mat_noise2 - 1.) * 0.125;
     v += noise(p*4.)*.125 * mat_noise3;
     v -= (mat_noise3 - 1.) * 0.075;
     return v * 1.0;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 4.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;


    float lim = mat_limit;

    vec3 finalColor = vec3(0.);
    for( int i=1; i > 0; ++i )
    {
        if(i > int(lim)) break;
        float val = float(i);

        float pct = val / lim;
        float t = abs(pct*pct / ((uv.y- sin(mat_time*2.-uv.x)/2.  + fbm( uv + (-mat_time*30.)/val)) * (lim * 10.0 / mat_glow)));
        float u = abs(pct*pct / ((uv.y- sin(-mat_time*2.-uv.x)/2.  + fbm( uv + (mat_time*30.)/val)) * (lim * 10.0 / mat_glow)));
        finalColor +=  t * vec3( pct+0.1, 1., 1.0 );
        finalColor +=  u * vec3( 1., pct+0.1, 1.0 );
            //HPPH
    }

    finalColor *= mat_color.rgb;

    out_color = vec4( finalColor, 1.0 );


    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;


    return out_color;
}
