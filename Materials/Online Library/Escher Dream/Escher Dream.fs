/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "phreax, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/dd2SRd.  Diffraction grating by Alan Zucconi: https://www.alanzucconi.com/2017/07/15/the-mathematics-of-diffraction-grating",

    "VSN": "1.0",

    "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": [
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232.png",
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232_1.png",
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232_2.png",
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232_3.png",
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232_4.png",
                "793a105653fbdadabdc1325ca08675e1ce48ae5f12e37973829c87bea4be3232_5.png"
            ],
            "TYPE": "cube"
        }
    },

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
            "LABEL": "Orbit/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Orbit/Orbit Scale",
            "NAME": "mat_orbit_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Shape/Size 1",
            "NAME": "mat_dist1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Size 2",
            "NAME": "mat_dist2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Fill 1",
            "NAME": "mat_fill1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Fill 2",
            "NAME": "mat_fill2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 200,
            "DEFAULT": 200
        },
        {
            "LABEL": "Surface/Texture",
            "NAME": "mat_surface",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Surface/Grid 1 Scale",
            "NAME": "mat_grid_scale",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Surface/Grid 2 Scale",
            "NAME": "mat_grid2_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Surface/Light Offset",
            "NAME": "mat_light_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
         {
            "LABEL": "Surface/Light Power",
            "NAME": "mat_light_pow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Surface/Shadow",
            "NAME": "mat_shadow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Spin/Mode",
            "NAME": "mat_a1_mode",
            "TYPE": "long",
            "VALUES": ["Sine","Continuous"],
            "DEFAULT": "Sine"
        },
        {
            "LABEL": "Spin/Sine Range",
            "NAME": "mat_a1_range",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Spin/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Spin/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spin/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Spin/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Spin/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Spin/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Spin/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },


        {
            "LABEL": "Shuffle/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Shuffle/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Shuffle/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Shuffle/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Shuffle/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Shuffle/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Shuffle/Restart",
            "NAME": "mat_a2_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Background/Level",
            "NAME": "mat_back_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Background/Color 1",
            "NAME": "mat_back_color1",
            "TYPE": "color",
            "DEFAULT": [
                0.478,
                1.0,
                0.914,
                1.0
            ]
        },
        {
            "LABEL": "Background/Color 2",
            "NAME": "mat_back_color2",
            "TYPE": "color",
            "DEFAULT": [
                0.922,
                0.784,
                0.976,
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
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
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
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = (mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale) * 2.;
float mat_a2_time = (mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 2.;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}


vec2 flipX(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the X axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.x = 1.-new_coord.x;
    new_coord -= vec2(0.5);
    return new_coord;
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



/* Creative Commons Licence Attribution-NonCommercial-ShareAlike
   phreax 2022

   Implementing physical diffraction grating by Alan Zucconi (great article)

   https://www.alanzucconi.com/2017/07/15/the-mathematics-of-diffraction-grating/

   This was of great help, as I was to stupid to compute the tangent vectors
   https://www.shadertoy.com/view/7dVGzz

   Based on https://www.shadertoy.com/view/dd2SRd
*/

// #define PI 3.141592
#define TAU (2.*PI)
#define SIN(x) (sin(x)*.5+.5)
#define BUMP_EPS 0.004


float g_mat;
vec2 g_uv;

// first time to use a struct in my shaders (;
struct ObjectInfo {
    float t;
    float mat;
    vec2 uv;
};


mat2 rot(float a) { return mat2(cos(a), -sin(a), sin(a), cos(a)); }

// float saturate(float x) {
//     return clamp(x, 0., 1.);
// }

// vec3 saturate(vec3 x) {
//     return clamp(x, vec3(0), vec3(1));
// }


// zucconis spectral palette https://www.alanzucconi.com/2017/07/15/improving-the-rainbow-2/
vec3 bump3y (vec3 x, vec3 yoffset)
{
    vec3 y = 1. - x * x;
    y = clamp((y-yoffset), vec3(0), vec3(1));
    return y;
}

const highp float NOISE_GRANULARITY = 0.5/255.0;

highp float random(highp vec2 coords) {
   return fract(sin(dot(coords.xy, vec2(12.9898,78.233))) * 43758.5453);
}

vec3 invGamma(vec3 col) {
    return pow(col, vec3(2.2));
}

vec3 gamma(vec3 col) {
    return pow(col, vec3(1./2.2));
}

vec3 spectralZucconi6(float x) {
    const vec3 c1 = vec3(3.54585104, 2.93225262, 2.41593945);
    const vec3 x1 = vec3(0.69549072, 0.49228336, 0.27699880);
    const vec3 y1 = vec3(0.02312639, 0.15225084, 0.52607955);

    const vec3 c2 = vec3(3.90307140, 3.21182957, 3.96587128);
    const vec3 x2 = vec3(0.11748627, 0.86755042, 0.66077860);
    const vec3 y2 = vec3(0.84897130, 0.88445281, 0.73949448);

    vec3 col =  bump3y(c1 * (x - x1), y1) + bump3y(c2 * (x - x2), y2);
    col = invGamma(col);
    return col;
}

// wrapped map
vec3 wrappedSpectralZucconi6(float x) {
    x = fract(x);
    return spectralZucconi6(x);
}

// wavelength normalized for diffraction grading
vec3 waveZucconi6(float w) {

    if(w > 700.0 || w < 400.0){
        return vec3(0);
    }

    float x = fract((w - 400.0)/ 300.0);

    return spectralZucconi6(x);
}

float rect( vec2 p, vec2 b, float r ) {
    vec2 d = abs(p) - (b - r);
    return length(max(d, 0.)) + min(max(d.x, d.y), 0.) - r;
}


// Get orthonormal basis from surface normal
// https://graphics.pixar.com/library/OrthonormalB/paper.pdf
// from: https://www.shadertoy.com/view/7dVGzz
void pixarONB(vec3 n, out vec3 b1, out vec3 b2){
    float sign_ = sign(n.z);
    float a = -1.0 / (sign_ + n.z);
    float b = n.x * n.y * a;
    b1 = vec3(1.0 + sign_ * n.x * n.x * a, sign_ * b, -sign_ * n.x);
    b2 = vec3(b, sign_ + n.y * n.y * a, -n.y);
}

vec3 diffraction(vec3 rd, vec3 n, vec3 td, vec3 l, float d) {

    vec3 col = vec3(0);

    float cos_ThetaL = dot(l, td);
    float cos_ThetaV = dot(rd, td);

    float u = abs(cos_ThetaL - cos_ThetaV);

    if(u == 0.) {
        return vec3(0);
    }

    for(float i=1.; i < 2.; i++) {
        float wavelength = u * d / i;
        col += waveZucconi6(wavelength);
    }
    col = clamp(col, vec3(0), vec3(1));
    return col;
}

// iq
float sdBoxFrame( vec3 p, vec3 b, float e ){

  p = abs(p)-b;
  vec3 q = abs(p+e)-e;
  return min(min(
      length(max(vec3(p.x,q.y,q.z),0.0))+min(max(p.x,max(q.y,q.z)),0.0),
      length(max(vec3(q.x,p.y,q.z),0.0))+min(max(q.x,max(p.y,q.z)),0.0)),
      length(max(vec3(q.x,q.y,p.z),0.0))+min(max(q.x,max(q.y,p.z)),0.0));
}


float box(vec3 p, vec3 r) {
  vec3 d = abs(p) - r;
  return length(max(d, 0.0)) + min(max(d.x, max(d.y, d.z)), 0.0);
}


vec3 transform(vec3 p) {

    float spin;

    if (mat_a1_mode == 0) { // Sine
        spin = mat_a1_range * sin(0.25*mat_a1_time);
    } else { // Continuous
        spin = 0.25*mat_a1_time;
    }

    vec2 orbit = flipX(mat_orbit, mat_orbit_scale * 0.25);

    p.z -= 5.;

    p.yz *= rot(.3*PI*spin);

    p.xz += vec2(0.5);
    p.xz = matRot2D(p.xz, -2*PI*orbit.x);
    p.xz -= vec2(0.5);

    p.yz += vec2(0.5);
    p.yz = matRot2D(p.yz, -2*PI*orbit.y);
    p.yz -= vec2(0.5);

    p.yx *= rot(PI*.25 );

    // // Orig:
    // p.z -= 5.;
    // p.yz *= rot(.3*PI*spin);
    // p.yx *= rot(PI*.25 );
    return p;
}

// polar coords
vec2 torusUV(vec3 p) {
    float a = atan(p.z, p.x);
    float r = length(p.zx);

    return vec2(r, a);
}

// adapted from https://www.shadertoy.com/view/4sjXW1
vec2 cubeUV(in vec3 p) {
    vec2 x = p.zy/p.x;
    vec2 y = p.xz/p.y;
    vec2 z = p.xy/p.z;

    //select face
    p = abs(p);
    if (p.x > p.y && p.x > p.z) return x;
    else if (p.y > p.x && p.y > p.z) return y;
    else return z;
}

float map(vec3 p) {

    vec3 bp = p;
    float edge = 0.005;

    p = transform(p);

    vec3 p1 = abs(p)- mix(.0, 1.9 * mat_dist1, SIN(0.5*mat_a2_time));
    float f1 = sdBoxFrame(p1, vec3(1.0), .15 * mat_fill1)-edge;

    vec3 p2 = abs(p)- mix(.0, 1.9*2. * mat_dist2, SIN(.25*mat_a2_time))-edge;
    float f2 = sdBoxFrame(p2, vec3(1.+.5*SIN(.25*mat_a2_time)), .15 * mat_fill2)*.75;

    g_uv = f1 < f2 ? cubeUV(p1) : cubeUV(p2);  // save uv's for texture and bump mapping

    float d = min(f1, f2);
    return d;

}


vec3 getNormal(vec3 p) {

    vec2 eps = vec2(0.001, 0.0);
    return normalize(vec3(map(p + eps.xyy) - map(p - eps.xyy),
                          map(p + eps.yxy) - map(p - eps.yxy),
                          map(p + eps.yyx) - map(p - eps.yyx)
                         )
                     );
}

// Shane's bump mapping
float gridSurface( in vec3 p){
    p = abs(mod(p*2., 1.*0.125 * mat_grid_scale)-0.0125);

    float x = min(p.x,min(p.z, p.y))/0.03125;

    return clamp(x, 0., 1.);
}

// Standard function-based bump mapping function (from Shane)
vec3 doBumpMap(in vec3 p, in vec3 nor, float bumpfactor){

    const float eps = BUMP_EPS;
    float ref = gridSurface(p);
    vec3 grad = vec3( gridSurface(vec3(p.x-eps, p.y, p.z))-ref,
                      gridSurface(vec3(p.x, p.y-eps, p.z))-ref,
                      gridSurface(vec3(p.x, p.y, p.z-eps))-ref )/eps;

    grad -= nor*dot(nor, grad);

    return normalize( nor + bumpfactor*grad ) * mat_surface;

}

// from iq
float softshadow( in vec3 ro, in vec3 rd, float mint, float maxt, float k )
{
    float res = 1.0;
    float ph = 1e20;
    for( float t=mint; t<maxt; )
    {
        float h = map(ro + rd*t);
        if( h<0.001 )
            return 0.0;
        float y = h*h/(2.0*ph);
        float d = sqrt(h*h-y*y);
        res = min( res, k*d/max(0.0,t-y) );
        ph = h;
        t += h;
    }
    return res;
}

ObjectInfo raymarch(vec3 ro, vec3 rd, float steps) {

    float mat = 0.,
          t   = 0.,
          d   = 0.;

    vec2 uv; // object uv

    vec3 p = ro;
    for(float i=.0; i<steps; i++) {

        d = map(p);
        mat = g_mat;  // save global material infos
        uv = g_uv;

        if(abs(d) < 0.0001 || t > 100.) break;

        t += d;
        p += rd*d;
    }

    return ObjectInfo(t, mat, uv);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec3 ro = vec3(uv*9.,-4.),      // ray origin and ray direction swapped, for isometric /
         rd = vec3(0,0,1.),         // orhographic perspective
         lp = vec3(3., 4., -4) * mat_light_offset,
         lp2 = vec3(-3., -4., -2);
    vec3 col;

    float mat = 0.,
          t   = 0.,
          d   = 0.;
    vec2 e = vec2(0.0035, -0.0035);

    // background color
    // vec3 c1 = vec3(0.478,1.000,0.914);
    // vec3 c2 = vec3(0.922,0.784,0.976);

    vec3 c1 = mat_back_color1.rgb;
    vec3 c2 = mat_back_color2.rgb;

    c1 *= mat_back_level;
    c2 *= mat_back_level;

    float steps = float(mat_iterations);
    ObjectInfo objectInfo = raymarch(ro, rd, steps);
    mat = objectInfo.mat;
    vec3 p = ro + objectInfo.t*rd;
    vec3 n = normalize(e.xyy*map(p+e.xyy) + e.yyx*map(p+e.yyx) +
                       e.yxy*map(p+e.yxy) + e.xxx*map(p+e.xxx));
    vec2 tuv = objectInfo.uv;
    n = doBumpMap(vec3(tuv*.5, 0.), n, .009);
    if(objectInfo.t < 50.) {
        vec3 l = normalize(lp-p);
        vec3 l2 = normalize(lp2-p);
        float dif = max(dot(n, l), .0);
        float dif2 = max(dot(n, l2), .0);
        float spe = pow(max(dot(reflect(-rd, n), -l), .0),40. * mat_shadow);
        float shd = softshadow(p, l2, 0.1, 50., 5.0) * mat_light_pow;
        float sss = smoothstep(0., 1., map(p + l * .4)) / .4;
        float height = atan(n.y, n.x);
        // some none physical iridescence
        vec3 iri = spectralZucconi6(height*1.11)*smoothstep(.8, .2, abs(n.z))-.08 * mat_shadow;
        // get tangent vector for diffraction grating
        vec3 tangent;
        vec3 bitangent;
        pixarONB(n, tangent, bitangent);
        tangent = normalize(tangent);
        bitangent = normalize(bitangent);

        mat3 tbn = mat3(tangent, bitangent, n);
        vec3 td = normalize(tbn * vec3(tuv, 0.));

        l = normalize(vec3(0, 1, 0));
        vec3 difr = diffraction(-rd, n, td, l, 780.); // zucconis diffraction grating
        col += .5*(c1*dif + c2*dif2)+ 1.8*difr + .9*iri + .4*sss + .2;
        float grids = 60. * mat_grid2_scale;
        vec2 grid = smoothstep(.0, .1, SIN(grids*(tuv+.5)));
        col *= dot(grid, vec2(.5));

        if(mat == 0.) {
            vec3 refld = reflect(ro, n);
            vec3 refl = textureCube(iChannel0,refld).rgb;

            refl = invGamma(refl);
            col = mix(col, refl, .5);
        }
        col *= 1.3; // brighten

        // fog
        float t = objectInfo.t;
        float fog = 1.-exp(-t*t*0.005);

        col = mix(col, c1, fog);
        col = mix(col, col*shd, .7);
    } else {
        col =  mix(c1, c2, (pow(dot(uv, uv), .8)))*.9; // background
        col = invGamma(col);
    }

    col += mix(-NOISE_GRANULARITY, NOISE_GRANULARITY, random(uv)); // dither
    col *= mix(.2, 1., (1.5-pow(dot(uv, uv), .5))); // vignette
    col = gamma(col); // gamma

    out_color = vec4(col, 1.0);


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
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
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    return out_color;
}
