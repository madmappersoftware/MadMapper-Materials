/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shirooo, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/sly3Dc",

    "VSN": "1.0",

    "IMPORTED": {
        "mat_iChannel0": {
            "NAME": "mat_iChannel0",
            "PATH": "1f7dca9c22f324751f2a5a59c9b181dfe3b5564a04b724c657732d0bf09c99db.jpg"
        }
    },

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
            "LABEL": "Circles/Spread",
            "NAME": "mat_spread",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Circles/Noise",
            "NAME": "mat_noise",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Circles/Chaos",
            "NAME": "mat_chaos",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Circles/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 200,
            "DEFAULT": 90
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
            "LABEL": "Texture/Custom Texture",
            "NAME": "mat_use_tex",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "Texture/Texture",
            "NAME": "mat_tex",
            "TYPE": "image"
        },
        {
            "LABEL": "Texture/Aspect",
            "NAME": "mat_tex_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture/Scale",
            "NAME": "mat_tex_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture/Scale to UV",
            "NAME": "mat_tex_scale_to_uv",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
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


#define TWO_PI 2*PI

//3d continuous noise
vec3 hash( vec3 p )
{
    p *= mat3( 127.1,311.7,-53.7,
               269.5,183.3, 77.1,
              -301.7, 27.3,215.3 );

    return 2.*fract(sin(p)*43758.5453123) -1.;
}

float noise( vec3 p )
{
    vec3 i = floor( p ),
         f = fract( p ),
         u = f*f*(3.-2.*f);

    return mat_noise * 2.*mix(
              mix( mix( dot( hash( i + vec3(0,0,0) ), f - vec3(0,0,0) ),
                        dot( hash( i + vec3(1,0,0) ), f - vec3(1,0,0) ), u.x),
                   mix( dot( hash( i + vec3(0,1,0) ), f - vec3(0,1,0) ),
                        dot( hash( i + vec3(1,1,0) ), f - vec3(1,1,0) ), u.x), u.y),
              mix( mix( dot( hash( i + vec3(0,0,1) ), f - vec3(0,0,1) ),
                        dot( hash( i + vec3(1,0,1) ), f - vec3(1,0,1) ), u.x),
                   mix( dot( hash( i + vec3(0,1,1) ), f - vec3(0,1,1) ),
                        dot( hash( i + vec3(1,1,1) ), f - vec3(1,1,1) ), u.x), u.y), u.z);
}

//1D gradient noise
float hash( uint n )
{   // integer hash copied from Hugo Elias
    n = (n<<13U)^n;
    n = n*(n*n*15731U+789221U)+1376312589U;
    return float(n&uvec3(0x0fffffffU))/float(0x0fffffff);
}

float gnoise( in float p )
{
    uint  i = uint(floor(p));
    float f = fract(p);
    float u = f*f*(3.0-2.0*f);

    float g0 = hash(i+0u)*2.0-1.0;
    float g1 = hash(i+1u)*2.0-1.0;
    return 2.4*mix( g0*(f-0.0), g1*(f-1.0), u) * mat_chaos;
}


float circle(vec2 c, vec2 p, float radius, float t)
{
    float d = length(c-p) - radius;
    return 1. - smoothstep(0., 1., abs(d) / t);
}

//input noise in [-1,1] range
vec2 distortByAngle(float noise, float strength, vec2 p)
{
    noise = noise * 0.5 + 0.5;
    float noiseAngle = noise * TWO_PI;
    p.x = p.x + strength * cos(noiseAngle*1.86);
    p.y = p.y + strength * sin(noiseAngle*1.3);
    return p * mat_spread;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    float noise = noise(vec3(uv*1.5, mat_time * 1.5));
    vec2 distUV = distortByAngle(noise, 0.015, uv);

    float circlesInt=0.;

    for(int i=0; i<mat_iterations; ++i)
    {
        float r = mix(0.05, 2.5, float(i)/float(mat_iterations));

        vec2 c = distortByAngle(gnoise(float(i)/float(mat_iterations)-mat_time*0.1), 0.3, vec2(0.,0.));
        //vec2 c = vec2(0.);
        circlesInt += circle(c, distUV, r, 0.03);
    }

    vec3 col;
    vec2 coord;

    if (mat_tex_scale_to_uv) {
        coord = mod(uv,1.0);
    } else {
        coord = mod(texCoord,1.0);
    }

    coord -= vec2(0.5);
    coord.x *= mat_tex_aspect;
    coord *= mat_tex_scale;
    coord += vec2(0.5);

    if (mat_use_tex) {

        col = mix(vec3(0.), IMG_NORM_PIXEL(mat_tex,coord).xyz, circlesInt);
    } else {
        col = mix(vec3(0.), IMG_NORM_PIXEL(mat_iChannel0,coord).xyz, circlesInt);
    }

    out_color = vec4(col,1.);

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
