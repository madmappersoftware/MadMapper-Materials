/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Ryoji Ikeda - Dataplex / 2005",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0, "FLAGS": "hidden" },
		{ "LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0, "FLAGS": "hidden"  },  
		{ "LABEL": "Global/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 0.25,  0.25 ], "MIN": [ -0.25, -0.25 ], "DEFAULT": [ 0.0, 0.0 ], "FLAGS": "hidden"},

		{ "LABEL": "Grid/Columns", "NAME": "mat_columns", "TYPE": "int", "MIN": 1, "MAX": 100, "DEFAULT": 50 },  
		{ "LABEL": "Grid/Rows", "NAME": "mat_rows", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 8 },  

		{ "LABEL": "Mask/Borders X", "NAME": "mat_bordersX", "TYPE": "float", "MIN": 0, "MAX": 0.5, "DEFAULT": 0.045 },  
		{ "LABEL": "Mask/Borders Y", "NAME": "mat_bordersY", "TYPE": "float", "MIN": 0, "MAX": 0.5, "DEFAULT": 0.115 }, 
 
		{ "LABEL": "Lines/Height", "NAME": "mat_linesHeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 }, 
		{ "LABEL": "Lines/Noise Power", "NAME": "mat_linesNoisePower", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1.0 },  
		{ "LABEL": "Lines/Noise Freq", "NAME": "mat_linesNoiseFrequency", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },
		{ "LABEL": "Lines/Speed", "NAME": "mat_linesSpeed", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },   

		{ "LABEL": "Numbers/Count", "NAME": "mat_numbersCount", "TYPE": "int", "MIN": 0, "MAX": 10, "DEFAULT": 10 }, 
		{ "LABEL": "Numbers/Size", "NAME": "mat_numbersSize", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1.0 },
		{ "LABEL": "Numbers/Toggle", "NAME": "mat_numbersFade", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1.0 },
		{ "LABEL": "Numbers/Speed", "NAME": "mat_numbersSpeed", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
		{ "LABEL": "Numbers/1024", "NAME": "mat_numbers1024", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0},      		
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
		{"NAME": "mat_lines_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_linesSpeed"} },
		{"NAME": "mat_numbers_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_numbersSpeed"} },
    ],
  	"IMPORTED": [
		{"NAME": "tex_bitmapFont", "PATH": "bitmap_font.png"},
    ]
}*/

#define SDF_ANTIALIASING_MEDIUM

#include "MadSDF.glsl"
#include "MadNoise.glsl"

const vec3 backgroundColor = vec3(1.0);
const vec3 lineColor = vec3(0);
const float[4] numbersArr = float[4](1, 0, 2, 4);

float map(float value, float min1, float max1, float min2, float max2) {
  return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
}

vec4 character(vec2 p, int c) {
    vec2 dFdx = dFdx(p/16.), dFdy = dFdy(p/16.);
    if (p.x<.0|| p.x>1. || p.y<0.|| p.y>1.) return vec4(0,0,0,1e5);

	return textureGrad(tex_bitmapFont, p/16. + fract( vec2(c, 15-c/16) / 16. ), dFdx, dFdy );
}

vec3 drawNumber(vec2 p, int n)
{
	return vec3(1 - character(p, n + 48).x);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// INITIALISATION
	vec2 uv = vec2(texCoord.x, -texCoord.y) * mat_scale;
	vec2 domain = fract(vec2(uv.x * mat_columns, uv.y * mat_rows));
	vec2 id = floor(vec2(uv.x * mat_columns, uv.y * mat_rows));
	vec2 borders = 0.5 - vec2(mat_bordersX, mat_bordersY); 
	vec3 color = backgroundColor;

	// DRAW LINES
	float linesHeight = map(mat_linesHeight, 0, 1, 0, 0.4);
	float lineThickness = mat_linesNoisePower * (noise(vec3(id * mat_linesNoiseFrequency, mat_lines_time)) * 0.5 + 0.5);

	float line = line(domain, vec2(0.5, 0.0), vec2(0.5, 1.0), lineThickness);
	line = max(line, rectangle(domain, vec2(0.0, 0.5), vec2(1, linesHeight)));
		
	color = fill(color, lineColor, line);

	// DRAW NUMBERS
	vec2 fontUV = vec2(uv.x, uv.y + 1);
	vec2 textOffset = vec2(0, 0.5);
	vec3 numbers = vec3(1);

	// Size
	float textSize = map(mat_numbersSize, 0, 1, 20, 4);

	fontUV = fract(vec2(fontUV.x * mat_rows, fontUV.y * mat_rows) + textOffset) * textSize - vec2(0.5) * (textSize -1);

	for(int i=0; i < mat_numbersCount; i ++){
		// Random value
		float noiseCountDown = noise(vec2(mat_numbers_time * (1-mat_numbers1024), i)) * 0.5 + 0.5;
		float number = mix(noiseCountDown * 10, numbersArr[-i%4], mat_numbers1024);
	
		// Spacing between letters
		vec2 letterSpacing = vec2(0.5 * (i-(mat_numbersCount/2.-0.5)), 0.);

		// Draw each char
		numbers *= drawNumber(fontUV + letterSpacing, int(number));
	}

	numbers = mix(color, numbers, mat_numbersFade);
	color *= numbers;

	// ADD MASK
	float cadreDist = rectangle(uv, vec2(0.5, -0.5), borders);
	vec3 cadre = fill(vec3(1), color, cadreDist);

	return vec4(max(color, cadre), 1);
}