/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "fizzer, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/lsS3zy",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
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
            "LABEL": "Spheres/Size",
            "NAME": "mat_radius_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Spheres/Constant Radius",
            "NAME": "mat_constant_sphere_radius",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spheres/Radius",
            "NAME": "mat_radius_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Spheres/Density",
            "NAME": "mat_density",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Spheres/Layers",
            "NAME": "mat_layers",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 10,
            "DEFAULT": 5
        },

        {
            "LABEL": "Spheres/P1",
            "NAME": "mat_p1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Spheres/P2",
            "NAME": "mat_p2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Expand/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Expand/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Expand/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Expand/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Expand/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Expand/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Spin/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Spin/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spin/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Spin/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Spin/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Spin/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Background/Level",
            "NAME": "mat_back_lvl",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Background/Alpha",
            "NAME": "mat_back_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
            "NAME": "mat_a1_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a1_speed",
                "speed_curve":2,
                "reverse": "mat_a1_reverse",
                "strob" : "mat_a1_strob",
                "bpm_sync": "mat_a1_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_a2_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a2_speed",
                "speed_curve":2,
                "reverse": "mat_a2_reverse",
                "strob" : "mat_a2_strob",
                "bpm_sync": "mat_a2_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = (mat_a1_source - mat_a1_offset * 8. * mat_a1_offset_scale) * 0.25;
float mat_a2_time = (mat_a2_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 0.25;

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



float de(vec3 p, float time)
{
    float inner_sphere_scale = 0.7;
    float s=4.0 * mat_density;
    float t=fract(time/0.6);
    float scale=pow(inner_sphere_scale, -t);
    float d=1e4;

    for(int i = 0; i < mat_layers; i += 1)
    {
        vec3 z=p/scale,az=abs(z),w=z;

        if(az.x>az.y && az.x>az.z)
            w/=w.x*sign(w.x);
        else if(az.y>az.x && az.y>az.z)
            w/=w.y*sign(w.y);
            else
                w/=w.z*sign(w.z);

            w=normalize(floor(w*s+vec3(0.5)))*0.4 * mat_radius_1;

        float r2 = mat_radius_2;

        if (!mat_constant_sphere_radius) {
            r2 *=max(az.x, az.y) * 3.;
        }

        d=min(d,(distance(z,w) - r2*0.02*min(1.0, (float(i) + 1.0 - t) / 1.5))*scale);
        scale*=inner_sphere_scale;
    }

    return d;
}

mat3 rotateXMat(float a)
{
    return mat3(1.0, 0.0, 0.0, 0.0, cos(a), -sin(a), 0.0, sin(a), cos(a));
}

mat3 rotateYMat(float a)
{
    return mat3(cos(a), 0.0, -sin(a), 0.0, 1.0, 0.0, sin(a), 0.0, cos(a));
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

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;


    float time=mat_a1_time+10.63;

    mat3 m=rotateXMat(mat_a2_time*0.5)*rotateYMat(mat_a2_time*0.7);

    vec2 p=uv;

    vec3 ro=m*vec3(0.0,0.0,0.4);
    vec3 rd=m*normalize(vec3(p, -2.0));

    float t=0.0;

    for(int i=0;i<120;i+=1)
    {
        float d=de(ro+rd*t, time);
        if(abs(d)<1e-4)
            break;
        t+=d;
    }

    vec3 rp=ro+rd*t;

    vec3 col;

    if (mat_back_alpha) {
        col = vec3(0.);
    } else {
        col = mix(vec3(1.0,1.0,0.5) * mat_back_lvl,vec3(1.0,0.5,0.25)*0.1 * mat_back_lvl,pow(abs(rd.y),0.5));
    }

    // vec3 col=mix(vec3(1.0,0.0,0.0),vec3(0.0,0.0,0.0)*0.1,pow(abs(rd.y),0.5));

    if(t<20.0)
    {
        float e=1e-3, c=de(rp, time);
        vec3 n=normalize(vec3(de(rp+vec3(e,0.0,0.0), time)-c,de(rp+vec3(0.0,e,0.0), time)-c,de(rp+vec3(0.0,0.0,e), time)-c));

        col=mix(vec3(0.0),vec3(1.7,0.6,0.2)*5.0*mix(0.6,1.0,(0.5+0.5*cos(time*30.0))),
                pow(0.5+0.5*cos(rp.x*50.0+sin(rp.y*10.0)*3.0+cos(rp.z*4.0)),3.0));

        col*=vec3(mix(0.3,2.0,0.5+0.5*dot(n,rd)));

        col+=vec3(pow(clamp(0.5+0.5*dot(n,rd),0.0,1.0),2.0))*0.6;

        vec3 l=normalize(vec3(1.0,1.0,-1.0)-rp);
        vec3 h=normalize(normalize(l)+normalize(-rd));

        col+=vec3(pow(clamp(0.5+0.5*dot(h,n),0.0,1.0),80.0))*0.7;
    }

    if (!mat_back_alpha) {
        col+=(0.03+cos(time*7.0)*0.01)*mix(vec3(0.1,0.5,0.0)*0.3,vec3(1.2,0.7,0.1),0.5+0.5*cos(p.y*0.3+sin(p.x*0.1+time)*6.0)*sin(p.y)) * mat_back_lvl;
    }

    out_color.rgb=sqrt(col);
    out_color.a = 1.;


    if (mat_back_alpha) {
        out_color.a = mat_luma(out_color);
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
