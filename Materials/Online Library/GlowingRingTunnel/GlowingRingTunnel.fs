/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "http:\/\/glslsandbox.com\/e#51492.0, adapted by Jason Beyers",
  "DESCRIPTION" : "Ring tunnel.  From http:\/\/glslsandbox.com\/e#51492.0",
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
        "Label": "Iterations",
        "NAME": "iterations",
        "TYPE": "int",
        "MIN": 0,
        "MAX": 40,
        "DEFAULT": 10
    },
    {
        "Label": "Twist",
        "NAME": "twist",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Brightness",
        "NAME": "brightness_input",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Glow",
        "NAME": "glow_input",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Light Color",
        "NAME": "light_color",
        "TYPE": "color",
        "DEFAULT": [ 1.0, 0.8, 0.6, 1.0 ]
    },
    {
        "LABEL": "Color/Use Default Color",
        "NAME": "use_default_color",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Tunnel/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Tunnel/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Tunnel/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Tunnel/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Tunnel/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Twist/BPM Sync",
        "NAME": "mat_twist_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Twist/Reverse",
        "NAME": "mat_twist_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Twist/Speed",
        "NAME": "mat_twist_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Twist/Offset",
        "NAME": "mat_twist_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Twist/Strob",
        "NAME": "mat_twist_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
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
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_twist_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_twist_speed",
            "speed_curve":2,
            "strob" : "mat_twist_strob",
            "reverse": "mat_twist_reverse",
            "bpm_sync": "mat_twist_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time * 5. - mat_offset * 10;
float twist_time = mat_twist_time / 50. - mat_twist_offset * 10;

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 position = texCoord - vec2(0.5);
    position *= mat_zoom;



    // vec3 light_color = vec3(1.2,0.8,0.6);

    float t = iTime;

    // 256 angle steps
    float angle = atan(position.y,position.x)/(2.*3.14159265359);
    angle -= floor(angle);
    float rad = length(position);

    float color = 0.1 * brightness_input;
    float brightness = 0.012 * glow_input;
    float speed = 0.2;

    for (int i = 0; i < iterations; i++) {
        float angleRnd = floor(angle*14.)+1.;
        float angleRnd1 = fract(angleRnd*fract(angleRnd*.7235)*45.1);
        float angleRnd2 = fract(angleRnd*fract(angleRnd*.82657)*13.724);
        float t = t*speed + angleRnd1*10.;
        float radDist = sqrt(angleRnd2+float(i));

        float adist = radDist/rad*.1;
        float dist = (t*.1+adist);
        dist = abs(fract(dist)-.5);
        color +=  (1.0 / (dist))*cos(0.7*(sin(t)))*adist/radDist * brightness;
        angle = fract(angle+(.161*twist + twist_time));
    }

    vec4 final_light_color = light_color;

    if (use_default_color) {
        final_light_color = vec4(1.2,0.8,0.6,1.0);
    }


    return vec4(color,color,color,1.0)*final_light_color;






}