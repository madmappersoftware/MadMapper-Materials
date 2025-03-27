/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "patriciogv, adapted by Jason Beyers",

    "DESCRIPTION": "Blob Cluster generator. From http:\/\/glslsandbox.com\/e#50556.0",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL" : "Center",
            "NAME" : "mat_center",
            "TYPE" : "point2D",
            "MAX" : [
                1.0,
                1.0
            ],
            "MIN" : [
                0.0,
                0.0
            ],
            "DEFAULT" : [
                0.5,
                0.5
            ]
        },

        {
            "LABEL": "Blob Cluster/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Blob Cluster/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.2
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
            "LABEL": "Color/Color",
            "NAME": "mat_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                0.25,
                0.25,
                1.0
            ]
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
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
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

vec2 random2( vec2 p ) {
    return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale * 10.;

    vec2 st = uv;
    // out_color = mat_back_color;

    // Tile the space
    vec2 i_st = floor(st);
    vec2 f_st = fract(st);

    float m_dist = 1.;  // minimun distance

    for (int y= -1; y <= 1; y++) {
        for (int x= -1; x <= 1; x++) {
            // Neighbor place in the grid
            vec2 neighbor = vec2(float(x),float(y));

            // Random position from current + neighbor place in the grid
            vec2 point = random2(i_st + neighbor)*(sin(mat_time*0.1));

      // Animate the point
            point = 0.5 + 0.5*sin(mat_time + 6.2831*point);

      // Vector between the pixel and the point
            vec2 diff = neighbor + point - f_st;

            // Distance to the point
            float dist = length(diff);

            // Keep the closer distance
            m_dist = min(m_dist, dist);
        }
    }

    vec2 pix = texCoord;

    pix -= vec2(0.5);
    pix /= mat_fill;
    pix += vec2(0.5);

    vec2 mousepos = vec2(mat_center.x, 1.-mat_center.y);
    // mousepos.x *= RENDERSIZE.x/RENDERSIZE.y;

    // Draw the min distance (distance field)
    float brightness = m_dist * pow(distance(mousepos, pix) * 10.0, 2.0);

    // color = vec3(brightness, brightness * 0.25, brightness * 0.25);

    // out_color = vec4(mat_color.r * brightness, mat_color.g * brightness * 0.25, mat_color.b * brightness * 0.25, mat_color.a);

    out_color = mat_color * brightness;
    out_color.a = mat_color.a;

    // Show isolines
    // out_color -= step(.7,abs(sin(27.0*m_dist)))*.5;

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;


    return out_color;
}
