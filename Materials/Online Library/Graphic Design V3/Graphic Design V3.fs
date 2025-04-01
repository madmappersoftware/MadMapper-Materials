/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "yasuo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/dsS3Wd",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

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
            "LABEL": "Graphics/Center Fill",
            "NAME": "mat_center_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Border",
            "NAME": "mat_border",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Arrow Split",
            "NAME": "mat_arrow_split",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Arrow Thick",
            "NAME": "mat_arrow_thick",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Animation 1/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation 1/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 1/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 1/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 1/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 1/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 1/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Animation 2/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation 2/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 2/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 2/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 2/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 2/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 2/Restart",
            "NAME": "mat_a2_restart",
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
            "LABEL": "Color/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Cut"
        },
        {
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Sensitivity",
            "NAME": "mat_back_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },







    ],
    "GENERATORS": [
        {
            "NAME": "mat_a1_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a1_speed",
                "speed_curve":2,
                "reverse": "mat_a1_reverse",
                "strob" : "mat_a1_strob",
                "reset": "mat_a1_restart",
                "bpm_sync": "mat_a1_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_a2_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a2_speed",
                "speed_curve":2,
                "reverse": "mat_a2_reverse",
                "strob" : "mat_a2_strob",
                "reset": "mat_a2_restart",
                "bpm_sync": "mat_a2_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale;
float mat_a2_time = mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale;

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
#define antialiasing(n) n/(mat_detail * 2000.)
#define S(d,b) smoothstep(antialiasing(1.0),b,d)
#define B(p,s) max(abs(p).x-s.x,abs(p).y-s.y)
#define Tri(p,s,a) max(-dot(p,vec2(cos(-a),sin(-a))),max(dot(p,vec2(cos(a),sin(a))),max(abs(p).x-s.x,abs(p).y-s.y)))
#define DF(a,b) length(a) * cos( mod( atan(a.y,a.x)+6.28/(b*8.0), 6.28/((b*8.0)*0.5))+(b-1.)*6.28/(b*8.0) + vec2(0,11) )
#define Skew(a,b) mat2(1.0,tan(a),tan(b),1.0)
#define SkewX(a) mat2(1.0,tan(a),0.0,1.0)
#define SkewY(a) mat2(1.0,0.0,tan(a),1.0)
#define SymdirX(p) mod(floor(p).x,2.)*2.-1.
#define SymdirY(p) mod(floor(p).y,2.)*2.-1.

float Hash21(vec2 p) {
    p = fract(p*vec2(234.56,789.34));
    p+=dot(p,p+34.56);
    return fract(p.x+p.y);
}

float thunderIcon(vec2 p){
    float dir = SymdirY(p);
    p.x = abs(p.x)-0.17;
    vec2 prevP2 = p;
    p.y+=mat_a1_time*0.15*dir;
    p.y = mod(p.y,0.1)-0.05;
    vec2 prevP = p;

    vec2 size = vec2(0.01,0.04);
    float a = radians(-25.);

    p.x+=0.008;
    p.y-=0.035;
    float d = B(p,size);
    p.x-=0.01;
    d = max(-dot(p,vec2(cos(a),sin(a))),d);

    p = prevP;
    p.x-=0.008;
    p.y+=0.035;
    a = radians(-25.);
    float d2 = B(p,size);
    p.x+=0.01;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);

    d = min(d,d2);

    return abs(d)-0.0005;
}

float arrow(vec2 p){
    float dir = SymdirY(p);
    p.x = abs(p.x)-0.05*mat_arrow_split;
    vec2 prevP = p;

    p.y+=(0.1*mat_a2_time)*dir;

    p.y=mod(p.y,0.07)-0.035;
    p.y+=0.025;
    if(dir == 1.){
        p.y-=0.05;
    }
    p.y*=dir*-1.;
    float a = radians(60.);
    p.x = abs(p.x)-0.1;
    float d = dot(p,vec2(cos(a),sin(a)));
    p.y+=0.03;
    float d2 = dot(p,vec2(cos(a),sin(a)));
    d = max(-d2,d);
    p = prevP;

    d = max(abs(p.x)-0.04,d);

    return abs(d)-0.0005 * pow(mat_arrow_thick,2.);
}

float arrowItem (vec2 p){
    vec2 prevP = p;
    float dist = 0.16;
    p.x = abs(p.x)-dist;
    p*=SkewX(radians(45.));
    float d = B(p,vec2(0.04,0.01));

    p = prevP;
    p.x = abs(p.x)-dist;
    p-=vec2(-0.04,0.07);
    p*=SkewY(radians(45.));
    float d2 = B(p,vec2(0.01,0.07));
    d = abs(min(d,d2))-0.0005;

    p =  prevP;
    p.y-=mat_a1_time*0.23;
    p.y = mod(p.y,0.3)-0.15;

    p.x = abs(p.x)-dist;
    p-=vec2(-0.04,0.0);
    p*=SkewY(radians(45.));
    d2 = B(p,vec2(0.01,0.1));
    p = prevP;
    d2 = max(-p.y+0.16,d2);

    p.x = abs(p.x);
    float a = radians(45.);

    p.y-=0.3;
    d2 = max(-dot(p,vec2(cos(a),sin(a))),d2);

    d = abs(min(d,d2))-0.0005;

    return d;
}

float arrows(vec2 p){
    vec2 prevP = p;
    p*=Rot(radians(45.));
    float d = arrow(p);

    p = prevP;
    p*=Rot(radians(-45.));
    float d2 = arrow(p);

    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(45.));

    d2 = B(p,vec2(0.32));
    d = max(-d2,d);

    p = prevP;

    p*=Rot(radians(45.));
    p.y=abs(p.y)-0.34;

    d2 = arrowItem(p);
    d = min(d,d2);

    p = prevP;

    p*=Rot(radians(-45.));
    p.y=abs(p.y)-0.34;

    d2 = arrowItem(p);
    d = min(d,d2);


    p = prevP;
    p*=Rot(radians(-45.));
    d2 = thunderIcon(p);

    p = prevP;
    p*=Rot(radians(45.));
    float d3 = thunderIcon(p);
    d2 = min(d2,d3);

    p = prevP;
    p*=Rot(radians(45.));

    float mask = B(p,vec2(0.38));
    d2 = max(-mask,d2);


    d = min(d,d2);

    return d;
}

