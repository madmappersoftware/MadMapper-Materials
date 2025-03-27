/*{
    "CREDIT": "Rrrrichard, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/7lffDj",

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
            "LABEL": "Wave/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 300,
            "DEFAULT": 300
        },
        {
            "LABEL": "Wave/Limit",
            "NAME": "mat_limit",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Wave/Orb Offset",
            "NAME": "mat_orb_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Wave/Height",
            "NAME": "mat_height",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Turbulence",
            "NAME": "mat_turbulence",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Bounce Freq",
            "NAME": "mat_bounce_freq",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Wave/Bounce Amp",
            "NAME": "mat_bounce_amp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Wave/Bounce Offset",
            "NAME": "mat_bounce_offset",
            "TYPE": "float",
            "MIN": 0.0,
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Background/Background",
            "NAME": "mat_back_light",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Background/Luma to Alpha",
            "NAME": "mat_alpha",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Background/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
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
            "NAME": "mat_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "reverse": "mat_reverse",
                "strob" : "mat_strob",
                "reset": "mat_restart",
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


    uv *= mat_scale * 0.75;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale * 0.25;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * (2*PI*(mat_rotate) / 360) + PI);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, PI + 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, PI + 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }



    return uv;
}



// ==========================================================
// Written by Rrrrichard(Zhehao Li)
// in a "2-hour shader challenge" on 2022/05/02
//
// Copyright Zhehao Li, 2022
// You cannot use this work in any commercial product,
// website or project. You cannot sell this Work
// and you cannot mint NFTs of it.
//
// ==========================================================

// #define MAX_STEPS 300
//#define MAX_DIST 10. --> amazing effect with the shape of SDF
#define MAX_DIST 1e10
#define EPS .001

float GetSceneDistance(vec3 point, out int obj)
{
    vec4 sphere = vec4(0, 1.5, 6, 1.); // (xyz, radius)
    vec4 sphere2 = vec4(0., 1.5, 6, 0.38);
    vec4 sphere3 = vec4(0., 1.5, 6, 0.1);
    vec4 sphere4 = vec4(0., 1.5, 6, 0.2);

    float rotate_r2 = 2.0;
    float rotate_r3 = 1.4;
    float rotate_r4 = 1.7;

    float bounce_frequen = 4. + mat_bounce_freq * 16.;
    sphere += vec4(0., 0.1 *sin(bounce_frequen * mat_time) + mat_bounce_offset * 2., 0., 0.) * pow(mat_bounce_amp,2.);

    sphere2 += vec4(rotate_r2 *sin(mat_time),
                    0.8*sin(mat_time),
                    rotate_r2 *cos(mat_time),
                    0.);

    float delta = 1.5 + mat_orb_offset * 4.;
    sphere3 += vec4(rotate_r3 *sin(mat_time+delta),
                    0.5*sin(mat_time+delta),
                    rotate_r3 *cos(mat_time+delta),
                    0.);
    sphere4 += vec4(rotate_r4 *sin(mat_time+2.*delta),
                    -0.6*sin(mat_time+2.*delta),
                    rotate_r4 *cos(mat_time+2.*delta),
                    0.);

    float sphere_dist = length(point - sphere.xyz)-sphere.w;
    float sphere_dist2 = length(point - sphere2.xyz)-sphere2.w;
    float sphere_dist3 = length(point - sphere3.xyz)-sphere3.w;
    float sphere_dist4 = length(point - sphere4.xyz)-sphere4.w;
    float ground_dist =  point.y;

    ground_dist /= pow(mat_height,1.25);

    float t = length(point.xz - sphere.xz);
    float t2 = length(point.xyz - sphere2.xyz) * mat_turbulence;
    float t3 = length(point.xyz - sphere3.xyz);
    float t4 = length(point.xyz - sphere4.xyz);
    bool is_background_obj = false;
    if (t < 15. * mat_limit)
    {
        /// Tricks to add sin wave surface, not precise distance, wrong with big magnitude
        float wave = 0.2 *pow(0.95, t)*  sin( 4. *pow(0.98, t) * t - bounce_frequen * mat_time);
        float wave2 = -10.  *pow(sphere2.w, 3.) * 0.15 *pow(0.8, t)*  sin(4. * t2);
        float wave3 = -5.  *pow(sphere3.w, 3.) *0.15 *pow(0.8, t)*  sin(4. * t3);
        float wave4 = -20.  *pow(sphere4.w, 3.)*0.15 *pow(0.9, t)*  sin(4. * t4);
        //ground_dist += (wave);
        ground_dist += (wave + wave2 + wave3 + wave4);
    }
    //else if (t <20.)
      //  ground_dist =  point.y;
    else
        is_background_obj = true;


    float d =
            min(
            min(
            min(
            min(sphere_dist, ground_dist),
            sphere_dist2),
            sphere_dist3),
            sphere_dist4);
    //float d = sphere_dist;

    float eps = 0.01;
    if( abs(sphere_dist - d) < eps)
        obj = 1;
    else if( abs(sphere_dist2 - d) < eps)
        obj = 2;
    else if( abs(sphere_dist3 - d) < eps)
        obj = 3;
    else if( abs(sphere_dist4 - d) < eps)
        obj = 4;
    else
        obj = 0;

    if(is_background_obj)
        obj = -1;

    return d;
}

float RayMarch(vec3 ray_origin, vec3 ray_dir, out int obj)
{
    float d = 0.;
    for(int i = 0; i < mat_iterations; i++)
    {
        vec3 p = ray_origin + ray_dir * d;
        float ds = GetSceneDistance(p, obj);
        d += ds;
        if(d > MAX_DIST || ds < EPS)
            break;  // hit object or out of scene
    }
    return d;
}

vec3 GetNormal(vec3 point)
{
    int obj_nouse;
    float d = GetSceneDistance(point, obj_nouse);
    vec2 e = vec2(0.01, 0);
    vec3 n = d - vec3(
        GetSceneDistance(point - e.xyy, obj_nouse),
        GetSceneDistance(point - e.yxy,  obj_nouse),
        GetSceneDistance(point - e.yyx, obj_nouse)
    );

    return normalize(n);
}

float smoothStep(float edge0, float edge1, float x)
{

    if (x < edge0)
        return 0.;
    if (x > edge1)
        return 1.;
    if(edge1 == edge0)
        return 0.;
    x = (x - edge0) / (edge1 - edge0);
    return x*x * (3.-2.*x);
}

vec3 GetLight(bool is_ground, vec3 point, vec3 light_pos, vec3 light_col, vec3 camera_pos)
{
    vec3 to_light = normalize(light_pos - point);
    vec3 normal = GetNormal(point);

    //======== 1. diffuse ==========
    bool in_shadow = false;
    float NdotL = dot(to_light, normal);
    float diffuse = NdotL > 0. ? 1. : 0.;
    if(is_ground)
    {
        float NdotL_smooth = smoothStep(0.4, 1.0, NdotL);
        diffuse = NdotL > 0.4 ? 1.0 : 0.5;
    }

    // Shoot a ray towards light
    int obj_nouse;
    float d = RayMarch(point+normal*2.*EPS, to_light, obj_nouse);

    // Shadow
    if (d < length(light_pos - point))
    {
        in_shadow = true;
        diffuse *= 0.;
    }

    // ======== 2. ambient ===========
    float ambient = 0.3;

    // ======== 3. specular ===========
    vec3 to_camera = normalize(camera_pos - point);
    vec3 half_vector = normalize(to_camera + to_light);
    float NdotH = dot(normal, half_vector);

    float glossiness = 20.;
    float specular = pow(NdotH, glossiness * glossiness);
    float specular_smooth = smoothStep(0.005, 0.01, specular);

    // ======== 4. rim light ===========
    float scale = 0.2;
    if (is_ground)
        scale = 0.3;
    float rimDot = pow(NdotL, scale)* (1. - clamp(dot(to_camera, normal), 0., 1.));
    float rimAmount = 0.616;
    float rim = 0.5 *  smoothStep(rimAmount - 0.01, rimAmount+0.01, rimDot);

    // ======== Final light ==========
    float intensity = 0.8;

    float sum = 0.;
    if(in_shadow)
        sum = (diffuse + ambient);
    else
        sum = (diffuse + ambient + specular_smooth + rim);

    vec3 light = intensity * sum * light_col ;

    return light;
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    vec3 ray_origin = vec3(0, 1.5, -1);
    vec3 ray_dir = normalize(vec3(uv.x, uv.y, 1.));

    // light
    vec3 light_pos = vec3(3, 8, -1);
    vec3 light_col = vec3(60.,179.,113.) / 255.;
    //vec3 ground_col = vec3(120.,19.,27.) / 255.;
    vec3 ground_col = vec3(105,26.,235.) / 255.;
    vec3 light_col2 = vec3(10.,149.,237.) / 255.;
    vec3 light_col3 = vec3(200.,100.,37.) / 255.;
    vec3 light_col4 = vec3(100.,149.,27.) / 255.;

    float spin = 5.;
    float range = 2.;
    vec3 light_pos2 = light_pos + range* vec3(sin(spin*mat_time), cos(spin *mat_time), cos(spin*mat_time));

    //
    int obj = 0;
    float d = RayMarch(ray_origin, ray_dir, obj);

    vec3 point = ray_origin + d * ray_dir;

    vec3 col;
    if(obj == 1)
        col = GetLight(false, point, light_pos2, light_col, ray_origin);
    else if(obj == 2)
        col = GetLight(false, point, light_pos2, light_col2, ray_origin);
    else if(obj == 3)
        col = GetLight(false, point, light_pos2, light_col3, ray_origin);
    else if(obj == 4)
        col = GetLight(false, point, light_pos2, light_col4, ray_origin);
    else if(obj==-1)
        col = ground_col * 0.1 * mat_back_light;
    else
        col = GetLight(true, point, light_pos, ground_col, ray_origin);

    col = pow(col, vec3(0.4545)); // Gamma correction

    // Output to screen
    out_color = vec4(col,1.0);

    if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color.a = 0.;
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
