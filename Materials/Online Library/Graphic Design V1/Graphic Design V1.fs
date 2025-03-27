/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "yasuo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/DsX3Rj",

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
            "LABEL": "Graphics/Ring 1",
            "NAME": "mat_ring1",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Ring 2",
            "NAME": "mat_ring2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Ring 3",
            "NAME": "mat_ring3",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Ring 4",
            "NAME": "mat_ring4",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 1",
            "NAME": "mat_radius1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 2",
            "NAME": "mat_radius2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 3",
            "NAME": "mat_radius3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 4",
            "NAME": "mat_radius4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 5",
            "NAME": "mat_radius5",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 6",
            "NAME": "mat_radius6",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 7",
            "NAME": "mat_radius7",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Graphics/Radius 8",
            "NAME": "mat_radius8",
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
            "LABEL": "Animation 3/Speed",
            "NAME": "mat_a3_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation 3/BPM Sync",
            "NAME": "mat_a3_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 3/Reverse",
            "NAME": "mat_a3_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 3/Offset",
            "NAME": "mat_a3_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 3/Offset Scale",
            "NAME": "mat_a3_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 3/Strob",
            "NAME": "mat_a3_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 3/Restart",
            "NAME": "mat_a3_restart",
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
            "DEFAULT": "Mix"
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
        {
            "NAME": "mat_a3_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a3_speed",
                "speed_curve":2,
                "reverse": "mat_a3_reverse",
                "strob" : "mat_a3_strob",
                "reset": "mat_a3_restart",
                "bpm_sync": "mat_a3_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale;
float mat_a2_time = mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale;
float mat_a3_time = mat_a3_time_source - mat_a3_offset * 8. * mat_a3_offset_scale;

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
#define DF(a,b) length(a) * cos( mod( atan(a.y,a.x)+6.28/(b*8.0), 6.28/((b*8.0)*0.5))+(b-1.)*6.28/(b*8.0) + vec2(0,11) )
#define B(p,s) max(abs(p).x-s.x,abs(p).y-s.y)
#define Tri(p,s,a) max(-dot(p,vec2(cos(-a),sin(-a))),max(dot(p,vec2(cos(a),sin(a))),max(abs(p).x-s.x,abs(p).y-s.y)))

float SimpleVesicaDistance(vec2 p, float r, float d) {
    p.x = abs(p.x);
    p.x+=d;
    return (length(p)-r) / pow(mat_ring1,2.);
}

float triOutside(vec2 p){
    vec2 prevP = p;

    p.x*=1.5;

    float d = abs(Tri(p,vec2(0.175),radians(45.)))-0.001;

    p*=1.5;
    p += vec2(0.0,0.05);
    float d2 = abs(Tri(p,vec2(0.175),radians(45.)))-0.001;
    d = min(d,d2);

    p*=1.5;
    p += vec2(0.0,0.06);
    d2 = abs(Tri(p,vec2(0.175),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.y+=0.11;
    d2 = abs(length(p)-0.015)-0.001;
    d = min(d,d2);

    return d / pow(mat_ring3,3.);
}

float triAnimation(vec2 p){
    vec2 prevP = p;

    p.y-=mat_a3_time*0.1+0.09;
    p.y = mod(p.y,0.16)-0.08;
    p.y -=0.01;
    p.x*=1.5;
    float d = Tri(p-vec2(0.0,0.032),vec2(0.015),radians(45.));
    float d2 = abs(Tri(p,vec2(0.02),radians(45.)))-0.001;
    d = min(d,d2);
    d2 = abs(Tri(p-vec2(0.0,-0.04),vec2(0.03),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    d = max(abs(p.y)-0.06,d);

    return d;
}

float waveCircle(vec2 p, float s, float numW, float amp, float deg, float thickness){
    float r = s+amp*cos(atan(p.y,p.x)*numW);
    float d = abs(length(p)-r)-thickness;
    p*=Rot(radians(deg));
    r = s+amp*cos(atan(p.y,p.x)*numW);
    float d2 = abs(length(p)-r)-thickness;
    d = min(d,d2);
    return d / pow(mat_ring4, 3.);
}

vec3 centerGraphic(vec2 p, vec3 col){
    p*=1.2;
    vec2 prevP = p;
    float thickness = 0.003 * pow(mat_ring2,1.5);
    float deg = -40.;
    float speed0 = -mat_a1_time*30.;
    float speed1 = mat_a2_time*20.; // wave
    float speed2 = -mat_a1_time*10.; // outer ring
    float speed3 = mat_a2_time*15.; // big ring
    float speed4 = mat_a2_time*25.;

    p*=Rot(radians(speed0));
    p = DF(p,3.0);
    p -= vec2(0.205);

    p*=Rot( radians(deg));
    float a = atan(p.x,p.y);
    float d = abs(length(p)-0.039)-thickness;
    d = max(-p.x,d);

    col = mix(col,vec3(1.)*a,S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed0));

    float deg2 = -44.5;
    p*=Rot( radians(deg2));
    p = DF(p,3.0);
    p -= vec2(0.2);

    p*=-1.;
    p*=Rot( radians(deg));
    a = atan(p.x,p.y);
    d = abs(length(p)-0.04)-thickness;
    d = max(-p.x,d);
    col = mix(col,vec3(1.)*a,S(d,0.0));

    p = prevP;

    d = abs(length(p)-0.29)-thickness;
    col = mix(col,vec3(1.),S(d,0.0));

    p*=Rot(radians(speed1));
    p+=sin(p*25.)*0.025;
    d = abs(length(p)-0.37)-thickness*0.2;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed0));

    p = DF(p,3.0);
    p -= vec2(0.217);
    d = abs(length(p)-0.007)-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed0));

    p*=Rot( radians(deg2));
    p = DF(p,3.0);
    p -= vec2(0.19);
    d = abs(length(p)-0.007)-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed0));

    p*=Rot( radians(deg2));
    p = DF(p,3.0);
    p -= vec2(0.225) * mat_radius8;
    d = abs(length(p)-0.015)-0.002;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed0));

    p = DF(p,3.0);
    p -= vec2(0.18) * mat_radius8;
    d = abs(length(p)-0.015)-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed2));
    p = DF(p,6.0);
    p -= vec2(0.161);
    d = max(-(length(p)-0.012),abs(length(prevP)-0.21)-0.02);
    p += vec2(0.024);
    d = max(-(length(p)-0.012),d);

    col = mix(col,vec3(1.),S(abs(d)-0.002,0.0));

    p = prevP;
    p*=Rot(radians(speed3));
    p = DF(p,6.0);
    p -= vec2(0.11) * mat_radius1;
    p*=Rot( radians(45.));
    p.y*=2.0;
    d = abs(SimpleVesicaDistance(p,0.12,0.11))-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed4));
    p = DF(p,3.0);
    p -= vec2(0.075) * mat_radius2;
    p*=Rot( radians(45.));
    d = abs(B(p,vec2(0.02,0.01)))-0.002;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(speed2));
    p*=Rot( radians(90.));
    p = DF(p,1.5);
    p -= vec2(0.04) * mat_radius3;
    d = abs(length(p)-0.025)-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot( radians(-60.));
    p = DF(p,1.5);
    p -= vec2(0.032) * mat_radius4;
    p*=Rot( radians(45.));
    d = B(p,vec2(0.04,0.003));
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot( radians(45.));
    d = abs(B(p,vec2(0.015)))-0.001;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(-speed2*0.5));
    p = DF(p,24.0);
    p -= vec2(0.29) * mat_radius5;
    d = abs(B(p,vec2(0.005, 0.2)))-0.0005;
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(-speed3));
    p = DF(p,2.);
    p -= vec2(0.4) * mat_radius6;
    p*=Rot( radians(45.));

    d = triOutside(p);

    d = max(-(length(prevP)-0.44),d);
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(-speed3));
    p = DF(p,2.);
    p -= vec2(0.32);
    p*=Rot( radians(45.));
    d = B(p,vec2(0.12, 0.2));

    p = prevP;
    d = max(-d, abs(length(p)-0.47)-0.01);
    col = mix(col,vec3(1.),S(abs(d)-0.001,0.0));

    p = prevP;
    p*=Rot(radians(-speed3));
    p = DF(p,2.);
    p -= vec2(0.32);
    p*=Rot( radians(45.));
    d = B(p,vec2(0.08, 0.2));

    p = prevP;
    d = max(-d, abs(length(p)-0.53)-0.01);
    col = mix(col,vec3(1.),S(abs(d)-0.001,0.0));

    p = prevP;
    p = DF(p,1.999);
    p -= vec2(0.475);
    p*=Rot( radians(45.));
    d = triAnimation(p);
    col = mix(col,vec3(1.),S(d,0.0));

    p = prevP;
    p*=Rot(radians(-speed3));
    d = waveCircle(p,0.485,10.,0.052,18.,0.001);

    p = prevP;
    p*=Rot(radians(-speed2));
    float d2 = waveCircle(p,0.585,10.,0.012,18.,0.001);
    d = min(d,d2);
    col = mix(col,vec3(1.),S(d,0.0));

    return col;
}

