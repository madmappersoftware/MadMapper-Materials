/*{
    "CREDIT": "chau_mu, adapted by Jason Beyers",

    "DESCRIPTION": "Blobiscope generator. From https://www.shadertoy.com/view/MsyyDy",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "Blobiscope/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Blobiscope/Shapes",
            "NAME": "mat_shapes",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 40.0,
            "DEFAULT": 20.0
        },
        {
            "LABEL": "Blobiscope/Sharpness",
            "NAME": "mat_sharpness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 2.5
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
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
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
            "LABEL": "Rotation/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotation/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotation/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },

        {
            "Label": "Rotation/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotation/Strob",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "NAME": "mat_rotate_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rotate_speed",
                "speed_curve":2,
                "reverse": "mat_rotate_reverse",
                "strob" : "mat_rotate_strob",
                "bpm_sync": "mat_rotate_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

#define SPEED 0.5

float mat_time = mat_time_source - mat_offset;
float mat_rotate_time = mat_rotate_time_source - mat_rotate_offset;

// Multiple the result of this function call to rotate the coordinates by the given angle.
#define mat_rotate(angle) mat2(cos(angle),-sin(angle), sin(angle),cos(angle));


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    float mask = 0.;
    for (float i=0.; i < 6.; i += 1.) {
        uv *= mat_rotate(mat_rotate_time / 10.)

        float dx = length(uv.x);
        float dy = length(uv.y);

        mask += sin((dx - (mat_time * float(SPEED))) * mat_shapes) * mat_sharpness;
        mask += sin((dy - (mat_time * float(SPEED))) * mat_shapes / 2.) * mat_sharpness;

    }

    // Time varying pixel color
    vec3 col = vec3(mask);

    // Output to screen
    out_color = vec4(col,1.0);









    return out_color;
}
