/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "kishimisu, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/dsS3Dh",

    "VSN": "1.0",

    "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": "f735bee5b64ef98879dc618b016ecf7939a5756040c2cde21ccb15e69a6e1cfb.png"
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
            "LABEL": "Camera/Camera 1",
            "NAME": "mat_cam_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Camera/Cam 1 Scale",
            "NAME": "mat_cam_pos_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Camera/Camera 2",
            "NAME": "mat_cam2_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Camera/Cam 2 Scale",
            "NAME": "mat_cam2_pos_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Clouds/Position",
            "NAME": "mat_cloud_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },



        {
            "LABEL": "Clouds/Position Scale",
            "NAME": "mat_cloud_pos_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Clouds/Z Offset",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Clouds/Limit",
            "NAME": "mat_cloud_limit",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Clouds/Cycle",
            "NAME": "mat_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Clouds/Intensity",
            "NAME": "mat_light_intensity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Clouds/Glow",
            "NAME": "mat_glow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Clouds/Absorption",
            "NAME": "mat_absorption",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Clouds/Smoothness",
            "NAME": "mat_cloud_smooth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Clouds/Volume Step",
            "NAME": "mat_volume_step",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Moon/Size",
            "NAME": "mat_moon_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Moon/Brightness",
            "NAME": "mat_moon_brightness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Moon/Surface",
            "NAME": "mat_moon_surface",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "Label": "XY Motion/Direction",
            "NAME": "mat_direction",
            "TYPE": "float",
            "MIN": 0.,
            "MAX": 360.,
            "DEFAULT": 45.0
        },
        {
            "LABEL": "XY Motion/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "XY Motion/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "XY Motion/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "XY Motion/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "XY Motion/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "XY Motion/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "XY Motion/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Z Motion/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Z Motion/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Z Motion/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Z Motion/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Z Motion/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Z Motion/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Z Motion/Restart",
            "NAME": "mat_a2_restart",
            "TYPE": "event",
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
            "LABEL": "Color/Back Color",
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
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
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
            "LABEL": "Color/Thresh",
            "NAME": "mat_back_thresh",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Color/Sensitivity",
            "NAME": "mat_back_sensitivity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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

float mat_a1_time = (mat_a1_time_source - mat_a1_offset * 8. * mat_a1_offset_scale);
float mat_a2_time = (mat_a2_time_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 16.;

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


/* From kishimisu (original author)

   I got the idea to create this shader after observing the moon
   on a cloudy night a few days ago, and also because I really
   like to explore volume rendering right now.

   I started by calculating and marching real light samples
   but this decreased the performances a lot and I wanted it
   to be accessible, so I replaced it with a fake light which
   intensity is mapped to the distance from the camera.

   You can move the camera with the mouse to better see the perspective.
   To see the entire "scene", set BEHIND_THE_SCENE to 1 =)

   Also I tried to add stars in the background (line 123) but I got
   heavy flickering when moving the camera. Does someone know a proper
   way to add stars without having this visual artefact ?
*/

float CLOUDS_SMOOTHNESS = 1.5 * mat_cloud_smooth;
float LIGHT_INTENSITY = 20. * mat_light_intensity;
float ABSORPTION = 5.5 * mat_absorption;
float VOLUME_STEPS = 40. * mat_volume_step;

// TODO: play around with this setting
#define BEHIND_THE_SCENE  0

#define hash33(p) fract(sin( (p) * mat3( 127.1,311.7,74.7 , 269.5,183.3,246.1 , 113.5,271.9,124.6) ) * 43758.5453123)

mat2 rot(float a) {
    return mat2(cos(a), -sin(a), sin(a), cos(a));
}

// noise and fbm are from https://www.shadertoy.com/view/XtS3DD
float noise(in vec3 p) {
    vec3 ip = floor(p);
    vec3 fp = fract(p);
    fp = fp*fp*(3.0-2.0*fp);
    vec2 tap = (ip.xy+vec2(37.0,17.0)*ip.z) + fp.xy;
    vec2 rz = textureLod(iChannel0, (tap+0.5)/256.0, 0.0).yx;
    return mix(rz.x, rz.y, fp.z) * pow(mat_cycle, 1.5);
}

float fbm(in vec3 x) {
    float rz = 0.;
    float a = .35;
    for (int i = 0; i<4; i++) {
        rz += noise(x)*a;
        a*=.35;
        x*= 4.;
    }
    return (rz - .25);
}

vec2 boxIntersection(in vec3 ro, in vec3 rd) {
    vec3 rad = vec3(6., 6., 2.);
    vec3 m = 1.0/rd;
    vec3 n = m*ro;
    vec3 k = abs(m)*rad;
    vec3 t1 = -n - k;
    vec3 t2 = -n + k;
    float tN = max( max( t1.x, t1.y ), t1.z );
    float tF = min( min( t2.x, t2.y ), t2.z );
    if( tN>tF || tF<0.0) return vec2(-1.0); // no intersection
    return vec2( tN, tF );
}

void marchVolume(vec3 ro, vec3 rd, float near, float far, inout vec3 color) {
    vec3 vColor = vec3(0.);
    float visibility = 1.;

    float inside = far - near + hash33(ro).x*.01;
    float stepSize = inside / VOLUME_STEPS;

    float a1_time_x = mat_a1_time * cos(2.*PI * mat_direction/360.0);
    float a1_time_y = mat_a1_time * sin(2.*PI * mat_direction/360.0);

    vec2 cloud_pos = (mat_cloud_pos) * mat_cloud_pos_scale * 2.;
    cloud_pos += vec2(0.5);
    cloud_pos.x = 1.-cloud_pos.x;
    cloud_pos -= vec2(0.5);

    float i = 0.;

    for (float t = near; t <= far; t += stepSize) {
        vec3 p = ro + t*rd;

        float s = CLOUDS_SMOOTHNESS*.01;
        // float dens = smoothstep(-s, s, fbm(p + vec3(mat_a1_time*.055, mat_a1_time*.065, 1.+mat_a2_time*.02)))*.1;
        float dens = smoothstep(-s, s, fbm(p + vec3(cloud_pos.x + a1_time_x, cloud_pos.y + a1_time_y, 1.+mat_a2_time*.02)))*.1;

        float prev = visibility;
        visibility *= exp(-stepSize*dens*ABSORPTION);

        float absorption = prev - visibility;
        float light = smoothstep(2.5, 6.5, p.z); // fake light
        vColor += absorption * dens * light * LIGHT_INTENSITY;

        if (i++ > VOLUME_STEPS) break;
    }

    color = min(vColor, 1.) + color * visibility;
}

void initCamera(vec2 uv, inout vec3 ro, inout vec3 rd) {

    vec2 cam_pos = (mat_cam_pos) * mat_cam_pos_scale * 2.;
    cam_pos += vec2(0.5);
    cam_pos.x = 1.-cam_pos.x;
    cam_pos -= vec2(0.5);

    vec2 m = false ? vec2(.5) : cam_pos;

    ro = vec3(m.x-.5, m.y-.5, .01)*.8;

    #if BEHIND_THE_SCENE
    ro.x += 8.; ro.z -= 8.;
    #endif

    vec3 ro2 = ro;

    ro.z += 6.*mat_dist;

    vec3 f = normalize(vec3(0., 0., 1.) - ro2*.05),
         r = normalize(cross(vec3(0,1,0), f)),
         u = cross(f,r),
         c = ro+f,
         i = c + uv.x*r + uv.y*u;
    rd = normalize(i-ro); // camera direction
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec3 ro, rd;
    initCamera(uv, ro, rd);



    vec2 cam2_pos = (mat_cam2_pos) * mat_cam2_pos_scale * 2.;
    cam2_pos += vec2(0.5);
    cam2_pos.x = 1.-cam2_pos.x;
    cam2_pos -= vec2(0.5);

    rd.x += cam2_pos.x;
    rd.y += cam2_pos.y;

    // moon
    float moon = dot(rd, vec3(0., 0., 1.)) * mat_moon_size;
    // vec3 color = (mat_moon_brightness*.7-fbm(rd*10.)*3.*mat_moon_surface) * vec3(smoothstep(.995, .9955, moon));
    vec3 color = (mat_moon_brightness*.7-fbm(rd*10.)*3.*mat_moon_surface) * vec3(smoothstep(.995, .9955, moon));
    color += (1.25-color)*pow((moon+.1) * mat_glow * 0.75, 6.)*.25;

    // stars (removed due to flickering)
    vec3 h = hash33(vec3(floor(rd.xy*80.), 0.));
    float stars = length(fract(rd.xy*80. + h.xy*.75)-.5);
    // color += .006/stars * h.z;

    // clouds
    vec2 hit = boxIntersection(ro - vec3(0., 0., 4.), rd) * mat_cloud_limit;
    marchVolume(ro, rd, hit.x, hit.y, color);

    out_color = vec4(color, 1.);

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // General way to add transparency to any shader
    if (mat_back_mode == 1) {
        // Differentiate front & back colors using a hard cut w/ threshold
        if (mat_luma(out_color.rgb) < mat_back_thresh) {
            out_color = mat_back_color;
        } else {
            out_color = mat_front_color;
        }
    } else {
        // Differentiate front & back colors using the gradual mix based on luma + a threshold used as an offset
        out_color = mix(mat_back_color, mat_front_color, mat_luma(out_color.rgb) * mat_back_sensitivity + mat_back_thresh);
    }

    return out_color;
}