float graphicItem0(vec2 p){
    vec2 prevP2 = p;
    p.x+=mat_a3_time*0.2;
    p.x+=0.2;
    p.x = mod(p.x,0.2)-0.1;
    p.x-=0.02;
    p*=1.5;
    p.x*=-1.;
    vec2 prevP = p;

    float d = B(p,vec2(0.1,0.015));
    float a = radians(45.0);
    p.x = abs(p.x)-0.065;
    d = max(dot(p,vec2(cos(a),sin(a))),d);
    p = prevP;
    float d2 = B(p-vec2(0.09,-0.008),vec2(0.08,0.007));
    d = min(d,d2);
    p.x-=0.145;
    a = radians(55.0);
    d = max(dot(p,vec2(cos(a),sin(a))),d);

    p = prevP;

    p-=vec2(0.0,-0.01);
    d2 = B(p,vec2(0.06,0.01));
    a = radians(45.0);
    p.x = abs(p.x)-0.035;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);
    d = max(-d2,d);

    p = prevP2;
    d = max(abs(p.x)-0.25,d);

    return d;
}

float graphicItem1(vec2 p){
    vec2 prevP = p;
    p.x-=mat_a3_time*0.1;
    p.y-=0.03;
    p.x = mod(p.x,0.06)-0.03;
    float d = abs(Tri(p,vec2(0.02),radians(45.)))-0.001;
    p = prevP;

    p.x+=mat_a3_time*0.06;
    p.x+=0.03;
    p.y+=0.03;
    p.x = mod(p.x,0.06)-0.03;
    p.y*=-1.0;

    float d2 = Tri(p,vec2(0.02),radians(45.));
    d = min(d,d2);

    p = prevP;
    d = max(abs(p.x)-0.2,d);

    return d;
}

