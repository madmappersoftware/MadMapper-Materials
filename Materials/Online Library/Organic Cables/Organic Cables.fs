/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Organic Cables generator. From http:\/\/glslsandbox.com\/e#57053.0",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Organic Cables/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Organic Cables/Fog",
            "NAME": "fog_mod",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Organic Cables/Ray",
            "NAME": "ray_mod",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Organic Cables/Wiggle",
            "NAME": "wiggle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Organic Cables/Trace",
            "NAME": "mat_trace",
            "TYPE": "float",
            "MIN": 1,
            "MAX": 256,
            "DEFAULT": 128
        },

        {
            "LABEL": "Organic Cables/Cam X",
            "NAME": "cam_x",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Organic Cables/Cam Y",
            "NAME": "cam_y",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Organic Cables/Cam Z",
            "NAME": "cam_z",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Organic Cables/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.,
            "MAX": 360.,
            "DEFAULT": 0.
        },
        {
            "LABEL": "Organic Cables/Shift",
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


mat2 genRot(float a){
    return mat2(cos(a),-sin(a),sin(a),cos(a));
}
vec2 pmod(vec2 p,float count){
    p *= genRot(PI/count);
    float at = atan(p.y/p.x);
    float r = length(p);
    at = mod(at,2. * PI / count);
    p = vec2(r * cos(at),r * sin(at));
    p *= genRot(-PI/count);
    return p;
}
float map(vec3 p){
    vec3 q = p;
    p.xy *= genRot(p.z/4.);
    p = (fract(p/1.5 + 0.5) - 0.5) * 1.5;
    p.xy = pmod(p.xy,8.);
    float string = length(p.xy - vec2(0.25 + 0.05 * sin(q.z + wiggle * mat_time * PI),0.)) - .01 + 0.005 * sin(q.z);
    float sphere = length(p - vec3(0.25 + 0.05 * sin(q.z + wiggle * mat_time * PI),0.,0.)) - .03;
    string = min(string,sphere);
    return string;
}
vec3 getNormal(vec3 p){
    vec3 x = dFdx(p);
    vec3 y = dFdy(p);
    return normalize(cross(x,y));
}
vec4 trace(vec3 o,vec3 r){
    float t = 0.;
    vec3 p;
    for(int i = 0; i < mat_trace; i++){
        p = o + r * t;
        float d = map(p);
        t += d * 0.75;
    }
    vec3 n = getNormal(p);
    return vec4(n,t);
}

vec3 cam(){
    // vec3 c = vec3(0.,0.,-1.5);
    // cool
    // vec3 c = vec3(0.,0.5,-1.5);

    vec3 c = vec3(cam_x,cam_y,-cam_z);


    c += vec3(.5 * sin(mat_time),.5 * cos(mat_time),mat_time * 2.);
    return c;
}
vec3 ray(vec2 uv,float z){
    vec3 r = normalize(vec3(uv,z)) * ray_mod;
    r.xy *= genRot(-mat_time);
    //r.xz *= genRot(mat_time/8.);
    //r.yz *= genRot(mat_time/8.);
    return r;
}

vec3 getColor(vec3 o, vec3 r, vec4 d){
    vec3 light = normalize(vec3(r.xy,0.));
    vec3 p = o + r * d.w;
    vec3 n = d.xyz;
    float a = 1. + dot(n,light);
    vec3 bc = (vec3(sin(p.x),sin(p.y),sin(p.z)) * 0.5 + 0.5) * a;
    bc = vec3(0.) * a;
    float t = d.w;
    float fog = 1./(1. + t * t * 0.025 * fog_mod);

    return (1. - bc) * (1. - fog);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*(mat_rotate + 170.) / 360.);
    uv -= vec2(0.5);

    vec3 c = cam();
    vec3 r = ray(uv,1.5);
    vec4 d = trace(c,r);
    vec3 color = getColor(c,r,d);

    out_color = vec4( color, 1.0 );


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
