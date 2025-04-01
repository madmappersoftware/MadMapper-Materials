/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "yasuo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/cdjGzD",

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
            "LABEL": "Graphics/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Animation/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation/Restart",
            "NAME": "mat_a1_restart",
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
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale;

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
    uv *= mat_scale * 0.925;

    uv = mirrorUV(uv);

    uv.y -= 0.015;

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
#define Tri(p,s,a) max(-dot(p,vec2(cos(-a),sin(-a))),max(dot(p,vec2(cos(a),sin(a))),max(abs(p).x-s.x,abs(p).y-s.y)))
#define B(p,s) max(abs(p).x-s.x,abs(p).y-s.y)

// The following data is generated by my tool.
vec2 svgPolygonData[34] = vec2[](vec2(0.1803,0.3769),vec2(0.09420002,0.354),vec2(0.04120001,0.3294),vec2(-0.01189999,0.3049),vec2(-0.0631,0.2727),vec2(-0.1072,0.237),vec2(-0.1487,0.194),vec2(-0.1832,0.1441),vec2(-0.2047,0.09520001),vec2(-0.2229,0.03929999),vec2(-0.2306,-0.0135),vec2(-0.2304,-0.07270001),vec2(-0.2218,-0.1314),vec2(-0.2062,-0.187),vec2(-0.1823,-0.2386),vec2(-0.1472,-0.2851),vec2(-0.1091,-0.3234),vec2(-0.06389999,-0.3516),vec2(-0.01620001,-0.3702),vec2(0.03920001,-0.3802),vec2(0.09659997,-0.3722),vec2(0.151,-0.347),vec2(0.1941,-0.3118),vec2(0.225,-0.2687),vec2(0.2414,-0.2238),vec2(0.2422,-0.1742),vec2(0.2302,-0.1154),vec2(0.2092,-0.059),vec2(0.1846,-0.006),vec2(0.1576,0.05610001),vec2(0.1374,0.118),vec2(0.1255,0.1803),vec2(0.1322,0.2427),vec2(0.1563,0.3115));

float SimpleVesicaDistance(vec2 p, float r, float d) {
    p.x = abs(p.x);
    p.x+=d;
    return length(p)-r;
}

float waveCircle(vec2 p, float s, float numW, float amp, float deg, float thickness){
    float r = s+amp*cos(atan(p.y,p.x)*numW);
    float d = abs(length(p)-r)-thickness;
    p*=Rot(radians(deg));
    r = s+amp*cos(atan(p.y,p.x)*numW);
    float d2 = abs(length(p)-r)-thickness;
    d = min(d,d2);
    return d;
}

float paiselyOutlineItem0(vec2 p, float i){
    vec2 prevP = p;
    float d = abs(length(p)-0.024)-0.001;
    p*=Rot(radians(mat_a1_time*30.*i));
    float d2 = abs(length(p)-0.014)-0.0025;
    d2 = max(-(abs(p.x)-0.005),d2);
    d = min(d,d2);
    return d;
}

float paiselyOutlineItem1(vec2 p, float deg){
    vec2 prevP = p;
    float d = abs(length(p)-0.03)-0.0005;
    p*=Rot(radians(sin(mat_a1_time)*deg));
    float d2 = abs(length(p)-0.03)-0.003;
    d2 = max(-p.x,d2);
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(mat_a1_time*deg*0.5));
    d2 = abs(length(p)-0.015)-0.002;
    d2 = max(-(abs(p.x)-0.005),d2);
    d = min(d,d2);

    return d;
}

float paiselyBase(vec2 p){
    vec2 prevP = p;

    float d = 10.;

    int num = svgPolygonData.length();

    float t = mat_a1_time*2.0;
    vec2 a = vec2(0.);
    vec2 b = vec2(0.);

    for( int i=0; i<num; i++ ){
        int scene = i+int(mod(t,float(num)));
        if(scene>=num)  scene -= num;
        if(scene<num-1){
            a = svgPolygonData[scene];
            b = svgPolygonData[scene+1];
        } else {
            a = svgPolygonData[scene];
            b = svgPolygonData[0];
        }

        vec2 newPos = mix(a,b,mod(t,1.));

        float d2 = paiselyOutlineItem0(p-newPos,float(i)*0.3);

        d = min(d,d2);
    }

    return d;
}

float arrow(vec2 p){
    float d = B((p-vec2(-0.015,0.0))*Rot(radians(-45.)),vec2(0.01,0.03));
    float d2 = B((p-vec2(0.025,0.012))*Rot(radians(45.)),vec2(0.01,0.018));
    d = min(d,d2);
    return d;
}

