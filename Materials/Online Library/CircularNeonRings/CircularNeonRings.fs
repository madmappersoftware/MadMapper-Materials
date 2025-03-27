/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [
    {
      "NAME" : "mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "MIN" : [
        0,
        0
      ]
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#44932.1"
}
*/


// by Natspir

#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

//#extension GL_OES_standard_derivatives : enable


float circle(vec2 st, float size, float w, float s){
    s/=10.0;
    float c = smoothstep(size-s,size+s,length(st*(1.0+w)))-smoothstep(size-s,size+s,length(st*(1.0-w)));
    c*=cos(atan(st.x,st.y)*10.0*mouse.x+iTime*mouse.y*10.0);
    return c;
}

mat2 Rot(float a){
    a*=0.0174533;
    return mat2(cos(a),-sin(a),sin(a),cos(a));
}
vec4 materialColorForPixel(vec2 texCoord) {

    // vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy );
    vec2 p = texCoord;

    vec2 p1 = p;
    if(p1.x>0.5){
        p1.x=1.0-p1.x;
        //p=p1;
    }
    if(p1.y>0.5){
        p1.y=1.0-p1.y;
        //p=p1;
    }
    p=mix(p,p1,0.0);
    p-=0.5;
    // p.x*=max(RENDERSIZE.x/RENDERSIZE.y,RENDERSIZE.y/RENDERSIZE.x);

    p *= mat_zoom;

    float s = 0.051;
    float w = 0.025;
    float c = 1.0;
    float c1 = 0.0;
    float cTmp = 0.0;
/*  for(int i = 0; i<5;i++){
        c += circle(p+vec2(cos(float(i))/2.0,sin(float(i))/2.0),0.25,w,s);
    }*/
    for(int i = 0; i<3;i++){
        for(int j = 0; j<5;j++){
            float t = iTime*15.0*float(i);
            cTmp = circle(p*Rot(120.0*float(i)+iTime*25.0)+vec2(0.0,float(j)*0.05),float(9-j)*0.05,w,s);
            c *= 1.0-cTmp;
            c1 += cTmp;
        }
    }
    c = 1.0-c;
    //c=min(c,c1);
    float anim = sin(length(p)*10.0+iTime)*0.5+0.5;
    vec4 col = vec4(mix(vec3(1.0,0.0,0.0)*2.5-anim,vec3(1.0,0.95,0.0)*1.50+anim,c)*clamp(c,0.0,1.0),1.0);
    return col;

}