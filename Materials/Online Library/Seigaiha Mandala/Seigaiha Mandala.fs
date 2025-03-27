/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "PixelPhil, adapted by Jason Beyers",

    "DESCRIPTION": "From https:\/\/www.shadertoy.com\/view\/WdtGWf",

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
            "LABEL": "Circles/Density",
            "NAME": "mat_density",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Size",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Variation",
            "NAME": "mat_variation",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Circles/Pattern Scale",
            "NAME": "mat_pattern_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Pattern Offset",
            "NAME": "mat_pattern_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Pattern Mod",
            "NAME": "mat_pattern_mod",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Pattern Gain",
            "NAME": "mat_pattern_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Blur",
            "NAME": "mat_blur",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Circles/Shadow",
            "NAME": "mat_shadow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circles/Detail",
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
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "Before Color Controls",
            "FLAGS": "generate_as_define"
        },

        {
            "LABEL": "Color/Circle Color",
            "NAME": "mat_color3",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Ring Color 1",
            "NAME": "mat_color1",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.5,
                0.6,
                1.0
            ]
        },
        {
            "LABEL": "Color/Ring Color 2",
            "NAME": "mat_color2",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.2,
                0.5,
                1.0
            ]
        },

        {
            "LABEL": "Color/Border Color",
            "NAME": "mat_border_color",
            "TYPE": "color",
            "DEFAULT": [
                0.7,
                0.2,
                0.2,
                1.0
            ]
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

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

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


//
// Seigaiha Mandala by Philippe Desgranges
// Email: Philippe.desgranges@gmail.com
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
//


#define S(a,b,c) smoothstep(a,b,c)


// blends a pre-multiplied src onto a dst color (without alpha)
vec3 premulMix(vec4 src, vec3 dst)
{
    return dst.rgb * (1.0 - src.a) + src.rgb;
}

// blends a pre-multiplied src onto a dst color (with alpha)
vec4 premulMix(vec4 src, vec4 dst)
{
    vec4 res;
    res.rgb = premulMix(src, dst.rgb);
    res.a = 1.0 - (1.0 - src.a) * (1.0 - dst.a);
    return res;
}

// compute the round scale pattern and its mask
// output rgb is premultiplied by alpha
vec4 roundPattern(vec2 uv)
{
    float dist = length(uv);

    // Resolution dependant Anti-Aliasing for a prettier thumbnail
    // Thanks Fabrice Neyret & dracusa for pointing this out.
    float aa = 8. / (1000. * mat_detail);

    // concentric circles are made by thresholding a triangle wave function
    float triangle = abs(fract(dist * 11.0 * mat_pattern_scale + 0.3 * pow(mat_pattern_offset,1.5)) - 0.5 * mat_pattern_mod);
    float circles = S(0.25 - aa * 10.0, 0.25 + aa * 10.0, triangle) * mat_pattern_gain;

    // a light gradient is applied to the rings
    float grad = dist * 2.0;
    // vec3 col = mix(vec3(0.0, 0.5, 0.6),  vec3(0.0, 0.2, 0.5), grad * grad);
    vec3 col = mix(mat_color1.rgb,  mat_color2.rgb, grad * grad);
    col = mix(col, mat_color3.rgb, circles);

    // border and center are red
    // vec3 borderColor = vec3(0.7, 0.2, 0.2);
    vec3 borderColor = mat_border_color.rgb;
    col = mix(col, borderColor, S(0.44 - aa, 0.44 + aa, dist));
    col = mix(col, borderColor, S(0.05 + aa, 0.05 - aa, dist));

    // computes the mask with a soft shadow
    float mask = S(0.5, 0.49, dist);
    float blur = 0.3;
    float shadow = S(0.5 + blur, 0.5 - blur, dist);


    return vec4(col * mask, clamp(mask + shadow * 0.55 * mat_shadow, 0.0, 1.0));
}




//computes the scales on a ring of a given radius with a given number of scales
vec4 ring(vec2 uv, float angle, float angleOffet, float centerDist, float numcircles, float circlesRad)
{
    // polar space is cut in quadrants (one per scale)
    float quadId = floor(angle * numcircles + angleOffet);

    // computes the angle of the center of the quadrant
    float quadAngle = (quadId + 0.5 - angleOffet) * (6.283 / numcircles);

    // computes the center point of the quadrant on the circle
    vec2 quadCenter = vec2(cos(quadAngle), sin(quadAngle)) * centerDist;

    // return to color of the scale in the quadrant
    vec2 circleUv = (uv + quadCenter) / circlesRad;
    return roundPattern(circleUv);
}

// computes a ring with two layers of overlapping patterns
vec4 dblRing(vec2 uv, float angle, float centerDist, float numcircles, float circlesRad, float t)
{
    // Odd and even scales dance up and down
    float s = sin(t * 3.0 + centerDist * 10.0 * mat_variation) * 0.05;
    float d1 = 1.05 + s;
    float d2 = 1.05 - s;

    // the whole thing spins with a sine perturbation
    float rot = t * centerDist * 0.4 + sin(t + centerDist * 5.0) * 0.2;

    // compute bith rings
    vec4 ring1 = ring(uv, angle, 0.0 + rot, centerDist * d1, numcircles, circlesRad);
    vec4 ring2 = ring(uv, angle, 0.5 + rot, centerDist * d2, numcircles, circlesRad);

    // blend the results
    vec4 col = premulMix(ring1, ring2);

    // add a bit of distance shading for extra depth
    col.rgb *= 1.0 - (centerDist * centerDist) * 0.4;

    return col;
}

// computes a double ring on a given radius with a number of scales to fill the circle evenly
vec4 autoRing(vec2 uv, float angle, float centerDist, float t)
{
    float nbCircles = 1.0 + floor(centerDist * 23.0 * mat_density);
    return dblRing(uv, angle, centerDist, nbCircles, 0.23 * mat_size, t) ;
}

// Computes the pixel color for the full image at a givent time
vec3 fullImage(vec2 uv, float angle, float centerDist, float t)
{
    vec3 col;

    // the screen is cut in concentric rings
    float space = 0.1;

    // determine in which ring the pixel is
    float ringRad = floor(centerDist / space) * space;

    // computes the scales in the previous, current and next ring
    vec4 ringCol1 = autoRing(uv, angle, ringRad - space, t);
    vec4 ringCol2 = autoRing(uv, angle, ringRad, t);
    vec4 ringCol3 = autoRing(uv, angle, ringRad + space, t);

    // blends everything together except in the center
    if (ringRad > 0.0)
    {
        col.rgb = ringCol3.rgb;
        col.rgb = premulMix(ringCol2, col.rgb);
        col.rgb = premulMix(ringCol1, col.rgb);
    }
    else
    {
        col.rgb = ringCol2.rgb;
    }

    return col;
}

// A noise function that I tried to make as gaussian-looking as possible
float noise21(vec2 uv)
{
    vec2 n = fract(uv* vec2(19.48, 139.9));
    n += sin(dot(uv, uv + 30.7)) * 47.0;
    return fract(n.x * n.y);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    // Computes polar cordinates
    float angle = atan(uv.y, uv.x) / 6.283 + 0.5;
    float centerDist = length(uv);

    vec3 col = vec3(0.0);

    // average 4 samples at slightly different times for motion blur
    float noise = noise21(uv + mat_time);
    for (float i = 0.0; i < 4.0; i++)
    {
        col += fullImage(uv, angle, centerDist, mat_time - ((i + noise) * 0.03 * mat_blur));
    }
    col /= 4.0;

    // Output to screen
    out_color = vec4(col,1.0);


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity) - mat_luma_threshold * 1.;

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

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity) - mat_luma_threshold * 1.;
    }

    return out_color;
}
