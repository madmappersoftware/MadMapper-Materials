/*
{
  "CATEGORIES" : [
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
            "LABEL": "Shift",
            "NAME": "mat_shift",
            "TYPE": "point2D",
            "MIN": [0.0,0.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.5,0.5]
        },

        {
            "LABEL" : "Direction",
            "MAX": [1.0,1.0],
            "MIN": [0.0,0.0],
            "DEFAULT":[0.0,0.0],
            "NAME": "direction",
            "TYPE": "point2D"
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
  "DESCRIPTION" : "Basic interlocking squares pattern with optional scroll.  From http:\/\/glslsandbox.com\/e#44617.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset * 10;


// modified colors by @hintz

vec4 materialColorForPixel(vec2 texCoord)
{
    vec3 c1 = vec3(1.0, 1.0, 0.0);
    vec3 cb = vec3(0.0);
    vec3 c3 = vec3(0.25, 1.0, 1.0);

    // vec2 b = 8.0 * fract(gl_FragCoord.xy/128.0);

    // vec2 uv = texCoord - vec2(0.5) - vec2(0.,iTime/4.);

    vec2 new_center = mat_shift;
    new_center.x = 1. - new_center.x;

    vec2 uv = 4. * mat_zoom * (texCoord - vec2(0.5)) - iTime * direction + vec2(0.5) + new_center;

    // uv *= 4 * mat_zoom;

    vec2 b = 8.0 * fract(uv);
    int x = int(b.x);
    int y = int(b.y);

    vec3 g = cb;

    if (y == 0)
    {
        g = x == 3 || x == 5 ? c1 : cb;
    }
    else if (y == 1)
    {
        g = x == 0 ? cb : x == 5 ? c1 : c3;
    }
    else if (y==2 || y==6)
    {
        g = x == 0 || x == 2 || x == 4 || x == 6 ? cb : (x == 1 || x == 7) ? c3 : c1;
    }
    else if (y == 3)
    {
        g = x==4 ? cb : x==7 ? c3 : c1;
    }
    else if (y == 4)
    {
        g = x==1||x==7 ? c3 : cb;
    }
    else if (y == 5)
    {
        g = x==4 ? cb : x==1 ? c3 : c1;
    }
    else if (y == 7)
    {
        g = x == 0 ? cb : x == 3 ? c1 : c3;
    }

    float sh = 0.0;
    vec3 d = 0.8 * g;

    if (y==3&&x==6 || y==5&&x==0 || y==1&&x==4 || y==7&&x==2)
    {
        g = mix(g, d, smoothstep(sh, 1.0, fract(b.x)));
    }
    else if (y==3&&x==0 || y==5&&x==2 || y==1&&x==6 || y==7&&x==4)
    {
        g = mix(g, d, smoothstep(1.0-sh, 0.0, fract(b.x)));
    }
    else if (y==0&&x==3 || y==6&&x==5 || y==2&&x==1 || y==4&&x==7)
    {
        g = mix(g, d, smoothstep(sh, 1.0, fract(b.y)));
    }
    else if (y==2&&x==3 || y==0&&x==5 || y==4&&x==1 || y==6&&x==7)
    {
        g = mix(g, d, smoothstep(1.0-sh, 0.0, fract(b.y)));
    }


    return vec4(g, 1.0);
}