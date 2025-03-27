/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "matt.beghin",
    "DESCRIPTION": "Laser audio osciloscope from stereo audio for xy",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "NAME": "waveform_left", "TYPE": "audio", "SIZE": 512, "CHANNEL": 0 },
        { "NAME": "waveform_right", "TYPE": "audio", "SIZE": 512, "CHANNEL": 1 },
        {"LABEL": "Level", "NAME": "mat_level", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 1024,
       "PRESERVE_ORDER": true
    }
}*/

#include "MadCommon.glsl"

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float indexNorm = float(pointNumber)/(pointCount-1);

	pos.x = mat_level * IMG_NORM_PIXEL(waveform_left,vec2(indexNorm ,0.5)).r;
	pos.y = mat_level * IMG_NORM_PIXEL(waveform_right,vec2(indexNorm ,0.5)).r;
	shapeNumber = 0;
	color = vec4(1);
}
