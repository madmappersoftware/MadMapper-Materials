/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "leon, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/4dXBD2",

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
            "LABEL": "Orbit/Enable Orbit",
            "NAME": "mat_orbit_enable",
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
            "LABEL": "Iterators/Replicas",
            "NAME": "mat_replicas",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 8
        },
        {
            "LABEL": "Iterators/Ray Iter",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 50,
            "DEFAULT": 50
        },

        {
            "LABEL": "Circle/Size",
            "NAME": "mat_circle_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circle/Fill",
            "NAME": "mat_circle_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circle/Speed",
            "NAME": "mat_circle_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Circle/BPM Sync",
            "NAME": "mat_circle_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Circle/Reverse",
            "NAME": "mat_circle_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Circle/Offset",
            "NAME": "mat_circle_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Circle/Offset Scale",
            "NAME": "mat_circle_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Circle/Strob",
            "NAME": "mat_circle_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Circle/Restart",
            "NAME": "mat_circle_restart",
            "TYPE": "event",
        },



        {
            "LABEL": "Tunnel/Size",
            "NAME": "mat_tunnel_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/FOV",
            "NAME": "mat_tunnel_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/Speed",
            "NAME": "mat_tunnel_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/BPM Sync",
            "NAME": "mat_tunnel_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Tunnel/Reverse",
            "NAME": "mat_tunnel_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Tunnel/Offset",
            "NAME": "mat_tunnel_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Tunnel/Offset Scale",
            "NAME": "mat_tunnel_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Tunnel/Strob",
            "NAME": "mat_tunnel_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Tunnel/Restart",
            "NAME": "mat_tunnel_restart",
            "TYPE": "event",
        },




        {
            "LABEL": "Wave/Amplitude",
            "NAME": "mat_wave_amplitude",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Speed",
            "NAME": "mat_wave_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/BPM Sync",
            "NAME": "mat_wave_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Wave/Reverse",
            "NAME": "mat_wave_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Wave/Offset",
            "NAME": "mat_wave_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Wave/Offset Scale",
            "NAME": "mat_wave_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Wave/Strob",
            "NAME": "mat_wave_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Wave/Restart",
            "NAME": "mat_wave_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Background/Background Alpha",
            "NAME": "mat_background",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
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
            "NAME": "mat_wave_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_wave_speed",
                "speed_curve":2,
                "reverse": "mat_wave_reverse",
                "strob" : "mat_wave_strob",
                "reset": "mat_wave_restart",
                "bpm_sync": "mat_wave_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_circle_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_circle_speed",
                "speed_curve":2,
                "reverse": "mat_circle_reverse",
                "strob" : "mat_circle_strob",
                "reset": "mat_circle_restart",
                "bpm_sync": "mat_circle_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_tunnel_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_tunnel_speed",
                "speed_curve":2,
                "reverse": "mat_tunnel_reverse",
                "strob" : "mat_tunnel_strob",
                "reset": "mat_tunnel_restart",
                "bpm_sync": "mat_tunnel_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_wave_time = mat_wave_time_source - mat_wave_offset * 8. * mat_wave_offset_scale;
float mat_circle_time = mat_circle_time_source - mat_circle_offset * 8. * mat_circle_offset_scale;
float mat_tunnel_time = mat_tunnel_time_source - mat_tunnel_offset * 8. * mat_tunnel_offset_scale;

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

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}


// Leon 04 / 07 / 2017
// using lines of code from iq, mercury, lj, koltes, duke

// #define PI 3.14159
#define TAU 2.*PI
// #define t mat_time*.3
#define DITHER
float STEPS = float(mat_iterations);

float rand(vec2 co) { return fract(sin(dot(co*0.123,vec2(12.9898,78.233))) * 43758.5453); }
float cyl (vec2 p, float r) { return length(p)-r; }
mat2 rot (float a) { float c=cos(a),s=sin(a);return mat2(c,-s,s,c);}
vec3 moda (vec2 p, float count) {
    float an = TAU/count;
    float a = atan(p.y,p.x)+an/2.;
    float c = floor(a/an);
    c = mix(c,abs(c),step(count/2., abs(c)));
    a = mod(a,an)-an/2.;
    return vec3(vec2(cos(a),sin(a))*length(p),c) * mat_tunnel_fov;
}
float smin (float a, float b, float r) {
    float h = clamp(.5+.5*(b-a)/r, 0., 1.);
    return mix(b, a, h) - r*h*(1.-h) * mat_circle_fill;
}

