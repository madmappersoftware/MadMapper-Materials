/*{
    "CREDIT": "setchi, zerbzman",
    "DESCRIPTION": "Inpired by https://twitter.com/setchi/status/1251153387187367939.",
    "TAGS": "",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "DEFAULT": 3, "MIN": 0.0, "MAX": 10.0 },
		{ "LABEL": "Distance", "NAME": "mat_distance", "TYPE": "float", "DEFAULT": 1.5, "MIN": 1.0, "MAX": 4.0 },

		{ "LABEL": "Foreground/Show", "NAME": "mat_flower1Show", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Foreground/Scale", "NAME": "mat_flower1Scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.5 },
		{ "LABEL": "Foreground/Rotate Plane", "NAME": "mat_flower1PlaneRotate", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 },

		{ "LABEL": "Foreground/Size Variation", "NAME": "mat_flower1SizeVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Foreground/Size Variation\nAmount", "NAME": "mat_flower1SizeVariationAmount", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Foreground/Center Scale", "NAME": "mat_flower1Center", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },

		{ "LABEL": "Foreground/Layers", "NAME": "mat_flower1Layers", "TYPE": "int", "DEFAULT": 8, "MIN": 0, "MAX": 20 },
		{ "LABEL": "Foreground/Petals", "NAME": "mat_flower1Petals", "TYPE": "float", "DEFAULT": 2.0, "MIN": 0.0, "MAX": 4 },
		{ "LABEL": "Foreground/Petal Variation", "NAME": "mat_flower1PetalsVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Foreground/Petal Variation\nAmount", "NAME": "mat_flower1PetalsVariationAmount", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4 },

		{ "LABEL": "Foreground/Rotation", "NAME": "mat_flower1Rotation", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Foreground/Speed", "NAME": "mat_flower1Speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

		{ "LABEL": "Foreground/Color", "NAME": "mat_flower1Color", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] },
		{ "LABEL": "Foreground/Color Variation", "NAME": "mat_flower1ColorVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Foreground/Variation\nAmount", "NAME": "mat_flower1VariationAmount", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 10.0 },

		{ "LABEL": "Background/Show", "NAME": "mat_flower2Show", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Background/Offset", "NAME": "offset", "TYPE": "point2D", "MIN": [ -2, -2 ], "MAX": [ 2, 2 ], "DEFAULT": [ 0.75, 0.75 ]},
		{ "LABEL": "Background/Scale", "NAME": "mat_flower2Scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2.0 },
		{ "LABEL": "Background/Rotate Plane", "NAME": "mat_flower2PlaneRotate", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 },

		{ "LABEL": "Background/Size Variation", "NAME": "mat_flower2SizeVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Background/Size Variation\nAmount", "NAME": "mat_flower2SizeVariationAmount", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Background/Center Scale", "NAME": "mat_flower2Center", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },

		{ "LABEL": "Background/Layers", "NAME": "mat_flower2Layers", "TYPE": "int", "DEFAULT": 8, "MIN": 0, "MAX": 20 },
		{ "LABEL": "Background/Petals", "NAME": "mat_flower2Petals", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4 },
		{ "LABEL": "Background/Petal Variation", "NAME": "mat_flower2PetalsVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Background/Petal Variation\nAmount", "NAME": "mat_flower2PetalsVariationAmount", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4 },

		{ "LABEL": "Background/Rotation", "NAME": "mat_flower2Rotation", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Background/Speed", "NAME": "mat_flower2Speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

		{ "LABEL": "Background/Color", "NAME": "mat_flower2Color", "TYPE": "color", "DEFAULT": [ 0.3, 0.0, 1.0, 1.0 ] },
		{ "LABEL": "Background/Color Variation", "NAME": "mat_flower2ColorVariation", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		{ "LABEL": "Background/Variation\nAmount", "NAME": "mat_flower2VariationAmount", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 10.0 },

		{ "LABEL": "Background\nColor", "NAME": "mat_background", "TYPE": "color", "DEFAULT": [ 0.87, 0.64, 0.84, 1.0 ] },
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_flower1Speed"} },
		{"NAME": "mat_time2", "TYPE": "time_base", "PARAMS": {"speed": "mat_flower2Speed"} },
    ],
}*/

