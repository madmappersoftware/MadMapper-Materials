/*{
    "CREDIT": "Collectif Scale | Furtive Vision",
    "TAGS": "Carnaval mask scale",
    "DESCRIPTION": "Carnaval mask",
    "VSN": "0.1",
    "INPUTS": [  
        { "LABEL": "Shape A", "NAME": "mat_shapeA", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1 },
        { "LABEL": "Shape B", "NAME": "mat_shapeB", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Shape C", "NAME": "mat_shapeC", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Shape D", "NAME": "mat_shapeD", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Shape E", "NAME": "mat_shapeE", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Shape F", "NAME": "mat_shapeF", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },
    ],
}*/

// #define NOISE_TEXTURE_BASED
// precision highp float;
#define SDF_ANTIALIASING_MEDIUM
#include "MadCommon.glsl"

vec4 materialColorForPixel(vec2 texCoord) {

    // first grid
	vec2 pos = texCoord; // 0. -> 1.
    float alpha = 0.;

	float size = 1./ 6.;
	float shapes[6] = float[](mat_shapeA, mat_shapeB,mat_shapeC,mat_shapeD,mat_shapeE,mat_shapeF); 

	for(int i = 0; i < 6; i++) {
		float start = float(i) * size;
		alpha += step(pos.y, start + size) * step(start, pos.y) * shapes[i];	
	}
    vec3 color = vec3(alpha);
	return vec4(color, 1.0);
}
