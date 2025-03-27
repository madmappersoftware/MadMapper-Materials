/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Plento, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/4dcfW2",

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
            "LABEL": "Shape/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Shape/Distort 1",
            "NAME": "mat_distort1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Distort 2",
            "NAME": "mat_distort2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Distort 3",
            "NAME": "mat_distort3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Thickness",
            "NAME": "mat_thickness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Hole Size",
            "NAME": "mat_hole_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 16,
            "MAX": 128,
            "DEFAULT": 128
        },

        {
            "LABEL": "Spin/Speed",
            "NAME": "mat_spin_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "Label": "Spin/Offset Scale",
            "NAME": "mat_spin_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Spin/Restart",
            "NAME": "mat_spin_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Move/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Move/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Move/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Move/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Move/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Move/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Move/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },




        {
            "LABEL": "Color/Fog",
            "NAME": "mat_fog_alpha",
            "TYPE": "bool",
            "DEFAULT": 1,
            "FLAGS": "button"
        },

        {
            "LABEL": "Color/Front Color",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                0.9,
                0.9,
                0.9,
                1.0
            ]
        },
        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                1.0,
                1.0,
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
        {
            "NAME": "mat_spin_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_spin_speed",
                "speed_curve":2,
                "reverse": "mat_spin_reverse",
                "strob" : "mat_spin_strob",
                "reset": "mat_spin_restart",
                "bpm_sync": "mat_spin_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;
float mat_spin_time = mat_spin_time_source - mat_spin_offset * 8. * mat_spin_offset_scale;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
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
    uv *= mat_scale * 2.5;

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


#define NEAR_CLIPPING_PLANE 0.1
#define FAR_CLIPPING_PLANE 100.0
#define MAX_MARCH_STEPS mat_iterations
#define EPSILON 0.01
#define DISTANCE_BIAS 0.7

// Change some parameters here!!
#define SPIN_SPEED 0.6
#define MOVE_SPEED 2.5

#define BEAM_WIDTH 0.4 * mat_hole_size


mat2 rotmat(float a) {
    return mat2(cos(a), -sin(a), sin(a), cos(a));
}



// distance functions: http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
float opS( float d1, float d2 ) {return max(-d1,d2);}

float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float opU( float d1, float d2 ) { return min(d1,d2);}



float sdBuilding(vec3 p) //my function that describes the "building" structure before its bent
{

    vec3 b = vec3(0.0, 2.0, 0.0);
    vec3 b2 = vec3(1.1, 1.1, 1.1);

    vec3 translate = vec3(0.0, 0.0, 16.0);

    vec3 pos = p - translate;
    vec3 pos2 = pos;
    vec3 pos3 = pos;

    //rotate
    pos.xz *= rotmat(mat_spin_time * SPIN_SPEED);
    pos2.xz *= rotmat(mat_spin_time * SPIN_SPEED);
    pos3.xz *= rotmat(mat_spin_time * SPIN_SPEED);

    // translate down
    pos.y += mat_time * MOVE_SPEED;
    pos2.y += mat_time * MOVE_SPEED;
    pos3.y += mat_time * MOVE_SPEED;

    pos = mod(pos, b) - 0.5 * b;
    float distance_1 = sdBox(pos, vec3(5.5 * mat_thickness, 1.0, 3.5 * mat_thickness)); // visible box

    pos2 = mod(pos2, b2) - 0.5 * b2;
    float distance_2 = sdBox(pos2, vec3(1.5, BEAM_WIDTH, BEAM_WIDTH)); // subtracting block

    pos3 = mod(pos3, b2) - 0.5 * b2;
    float distance_3 = sdBox(pos3, vec3(BEAM_WIDTH, BEAM_WIDTH, 1.5)); //subtracting block

    float distance_4 = opU(distance_2, distance_3); // union the two subtracting blocks

    float distance_5 = opS(distance_4, distance_1); // final


    return distance_5;
}

float Bend( vec3 p ) // modified version of the bend function from Iq's website
{
    float c = cos( length(p) * 0.07 * mat_distort1) ;
    float s = sin( length(p) * 0.07 * mat_distort2);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xy,p.z) * mat_distort3;

    return sdBuilding(q);
}

vec2 sdScene(vec3 position) // my scene
{

    vec3 translate = vec3(0.0, 0.0, 16.0) * mat_fov;

    vec3 pos = position - translate;

    float mat_id = 1.0;

    float distance_5 = Bend(pos);

    return vec2 (distance_5, mat_id);

}

vec2 raymarch(vec3 position, vec3 direction) // MARCH
{
    float depth = NEAR_CLIPPING_PLANE;

    for(int i = 0; i < MAX_MARCH_STEPS; i++)
    {
        vec2 result = sdScene(position + direction * depth);

        if(result.x < EPSILON)
        {
            return vec2(depth, result.y);
        }

        depth += result.x * DISTANCE_BIAS;

        if(depth > FAR_CLIPPING_PLANE)
            break;
    }
    return vec2(FAR_CLIPPING_PLANE, 0.0);
}


vec3 normal(vec3 ray_hit_position, float smoothness) //Thanks MacSlow
{
    vec3 n;
    vec2 dn = vec2(smoothness, 0.0);
    float d = sdScene(ray_hit_position).x;
    n.x = sdScene(ray_hit_position + dn.xyy).x - d;
    n.y = sdScene(ray_hit_position + dn.yxy).x - d;
    n.z = sdScene(ray_hit_position + dn.yyx).x - d;
    return normalize(n);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    vec3 camera = vec3(0.0, 0.0, 0.0);

    vec3 ray_direction = normalize(vec3(uv, 2.0));

    vec2 result = raymarch(camera, ray_direction);

    vec3 materialColor = vec3(0.0, 0.0, 0.0);

    if(result.x != FAR_CLIPPING_PLANE)
        materialColor = mat_front_color.rgb;


    float fog = pow(0.4 / (0.4 + result.x), 0.3);


    vec3 intersection = camera + ray_direction * result.x;
    vec3 nrml = normal(intersection, 0.001); // get normals

    vec3 light_dir = normalize(vec3(0.1, 0.1, -1.0));

    float diffuse = dot(light_dir, nrml);

    diffuse = max(0.3, diffuse);

    vec3 light_color = vec3(1.6, 1.2, 0.7) * 2.55;
    vec3 ambient_color = vec3(0.2, 0.45, 0.5) * 1.6;

    vec3 diffuseLit = materialColor * (diffuse * light_color + ambient_color); // final color for scene object

    if(result.x == FAR_CLIPPING_PLANE)
    {
        out_color = mat_back_color;
    }
    else
    {
        if (mat_fog_alpha) {
             out_color = vec4(diffuseLit, 1.0) * fog;
        } else {
            out_color = vec4(diffuseLit * fog, 1.0);
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
