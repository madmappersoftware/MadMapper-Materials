/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "stb, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/MlcXDl",

    "VSN": "1.0",

    "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": [
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919.jpg",
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919_1.jpg",
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919_2.jpg",
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919_3.jpg",
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919_4.jpg",
                "488bd40303a2e2b9a71987e48c66ef41f5e937174bf316d3ed0e86410784b919_5.jpg"
            ],
            "TYPE": "cube"
        },
        "iChannel1": {
            "NAME": "iChannel1",
            "PATH": [
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663.png",
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663_1.png",
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663_2.png",
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663_3.png",
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663_4.png",
                "550a8cce1bf403869fde66dddf6028dd171f1852f4a704a465e1b80d23955663_5.png"
            ],
            "TYPE": "cube"
        },
        "iChannel2": {
            "NAME": "iChannel2",
            "PATH": "8979352a182bde7c3c651ba2b2f4e0615de819585cc37b7175bcefbca15a6683.jpg"
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
            "LABEL": "Camera/Enable",
            "NAME": "mat_cam_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "Camera/Position",
            "NAME": "mat_cam_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Camera/Pos Scale",
            "NAME": "mat_cam_pos_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Camera/Zoom",
            "NAME": "mat_cam_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 3.0
        },


        {
            "LABEL": "Sphere/Ridge",
            "NAME": "mat_ridge",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Sphere/Cycle 1",
            "NAME": "mat_cycle1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Sphere/Cycle 2",
            "NAME": "mat_cycle2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
         {
            "LABEL": "Sphere/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Sphere/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 32,
            "DEFAULT": 32
        },
        {
            "LABEL": "Sphere/Detail",
            "NAME": "mat_detail",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Sky/Level",
            "NAME": "mat_sky",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Noise/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Noise/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Noise/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Noise/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Noise/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Noise/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Noise/Restart",
            "NAME": "mat_a1_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Orbit/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Orbit/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Orbit/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Orbit/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Orbit/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Orbit/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Orbit/Restart",
            "NAME": "mat_a2_restart",
            "TYPE": "event",
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

float mat_a1_time = (mat_a1_time_source - mat_a1_offset * 16. * mat_a1_offset_scale) * 8.;
float mat_a2_time = (mat_a2_time_source - mat_a2_offset * 16. * mat_a2_offset_scale) * 2.;

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

vec2 flipY(vec2 coord, float multiplier) {
    // Scale XY coord ranging from [-1,-1] to [1,1] from 2D user input
    // Then flip the Y axis
    vec2 new_coord = coord * multiplier;
    new_coord += vec2(0.5);
    new_coord.y = 1.-new_coord.y;
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


// const float Iters   = 32.0; // lower this if shader is running slowly
// const float Norm    = 0.13; // amt of influence map() has on surface normal
// const float IOR     = 1.3;  // index of refraction (1.0 = air)
// const float FRB     = 0.2;  // level of fake refraction blurring (keep it above 0.0)

float Norm = 0.13 * mat_ridge; // amt of influence map() has on surface normal
float IOR = 1.3 * mat_fill;  // index of refraction (1.0 = air)
const float FRB = 0.2;  // level of fake refraction blurring (keep it above 0.0)


float s, c;
#define rotate(p, a) mat2(c=cos(a), s=-sin(a), -s, c) * p

vec3 rotXY(in vec3 p, vec2 r) {
    p.yz = rotate(p.yz, r.x);
    p.xz = rotate(p.xz, r.y);
    return p;
}

// returns both near & far distances
vec2 isectSphere(vec3 p, vec3 d, vec3 sPos, float sRad) {
    vec2 ret;
    vec3 v = p - sPos;
    float r  = sRad;
    float dv = dot(d, v);
    float d2 = dot(d, d);
    float sq = dv*dv - d2 * (dot(v, v)-r*r);
    if(sq < 0.) {
        return vec2(-1.);
    }
    else {
        sq = sqrt(sq);
        float t1 = (-dv+sq)/d2;
        float t2 = (-dv-sq)/d2;
        return (t1<t2 ? vec2(t1, t2) : vec2(t2, t1));
    }
}

float map(in vec3 p) {
    vec3 o = vec3(-1., 0., 1.);
    float sph = dot(p-o.yxy, p-o.yxy);
    p = vec3(p.x, 0., p.z) / sph;

    float sc=1.3, f=0.;
    for(float i=0.; i<float(mat_iterations); i++) {
        p = abs(mod(p-1., 2.)-1.);
        p.xz -= .75 - mat_cycle1*.5*vec2(sin(.007145*mat_a1_time), cos(.005072*mat_a1_time));

        p.xz = sc * rotate(p.xz, 1.4+.01*mat_a1_time);

        f += length(p) / pow(sc, i+1.);
    }
    return 2. * abs(fract(2.*f)-.5 * mat_cycle2);
}

vec3 getNorm(vec3 p, float acc) {
    vec3 o = acc * vec3(-1., 0., 1.);
    return
        normalize(
            vec3(
                map(p+o.zyy)-map(p+o.xyy),
                map(p+o.yzy)-map(p+o.yxy),
                map(p+o.yyz)-map(p+o.yyx)
            )
        );
}

vec3 getSky(vec3 p) {
    return - .25 * p.y + 1.5 * mix(textureCube(iChannel0,p).rgb, textureCube(iChannel1,p).rgb, .7) * mat_sky;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    // vec2 res    = RENDERSIZE.xy;
    // vec2 uv     = (gl_FragCoord.xy-.5*res) / res.y;
    // vec2 mPos   = (iMouse.xy-.5*res) / res.y;

    vec2 res = vec2(10.) * pow(mat_detail + 1., 8.);

    vec2 mPos = 0.5*mat_cam_pos + vec2(0.5);

    // general-purpose vector
    vec3 o = vec3(-1., 0., 1.);

    // camera zoom
    float Zoom = 1.3;

    // zoom and pan with mouse
    if(mat_cam_enable) {
        // uv += 2. * (iMouse.xy/res-.5);
        uv += flipX(mat_cam_pos, mat_cam_pos_scale);
        Zoom *= mat_cam_zoom;
    }

    // set up camera ray
    vec3 rayBeg = 3. * o.yyx;
    vec3 rayDir = normalize(vec3(uv, Zoom));

    // camera rotation
    vec2 rot = .1 * vec2(2.+1.94*cos(.14*mat_a2_time), mat_a2_time);
    rayBeg = rotXY(rayBeg, rot);
    rayDir = rotXY(rayDir, rot);

    // trace sphere
    vec2 hitDists = isectSphere(rayBeg, rayDir, o.yyy, 1.);
    vec3 hit1 = rayBeg + rayDir * hitDists.x, hit2;

    vec3 outCol;

    // default sky color
    vec3 sky = getSky(-rayDir);

    // if ray intersects sphere...
    if(hitDists.x>0.) {

        // various variables
        float f1, f2, aoi, thk, iBl;
        vec3 norm, col1, col2, ref, sCol, iCol;
        // light position
        vec3 lPos = 3. * o.yzy;
        // interior color (volume)
        iCol = vec3(.2, .3, .425);


        //*** sphere exterior ***//

        // map() value; used for various tings
        f1 = max(0., map(hit1));

        // surface normal
        norm = normalize(hit1-Norm*getNorm(hit1, 2./res.y/Zoom));

        // solid color
        sCol = IMG_NORM_PIXEL(iChannel2,mod(vec2(.15*f1, 0.),1.0)).rgb;

        // initial color
        col1 = .1  * sCol;

        // diffuse
        col1 += .42 * sCol * mix(f1, 1., .5) * pow(max(0., 1.+.25*dot(norm, lPos)), 2.);
        col1 += .2*textureCube(iChannel1,-norm).rgb;

        // darken solid edges
        col1 *= mix(smoothstep(.5, .7, f1), 1., .75);

        // reflection color
        ref = getSky(reflect(-rayDir, norm));

        // angle of incidence (for blending reflection)
        aoi = pow(max(0., (1.+1.*dot(normalize(rayDir), norm))), 1.);


        //*** sphere interior ***//

        // new refracted ray
        rayBeg = hit1 + .01*norm;
        rayDir = refract(normalize(rayDir), norm, 1./IOR);
        hitDists = isectSphere(rayBeg, rayDir, vec3(0.), 1.);
        hit2 = rayBeg + rayDir * hitDists.y;

        // map() value; used for various tings
        f2 = max(0., map(hit2));

        // surface normal
        norm = normalize(hit2+Norm*getNorm(hit2, (80.*FRB)/res.y/Zoom));

        // solid color
        sCol = IMG_NORM_PIXEL(iChannel2,mod(vec2(.15*f2, 0.),1.0)).rgb;

        // initial color
        col2 = .1 * sCol;

        // diffuse
        col2 += .42 * sCol * mix(f2, 1., .5) * pow(max(0., 1.+.25*dot(norm, lPos)), 2.);
        col2 += .2 * textureCube(iChannel1,-norm).rgb;

        // darken solid edges
        col2 *= mix(smoothstep(.5, .7, f2), 1., .75);


        //*** final sphere color ***//

        // distance between ray-sphere intersections
        thk = distance(hit1, hit2);

        // for blending solid and transparent colors
        vec2 edge = .5 + vec2(.0, .01);

        // modified f1 for blending solid & transparent areas (exterior)
        f1 = clamp(smoothstep(edge.x, edge.y, 1.-f1), 0., 1.);

        // for fake refraction blurring of solid edges
        iBl = FRB * thk;

        // modified f2 for blending solid & transparent areas (interior)
        f2 = clamp(smoothstep(edge.x-iBl, edge.y+iBl, 1.-f2), 0., 1.);

        // final ray (exiting sphere)
        rayDir = refract(normalize(rayDir), -norm, IOR);

        // sky color from refracted ray
        sky = getSky(-rayDir);

        // blend interior color with refracted sky
        col2 = mix(col2, vec3(sky), f2);

        // blend col2 with interior color
        col2 = mix(col2, .7 * iCol, min(1., .4*thk));

        // blend exterior with interior, by f1
        outCol = mix(col1, col2, f1);

        // make surface reflection favor transparent areas
        ref = mix(outCol, ref, f1/2.+.2);

        // blend in reflection color, by aoi
        outCol = mix(outCol, ref, aoi);

    }
    // no sphere intersection, return sky color
    else
        outCol = sky;

    out_color = vec4(outCol, 1.);


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
