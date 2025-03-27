/*{
    "CREDIT": "SnoopethDuckDuck, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/7lyXWz",

    "VSN": "1.0",

    "IMPORTED": {
        "mat_cube_1": {
            "NAME": "mat_cube_1",
            "PATH": [
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0.png",
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0_1.png",
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0_2.png",
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0_3.png",
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0_4.png",
                "94284d43be78f00eb6b298e6d78656a1b34e2b91b34940d02f1ca8b22310e8a0_5.png"
            ],
            "TYPE": "cube"
        },
        "mat_cube_2": {
            "NAME": "mat_cube_2",
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
            "LABEL": "Noise/Distance",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Scale",
            "NAME": "mat_n_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Cycle",
            "NAME": "mat_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
         {
            "LABEL": "Noise/Limit 1",
            "NAME": "mat_limit_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Noise/Limit 2",
            "NAME": "mat_limit_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Sharpness",
            "NAME": "mat_sharp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Fuzz",
            "NAME": "mat_fuzz",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 200,
            "DEFAULT": 50
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
            "LABEL": "Background/Custom Background",
            "NAME": "mat_background",
            "TYPE": "bool",
            "DEFAULT": 0,
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
            "LABEL": "Background/Back Thresh",
            "NAME": "mat_back_thresh",
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

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



// "RayMarching starting point"
// by Martijn Steinrucken aka The Art of Code/BigWings - 2020
// The MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// Email: countfrolic@gmail.com
// Twitter: @The_ArtOfCode
// YouTube: youtube.com/TheArtOfCodeIsCool
// Facebook: https://www.facebook.com/groups/theartofcode/
//
// You can use this shader as a template for ray marching shaders

#define pi 3.14159

// #define MAX_STEPS 50
#define MAX_DIST 50.
float SURF_DIST = .001 * pow(mat_fuzz, 4.);

#define S smoothstep


mat2 Rot(float a) {
    float s=sin(a), c=cos(a);
    return mat2(c, -s, s, c);
}

float thc(float a, float b) {
    return tanh(a * cos(b)) / tanh(a);
}


float h21 (vec2 a) {
    return fract(sin(dot(a.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

float mlength(vec2 uv) {
    return max(abs(uv.x), abs(uv.y));
}

float mlength(vec3 p) {
    return max(max(abs(p.x), abs(p.y)), abs(p.z));
}

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

float GetDist(vec3 p) {
    float sc = 4.;
    float sc2 = 2. * mat_n_scale;

    float a = atan(p.z, p.x);
    float th = atan(p.y, p.x);
    float r = length(p);/// (1. + p.y);

    float tim = 1. * a + r * 4. - mat_time;
    tim += 0.4 * h21(p.xz) / mat_sharp;
    p = 1.2 * r * vec3(sin(a + tim) * cos(th+ tim), cos(a - tim) * sin(th+ tim), cos(th + tim) + sin(th + tim));

    vec3 ip = floor(sc2 * p) + 0.;
    vec3 fp = fract(sc2 * p) - 0.5;


    vec3 vec = 0.3 * vec3(cos(10. * h21(ip.xy) + tim), cos(10. * h21(ip.yz) + tim), cos(10. * h21(ip.zx) + tim));
    float d0 = length(p) - 4. * mlength(fp) * mat_limit_1 - 0.;
    //d0 = length(p) - 1.;

    //p.y += cos(0.4 * r);
   // p = vec3(a / pi, p.y, -0.4 * log(r) + .2 * mat_time);

    vec2 ipos = floor(sc * p.xz) + 0.5;
    vec2 fpos = fract(sc * p.xz) - 0.5; //cos(sc * p.xz / pi) - 0.5;

    return 0.23 * d0 / mat_limit_2;
}

float RayMarch(vec3 ro, vec3 rd) {
    float dO=0.;

    for(int i=0; i<mat_iterations; i++) {
        vec3 p = ro + rd*dO;
        float dS = GetDist(p);
        dO += dS;
        if(dO>MAX_DIST || abs(dS)<SURF_DIST) break;
    }

    return dO;
}

vec3 GetNormal(vec3 p) {
    float d = GetDist(p);
    vec2 e = vec2(.001, 0);

    vec3 n = d - vec3(
        GetDist(p-e.xyy),
        GetDist(p-e.yxy),
        GetDist(p-e.yyx));

    return normalize(n);
}

vec3 GetRayDir(vec2 uv, vec3 p, vec3 l, float z) {
    vec3 f = normalize(l-p),
        r = normalize(cross(vec3(0,1,0), f)),
        u = cross(f,r),
        c = f*z,
        i = c + uv.x*r + uv.y*u,
        d = normalize(i);
    return d;
}

vec3 Bg(vec3 rd) {
    float k = rd.y *.5 + .5;

    vec3 col = mix(vec3(.5,0.,0.),vec3(0.),k);
    return col;
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

    vec2 uv_orig = uv;

    float time = 0.2 * mat_time;
    float height = 0.;//1. + cos(time);
    float dist = 5. * mat_dist;
    vec3 ro = vec3(dist * cos(time), height, dist * sin(time));
    //ro.yz *= Rot(-m.y*3.14+1.);
    //ro.xz *= Rot(-m.x*6.2831);

    vec3 rd = GetRayDir(uv, ro, vec3(0,height,0), 1.);
    vec3 col = vec3(0);

    float d = RayMarch(ro, rd);

    if(d<MAX_DIST) {
        vec3 p = ro + rd * d;
        vec3 n = GetNormal(p);
        vec3 r = reflect(rd, n);
       // vec3 rf = refract(rd, n,0.1);

        float dif = dot(n, normalize(vec3(1,2,3)))*.5+.5;
        col = 0.5 * vec3(dif);

        float b = .5 + .5 * cos(mat_time);

        // uv = gl_FragCoord.xy / RENDERSIZE.xy;

        uv = uv_orig;
        col += 0.5 * textureCube(mat_cube_1,r).rgb;

       // float a = atan(rf.z, rf.x);
       // float c = atan(rf.z,rf.y);
        vec2 ipos = floor(p.xz) + 0.5;
        float h = mix(h21(ipos),0., 0.5 + 0.5 * cos(mat_time));
        //col.r += 0.15 + .3 * cos(10. * rf.y + mat_time - .5 * pi);
        //col.g += 0.15 + .3 * cos(10. * rf.y + mat_time);
        //col.b += 0.15 + .3 * cos(10. * rf.y + mat_time + .5 * pi);
        //col = min(col, 1.2 * vec3(0.5 + 0.5 * cos(2. * p.y)));
       // col *= clamp(1.05 * p.y, 0., 1.);
        //col *= 0.5 + 0.5 * cos(length(p) + mat_time);
        col += thc(2., mat_cycle*2.5 * length(p) - mat_time);

        vec3 e = vec3(1.);
        col = col * pal(2.5 * length(p) - mat_time, e, e, e, 0.25 * vec3(0.,0.33,0.66));
       // col *= n.y;
    } else {
       // col = textureCube(mat_cube_1,rd).rgb;
    }

    //col.r =0.02;

    col = pow(col, vec3(.4545));    // gamma correction

    out_color = vec4(col,1.0);

    if (mat_background) {
        if (mat_luma(out_color) < (0.02 * mat_back_thresh)) {
            out_color = mat_back_color;
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
