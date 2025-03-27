/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Fractal Petri Dish generator. From http:\/\/glslsandbox.com\/e#55304.0",

    "VSN": "1.1",

    "INPUTS": [



        {
            "LABEL": "Fractal Petri Dish/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal Petri Dish/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 100,
            "DEFAULT": 30
        },
        {
            "LABEL": "Fractal Petri Dish/Circle",
            "NAME": "mat_use_modifier",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Fractal Petri Dish/Circle Center",
              "NAME" : "mat_modifier",
              "TYPE" : "point2D",
              "MAX" : [
                1,
                1
              ],
              "MIN" : [
                0,
                0
              ],
              "DEFAULT" : [0.5,0.5]
        },
        {
            "LABEL": "Fractal Petri Dish/Shift",
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

float mat_time = mat_time_source - mat_offset * 100. * mat_offset_scale;

float  tri( float x ){
    return (abs(fract(x)-0.5)-0.25);
}
vec3 tri( vec3 p ){
    return vec3( tri(p.x), tri(p.y), tri(p.z) );
}


#define PI2 (PI*2.0)

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


    // float md = length((gl_FragCoord.xy / RENDERSIZE - mat_modifier) * (vec2(RENDERSIZE.x / RENDERSIZE.y, 1.0)));

    vec2 modifier = mat_modifier;
    modifier.y = 1. - modifier.y;

    float md = length(uv - modifier + vec2(0.5));

    md *= mat_scale;

    float t = mat_time;
    if (mat_use_modifier) {
        t += 7.0*sin(mat_time*0.03) * md;
    }

    // vec3 v = vec3( (gl_FragCoord.xy - RENDERSIZE/2.0) / min(RENDERSIZE.y,RENDERSIZE.x) * 1.5, 0.0);

    vec3 v = vec3( uv * 1.5, 0.0);

    v.z = (1.0 - md) * 0.33;

    float mx = sin(2.32 + t * 0.0037312) * 1.45 + 0.5;
    float my = cos(0.01 + t * 0.0023312) * 1.45 + 0.5;

    float mx2 = sin(3.6 - t * 0.0027312) * 1.25 + 0.5;
    float my2 = cos(2.3 - t * 0.0033312) * 1.25 + 0.5;

    float c1 = cos(my * PI2);
    float s1 = sin(my * PI2);
    float c2 = cos(mx * PI2);
    float s2 = sin(mx * PI2);
    float c3 = cos(mx2 * PI2);
    float s3 = sin(mx2 * PI2);
    vec3 vsum = vec3(0.0);
    for ( int i = 0; i < mat_iterations; i++ ){
        float f = float(i) / float(mat_iterations);

        v *= 1.0 + 0.1/(f*f+0.45);
        v += (my2-.5)*2.0;
        v = vec3( v.x*c1-v.y*s1, v.y*c1+v.x*s1, v.z ); // rotate
        v = vec3( v.x, v.y*c2-v.z*s2, v.z*c2+v.y*s2 ); // rotate
//      v = vec3( v.x*c3-v.z*s3, v.y, v.z*c3+v.x*s3 ); // rotate
        v = tri( v ); // fold
        vsum += v;
        vsum *= 0.9;

    }
    v = vsum *.3;
    out_color = vec4( sin(v * PI2 * 4.0)*.9+.9, 1.0 );






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
