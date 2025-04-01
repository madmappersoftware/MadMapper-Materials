/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "wnu, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/NdGXWz",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Sphere/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Sphere/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "Label": "Lighting/Power",
            "NAME": "mat_power",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/Position",
            "NAME": "mat_light_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [-1.0,-1.0]

        },

        {
            "LABEL": "Lighting/Animate",
            "NAME": "mat_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "Label": "Lighting/Direction",
            "NAME": "mat_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Lighting/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Lighting/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Lighting/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Lighting/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Color/Tint",
            "NAME": "mat_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Alpha",
            "NAME": "mat_use_alpha",
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
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8.;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


float random(vec2 p) { return fract(1e4 * sin(17.0 * p.x + p.y * 0.1) * (0.1 + abs(sin(p.y * 13.0 + p.x)))); }

mat4 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;

    return mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
}

vec3 rot(vec3 p, vec3 axis, float a){
    return (rotationMatrix(axis,a)*vec4(p,1.)).xyz;
}

vec3 mouseRot(vec3 p){

    //vec2 mouse = (mat_light_pos + vec2(1.)) / 2.;

    vec2 mouse = mat_light_pos;

    if (mat_animate) {
        float time_x = mat_time * cos(PI * mat_angle);
        float time_y = mat_time * sin(PI * mat_angle);
        mouse.x += time_x;
        mouse.y += time_y;

        // mouse.y += mat_time;
    }
    vec3 p1 = p;
    if (1.0 > 0.5) {
        p1 = rot(p1,vec3(0.,1.,0.),mouse.x*3.1415/2.);
        p1 = rot(p1,vec3(1.,0.,0.),-mouse.y*3.1415/2.);
    }
    return p1;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 4.;

    // uv += vec2(0.5);
    // uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    // uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    vec2 U = uv;

    float sinAcosY = sin(acos(uv.y));
    float zVal = sin(acos(uv.x/sinAcosY));
    zVal = sqrt(1.-dot(uv.xy,U)); //Better than first method
    vec3 p = vec3(uv.xy,zVal); //Sphere positions

    vec3 p3 = mouseRot(p); //rotate with mouse

    vec3 n = normalize(p3); //visualize with normal
    vec3 up = normalize(vec3(0.,1.,0.));
    float dotUp = dot(n,up);

    vec3 col = vec3(dotUp);
    col = pow(col,vec3(2.2 / mat_power));

    out_color = vec4(col,1.) * mat_color;

    if (mat_use_alpha) {
        out_color.a = mat_luma(out_color);
    }


    return out_color;
}
