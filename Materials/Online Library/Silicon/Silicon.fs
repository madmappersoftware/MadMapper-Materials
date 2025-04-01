/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#60310.2",

    "VSN": "1.1",

    "INPUTS": [

        {
            "LABEL": "Silicon/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Silicon/Layers",
            "NAME": "mat_layers",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 16.0,
            "DEFAULT": 8.0
        },
        {
            "LABEL": "Silicon/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Silicon/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Silicon/Shift",
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

float mat_time = mat_time_source - mat_offset * 8.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

/*
 +++ Tile-based rendering. Finding cases by adding up binary values in a 2x2 square.

    Could be simplified.*/


//const int nLayers = 8;

float rand(vec2 p){ return fract(sin(dot(p, vec2(12.989, 78.233)))*43758.5453); }

float plane(vec2 p, vec2 n){ return dot(p, normalize(n)); }

// the overall shape of the land
// vec2 o helps randomize noise w/o messing up the overall shape (should be in range 0.-1.)
int binLand(in vec2 p, vec2 o) {
    p = floor(-p);

    float noise = rand(p-o);
    float land = noise - p.y/(100.-100.*(1.-mat_fill) );

    return (land < .15 ? 1: 0);
}

// used for some tiles
float roof(vec2 p) {
    return p.y*2.;
}
float wall(vec2 p) {
    return p.x*2.-1.;
}
float ceiling(vec2 p) {
    return -p.y*2.+1.;
}
float ceilingDiag(vec2 p) {
    return dot(p, normalize(vec2(-1., -1.)))*2.+1.;
}

// 16 tile definitions, one for each case
float tileEmpty(vec2 p) {
    return 1.;
}
float tileFilled(vec2 p) {
    return -1.;
}
float tileCornerOut_ul(in vec2 p, float r) {
    p.x = 1.-p.x;
    return max(wall(p), roof(p));
}
float tileCornerOut_ur(in vec2 p, float r) {
    return max(wall(p), roof(p));
}
float tileCornerOut_dl(in vec2 p, float r) {
    vec2 p_ = vec2(1.-p.x, p.y);
    return max(max(wall(p_), ceiling(p)), ceilingDiag(p-.3));
}
float tileCornerOut_dr(in vec2 p, float r) {
    vec2 p_ = vec2(1.-p.x, p.y);
    return max(max(wall(p), ceiling(p)), ceilingDiag(p_-.3));
}
float tileCornerIn_ul(in vec2 p, float r) {
    p.x = 1.-p.x;
    return (min(wall(p), roof(p)));
}
float tileCornerIn_ur(in vec2 p, float r) {
    return (min(wall(p), roof(p)));
}
float tileCornerIn_dl(in vec2 p, float r) {
    p.x = 1. - p.x;
    return min(wall(p), ceiling(p));
}
float tileCornerIn_dr(in vec2 p, float r) {
    return (min(wall(p), ceiling(p)));
}
float tileWall_l(in vec2 p, float r) {
    p.x = 1.-p.x;
    return wall(p);
}
float tileWall_r(in vec2 p, float r) {
    return wall(p);
}
float tileWall_u(in vec2 p, in float r) {
    r *= .4;
    return min(roof(p), clamp(length(p-vec2(r+(1.-2.*r)*r, .5))/r, 1.-r*2.5, 1.));
}
float tileWall_d(in vec2 p, float r) {
    return ceiling(p);
}
float tileJoin_a(in vec2 p, float r) {
    return min(max(wall(vec2(1.-p.x, p.y)), roof(p)), tileCornerOut_dr(p, r));
}
float tileJoin_b(in vec2 p, float r) {
    return min(max(wall(p), roof(p)), tileCornerOut_dl(p, r));
}

// get landscape
float getLand(in vec2 p, vec2 o) {
    // get decimal value from 2x2 neighborhood of landFunc()
    int dec = 0;
    dec += 1 * binLand(vec2(p.x,    p.y), o);
    dec += 2 * binLand(vec2(p.x+1., p.y), o);
    dec += 4 * binLand(vec2(p.x,    p.y+1.), o);
    dec += 8 * binLand(vec2(p.x+1., p.y+1.), o);

    float r = rand(floor(p));

    // choose tile based on decimal value
    p = fract((p*p));
    float f;

    // there is probably a more performant method
    if(dec==0)
        f = tileEmpty(p);
    else if(dec==1)
        f = tileCornerOut_ur(p, 0.);
    else if(dec==2)
        f = tileCornerOut_ul(p, 0.);
    else if(dec==3)
        f = tileWall_u(p, r);
    else if(dec==4)
        f = tileCornerOut_dr(p, 0.);
    else if(dec==5)
        f = tileWall_r(p, 0.);
    else if(dec==6)
        f = tileJoin_a(p, 0.);
    else if(dec==7)
        f = tileCornerIn_ur(p, 0.);
    else if(dec==8)
        f = tileCornerOut_dl(p, 0.);
    else if(dec==9)
        f = tileJoin_b(p, 0.);
    else if(dec==10)
        f = tileWall_l(p, 0.);
    else if(dec==11)
        f = tileCornerIn_ul(p, 0.);
    else if(dec==12)
        f = tileWall_d(p, 0.);
    else if(dec==13)
        f = tileCornerIn_dr(p, 0.);
    else if(dec==14)
        f = tileCornerIn_dl(p, 0.);
    else if(dec==15)
        f = tileFilled(p);


    return clamp(1.-f, 0., 1.);

}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);


    uv *= mat_scale * 2.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= 8.;

    uv.y = 1. - uv.y;

    float scale;
    vec3 bg =
        mix(
            vec3(1.975, 1.43, 1.),
            vec3(1., .4, 1.),
            vec3(0.)
        );

    vec3 RGB = bg;
    vec2 uvb;

    vec2 camPos = vec2(0., 0.);
    vec2 camLook    = vec2(2.*mat_time, 0.);

    vec2 rayDir = (vec2(uv+camLook));

    vec2 v = camPos;
    float nLayers = mat_layers;
    for(float i=0.; i<nLayers; i++) {

        v += rayDir;

        float fTemp = getLand(v.xy, vec2(0., .1*float(i)));

        if(fTemp>0.9) {
            RGB = mix(vec3(fTemp+v.y/4.) * vec3(.9, .0, .1), bg, clamp((i+1.)/nLayers, 0.0, 1.));
            break;
        }

    }

    out_color = vec4(1.-RGB, 1.0);




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
