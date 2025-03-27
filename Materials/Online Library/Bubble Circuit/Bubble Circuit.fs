/*{
    "CREDIT": "resontone, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3dtcD2",

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
            "LABEL": "Bubbles/Size",
            "NAME": "mat_bubble_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Fill 1",
            "NAME": "mat_fill1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Fill 2",
            "NAME": "mat_fill2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Gain",
            "NAME": "mat_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.5,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubbles/Seed",
            "NAME": "mat_seed",
            "TYPE": "float",
            "MIN": 0.01,
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
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
            "DEFAULT": 90.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "DEFAULT": "Mix"
        },
        {
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
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
        {
            "NAME": "mat_shift_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shift_speed",
                "speed_curve":2,
                "reverse": "mat_shift_reverse",
                "strob" : "mat_shift_strob",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

float mat_shift_time = (mat_shift_time_source - mat_shift_offset * 8.) * 0.05;

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
    uv *= mat_scale * 0.5;

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


const float TWO_PI = 6.2831853071795;

mat4 rotationMatrix(vec3 axis, float angle) {
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;

    return mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
}

vec3 rotate(vec3 v, vec3 axis, float angle) {
    mat4 m = rotationMatrix(axis, angle);
    return (m * vec4(v, 1.0)).xyz;
}



float snoise(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float snoise(float co){
    return fract(sin(co) * 43758.5453);
}


//x,y = local coordinates
//z = square id
//w = zoom
vec4 irregularGridXYID(vec2 uv, float squareSize, int maxZoomLevel, float splitLikelihood)
{
    float zoom = 1./squareSize;
    vec2 uv_irregular_grid;
    vec2 uv_irregular_grid_id;

    bool endFlag = false;
    int zoomIndex = 0;
    float zoomFactor = 1.;
    for(zoomIndex = 0; zoomIndex < maxZoomLevel && !endFlag; zoomIndex++)
    {
        uv_irregular_grid = fract(uv*zoom*zoomFactor) - 0.5;
        uv_irregular_grid_id = floor(uv*zoom*zoomFactor);

        uv_irregular_grid /= mat_bubble_size;
        uv_irregular_grid_id *= mat_seed;

        endFlag = snoise(uv_irregular_grid_id) > (splitLikelihood * mat_cycle2);

        zoomFactor *= 2.;

    }
    return vec4(uv_irregular_grid, length(uv_irregular_grid_id)/zoom, zoomFactor);
}

float circle(vec2 uv, float diam, float blur)
{
    return (1. - smoothstep(diam-blur,diam+blur, length(uv) ) * mat_fill2) * mat_gain;
}

float square(vec2 uv, float diam, float blur)
{
    float val = 1. - smoothstep(diam-blur,diam+blur, max(abs(uv.x), abs(uv.y)) );
    //val *= 1. - smoothstep(diam-blur,diam+blur, abs(uv.y) );


    return(val);
}

vec3 bubbleBeadCircuit(vec2 uv, float loopFrequency)
{
    float chromSplit = 0.;//1.5/100.;
    float zoom = 0.0625;


    //uv.x += mat_time;

    vec4 grid_R = irregularGridXYID(uv, zoom, 4, 0.75);
    float grid_R_id = grid_R.z;

    vec4 grid_G = irregularGridXYID(uv + chromSplit, zoom, 4, 0.75);
    float grid_G_id = grid_G.z;

    vec4 grid_B = irregularGridXYID(uv - chromSplit, zoom, 4, 0.75);
    float grid_B_id = grid_B.z;


    float squareTimeFreqR = (floor(snoise(grid_R.z))+1.)*loopFrequency;
    //frequency must be a whole number multiple of loopFrequency


    float squareTimePhaseR = snoise(grid_R.z+10.)*TWO_PI; //between 0 and 2 hz
    float squareSizeR =  0.+abs(0.5*sin(TWO_PI*mat_time*squareTimeFreqR + squareTimePhaseR));


    float squareTimeFreqG = (floor(snoise(grid_G.z))+1.)*loopFrequency;


    float squareTimePhaseG = snoise(grid_G.z+10.)*TWO_PI; //between 0 and 2 hz
    float squareSizeG =  0.+abs(0.125*tan(TWO_PI*mat_time*squareTimeFreqG + squareTimePhaseG));

    float squareTimeFreqB = (floor(snoise(grid_B.z))+1.)*loopFrequency;


    float squareTimePhaseB = snoise(grid_B.z+10.)*TWO_PI; //between 0 and 2 hz
    float squareSizeB =  0.5+(0.5*cos(TWO_PI*mat_time*squareTimeFreqB + squareTimePhaseB)) * mat_fill1;

    // Time varying pixel color
    vec3 col = vec3(circle(grid_R.xy, 0.4 *squareSizeR, 0.0125 * grid_R.w),
                   circle(grid_G.xy, 0.4 *squareSizeG, 0.0125 * grid_G.w),
                   circle(grid_B.xy, 0.4 *squareSizeB, 0.0125 * grid_B.w));

    float setting = 1.15 * mat_cycle1;//also try 2.45
    col = rotate(col, vec3(4.,0.,8.), 0.25*(setting)*TWO_PI);
    col = mix(col, vec3(col.g), 0.5);
    col.rb = mix(col.rb, col.gg, 1.);
    col *= 2.;

    return(clamp(col, 0., 1.));
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    if (mat_shift_animate) {
        float shift_time_x = mat_shift_time * cos(2.*PI * mat_shift_angle/360.0);
        float shift_time_y = mat_shift_time * sin(2.*PI * mat_shift_angle/360.0);
        uv.x -= shift_time_x;
        uv.y -= shift_time_y;
    }

    vec3 col = bubbleBeadCircuit(uv, 1./3.);
    // col += vec3(0.25, 0., 0.2);

    // Output to screen
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
        // out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) + mat_back_thresh);
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * (mat_back_thresh + 1.));
    }

    return out_color;
}
