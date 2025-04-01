/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "ufffd, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/ss2fR3",

    "VSN": "1.1",

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
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },



        {
            "LABEL": "Shape/Shift",
            "NAME": "mat_shape_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Shape/Scale X",
            "NAME": "mat_scale_x",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Shape/Scale Y",
            "NAME": "mat_scale_y",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Column Offset",
            "NAME": "mat_column_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Shape/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Shape/Cycle 3",
            "NAME": "mat_cycle3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Cycle 4",
            "NAME": "mat_cycle4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Shape/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },




        {
            "LABEL": "Shape/Iterations 1",
            "NAME": "mat_iterations_1",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 100,
            "DEFAULT": 58
        },
        {
            "LABEL": "Shape/Iterations 2",
            "NAME": "mat_iterations_2",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 30,
            "DEFAULT": 14
        },

        {
            "LABEL": "Shape/Detail",
            "NAME": "mat_detail",
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Color/Front Color",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },

        {
            "LABEL": "Color/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Cut"
        },
        {
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
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
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

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

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}



float smoothedge(in float val, in float edge) {
    float pxl = 1./(mat_detail * 1000.);
    return smoothstep(edge-pxl*1.,edge,val);
}

float nsin(in float val) {
    return sin(val)*0.5+0.5;
}

float drawline(in float val, in float edge, in float width) {
    return smoothedge(val,edge+width) - smoothedge(val,edge-width);

}

float bluredge(in float val, in float edge, in float blur) {
    float pxl = 1./(mat_detail * 1000.);
    return smoothstep(edge-pxl*blur,edge,val);
}

vec2 rotate(vec2 v, float a) {
    float s = sin(a);
    float c = cos(a);
    mat2 m = mat2(c, -s, s, c);
    return m * v;
}

float rand(vec2 n) {
    return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453);
}

float mat_noise(vec2 p){
    vec2 ip = floor(p);
    vec2 u = fract(p);
    u = u*u*(3.0-2.0*u);

    float res = mix(
        mix(rand(ip),rand(ip+vec2(1.0,0.0)),u.x),
        mix(rand(ip+vec2(0.0,1.0)),rand(ip+vec2(1.0,1.0)),u.x),u.y);
    return res*res;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 uvc = uv + vec2(0.5);
    vec2 warp = uvc;
    float mono = 1. * mat_cycle1;
    float flower = 0.;

    vec2 shape_offset = mat_shape_offset;
    shape_offset += vec2(0.5);
    shape_offset.x = 1.-shape_offset.x;
    shape_offset -= vec2(0.5);

    for (int i=0;i<mat_iterations_1;i++){
        for (int j=0;j<mat_iterations_2;j++) {
            // hex grid
            float fi = float(i);

            float row = floor(fi/6.);
            // float row = fi;

            // vec2 cp = vec2(
            //     0.5 * (mod(fi,6.) + mod(row,2.) / 2.),
            //     row / 12. * sqrt(3.)
            // );

            float cp_x = 0.5 * (mod(fi,6.) + mat_column_offset*mod(row,2.) / 2.);
            float cp_y = row / 12. * sqrt(3.);

            vec2 cp = vec2(cp_x, cp_y);

            // magic numbers
            cp.y += 0.2225;
            cp.x -= 0.2;

            cp -= vec2(0.5);
            cp.x *= mat_scale_x;
            cp.y *= mat_scale_y;
            cp += vec2(0.5);

            cp += shape_offset;

            float cs = float(j) / 22.5;
            cs /= 4. * mat_cycle2; // toggle
            cs = mat_cycle3*1./6. - cs;
            // draw circle

            // float circle = distance(uvc+cp-0.8, vec2(0.5));
            float circle = distance(uvc + cp - 0.8, vec2(0.5));

            // mono += circle * mat_noise(cp * 200. + mat_time * .01);
            // mono += mat_noise(cp * 200. + mat_time * .001);

            mono += bluredge(circle,cs,mat_detail * 1000. * .1 * sin(
                distance(uvc,vec2(0.5)) + mat_time * 0.5
            ));

            if (j==0) {
                flower += drawline(circle,cs,0.001);
            }
        }
    }


    flower = mod(flower,3.0) * nsin(3.14 + mat_time*0.5);

    mono += flower * .1;
    mono = mod(mono,2. * mat_cycle4);

    float bw = smoothedge(mono,0.5 / mat_fill);

    // bw = mat_noise(uvc * 20. + mat_time * .01);
    // bw = flower;

    vec3 col = vec3(bw);
    out_color = vec4(col,1.0);

    // General way to add transparency to any shader
    if (mat_back_mode == 1) {
        // Differentiate front & back colors using a hard cut w/ threshold
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = mat_back_color;
        } else {
            out_color = mat_front_color;
        }
    } else {
        // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) + mat_back_thresh);
    }

    return out_color;
}
