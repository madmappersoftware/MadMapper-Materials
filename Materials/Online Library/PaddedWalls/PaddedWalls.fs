/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [



    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Pad Scale",
        "NAME": "mat_pad_scale",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 8.0,
        "DEFAULT": 3.0
    },
    {
        "Label": "Pad Corners",
        "NAME": "mat_pad_corners",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 60.0,
        "DEFAULT": 33.0
    },
    {
        "Label": "Glow",
        "NAME": "mat_glow",
        "TYPE": "int",
        "MIN": 1,
        "MAX": 10,
        "DEFAULT": 2
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
  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
],
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#47945.0"
}
*/


#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

// #define SCALE 3.
// #define CORNER 22.

// vec3 hsv2rgb(vec3 c)
// {
//     vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
//     vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
//     return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
// }


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    vec2 position = uv;

    float s = sin(iTime*0.1), c = cos(iTime*0.1);
    mat2 rot = mat2( c, s, -s, c);
    position *= rot;


    //vec2 mousePos = vec2(-1., 1.) * 0.7;
    vec2 mousePos = vec2(sin(iTime)*0.2,cos(iTime)*0.2);//mouse-0.5;
    vec3 light = vec3((mousePos - position), 0.5);

    vec3 normal = normalize(vec3(tan(position.x * PI * mat_pad_scale), tan(position.y * PI * mat_pad_scale), mat_pad_corners));

    float bright = dot(normal, normalize(light));
    //bright = pow(bright, 1.);
    //bright *= step(length(position), 1.);

    vec3 color = vec3(0.125,0.025,0.125)* bright;

    vec3 heif = normalize(light + vec3(0., 0., 1.));

    vec3 spec = vec3(pow(dot(heif, normal), 128.));

    color += spec * 0.55;

    //gl_FragColor = vec4( vec3( color, color * 0.5, sin( color + iTime / 3.0 ) * 0.75 ), 1.0 );
    vec4 outColor = max(vec4(0.), vec4(color, 1.));

    for (float i = 0.; i < float(mat_glow); i++) {
        float y = position.y;
        float col = (0.125) / abs((y * 6. - sin(((position.x + iTime * 0.1) * 2. + i)*PI)));
        float mul = 0.5 + 0.5 * (sin(PI * (i + (-iTime*0.5 + position.x * 6.) * 0.5)));
        col *= mul;
        float hue = i / 2.;
        outColor.rgb += hsv2rgb(vec3(hue, 1. - mul*0.5, col));
    }

    return outColor;

}