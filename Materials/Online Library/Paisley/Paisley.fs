/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "yasuo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wldcRn",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },

        {
            "LABEL": "Shape/Lines",
            "NAME": "mat_lines",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Size 1",
            "NAME": "mat_size1",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Size 2",
            "NAME": "mat_size2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "Label": "Scroll/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Offset Scale",
            "NAME": "mat_scroll_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Restart",
            "NAME": "mat_scroll_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Color/Tint",
            "NAME": "mat_tint",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Mix"
        },
        {
            "LABEL": "Alpha/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },





    ],
    "GENERATORS": [
        {
            "NAME": "mat_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "reverse": "mat_reverse",
                "strob" : "mat_strob",
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "reset": "mat_scroll_restart",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_scroll_time = mat_scroll_time_source - mat_scroll_offset * 8. * mat_scroll_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}


#define Rot(a) mat2(cos(a),-sin(a),sin(a),cos(a))
#define antialiasing(n) n/(mat_detail * 1000. * 2.)
#define S(d,b) smoothstep(antialiasing(1.0),b,d)
#define BASE_COLOR vec3(0.0,0.0,0.0)

vec2 bend(vec2 p, float k){
    float c = cos(k*p.y);
    float s = sin(k*p.y);
    mat2  m = mat2(c,-s,s,c);
    vec2  q = p*m;
    return q;
}

// https://iquilezles.org/articles/distfunctions2d
float sdVesica(vec2 p, float r, float d)
{
    p = abs(p);
    float b = sqrt(r*r-d*d);
    return ((p.y-b)*d>p.x*b) ? length(p-vec2(0.0,b))
                             : length(p-vec2(-d,0.0))-r;
}

// https://iquilezles.org/articles/distfunctions2d
float sdUnevenCapsule( vec2 p, float r1, float r2, float h )
{
    p.x = abs(p.x);
    float b = (r1-r2)/h;
    float a = sqrt(1.0-b*b);
    float k = dot(p,vec2(-b,a));
    if( k < 0.0 ) return length(p) - r1;
    if( k > a*h ) return length(p-vec2(0.0,h)) - r2;
    return dot(p, vec2(a,b) ) - r1;
}

float paiselyDist(vec2 p, float sy, float scale) {
    vec2  q = bend(p,1.5);

    q.y*=sy*scale;
    q*=0.8*scale;
    float d = sdUnevenCapsule(q,0.15,0.02*scale,0.35*scale);
    return d / mat_lines;
}

vec3 paiselyTex(vec2 p, vec3 col, float dir, float t) {
    p*=Rot(radians(t*30.0*dir));
    p*=1.2;
    vec2 prevP = p;
    p = abs(p);
    p -= vec2(0.05,0.05) * mat_size1;
    float d = abs(sdVesica(p*Rot(radians(45.0)),0.1,0.07))-0.005;
    col = mix(col,BASE_COLOR,S(d,0.0));

    p = prevP;

    p *= Rot(radians(45.0)) / mat_size2;
    p = abs(p);
    p -= vec2(0.05,0.05) * mat_size1;
    d = abs(sdVesica(p*Rot(radians(45.0)),0.1,0.07))-0.005;
    col = mix(col,BASE_COLOR,S(d,0.0));

    return col;
}

vec3 paisely(vec2 p, vec3 col, float t) {
    vec3 baseCol = BASE_COLOR;
    vec2 pos = vec2(0.0,-0.1);
    vec2 prevP = p;
    float d = abs(paiselyDist(p-pos,0.9,0.88))-0.002;
    float d2 = abs(paiselyDist(p-pos,0.87,1.05))-0.001;
    float d3 = abs(paiselyDist(p-pos,0.85,1.25))-0.003;
    float d4 = abs(paiselyDist(p-pos,0.9,0.82))-0.001;
    col = mix(col,baseCol,S(d,0.0));
    col = mix(col,baseCol*1.2,S(d2,0.0));
    col = mix(col,baseCol,S(d3,0.0));
    col = mix(col,baseCol,S(d4,0.0));

    p*=3.2;
    col = paiselyTex(p-vec2(0.4,0.55),col,1.0,t);

    p = prevP;
    p*=1.8;
    col = paiselyTex(p-vec2(0.11,0.15),col,-1.0,t);

    p = prevP;
    col = paiselyTex(p-vec2(0.01,-0.11),col,1.0,t);

    p = prevP;
    p*=3.5;
    col = paiselyTex(p-vec2(-0.13,0.13),col,1.0,t);

    p = prevP;
    p*=2.8;
    col = paiselyTex(p-vec2(0.1,-0.82),col,-1.0,t);

    p = prevP;
    p*=3.2;
    col = paiselyTex(p-vec2(-0.2,-0.89),col,1.0,t);

    p = prevP;
    p*=3.2;
    col = paiselyTex(p-vec2(0.4,-0.78),col,1.0,t);

    p = prevP;
    p*=4.2;
    col = paiselyTex(p-vec2(1.15,1.25),col,1.0,t);

    p = prevP;

    p.x -=0.01;
    p.x = abs(p.x);
    p.x -= 0.15;
    d = length(p-vec2(0.01,0.0))-0.02;
    col = mix(col,baseCol,S(d,0.0));

    p = prevP;
    p.x = abs(p.x);
    p.x -= 0.18;
    d = length(p-vec2(0.0,-0.12))-0.02;
    col = mix(col,baseCol,S(d,0.0));

    p = prevP;
    d = length(p-vec2(-0.14,-0.22))-0.02;
    col = mix(col,baseCol,S(d,0.0));

    d = length(p-vec2(-0.072,0.11))-0.02;
    col = mix(col,baseCol,S(d,0.0));

    d = length(p-vec2(0.063,0.215))-0.013;
    col = mix(col,baseCol,S(d,0.0));

    d = length(p-vec2(0.187,0.13))-0.017;
    col = mix(col,baseCol,S(d,0.0));

    return col;
}

vec3 renderTexture(vec2 p, vec3 col, float t) {
    p*=1.3;

    vec2 prevP = p;

    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;
    col = paisely(p,col,t);
    p = prevP;

    p.y+=0.1;
    p.x+=0.475;
    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;

    p*=1.2;
    col = paisely(p*Rot(radians(-180.0)),col,t);
    p = prevP;

    p.x+=0.3;
    p.y+=-0.35;
    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;
    p*=1.8;
    col = paisely(p*Rot(radians(120.0)),col,t);
    p = prevP;

    p.x+=0.08;
    p.y+=-0.31;
    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;
    p*=1.2;
    col = paiselyTex(p,col,1.0,t);
    p = prevP;

    p.x+=0.28;
    p.y+=-0.57;
    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;
    p*=1.8;
    col = paiselyTex(p,col,-1.0,t);
    p = prevP;

    p.x+=0.56;
    p.y+=-0.3;
    p.x = mod(p.x,1.0)-0.5;
    p.y = mod(p.y,0.8)-0.4;
    p*=2.1;
    col = paiselyTex(p,col,1.0,t);

    return  col;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    float t = mat_scroll_time*0.1;

    uv.y+=t;

    vec2 prevUV = uv;
    // vec3 col = vec3(0.99,0.98,0.95);
    // vec3 col = vec3(1.);
    vec3 col = mat_tint.rgb;
    float t2 = mod(mat_time,8000.0);
    col = renderTexture(uv,col,t2);

    out_color = vec4(col,1.0);

    // out_color *= mat_front_color;

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    if (mat_luma_to_alpha) {
        // General way to add transparency to any shader
        if (mat_back_mode == 1) {
            // Differentiate front & back colors using a hard cut w/ threshold
            if (mat_luma(out_color.rgb) < mat_back_thresh) {
                out_color.a = 0.;
            }
        } else {
            // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
            out_color.a = mat_luma(out_color.rgb) + mat_back_thresh;
        }
    }

    return out_color;
}
