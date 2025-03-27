/*{
    "CREDIT": "TinyTexel, adapted by Jason Beyers, modified by Simon Holden",
    "DESCRIPTION": "Glowing Dots generator.",
    "VSN": "1.0",
    "INPUTS": [
        {
            "LABEL": "Glowing Dots/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.25
        },
        {
            "LABEL": "Glowing Dots/Gamma",
            "NAME": "gamma_encode",
            "TYPE": "float",
            "MIN": 0.0001,
            "MAX": 10.0,
            "DEFAULT": 1.0
        },


 //       {
 //          "LABEL": "Glowing Dots/Dotsize",
 //          "NAME": "circle_size",
 //           "TYPE": "float",
 //           "MIN": 0.0,
 //           "MAX": 4.0,
 //           "DEFAULT": 1.0
 //       },


{
            "LABEL": "Glowing Dots/Falloff",
            "NAME": "falloff_scale",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 10.0,
            "DEFAULT": 1.0
        },
{
            "LABEL": "Glowing Dots/Focus",
            "NAME": "glow_scale",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 1.0,
            "DEFAULT": 0.2
        },
{
            "LABEL": "Glowing Dots/Sparsity",
            "NAME": "dot_sparsity",
            "TYPE": "float",
            "MIN": 0.0001,
            "MAX": 1.5,
            "DEFAULT": 0.5
        },


        {
            "LABEL": "Glowing Dots/Color",
            "NAME": "mat_color",
            "TYPE": "color",
            "DEFAULT": [
                0.63,
                0.24,
                0.0,
                1.0
            ]
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
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Animation/Strob",
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
                "speed_curve": 2,
                "reverse": "mat_reverse",
                "strob": "mat_strob",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm": true
            }
        }
    ]
}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 10.0;

#define clamp01(x) clamp(x, 0.0, 1.0)
#define mad(x, a, b) ((x) * (a) + (b))
#define rsqrt(x) inversesqrt(x)

vec3 GammaEncode(vec3 x) { return pow(x, vec3(1.0 / gamma_encode)); }

float SDFtoMask(float sdf)
{
    return sdf / length(vec2(dFdx(sdf), dFdy(sdf))) * 1.2;
}

float Hash(float v)
{
    return fract(sin(v) * 43758.5453);
}

float Hash(vec2 v)
{
    return Hash(v.y + v.x * 12.9898);
}

float Hash(vec3 v)
{
    return Hash(v.y + v.x * 12.9898 + v.z * 33.7311);
}

float Pow2(float v) { return v * v; }
float Pow3(float v) { return v * v * v; }
float Pow4(float v) { return Pow2(Pow2(v)); }
float Pow8(float v) { return Pow2(Pow4(v)); }
float Pow16(float v) { return Pow4(Pow4(v)); }
float Pow32(float v) { return Pow16(Pow2(v)); }

float SqrLen(vec2 v) { return dot(v, v); }
float SqrLen(vec3 v) { return dot(v, v); }
float SqrLen(vec4 v) { return dot(v, v); }

float EvalIntensityCurve(vec2 id, float time)
{
    time += Hash(id.yx * 1.733);

    float mat_time = floor(time);
    float fTime = fract(time);

    float h = Hash(vec3(id, mat_time));

    h *= h;


    float falloff = 1.0 - Pow2(fTime * 1.5 - 1.0);

    {
        float f = 100.0;
        float d = dot_sparsity / (exp2(f) - 1.0);

        falloff = mad(exp2(falloff * f), d, -d);
    }

    return h * falloff * falloff_scale;
}

float PlotDot(vec2 sp, vec2 dp, float dr)
{
    float v = length(sp - dp);

    float d = v - dr;
    d /= length(vec2(dFdx(v), dFdy(v)));
    d = clamp01(1.0 - d * 1.2);

    return d;
}

float EvalGlyph(vec2 uv, vec2 off, float time)
{
    vec2 iUV = floor(uv) + off;
    vec2 fUV = fract(uv) - off;

    vec2 fUV2 = fUV * 2.0 - 1.0;

    float distToCenter = length(fUV2);

    // Generate a random radius between 0.5 and 1.0
    float randomRadius = 0.5 + 0.5 * Hash(iUV);

    float gMask = distToCenter - randomRadius;
    gMask = SDFtoMask(gMask);
    gMask = clamp01(1.0 - gMask);

    gMask = PlotDot(fUV2, vec2(0.0), randomRadius);

    return EvalIntensityCurve(iUV.xy, time) * gMask;
}

float GlowKern1(float x, float s)
{
    float rx = sqrt(x);
    return (exp(-s * rx) * s) / (2.0 * rx);
}

float GlowKern2(float x, float s)
{
    float rx = sqrt(x);
    return s / (2.0 * Pow2(1.0 + s * rx) * rx);
}

float GlowKern3(float x, float s)
{
    return s / Pow3(1.0 + s * x);
}

float GlowKern4(float x, float c)
{
    return c * rsqrt(x * Pow3(x + c)) * .5;
}

float CurveU(float x, float u)
{
    return (u - u * x) / (u + x);
}

float EvalGlow3(vec2 uv, vec2 off, float time)
{
    vec2 iUV = floor(uv) + off;
    vec2 fUV = fract(uv) - off;

    vec2 fUV2 = fUV * 2.0 - 1.0;

    float dist2 = SqrLen(fUV2);

    float l = length(fUV2);
    l = max(0.0, l - 0.7);
    float glow = 0.0;
    glow = GlowKern3(l, glow_scale);

    glow = clamp01(glow);
    glow *= clamp01(1.0 - Pow2(l * 0.25)); // window

    return EvalIntensityCurve(iUV.xy, time) * glow;
}

vec3 EvalTile(vec2 uv, vec2 off, float time)
{
    vec2 iUV = floor(uv) + off;
    vec2 fUV = fract(uv) - off;

    vec3 blue1 = mat_color.rgb;

    float nTime = time * 50.0;
    float time2 = floor(nTime);
    float fTime = fract(nTime);

    float n0 = Hash(vec3(uv, time2));
    float n1 = Hash(vec3(uv, time2 + 1.0));

    float glyph = EvalGlyph(uv, off, time);

    float glow = 0.0;

    for (float i = -2.0; i <= 2.0; ++i)
    for (float j = -2.0; j <= 2.0; ++j)
    {
        glow += EvalGlow3(uv, off + vec2(i, j), time);
    }

    return vec3(mix(glow, glyph, 0)).xxx * blue1 * 32.0;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 50.0;

    float time = mat_time * 0.1;

    vec3 outCol = vec3(1.0, 1.0, 1.0);

    outCol = vec3(0.0);

    outCol = EvalTile(uv, vec2(0.0), time);

    out_color = vec4(GammaEncode(clamp01(outCol.xyz)), 1.0);

    return out_color;
}