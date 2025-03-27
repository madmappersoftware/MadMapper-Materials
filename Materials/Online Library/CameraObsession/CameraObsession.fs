/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [


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
  "DESCRIPTION" : "Abstract camera lens patterns.  Converted from https://www.shadertoy.com/view/MssXDn"
}
*/

// #include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

#define NumberOfParticles 64
#define Pi 3.141592

vec3 palette(float x)
{
    return vec3(
        sin(x*2.0*Pi)+1.5,
        sin((x+1.0/3.0)*2.0*Pi)+1.5,
        sin((x+2.0/3.0)*2.0*Pi)+1.5
    )/2.5;
}

float starline(vec2 relpos,float confradius,float filmsize)
{
    if(abs(relpos.y)>confradius) return 0.0;
    float y=relpos.y/confradius;
    float d=abs(relpos.x/filmsize);
    return sqrt(1.0-y*y)/(0.0001+d*d)*0.00001;
}

float star(vec2 relpos,float confradius,float filmsize)
{
    vec2 rotpos=mat2(cos(Pi/3.0),-sin(Pi/3.0),sin(Pi/3.0),cos(Pi/3.0))*relpos;
    vec2 rotpos2=mat2(cos(Pi/3.0),sin(Pi/3.0),-sin(Pi/3.0),cos(Pi/3.0))*relpos;
    return starline(relpos,confradius,filmsize)+
        starline(rotpos,confradius,filmsize)+
        starline(rotpos2,confradius,filmsize);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 screenpos = texCoord - vec2(0.5);

    screenpos *= mat_zoom;

    float focaldistance=0.5+sin(iTime*0.05)*0.013;
    float focallength=0.100;
    float filmsize=0.036;
    float minconf=filmsize/1000.0;
    float lensradius=focallength/1.0;

    float filmdistance=1.0/(1.0/focallength-1.0/focaldistance);

    vec3 c=vec3(0.0);
    for(int i=0;i<NumberOfParticles;i++)
    {
        float t=float(i)/float(NumberOfParticles);
        float a=t*2.0*Pi+iTime*0.1;

        vec3 pos=vec3(sin(a)+2.0*sin(2.0*a),cos(a)-2.0*cos(2.0*a),-sin(3.0*a))*0.01;

        float a1=0.1*iTime;
        pos.xz*=mat2(cos(a1),-sin(a1),sin(a1),cos(a1));
        //float a2=0.1;
        //pos.yz*=mat2(cos(a2),-sin(a2),sin(a2),cos(a2));

        pos.z+=0.5;

        float intensity=0.0000002;

        vec2 filmpos=pos.xy/pos.z*filmdistance;
        float confradius=lensradius*filmdistance*abs(1.0/focaldistance-1.0/pos.z)+minconf;

        float diffusedintensity=intensity/(confradius*confradius);

        vec3 colour=palette(t);

        vec2 relpos=filmpos-screenpos/2.0*filmsize;
        if(length(relpos)<confradius) c+=colour*diffusedintensity;

        c+=colour*diffusedintensity*star(relpos,confradius,filmsize);
    }

    return vec4(pow(c,vec3(1.0/2.2)),1.0);




}