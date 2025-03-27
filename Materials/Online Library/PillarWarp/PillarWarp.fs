/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#61431.1",
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
        "Label": "modD",
        "NAME": "modD",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.1
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
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;


/*
 * Original shader from: https://www.shadertoy.com/view/WlKXRR
 */

//  --------[ Original ShaderToy begins here ]---------- //
// thanks to mla and Kali!
// They have super nice examples on inversion

// it's really simple, basically
// p /= dot(p,p);
// SDFs
// return distance*dot(p,p);


// and ofc Inigo quilez for pallete!


vec3 getRd(vec3 ro, vec3 lookAt, vec2 uv){
    vec3 dir = normalize(lookAt - ro);
    vec3 right = normalize(cross(vec3(0,1,0), dir));
    vec3 up = normalize(cross(dir, right));
    return normalize(dir + right*uv.x + up * uv.y);
}
#define pmod(p,x) mod(p,x) - 0.5*x
#define mx 20.

#define pi acos(-1.)
#define tau (2.*pi)
#define T (iTime*0.125)
#define rot(x) mat2(cos(x),-sin(x),sin(x),cos(x))
#define pal(a,b,c,d,e) (a + b*cos(tau*(c*d + e)))

vec3 pA = vec3(0);

vec2 map(vec3 p){
    vec2 d = vec2(10e6);

    float sc = 2.4;
    float dp = dot(p,p);
    p /= dp;
    p*= sc;

    float m = sin(2.*8.*T/tau)*tau;
    p=sin(p+vec3(T*tau,1.4 - 1.*T*tau,0. + m*0.));
    pA = p;
    float modD = 3.14/2.;

    d.x = min(d.x, length(p) - 3.6); // something arbitrarily big
    d.x = max(d.x,-length(p) + 1.4);

    return d*dp/sc;
}

vec3 glow = vec3(0);
vec2 trace(vec3 ro, vec3 rd,inout vec3 p,inout float t, inout bool hit){
    vec2 d = vec2(10e6);
    t = 0.; hit = false; p = ro;

    for(int i = 0; i < 100; i++){
        d = map(p);
        glow += exp(-d.x*90.);
        if(d.x < 0.0001){
            hit = true;
            break;
        }
        t += d.x;
        p = ro + rd*t;
    }


    return d;
}

vec3 getNormal(vec3 p){
    vec2 t = vec2(0.001,0);
    return normalize(map(p).x - vec3(
        map(p - t.xyy).x,
        map(p - t.yxy).x,
        map(p - t.yyx).x
    ));
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    vec3 col = vec3(0);

    uv *= 1. + dot(uv,uv)*1.9*sin(iTime);
    vec3 ro = vec3(0.,0.,1.)*0.9;

    ro.x += sin(mx)*0.;
    vec3 lookAt = vec3(0.);
    vec3 rd = getRd(ro, lookAt, uv);
    rd.xy *= rot(sin(iTime*0.5)*0.6);
    rd.xz *= rot(sin(iTime*0.75)*0.2);
    vec3 p = vec3(0.); float t = 0.; bool hit = false;
    vec2 d = trace(ro, rd, p, t, hit);

    if(hit){
        vec3 pAA = pA;
        // float modD = 0.1;
        float id = floor(pA.x/modD);
        pA = pmod(pA, modD);
        col += pal(0.,vec3(1,0.7 + sin(id + iTime*0.25)*0.24,1.1)*1., vec3(5.,3.75 + cos(iTime + length(p))*0.04,1.1 ), 1.5, id*0.3 + pAA.z*0.5 + pAA.y*0.2 + iTime*0.12);
        col *= step(abs(sin(id*4.))*1., 0.7);
        col -= exp((abs(pA.x) - modD*0.5)*100.);

        col -= exp(-length(p)*20.)*10.;
        //col += smoothstep(0.01,0., length(pA.x) - modD*0.175);
    }

    col -= glow*0.016;
    col = clamp(col, 0., 1.);

    col *= 1.9;
    col = pow(col, vec3(0.45));
    col *= 1. - 1.*pow(abs(uv.x)*0.55,2.9)*0.5;
    col *= 1. - 1.*pow(abs(uv.y)*1.0,2.9)*0.5;

    return vec4(col,1.0);




}