float paiselyItem0(vec2 p){
    vec2 prevP = p;
    p*=Rot(-radians(mat_a1_time*40.));
    p = DF(p,4.0);
    p -= vec2(0.08);
    p*=1.55;
    p*=Rot( radians(150.));
    float d = arrow(p);

    p = prevP;
    p*=Rot(radians(mat_a1_time*30.));
    p = DF(p,0.75);
    p -= vec2(0.03);
    p*=Rot( radians(45.));
    float d2 = B(p,vec2(0.02, 0.5));

    p = prevP;
    d2 = max(-d2, abs(length(p)-0.05)-0.01);

    d = min(d,d2);

    p = prevP;
    p*=Rot(-radians(mat_a1_time*20.));
    d2 = B(p,vec2(0.015));
    d = min(d,d2);

    return abs(d)-0.001;
}

float paiselyItem1(vec2 p){
    vec2 prevP = p;
    p*=Rot(radians(mat_a1_time*50.));
    p = DF(p,2.0);
    p -= vec2(0.055);

    p*=Rot( radians(45.));
    float d = abs(SimpleVesicaDistance(p,0.13,0.11))-0.0005;

    p = prevP;
    p*=Rot(radians(mat_a1_time*50.));
    p = DF(p,4.0);
    p -= vec2(0.04);

    p*=Rot( radians(45.));
    float d2 = abs(SimpleVesicaDistance(p,0.12,0.115))-0.0005;

    d = min(d,d2);
    return d;
}

float paiselyItem2(vec2 p){
    vec2 prevP2 = p;

    p*=Rot(-radians(mat_a1_time*40.));

    vec2 prevP = p;

    float d = length(p)-0.1;
    d = max(-(abs(p.x)-0.03),d);

    p*=Rot(radians(90.));
    p.x-=0.02;
    p.y-=0.03;

    float d2 = B(p,vec2(0.06,0.02));
    float a = radians(45.);
    p.x = abs(p.x)-0.06;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);

    d = max(-d2,d);

    p = prevP;
    p*=Rot(radians(90.));
    p.x+=0.02;
    p.y+=0.03;

    d2 = B(p,vec2(0.06,0.02));
    a = radians(-45.);
    p.x = abs(p.x)-0.06;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);
    d = max(-d2,d);

    p = prevP;
    a = radians(-10.);
    p.x = abs(p.x)-0.07;
    d = max(dot(p,vec2(cos(a),sin(a))),d);

    d = abs(d)-0.001;

    p = prevP;
    d2 = abs(length(p)-0.12)-0.001;

    d = min(d,d2);

    p.x = abs(p.x)-0.09;
    d2 = abs(B(p,vec2(0.005,0.03)))-0.0005;
    d = min(d,d2);

    p = prevP;

    d2 = abs(B(p,vec2(0.01,0.08)))-0.001;
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(mat_a1_time*50.));
    p = DF(p,6.0);
    p -= vec2(0.1);

    p*=Rot( radians(45.));
    d2 = abs(B(p,vec2(0.01)))-0.0005;
    d = min(d,d2);

    p = prevP;

    p*=Rot( radians(sin(mat_a1_time*3.)*45.));
    d2 = abs(length(p)-0.16)-0.002;
    d2 = max(-(abs(p.x)-0.15),d2);
    d = min(d,d2);

    p = prevP;
    d2 = waveCircle(p,0.18,10.,0.01,18.,0.001);
    d = min(d,d2);
    return d;
}

float paiselyItem3(vec2 p){
    vec2 prevP = p;
    p*=Rot( -radians(mat_a1_time*25.));
    float d = waveCircle(p,0.15,10.,0.03,18.,0.001);
    p = prevP;
    p*=Rot( radians(mat_a1_time*15.));
    float d2 = waveCircle(p,0.11,8.,0.02,18.,0.002);
    d = min(d,d2);
    p = prevP;
    p*=Rot( -radians(mat_a1_time*20.));
    d2 = waveCircle(p,0.07,7.,0.02,18.,0.001);
    d = min(d,d2);
    p = prevP;
    p*=Rot( radians(mat_a1_time*10.));
    d2 = waveCircle(p,0.03,6.,0.02,18.,0.002);
    d = min(d,d2);
    return d;
}

