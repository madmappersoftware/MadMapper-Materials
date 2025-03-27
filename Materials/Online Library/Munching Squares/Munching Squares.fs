/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "noby, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/MdcBWl",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Pattern/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pattern/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "float",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 4
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
            "Label": "Animation/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
                0.0,
                0.0,
                0.0,
                1.0
            ]
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

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}


float xor(vec2 p, vec2 lims, float A)
{
    return smoothstep(lims.x, lims.y, float(int(A*p.x) ^ int(A*p.y))/A);
}

float pattern(vec2 uv)
{
    const float TS = 4.0*120.0/131.0;

    uv /= 1.0+0.5*sin(mat_time*2.065*0.25);
    uv *= 0.75;


    float RC = float(mat_iterations);
    float a = PI/RC;
    const float tq = 60.0;
    vec2 lims = vec2(0.495,0.505) * mat_limit;
    mat2 R = mat2(cos(a),sin(a),-sin(a),cos(a));
    float rf = mat_time*2.065*0.25*0.25;
    mat2 R2 = mat2(cos(rf),sin(rf),-sin(rf),cos(rf));

    uv *= R2;

    // TODO: the 2.065 constant is not exact, idk what it should actually be
    float A = exp(15.0+mod(-mat_time*0.25, 2.065));
    float p = xor(uv, lims, A);

    for(int i = 0; i < int(RC)-1; i++)
    {
        if(p > 0.0) break;
        uv *= 1.5+0.5*sin(mat_time*2.065*0.25);
        uv *= R;
        p = xor(uv, lims, A);
    }

    return p;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;


    float p = 0.0;
    // const float S = 1.0/3.0;

    float S = 0.0;
    p += pattern(uv+vec2(0));
    p += pattern(uv+vec2(S,0));
    p += pattern(uv+vec2(-S,0));
    p += pattern(uv+vec2(0,S));
    p += pattern(uv+vec2(0,-S));
    p += pattern(uv+vec2(-S,S));
    p += pattern(uv+vec2(S,-S));
    p += pattern(uv+vec2(S));
    p += pattern(uv+vec2(-S));
    p /= 9.0;
    out_color = vec4(sqrt(p));

    out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color));

    return out_color;
}
