/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Shape Patterns with an image as timeline",
    "TAGS": "atmospheric,bpm,beam",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.0 }, 
	    {"LABEL": "Global/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{"LABEL": "Color/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
		{"LABEL": "Color/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [1,0,0,1] }, 
		{"LABEL": "Color/Color 3", "NAME": "mat_color3", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","bpm_sync": "mat_bpmsync", "speed_curve": 2,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": 20,
		"PRESERVE_ORDER": true
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
	float animTime = mat_time/8;

	bool drawAnim1 = ((int(animTime)%4) == 0);
	bool drawAnim2 = ((int(animTime)%4) == 1 || (int(animTime)%4) == 2);
	bool anim2Flicker = (int(animTime)%3) == 0;
	bool drawAnim3 = ((int(animTime)%4) == 3);
	bool drawTrick1 = ((int(animTime/2)%8) == 4);
	bool drawTrick2 = ((int(animTime*64)%32) == 0);

	pos = vec2(0);
	color = mat_color1;
	shapeNumber = -1;

	if (drawTrick1) {
		if (pointNumber < 8) {
			pos = vec2(curlNoise(vec2(animTime,0)).x/6+0.5*(-0.5+pointNumber/7.),0);
			color = mat_color1 * (int(animTime*32)%2==0?1:0);
			shapeNumber = pointNumber;
		}
	} else if (drawTrick2) {
		if (pointNumber < 8) {
			pos = vec2(-1+2*(pointNumber)/7.,0);
			color = mat_color1;
			shapeNumber = pointNumber;
		}
	} else if (pointNumber < 4) {
		if (drawAnim1) {
			pos = vec2(sin(animTime*PI)*(-1+2*pointNumber/3.),0);
			color = mat_color1;
			shapeNumber = pointNumber;
		}
	} else if (pointNumber < 12) {
		if (drawAnim2) {
			if (pointNumber < 8) {
				pos = vec2(-0.5 + (-0.5+0.5*fract(-animTime)) * (-1+2*(pointNumber-4)/3.),0);
			} else {
				pos = vec2(0.5 + (-0.5+0.5*fract(-animTime)) * (-1+2*(pointNumber-8)/3.),0);
			}
			color = mat_color2;
			if (anim2Flicker) {
				color *=  (int(animTime*32)%2==0)?1:0;
			}
			shapeNumber = pointNumber;
		}
	} else if (pointNumber < 20) {
		if (drawAnim3) {
			pos = vec2(curlNoise(vec2(animTime/5+int(animTime*4),pointNumber)).x/5,0);
			color = mat_color3;
			color *=  (int(animTime*32)%2==0)?1:0;
			shapeNumber = pointNumber;
		}
	}
}