float paiselyItem4(vec2 p){
    vec2 prevP = p;
    p*=Rot(-radians(mat_a1_time*30.));
    p = DF(p,6.0);
    p -= vec2(0.1);

    p*=Rot( radians(45.));
    float d = B(p,vec2(0.001,0.015));

    p = prevP;
    p*=Rot(-radians(mat_a1_time*30.));
    p*=Rot( radians(7.));
    p = DF(p,6.0);
    p -= vec2(0.095);
    p*=Rot( radians(45.));
    float d2 = B(p,vec2(0.001,0.01));
    d = min(d,d2);

    p = prevP;
    p*=Rot( -radians(45.-sin(mat_a1_time)*150.));
    p.y=abs(p.y)-0.17;
    p.y*=-1.;
    d2 = abs(Tri(p,vec2(0.02),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(mat_a1_time*25.));
    p = DF(p,1.25);
    p -= vec2(0.06);
    p*=Rot( radians(45.));
    d2 = B(p,vec2(0.02, 0.5));

    p = prevP;
    d2 = abs(max(-d2, abs(length(p)-0.09)-0.02))-0.001;
    d = min(d,d2);

    p = prevP;
    p*=Rot( radians(45.+sin(mat_a1_time*0.5)*150.));
    p.y=abs(p.y)-0.05;
    d2 = abs(Tri(p,vec2(0.02),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    d2 = length(p)-0.01;
    d = min(d,d2);

    return d;
}

float paiselyItem5(vec2 p){
    vec2 prevP = p;
    p*=Rot(radians(mat_a1_time*50.));
    p = DF(p,4.0);
    p -= vec2(0.09);

    p*=Rot( radians(45.));
    float d = abs(SimpleVesicaDistance(p,0.115,0.10))-0.001;

    p = prevP;
    p*=Rot(-radians(mat_a1_time*40.));
    p = DF(p,2.0);
    p -= vec2(0.033);

    p*=Rot( radians(45.));
    float d2 = abs(B(p,vec2(0.01)))-0.0005;
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(12.));
    p*=Rot(radians(mat_a1_time*50.));
    p = DF(p,4.0);
    p -= vec2(0.115+sin(mat_a1_time*3.)*0.005);

    p*=Rot( radians(225.));
    d2 = abs(Tri(p,vec2(0.02),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p*=Rot(-radians(mat_a1_time*30.));
    d2 = abs(length(p)-0.21)-0.006;
    d2 = max(p.x+0.1,d2);
    d = min(d,d2);

    p = prevP;
    d2 = abs(length(p)-0.21)-0.0005;
    d = min(d,d2);

    return d;
}

float paiselyItem6(vec2 p){
    vec2 prevP = p;

    float d = abs(length(p)-0.09)-0.001;
    p*=Rot(radians(mat_a1_time*25.));
    p = DF(p,1.25);
    p -= vec2(0.09);
    p*=Rot( radians(45.));
    float d2 = B(p,vec2(0.02, 0.5));
    d = max(d2,d);

    p = prevP;
    d2 = abs(max(-d2, abs(length(p)-0.09)-0.012))-0.001;

    d = min(d,d2);

    p = prevP;
    p*=Rot(-radians(sin(mat_a1_time)*135.));
    p = DF(p,0.5);
    p -= vec2(0.02);
    p*=Rot( radians(45.));
    d2 = B(p,vec2(0.03, 0.5));

    p = prevP;
    d2 = max(-d2, abs(length(p)-0.05)-0.005);
    d = min(d,d2);

    p = prevP;
    d2 = abs(length(p)-0.12)-0.005;
    p*=Rot(radians(-mat_a1_time*60.+50.));
    float deg = 135.;
    float a = radians(deg);
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);
    a = radians(-deg);
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);
    d = min(d,d2);

    p = prevP;
    d2 = abs(length(p)-0.02)-0.001;
    d = min(d,d2);

    return d;
}

float paiselyItem7(vec2 p){
    p*=Rot(radians(mat_a1_time*50.));

    vec2 prevP = p;
    p = DF(p,0.75);
    p -= vec2(0.04);

    p*=Rot( radians(45.));
    float d = abs(SimpleVesicaDistance(p,0.115,0.10))-0.001;

    p = prevP;
    p*=Rot(radians(60.));
    p = DF(p,0.75);
    p -= vec2(0.03);

    p*=Rot( radians(225.));
    float d2 = abs(Tri(p,vec2(0.03),radians(45.)))-0.002;
    d = min(d,d2);

    p = prevP;
    p*=Rot(radians(60.));
    p = DF(p,0.75);
    p -= vec2(0.07);

    p*=Rot( radians(45.));
    d2 = B(p,vec2(0.07,0.001));

    p = prevP;
    p*=Rot(-radians(mat_a1_time*35.));
    d2 = max(-(abs(p.x)-0.035),d2);
    d2 = max(-(abs(p.y)-0.035),d2);
    d = min(d,d2);

    return d;
}

float paiselyItem8(vec2 p){
    //p.y = mod(p.y,0.12)-0.06;
    vec2 prevP = p;
    p-=vec2(-0.01,0.04);
    float d =  B(p,vec2(0.02,0.05));
    float a = radians(-22.);
    p.x-=0.01;
    d = max(-dot(p,vec2(cos(a),sin(a))),d);
    p = prevP;
    p-=vec2(0.01,-0.04);
    float d2 = B(p,vec2(0.02,0.05));
    a = radians(-22.);
    p.x+=0.01;
    d2 = max(dot(p,vec2(cos(a),sin(a))),d2);
    d = min(d,d2);

    return abs(d)-0.0005;
}

float paiselyItem9(vec2 p){
    vec2 prevP = p;

    float d = 10.;
    for(float i = 0.; i<2.; i++){
        p*= 1.0+i*0.03;
        p*=Rot(radians(i*30.0+mat_a1_time*50.));
        p = abs(p);
        p-=0.025+i*0.01;
        float d2 = arrow(p);
        d = min(d,d2);
    }

    return abs(d)-0.001;
}

float paisely(vec2 p){
    vec2 prevP = p;
    float d = paiselyBase(p);
    float d2 = 10.;

    p*=0.95;
    d2 = paiselyItem0(p-vec2(0.04,-0.18));
    d = min(d,d2);

    p*=1.6;
    d2 = paiselyItem4(p-vec2(-0.045,0.14));
    d = min(d,d2);

    p = prevP;
    p-=vec2(-0.145,-0.075);
    p*=2.3;
    d2 = paiselyItem6(p);
    d = min(d,d2);

    p = prevP;
    p*=0.95;
    d2 = paiselyOutlineItem1(p-vec2(0.105,0.0),-160.);
    d = min(d,d2);

    p = prevP;
    p-=vec2(0.06,0.25);
    p*=0.9;
    d2 = paiselyOutlineItem1(p,-170.);
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

    p*=0.9;
    p.x = abs(p.x)-0.35;
    p.x*=-1.0;
    p.y-=-0.04;
    p*=Rot(radians(-20.));
    float d = paisely(p);

    p = prevP;
    p-= vec2(0.0,0.23);
    float d2 =  paiselyItem2(p);
    d = min(d,d2);

    p = prevP;
    p-= vec2(0.0,-0.42);
    p*=1.8;
    d2 =  paiselyItem7(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-= vec2(0.72,0.32);
    p*=1.2;
    d2 =  paiselyItem3(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-= vec2(0.735,-0.335);
    p*=1.1;
    d2 =  paiselyItem1(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-= vec2(0.78,-0.02);
    p*=1.3;
    d2 =  paiselyItem9(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.035,-0.05);
    p*=Rot(radians(10.));
    d2 =  paiselyItem8(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.53,-0.43);
    p*=Rot(radians(45.));
    d2 =  paiselyItem8(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.53,0.43);
    p*=Rot(radians(60.));
    d2 =  paiselyItem8(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.25,0.38);
    p*=Rot(radians(10.));
    d2 =  paiselyItem8(p);
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.25,0.25);
    d2 =abs( length(p)-0.02)-0.001;
    d = min(d,d2);
    p-=vec2(-0.04,-0.12);
    d2 = abs(length(p)-0.015)-0.0005;
    d = min(d,d2);
    p-=vec2(-0.09,-0.1);
    d2 = length(p)-0.01;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.17,0.4);
    p.y*=-1.;
    d2 = abs(Tri(p,vec2(0.04),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.12,-0.44);
    d2 = abs(Tri(p,vec2(0.03),radians(45.)))-0.001;
    d = min(d,d2);

    p = prevP;
    p.y+=0.02;
    p = abs(p);
    p-=vec2(0.83,0.15);
    d2 = abs( length(p)-0.03)-0.001;
    d = min(d,d2);

    p-=vec2(-0.12,-0.03);
    d2 = length(p)-0.015;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.53,0.36);
    d2 = abs( length(p)-0.015)-0.001;
    d = min(d,d2);

    p = prevP;
    p.x = abs(p.x);
    p-=vec2(0.6,-0.46);
    d2 = abs( length(p)-0.015)-0.001;
    d = min(d,d2);

    p = prevP;
    p-=vec2(0.0,-0.3);
    d2 = abs( length(p)-0.015)-0.001;
    d = min(d,d2);

    col = mix(col,vec3(1.),S(d,0.0));
    // Output to screen
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
