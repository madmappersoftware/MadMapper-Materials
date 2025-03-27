/*{
    "CREDIT": "sukupaper, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wd3yRs",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Camera/Shift",
            "NAME": "mat_cam_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

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
            "Label": "Camera/Strob",
            "NAME": "mat_cam_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "Label": "Rotate/Amount",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rot_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rot_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rot_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rot_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Strob",
            "NAME": "mat_rot_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Color Swipe/Speed",
            "NAME": "mat_color_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color Swipe/BPM Sync",
            "NAME": "mat_color_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Swipe/Reverse",
            "NAME": "mat_color_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Color Swipe/Offset",
            "NAME": "mat_color_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Color Swipe/Strob",
            "NAME": "mat_color_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_hue_shift",
            "LABEL": "Color/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        }


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
                "bpm_sync": "mat_cam_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_color_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_color_speed",
                "speed_curve":2,
                "reverse": "mat_color_reverse",
                "strob" : "mat_color_strob",
                "bpm_sync": "mat_color_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_rot_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rot_speed",
                "speed_curve":2,
                "reverse": "mat_rot_reverse",
                "strob" : "mat_rot_strob",
                "bpm_sync": "mat_rot_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 16.;
float mat_cam_time = mat_cam_time_source - mat_cam_offset * 16.;
float mat_rot_time = mat_rot_time_source - mat_rot_offset * 16.;
float mat_color_time = mat_color_time_source - mat_color_offset * 16.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// Author: sukupaperu
// Title: subtle scales

#define P 6.2831853071795

float rand(in vec2 st){ return fract(sin(dot(st.xy,vec2(12.9898,78.233)))*43758.585); }
vec3 SpectrumPoly(in float x) {
    // https://www.shadertoy.com/view/wlSBzD
    return (vec3( 1.220023e0,-1.933277e0, 1.623776e0)+(vec3(-2.965000e1, 6.806567e1,-3.606269e1)+(vec3( 5.451365e2,-7.921759e2, 6.966892e2)+(vec3(-4.121053e3, 4.432167e3,-4.463157e3)+(vec3( 1.501655e4,-1.264621e4, 1.375260e4)+(vec3(-2.904744e4, 1.969591e4,-2.330431e4)+(vec3( 3.068214e4,-1.698411e4, 2.229810e4)+(vec3(-1.675434e4, 7.594470e3,-1.131826e4)+ vec3( 3.707437e3,-1.366175e3, 2.372779e3)*x)*x)*x)*x)*x)*x)*x)*x)*x;
}
mat2 rot(in float a) { return mat2(cos(a),sin(a),-sin(a),cos(a)); }

vec4 tri(in vec2 p) {
    #define R .86602544038
    vec2 r = vec2(R,1.);
    vec2 pr = p*r;
    vec3 floorPy = vec3(floor(p.y)*.5,floor(p.y)*.5 + .5,0.);

    vec3 pTm1 = (fract(pr.xxy + floorPy.xyz));
    vec4 pTm2 = (pTm1.xzyz - vec3(.5,1./3.,2./3.).xyxz)/r.xyxy;
    vec2 pT = fract(pr + vec2(p.y*.5,0.));
    vec2 pId;
    if(pT.y < pT.x) {
        pId = floor(pr + floorPy.xz);
        pT = pTm2.xy;
    } else {
        pId = 1. + floor(pr + floorPy.yz);
        pT = pTm2.zw;
    }
    return vec4(pT,pId);
}
float cosa(in float x, in float y, in float w) { return cos((x/P)*y + w)*.5+.5; }
float sq(in vec2 p, in float s, in float r) { return length(max(abs(p) - s,0.)) - r; }

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);



    float aa = 0.001;

    vec2 st = uv;

    st -= st*length(st)*.5;
    float sz = 8.;
    // float t = mat_time - 17.;

    // ROTATE
    st *= rot(P*.125 + .1*mat_rot_time);

    vec2 uv_shift = mat_cam_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);



    vec2 p = st + vec2(mat_cam_time*.1,0.);

    p += uv_shift;

    vec4 pT = tri(p*sz);
    sz *= 1. + floor(rand(vec2(pT.zw))*15.);
    pT = tri(p*sz);

    float taille = cosa(mat_time,12.,pT.w*.25);
    float d = (length(pT.xy) - taille/3.)/sz;
    d = max(d,-sq(pT.xy*rot(P/8. + mat_time*2. + pT.w),.2125*taille,.02)/sz);
    float dd = (.3 - d*2.);
    d = smoothstep(-aa,aa,-d) + dd;

    vec3 color = SpectrumPoly(rand(vec2(pT.zw))*.1 + mix(.2,.625, smoothstep(.46,.52,cosa(mat_color_time + pT.w*.25,1.,0.)) ))*(dd*8.)*d;

    out_color = vec4(pow(color - length(st)*length(st)*1.69,vec3(1.2)),1.0);




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
