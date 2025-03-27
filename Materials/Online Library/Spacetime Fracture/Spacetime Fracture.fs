/*{
    "CREDIT": "gunzes, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/fs3SD2",

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
            "LABEL": "Deform/Deform 1",
            "NAME": "mat_deform_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Deform/Deform 2",
            "NAME": "mat_deform_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Deform/Deform 3",
            "NAME": "mat_deform_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Deform/Size 1",
            "NAME": "mat_size_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Deform/Size 2",
            "NAME": "mat_size_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Deform/Size 3",
            "NAME": "mat_size_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Animation/Factor 1",
            "NAME": "mat_f1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/Factor 2",
            "NAME": "mat_f2",
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
            "LABEL": "Background/Custom Background",
            "NAME": "mat_background",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Background/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                0.0
            ]
        },
        {
            "LABEL": "Background/Back Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 0.5;
float mat_scroll_time = (mat_scroll_time_source - mat_scroll_offset * 8. * mat_scroll_offset_scale) * 0.5;

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



//random

float rand(vec2 coord){
    return fract(sin(dot(coord,vec2(12.9898,78.233)))*
        43758.5453123);
}


//noise1d

float noise1D(float coord){

    float cellId = floor(coord);
    float cellFraction = fract(coord);
    //return clamp(mix(rand(vec2(cellId)),rand(vec2(cellId+1.)),smoothstep(0.,1.,cellFraction)),0.,1.);
    //return mix(rand(vec2(cellId)),rand(vec2(cellId+1.)),cellFraction);
    return rand(vec2(cellId));
}

//noise2d

float noise2D(vec2 coord){
    vec2 id = floor(coord);
    vec2 fraction = fract(coord);
    //4 punkty kwadratu
    float a = rand(id);
    float b = rand(id + vec2(1.,0.));
    float c = rand(id + vec2(0.,1.));
    float d = rand(id + vec2(1.,1.));

    //interpolacja
    vec2 smoothCorners = smoothstep(0.,1.,fraction);
    //vec2 smoothCorners = fraction*fraction*(3.0-2.0*fraction)

    //mix kornerow
    return mix(a,b,smoothCorners.x) +
           (c - a) * smoothCorners.y * (1.- smoothCorners.x) +
           (d - b) * smoothCorners.x * smoothCorners.y;
}

//rysowanie kwadratow


float drawSquare (vec2 coord, float a, float blur){
    float band1 = smoothstep(a+blur,a-blur,coord.x);
    float band2 = smoothstep(-a-blur,-a+blur,coord.x);
    float band3 = smoothstep(a+blur,a-blur,coord.y);
    float band4 = smoothstep(-a-blur,-a+blur,coord.y);
    return band1*band2*band3*band4;
}

//rotate

mat2 rotate(float angle){

    return mat2(cos(angle), -sin(angle),
                sin(angle), cos(angle));

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

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;

    float mat_time_orig = mat_time;

    float noise1d1 = noise1D(uv.x*65. * mat_deform_1)*.1;
    float noise1d2 = noise1D(uv.y*15.);
    //noise1d1 *= noise1d2;
    //uv.x += ((noise1d1-.5)*2.)*.01;
    //uv.y += ((noise1d2-.5)*2.)*.01;

    // mat_time *= 10.;

    float timeRemapped = (((sin(mat_time))*.5)+.5)*1. * mat_f1;
    float timeSin = sin(mat_time) * mat_f1;
    float timeCos = cos(mat_time) * mat_f1;


   // uv *= rotate(timeRemapped*3.1);

    vec2 uv2 = abs(uv*.15*150.*(noise1d1*sin(mat_time*1.21))) * mat_deform_2;




    vec2 uv3 = (uv*.15*50. * mat_deform_3);


    //uv2 *= rotate(mat_time*.5);

    vec2 uvSquareRot1 = uv*1.7 * mat_size_1;
    vec2 uvSquareRot2 = uv*2.2 * mat_size_2;
    vec2 uvSquareRot3 = uv*2. * mat_size_3;

    //uv2 *= rotate(mat_time*-.5);
    //uv3 *= rotate(mat_time*.15);



    // mat_time = 0.;


    vec3 col = vec3(.0);
    vec3 blue = vec3(0.,.5,1.);
    vec3 green = vec3(0.,.7,0.);
    vec3 red = vec3(1.,0.,0.);
    //col += vec3(vec2(noise2D(uv2)),0.);
    float noise1 = noise2D(vec2(uv2.x+mat_time,uv2.y)) *
                  noise2D(vec2(-uv2.x+mat_time,uv2.y)) *
                  noise2D(vec2(-uv2.x,-uv2.y+mat_time)) *
                  noise2D(vec2(uv2.x,-uv2.y+mat_time))
                  ;
    float noise2 = noise2D(vec2(uv3.x+mat_time,uv3.y)) *
                  noise2D(vec2(-uv3.x+mat_time,uv3.y)) *
                  noise2D(vec2(-uv3.x,-uv3.y+mat_time)) *
                  noise2D(vec2(uv3.x,-uv3.y+mat_time))
                  ;
    //jazda
    uv2.x += noise1*1.5;



    float noise3x = noise2D(vec2(uv2.x*1.+mat_time*1.1+mat_time));
    noise3x += noise2D(vec2(uv2.x*2.));
    float noise3y = noise2D(vec2(uv3.y*1.+mat_scroll_time*5.));


    //float noise3 = noise2D(vec2(uv3.y*2.));

    float time = mat_time*-2.5*mat_f2;




    //uvSquareRot1.y += noise1*3.5;
    //uvSquareRot2.y += noise1*3.5;
    //uvSquareRot3.y += noise1*3.5;

    //col = vec3(length(uv * noise * 2.));
    col = 1.-col;

    uvSquareRot1.y += ((noise3x-.5)*.2)*10.* timeSin;
    uvSquareRot2.y += ((noise3x-.5)*.2)*10.* timeSin;
    uvSquareRot3.y += ((noise3x-.5)*.2)*10.* timeSin;

   // uvSquareRot1.x *= (timeSin*3.)+2.;

    uvSquareRot1.x += ((noise3y-.5)*.2)*5.* timeCos*3.;
    uvSquareRot2.x += ((noise3y-.5)*.2)*5.* timeCos*2.;
    uvSquareRot3.x += ((noise3y-.5)*.2)*5.* timeCos*1.;

    uvSquareRot1 *= rotate(1. * time*.1 *noise1d1*15.); ///////////////////////
    uvSquareRot2 *= rotate(-1. * time*.5);
    uvSquareRot3 *= rotate(1. * time*.25 );

    col *= (drawSquare(uvSquareRot1,.55+noise1,.2) - drawSquare(uvSquareRot1,.55*.99 + noise1,.01)) * blue*.7 +
           (drawSquare(uvSquareRot1,1.+noise1,.2) - drawSquare(uvSquareRot1,1.*.9 + noise1,.01)) * blue*.1 +
           (drawSquare(uvSquareRot2,.5+noise1,.1)- drawSquare(uvSquareRot2,.5*.9 + noise1,.01)) * blue *.5 +
           (drawSquare(uvSquareRot3,.35+noise1,.3) - drawSquare(uvSquareRot3,.35*.9 + noise1,.01)) * blue *1. +
           drawSquare(uvSquareRot3,.1+noise1,.05);


    col += vec3(pow(col.x,1.5));
    col += vec3(pow(col.y,.9));
    col += vec3(pow(col.z,3.5));

   /*     if (col.x > .1) {

        col.x -= noise3;
    }
    */

    //col = vec3(noise3);

    out_color = vec4(col,1.0);

    if (mat_background) {
        if (mat_luma(out_color) < (0.02 * mat_back_thresh)) {
            out_color = mat_back_color;
        }
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
