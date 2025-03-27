/*{
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#49867.1",

    "VSN": "1.1",

    "INPUTS": [



        {
            "LABEL": "Retro Lava/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Retro Lava/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Retro Lava/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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

float mat_time = mat_time_source - mat_offset;

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

const float EPSILON = 0.00005;

float SoftMax(float a, float b, float k)
{
    return log(exp(k*a)+exp(k*b))/k;
}

float SoftMin(float a, float b, float k)
{
    return -(log(exp(k*-a)+exp(k*-b))/k);
}

struct Camera
{
    vec3 position;
    vec3 dir;
    vec3 up;
    vec3 rayDir;
};

struct MapResult
{
    float dist;
    vec3 color;
};

struct MarchResult
{
    vec3 position;
    float dist;
    vec3 color;
};

float smoothcurve(float f) {
    return 0.5*(1.0+cos(3.14*f));
}

mat4 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    return mat4(oc * axis.x * axis.x + c, oc * axis.x * axis.y - axis.z * s, oc * axis.z * axis.x + axis.y * s, 0.0,
            oc * axis.x * axis.y + axis.z * s, oc * axis.y * axis.y + c, oc * axis.y * axis.z - axis.x * s, 0.0,
            oc * axis.z * axis.x - axis.y * s, oc * axis.y * axis.z + axis.x * s, oc * axis.z * axis.z + c, 0.0,
            0.0, 0.0, 0.0, 1.0);
}

vec3 opCheapBend(vec3 p, float x, float y)
{
    float c = cos(x*p.y);
    float s = sin(y*p.y);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xy,p.z);
    return q;
}

vec3 opTwist(vec3 p, float x, float y)
{
    float c = cos(x*p.y);
    float s = sin(y*p.y);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xz,p.y);
    return q;
}

MapResult map_cube(vec3 position)
{
    MapResult result;


    float sphere = length(position) - 1.2;
    float mat_time2 = mat_time - position.y;


    float c = 10.0;


    position = (rotationMatrix(vec3(0,0,1), mat_time2) * vec4(position, 1.0)).xyz;


    position = (rotationMatrix(vec3(0,1,0), mat_time2) * vec4(position, 1.0)).xyz;
    float cube = length(max(abs(position) - vec3(0.5), 0.0)) - 0.1;

    position = (rotationMatrix(vec3(1,0,0), mat_time2) * vec4(position, 1.0)).xyz;


    vec2 q = vec2(length(position.xz) - 2.5, position.y);
    float torus= length(q) - 0.2;


    float d = SoftMax(cube, -sphere, 7.5);
    //float d = max(cube, -sphere);

    vec3 color3 = vec3(.5, 0.0, 0.0);
    vec3 color2 = vec3(0.0, 0.5, 0.0);
    vec3 color1 = vec3(0.0, 0.0, 0.5);

    d =+ SoftMin(d, torus, 1.);

    result.color = mix(color1, mix(color3, color2, cube), torus) * 4.;

    result.dist = d;
    return result;
}

MapResult map_torus(vec3 position)
{
    MapResult result;
    result.color = vec3(0.0, 0.5, 0.8);

    position = (rotationMatrix(vec3(0,0,1), mat_time) * vec4(position, 1.0)).xyz;
    position = (rotationMatrix(vec3(0,1,0), mat_time) * vec4(position, 1.0)).xyz;
    position = (rotationMatrix(vec3(1,0,0), mat_time) * vec4(position, 1.0)).xyz;

    vec2 q = vec2(length(position.xz) - 2.5, position.y);
    result.dist = length(q) - 0.2;

    return result;
}

MapResult map(vec3 position)
{
    MapResult result;

    MapResult cube = map_cube(position);


    result = cube;

    return result;
}

vec3 getColor(const in Camera cam, const in vec3 position, const in float dist, const in vec3 color)
{
    vec3 eps = vec3(EPSILON, 0, 0);

    vec3 normal=normalize(
           vec3(dist - map(position-eps.xyy).dist,
            dist - map(position-eps.yxy).dist,
            dist - map(position-eps.yyx).dist));

    float lambert = dot(normal, -cam.rayDir);

    return lambert * color;
}

MarchResult raymarch(const in Camera cam)
{
    MarchResult result;
    result.color = vec3(0);

    const int MAX_ITERATIONS = 128;
    const float MAX_DEPTH = 52.0;

    float depth = 0.0;
    MapResult mapping;
    for(int i = 0; i < MAX_ITERATIONS; ++i)
    {
        result.position = cam.position + cam.rayDir * depth;
        mapping = map(result.position);

        if(mapping.dist <= EPSILON)
        {
            break;
        }

        depth += mapping.dist;

        if(depth > MAX_DEPTH)
        {
            break;
        }
    }

    result.dist = mapping.dist;

    if(depth < MAX_DEPTH)
        result.color = getColor(cam, result.position, result.dist, mapping.color);

    return result;
}

Camera getCamera(vec2 uv)
{
    Camera cam;
    cam.dir = vec3(0,0,0);
    float t = (mat_time+1.0) * 0.15;
    cam.position = vec3(sin(t + 1.0)*4.0, 4, cos(t)*4.0);
    cam.up = vec3(0,1,0);
    vec3 forward = normalize(cam.dir - cam.position);
    vec3 left = cross(forward, cam.up);
    cam.up = cross(left, forward);

    vec3 screenOrigin = (cam.position+forward);
    vec2 screenPos = uv;
    float screenAspectRatio = 1.; //RENDERSIZE.x/RENDERSIZE.y;
    vec3 screenHit = screenOrigin + screenPos.x * left * screenAspectRatio + screenPos.y * cam.up;
    cam.rayDir = normalize(screenHit-cam.position);

    // const float pi = 3.14159;
    if(true){ // `360` view from center
        cam.position = vec3(0);

        // float a1 = (RENDERSIZE.x-gl_FragCoord.x-1.)*pi*2./(RENDERSIZE.x-1.) + pi/2.;
        // float a2 = (RENDERSIZE.y-gl_FragCoord.y-1.)*pi/(RENDERSIZE.y-1.);

        float a1 = uv.x*PI;
        float a2 = uv.y*PI;

        cam.rayDir = vec3(cos(a1)*sin(a2), sin(a1)*sin(a2), cos(a2));

    }
    return cam;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv += vec2(0.5);


    Camera cam = getCamera(uv);
    MarchResult result = raymarch(cam);

    vec3 color = result.color; //AddMore(zOrder);

    out_color = vec4(color, 1.0);

    if (mat_luma(out_color) < 0.02) {
        out_color = mat_back_color;
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