vec3 camera (vec3 p) {
    //p.xz *= rot((PI*(iMouse.x/RENDERSIZE.x-.5)*step(0.5,iMouse.z)));
    //p.yz *= rot((PI*(iMouse.y/RENDERSIZE.y-.5)*step(0.5,iMouse.z)));

    if (mat_orbit_enable) {
        p.xz *= rot((PI*mat_orbit.x*step(0.5,1.)));
        p.yz *= rot((PI*mat_orbit.y*step(0.5,1.)));
    }
    p.yz *= rot(PI/2.);
    return p;
}

float curve (float x) {
    return sin(x*4.-mat_wave_time*.3*2.)*.1 + sin(x*2.+mat_wave_time*.3*4.)*0.2 * pow(mat_wave_amplitude,2.);
}

// lines tunnel
vec3 displace (vec3 p, float radius, float count) {
    vec3 p1 = moda(p.xz, count);
    p1.x -= radius * mat_tunnel_size;
    p.xz = p1.xy;
    p.z -= curve(p.y+p1.z)*.2;
    return p;
}

// circles stuff
vec3 displace2 (vec3 p, float radius, float count) {
    float a = atan(p.y,p.x);
    float l = length(p.xy);
    p.x = l - radius * mat_circle_size;
    return p;
}

float map (vec3 p) {
    float scene = 1.;
    vec3 p0 = p;

    // tunnel distortion
    p.xy *= rot(sin(length(p)+mat_tunnel_time*.3*4.)*.1);
    p.yz *= rot(sin(length(p*.5)+mat_tunnel_time*.3)*.2);
    p.xz *= rot(sin(length(p*2.)+mat_tunnel_time*.3*2.)*.1);
    float radius = 2.;
    float size = 0.02;
    float count = 9.;
    vec2 front = normalize(p.xz)*.1;
    vec2 right = vec2(front.y,-front.x);
    vec3 p2 = p;
    p.y -= mat_tunnel_time*.3*4.;
    float repeat = float(mat_replicas);
    for (float i = 0.; i < repeat; ++i) {

        // lines tunnel
        p.xz -= front*1.5;
        p.xz -= right*.5;
        scene = min(scene,cyl(displace(p, radius, count).xz,size));

        // circle stuff
        p2.xz *= rot(.3*(i/repeat)+mat_circle_time*.3*.5);
        p2.yz *= rot(.2*i/repeat+mat_circle_time*.3);
        p2.yx *= rot(.4*i/repeat+mat_circle_time*.3*2.);
        scene = smin(scene,cyl(displace2(p2, radius*.5-i*.05, count).xz,size),.1);
    }
    return scene;
}

vec3 getNormal (vec3 p) {
    float e = 0.01;
    return normalize(vec3(map(p+vec3(e,0,0))-map(p-vec3(e,0,0)),
                          map(p+vec3(0,e,0))-map(p-vec3(0,e,0)),
                          map(p+vec3(0,0,e))-map(p-vec3(0,0,e))));
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    #ifdef DITHER
    vec2 dpos = uv - vec2(0.5);
    vec2 seed = dpos + fract(mat_wave_time);
    #endif
    vec3 eye = camera(vec3(uv,-3.));
    vec3 ray = normalize(camera(vec3(uv,1.0)));
    vec3 pos = eye;
    float shade = 0.;
    for (float i = 0.; i < STEPS; ++i) {
        float dist = map(pos);
        if (dist < 0.001) {
            break;
        }
        #ifdef DITHER
        dist=abs(dist)*(.8+0.2*rand(seed*vec2(i)));
        #endif
        pos += ray*dist;
        shade = i;
    }
    vec3 n = getNormal(pos);
    out_color = vec4(1);
    out_color.rgb = n*.5+.5;
    out_color.rgb *= 1.-shade/(STEPS-1.);


    // if (mat_luma(out_color.rgb) < mat_back_thresh) {
    //     out_color.a = 0.;
    // }

    if (mat_background) {
        out_color.a = mix(0., out_color.a, mat_luma(out_color.rgb) + mat_back_thresh);

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
