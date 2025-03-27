/*
{
    "CREDIT": "Jason Beyers",
    "CATEGORIES" : [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION" : "From http:\/\/glslsandbox.com\/e#51874.0",
    "TAGS": "converted_from_isf",
    "VSN": "1.2",
    "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
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
            "DEFAULT": 1.0
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
        }

    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "strob" : "mat_strob",
                "bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true
            }
        }
    ]

}
*/



//precision mediump float;

const float PI = 3.1415926535;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}



vec4 materialColorForPixel( vec2 texCoord ) {



    float adj_time = mat_time  - mat_offset * 100.;

    //adj_time = adjusted_time(adj_time, mat_decay, mat_release);

    vec2 coord = texCoord - vec2(0.5,0.5);

    coord *= 1000;

    coord *= mat_zoom;

    // vec2 coord = texCoord - vec2(RENDERSIZE.x * 0.5, RENDERSIZE.y * 0.5);
    coord *= 3.0;

    float phi = atan(coord.y, coord.x + 1e-6);
    phi = phi / PI * 0.5 + 0.5;
    float seg = floor(phi * 6.0);

    float theta = (seg + 0.5) / 6.0 * PI * 2.0;
    vec2 dir1 = vec2(cos(theta), sin(theta));
    vec2 dir2 = vec2(-dir1.y, dir1.x);

    float l = dot(dir1, coord);
    float w = sin(seg * 31.415926535) * 18.0 + 400.0;
    float prog = l / w + adj_time * .25;
    float idx = floor(prog);

    float phase = adj_time * 0.8;
    float th1 = fract(273.84937 * sin(idx * 54.67458 + floor(phase    )));
    float th2 = fract(273.84937 * sin(idx * 54.67458 + floor(phase + 1.0)));
    float thresh = mix(th1, th2, smoothstep(0.75, 1.0, fract(phase)));

    float l2 = dot(dir2, coord);
    float slide = fract(idx * 32.74853) * 200.0 * adj_time;
    float w2 = fract(idx * 39.721784) * 500.0;
    float prog2 = (l2 + slide) / w2;

    float c = clamp((fract(prog) - thresh) * w * 0.3, 0.0, 1.0);
    c *= clamp((fract(prog2) - 1.0 + thresh) * w2 * 0.3, 0.0, 1.0);
    c += clamp((fract(prog2) - 1.0 + thresh) * w2 * 0.3, 0.0, 1.0);

    vec3 col = vec3(c);

    if (mat_luma(col) > 0.02) {
        return mat_front_color;
    } else {
        return mat_back_color;
    }

    // return vec4(c, c, c, 1);
}