/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "yasuo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/DdXSWj",

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
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },

        {
            "LABEL": "Character/Scale 1",
            "NAME": "mat_scale1",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Character/Scale 2",
            "NAME": "mat_scale2",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Character/Scale 3",
            "NAME": "mat_scale3",
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },




        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_scroll_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_scroll_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Offset Scale",
            "NAME": "mat_scroll_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Restart",
            "NAME": "mat_scroll_restart",
            "TYPE": "event",
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
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
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
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "reset": "mat_scroll_restart",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_scroll_time = mat_scroll_time_source - mat_scroll_offset * 8. * mat_scroll_offset_scale;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}



#define Rot(a) mat2(cos(a),-sin(a),sin(a),cos(a))
#define antialiasing(n) n/min(RENDERSIZE.y,RENDERSIZE.x)
#define S(d,b) smoothstep(antialiasing(1.0),b,d)
#define B(p,s) max(abs(p).x-s.x,abs(p).y-s.y)
#define Tri(p,s,a) max(-dot(p,vec2(cos(-a),sin(-a))),max(dot(p,vec2(cos(a),sin(a))),max(abs(p).x-s.x,abs(p).y-s.y)))
#define DF(a,b) length(a) * cos( mod( atan(a.y,a.x)+6.28/(b*8.0), 6.28/((b*8.0)*0.5))+(b-1.)*6.28/(b*8.0) + vec2(0,11) )

float FS = 0.46 / mat_scale1; // font size
float FGS = FS/5. / mat_scale2; // font grid size
//#define OUTLINE

// fully modified version of the Letterform Variations font
#define char_0 0
#define char_1 1
#define char_2 2
#define char_3 3
#define char_4 4
#define char_5 5
#define char_6 6
#define char_7 7
#define char_8 8
#define char_9 9
#define char_A 10
#define char_B 11
#define char_C 12
#define char_D 13
#define char_E 14
#define char_F 15
#define char_G 16
#define char_H 17
#define char_I 18
#define char_J 19
#define char_K 20
#define char_L 21
#define char_M 22
#define char_N 23
#define char_O 24
#define char_P 25
#define char_Q 26
#define char_R 27
#define char_S 28
#define char_T 29
#define char_U 30
#define char_V 31
#define char_W 32
#define char_X 33
#define char_Y 34
#define char_Z 35

float random (vec2 p) {
    return fract(sin(dot(p.xy, vec2(12.9898,78.233)))* 43758.5453123);
}

float charA(vec2 p){
    vec2 prevP = p;
    float d = B(p-vec2(0.0,FGS*4.),vec2(FS,FGS));
    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);
    p.x = abs(p.x);
    d2 = B(p-vec2(FGS*4.,0.),vec2(FGS,FS));
    d = min(d,d2);
    return d;
}

