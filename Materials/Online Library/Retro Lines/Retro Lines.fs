/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "xbe, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/4dfSDj",

    "VSN": "1.0",

    "INPUTS": [

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

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
            "LABEL": "Lines/Lines",
            "NAME": "mat_lines",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 128,
            "DEFAULT": 32
        },

        {
            "LABEL": "Lines/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Spread",
            "NAME": "mat_spread",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Offset",
            "NAME": "mat_line_offset",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Lines/Mod 1",
            "NAME": "mat_mod1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Mod 2",
            "NAME": "mat_mod2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Mod 3",
            "NAME": "mat_mod3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Mod 4",
            "NAME": "mat_mod4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Lines/Thickness",
            "NAME": "mat_thickness",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lines/Fade",
            "NAME": "mat_fade",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Lines/Occlusion",
            "NAME": "mat_occlusion",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Scale",
            "NAME": "mat_noise_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Amplitude 1",
            "NAME": "mat_amplitude1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Amplitude 2",
            "NAME": "mat_amplitude2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Noise/Noise 1",
            "NAME": "mat_noise1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Noise 2",
            "NAME": "mat_noise2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Noise 3",
            "NAME": "mat_noise3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Noise 4",
            "NAME": "mat_noise4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Seed",
            "NAME": "mat_seed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },




        {
            "LABEL": "Animation 1/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Animation 1/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 1/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 1/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 1/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 1/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 1/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Animation 2/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Animation 2/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Animation 2/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Animation 2/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Animation 2/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Animation 2/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation 2/Restart",
            "NAME": "mat_a2_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Scroll/Enable",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
            "Label": "Scroll/Offset Scale",
            "NAME": "mat_shift_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scroll/Strobe",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Restart",
            "NAME": "mat_shift_restart",
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
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
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
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Sensitivity",
            "NAME": "mat_back_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

    ],
    "GENERATORS": [
        {
            "NAME": "mat_a1_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a1_speed",
                "speed_curve":2,
                "reverse": "mat_a1_reverse",
                "strob" : "mat_a1_strob",
                "reset": "mat_a1_restart",
                "bpm_sync": "mat_a1_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_a2_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a2_speed",
                "speed_curve":2,
                "reverse": "mat_a2_reverse",
                "strob" : "mat_a2_strob",
                "reset": "mat_a2_restart",
                "bpm_sync": "mat_a2_bpm_sync",
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
                "reset": "mat_shift_restart",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = (mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale) * 2.;
float mat_a2_time = (mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 2.;
float mat_shift_time = (mat_shift_time_source - mat_shift_offset * mat_shift_offset_scale * 8.) * 0.5;

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
    uv *= mat_scale * 2.;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    if (mat_shift_animate) {
        uv_shift.x += mat_shift_time;
    }

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


/*
 * Original shader from: https://www.shadertoy.com/view/4dfSDj
 */

 // --------[ Original ShaderToy begins here ]---------- //
/////////////////////////////////////////////////////////////////////////////
// XBE
// Retro style terrain rendering
//

// Noise from IQ
vec2 hash( vec2 p )
{
    p = vec2( dot(p,vec2(127.1,311.7)),
             dot(p,vec2(269.5,183.3)) );
    return -1.0 + 2.0*fract(sin(p)*43758.5453123 * (mat_seed + 1.));
}

float noise( in vec2 p )
{
    const float K1 = 0.366025404;
    const float K2 = 0.211324865;

    vec2 i = floor( p + (p.x+p.y)*K1) * pow(mat_mod1,0.05);

    vec2 a = pow(mat_mod2,.05)*p - pow(mat_mod3,0.05)*i + pow(mat_mod4,0.5)*(i.x+i.y)*K2;

    vec2 o = (a.x>a.y) ? vec2(1.0,0.0) : vec2(0.0,1.0);

    vec2 b = a - o + K2;
    vec2 c = a - 1.0 + 2.0*K2;

    vec3 h = max(0.5-vec3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );



    vec3 n = h*h*h*h*vec3( dot(a,hash(i+0.0)), dot(b,hash(i+o)), dot(c,hash(i+1.0)));

    return dot( n, vec3(70.0) );
}

const mat2 m = mat2( 0.80,  0.60, -0.60,  0.80 );

float fbm4( in vec2 p )
{
    float f = 0.0;
    f += pow(mat_noise1,2.)*0.5000*noise( p ); p = m*p*2.02*mat_noise_scale;
    f += pow(mat_noise2,2.)*0.2500*noise( p ); p = m*p*2.03*mat_noise_scale;
    f += pow(mat_noise3,2.)*0.1250*noise( p ); p = m*p*2.01*mat_noise_scale;
    f += pow(mat_noise4,2.)*0.0625*noise( p );
    return f;
}

float fbm6( in vec2 p )
{
    float f = 0.0;
    f += 0.5000*noise( p ); p = m*p*2.02;
    f += 0.2500*noise( p ); p = m*p*2.03;
    f += 0.1250*noise( p ); p = m*p*2.01;
    f += 0.0625*noise( p ); p = m*p*2.04;
    f += 0.031250*noise( p ); p = m*p*2.01;
    f += 0.015625*noise( p );
    return f;
}

mat4 CreatePerspectiveMatrix(in float fov, in float aspect, in float near, in float far)
{
    mat4 m = mat4(0.0);
    float angle = (fov / 180.0) * PI;
    float f = 1. / tan( angle * 0.5 );
    m[0][0] = f / aspect;
    m[1][1] = f;
    m[2][2] = (far + near) / (near - far);
    m[2][3] = -1.;
    m[3][2] = (2. * far*near) / (near - far);
    return m;
}

mat4 CamControl( vec3 eye, float pitch)
{
    float cosPitch = cos(pitch) * pow(mat_amplitude2, 1.5);
    float sinPitch = sin(pitch) * pow(mat_amplitude2, 1.5);
    vec3 xaxis = vec3( 1, 0, 0. );
    vec3 yaxis = vec3( 0., cosPitch, sinPitch );
    vec3 zaxis = vec3( 0., -sinPitch, cosPitch );
    // Create a 4x4 view matrix from the right, up, forward and eye position vectors
    mat4 viewMatrix = mat4(
        vec4(       xaxis.x,            yaxis.x,            zaxis.x,      0 ),
        vec4(       xaxis.y,            yaxis.y,            zaxis.y,      0 ),
        vec4(       xaxis.z,            yaxis.z,            zaxis.z,      0 ),
        vec4( -dot( xaxis, eye ), -dot( yaxis, eye ), -dot( zaxis, eye ), 1 )
    );
    return viewMatrix;
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    // vec2 p = texCoord;

    uv = transformUV(uv);

    vec2 p = uv;

    vec3 eye = vec3(0., pow(mat_spread,2.)*0.25+0.25*cos(0.5*mat_a2_time), -1.);
    // mat4 projmat = CreatePerspectiveMatrix(50., iResolution.x/iResolution.y, 0.1, 10.);
    mat4 projmat = CreatePerspectiveMatrix(50., 1., 0.1, 10.);
    mat4 viewmat = CamControl(eye, -5.*PI/180.);
    mat4 vpmat = viewmat*projmat;

    vec3 col = vec3(0.);
    vec3 acc = vec3(0.);
    float d;

    vec4 pos = vec4(0.);
    // float lh = -iResolution.y;
    float lh = -1000.;
    float off = 0.1*mat_a1_time;
    float h = 0.;
    float z = 0.1 + mat_line_offset * 2.;
    float zi = 0.05 * pow(mat_limit,2.);
    for (int i=0; i<mat_lines; ++i)
    {
        pos = vec4(p.x, pow(mat_amplitude1, 1.5)*0.5*fbm4(0.5*vec2(eye.x+p.x, z+off)), eye.z+z, 1.);
        h = (vpmat*pos).y - p.y;
        if (h>lh)
        {
            d = abs(h) / pow(mat_thickness,2.);
            col = vec3( d<0.005?smoothstep(1.,0.,d*192.):0. );
            col *= exp(-0.1*float(i) * mat_fade);
            acc += col * pow(mat_brightness,2.);
            lh = h * mat_occlusion;
        }
        z += zi;
    }
    col = sqrt(clamp(acc, 0., 1.));
    // Background
    // vec3 bkg = vec3(0.32,0.36,0.4) + p.y*0.1;
    // col += bkg * mat_back_level;
    // Vignetting
    // vec2 r = -1.0 + 2.0*(uv);
    // float vb = max(abs(r.x), abs(r.y));
    // col *= (0.15 + 0.85*(1.0-exp(-(1.0-vb)*30.0)));
    out_color = vec4(col, 1.);



    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

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
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * mat_back_sensitivity + mat_back_thresh);
    }

    return out_color;
}
