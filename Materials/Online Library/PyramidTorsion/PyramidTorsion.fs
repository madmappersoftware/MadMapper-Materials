/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "AntoineC, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/lsVczK by AntoineC.  A slightly disquieting animation of torsioned (tortured?) pyramids!",
  "VSN": "1.0",
  "INPUTS" : [

    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 2.0
    },
    {
        "Label": "Phase X",
        "NAME": "phase_x",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 16.0,
        "DEFAULT": 2.0
    },
    {
        "Label": "Phase Y",
        "NAME": "phase_y",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 16.0,
        "DEFAULT": 8.0
    },
    {
        "Label": "Edge Blur",
        "NAME": "edge_blur",
        "TYPE": "float",
        "MIN": 50.0,
        "MAX": 1000.0,
        "DEFAULT": 500.0
    },
    {
        "LABEL": "Vertical Flip",
        "NAME": "v_flip",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Clip Sides",
        "NAME": "clip",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
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

float Pyramid(vec2 uv, float t)
{
    float r = 2.0*t*pow(1.0 - length(uv)/sqrt(2.0), 1.5);

    uv = mat2(cos(r),sin(r),-sin(r),cos(r))*uv;

    uv = abs(uv)/sqrt(2.0);
    return max(uv.x, uv.y);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv0 = texCoord - vec2(0.5);
    // uv0 -= shift;

    uv0 *= mat_zoom;

    if (!v_flip) {
        uv0.y = 1.-uv0.y;
    }

    float t = 0.1*iTime;

    vec2 uv = (uv0+vec2(1.0))/2.0;

    float eps = 1.0/edge_blur;
    // Tile:
    uv*= 4.0;
    vec2 uvi = floor(uv);
    uv = 2.0*fract(uv)-vec2(1.0);

    // Row/column phase:
    t = t + uvi.x/phase_x + uvi.y/phase_y;

    // Smooth triangle tweener:
    t = 2.0*abs(fract(t)-0.5);
    t = 2.0*smoothstep(0., 1., t) - 1.0;

    // Normal from function gradient:
    vec2 h = vec2(4.0*eps, 0);
    vec2 grad = vec2(
        Pyramid(uv + h.xy, t) - Pyramid(uv - h.xy, t),
        Pyramid(uv + h.yx, t) - Pyramid(uv - h.yx, t)) / (2.0*h.x);
    vec3 n = cross(vec3(1.0, 0.0, grad.x), vec3(0.0, 1.0, grad.y));
    n = normalize(n);


    // Shading:
    vec3 l = normalize(vec3(0.0, -3.0, 1.0));
    float cs = dot(l,n);

    // Diffuse + weird back lighting!
    float c;
    c = 0.7*max(0.0, cs) + 0.07*max(0.0, -cs);
    c += 0.1*pow(max(0.0, -cs), 0.5);

    // Contrast correction:
    c = pow(c, 0.8);

    // "Ambient occlusion":
    uv = abs(uv);
    c *= smoothstep(0.0, 0.2, pow(1.0-max(uv.x, uv.y)/sqrt(2.0), 2.0));


    // Vertical gradient:
    c *= mix(0.3, 1.3, pow((uv0.y + 1.0)/2.0, 1.5));

    // Clipping:
    if (clip) {
        c *= step(abs(uv0.x), 1.0);
    }


    // Temporal noise/dithering:
    //  (to attenuate solarization on bad LCD screens)
    vec3 color;
    color = vec3(c);

    float rand = fract(sin((iTime + dot(uv0, vec2(12.9898, 78.233))))* 43758.5453);
    color = (floor(90.0*color) + step(rand, fract(90.0*color)))/90.0;


    return vec4(color,1.0);




}