/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [
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
        }
    ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#47807.1"
}
*/
float iTime = mat_time - mat_offset * 10;

//https://beesandbombs.tumblr.com/post/87834642859/zoomin
#ifdef GL_ES
precision mediump float;
#endif
//#extension GL_OES_standard_derivatives : enable
#define  PI 3.1415926535897932384626433832795
#define TAU 6.283185307179586476925286766559
mat2 rot(float a){return mat2(cos(a),sin(a),-sin(a),cos(a));}
float ease(float p){return(3.*p*p)-(2.*p*p*p);}
float ease(float p,float g){return(p<.5)?(.5*pow(2.*p,g)):(1.-.5*pow(2.*(1.-p),g));}
float c01(float x){return clamp(x,0.,1.);}
const int N=20;
float original(vec2 p){
    float t=fract(iTime/2.);

    float r=100.,d=16.,scal=1.3;

    float tt=ease(t,4.),O=0.;
    for(int a=2;a<=40;a++){
        vec2 p1=p*rot(PI*(float(a)+2.*tt)/float(N));
        p1*=pow(scal,float(a)+(12.*mod(tt,2.)));
        for(int i=0;i<N;i++){
            vec2 p2=p1*rot(TAU*(float(i)/float(N)));
            O+=1.-c01((length(p2-vec2(0,r))-(d/2.))*2.);
        }
    }
    return c01(O);
}
vec4 materialColorForPixel( vec2 texCoord ){
    // vec2 uv=(gl_FragCoord.xy-(RENDERSIZE.xy/2.))/RENDERSIZE.y;
    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    return vec4(vec3(original(uv)),1.);
}