float charB(vec2 p) {
    vec2 prevP = p;
    p.y = abs(p.y);
    float d = B(p-vec2(0.0,FGS*4.),vec2(FS,FGS));
    p = prevP;
    float d2 = B(p-vec2(-FGS,0.0),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,0.),vec2(FGS,FS));
    d = min(d,d2);

    p.y = abs(p.y);
    p-=vec2(FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charC(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.0,FGS*4.),vec2(FS,FGS));

    float d2 = B(p-vec2(FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p-=vec2(-FGS*2.,-FGS*2.);
    p*=Rot(radians(-45.));

    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charD(vec2 p){
    vec2 prevP = p;
    p.y=abs(p.y);
    float d = B(p-vec2(0.0,FGS*4.),vec2(FS,FGS));
    p = prevP;
    float d2 = B(p-vec2(FGS*4.,0.),vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);
    return d;
}

float charE(vec2 p) {
    vec2 prevP = p;

    float d = charC(p);
    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float charF(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.0,FGS*4.),vec2(FS,FGS));

    float d2 = B(p-vec2(-FGS,0.),vec2(FGS*4.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p-=vec2(0.,-FGS*2.);

    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charG(vec2 p) {
    vec2 prevP = p;

    float d = charC(p);
    float d2 = B(p-vec2(FGS*2.,0.),vec2(FGS*3.,FGS));
    d = min(d,d2);
    d2 = B(p-vec2(FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charH(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*4.,0.0),vec2(FGS,FS));

    float d2 = B(p-vec2(0.0,0.0),vec2(FGS,FS));
    d = min(d,d2);
    d2 = B(p-vec2(FGS*2.,0.0),vec2(FGS*3.,FGS));
    d = min(d,d2);
    p.y = abs(p.y);
    d2 = B(p-vec2(-FGS*2.,FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    return d;
}

float charI(vec2 p) {
    vec2 prevP = p;
    p.y = abs(p.y);
    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));
    p = prevP;
    float d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);
    d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);
    return d;
}

float charJ(vec2 p) {
    vec2 prevP = p;
    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));

    float d2 = B(p-vec2(0.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);
    d2 = B(p-vec2(-FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p-=vec2(-FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));

    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charK(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,0.0),vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*2.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charL(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    float d2 = B(p-vec2(FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);


    p-=vec2(-FGS*2.,-FGS*2.);
    p*=Rot(radians(-45.));

    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charM(vec2 p) {
    vec2 prevP = p;

    float d = B(p,vec2(FS,FGS));

    float d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);

    p.x = abs(p.x);
    d2 = B(p-vec2(FGS*4.,-FGS),vec2(FGS,FGS*4.));
    d = min(d,d2);

    return d;
}

float charN(vec2 p) {
    vec2 prevP = p;

    p.x = abs(p.x);
    float d = B(p-vec2(FGS*4.,0.),vec2(FGS,FS));

    p = prevP;
    float d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*2.,FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    return d;
}

float charO(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    p = prevP;
    float d2 = B(p-vec2(FGS*2.,FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p-=vec2(-FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p =prevP;
    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charP(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*2.,0.),vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charQ(vec2 p) {
    vec2 prevP = p;

    float d = charO(p);

    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS*3.,FGS));
    d = min(d,d2);

    return d;
}

float charR(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,0.),vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    d2 = B(p-vec2(0.0,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    return d;
}

float charS(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*2.,FGS*4.),vec2(FGS*3.,FGS));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    p-=vec2(-FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(-FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float charT(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,FGS*2.),vec2(FS,FGS));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    return d;
}

float charU(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,-FGS*2.),vec2(FS,FGS));

    p.x = abs(p.x);
    float d2 = B(p-vec2(FGS*2.,0.),vec2(FGS,FS));
    d = min(d,d2);

    return d;
}

float charV(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,0.),vec2(FGS,FS));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    return d;
}

float charW(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,0.),vec2(FGS,FS));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    p = prevP;
    d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);

    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(-45.));
    d2 = B(p,vec2(FGS*3.,FGS));
    d = min(d,d2);

    return d;
}

