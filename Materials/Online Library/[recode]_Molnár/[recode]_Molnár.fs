/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Vera Molnar - No title / 1971",
    "VSN": "1.0",
    "TAGS": "painting",
    "INPUTS": [
        { "LABEL": "Transforms/Global scale", "NAME": "globalScale", "TYPE": "float", "MIN": 2, "MAX": 20, "DEFAULT": 11 },
        { "LABEL": "Transforms/Cells scale", "NAME": "cellsScale", "TYPE": "float", "MIN": 0, "MAX": 0.19, "DEFAULT": 0.15 },
        { "LABEL": "Transforms/Cells rotation", "NAME": "cellsRotation", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 0 },
        { "LABEL": "Transforms/Global rotation", "NAME": "globalRotation", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 0 },
        { "LABEL": "Transforms/Rectangle translation", "NAME": "rectTranslation", "TYPE": "float", "MIN": 0, "MAX": 5, "DEFAULT": 0 },

        { "LABEL": "Anims/Speed", "NAME": "speed", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 0 },
		{ "LABEL": "Anims/Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button, trigger"},

        { "LABEL": "Others/Offset Y", "NAME": "offsetY", "TYPE": "float", "MIN": -0.1, "MAX": 0.1, "DEFAULT": 0.08 },
        { "LABEL": "Others/Borders weight", "NAME": "bordersWeight", "TYPE": "float", "MIN": 0, "MAX": 0.5, "DEFAULT": 0.05 },

        { "LABEL": "Color/Shapes Color", "NAME": "shapesColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.78, 0.75, 0.70, 1.0 ] },
	],
	"GENERATORS": [
        { "NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse"}},
        { "NAME": "mat_global_time", "TYPE": "time_base", "PARAMS": {"speed": "globalRotation", "reverse": "reverse"}},
        { "NAME": "mat_cells_time", "TYPE": "time_base", "PARAMS": {"speed": "cellsRotation", "reverse": "reverse"}},
        { "NAME": "mat_rect_time", "TYPE": "time_base", "PARAMS": {"speed": "rectTranslation", "reverse": "reverse"}},
    ]
}*/


#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"

float map(float value, float min1, float max1, float min2, float max2) {
	return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
}

float randomRotation (vec2 _st) {
	float randomValue = fract(sin(dot(_st.xy, vec2(12.9898,78.233)))*43758.5453123);
	float result = mix(mix(1, 1.5, step(0.25, randomValue)), 
					   mix(0.5, 0, step(0.75, randomValue)), 
					   step(0.5, randomValue));
	
	return result;
}

vec4 materialColorForPixel( vec2 uv )
{
	// Set up
	vec2 cellId;
	vec2 center = vec2(0.5);
	
	// Settings
	vec2 borders = vec2(bordersWeight, 1 - bordersWeight);
	vec2 movableRectSize = vec2(cellsScale * 0.9, cellsScale * 0.35);

	if (uv.x < borders.y && uv.x > borders.x && uv.y < borders.y && uv.y > borders.x) {
		uv = (uv-center) * (globalScale/2);
		uv = rotate(uv, PI * globalRotation/10);

		// Setting domain
		vec2 domain = repeat(uv, center, cellId);
 		domain.y += mix(offsetY, -offsetY, step(uv.x + center.x, 0.5));

		domain = rotate(domain, PI * randomRotation(cellId));
		domain = rotate(domain, PI * cellsRotation / 10);
	
		// Columns
		float square = rectangle(domain, cellsScale);
		float bars = rectangle(domain, vec2(cellsScale * 0.35, cellsScale * 1.1));	
		float columns = subtract(square, bars);
	
		// Movable rect		
		float movableRect = rectangle(vec2(domain.x, (cellsScale - movableRectSize.y) * cos(rectTranslation) + domain.y),
									  movableRectSize);
		float shapeGeo = blend(columns, movableRect);
		vec3 shape = fill(backgroundColor.rgb, shapesColor.rgb, shapeGeo);
		
		return vec4(shape, 1);
	} else {
		return vec4(backgroundColor.rgb, 1.0);
	}
}