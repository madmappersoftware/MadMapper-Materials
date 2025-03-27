/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Flopine, adapted by Jason Beyers",

    "DESCRIPTION": "Oriental tile generator. From https://www.shadertoy.com/view/WsSfRG",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Oriental/Scale",
            "NAME": "mat_scale",
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
            "LABEL": "Color/Color",
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
                0.0
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

// Original shader by Flopine: https://www.shadertoy.com/view/WsSfRG

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 10.;

#define AAstep(thre, val) smoothstep(-.7,.7,(val-thre)/min(.05,fwidth(val-thre)))

float mat_xor (float a, float b)
{return (1.-a)*b + (1.-b)*a;}

float mat_circle(vec2 uv, float r)
{return AAstep(r,length(uv));}

float mat_pattern(vec2 uv)
{
    const float per = 1.5;
    const int ip = int(per);
    vec2 id = floor((uv)/per);
    uv = mod(uv,per)-per*0.5;
    float r = sin(length(id+0.5)-mat_time*PI/6.)*0.5+0.5;
    float d = mat_circle(uv,0.5);
    for(int i=-ip; i<=ip;i+=ip)
    {
        for (int j=-ip; j<=ip;j+=ip)
        {
          d = mat_xor(d,mat_circle(uv+vec2(float(i),float(j))/2.,r));
        }
    }
    return d;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    // float p = mat_pattern(uv*5);
    // vec3 col = vec3(p);//mix(vec3(0.8,0.0,0.0),vec3(0.1,0.9,0.6),mat_pattern(uv*5.));

    // col = mix(vec3(0.8,0.0,0.0),vec3(0.1,0.9,0.6),mat_pattern(uv*5.));
    // out_color = vec4(sqrt(col),1.0);

    vec4 col;

    col = mix(mat_front_color,mat_back_color,mat_pattern(uv*5.));
    out_color = sqrt(col);








    return out_color;
}