float graphicItem2(vec2 p){
    vec2 prevP = p;

    p.x+=mat_a3_time*0.08;
    p.x = mod(p.x,0.12)-0.06;
    p.x+=0.05;
    p.x*=-1.;
    p*=Rot(radians(90.));
    p.x*=3.;
    p.y*=0.3;
    float d = Tri(p,vec2(0.03),radians(45.));

    p = prevP;
    d = max(abs(p.x)-0.2,d);

    return d;
}

float graphicItem3(vec2 p){
    vec2 prevP = p;

    p.x-=mat_a3_time*0.12;
    p.x = mod(p.x,0.3)-0.15;

    float d = length(p)-0.01;
    float d2 = length(p-vec2(-0.06,0.0))-0.013;
    d = min(d,d2);
    d2 = length(p-vec2(0.06,0.0))-0.007;
    d = min(d,d2);
    d2 = length(p-vec2(-0.12,0.0))-0.014;
    d = min(d,d2);
    d2 = length(p-vec2(0.12,0.0))-0.005;
    d = min(d,d2);

    p = prevP;
    d = max(abs(p.x)-0.25,d);

    return d;
}

float graphicItem4(vec2 p){
    vec2 prevP = p;
    float d = abs(length(p)-0.04)-0.001;
    p*=Rot(radians(mat_a3_time*50.));
    float d2 = abs(length(p)-0.026)-0.006;
    d2 = max(-(abs(p.x)-0.01),d2);
    d = min(d,abs(d2)-0.001);

    return d;
}