float charX(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float charY(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    float d2 = B(p-vec2(0.,-FGS),vec2(FGS,FGS*4.));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float charZ(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,FGS*3.),vec2(FGS,FGS*2.));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(FGS*4.,-FGS*3.),vec2(FGS,FGS*2.));
    d = min(d,d2);

    p.y = abs(p.y);
    d2 = B(p-vec2(0., FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float char1(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*2.,FGS*4.),vec2(FGS*3.,FGS));

    float d2 = B(p,vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(0.,-FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float char2(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*2.,FGS*4.),vec2(FGS*3.,FGS));

    float d2 = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);
    d2 = B(p-vec2(FGS*2.,0.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(0.,-FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    p-=vec2(-FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p = prevP;
    p-=vec2(-FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char3(vec2 p) {
    vec2 prevP = p;

    p.y = abs(p.y);
    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));

    p = prevP;
    p.x = abs(p.x);
    float d2 = B(p-vec2(FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(FGS*2.,0.),vec2(FGS*3.,FGS));
    d = min(d,d2);


    p-=vec2(FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char4(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,-FGS*2.),vec2(FS,FGS));

    float d2 = B(p-vec2(-FGS*2.,0.),vec2(FGS,FS));
    d = min(d,d2);

    d2 = B(p-vec2(FGS*2.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char5(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(0.,FGS*4.),vec2(FS,FGS));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*4.,FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(-FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char6(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*2.,FGS*4.),vec2(FGS*3.,FGS));

    float d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    p-=vec2(-FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p = prevP;
    d2 = B(p-vec2(0.,-FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    p.x = abs(p.x);
    d2 = B(p-vec2(FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char7(vec2 p) {
    vec2 prevP = p;

    float d = B(p,vec2(FS,FGS));

    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    p = prevP;


    d2 = B(p-vec2(0., FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    return d;
}

float char8(vec2 p) {
    vec2 prevP = p;

    p.y = abs(p.y);
    float d = B(p-vec2(0., FGS*4.),vec2(FS,FGS));

    p = prevP;
    p*=Rot(radians(45.));
    float d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(-45.));
    d2 = B(p,vec2(FGS,FS*1.2));
    d = min(d,d2);

    return d;
}

float char9(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(FGS*4.,FGS*2.),vec2(FGS,FGS*3.));

    p = prevP;
    float d2 = B(p-vec2(FGS*2.,FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(-FGS*2.,-FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p,vec2(FS,FGS));
    d = min(d,d2);

    p-=vec2(-FGS*2.,FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p =prevP;
    p-=vec2(FGS*2.,-FGS*2.);
    p*=Rot(radians(45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float char0(vec2 p) {
    vec2 prevP = p;

    float d = B(p-vec2(-FGS*4.,0.),vec2(FGS,FS));

    float d2 = B(p-vec2(-FGS*2.,FGS*4.),vec2(FGS*3.,FGS));
    d = min(d,d2);

    d2 = B(p-vec2(0.0,-FGS*4.),vec2(FS,FGS));
    d = min(d,d2);

    p-=vec2(FGS*2.,FGS*2.);
    p*=Rot(radians(-45.));
    d2 = B(p,vec2(FGS,FGS*3.));
    d = min(d,d2);

    p =prevP;
    d2 = B(p-vec2(FGS*4.,-FGS*2.),vec2(FGS,FGS*3.));
    d = min(d,d2);

    return d;
}

float checkChar(int targetChar, int char2){
    return 1.-abs(sign(float(targetChar) - float(char2)));
}

float drawFont(vec2 p, int char_in){
    float d = char0(p)*checkChar(char_0,char_in);
    d += char1(p)*checkChar(char_1,char_in);
    d += char2(p)*checkChar(char_2,char_in);
    d += char3(p)*checkChar(char_3,char_in);
    d += char4(p)*checkChar(char_4,char_in);
    d += char5(p)*checkChar(char_5,char_in);
    d += char6(p)*checkChar(char_6,char_in);
    d += char7(p)*checkChar(char_7,char_in);
    d += char8(p)*checkChar(char_8,char_in);
    d += char9(p)*checkChar(char_9,char_in);
    d += charA(p)*checkChar(char_A,char_in);
    d += charB(p)*checkChar(char_B,char_in);
    d += charC(p)*checkChar(char_C,char_in);
    d += charD(p)*checkChar(char_D,char_in);
    d += charE(p)*checkChar(char_E,char_in);
    d += charF(p)*checkChar(char_F,char_in);
    d += charG(p)*checkChar(char_G,char_in);
    d += charH(p)*checkChar(char_H,char_in);
    d += charI(p)*checkChar(char_I,char_in);
    d += charJ(p)*checkChar(char_J,char_in);
    d += charK(p)*checkChar(char_K,char_in);
    d += charL(p)*checkChar(char_L,char_in);
    d += charM(p)*checkChar(char_M,char_in);
    d += charN(p)*checkChar(char_N,char_in);
    d += charO(p)*checkChar(char_O,char_in);
    d += charP(p)*checkChar(char_P,char_in);
    d += charQ(p)*checkChar(char_Q,char_in);
    d += charR(p)*checkChar(char_R,char_in);
    d += charS(p)*checkChar(char_S,char_in);
    d += charT(p)*checkChar(char_T,char_in);
    d += charU(p)*checkChar(char_U,char_in);
    d += charV(p)*checkChar(char_V,char_in);
    d += charW(p)*checkChar(char_W,char_in);
    d += charX(p)*checkChar(char_X,char_in);
    d += charY(p)*checkChar(char_Y,char_in);
    d += charZ(p)*checkChar(char_Z,char_in);

    float a = radians(45.);
    p = abs(p)-0.37;
    d = max(dot(p,vec2(cos(a),sin(a))),d);

    #ifdef OUTLINE
        return abs(d)-0.01;
    #endif

    return d;
}

float dSlopeLines(vec2 p){
    float lineSize = 24.;
    float d = tan((mix(p.x,p.y,0.7)+(-mat_time*1.5/lineSize))*lineSize)*lineSize;
    return d;
}

float blocks(vec2 p){
    vec2 prevP = p;

    p.x = mod(p.x,0.24)-0.12;
    float d = B(p, vec2(FGS*0.55));
    p = prevP;
    p.x+=0.12;
    p.x = mod(p.x,0.24)-0.12;
    p.y = abs(p.y)-0.12;
    float d2 = B(p, vec2(FGS*0.55));
    return min(d,d2);
}

float blocks2(vec2 p){
    p.y = mod(p.y,0.92)-0.46;
    vec2 prevP = p;
    p.y-=FGS*2.5;
    float d = abs(B(p,vec2(FGS*1.7)))-0.03;
    float d2 = B(p,vec2(FGS*0.5));
    d = min(d,d2);
    p = prevP;
    p.y-=-FGS*2.5;
    d2 = abs(B(p,vec2(FGS)))-0.03;
    d = min(d,d2);

    return d;
}

float cubicInOut(float t) {
  return t < 0.5
    ? 4.0 * t * t * t
    : 0.5 * pow(2.0 * t - 2.0, 3.0) + 1.0;
}

float getTime(float t, float duration){
    return clamp(t,0.0,duration)/duration;
}

float drawFonts4GridsSpace(int char_in, float scale, vec2 grd, vec2 prevGrd, vec2 pa, vec2 pb, vec2 pc, vec2 pd){
    grd-=pa;
    grd*=scale;

    float d = drawFont(grd,char_in);
    grd = prevGrd;

    grd -=pb;
    grd*=scale;
    float d2 = drawFont(grd,(char_in+1>=35)?10:char_in+1);
    d = min(d,d2);
    grd = prevGrd;

    grd -=pc;
    grd*=scale;
    d2 = drawFont(grd,(char_in+2>=35)?10:char_in+2);
    d = min(d,d2);
    grd = prevGrd;

    grd -=pd;
    grd*=scale;
    d2 = drawFont(grd,(char_in+3>=35)?10:char_in+3);
    d = min(d,d2);
    return d;
}

float gridSystem(vec2 p){
    vec2 prevP = p;
    p*=3.;
    p.y-=mat_scroll_time*0.5;
    vec2 id = floor(p);
    vec2 grd = fract(p)-0.5;

    float n = random(id);
    float nChar = random(id)*35.0;
    int char_in = int(nChar);
    float d = drawFont(grd,char_in);
    if(n>=0.1 && n<0.2 && char_in < 10){
        int num = int(mod(mat_time*float(nChar),10.0));
        d = drawFont(grd,num);
    }

    float d2 = 10.;

    vec2 prevGrd = grd;
    float scale = 2.1 * mat_scale3;
    if(n>=0.2 && n<0.5){

        float frame = mod(mat_time,10.0);
        float time = frame;

        vec2 pa = vec2(-0.24,0.24);
        vec2 pb = vec2(-0.24,-0.24);
        vec2 pc = vec2(0.24,-0.24);
        vec2 pd = vec2(0.24,0.24);
        if(frame>=1. && frame<3.){
            time = getTime(time-1.,0.6);
            float val = cubicInOut(time)*0.48;
            pa = vec2(-0.24,0.24-val);
            pb = vec2(-0.24+val,-0.24);
            pc = vec2(0.24,-0.24+val);
            pd = vec2(0.24-val,0.24);
        } else if(frame>=3. && frame<5.){
            time = getTime(time-3.,0.6);
            float val = cubicInOut(time)*0.48;
            pa = vec2(-0.24+val,-0.24);
            pb = vec2(0.24,-0.24+val);
            pc = vec2(0.24-val,0.24);
            pd = vec2(-0.24,0.24-val);
        } else if(frame>=5. && frame<7.){
            time = getTime(time-5.,0.6);
            float val = cubicInOut(time)*0.48;
            pa = vec2(0.24,-0.24+val);
            pb = vec2(0.24-val,0.24);
            pc = vec2(-0.24,0.24-val);
            pd = vec2(-0.24+val,-0.24);
        } else if(frame>=7. && frame<10.){
            time = getTime(time-7.,0.6);
            float val = cubicInOut(time)*0.48;
            pa = vec2(0.24-val,0.24);
            pb = vec2(-0.24,0.24-val);
            pc = vec2(-0.24+val,-0.24);
            pd = vec2(0.24,-0.24+val);
        }

        d = drawFonts4GridsSpace(char_in, scale, grd, prevGrd, pa, pb, pc, pd);

    } else if(n>=0.5 && n<0.6){
        // up
        grd-=vec2(-0.24,0.24);
        grd*=scale;
        d = drawFont(grd,char_in);
        grd = prevGrd;

        grd -= vec2(0.24,0.24);
        grd*=scale;
        d2 = drawFont(grd,(char_in+1>=35)?10:char_in+1);
        d = min(d,d2);
        grd = prevGrd;

        float d3 = B(grd-vec2(0.,-0.24),vec2(0.46,0.22));

        float dir = (n>=0.55)?-1.:1.;
        grd.x*=dir;
        grd.x+=mat_time*n*0.5;
        grd.x = mod(grd.x,0.2)-0.1;
        grd.x+=0.1;
        grd-=vec2(0.,-0.24);
        grd*=Rot(radians(-90.));

        d2 = Tri(grd,vec2(FGS*2.),radians(45.));
        float mask = Tri(grd-vec2(0.0,-FGS),vec2(FGS*2.),radians(45.));
        d2 = max(-mask,d2);
        d2 = max(d3,d2);
        d2 = min(d2,abs(d3)-0.01);
        d = min(d,d2);

    } else if(n>=0.7 && n<0.8){
        // down
        grd-=vec2(-0.24,-0.24);
        grd*=scale;
        d = drawFont(grd,char_in);
        grd = prevGrd;

        grd -= vec2(0.24,-0.24);
        grd*=scale;
        d2 = drawFont(grd,(char_in+1>=35)?10:char_in+1);
        d = min(d,d2);
        grd = prevGrd;

        float d3 = B(grd-vec2(0.,0.24),vec2(0.46,0.22));

        float dir = (n>=0.75)?-1.:1.;
        grd.x += dir*mat_time*0.2;
        d2 = blocks(grd-vec2(0.,0.24));
        d2 = max(d3,d2);
        d2 = min(d2,abs(d3)-0.01);
        d = min(d,d2);
    } else if(n>=0.8 && n<0.9){
        // left
        grd-=vec2(-0.24,0.24);
        grd*=scale;
        d = drawFont(grd,char_in);
        grd = prevGrd;

        grd -= vec2(-0.24,-0.24);
        grd*=scale;
        d2 = drawFont(grd,(char_in+1>=35)?10:char_in+1);
        d = min(d,d2);
        grd = prevGrd;


        float d3 = B(grd-vec2(0.24,0.0),vec2(0.22,0.46));

        grd-=vec2(0.24,0.0);
        d2 = dSlopeLines(grd);
        d2 = max(d3,d2);
        d2 = min(d2,abs(d3)-0.01);
        d = min(d,d2);
    } else if(n>=0.9 && n<1.){
        // right
        grd-=vec2(0.24,0.24);
        grd*=scale;
        d = drawFont(grd,char_in);
        grd = prevGrd;

        grd -= vec2(0.24,-0.24);
        grd*=scale;
        d2 = drawFont(grd,(char_in+1>=35)?10:char_in+1);
        d = min(d,d2);
        grd = prevGrd;

        float d3 = B(grd-vec2(-0.24,0.0),vec2(0.22,0.46));

        float dir = (n>=0.95)?-1.:1.;
        grd.y += dir*mat_time*0.2;
        d2 = blocks2(grd-vec2(-0.24,0.0));
        d2 = max(d3,d2);
        d2 = min(d2,abs(d3)-0.01);

        d = min(d,d2);
    }

    return d;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    vec2 p = uv;
    vec3 col =vec3(0.0);
    float d = gridSystem(p);
    col = mix(col,vec3(1.),S(d,0.0));
    out_color = vec4(sqrt(col),1.0);




    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;


    if (mat_luma(out_color.rgb) > 0.02) {
        out_color = mat_front_color;
    } else {
        out_color = mat_back_color;
    }


    return out_color;
}
