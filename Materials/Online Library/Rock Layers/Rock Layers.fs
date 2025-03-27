/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Plento, adapted by Jason Beyers",

    "DESCRIPTION": "*Looks better in textured mode*. From https://www.shadertoy.com/view/wtyyWD by Plento.  Experiment with triplanar mapping and raymarching.",

    "VSN": "1.0",

    "IMPORTED": {
        "mat_default_tex": {
            "NAME": "mat_default_tex",
            "PATH": "ad56fba948dfba9ae698198c109e71f118a54d209c0ea50d77ea546abad89c57.png"
        },
        "mat_cube_map": {
            "NAME": "mat_cube_map",
            "PATH": [
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785.png",
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785_1.png",
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785_2.png",
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785_3.png",
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785_4.png",
                "0681c014f6c88c356cf9c0394ffe015acc94ec1474924855f45d22c3e70b5785_5.png"
            ],
            "TYPE": "cube"
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
            "LABEL": "Morph/X Factor 1",
            "NAME": "mat_x_factor1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Y Factor 1",
            "NAME": "mat_y_factor1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Z Factor 1",
            "NAME": "mat_z_factor1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Morph/X Factor 2",
            "NAME": "mat_x_factor2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Y Factor 2",
            "NAME": "mat_y_factor2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Z Factor 2",
            "NAME": "mat_z_factor2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },






        {
            "LABEL": "Morph/Morph 1",
            "NAME": "mat_morph1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Morph/Morph 2",
            "NAME": "mat_morph2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Morph 3",
            "NAME": "mat_morph3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Morph/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Morph/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 64,
            "DEFAULT": 64
        },



        {
            "LABEL": "Expand/Animate",
            "NAME": "mat_animate_expand",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "LABEL": "Expand/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Expand/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Expand/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Expand/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Expand/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Expand/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Spin/Spin",
            "NAME": "mat_spin",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "Label": "Spin/Axis",
            "NAME": "mat_spin_axis",
            "TYPE": "float",
            "MIN": -1.,
            "MAX": 1.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Spin/Animate",
            "NAME": "mat_animate_spin",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spin/Speed",
            "NAME": "mat_spin_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Spin/BPM Sync",
            "NAME": "mat_spin_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spin/Reverse",
            "NAME": "mat_spin_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Spin/Offset",
            "NAME": "mat_spin_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Spin/Offset Scale",
            "NAME": "mat_spin_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Spin/Strob",
            "NAME": "mat_spin_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "LABEL": "Texture/Aspect",
            "NAME": "mat_tex_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Texture/Use Custom Texture",
            "NAME": "mat_use_custom_tex",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture/Texture",
            "NAME": "mat_tex",
            "TYPE": "image",
        },

        {
            "LABEL": "Surface/Shading",
            "NAME": "mat_shading",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Surface/Smooth",
            "NAME": "mat_smooth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting/Power",
            "NAME": "mat_power",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting/Dist",
            "NAME": "mat_lighting",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Lighting/Pos",
            "NAME": "mat_lighting_shift",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Lighting/Shine",
            "NAME": "mat_shine",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/Stroke",
            "NAME": "mat_shine_stroke",
            "TYPE": "float",
            "MIN": -8.0,
            "MAX": 8.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Background/Background",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.01,
                0.01,
                0.01,
                1.0
            ]
        },
        {
            "LABEL": "Background/Gradient",
            "NAME": "mat_gradient",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "Label": "Background/Rotate",
            "NAME": "mat_back_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Gain",
            "NAME": "mat_gain",
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
            "NAME": "mat_spin_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_spin_speed",
                "speed_curve":2,
                "reverse": "mat_spin_reverse",
                "strob" : "mat_spin_strob",
                "bpm_sync": "mat_spin_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 1.;

float mat_spin_time = (mat_spin_time_source - mat_spin_offset * 8. * mat_spin_offset_scale) * 0.5;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// Cole Peterson

// #define R RENDERSIZE.xy
// #define m vec2(R.x/R.y*(iMouse.x/R.x-.5),iMouse.y/R.y-.5)
#define mat_ss(a, b, t) smoothstep(a, b, t)
#define mat_rot(a) mat2(cos(a), -sin(a), sin(a), cos(a))
// #define mat_tex(chan, p) texture(chan, p)

// Get color from triplanar map. Not sure if this is 100% correct?
vec3 trimap(vec3 p, vec3 n){

    vec3 yz;
    vec3 xz;
    vec3 xy;

    vec2 coord1, coord2, coord3;

    coord1 = p.yz;
    coord2 = p.xz;
    coord3 = p.xy;

    coord1 -= vec2(0.5);
    coord2 -= vec2(0.5);
    coord3 -= vec2(0.5);

    coord1.x *= mat_tex_aspect;
    coord1 *= mat_tex_scale;

    coord2.x *= mat_tex_aspect;
    coord2 *= mat_tex_scale;

    coord3.x *= mat_tex_aspect;
    coord3 *= mat_tex_scale;

    coord1 += vec2(0.5);
    coord2 += vec2(0.5);
    coord3 += vec2(0.5);

    if (mat_use_custom_tex) {

        yz = IMG_NORM_PIXEL(mat_tex, coord1).xyz;
        xz = IMG_NORM_PIXEL(mat_tex, coord2).xyz;
        xy = IMG_NORM_PIXEL(mat_tex, coord3).xyz;

    } else {

        yz = IMG_NORM_PIXEL(mat_default_tex, coord1).xyz;
        xz = IMG_NORM_PIXEL(mat_default_tex, coord1).xyz;
        xy = IMG_NORM_PIXEL(mat_default_tex, coord1).xyz;
    }

    n /= (n.x + n.y + n.z);
    return yz*n.x + xz*n.y + xy*n.z;

    // return vec3(1.);
}


void trn(inout vec3 p){
    p.xz *= mat_rot((mat_spin + 0.5) * PI);
    p.z += mat_spin_axis * PI;
}

// value for morphing/ color
float mat_val(vec3 p) {
    // return .5 + .5*cos(length(p)*15. - (mat_time*.75 + 2. )) - mat_limit;

    float expand_time = mat_time;
    if (!mat_animate_expand) {
        expand_time = 0.;
    }

    // p.x = 0.;

    return .5 + .5*cos(length(p)*15. - (expand_time*.75 + 2.)) - mat_limit;

}


float map(vec3 p){
    float d;

    vec3 n = normalize(p);

    trn(p);
    trn(n);

    float spin_time = mat_spin_time;
    if (!mat_animate_spin) {
        spin_time = 0.;
    }

    vec3 map_in = (p+spin_time*.2)*.26;

    // vec2 map_xy = vec2(map_in.x, map_in.y);


    // map_xy += vec2(0.5);
    // map_xy = matRot2D(map_xy, 2*PI*(mat_spin_axis) / 360);
    // map_xy -= vec2(0.5);

    // map_in = vec3(map_in.x, map_xy.y, map_xy.y);





    // float h = trimap((p+mat_spin_time*.2)*.26, abs(n)).x;


    // vec3 mapin = vec3(p.x+mat_spin_time*.2, p.y)*.26;

    // vec3 mapin = p * 0.26;

    // mapin.z += mat_spin_time*0.2;

    p.x *= pow(mat_x_factor1, 0.75);
    p.y *= pow(mat_y_factor1, 0.75);
    p.z *= pow(mat_z_factor1, 0.75);

    float h = trimap(map_in, abs(n)).x;
    float r = 1. + h *.7 * mat_morph1;


    float dis = length(p)-r;
    float reg = length(p)-1.;

    p.x *= mat_x_factor2;
    p.y *= mat_y_factor2;
    p.z *= mat_z_factor2;

    d = mix(dis, reg, mat_val(p)) * mat_morph2;

    return d;
}


vec3 normal( in vec3 pos ){
    vec2 e = vec2(0.002, -0.002) * pow(mat_smooth, 2.);
    return normalize(
        e.xyy * map(pos + e.xyy) +
        e.yyx * map(pos + e.yyx) +
        e.yxy * map(pos + e.yxy) +
        e.xxx * map(pos + e.xxx));
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    // vec2 uv_orig = uv;

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

    vec3 rd = normalize(vec3(uv, 1.));

    // rd.x *= mat_x_factor3;
    // rd.y *= mat_y_factor3;
    // rd.z *= mat_z_factor3;

    vec3 ro = vec3(0., .0, -3.1);


    float d = 0.0, t = 0.0, ns = 0.;

    for(int i = 0; i < mat_iterations; i++){
        d = map(ro + rd*t);

        if(d < 0.001 || t > 100.) break;
        t += d * .5 * mat_morph3;
        ns++;
    }

    vec3 p = ro + rd*t;
    vec3 n = normal(p);

    // changes lighting
    // p.z *= mat_z_factor3;

    vec3 lp = vec3(-0.5, 0.5, -3.) * mat_lighting;
    vec3 ld = normalize(lp-p);

    float dd = length(p - lp) / mat_power;

    // ld.x += mat_x_factor3;
    // ld.y += mat_y_factor3;

    vec2 lighting_shift = mat_lighting_shift;
    lighting_shift += vec2(0.5);
    lighting_shift.y = 1. - lighting_shift.y;
    lighting_shift -= vec2(0.5);

    // lighting_shift *= 2.;

    ld.x += lighting_shift.x;
    ld.y += lighting_shift.y;

    ld.z += mat_shine_stroke;

    float fal = 3.5 / (dd*dd);
    float dif = max(dot(n, ld), .01);
    float spec = pow(max(dot( reflect(-ld, n), -rd), 0.), 23.)  * pow(mat_shine,8.);

    trn(p);

    vec3 ref;
    if (mat_shading) {
        ref = reflect(rd, n);
    } else {
        ref = vec3(0.);
    }

    // * mat_morph5;


    vec3 cm = textureCube(mat_cube_map, ref).xyz;


    float expand_time = mat_time;
    if (!mat_animate_expand) {
        expand_time = 0.;
    }

    // color fade
    vec3 mm = .5+.5*cos(vec3(1., 2., 3.0)*(expand_time-2.)*.08+vec3(4., .8, .4));
    vec3 mat = mix(cm*.7, mm*2.8, mat_val(p)*0.1);

    vec3 col = mat * dif * fal  * pow(mat_gain, 4.);
    col += spec * vec3(.9, .7, .7)*.5;

    col *= max(mat_ss(25., 8., ns), .06);

    // col = mix(vec3(.01)*abs(gl_FragCoord.xy/R).y, col, exp(-t*t*t*.001));


    // background
    // col = mix(vec3(.01)*abs(texCoord).y, col, exp(-t*t*t*.001));

    // out_color = vec4(pow(max(col, 0.), vec3(1./2.2)), 1);

    vec4 col2 = vec4(col, 1.);

    vec2 back_uv = texCoord;

    // back_uv += vec2(0.5);
    back_uv = matRot2D(back_uv, 2*PI*mat_back_rotate/360);
    // back_uv -= vec2(0.5);

    if (mat_gradient) {
        col2 = mix(mat_back_color*abs(back_uv).y, col2, exp(-t*t*t*.001));
    } else {
        col2 = mix(mat_back_color, col2, exp(-t*t*t*.001));

    }

    // out_color = vec4(pow(max(col2, 0.), vec4(vec3(1./2.2),1.)));

    out_color = vec4(pow(max(col2, 0.), vec4(1./2.2)));


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
