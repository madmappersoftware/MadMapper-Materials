/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Damper Tutorial: apply a damper on a slider value with parametrable hardness & damping. Use damper output for an horizontal line position.",
    "TAGS": "tutorial,damper",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Anim1/Value", "NAME": "mat_value", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },  
        { "LABEL": "Anim1/Hardness", "NAME": "mat_hardness", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },  
        { "LABEL": "Anim1/Damping", "NAME": "mat_damping", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },  
    ],
    "GENERATORS": [
        {"NAME": "mat_damped_value", "TYPE": "damper", "PARAMS": {"input_value": "mat_value","hardness": "mat_hardness", "damping": "mat_damping"}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	float dist = abs(texCoord.y - mat_damped_value) * 10;
	float value = pow(1-dist,3);
	return vec4(value*2,value,value,1);
}
