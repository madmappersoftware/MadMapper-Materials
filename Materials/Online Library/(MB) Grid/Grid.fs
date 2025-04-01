/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "CATEGORIES": [ "Graphic" ],
    "DESCRIPTION": "Draw a grid with customizable resolution, with auto-move / scale & rotate.",
    "TAGS": "grid,geometry",
    "VSN": "1.0",
    "INPUTS": [
        {
            "NAME": "frequency_x",
            "Label": "H. Freq",
            "TYPE": "int",
            "DEFAULT": 10,
            "MIN": 0,
            "MAX": 200
        },
        {
            "NAME": "frequency_y",
            "Label": "V. Freq",
            "TYPE": "int",
            "DEFAULT": 10,
            "MIN": 0,
            "MAX": 200
        },
        {
            "NAME": "width_x",
            "Label": "V. Width",
            "TYPE": "float",
            "DEFAULT": 1.01,
            "MIN": 1.000001,
            "MAX": 10.0
        },
        {
            "NAME": "width_y",
            "Label": "H. Width",
            "TYPE": "float",
            "DEFAULT": 1.01,
            "MIN": 1.000001,
            "MAX": 10.0
        },
        {
            "NAME": "speed_x",
            "Label": "V. Speed",
            "TYPE": "float",
            "MIN" : -2.0,
            "MAX" : 2.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "speed_y",
            "Label": "H. Speed",
            "TYPE": "float",
            "MIN" : -2.0,
            "MAX" : 2.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "Smooth",
            "Label": "Smooth",
            "TYPE": "float",
            "MIN" : 0.0,
            "MAX" : 1.0,
            "DEFAULT": 0.2
        }
    ],
    "GENERATORS": [
        {
            "NAME": "animation_time_x",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed_x", "speed_curve": 3}
        },
        {
            "NAME": "animation_time_y",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed_y", "speed_curve": 3}
        }
    ]
}*/

#define M_PI 3.1415926535897932384626433832795

vec4 materialColorForPixel(vec2 texCoord)
{
    float Sx = 0.9999 - Smooth*pow(width_x/10,2);
    float Sy = 0.9999 - Smooth*pow(width_y/10,2); 
    float x = smoothstep(Sx,1.0,width_x*sin(frequency_x*2*M_PI*(texCoord.x + animation_time_x/2)));
    float y = smoothstep(Sy,1.0,width_y*sin(frequency_y*2*M_PI*(texCoord.y + animation_time_y/2)));

    vec3 color = vec3(x+y,x+y,x+y);

    return vec4(color,1);
}
