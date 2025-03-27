/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "izutionix, adapted by Jason Beyers",

    "DESCRIPTION": "Countdown clock to midnight, using system clock.  From https://www.shadertoy.com/view/7d3cRS",

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
            "LABEL": "Digits/Slant",
            "NAME": "mat_slant",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Color/Cut Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Alpha",
            "NAME": "mat_alpha",
            "TYPE": "bool",
            "DEFAULT": 'true',
            "FLAGS": "button"
        },
    ]

}*/

#include "MadCommon.glsl"

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

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale;

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


// original shader: https://www.shadertoy.com/view/Xsy3zG

float segment(vec2 uv)
{
    uv = abs(uv);
    return (1.0-smoothstep(0.07,0.10,uv.x))
         * (1.0-smoothstep(0.46,0.49,uv.y+uv.x))
         * (1.25 - length(uv*vec2(3.8,1.3)));
}

float sevenSegment(vec2 uv,int num)
{
    float seg= 0.0;

    if (num>=2 && num!=7 || num==-2)
        seg = max(seg,segment(uv.yx));

    if (num==0 ||
            (uv.y<0.?((num==2)==(uv.x<0.) || num==6 || num==8):
            (uv.x>0.?(num!=5 && num!=6):(num>=4 && num!=7) )))
        seg = max(seg,segment(abs(uv)-0.5));

    if (num>=0 && num!=1 && num!=4 && (num!=7 || uv.y>0.))
        seg = max(seg,segment(vec2(abs(uv.y)-1.0,uv.x)));

    return seg;
}

float showNum(vec2 uv,float nr, bool zeroTrim)
{
    if (uv.x>-3.0 && uv.x<0.0)
    {
        float digit = floor(-uv.x / 1.5);
        nr /= pow(10.,digit);
        nr = mod(floor(nr+0.000001),10.0);
        if (nr==0.0 && zeroTrim && digit!=0.0)
            return 0.;
        return sevenSegment(uv+vec2( 0.75 + digit*1.5,0.0),int(nr));
    }
    return 0.;
}

float dots(vec2 uv)
{
    uv.y = abs(uv.y)-0.5;
    float l = length(uv);
    return (1.0-smoothstep(0.11,0.13,l)) * (1.0-l*2.0);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    out_color = vec4(0.);



    // uv -= vec2(0.5);
    uv *= 11.0;

    uv.y = 1. - uv.y;
    uv.y -= 1.;
    // uv.x -= 5.+uv.y*.07;
    uv.x -= 5. + uv.y*.07 * mat_slant;
    if (uv.x<-10.0 || uv.x>0.0 || abs(uv.y)>1.2) {

        if (mat_alpha) {
            return vec4(0.);
        } else {
            return vec4(0.,0.,0.,1.);
        }
    }

    float p = floor(abs(uv.x/3.5));
    uv.x = mod(uv.x,3.5)-3.5;

    float seg = 0.0;
    if (uv.x>-3.)
        seg = showNum(uv,mod((86400.-DATE.w)/pow(60.0,p),60.0),p==2.0);
    else
    {
        uv.x += 3.25;
        seg = dots(uv);
    }

    out_color = vec4(seg,seg,seg,seg);


    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    if (mat_back_mode == 1) {
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = vec4(0.);
        } else {
            out_color = mat_front_color;
        }
    } else {
        out_color *= mat_front_color;
    }

    if (!mat_alpha) {
        out_color.a = 1.;
    }

    return out_color;
}
