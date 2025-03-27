/*{
    "CREDIT": "annaesch, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/3dccD2",

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
            "LABEL": "Objects/Count",
            "NAME": "mat_count",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 40,
            "DEFAULT": 10
        },

        {
            "LABEL": "Objects/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Animation/Poly Factor",
            "NAME": "mat_anim_poly",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/Ball Factor",
            "NAME": "mat_anim_ball",
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
            "LABEL": "Color Fade/Speed",
            "NAME": "mat_color_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color Fade/BPM Sync",
            "NAME": "mat_color_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Color Fade/Reverse",
            "NAME": "mat_color_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Color Fade/Offset",
            "NAME": "mat_color_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Color Fade/Strob",
            "NAME": "mat_color_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },



        {
            "Label": "Background/Level",
            "NAME": "mat_background",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Background/Use Background Color",
            "NAME": "mat_use_background_color",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Background/Color",
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
        {
            "NAME": "mat_color_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_color_speed",
                "speed_curve":2,
                "reverse": "mat_color_reverse",
                "strob" : "mat_color_strob",
                "bpm_sync": "mat_color_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

float mat_color_time = mat_color_time_source - mat_color_offset * 16.;

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

#define MAX_STEPS 100
#define MAX_DIST 100.
#define SURF_DIST .01

// HELPER FUNCTIONS //
// from https://iquilezles.org/www/articles/distfunctions/distfunctions.htm
// and https://www.youtube.com/channel/UCcAlTqd9zID6aNX3TzwxJXg
// lighting and movement partially based on https://www.shadertoy.com/view/3sySRK

// SMOOTH UNION FUNCTION
// k controls the radious/distance of the smoothness
float smin( float a, float b, float k ) {
    float h = clamp( 0.5+0.5*(b-a)/k, 0., 1. );
    return mix( b, a, h ) - k*h*(1.0-h);
}

// ROTATE FUNCTION
mat2 Rot(float a) {
    float s = sin(a);
    float c = cos(a);
    return mat2(c, -s, s, c);
}

// GEOMETRY SIGNED DISTANCE FUNCTIONS (SDF) //

// OCTAHEDRON
float sdOctahedron( vec3 p, float s)
{
  p = abs(p);
  float m = p.x+p.y+p.z-s;

  vec3 q;
       if( 3.0*p.x < m ) q = p.xyz;
  else if( 3.0*p.y < m ) q = p.yzx;
  else if( 3.0*p.z < m ) q = p.zxy;
  else return m*0.57735027;

  float k = clamp(0.5*(q.z-q.y+s),0.0,s);
  return length(vec3(q.x,q.y-s+k,q.z-k));
}

// BOX
float dBox(vec3 p, vec3 s) {
    return length(max(abs(p)-s, 0.));
}

// TORUS
float sdTorus(vec3 p, vec2 r) {
    float x = length(p.xz)-r.x;
    return length(vec2(x, p.y))-r.y;
}

// SPHERE
float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}

// GET DISTANCE FROM VARIOUS GEOMETRY
float GetDist(vec3 p) {
    float d = 2.0;
    for (int i = 0; i < mat_count; i++) {
        float fi = float(i);
        float time = mat_time * (fract(fi * 0.03  ) - 0.2);

        vec3 tp = p;
        tp.yz *= Rot(fi + time/3.0);

        // tp.xy -= vec2(0.5);
        // tp += vec3(.);
        // tp.xy -= vec2(0.5);

        // tp /= mat_separation;

        // smin() to add objects together smoothly
         d = smin(sdOctahedron(tp + sin(time * pow(mat_anim_poly,2.) + fi * vec3(4.0, 6.0, 15.0)) *
                    vec3(2.5, 1.8, 0.8), mix(.8, 1.4, fract(fi * 0.25))),
                d,
                0.6 * mat_fill
                );
        /*
        d = smin(dBox(tp + sin(time + fi * vec3(4.0, 6.0, 15.0)) *
                     vec3(1.5, 1.5, 0.5),
                     vec3(mix(.5, 2.0, fract(fi * 0.25)),.5,.8)),
                 d,
                 0.4
                 );

        d = smin(sdTorus(tp + sin(time + fi * vec3(40.0, 60.0, 150.0)) *
                     vec3(3.0, 3.0, 0.3), vec2(mix(0.5, 1.5, fract(fi * 0.15)),.25)),
                 d,
                 0.4
                 );

        */

        // p /= mat_separation;

        d = smin(
            sdSphere(p + sin(time * pow(mat_anim_ball, 2.) + fi * vec3(40.0, 60.0, 150.0)) *
                     vec3(2.5, 2.0, 0.9),
                     // mix interpolates between different sphere radius
                     mix(0.2, .3, fract(fi * 0.25 + .5))),
            d,  // compare distance (d) from sdSphere with previous d
            0.4 * mat_fill // smooth factor
        );
    }
    return d;
}

// GET NORMALS
vec3 GetNormal(vec3 p) {
    float d = GetDist(p);   //get distance to surface
    vec2 e = vec2(.01, 0);

    vec3 n = d - vec3(      // points around p
        GetDist(p-e.xyy),
        GetDist(p-e.yxy),
        GetDist(p-e.yyx));

    return normalize(n);
}


// RAYMARCHING ALGORITHM
float RayMarch(vec3 rayPos, vec3 rayDir) {
    float distO=0.; // distance origin

    for(int i=0; i<MAX_STEPS; i++) {
        vec3 p = rayPos + rayDir*distO; //marching step point p
        float distS = GetDist(p);   // distance to closest object
        distO += distS;             // move to next marching step
        if(distO>MAX_DIST || distS<SURF_DIST) break;
    }

    return distO;
}

// SCENE LIGHTING
float GetLight(vec3 p) {
    vec3 lightPos = vec3(0, 8, 4);
    lightPos.xz += vec2(sin(mat_time), cos(mat_time))*3.; // moving lightPos
    vec3 l = normalize(lightPos-p); // vec from p to light source
    vec3 n = GetNormal(p);

    float dif = clamp(dot(n, l), 0., 1.); // no negative results

    // shadow
    //float d = RayMarch(p+n*SURF_DIST*2., l); // distance to object
    //if(d<length(lightPos-p)) dif *= .1; //check if distance to object is closer than ditance to light

    return dif;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

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

    vec3 col = vec3(0);

    vec3 rayPos = vec3(uv *6., 3.0); // ray origin
    vec3 rayDir = normalize(vec3(0, 0, -1)); // ray direction normalized
    float d = RayMarch(rayPos, rayDir); // distance to objects

    float d2 = d;

    vec3 p = rayPos + rayDir * d; // position for lighting

    if (!mat_use_background_color) {
        d = min(6.0 / mat_background, d); // min d value of 6
    }

    // d = 0.;
    float dif = GetLight(p); // diffuse light

    col = vec3(dif);

    // Color
    col = (0.6 + 0.4 * cos((dif + mat_color_time * 1.) + uv.yxx * 1.8 +
                           vec3(4,8,0))) * (0.95 + dif * 0.35);

    vec3 col2 = col;
    col *= exp( -d * 0.15 ); // darker

    //col =  pow(col, vec3(.5)); // gamma correction

    out_color = vec4(col,1.0);

    if (mat_use_background_color) {

        if (mat_luma(col) < 0.05) {
            out_color = mat_back_color;

        }
        // out_color = mix(mat_back_color, out_color, dif);
    }

    // if (mat_luma(col) > 0.) {
    //     out_color.a = 1.;
    // }



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
