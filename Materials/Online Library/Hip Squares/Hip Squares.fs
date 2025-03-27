/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Hadyn, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/XsBfRW",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Hip Squares/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Hip Squares/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 45.0
        },


        {
            "LABEL": "Hip Squares/Origin",
            "NAME": "mat_origin",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "LABEL": "Hip Squares/Shift XY",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "LABEL": "Hip Squares/Pattern",
            "NAME": "mat_pattern",
            "TYPE": "float",
            "MIN": 0.001,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Hip Squares/Pulse",
            "NAME": "mat_pulse",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Color/Front Color",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.5,
                0.75,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Alpha",
            "NAME": "mat_use_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

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

float mat_time = mat_time_source + mat_offset * 4.;


vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= mat_scale * 6.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 origin = mat_origin;
    origin += vec2(0.5);
    origin.y = 1.-origin.y;

    origin -= vec2(0.5);
    origin *= 6.;

    origin += vec2(0.5);
    origin = matRot2D(origin, 2*PI*mat_rotate / 360);
    origin -= vec2(0.5);

    float value;
    vec2 pos = uv;
    vec2 rep = fract(pos);

    float dist = 2.0*min(min(rep.x, 1.0-rep.x), min(rep.y, 1.0-rep.y));
    float squareDist = length((floor(pos)+vec2(0.5)) - origin);

    float edge = sin(mat_time-squareDist*0.5)*0.5+0.5;

    edge = (mat_time-squareDist*0.5/mat_pattern)*0.5;
    edge = 2.0*fract(edge*0.5);
    //value = 2.0*abs(dist-0.5);
    //value = pow(dist, 2.0);
    value = fract (dist*2.0);
    value = mix(value, 1.0-value, step(1.0, edge));
    //value *= 1.0-0.5*edge;
    edge = pow(abs(1.0-edge), 2.0 * mat_pulse);

    //edge = abs(1.0-edge);
    value = smoothstep( edge-0.05, edge, 0.95*value);

    value += squareDist*.1;
    //gl_FragColor = vec4(value);
    out_color = mix(mat_front_color,mat_back_color, value);
    if (mat_use_alpha) {
        out_color.a = 0.25*clamp(value, 0.0, 1.0);
    } else {
        out_color.a = 1.;
    }

    return out_color;
}
