/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "totetmatt, adapted by Jason Beyers",

    "DESCRIPTION": "Rainbow Lensing generator. From https://www.shadertoy.com/view/WsGBRd",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Rainbow Lensing/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rainbow Lensing/Shift",
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

float mat_time = mat_time_source - mat_offset * 10.;

mat2 r(float a){
    float c=cos(a),s=sin(a);
    return mat2(c,s,-s,c);
}
vec3 p(float i)
{
    return .5+.5*cos(2.*3.1415*(1.*i+vec3(0.,.33,.67)));
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    float bpm = sqrt(fract(-length(uv)+mat_time*130./60.*.5));
    //uv+=vec2(cos(mat_time),sin(mat_time))*.1;
    uv*=r(mat_time*.1)*+bpm;
    uv= abs(uv)-.1;
    float f = 1.01+(sin(mat_time)*.02+0.02);//sin(mat_time+texture1D(spectrum1,.1).r*100.)*2.+2.5;
        uv = vec2(uv.x/(1.-f),uv.y/(1.-f));
    uv = vec2( f*uv.x/ (uv.x*uv.x+uv.y*uv.y+1.),f*uv.y/ (uv.x*uv.x+uv.y*uv.y+1.));
    uv+=vec2(cos(mat_time),sin(mat_time))*.1;
    uv*=r(mat_time*.1);
    float d = max(abs(fract(uv.y*10.)-.5+.2)-.015,0.);
    d= smoothstep(0.25,0.19,d);
    float e = max(abs(fract(uv.x*10.)-.5+.2)-.015,0.);
    e=smoothstep(0.25,0.19,e);
    d=d+e;
    vec3 col = vec3(d);
    col = col*p(length(bpm));
    //col = texture2D(spectrum1,vec2(uv.x*.5+.75,0)).rrr*10.;
    out_color = vec4(col,1.0);




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
