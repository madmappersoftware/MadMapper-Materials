/*{
    "DESCRIPTION": "https://www.shadertoy.com/view/wdfGRX",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers",
    "TAGS": "converted_from_isf",
    "VSN": "1.0",
    "CATEGORIES": [
        "Your category"
    ],
    "INPUTS": [

        {
            "Label": "Color",
            "NAME": "color",
            "TYPE": "color",
            "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ]
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

        {
            "LABEL" : "Rot/Rot Center",
            "MAX": [1.0,1.0],
            "MIN": [0.0,0.0],
            "DEFAULT":[0.5,0.5],
            "NAME": "rot_center",
            "TYPE": "point2D"
        },

        {
            "LABEL": "Rot/Separate Rot Controls",
            "NAME": "rot_separate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Rot/Rot BPM Sync",
            "NAME": "rot_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rot/Rot Reverse",
            "NAME": "rot_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rot/Rot Speed",
            "NAME": "rot_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Rot/Rot Offset",
            "NAME": "rot_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rot/Rot Strob",
            "NAME": "rot_strob",
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
        },
        {
            "NAME": "rot_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "rot_speed",
                "speed_curve":2,
                "strob" : "rot_strob",
                "reverse": "rot_reverse",
                "bpm_sync": "rot_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = mat_time - mat_offset;

float rotTime;


#define SIZE 100.
#define PI 3.14159265359
#define PI2 1.570796326795

//#define HORIZONTAL

mat2 rot(float a)
{
    return mat2(cos(a),-sin(a),
                sin(a),cos(a));
}
float in_out_cubic(float t)
{
    t *= 2.;
    if (t < 1.) return .5*t*t*t;
    t-=2.;
    return .5*(t*t*t+2.);
}
vec4 mainImage(vec2 uv_orig)
{
    // vec2 R2 = iResolution.xy / 2.;

    if (rot_separate) {
        rotTime = rot_time - rot_offset;

    } else {
        rotTime = iTime;
    }

    vec2 R2 = iResolution.xy - rot_center;

    float t = fract(iTime / 2.),
         t3 = step(abs(mod(iTime, 4.) - 2.), 1.),
         t4 = 1. - t3,
   halfdiag = sqrt(2.)/2.,
        pad = .5-halfdiag;

    vec2 uv = uv_orig - rot_center;

    uv *= 1000 * mat_zoom;

#ifndef HORIZONTAL
    t = in_out_cubic(t);
    // uv = (uv - R2) * rot(-rotTime/5.) + R2;

    uv = uv * rot(-rotTime/5.);
#else
    uv.y += sin(t*PI)*pad*SIZE/2.;
    uv.x += t*-halfdiag*SIZE;
    uv.x -= halfdiag*SIZE*floor(iTime/2.);
#endif

    uv = fract(uv / SIZE + t4 / 2.);
    uv = (1.-pad-pad-.6/SIZE)*uv+pad;
    uv = (uv - .5) * rot(t*PI2);
    uv =
#ifdef HORIZONTAL
        // smoothstep makes the transition look
        //   somewhat less smooth if horizontal
        step(0.,
#else
        smoothstep(0.,2./SIZE,
#endif
                              .5-abs(uv));

    // vec4 fragColor = vec4(1.,.6,1.,1.)*mix(t4,t3,uv.x*uv.y);
    vec4 fragColor = color*mix(t4,t3,uv.x*uv.y);
    return fragColor;
}

/* old code for reference
void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
    vec2 fc = (fragCoord.xy - iResolution.xy/2.) * rot(-iTime/5.) + iResolution.xy/2.;
    float t = in_out_cubic(mod(iTime, 2.) / 2.);
    float t3 = mod(iTime, 4.);
    t3 = step(1., t3) * step(t3, 3.);
    float t4 = 1. - t3;

    float pad = (1.-sqrt(2.))/2.;
    vec2 uv = mod(fc + SIZE/2. * t4, vec2(SIZE))/SIZE;
    uv = (1.-pad-pad)*uv+pad;
    uv = (uv - .5) * rot(t*PI2) + .5;

    vec3 incol = vec3(1.,.6,1.);
    float val = step(0.,uv.x)*step(0.,uv.y)*step(uv.x,1.)*step(uv.y,1.);
    fragColor = vec4(incol*val*t3+t4*incol-t4*incol*val,1.0);
}
*/

vec4 materialColorForPixel( vec2 texCoord ) {
    return mainImage(texCoord);
}