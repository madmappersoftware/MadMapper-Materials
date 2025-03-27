/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Patricio Gonzalez Vivo, adapted by Jason Beyers",
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
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#49243.0"
}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

// Original Author: Patricio Gonzalez Vivo
// Title: Cosmic Ripples

vec3 mod289(vec3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec2 mod289(vec2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec3 permute(vec3 x) { return mod289(((x*34.0)+1.0)*x); }

float snoise(vec2 v) {
    const vec4 C = vec4(0.211324865405187,  // (3.0-sqrt(3.0))/6.0
                        0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                        -0.577350269189626,  // -1.0 + 2.0 * C.x
                        0.024390243902439); // 1.0 / 41.0
    vec2 i  = floor(v + dot(v, C.yy) );
    vec2 x0 = v -   i + dot(i, C.xx);
    vec2 i1;
    i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
    vec4 x12 = x0.xyxy + C.xxzz;
    x12.xy -= i1;
    i = mod289(i); // Avoid truncation effects in permutation
    vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
        + i.x + vec3(0.0, i1.x, 1.0 ));

    vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
    m = m*m ;
    m = m*m ;
    vec3 x = 2.0 * fract(p * C.www) - 1.0;
    vec3 h = abs(x) - 0.5;
    vec3 ox = floor(x + 0.5);
    vec3 a0 = x - ox;
    m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );
    vec3 g;
    g.x  = a0.x  * x0.x  + h.x  * x0.y;
    g.yz = a0.yz * x12.xz + h.yz * x12.yw;
    return 130.0 * dot(m, g);
}

float circle (vec2 st, float radius) {
    vec2 pos = st;
    return smoothstep(1.0-radius,1.0-radius+radius*0.2,1.-dot(pos,pos)*PI);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 st = texCoord - vec2(0.5);

    st *= mat_zoom;

    vec4 color = vec4(0.);

    float t = iTime*.1;
    vec2 pol = vec2(0.0);
    pol.x = atan(st.x,st.y)/PI;
    pol.y = dot(st,st)*1.;

    float pct = 0.0;
    pct += snoise(vec2(log(pol.y),abs(pol.x)+t*5.));
    pol.x *= sin(iTime*0.2)*3.8;
    //pol.x*=3.2;
    pct += snoise(vec2(log(pol.y)*(1.5+cos(t*1.1)),abs(pol.x)-t))*2.;

    float a = atan(st.x,st.y);
    float t1 = iTime*0.25;
    float r = smoothstep(0.,.2+abs(cos(t*.2)),pct) * circle(st,.8) * smoothstep(.00,.05,pol.y);

    color += vec4(r, abs(r*r*cos(a*5.+t1*1.13)),r*cos(a*3.-t1*11.77+r+.5*PI),1.0)*0.8 +vec4(0.2,0.0,0.1,0.0);
    color += vec4( vec3( r*r*r*2., abs(r) * 0.5+sin(  iTime / 3.0 ) * .75, sin(  iTime / 3.0 ) * 0.75 ), 1.0 )*.2;
    return vec4(color);



}