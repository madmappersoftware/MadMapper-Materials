/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [
        {
            "Label": "Distance",
            "NAME": "distance",
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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45587.3"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#ifndef PI
    #define PI 3.14159265359
#endif

//#extension GL_OES_standard_derivatives : enable
float iTime = mat_time - mat_offset * 10;

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec4 materialColorForPixel(vec2 texCoord) {

    float t = iTime * 0.1;

    // vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy ) - vec2(0.5, 0.0);
    vec2 position = texCoord;



    position = rotate2D(position, PI);


    position -= vec2(0.5, 0.0);

    // position *= mat_zoom;



     position /= -(position.y) + 0.8;

    // position.x = 1.0-position.x;
    //  position.y = 1.0-position.y;


     position *= 20.0;

     //

     position *= distance;
     vec2 horizon = position.xy;

         //position *= mat2(cos(-t), -sin(-t), sin(-t), cos(-t)) ;
      position.x =position.x+iTime*18.;


    vec3 color = vec3(2.-floor(1.+length(cos(position.xy)*2.0+2.*sin(iTime))));
         color *= horizon.y < 0.0 ? 0.0 : 1.0;
        horizon.y -= 5.+position.y;

        color += horizon.y > 0.0 ? 0.0 : 1.0;
        color *= pow(vec3(0.1, 0.7, 0.9), vec3(length(horizon.xy)) * 0.1);





    return vec4(color, 1.0 );

}
