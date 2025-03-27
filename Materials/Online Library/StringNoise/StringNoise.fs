/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#46717.0"
}
*/


//--- mucous membrane
// by Catzpaw 2018

#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

float wave(vec2 p){float v=sin(p.x+sin(p.y)+sin(p.y*.43));return v*v;}

const mat2 rot=mat2(.5,.86,-.86,.5);
float map(vec2 p)
{
    float v=0.;
    v+=wave(p);
    p.x+=iTime;p*=rot;
    v+=wave(p);
    p.x+=iTime*.17;
    p*=rot;
    v+=wave(p);
    v=abs(1.5-v);
    return v;
}

vec4 materialColorForPixel(vec2 texCoord) {
    // vec2 uv=(gl_FragCoord.xy*2.-RENDERSIZE.xy)/min(RENDERSIZE.x,RENDERSIZE.y);

    vec2 uv = texCoord - vec2(0.5);

    vec2 p=normalize(vec3(uv.xy,2.3)).xy*19.;

    p *= 2. * mat_zoom;

    p.y+=iTime*2.;
    float v=map(p);
    //vec3 c=mix(vec3(1.8,.4,.5),vec3(1.,.3+map(p*2.5)*.1,.5),v);
    //gl_FragColor = vec4(v,v,v,1);
    float vs = smoothstep(0.1, 0.2, v);
    return vec4(vs,vs,vs,1.0);
}