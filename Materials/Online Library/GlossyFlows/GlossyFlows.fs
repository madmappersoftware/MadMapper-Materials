/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#61493.0",
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
        "Label": "Gloss Level",
        "NAME": "mat_gloss",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Gloss Mod",
        "NAME": "mat_gloss_modifier",
        "TYPE": "float",
        "MIN": 16.0,
        "MAX": 256.0,
        "DEFAULT": 128.0
    },
    {
        "LABEL": "Flow/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Flow/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Flow/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Color/Grayscale",
        "NAME": "mat_color_grayscale",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/BPM Sync",
        "NAME": "mat_color_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Reverse",
        "NAME": "mat_color_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Speed",
        "NAME": "mat_color_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Offset",
        "NAME": "mat_color_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Color/Strob",
        "NAME": "mat_color_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Scroll/Animate",
        "NAME": "mat_scroll",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/BPM Sync",
        "NAME": "mat_scroll_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Reverse",
        "NAME": "mat_scroll_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Speed",
        "NAME": "mat_scroll_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Angle",
        "NAME": "mat_scroll_angle",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "Label": "Scroll/Offset",
        "NAME": "mat_scroll_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Strob",
        "NAME": "mat_scroll_strob",
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
    },
    {
        "NAME": "mat_color_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_color_speed",
            "speed_curve":2,
            "strob" : "mat_color_strob",
            "reverse": "mat_color_reverse",
            "bpm_sync": "mat_color_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
    {
        "NAME": "mat_scroll_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_scroll_speed",
            "speed_curve":2,
            "strob" : "mat_scroll_strob",
            "reverse": "mat_scroll_reverse",
            "bpm_sync": "mat_scroll_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 20;

vec3 shape( in vec2 p )
{
    p *= 2.0;

    vec3 s = vec3( 0.0 );
    vec2 z = p;
    for( int i=0; i<8; i++ )
    {
        // transform
        z += cos(z.yx + cos(z.yx + cos(z.yx+0.5*iTime) ) );

        // orbit traps
        float d = dot( z-p, z-p );
        s.x += 1.0/(1.0+d);
        s.y += d;
        s.z += sin(atan(z.y-p.y,z.x-p.x));

    }

    return s / 8.0;
}

vec4 materialColorForPixel(vec2 texCoord) {

    float color_time =  mat_color_time - mat_color_offset * 10;

    float scroll_angle = mat_scroll_angle * 0.5;
    float scroll_time = mat_scroll_time - mat_scroll_offset * 10.;
    if (!mat_scroll) {
        scroll_time = 0.;
    }
    float scroll_time_x = scroll_time * cos(PI * scroll_angle);
    float scroll_time_y = scroll_time * sin(PI * scroll_angle);

    vec2 pc = texCoord - vec2(0.5);

    pc *= mat_zoom * 2.;
    vec2 pc_orig = pc;

    pc.x -= scroll_time_x;
    pc.y -= scroll_time_y;

    vec2 pa = pc + vec2(0.04,0.0);
    vec2 pb = pc + vec2(0.0,0.04);

    // shape (3 times for diferentials)
    vec3 sc = shape( pc );
    vec3 sa = shape( pa );
    vec3 sb = shape( pb );

    vec3 col;
    if (mat_color_grayscale) {
        col = mix( vec3(0.), vec3(1.), sc.x );
    } else {
        col = mix( vec3(0.08,0.02,0.15), vec3(0.6,1.1,1.6), sc.x );
    }

    col = mix( col, col.zxy, smoothstep(-0.5,0.5,cos(0.5*color_time)) );
    col *= 0.15*sc.y;
    col += 0.4*abs(sc.z) - 0.1;

    // light
    vec3 nor = normalize( vec3( sa.x-sc.x, 0.01, sb.x-sc.x ) );
    float dif = clamp(0.5 + 0.5*dot( nor,vec3(0.5773) ),0.0,1.0);
    col *= 1.0 + 0.7*dif*col;
    col += 0.3 * mat_gloss * pow(nor.y,mat_gloss_modifier);

    // vignetting
    col *= 1.0 - 0.1*length(pc_orig);

    return vec4( col, 1.0 );




}