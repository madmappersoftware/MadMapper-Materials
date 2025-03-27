/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Migrating Squares. From http:\/\/glslsandbox.com\/e#44337.0",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Migrating Squares/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Migrating Squares/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Animation/Angle",
            "NAME": "mat_angle",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
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

float nrand (vec2 co)
{
    float a = fract(cos(co.x * 8.3e-3 + co.y) * 4.7e5);
    float b = fract(sin(co.x * 0.3e-3 + co.y) * 1.0e5);
    return a * .5 + b * .5;
}

float genstars (float starsize, float density, float intensity, vec2 seed)
{
    float rnd = nrand(floor(seed * starsize));
    float stars = pow(rnd,density) * intensity;
    return stars;
}
vec3 Blue = vec3(0,0,1);

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

    float r1,g1,b1,r2,g2,b2,r3,g3,b3,r4,g4,b4;
    r1=abs(sin(mat_time*1.0));
    g1=abs(sin(mat_time*1.7));
    b1=abs(sin(mat_time*2.3));//r,g,bの値をsinで変化
    r2=abs(sin(mat_time*2.0));
    g2=abs(sin(mat_time*1.5));
    b2=abs(sin(mat_time*1.7));
    r3=abs(sin(mat_time*3.0));
    g3=abs(sin(mat_time*2.7));
    b3=abs(sin(mat_time*1.5));//r,g,bの値をsinで変化
    r4=abs(sin(mat_time*3.1));
    g4=abs(sin(mat_time*1.3));
    b4=abs(sin(mat_time*1.6));
    // vec2 offset = vec2(mat_time * 1.25,0);

    float offset_x = mat_time * 1.25 * cos(PI * mat_angle);
    float offset_y = mat_time * 1.25 * sin(PI * mat_angle);

    vec2 offset = vec2(offset_x, offset_y);

    vec2 p = uv;


    p *= 750.0;

    float intensity1 = genstars(0.025, 16.0, 2.5, p + offset * 40.);
    float intensity2 = genstars(0.025, 16.0, 1.5, p + offset * 20.);
    float intensity3 = genstars(0.025, 16.0, 1.0, p + offset * 10.);
    float intensity4 = genstars(0.025, 16.0, 3.0, p + offset * 60.);

    out_color = vec4(intensity1 *vec3(r1,g1,b1)+intensity2 *vec3(r2,g2,b2)+intensity3 *vec3(r3,g3,b3)+intensity4 *vec3(r4,g4,b4), 1);//


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
