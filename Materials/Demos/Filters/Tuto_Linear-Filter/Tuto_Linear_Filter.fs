/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Damper Tutorial: apply a damper on a slider value with parametrable hardness & damping. Use damper output for an horizontal line position.",
    "TAGS": "tutorial,damper",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Value", "NAME": "mat_value", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },  
        { "LABEL": "Duration", "NAME": "mat_duration", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },  
    ],
    "GENERATORS": [
        {"NAME": "mat_filtered_value", "TYPE": "linear_filter", "PARAMS": {"input_value": "mat_value","duration": "mat_duration"}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    float dist = (texCoord.y - mat_filtered_value) * 30;
    float value = 1-pow(dist,3);
    return vec4(value*2,value,value,1);
}
