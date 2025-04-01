/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "AntoineC, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/MsGczV",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "Hypnotic Gears/Count",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 16,
            "DEFAULT": 8
        },
        {
            "LABEL": "Hypnotic Gears/Sharpness",
            "NAME": "mat_quality",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Hypnotic Gears/Shadow",
            "NAME": "mat_shadow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Hypnotic Gears/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Hypnotic Gears/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Hypnotic Gears/Shift",
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
            "LABEL": "Color/Tint",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                0.95,
                0.7,
                0.2,
                1.0
            ]
        },
        {
            "LABEL": "Color/Background",
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
            "LABEL": "Color/Alpha",
            "NAME": "mat_alpha",
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

// --------[ Original ShaderToy begins here ]---------- //

// Inspired by:
//  http://cmdrkitten.tumblr.com/post/172173936860



struct Gear
{
    float t;            // Time
    float gearR;        // Gear radius
    float teethH;       // Teeth height
    float teethR;       // Teeth "roundness"
    float teethCount;   // Teeth count
    float diskR;        // Inner or outer border radius
    vec4 color;         // Color
};



float GearFunction(vec2 uv, Gear g)
{
    float r = length(uv);
    float a = atan(uv.y, uv.x);

    // Gear polar function:
    //  A sine squashed by a logistic function gives a convincing
    //  gear shape!
    float p = g.gearR-0.5*g.teethH +
              g.teethH/(1.0+exp(g.teethR*sin(g.t + g.teethCount*a)));

    float gear = r - p;
    float disk = r - g.diskR;

    return g.gearR > g.diskR ? max(-disk, gear) : max(disk, -gear);
}


float GearDe(vec2 uv, Gear g)
{
    // IQ's f/|Grad(f)| distance estimator:
    float f = GearFunction(uv, g);
    vec2 eps = vec2(0.0001, 0);
    vec2 grad = vec2(
        GearFunction(uv + eps.xy, g) - GearFunction(uv - eps.xy, g),
        GearFunction(uv + eps.yx, g) - GearFunction(uv - eps.yx, g)) / (2.0*eps.x);

    return (f)/length(grad);
}



float GearShadow(vec2 uv, Gear g)
{
    float r = length(uv+vec2(0.1));
    float de = r - g.diskR + 0.0*(g.diskR - g.gearR);
    float eps = 0.4*g.diskR;
    return smoothstep(eps, 0., abs(de)) * mat_shadow;
}


void DrawGear(inout vec4 color, vec2 uv, Gear g, float eps)
{
    float d = smoothstep(eps, -eps, GearDe(uv, g));
    float s = 1.0 - 0.7*GearShadow(uv, g);

    color = mix(s*color, g.color, d);

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

    float t = 0.5*mat_time;
    float eps = 2. / (mat_quality * 2000.);

    // Scene parameters;
    vec4 base = mat_front_color;

    Gear outer = Gear(0.0, 0.8, 0.08, 4.0, 32.0, 0.9, base);
    Gear inner = Gear(0.0, 0.4, 0.08, 4.0, 16.0, 0.3, base);
    float count = float(mat_iterations);

    // Draw inner gears back to front:
    vec4 color = mat_back_color;
    for(float i=0.0; i<count; i++)
    {
        t += 2.0*PI/count;
        inner.t = 16.0*t;
        inner.color = base*(0.35 + 0.6*i/(count-1.0));
        DrawGear(color, uv+0.4*vec2(cos(t),sin(t)), inner, eps);
    }

    // Draw outer gear:
    DrawGear(color, uv, outer, eps);

    // out_color = vec4(color,1.0);
    out_color = color;

    if (!mat_alpha) {
        out_color.a = 1.;
    }


    return out_color;
}
