/*{
    "CREDIT": "aodnnawg, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3tGXzz",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Asteroid Field/Threshold",
            "NAME": "mat_thresh",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Asteroid Field/Depth",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Asteroid Field/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 200,
            "DEFAULT": 100
        },

        {
            "LABEL": "Asteroid Field/Camera",
          "NAME" : "mat_mouse",
          "TYPE" : "point2D",
          "MAX" : [
            1,
            1
          ],
          "MIN" : [
            0,
            0
          ],
          "DEFAULT" : [
            0.5,
            0.5
          ]
        },
        {
            "LABEL": "Asteroid Field/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Asteroid Field/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Asteroid Field/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 16. * mat_offset_scale;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


// --------[ Original ShaderToy begins here ]---------- //
float mod289(float x){return x - floor(x * (1.0 / 289.0)) * 289.0;}
vec4 mod289(vec4 x){return x - floor(x * (1.0 / 289.0)) * 289.0;}
vec4 perm(vec4 x){return mod289(((x * 34.0) + 1.0) * x);}

float noise(vec3 p){
    vec3 a = floor(p);
    vec3 d = p - a;
    d = d * d * (3.0 - 2.0 * d);

    vec4 b = a.xxyy + vec4(0.0, 1.0, 0.0, 1.0);
    vec4 k1 = perm(b.xyxy);
    vec4 k2 = perm(k1.xyxy + b.zzww);

    vec4 c = k2 + a.zzzz;
    vec4 k3 = perm(c);
    vec4 k4 = perm(c + 1.0);

    vec4 o1 = fract(k3 * (1.0 / 41.0));
    vec4 o2 = fract(k4 * (1.0 / 41.0));

    vec4 o3 = o2 * d.z + o1 * (1.0 - d.z);
    vec2 o4 = o3.yw * d.x + o3.xz * (1.0 - d.x);

    return o4.y * d.y + o4.x * (1.0 - d.y);
}

float fbm(vec3 x) {
    float v = 0.0;
    float a = 0.5;
    vec3 shift = vec3(100);
    for (int i = 0; i < 5; ++i) {
        v += a * noise(x);
        x = x * 2.0 + shift;
        a *= 0.5;
    }
    return v;
}

void makeRoRd(in vec2 uv, out vec3 ro, out vec3 rd) {
    ro = vec3(0,0,-5);

    vec2 cam = mat_mouse;
    cam.y = 1. - cam.y;
    vec2 mou = (cam-.5) * 10.;
    vec3 lookat = vec3(mou,0);
    vec3 f = normalize(lookat-ro);
    float z = 1.;
    vec3 c = ro+f*z;
    vec3 r = cross(vec3(0,1,0), f);
    vec3 u = cross(f, r);
    vec3 i = c + uv.x * r + uv.y * u;
    rd = normalize(i-ro);
}

mat2 rot (float a) {
    return mat2(
        cos(a), sin(a),
        -sin(a), cos(a)
    );
}

float getDist(in vec3 p) {
    p.xz *= rot(mat_time*.1);
    float wave = sin(mat_time)*.05;
    float d2 = fbm(p)-.15 + wave;
    float t = 10.;
    float r = .1;
    return length(max(vec2(d2,abs(p.y)-t),0.0))-r;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    vec3 col = vec3(1.);

    vec3 ro,rd;
    makeRoRd(uv, ro, rd);
    col = rd;

    float t=0., step=0.;
    vec3 hitPos;
    for(int i=0; i<=mat_iterations; i++) {
        vec3 p = ro+rd*t;
        float dS = getDist(p) * mat_dist;

        if(dS<0.01 * pow(mat_thresh, 2.)) {
            hitPos = p;
            col.r = 1.;
            break;
        }
        if(t>float(mat_iterations)) break;
        t += dS;
        step += 1./float(mat_iterations);
    }

    float m = pow(step, 2.0);

    col.rgb = vec3(m);

    out_color = vec4(col,1.0) * mat_tint;


    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;


    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;


    return out_color;
}
