/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Krek",
    "DESCRIPTION": "Single Static Beam \nBe extra careful,  as static beams are very dangerous to eyes, optics and can even set things on fire!",
    "TAGS": "laser, point",
    "VSN": "1.0",
    "INPUTS": [
        {"LABEL": "Global/Point Repeat", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 50, "DEFAULT": 10 }, 
        {"LABEL": "Global/Position", "NAME": "mat_position_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.0, 0.0 ] },
        {"LABEL": "Global/Intensity", "NAME": "mat_intensity", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 1.0 },

        {"LABEL": "Color/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha"}
    ],

    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_amount"
    }
}*/


#include "MadCommon.glsl"


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	// each point is in the middle
    pos = vec2(0, 0);

    // apply position offset
    pos += mat_position_offset;

    // each point is a shape
    shapeNumber = pointNumber; 

    // color point
    color = vec4(mat_color.rgb, mat_intensity);
}
