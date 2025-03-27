/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#46679.0",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Astral Cord/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Astral Cord/Perspective",
            "NAME": "mat_perspective",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Astral Cord/Rotate",
            "NAME": "mat_rot",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Astral Cord/Shift",
            "NAME": "mat_shift",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Astral Cord/Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Astral Cord/Lattice",
            "NAME": "mat_lattice",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Astral Cord/Cord",
            "NAME": "mat_cord",
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
            "NAME": "mat_color",
            "TYPE": "color",
            "DEFAULT": [
                0.9,
                0.3,
                0.3,
                1.0
            ]
        },

        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.2,
                0.1,
                0.2,
                1.0
            ]
        },


        {
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
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

float mat_time = mat_time_source - mat_offset;

mat2 mat_rotate(float a){
    float c=cos(a),s=sin(a);
    return mat2(c, s, -s, c);
}

float mat_sc(vec3 p, float s) {
    p=abs(p);
    p=max(p,p.yzx);
    return min(p.x,min(p.y,p.z)) - s;
}

vec2 mat_amod(vec2 p, float c) {
    float m = c/6.28;
    float a = mod(atan(p.x,p.y)-m*.5, m)-m*.5;
    return vec2(cos(a), sin(a)) * length(p);
}

void mat_mo(inout vec2 p, vec2 d) {
    p.x = abs(p.x) - d.x;
    p.x = abs(p.x) - d.x;
    if(p.y>p.x)p=p.yx;
}

float glow = 0.;

float mat_de(vec3 p) {
    vec3 q = p;
    p.xy *= mat_rotate(mat_time*.01+mat_rot*PI);


    q.xy += vec2(sin(q.z*2.+1.)*.2+sin(q.z)*.05, sin(q.z)*.4);
    float c = length(max(abs(q+vec2(0,.05).xyx) - vec3(.01, .01, 1e6), 0.));

    c /= mat_cord;

    p.xy *= mat_rotate(p.z*.1);
    p.xy = mat_amod(p.xy, 19. );//8.
    float d1 = 2.;
    p.z = mod(p.z-d1*.5, d1) - d1*.5;


    mat_mo(p.xy, vec2(.1, .3));
    mat_mo(p.xy, vec2(.8, .9));

    p.x = abs(p.x) - .8;

    p.xy *= mat_rotate(.785);
    mat_mo(p.xy, vec2(.2, .2));


    float d = mat_sc(p, .1)/mat_lattice;
    d = min(d, c);
    glow+=.01/(.01+d*d);

    return d;
}

float mat_raycast(vec3 ro, vec3 rd){
vec3 p;
float t=0.;

    for(int i=0;i<128.;i+=1) {
        p=ro+rd*t;
        float d = mat_de(p);
        if(t>30.) break;
        d = max(abs(d), .0004);
        t+=d*.5;
    }

    return t;

}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    vec2 shift = mat_shift;
    shift += vec2(0.5);
    shift.x = 1. - shift.x;
    shift -= vec2(0.5);

    vec3 ro=vec3(shift.x,shift.y,mat_time*2.);
    vec3 rd=normalize(vec3(uv,1*mat_perspective));


    float t = mat_raycast(ro,rd);

    // vec3 bg = vec3(.2, .1, .2);

    // vec3 col = bg;
    // if(t<=30.)
    //     col = mix(vec3(.9, .3, .3), bg, uv.x );

    // col+=glow*.02;
    // col = mix(col, bg, 1.-exp(-.1*t*t));

    // out_color = vec4(col,1.0);

    vec4 bg = mat_back_color;

    vec4 col = bg;
    if(t<=30.)
        col = mix(mat_color, bg, uv.x );

    col+=glow*.02*mat_glow;
    col = mix(col, bg, 1.-exp(-.1*t*t));

    out_color = col;




    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply Hue Shift and saturation
    if (mat_saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.y = max(hsv.y + mat_saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;


    return out_color;
}
