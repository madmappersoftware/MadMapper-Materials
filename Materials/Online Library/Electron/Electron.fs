/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "gopher, adapted by Jason Beyers",

    "DESCRIPTION": "Electron generator. From https:\/\/www.shadertoy.com\/view\/MslGRn",

    "VSN": "1.1",

    "INPUTS": [




        {
            "LABEL": "Electron/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Electron/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "Label": "Electron/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "float",
            "MIN": 10.0,
            "MAX": 100.0,
            "DEFAULT": 50.0
        },

        {
            "Label": "Electron/Amplitude",
            "NAME": "mat_amplitude",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.05,
            "DEFAULT": 0.01
        },

        {
            "Label": "Electron/Frequency",
            "NAME": "mat_frequency",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.25
        },

        {
            "Label": "Electron/Density",
            "NAME": "mat_density",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.2,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Noise/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",

        },

        {
            "LABEL": "Spin/Spin",
            "NAME": "mat_spin_active",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "Label": "Spin/Amount",
            "NAME": "mat_spin_amount",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Spin/Animate",
            "NAME": "mat_spin_animate",
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
            "DEFAULT": 0.5
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
            "Label": "Spin/Strob",
            "NAME": "mat_spin_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Spin/Decay",
            "NAME": "mat_spin_decay",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Spin/Release",
            "NAME": "mat_spin_release",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Spin/Restart",
            "NAME": "mat_spin_restart",
            "TYPE": "event",

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
                "reset": "mat_restart",
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
                "reset": "mat_spin_restart",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_noise_time = 8. * mat_time_source - mat_offset;


///////////////////////////////////////////////////////////////////////////////////////
// INFO:
// - use the mouse to navigate (x is rotation, y is zoom)
// - play with the defines below to change the visuals
////////////////////////////////////////////////////////////////////////////////////////



// // the more slices the slower
// #define mat_iterations          50.0
// start amplitude for the mat_noise
// #define mat_amplitude 0.01
// // start frequency for the mat_noise
// #define mat_frequency 1.25
// // start density value
// #define mat_density   0.0
// // animation speed
#define ANIMATION_SPEED 0.075

////////////////////////////////////////////////////////////////////////////////////////
// iq's 3d mat_noise functions from the elevated shader (incl. modifications where needed)
////////////////////////////////////////////////////////////////////////////////////////

// rotation matrix for mat_fbm octaves
mat3 m = mat3( 0.00,  0.80,  0.60,
              -0.80,  0.36, -0.48,
              -0.60, -0.48,  0.64 );

float mat_hash( float n )
{
    return fract(sin(n)*43758.5453123);
}

// 3d mat_noise function
float mat_noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    float res = mix(mix(mix( mat_hash(n+  0.0), mat_hash(n+  1.0),f.x),
                        mix( mat_hash(n+ 57.0), mat_hash(n+ 58.0),f.x),f.y),
                    mix(mix( mat_hash(n+113.0), mat_hash(n+114.0),f.x),
                        mix( mat_hash(n+170.0), mat_hash(n+171.0),f.x),f.y),f.z);
    return res;
}

// mat_fbm mat_noise for 2-4 octaves including rotation per octave
float mat_fbm( vec3 p )
{
    float f = 0.0;
    f += 0.5000*mat_noise( p );
    p = m*p*2.02;
    f += 0.2500*mat_noise( p );
// set to 1 for 2 octaves
#if 0
    return f/0.75;
#else
    p = m*p*2.03;
    f += 0.1250*mat_noise( p );
// set to 1 for 3 octaves, 0 for 4 octaves
#if 1
    return f/0.875;
#else
    p = m*p*2.01;
    f += 0.0625*mat_noise( p );
    return f/0.9375;
#endif
#endif
}

////////////////////////////////////////////////////////////////////////////////////////

// color mat_gradient
vec3 mat_gradient(float s)
{
    return vec3(0.0, max(1.0-s*2.0, 0.0), max(s>0.5?1.0-(s-0.5)*5.0:1.0, 0.0));
}

// intersection for a sphere with a ray
#define RADIUS 0.5
bool intersectSphere(vec3 origin, vec3 direction, out float tmin, out float tmax)
{
    bool hit = false;
    float a = dot(direction, direction);
    float b = 2.0*dot(origin, direction);
    float c = dot(origin, origin) - 0.5*0.5;
    float disc = b*b - 4.0*a*c;           // discriminant
    tmin = tmax = 0.0;

    if (disc > 0.0) {
        // Real root of disc, so intersection
        float sdisc = sqrt(disc);
        float t0 = (-b - sdisc)/(2.0*a);          // closest intersection distance
        float t1 = (-b + sdisc)/(2.0*a);          // furthest intersection distance

        tmax = t1;
        if (t0 >= 0.0)
            tmin = t0;
        hit = true;
    }

    return hit;
}

// rotate around axis
vec2 rt(vec2 x,float y)
{
    return vec2(cos(y)*x.x-sin(y)*x.y,sin(y)*x.x+cos(y)*x.y);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;


    // normalized and aspect ratio corrected pixel coordinate
    vec2 p = uv;

    float spin_time = 0.;

    if (mat_spin_active) {


        if (mat_spin_animate) {
            spin_time = fract(0.25*mat_spin_time_source - mat_spin_offset);
        } else {
            spin_time = 0.;
        }

        if (spin_time < mat_spin_decay) {
            spin_time = 1.;
        } else {
            // get back a value from 0-1 (from end of decay to 1 - end of beat)
            spin_time = (spin_time - mat_spin_decay) * 1. / (1. - mat_spin_decay);
            if (spin_time < mat_spin_release) {
                spin_time = 1. - spin_time * 1. / mat_spin_release;
            } else {
                spin_time = 0.;
            }
        }

        spin_time = 1. - spin_time;

        spin_time += mat_spin_amount / 360.;
    }



    // camera and user input
    // vec3 oo = vec3(0, 0, 1.0-iMouse.y/RENDERSIZE.y);

    vec3 oo = vec3(0., 0., 1.);

    vec3 od = normalize(vec3(p.x, p.y, -2.0));
    vec3 o,d;
    o.xz = rt(oo.xz, 6.3*spin_time);
    o.y = oo.y;
    d.xz = rt(od.xz, 6.3*spin_time);
    d.y = od.y;
    // render
    vec4 col = vec4(0, 0, 0, 0);
    float tmin, tmax;
    if (intersectSphere(o, d, tmin, tmax))
    {
        // step thoug the sphere with max mat_iterations steps
        for (float i = 0.0; i < mat_iterations; i+=1.0)
        {
            // stay within the sphere bounds
            float t = tmin+i/mat_iterations;
            if (t > tmax)
                break;
            vec3 curpos = o + d*t;

            // get sphere falloff in s
            float s = (0.5-length(curpos))*2.0;

            s*=s;
            // get turbulence in d
            float a = mat_amplitude;
            float b = mat_frequency;
            float d = mat_density;
            for (int j = 0; j < 3; j++)
            {
                d += 0.5/abs((mat_fbm(5.0*curpos*b+ANIMATION_SPEED*mat_noise_time/b)*2.0-1.0)/a);
                b *= 2.0;
                a /= 2.0;
            }

            // get mat_gradient color depending on s
            col.rgb += mat_gradient(s)*max(d*s,0.0);
        }
    }
    out_color = col;

    out_color.a = 1.;




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
