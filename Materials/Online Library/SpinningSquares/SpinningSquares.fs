/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#60162.0",
  "VSN": "1.0",
  "INPUTS" : [

    {
        "LABEL": "BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.5
    },
    {
        "Label": "Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "LABEL": "Scale/Animate Scale",
        "NAME": "mat_animate_scale",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scale/Shape",
        "NAME": "mat_scale_shape",
        "TYPE": "long",
        "VALUES": ["Linear","In","Out","Smooth"], "DEFAULT": "Smooth"
    },

    {
        "LABEL": "Scale/BPM Sync",
        "NAME": "mat_scale_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scale/Reverse",
        "NAME": "mat_scale_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scale/Speed",
        "NAME": "mat_scale_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Scale/Amount",
        "NAME": "mat_scale_amount",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 2.0
    },
    {
        "Label": "Scale/Range",
        "NAME": "mat_scale_range",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scale/Strob",
        "NAME": "mat_scale_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },




  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
        "TYPE": "animator",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "shape": "mat_shape",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_scale_time",
        "TYPE": "animator",
        "PARAMS": {
            "speed": "mat_scale_speed",
            "speed_curve":2,
            "strob" : "mat_scale_strob",
            "reverse": "mat_scale_reverse",
            "bpm_sync": "mat_scale_bpm_sync",
            "shape": "mat_scale_shape",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

// #include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

const float PI = 3.14159265;

vec2 rotate(vec2 v, float a) {
    float sinA = sin(a);
    float cosA = cos(a);
    return vec2(v.x * cosA - v.y * sinA, v.y * cosA + v.x * sinA);
}

float square(vec2 uv, float d) {
    return max(abs(uv.x), abs(uv.y)) - d;
}

float smootheststep(float edge0, float edge1, float x)
{
    x = clamp((x - edge0)/(edge1 - edge0), 0.0, 1.0) * 3.14159265;
    return 0.5 - (cos(x) * 0.5);
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    float scale_time = 0.;
    if (mat_animate_scale) {
        scale_time = mat_scale_time;
    } else {
        scale_time = 1.;
    }
    scale_time *= 2 * mat_scale_amount;

    uv *= 0.75 * mat_scale_amount + (mat_scale_range * scale_time);


    float blurAmount = -0.005;

    float period = 2.0;
    float btime = iTime / period;
    btime = mod(btime, 1.0);
    btime = smootheststep(0.0, 1.0, btime);

    vec4 out_color = vec4(0.0, 0.0, 0.0, 1.0);
    for (int i = 0; i < 9; i++) {
        float n = float(i);
        float size = 1.0 - n / 9.0;
        float rotateAmount = (n * 0.5 + 0.25) * PI * 2.0;
        out_color.rgb = mix(out_color.rgb, vec3(1.0), smoothstep(0.0, blurAmount, square(rotate(uv, -rotateAmount * btime), size)));
        float blackOffset = mix(1.0 / 4.0, 1.0 / 2.0, n / 9.0) / 9.0;
        out_color.rgb = mix(out_color.rgb, vec3(0.0), smoothstep(0.0, blurAmount, square(rotate(uv, -(rotateAmount + PI / 2.0) * btime), size - blackOffset)));
        out_color.gb *= 0.8;
        out_color.b *= 0.8-uv.y;
    }

    return out_color;




}