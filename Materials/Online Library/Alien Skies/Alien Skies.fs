/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "gleurop, adapted by Jason Beyers",

    "DESCRIPTION": "Alien Skies generator. From https://www.shadertoy.com/view/Msl3Rs",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Alien Skies/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Alien Skies/Modifier",
            "NAME" : "mat_modifier",
            "TYPE" : "point2D",
            "MAX" : [1,1],
            "MIN" : [0,0],
            "DEFAULT" : [0.5,0.5]
        },
        {
            "LABEL": "Alien Skies/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 16,
            "MAX": 64,
            "DEFAULT": 32
        },


        {
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
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

float mat_time = mat_time_source - mat_offset;

//#define ALT_COLOR

float mat_iSphere(in vec3 ro, in vec3 rd, in vec4 sph) // From iq
{
    // a sphere centered at the origin has equation |xyz| = r
    // meaning, |xyz|^2 = r^2, meaning <xyz, xyz> = r^2
    // now, xyz = ro + t*rd, therefore |ro|^2+t^2 + 2<ro, rd> t - r^2 = 0
    // which is a quadratic equation, so

    vec3 oc = ro - sph.xyz;
    float b = dot(oc, rd);
    float c = dot(oc,oc) - sph.w*sph.w;
    float h = b*b - c;
    if(h <0.0) return -1.0; //no intersection

    //pick smaller one(i.e, close one)
    //not (-b+sqrt(h)) /2
    //Edited: actually pick larger one
    float t = (-b + sqrt(h));
    return t;
}

vec2 mat_rotate(vec2 v, float a) {
    return vec2(cos(a)*v.x + sin(a)*v.y, -sin(a)*v.x + cos(a)*v.y);
}

vec3 mat_hsv(in float h, in float s, in float v) {
    return mix(vec3(1.0), clamp((abs(fract(h + vec3(3, 2, 1) / 3.0) * 6.0 - 3.0) - 1.0), 0.0 , 1.0), s) * v;
}

vec3 mat_toSpherical(in vec3 c)
{
    float r = length(c);
    return vec3(r, acos(c.z/r), atan(c.y,c.x));
}

vec3 mat_toCartesian(in vec3 s)
{
    float sy = sin(s.y);
    return s.x * vec3(sy*cos(s.z), sy*sin(s.z), cos(s.y));
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    vec2 uv_orig = uv;
    uv *= mat_scale * 4.;

    vec3 ro = mat_toCartesian(vec3(1.0, mat_time*0.1, mat_time*0.1419+0.5));
    vec3 cd = normalize(-ro);
    vec3 u = normalize(vec3(-ro.z, 0, ro.x));
    vec3 v = cross(u, cd);
    vec3 rd = normalize(u*uv.x + v*uv.y + cd*exp(sin(mat_time*0.3)*0.5+0.5)*2.0);
    vec4 color = vec4(0);
    float it = 0.0;
    vec2 m = mat_modifier;
    // if (mat_modifier.x < 1.0)
    //     // Changing these to common fractions can yeild some interesting results
    //     // It's hard to do it accurately with the mouse
    //     m = vec2(0.67, 0.6);
    m *= 3.1415;
    mat2 r1 = mat2(cos(m.x),  sin(m.x),
                   -sin(m.x), cos(m.x));
    mat2 r2 = mat2(cos(m.y),  sin(m.y),
                   -sin(m.y), cos(m.y));

    for (int i = 0; i < mat_iterations; i++) {
        float d = mat_iSphere(ro, rd, vec4(0,0,0,1.0-float(i)/100.0));
        vec3 p = ro+rd*d;
        ro = p * 9.0;
        ro.xy *= r1;
        ro.yz *= r2;
        ro = abs(ro-1.0);
        rd = normalize(ro);
    #ifdef ALT_COLOR
            color += vec4(mat_hsv(float(i)/10.0, dot(p, rd), 1.0), 1.0);
    #else
            color += vec4(mat_hsv(float(i)/10.0+length(ro), dot(p, rd), 1.0), 1.0);
    #endif
        }
        color = sin(color)*0.5+0.5;
        out_color = color * min(2.0-length(uv_orig), 1.0);








    return out_color;
}
