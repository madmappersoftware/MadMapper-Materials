/*{
    "CREDIT": "passion, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3l2XWV.",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Swirl Segments/Radius",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Swirl Segments/Thickness",
            "NAME": "mat_thick",
            "TYPE": "float",
            "MIN": 0.9,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Swirl Segments/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Swirl Segments/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Swirl Segments/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Swirl Segments/Shift XY",
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
            "LABEL": "Scroll/Shift",
            "NAME": "mat_scroll_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },
        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_animate_scroll",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
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
            "Label": "Scroll/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Slide/Animate",
            "NAME": "mat_animate_slide",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Slide/Path",
            "NAME": "mat_slide_diff",
            "TYPE": "int",
            "MIN": 0,
            "MAX": 2,
            "DEFAULT": 1
        },
        {
            "LABEL": "Slide/Speed",
            "NAME": "mat_slide_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Slide/BPM Sync",
            "NAME": "mat_slide_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Slide/Reverse",
            "NAME": "mat_slide_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Slide/Offset",
            "NAME": "mat_slide_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Slide/Strob",
            "NAME": "mat_slide_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color Fade/Mix",
            "NAME": "mat_color_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Color Fade/Animate",
            "NAME": "mat_animate_color",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Fade/Speed",
            "NAME": "mat_color_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color Fade/BPM Sync",
            "NAME": "mat_color_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Fade/Reverse",
            "NAME": "mat_color_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Color Fade/Offset",
            "NAME": "mat_color_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Color Fade/Strob",
            "NAME": "mat_color_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Background",
            "NAME": "mat_background",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
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
        {
            "NAME": "mat_slide_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_slide_speed",
                "speed_curve":2,
                "reverse": "mat_slide_reverse",
                "strob" : "mat_slide_strob",
                "bpm_sync": "mat_slide_bpm_sync",
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8.) * 0.25;
float mat_scroll_time = (mat_scroll_time_source - mat_scroll_offset * 8.) * 0.25;
float mat_slide_time = (mat_slide_time_source - mat_slide_offset * 8.) * 0.25;
float mat_color_time = (mat_color_time_source - mat_color_offset * 32.) * 0.25;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


// --------[ Original ShaderToy begins here ]---------- //
/* Timothy Michael Walsh */
mat2 r2(float a){
    float s = sin(a);
    float c = cos(a);
    return mat2(s, c, -c, s);
}

float random (vec2 st) {
    return fract(sin(dot(st.xy,
        vec2(12.9898,78.233)))*43758.5453123);
}

vec3 effect(vec2 uv, float zoomSpeed, float ringCount, float size, float rotationSpeed){
    float spacing = 1.3;
    vec2 id = floor(uv/spacing-.5); //*spacing;
    float rrr = random(id.yy);
    rrr *= rrr<(mat_slide_diff/2.) ? 1. : -1.;

    float slide_time = mat_slide_time;
    if (!mat_animate_slide) {
        slide_time = 0.;
    }
    uv.x+=rrr*slide_time*1.5;

    id = floor(uv/spacing-.5);
    uv = (fract(uv/spacing-.5)-.5)*spacing;

    float r = random(id);
    float rr = random(id);

    uv*=r2(rr*rotationSpeed*mat_time+(sin(mat_time*r)*3.*r));
    float s = length(uv)-size*mat_size; //*(sin(mat_time*zoomSpeed)*.5);

    float f = smoothstep(s-.005, s, .2935);
    f -= smoothstep(s,s + 0.005, .27 / mat_thick);
    // Time varying pixel color


    float color_time = mat_color_time;
    if (!mat_animate_color) {
        color_time = 0.;
    }

    vec3 col = 0.5 + 0.5*cos(r+rr+color_time+uv.xyx+vec3(r*f,3.-rr+r,r+1.)-color_time*2.);

    col = mix(vec3(1.), col, mat_color_mix);

    float a = atan(uv.x,uv.y);
    //f = f * step(a, sin(mat_time-a*18.0)+cos(3.*mat_time+a*18.0));
    f = f * smoothstep(f,  cos(a*(ringCount+(sin(mat_time*2.*r)*8.*rotationSpeed*r))*sin(r+mat_time)+cos(-mat_time*11.75*rotationSpeed*rr)) +1., .02);   //+cos(mat_time-s*10.)

    return col*f;
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

    float scroll_time = mat_scroll_time;

    if (!mat_animate_scroll) {
        scroll_time = 0.;
    }

    uv*=r2(sin(scroll_time/2.));
    uv *= 1.0 + dot(uv,uv)*.5 * mat_fov;
    uv.x+=sin(scroll_time/5.)*13.;
    uv.y+=sin(scroll_time/5.5)*11.;

    uv += mat_scroll_amount;

    vec3 col = effect(uv, 4., 11., .125, .75);
    col += effect(uv, 2.5, 8., .2, 1.);
    col += effect(uv, 1.5, 6., .25, 0.5);
    col += effect(uv, 0.5, 7., .34, 0.35);
    col += effect(uv, 0.25, 5., .081, 0.865);
    col += effect(uv, 0.25, 5., .03, 01.1865);



    //f = f * step(f,sin(a*12.));

    if (col == vec3(0.)) {
        out_color = mat_background;
    } else {
        out_color = vec4(col,1.0);
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
