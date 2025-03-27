/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "Burst shader. From http:\/\/glslsandbox.com\/e#55101.3",

    "VSN": "1.1",

    "INPUTS": [

        {
            "LABEL": "Burst/Particles",
            "NAME": "mat_particles",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 128,
            "DEFAULT": 16
        },
        {
            "LABEL": "Burst/Size",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Burst/Seed",
            "NAME": "mat_seed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Burst/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Burst/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.,
            "MAX": 360.,
            "DEFAULT": 0.
        },
        {
            "LABEL": "Burst/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Animation/Interval",
            "NAME": "mat_spacing",
            "TYPE": "float",
            "MIN": 1.,
            "MAX": 8.,
            "DEFAULT": 1.
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

float mat_time = mat_time_source + mat_offset;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}


vec4 DrawExplosion(vec2 uv, vec4 explosionColour, float frame, float size, float seed)
{
    vec4 renderedColour = vec4(0.0);

    vec2 point = vec2(1.0, 0.0);

    mat2 rr = mat2(0.54030230586, -0.8414709848, 0.8414709848, 0.54030230586);

    float particleSize  = (size * 100.0);

    for (int particle = 1; particle <= mat_particles; particle += 1)
    {
        float particleDist = cos(float(particle) * sin(float(particle) * seed) * seed);

        vec2 particpos = (point * frame) * particleDist;

        float particleDistance = dot(particpos - uv, particpos - uv);

        if (particleDistance < particleSize)
        {
            float fade = (float(particle) / mat_particles) * frame;

            renderedColour = mix((explosionColour / fade), renderedColour, smoothstep(0.0, size * 2.0, particleDistance));
        }

        point = point * rr;
    }

    renderedColour *= smoothstep(0.0, 1.0, (1.0 - frame) / 1.0);

    return renderedColour;
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

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*(mat_rotate + 170.) / 360.);
    uv -= vec2(0.5);

    out_color = DrawExplosion(uv, vec4(0.5, 0.3, 0.8, 1.0), mod(mat_time, mat_spacing), 0.005 * mat_size, 1.0 * mat_seed);


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
