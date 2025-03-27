/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS": [
        {
            "Label": "Thickness",
            "NAME": "thickness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.5,
            "DEFAULT": 0.033
        },
        {
            "Label": "Square Size",
            "NAME": "size",
            "TYPE": "float",
            "MIN": 0.15,
            "MAX": 0.5,
            "DEFAULT": 0.2
        },
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#40857.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

float iTime = mat_time - mat_offset;
//#extension GL_OES_standard_derivatives : enable


float rectangle(vec4 borders, vec2 coords){
    vec4 coordVals = vec4(coords.x, 1. - coords.x, 1. - coords.y, coords.y);//left, right, up, down
    vec4 vals = step(borders, coordVals);
    float val = (vals.x * vals.y * vals.z * vals.w);

    return val;
}

float rectangleOutline(vec4 borders, vec2 coords, float thickness){
    vec4 realBorders = vec4(borders.x  + thickness, borders.y + thickness,
                            borders.z + thickness, borders.w + thickness);

    return abs(rectangle(borders, coords) * (rectangle(realBorders, coords) - 1.));
}

vec2 tile(vec2 coords, float zoom, bool brick){ //tiles the initial picture if set to the coordinates
    coords *= zoom;
    float t = iTime * .5;
    if (brick){
        if( fract(t)>0.5 ){
            if (fract( coords.y * 0.5) > 0.5){
                coords.x += fract(t)*2.0;
            } else {
                coords.x -= fract(t)*2.0;
            }
        } else {
            if (fract( coords.x * 0.5) > 0.5){
                coords.y += fract(t)*2.0;
            } else {
                coords.y -= fract(t)*2.0;
            }
        }
    }
    return fract(coords);
}
vec2 rotate2D(vec2 coords, float angle){ //rotates frame if set to coordinates
    coords -= 0.500;
    coords =  mat2(cos(angle),-sin(angle),
                sin(angle),cos(angle)) * coords;
    coords += 0.5;
    return coords;
}


vec4 materialColorForPixel( vec2 texCoord ) {
    // vec2 st = gl_FragCoord.xy/RENDERSIZE.xy;

    vec2 st = texCoord;

    st *= mat_zoom;

    st = tile(st, 5., true);
    st = rotate2D(st, iTime);

    float t = abs(sin(iTime));
    float c = rectangleOutline(vec4(size), st, thickness);

    return vec4(c, c, c, 1.0);

    // return vec4(0.,0.0, c ,1.0);
}