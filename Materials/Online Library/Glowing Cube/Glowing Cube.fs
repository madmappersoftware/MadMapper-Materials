/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "sinvec, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/7tScRt",

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
            "LABEL": "Shape/Container Size",
            "NAME": "mat_container_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Cube Size",
            "NAME": "mat_cube_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Corner Size",
            "NAME": "mat_corner_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Corner Spread",
            "NAME": "mat_corner_spread",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Inner Size",
            "NAME": "mat_inner_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Shape/Stretch X",
            "NAME": "mat_stretch_x",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Stretch Y",
            "NAME": "mat_stretch_y",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Stretch Z",
            "NAME": "mat_stretch_z",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },




        {
            "LABEL": "Shape/Opacity",
            "NAME": "mat_opacity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Orbit/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Orbit/Speed",
            "NAME": "mat_orbit_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Orbit/BPM Sync",
            "NAME": "mat_orbit_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Orbit/Reverse",
            "NAME": "mat_orbit_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Orbit/Offset",
            "NAME": "mat_orbit_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Orbit/Offset Scale",
            "NAME": "mat_orbit_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Orbit/Strob",
            "NAME": "mat_orbit_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Orbit/Restart",
            "NAME": "mat_orbit_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Inner Shapes/Speed",
            "NAME": "mat_inside_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Inner Shapes/BPM Sync",
            "NAME": "mat_inside_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Inner Shapes/Reverse",
            "NAME": "mat_inside_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Inner Shapes/Offset",
            "NAME": "mat_inside_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Inner Shapes/Offset Scale",
            "NAME": "mat_inside_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Inner Shapes/Strob",
            "NAME": "mat_inside_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Inner Shapes/Restart",
            "NAME": "mat_inside_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Background/Background",
            "NAME": "mat_background",
            "TYPE": "bool",
            "DEFAULT": false,
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
            "LABEL": "Background/Mode",
            "NAME": "mat_back_mode",
            "TYPE": "long",
            "VALUES": ["Mix", "Cut"],
            "DEFAULT": "Mix"
        },
        {
            "LABEL": "Background/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
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
            "NAME": "mat_orbit_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_orbit_speed",
                "speed_curve":2,
                "reverse": "mat_orbit_reverse",
                "strob" : "mat_orbit_strob",
                "reset": "mat_orbit_restart",
                "bpm_sync": "mat_orbit_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_inside_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_inside_speed",
                "speed_curve":2,
                "reverse": "mat_inside_reverse",
                "strob" : "mat_inside_strob",
                "reset": "mat_inside_restart",
                "bpm_sync": "mat_inside_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_orbit_time = mat_orbit_time_source - mat_orbit_offset * 8. * mat_orbit_offset_scale;
float mat_inside_time = mat_inside_time_source - mat_inside_offset * 8. * mat_inside_offset_scale;


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

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 0.5;
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

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}



vec3 gl1 = vec3(0.);
vec3 gl2 = vec3(0.);
vec3 gl3 = vec3(0.);
vec3 gl4 = vec3(0.);

mat2 r2d(float a){
    float s = sin(a), c = cos(a);
    return mat2(c, s, -s, c);
}

float vma(vec3 v) {
    return max(v.x, max(v.y, v.z)) / (mat_opacity * 4. + 1.);
}

// from hg_sdf
float fBox(vec3 p, vec3 b) {
    vec3 d = abs(p) - b;
    return length(max(d, vec3(0))) + vma(min(d, vec3(0)));
}

vec2 de(vec3 p) {

    float d = 0.;

    p.xz *= r2d(mat_orbit_time * .5 - mat_orbit.x * 2.);
    p.zy *= r2d(mat_orbit_time * .5 - mat_orbit.y * 2.);

    vec3 q = p;
    vec3 z = p;

    vec2 a = vec2(9999.);
    vec2 b = vec2(9999.);

    float h = .5 * mat_container_size;
    vec3 k = vec3(.2) * mat_corner_size;

    a.x = max ( fBox(p, vec3(h * mat_stretch_x * mat_cube_size + .18, h * mat_stretch_y * mat_cube_size + .18, h * mat_stretch_z * mat_cube_size + .18)), -length(p) + .8);
    a.x = max ( a.x, -fBox(abs(p) - vec3( h * mat_stretch_x, h * mat_stretch_y, h * mat_stretch_z), k));

    gl1+=(0.0004/(0.03+a.x*a.x))*vec3(0,1,1);
    a.y = .5;

    b.x = fBox(abs(p) - vec3( h * mat_stretch_x * mat_corner_spread, h * mat_stretch_y * mat_corner_spread, h * mat_stretch_z * mat_corner_spread), k);
    b.y = .3;
    gl3+=(0.0004/(0.03+b.x*b.x))*vec3(0,1,0);
    a = (a.x < b.x) ? a : b;

    p = q;
    p.xz *= r2d(mat_inside_time * 2.);
    b.x = length(abs(p) - vec3(.5, 0., 0.)) - .2 * mat_inner_size;
    gl4+=(0.0004/(0.03+b.x*b.x))*vec3(0,0,1);
    b.y = .3;
    a = (a.x < b.x) ? a : b;

    p = q;
    p.xy *= r2d(mat_inside_time * 2.);
    b.x = length(abs(p) - vec3(0., .5, 0.)) - .2 * mat_inner_size;
    gl4+=(0.0004/(0.03+b.x*b.x))*vec3(0,0,1);
    b.y = .5;
    a = (a.x < b.x) ? a : b;

    return a;
}

const vec2 e = vec2(.000035, -.000035);
vec3 norm(vec3 po) {
        return normalize(e.yyx*de(po+e.yyx).x + e.yxy*de(po+e.yxy).x +
                         e.xyy*de(po+e.xyy).x + e.xxx*de(po+e.xxx).x);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    // float time = mat_time * 2.;

    vec3 ro = vec3(0., 0., 7.);
    vec3 ta = vec3(0.);

    vec3 vd = normalize(ta - ro);
    vec3 ri = normalize(cross(vd, vec3(.0, 1., 0.)));
    vec3 dw = normalize(cross(ri, vd));
    vec3 rd = normalize(ri * uv.x + dw * uv.y + 3. * vd);

    float t = 0.;
    vec2 h;
    vec3 po = vec3(0.);

    for (int i = 0; i < 64; i++) {
        po = ro + rd * t;
        h = de(po);
        if (h.x < .001) {
            if (h.y == .5) {
                vec3 n = norm(po);
                rd = reflect(rd, n);
                ro = po + n * .01;
                t = .0;
            } else
                if(h.y == .3)
                    h.x = abs(h.x) + .001;
        }
        t += h.x;
    }

    vec3 c = vec3(.1);
    c += gl1 * .7;
    c += gl2 * .9;
    c += gl3 * .5;
    c += gl4 * .9;

    float alpha = 1.;

    // if (mat_luma(c) < 0.2) {
    //     alpha = 0.;
    // }

    out_color = vec4(c, alpha);


    if (mat_background) {
        // General way to add transparency to any shader
        if (mat_back_mode == 1) {
            // Differentiate front & back colors using a hard cut w/ threshold
            if (mat_luma(out_color.rgb) < mat_back_thresh) {
                out_color = mat_back_color;
            }
        } else {
            // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
            out_color = mix(mat_back_color, out_color, mat_luma(out_color.rgb) + mat_back_thresh);
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