float graphicItem5(vec2 p){
    vec2 prevP = p;
    p.x*=0.8;
    float d = abs(Tri(p,vec2(0.06),radians(45.)))-0.001;
    float d2 = abs(Tri(p,vec2(0.06),radians(45.)))-0.005;
    p.y+=0.05;
    p*=Rot(radians(45.+mat_a3_time*-70.));
    d2 = max(-(abs(p.y)-0.02),d2);
    d = min(d,d2);

    return d;
}

float graphicItem6(vec2 p){
    vec2 prevP = p;
    float d = abs(Tri(p,vec2(0.025),radians(45.)))-0.001;
    float d2 = Tri(p-vec2(0.055,-0.01),vec2(0.015),radians(45.));
    d = min(d,d2);
    return d;
}

float graphicItem7(vec2 p){
    vec2 prevP = p;
    p.x-=mat_a3_time*0.12;
    p.x = mod(p.x,0.07)-0.035;
    p.x-=0.0325;

    p*=vec2(0.9,1.5);
    p*=Rot(radians(90.));
    float d = Tri(p,vec2(0.05),radians(45.));
    d = max(-Tri(p-vec2(0.,-0.03),vec2(0.05),radians(45.)),d);
    d = abs(d)-0.001;
    p = prevP;
    d = max(abs(p.x)-0.15,d);
    return d;
}

float graphicItems(vec2 p) {

    vec2 prevP = p;

    p-=vec2(-0.71,-0.33);
    p*=Rot(radians(-25.));
    float d = graphicItem0(p);

    p = prevP;

    p-=vec2(-0.7,-0.16);
    p*=Rot(radians(-15.));
    float d2 = graphicItem1(p);

    d = min(d,d2);

    p = prevP;

    p-=vec2(-0.585,-0.44);
    p*=Rot(radians(-35.));
    d2 = graphicItem2(p);

    d = min(d,d2);

    p = prevP;

    p-=vec2(-0.73,-0.26);
    p*=Rot(radians(-20.));
    d2 = abs(graphicItem3(p))-0.001;

    d = min(d,d2);

    p = prevP;

    p-=vec2(-0.67,-0.4);
    p*=Rot(radians(-30.));
    d2 = graphicItem3(p);

    d = min(d,d2);

    p = prevP;

    p-=vec2(-0.78,-0.09);
    p*=Rot(radians(-10.));
    d2 = graphicItem3(p);

    d = min(d,d2);

    p = prevP;
    d2 = graphicItem4(p-vec2(-0.34,-0.45));
    d = min(d,d2);

    d2 = graphicItem5(p-vec2(-0.48,-0.42));
    d = min(d,d2);

    d2 = graphicItem6(p-vec2(-0.265,-0.46));
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

    col = centerGraphic(p,col);

    p = abs(p);
    p*=Rot(radians(180.));
    float d = graphicItems(p);
    col = mix(col,vec3(0.6),S(d,0.));

    p = prevP;
    p.x = abs(p.x)-0.77;
    d = graphicItem7(p);
    col = mix(col,vec3(0.6),S(d,0.));
    out_color = vec4(col,1.0);


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