float arrow2(vec2 p){
    vec2 prevP = p;

    float dir = SymdirY(p);
    p.y-=0.03;
    p.y+=(0.1*mat_a1_time)*dir;
    p.y=mod(p.y,0.08)-0.04;
    p.x = abs(p.x)-0.04;
    p*=SkewY(radians(45.*dir*-1.));
    float d = abs(B(p,vec2(0.025,0.015)))-0.0005;

    return d;
}

float arrowItem2 (vec2 p, float dist){
    vec2 prevP = p;
    p.x = abs(p.x)-dist;
    float d = B(p,vec2(0.0119,0.057));

    p = prevP;
    p.x = abs(p.x)-dist;
    p-=vec2(0.025,0.075);
    p*=Rot(radians(-45.));
    float d2 = B(p,vec2(0.04,0.0125));
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-dist;
    p-=vec2(0.0487,0.225);
    d2 = B(p,vec2(0.0127,0.13));
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    float a = radians(45.);

    p.y-=0.49;
    d = max(dot(p,vec2(cos(a),sin(a))),d);


    return abs(d)-0.0005;
}


float arrows2(vec2 p){
    vec2 prevP = p;
    float d = arrow2(p);

    p*=Rot(radians(90.));
    float d2 = arrow2(p);

    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(45.));

    d2 = B(p,vec2(0.32));
    d = max(-d2,d);

    p = prevP;

    p.x = abs(p.x)-0.47;
    p*=Rot(radians(90.));
    d2 = arrowItem2(p,0.095);
    d = min(d,d2);

    p = prevP;

    p.y = abs(p.y)-0.47;
    d2 = arrowItem2(p,0.095);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.6;
    p.x -= 0.05;
    p.y = abs(p.y)-0.1;
    p.x = mod(p.x,0.08)-0.04;
    d2 = abs(length(p)-0.01)-0.001;
    p = prevP;
    d2 = max(-(abs(p.x)-0.58),d2);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.46;
    p.y = abs(p.y)-0.17;
    d2 = abs(Tri(p,vec2(0.035),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.17;
    p.y = abs(p.y)-0.46;
    p*=Rot(radians(90.));
    d2 = abs(Tri(p,vec2(0.035),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.505;
    p.y = abs(p.y)-0.18;
    p*=Rot(radians(45.));
    d2 = abs(B(p,vec2(0.03,0.01)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.18;
    p.y = abs(p.y)-0.505;
    p*=Rot(radians(45.));
    d2 = abs(B(p,vec2(0.03,0.01)))-0.001;
    d = min(d,d2);

    return d;
}

float shapeBase(vec2 p, float s, int mode){
    vec2 prevP = p;
    p*=10.;

    if(mode ==1){
        p = abs(p);
        p-=mat_a1_time*0.5;
    } else {
        p*=2.;
        p.x-=0.2;
        p.y+=mat_a1_time*1.;
    }

    vec2 id = floor(p);
    vec2 gv = fract(p)-0.5;

    float n = Hash21(id);

    float w = 0.1;
    if(n<0.5 || n>=0.8){
        float dir = (n>=0.8)?1.0:-1.0;
        gv*=Rot(radians(dir*45.0));
        if(mode ==1){
            gv.x = abs(gv.x);
        }
        gv.x-=0.355;
    } else {
        w = 0.135;
    }

    w*=s;
    float d = B(gv,vec2(w,1.));
    return d;
}

float centerItem(vec2 p){
    vec2 prevP = p;

    float d = shapeBase(p,1. * mat_center_fill,1);

    p = prevP;
    p*=Rot(radians(45.));
    float d2 = B(p,vec2(0.2));
    d = max(d2,d);

    d2 = abs(B(p,vec2(0.22)))-0.005;
    p = prevP;
    d2 = max(abs(p.x)-0.1,d2);
    d = min(d,d2);

    p*=Rot(radians(45.));
    d2 = abs(B(p,vec2(0.22)))-0.005;
    p = prevP;
    d2 = max(abs(p.y)-0.1,d2);
    d = min(d,d2);

    p*=Rot(radians(45.));
    d2 = abs(B(p,vec2(0.24)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.2;
    p.y-=0.2;
    p*=Rot(radians(-225.));
    d2 = shapeBase(p,1. * mat_border,0);

    d2 = max(abs(p.x)-0.02,d2);
    d2 = max(abs(p.y)-0.3,d2);
    p = prevP;
    d2 = max(-p.y,d2);

    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.2;
    p.y+=0.2;
    p*=Rot(radians(225.));
    d2 = shapeBase(p,1. * mat_border,0);

    d2 = max(abs(p.x)-0.02,d2);
    d2 = max(abs(p.y)-0.3,d2);
    p = prevP;
    d2 = max(p.y,d2);
    d = min(d,d2);


    return d;
}

float circleItem(vec2 p){
    vec2 prevP = p;
    p*=Rot(radians(mat_a1_time*-30.));
    p = DF(p,2.);
    p -= vec2(0.06);
    p*=Rot( radians(-45.+sin(mat_a1_time*2.)*-10.));

    p.x*=2.;
    float d = abs(Tri(p,vec2(0.025),radians(45.)))-0.002;
    p = prevP;
    float d2 = abs(length(p)-0.05)-0.002;
    d = min(d,d2);
    d2 = length(p)-0.02;
    d = min(d,d2);
    return d;
}

float circleItems(vec2 p){
    vec2 prevP = p;

    p.x = abs(p.x)-0.77;
    p.y = abs(p.y)-0.32;
    float d = circleItem(p);

    p = prevP;
    p.x = abs(p.x)-0.61;
    p.y = abs(p.y)-0.21;
    p*=Rot(radians(45.));
    float d2 = B(p,vec2(0.07,0.04));
    float a = radians(-45.);
    p.y+=0.03;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);

    d = min(d,abs(abs(d2)-0.01)-0.001);

    p = prevP;
    p.x = abs(p.x)-0.815;
    p.y = abs(p.y)-0.19;
    p*=Rot(radians(-90.));
    d2 = abs(Tri(p,vec2(0.04),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.63;
    p.y = abs(p.y)-0.32;
    d2 = abs(length(p)-0.015)-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.84;
    p.y = abs(p.y)-0.46;
    d2 = abs(length(p)-0.025)-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.77;
    p.y = abs(p.y)-0.46;
    d2 = abs(length(p)-0.013)-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x)-0.7585;
    p.y = abs(p.y)-0.186;
    d2 = abs(B(p,vec2(0.04,0.006)))-0.001;
    d = min(d,d2);

    return d;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 p = uv;
    vec2 prevP = p;
    vec3 col = vec3(0.);
    float d = centerItem(p);
    col = mix(col,vec3(1.),S(d,0.0));

    d = arrows2(p);

    float d2 = arrows(p);
    d = min(d,d2);
    d2 = circleItems(p);
    d = min(d,d2);

    col = mix(col,vec3(0.5),S(d,0.0));


    out_color = vec4(sqrt(col),1.0);

    // General way to add transparency to any shader
    if (mat_back_mode == 1) {
        // Differentiate front & back colors using a hard cut w/ threshold
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = mat_back_color;
        } else {
            out_color = mat_front_color;
        }
    } else {
        // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * mat_back_sensitivity + mat_back_thresh);
    }

    return out_color;
}
