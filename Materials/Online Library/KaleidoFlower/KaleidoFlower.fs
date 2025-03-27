/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Author unknown, adapted by Jason Beyers",
  "DESCRIPTION" : "From http:\/\/glslsandbox.com\/e#52856.0",
  "VSN": "1.0",
  "INPUTS" : [

    {
        "Label": "Flower Dance/Scale",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },

    {
        "Label": "Flower Dance/Rotate",
        "NAME": "mat_rotate",
        "TYPE": "float",
        "MIN": -360.0,
        "MAX": 360.,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Flower Dance/Shift",
        "NAME": "mat_shift_amount",
        "TYPE": "point2D",
        "MIN": [-1.0,-1.0],
        "MAX": [1.0,1.0],
        "DEFAULT": [0.0,0.0]

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
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float mat_time = mat_time_source * 0.25 - mat_offset * mat_offset_scale;

#define beat sin(mat_time/(0.25 * 60./149.)/2.)

// #define beat sin(mat_time/(mat_speed * 60./149.)/2.)

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

mat2 rot(float a) {
  float s = sin(a), c = cos(a);
  return mat2(c, s, -s, c);
}

vec2 kaleidoscope(vec2 p, float m) {
  float l = 1. / m;
  float t = l * .2887;
  float f = 1.;
  float a = 0.;
  p.y += t * 2.;
  vec2 c = vec2(0., t * 2.);
  vec2 q = p * m;
  q.y *= 1.1547;
  q.x += .5 * q.y;
  vec2 r = fract(q);
  vec2 s = floor(q);
  p.y -= s.y * .866 * l;
  p.x -= (s.x - q.y * .5) * l + p.y * .577;
  a += mod(s.y, 3.) * 2.;
  a += mod(s.x, 3.) * 2.;
  if (r.x > r.y) {
    f *= -1.;
    a += 1.;
    p += vec2(-l * .5, t);
  }
  p.x *= f;
  p -= c;
  p *= rot(a * 1.0472);
  p += c;
  p.y -= t * 2.;
  return p;
}

// vec3 hsv2rgb(vec3 c) {
//     vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );
//     rgb = rgb*rgb*(3.0-2.0*rgb);
//     return c.z * mix( vec3(1.0), rgb, c.y);
// }

vec2 brickTile(vec2 _st, float _zoom){
    _st *= _zoom;

    // Here is where the offset is happening
    if (mod(mat_time, 4.) > 2.0) {
        if (mod(mat_time, 2.) > 1.0)
        _st.x += step(1., mod(_st.y,2.0)) * mat_time;
        else
        _st.y += step(1., mod(_st.x,2.0)) * mat_time;
    }
    else {
        if (mod(mat_time, 2.) > 1.0)
        _st.x -= step(1., mod(_st.y,2.0)) * mat_time;
        else
        _st.y -= step(1., mod(_st.x,2.0)) * mat_time;
    }

    return fract(_st);
}

float box(vec2 _st, vec2 _size){
    _size = vec2(0.5)-_size*0.5;
    vec2 uv = smoothstep(_size,_size+vec2(1e-4),_st);
    uv *= smoothstep(_size,_size+vec2(1e-4),vec2(1.0)-_st);
    return uv.x*uv.y;
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= rot(sin(mat_time*0.25)*2.);
    uv = kaleidoscope(uv, 3.+sin(mat_time*0.1));
    uv *= rot(mat_time*0.3+beat);
    uv.x += cos(mat_time*0.1);
    uv = kaleidoscope(uv, 6.+cos(mat_time*0.3));
    uv *= rot(mat_time*0.35+beat);
    uv = kaleidoscope(uv, 12.+sin(mat_time*0.25));
    uv *= rot(mat_time*0.45+beat);
    uv.x += sin(mat_time*0.3);
    vec3 color;

    uv = brickTile(uv,4.0);

    color = vec3(box(uv,vec2(0.9)));

    // Uncomment to see the space coordinates
    color = hsv2rgb(reflect(vec3(uv.xy, uv.y/uv.x), color));

    out_color = vec4(color,1.0);

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