#include "MadSDF.glsl"
#include "MadCOMMON.glsl"
#include "MadNOISE.glsl"

float petal(float q, float th, float th_offset, float size) {
	return (step(
		q, 
		min(
			abs(sin(th + th_offset)) + .4, 
			abs(cos(th + th_offset)) + 1.1
		) * size
	));
}

float flower(vec2 uv, int layers, float petals) {
	float q = length(uv);
	float a = atan(uv.y, uv.x) * petals;
	float a2 = PI / 2.;
	
	float petal1 = 0.7 * q * petal(q, a, 0., .7);

	for(int i=0; i < layers; i++){
		float fi = float(i);
		petal1 = mix(petal1, (0.9 + 0.2 * fi) * q, petal(q, a, 1.5 + 1.5 * fi, .6 - .05 * fi));
	}
	return petal1;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 p1 = (texCoord - vec2(0.5, 0.5)) * scale;

	vec2 p2 = p1 + offset;

	float petal1 = 0.0;
	float petal2 = 0.0;

	vec4 color1 = vec4(0.0);
	vec4 color2 = vec4(0.0);

	if (mat_flower1Show) {
		vec2 cellId1;
		p1 = rotate(p1, mat_flower1PlaneRotate);
		p1 = repeat(p1, vec2(mat_distance), cellId1);
		
		
		if (mat_flower1SizeVariation) {
			p1 *= mat_flower1Scale + noise(cellId1) / mat_flower1SizeVariationAmount / 5;
		} else {
			p1 *= mat_flower1Scale;
		}
		p1 = rotate(p1, mat_flower1Rotation);
		p1 = rotate(p1, mat_time + length(p1) * 1.2);

		float petals = 0.5 + mat_flower1Petals;
		if (mat_flower1PetalsVariation) {
			petals += abs(noise(cellId1)) * mat_flower1PetalsVariationAmount;
		}

		petal1 = flower(p1, mat_flower1Layers, petals);
		if (petal1 > 0) {
			color1 = mix(vec4(vec3(0.0), 1.0), mat_flower1Color, petal1);
		}

		float dd = length(p1 * 50.0 * mat_flower1Center);
		dd = smoothstep(3.0,2., dd);
		color1.rgb += dd;
		
		if (petal1 > 0 && mat_flower1ColorVariation) {
			vec3 hsv = rgb2hsv(color1.rgb);
		
			// put some noise on the color
			hsv.x += noise(cellId1)/mat_flower1VariationAmount;
			color1.rgb = hsv2rgb(hsv);
		}
	}

	if (mat_flower2Show) {
		vec2 cellId2;
		p2 = rotate(p2, mat_flower2PlaneRotate);
		p2 = repeat(p2, vec2(mat_distance), cellId2);

		if (mat_flower2SizeVariation) {
			p2 *= mat_flower2Scale + noise(cellId2) / mat_flower2SizeVariationAmount / 5;
		} else {
			p2 *= mat_flower2Scale;
		}

		p2 = rotate(p2, mat_flower2Rotation);
		p2 = rotate(p2, mat_time2 + length(p2) * 1.2);

		float petals2 = 0.5 + mat_flower2Petals;
		if (mat_flower2PetalsVariation) {
			petals2 += abs(noise(cellId2)) * mat_flower2PetalsVariationAmount;
		}

		petal2 = flower(p2, mat_flower2Layers, petals2);
		if (petal2 > 0) {
			color2 = mix(vec4(vec3(0.0), 1.0), mat_flower2Color, petal2);
		}
		

		float dd2 = length(p2 * 50.0 * mat_flower2Center);
		dd2 = smoothstep(3.0,2., dd2);
		color2.rgb += dd2;
		
		if (petal2 > 0 && mat_flower2ColorVariation) {
			vec3 hsv2 = rgb2hsv(color2.rgb);
		
			// put some noise on the color
			hsv2.x += noise(cellId2) / mat_flower2VariationAmount;
			color2.rgb = hsv2rgb(hsv2);
		}
	}
	
	if (petal1 > 0) {
		return color1;
	}
	if (petal2 > 0) {
		return color2;
	}

	return mat_background;

}