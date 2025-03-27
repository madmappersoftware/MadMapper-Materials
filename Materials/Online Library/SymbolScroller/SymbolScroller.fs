/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#61601.0",
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
        "Label": "Rotate",
        "NAME": "rotate",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": -0.8
    },

    {
        "Label": "Shift",
        "NAME": "shift",
        "TYPE": "float",
        "MIN": -3.0,
        "MAX": 3.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Subdivisions",
        "NAME": "maxSubdivisions",
        "TYPE": "int",
        "MIN": 1,
        "MAX": 5,
        "DEFAULT": 3
    },

    {
        "Label": "Dims",
        "NAME": "dims_input",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 2.0
    },

    {
        "Label": "Variation",
        "NAME": "variation",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.3
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
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

float random2d(vec2 n) {
    return fract(sin(dot(n, vec2(129.9898, 4.1414))) * 2398.5453);
}

vec2 getCellIJ(vec2 uv, float gridDims){
    return floor(uv * gridDims)/ gridDims;
}

vec2 rotate2D(vec2 position, float theta)
{
    mat2 m = mat2( cos(theta), -sin(theta), sin(theta), cos(theta) );
    return m * position;
}

//from https://github.com/keijiro/ShaderSketches/blob/master/Text.glsl
float letter(vec2 coord, float size)
{
    vec2 gp = floor(coord / size * 7.); // global
    vec2 rp = floor(fract(coord / size) * 7.); // repeated
    vec2 odd = fract(rp * 0.5) * 2.;
    float rnd = random2d(gp);
    float c = max(odd.x, odd.y) * step(0.5, rnd); // random lines
    c += min(odd.x, odd.y); // fill corner and center points
    c *= rp.x * (6. - rp.x); // cropping
    c *= rp.y * (6. - rp.y);
    return clamp(c, 0., 1.);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

     float t = iTime;
     float scrollSpeed = 0.3;
     // float dims = 2.0;

     float dims = dims_input;
     // const int maxSubdivisions = 3;

     // uv = rotate2D(uv,rotate*PI/12.0);

     uv = rotate2D(uv,rotate*PI);

     uv.y -= shift + iTime * scrollSpeed;

     float cellRand;
     vec2 ij;

        for(int i = 0; i <= maxSubdivisions; i++) {
         ij = getCellIJ(uv, dims);
         cellRand = random2d(ij);
         dims *= 2.0;
         //decide whether to subdivide cells again
         float cellRand2 = random2d(ij + 454.4543);
         if (cellRand2 > variation){
            break;
         }
     }

     //draw letters
     float b = letter(uv, 1.0 / (dims));

     //fade in
     float scrollPos = iTime*scrollSpeed + 0.5;
     float showPos = -ij.y + cellRand;
     float fade = smoothstep(showPos ,showPos + 0.05, scrollPos );
     b *= fade;

     //hide some
     //if (cellRand < 0.1) b = 0.0;

     return vec4(vec3(b), 1.0);




}