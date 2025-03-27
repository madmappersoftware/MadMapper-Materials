/*{
    "CREDIT": "jj99, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/WdccDj",

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
            "LABEL": "Waves/Scale",
            "NAME": "mat_wave_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Factor 1",
            "NAME": "mat_factor_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Factor 2",
            "NAME": "mat_factor_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Factor 3",
            "NAME": "mat_factor_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },





        {
            "LABEL": "Waves/Distort 1",
            "NAME": "mat_distort_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Distort 2",
            "NAME": "mat_distort_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Waves/Distort 3",
            "NAME": "mat_distort_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Distort 4",
            "NAME": "mat_distort_4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Waves/Height",
            "NAME": "mat_height",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Waves/Position",
            "NAME": "mat_wave_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Waves/Distance",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": -8.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "LABEL": "Lighting/Position",
            "NAME": "mat_light_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Lighting/Height",
            "NAME": "mat_light_height",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
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
            "LABEL": "Color/Tint",
            "NAME": "mat_tint",
            "TYPE": "color",
            "DEFAULT": [
                0.11,
                0.66,
                1.0,
                1.0
            ]
        },

        {
            "LABEL": "Color/Gamma",
            "NAME": "mat_gamma",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
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


// Fork of "18 - Sea of Sinewaves" by Krabcode. https://shadertoy.com/view/3ddcz2
// 2020-10-02 16:01:39
// forked again from https://www.shadertoy.com/view/WstcDB


// Based on Ray Marching for Dummies!"
// by Martijn Steinrucken aka BigWings/CountFrolic - 2018
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// https://www.shadertoy.com/view/XlGBW3


const int MAX_STEPS = 1;
const float MAX_DIST = 1.;
const float SURF_DIST = 0.0001;
const float NORMAL_DIST = 0.001;
float SHININESS = 10 / pow(mat_power, 2.);
const float DOWNSTEP = 0.1;
// const float PI = 3.14159;

vec2 uv;
vec2 lightOffset;
vec3 lightPos;

float rayLength;
float closestDist;
float hitDist;
vec3 hit;
vec3 intersect;
vec2 m;

mat2 rotate(float a){
  return mat2(cos(a), -sin(a), sin(a), cos(a));
}


float wave(vec2 p) {

    p *= mat_wave_scale;
    float v = sin(p.x*0.7*mat_factor_1 + sin(p.y*2.2*mat_factor_2) + sin(p.y * .43*mat_factor_3));
    return v*v;
}

const mat2 rot = mat2(0.5, 0.86, -0.86, 0.5);

float get(vec2 p,float t)
{
  float v = 0.0;//abs(sin(p.x+p.y*1.4))*0.1;
  v += wave(p) * mat_distort_1;
  p.y += t;

  // float shift_x = t * mat_drift * cos(2.*PI * mat_angle/360.0);
  // float shift_y = t * mat_drift * sin(2.*PI * mat_angle/360.0);
  // // p += vec2(shift_x, shift_y);

  p *= rot;
  v += wave(p) * mat_distort_2;
  p.y += t * .17;   //0.17
  p *= rot;
  v += wave(p) * mat_distort_3;
  v+=pow(abs(sin(p.x+v)),2.0);

  v = abs(1.5 - v) * mat_distort_4;
  return v;
}

float sdf(vec3 p){
    float v = get(p.xy*6.0,mat_time)*0.1;
        v = smoothstep(0.02,3.5,v)*2.5 * mat_height;

    return p.z+v;
}

float rayMarch(vec3 ro, vec3 rd)
{
    float dO=0.;
    for(int i=0; i<MAX_STEPS; i++) {
        vec3 p = ro + rd*dO;
        float dS = sdf(p);
        closestDist = min(dS, closestDist);
        dO += dS*DOWNSTEP;
        if(dO>MAX_DIST || dS<SURF_DIST) break;
    }
    return dO;
}

vec3 normal(vec3 p)
{
    float d = sdf(p);
    vec2 e = vec2(NORMAL_DIST, 0);
    vec3 n = d - vec3(
        sdf(p-e.xyy),
        sdf(p-e.yxy),
        sdf(p-e.yyx));
    return normalize(n);
}

float diffuseLight(vec3 p, vec3 normal)
{
    vec3 l = normalize(lightPos-p);
    float dif = clamp(dot(normal, l), 0., 1.);
    float d = rayMarch(p+normal*SURF_DIST*2., l);
    if(d<length(lightPos-p)){ dif *= .1; }
    return dif;
}


float specularLight(vec3 p, vec3 rayDir, vec3 normal) {
    vec3 lightDir = normalize(p-lightPos);
    vec3 reflectionDirection = reflect(-lightDir, normal);
    float specularAngle = max(dot(reflectionDirection, rayDir), 0.);
    return pow(specularAngle, SHININESS);
}

float render(vec2 uv)
{

    vec2 wave_pos = mat_wave_pos;
    wave_pos += vec2(0.5);
    wave_pos.x = 1.-wave_pos.x;
    wave_pos -= vec2(0.5);

    vec3 rayOrigin = vec3(uv+vec2(0. + wave_pos.x, 0.25 + wave_pos.y), 0. + mat_dist);


    // vec3 rayOrigin = vec3(uv+vec2(0., 0.25), 0.);
    vec3 rayDir = normalize(vec3(uv.x, uv.y, 1.));
    hitDist = rayMarch(rayOrigin, rayDir);
    hit = rayOrigin + rayDir * hitDist;
    vec3 normal = normal(hit);
    float diff = diffuseLight(hit, normal);
    float spec = specularLight(hit, rayDir, normal);
    return .5*diff + .5*spec;
}

float aaRender(vec2 uv){
    // vec2 third = vec2(1./RENDERSIZE.x, 1./RENDERSIZE.y) / 3.0;
    vec2 third = vec2(1./(1000.), 1./(1000.)) / 3.0;
    vec2 mult = vec2(1, -1);
    float c1 = render(uv+third*mult.xx);
    float c2 = render(uv+third*mult.xy);
    float c3 = render(uv+third*mult.yx);
    float c4 = render(uv+third*mult.yy);
    return (c1+c2+c3+c4) / 4.;
}

vec3 gammaCorrection(vec3 rgb){
    float gamma = 2.2 * mat_gamma;
    rgb = smoothstep(0., 1., rgb);
    return pow(max(rgb, 0.), vec3(1.0/gamma));
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

    vec2 light_pos = mat_light_pos;
    light_pos += vec2(0.5);
    light_pos.y = 1.-light_pos.y;
    light_pos *= 4.;
    light_pos -= vec2(0.5);


    // lightPos = vec3(0.,0.,-16.0);
    // lightPos = vec3(0.,5.0,-16.0);
    lightPos = vec3(0. + light_pos.x,5.0 + light_pos.y,-16.0 + mat_light_height * 2.);

    float lit = render(uv);
    lit *= smoothstep(MAX_DIST*.15, MAX_DIST*.05, hitDist);
    // vec3 col = vec3(0.2,1.2,1.8)*lit;

    // vec3 col = 1.8 * vec3(0.11,0.66,1.)*lit;
    vec3 col = 1.8 * mat_tint.rgb * lit;

    out_color = vec4(gammaCorrection(col),1.0);
    // out_color = vec4(col,1.0);

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
