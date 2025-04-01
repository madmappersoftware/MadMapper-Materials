/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "tdhooper, adapted by Jason Beyers",

    "DESCRIPTION": "Bubble Rings generator. From https:\/\/www.shadertoy.com\/view\/WdB3Dw",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Bubble Rings/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Bubble Rings/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 200.0,
            "DEFAULT": 82.0
        },
        {
            "LABEL": "Bubble Rings/Fudge Factor",
            "NAME": "mat_fudge_factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 0.8
        },
        {
            "LABEL": "Bubble Rings/Precision",
            "NAME": "mat_precision",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.01,
            "DEFAULT": 0.001
        },
        {
            "LABEL": "Bubble Rings/Max Dist",
            "NAME": "mat_max_dist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 40.0,
            "DEFAULT": 20.0
        },
        {
            "LABEL": "Bubble Rings/Surface",
            "NAME": "mat_surface",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Bubble Rings/Suction",
            "NAME": "mat_suction",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Bubble Rings/Mod A",
            "NAME": "mat_mod_a",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Bubble Rings/Mod B",
            "NAME": "mat_mod_b",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Bubble Rings/Mod C",
            "NAME": "mat_mod_c",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
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

void pR(inout vec2 p, float a) {
    p = cos(a)*p + sin(a)*vec2(p.y, -p.x);
}

float smax(float a, float b, float r) {
    vec2 u = max(vec2(r + a,r + b), vec2(0));
    return min(-r, max (a, b)) + length(u);
}


// --------------------------------------------------------
// Spectrum colour palette
// IQ https://www.shadertoy.com/view/ll2GD3
// --------------------------------------------------------

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d ) {
    return a + b*cos( 6.28318*(c*t+d) );
}

vec3 spectrum(float n) {
    return pal( n, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67) );
}


// --------------------------------------------------------
// Main SDF
// https://www.shadertoy.com/view/wsfGDS
// --------------------------------------------------------

vec4 inverseStereographic(vec3 p, out float k) {
    k = 2.0/(1.0+dot(p,p));
    return vec4(k*p,k-1.0);
}

float fTorus(vec4 p4) {
    float d1 = length(p4.xy) / length(p4.zw) - 1.;
    float d2 = length(p4.zw) / length(p4.xy) - 1.;
    float d = d1 < 0. ? -d1 : d2;
    d /= PI;
    return d;
}

float fixDistance(float d, float k) {
    float sn = sign(d);
    d = abs(d);
    d = d / k * 1.82;
    d += 1.;
    d = pow(d, .5);
    d -= 1.;
    d *= 5./3.;
    d *= sn;
    return d;
}



float map(vec3 p) {
    float k;
    vec4 p4 = inverseStereographic(p,k);

    pR(p4.zy, mat_time * -PI / 2.);
    pR(p4.xw, mat_time * -PI / 2.);

    // A thick walled clifford torus intersected with a sphere

    float d = fTorus(p4);
    d = abs(d);
    d -= .2 * mat_surface;
    d = fixDistance(d, k);
    d = smax(d, length(p) - 1.85, .2 * pow(mat_suction,3));

    return d;
}


// --------------------------------------------------------
// Rendering
// --------------------------------------------------------

mat3 calcLookAtMatrix(vec3 ro, vec3 ta, vec3 up) {
    vec3 ww = normalize(ta - ro);
    vec3 uu = normalize(cross(ww,up));
    vec3 vv = normalize(cross(uu,ww));
    return mat3(uu, vv, ww);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 2.;

    vec3 camPos = vec3(1.8, 5.5, -5.5) * 1.75;
    vec3 camTar = vec3(.0,0,.0);
    vec3 camUp = vec3(-1,0,-1.5);

    // camTa.y += mat_time;

    // camPos *= mat_cam_x;

    camPos.x += mat_mod_a * 2.;
    camPos.y += mat_mod_b * 2.;
    camPos.z += mat_mod_c * 2.;

    // camUp += mat_cam_x;
    // camUp.y += mat_cam_y * 10.;
    // camUp.z += mat_cam_z * 10.;


    mat3 camMat = calcLookAtMatrix(camPos, camTar, camUp);


    float focalLength = 5.;

    vec2 p = uv;

    vec3 rayDirection = normalize(camMat * vec3(p, focalLength));
    vec3 rayPosition = camPos;
    float rayLength = 0.;
    float distance = 0.;
    vec3 color = vec3(0);
    vec3 c;
    // Keep iteration count too low to pass through entire model,
    // giving the effect of fogged glass

    for (float i = 0.; i < mat_iterations; i++) {
        // Step a little slower so we can accumilate glow
        rayLength += max(mat_precision, abs(distance) * mat_fudge_factor);
        rayPosition = camPos + rayDirection * rayLength;
        distance = map(rayPosition);
        // Add a lot of light when we're really close to the surface
        c = vec3(max(0., .01 - abs(distance)) * .5);
        c *= vec3(1.4,2.1,1.7); // blue green tint
        // Accumilate some purple glow for every step
        c += vec3(.6,.25,.7) * mat_fudge_factor / 160.;
        c *= smoothstep(20., 7., length(rayPosition));
        // Fade out further away from the camera
        float rl = smoothstep(mat_max_dist, .1, rayLength);
        c *= rl;
        // Vary colour as we move through space
        c *= spectrum(rl * 6. - .6);
        color += c;
        if (rayLength > mat_max_dist) {
            break;
        }
    }
    // Tonemapping and gamma
    color = pow(color, vec3(1. / 1.8)) * 2.;
    color = pow(color, vec3(2.)) * 3.;
    color = pow(color, vec3(1. / 2.2));
    out_color = vec4(color, 1);







    return out_color;
}
