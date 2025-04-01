/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "leon, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/4sSyDd",

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
            "LABEL": "Smoke/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 50,
            "DEFAULT": 50
        },
        {
            "LABEL": "Smoke/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Smoke/Level",
            "NAME": "mat_level",
            "TYPE": "float",
            "MIN": 0.01,
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
            "LABEL": "Travel/Speed",
            "NAME": "mat_travel_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Travel/BPM Sync",
            "NAME": "mat_travel_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Travel/Reverse",
            "NAME": "mat_travel_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Travel/Offset",
            "NAME": "mat_travel_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Travel/Offset Scale",
            "NAME": "mat_travel_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Travel/Strob",
            "NAME": "mat_travel_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Travel/Restart",
            "NAME": "mat_travel_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Camera/Speed",
            "NAME": "mat_cam_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Camera/BPM Sync",
            "NAME": "mat_cam_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Camera/Reverse",
            "NAME": "mat_cam_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Camera/Offset",
            "NAME": "mat_cam_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Camera/Offset Scale",
            "NAME": "mat_cam_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Camera/Strob",
            "NAME": "mat_cam_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Camera/Restart",
            "NAME": "mat_cam_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Background/Alpha",
            "NAME": "mat_alpha",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },


        {
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Saturation",
            "NAME": "mat_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "mat_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
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
            "NAME": "mat_cam_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_cam_speed",
                "speed_curve":2,
                "reverse": "mat_cam_reverse",
                "strob" : "mat_cam_strob",
                "reset": "mat_cam_restart",
                "bpm_sync": "mat_cam_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_travel_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_travel_speed",
                "speed_curve":2,
                "reverse": "mat_travel_reverse",
                "strob" : "mat_travel_strob",
                "reset": "mat_travel_restart",
                "bpm_sync": "mat_travel_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_cam_time = mat_cam_time_source - mat_cam_offset * 8. * mat_cam_offset_scale;
float mat_travel_time = mat_travel_time_source - mat_travel_offset * 8. * mat_travel_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
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



// training for modeling shapes
// using koltes code as base https://www.shadertoy.com/view/XdByD3
// using iq articles
// using mercury library

// #define PI 3.1415926535897932384626433832795
#define TAU 2*PI
// #define t TIME

mat2 rz2 (float a) { float c=cos(a), s=sin(a); return mat2(c,s,-s,c); }
float sphere (vec3 p, float r) { return length(p)-r; }
float iso (vec3 p, float r) { return dot(p, normalize(sign(p)))-r; }
float cyl (vec2 p, float r) { return length(p)-r; }
float cube (vec3 p, vec3 r) { return length(max(abs(p)-r,0.)); }
vec2 modA (vec2 p, float count) {
    float an = TAU/count;
    float a = atan(p.y,p.x)+an*.5;
    a = mod(a, an)-an*.5;
    return vec2(cos(a),sin(a))*length(p);
}
float smin (float a, float b, float r)
{
    float h = clamp(.5+.5*(b-a)/r,0.,1.);
    return mix(b, a, h) - r*h*(1.-h);
}

float map (vec3 p)
{
    float sph3 = sphere(p, 3.);
    p.yz *= rz2(mat_cam_time*.2);
    p.xy *= rz2(mat_cam_time*.3);

    float d = length(p);

    float a = atan(p.y,p.x);
    float l = length(p.xy)-2.;
    p.xy = vec2(l,a);

    float as = PI*0.3;
    p.z += sin(a*2.+sin(l*4.))*.5;

    float wave1 = sin(p.y*6.)*.5+.5;
    float wave2 = .5+.5*sin(p.z*3.+mat_time);

    p.x -= sin(p.z*1.+mat_travel_time)*.5;
    p.z = mod(p.z+mat_travel_time,as)-as*.5;

    float sphR = .2-.1*wave1;
    float sphC = .3;
    float sphN = 0.2;
    float sph1 = sphere(vec3(p.x,mod(sphN*p.y/TAU+mat_time*.1,sphC)-sphC*.5,p.z), sphR);

    p.xz *= rz2(p.y*3.);
    p.xz = modA(p.xz, 3.);
    p.x -= 0.3*wave2;
    float cyl1 = cyl(p.xz, 0.02) / mat_fill;
    float sph2 = sphere(vec3(p.x,mod(p.y*2.-mat_time,1.)-.5,p.z), .1);

    return smin(sph1, smin(cyl1,sph2,.2), .2);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    vec3 ro = vec3(uv,-5), rp = vec3(uv,1), mp = ro;
    int i = 0;
    int count = mat_iterations;
    for(;i<count;++i) {
        float md = map(mp);
        if (md < 0.001) {
            break;
        }
        mp += rp*md*.35;
    }
    float r = float(i)/float(count);
    out_color = vec4(1);
    out_color *= smoothstep(.0,10.,length(mp-ro) / mat_level);
    out_color *= r;
    out_color = 1. - out_color;
    if (!mat_alpha) {
        out_color.a = 1.;
    }




    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply Hue Shift and saturation
    if (mat_hue_shift > 0.01 || mat_saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+mat_hue_shift));
        hsv.y = max(hsv.y + mat_saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;


    return out_color;
}
