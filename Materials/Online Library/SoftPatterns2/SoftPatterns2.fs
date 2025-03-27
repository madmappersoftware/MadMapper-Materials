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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#46424.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;

//#extension GL_OES_standard_derivatives : enable

#define PI 3.14159265358
#define TWO_PI 6.28318530718


vec3 colorA = vec3(0.149, 0.141, 0.912);
vec3 colorB = vec3(1.000, 0.833, 0.224);

vec3 rgb2hsb( in vec3 c ){
    vec4 K = vec4(0.0, -1.0 / .0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz),
                 vec4(c.gb, K.xy),
                 step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r),
                 vec4(c.r, p.yzx),
                 step(p.x, c.r));
    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)),
                d / (q.x + e),
                q.x);
}

//  Function from Iñigo Quiles
//  https://www.shadertoy.com/view/MsS3Wc
vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),
                             6.0)-3.0)-1.0,
                     0.0,
                     1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix(vec3(1.0), rgb, c.y);
}

/* Coordinate and unit utils */
vec2 coord(in vec2 p) {
    p = p / RENDERSIZE.xy;
    // correct aspect ratio
    if (RENDERSIZE.x > RENDERSIZE.y) {
        p.x *= RENDERSIZE.x / RENDERSIZE.y;
        p.x += (RENDERSIZE.y - RENDERSIZE.x) / RENDERSIZE.y / 2.0;
    } else {
        p.y *= RENDERSIZE.y / RENDERSIZE.x;
        p.y += (RENDERSIZE.x - RENDERSIZE.y) / RENDERSIZE.x / 2.0;
    }
    // centering
    p -= 0.5;
    p *= vec2(-1.0, 1.0);
    return p;
}
// #define rx 1.0 / min(RENDERSIZE.x, RENDERSIZE.y)
// #define uv gl_FragCoord.xy / RENDERSIZE.xy
// #define st coord(gl_FragCoord.xy)
// #define mx coord(u_mouse)

mat2 rot(in float angle) {
    return mat2(cos(angle), -sin(angle),
                sin(angle),  cos(angle));
}

float wave(in vec2 pt, in float f, in float v) {
    float r = length(pt);
    float a = atan(pt.y, pt.x);
    return sin(r * TWO_PI * f - v * iTime);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    vec2 st = uv;

    // #define rx 1.0 / min(RENDERSIZE.x, RENDERSIZE.y)
    float rx = 1.0;
    // #define uv gl_FragCoord.xy / RENDERSIZE.xy
    // vec2 st = coord(uv);
    // vec2 mx = coord(u_mouse);


    vec2 pt = st;

    float f = 10.0 * abs(sin(0.1 * iTime));
    float v = 10.0;
    float g = wave(pt, f, v);

    for (float k = 0.0; k < 8.0; k++) {
        float t = PI / 4.0 * k;
        vec2 offset = 0.5 * vec2(cos(t), sin(t));
        g += wave(pt + offset, f, v);
    }

    g *= 0.5;
    g = step(g, 0.0);

    vec3 rgb = mix(colorA, colorB, g);

    return vec4(rgb, 1.0);
}