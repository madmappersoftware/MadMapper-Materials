/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#61566.0",
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
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

// #define PI 3.14159265359
#define EPS 0.0001

float random(vec2 st){
    return fract(sin(dot(st, vec2(12.9898, 78.233))) * 43758.5453123);
}

vec2 random2D(vec2 st){
    st = vec2(dot(st, vec2(127.1, 311.7)), dot(st, vec2(269.5, 183.3)));
    return 2.0 * fract(sin(st) * 43758.5453123) - 1.0;
}

float noise(in vec2 st){
    vec2 i = floor(st * 20.);
    vec2 f = fract(st * 20.);

    vec2 u = f*f*(3.0 - 2.0 * f);

    return mix(
        mix(
            dot(random2D(i + vec2(0.0, 0.0)), f - vec2(0.0, 0.0)),
            dot(random2D(i + vec2(1.0, 0.0)), f - vec2(1.0, 0.0)),
            u.x
        ),
        mix(
            dot(random2D(i + vec2(0.0, 1.0)), f - vec2(0.0, 1.0)),
            dot(random2D(i + vec2(1.0, 1.0)), f - vec2(1.0, 1.0)),
            u.x
        ),
        u.y
    );
}

float circle(in vec2 st, float r, float width, float n){
    float w = (width + noise(st) * width) / 2.;
    float l =  length(st) + noise(st) * 0.2 - iTime * 0.1;
    // l = length(st);
    l = mod(l , n);
    return min(step(l, r + w), step(r - w, l));
}
float circle2(in vec2 st, float r, float width, float n){
    float w = (width + noise(st) * width) / 2.;
    float l =  length(st) + noise(st) * 0.2 + iTime * 0.1;
    // l = length(st);
    l = mod(l , n);
    return min(step(l, r + w), step(r - w, l));
}

float lectangle(vec2 st, vec2 size){
    st = abs(st) - size + noise(st + iTime * 0.05) * 0.5;
    return step(max(st.x, st.y), 0.);
}

vec3 rotate(in vec3 p, float ax, float ay, float az){
    return p *
        mat3(
            1., 0., 0.,
            0., cos(ax), sin(ax),
            0., sin(ax), cos(ax)
        ) *
        mat3(
            cos(ay), 0., sin(ay),
            0., 1., 0.,
            -sin(ay), 0., cos(ay)
        ) *
        mat3(
            cos(az), -sin(az), 0.,
            sin(az),  cos(az), 0.,
            0., 0., 1.
        );
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 st = texCoord - vec2(0.5);
    st *= mat_zoom;

    vec2 p = rotate(vec3(st, 0.), 0., 0., noise(st) * 0.2  + iTime * 0.3).xy;
    vec2 r = rotate(vec3(st, 0.), 0., 0., noise(st) * 0.2  - iTime * 0.5).xy;
    // p = st;
    vec2 q = rotate(vec3(st, 0.), 0., 0., noise(st) * 5.).xy;
    vec3 col = vec3(0.1333, 0.6235, 0.9451);
    vec3 col2 = vec3(0.3608, 0.9333, 0.8941);
    float f = 0., g = 0., h = 0.;

    // f = circle(p, 0.1, 0.1, 0.6);
    // f = max(f, circle2(p, 0.1, 0.03, 0.4));
    f = max(f, lectangle(q, vec2(.2, .2)));
    g = circle2(r, 0.1, 0.1, 0.5);
    h = min(pow(0.2 / length(st), 5.), 5.);
    h = max(h, lectangle(q, vec2(.2, .2)));

    float n = 0.;
    st = rotate(vec3(st, 0.), 0., 0., -iTime * 0.1).xy;
    for(int j = 0; j < 30; j++){
        st = rotate(vec3(st, 0.), 0., 0., PI / 15000. * float(j)).xy;
        for(int i = 1; i < 3; i++){
            // n += noise(rotate(vec3(st,0.), 0.,0., u_time + noise(st + noise(vec2(st * float(i) * 0.01 + u_time * 0.001)))).xy) + float(i) * 0.01;
            n += noise(st * float(i) * 3.) + float(i) * 0.01;
            // n += noise(vec2(u_time * 0.02));
        }
        n = pow(n, 6.);
        // n = min(1. ,n);
    }
    n = min(2., n);
    // h *= n;
    n = pow(n, 1.);
    g = pow(g, 3.);
    f = pow(f, 5.);
    // n = 0.;
    // g = 0.;
    // h = 0.;
    float hoge = max(g,h);
    // gl_FragColor = vec4(h * col2 + g * col2 + n * col, 1.);
    return vec4(hoge * col2 + n * col, 1.);




}