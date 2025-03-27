/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Josef Albers - SP / 1967",
    "TAGS": "painting",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1},
		{ "LABEL": "Global/Offset", "NAME": "offset", "TYPE": "point2D", "MIN": [ -1, -1 ], "MAX": [ 1, 1 ], "DEFAULT": [ 0.0, 0.0 ]},

		{ "LABEL": "Grid/Columns", "NAME": "rows", "TYPE": "int", "MIN": 1, "MAX": 3, "DEFAULT": 3 },
		{ "LABEL": "Grid/Rows", "NAME": "cols", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 4 },
		{ "LABEL": "Grid/Space X", "NAME": "spaceX", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0.2 },
		{ "LABEL": "Grid/Space Y", "NAME": "spaceY", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0.2 },

		{ "LABEL": "Squares/Iterations", "NAME": "iterations", "TYPE": "int", "MIN": 1, "MAX": 4, "DEFAULT": 4 },
		{ "LABEL": "Squares/Size offset", "NAME": "offsetSize", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.23 },
		{ "LABEL": "Squares/Decal offset", "NAME": "decal", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "Squares/Randomise", "NAME": "isRandomised", "TYPE": "bool",  "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Squares/Pos offset", "NAME": "offsetPos", "TYPE": "point2D", "MIN": [ -1, -1 ], "MAX": [ 1, 1 ], "DEFAULT": [ 0.0, 0.65 ]},

		{ "LABEL": "Noise/Speed", "NAME": "noiseSpeed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1},
		{ "LABEL": "Noise/Power", "NAME": "noisePower", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0 },

		{ "LABEL": "Render/Stroke", "NAME": "isShapeStroked", "TYPE": "bool",  "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Render/Strokeweight", "NAME": "strokeWeight", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.0},

		{ "LABEL": "Colors/Animation", "NAME": "colorAnimation", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0},			
        { "LABEL": "Colors/Stroke", "NAME": "strokeColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 0.0 ]},
        { "LABEL": "Colors/Background", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ]}
	],
	"GENERATORS": [
		{ "NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "noiseSpeed"}},
		{ "NAME": "mat_noise_color_time", "TYPE": "time_base", "PARAMS": {"speed": "noiseColor"}},
	]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

// Set up constants
const vec2 center = vec2(0.5);
const float squareSize = 0.22;

// First squares (with all iterations) from left to right
const float[48] HUES =			float[48](0.06,0.60,0.48,0.45, 0.13,0.14,0.11,0.14, 0.22,0.69,0.63,0.78, 0.52,0.57,0.06,0.06, 0.20,0.54,0.54,0.14, 0.63,0.37,0.42,0.47, 0.13,0.09,0.07,0.09, 0.02,0.04,0.02,0.02, 0.45,0.50,0.41,0.13, 0.12,0.11,0.12,0.12, 0.08,0.09,0.09,0.07, 0.45,0.51,0.50,0.56); 
const float[48] SATURATIONS = 	float[48](0.55,0.08,0.68,1.00, 0.60,0.70,0.58,0.77, 0.55,0.04,0.04,0.02, 0.89,0.22,0.57,0.58, 0.66,0.07,0.06,0.71, 0.06,0.38,0.99,0.78, 1.00,0.64,0.59,0.91, 0.80,0.74,0.86,0.86, 0.68,0.71,0.06,0.64, 1.00,0.70,0.74,1.00, 0.67,0.68,0.87,0.86, 0.59,0.83,0.84,1.00);
const float[48] VALUES = 		float[48](0.47,0.39,0.58,0.67, 0.88,0.91,0.96,0.95, 0.47,0.56,0.47,0.40, 0.48,0.45,0.49,0.48, 0.63,0.63,0.63,0.85, 0.43,0.43,0.50,0.54, 0.97,0.92,0.89,0.94, 0.77,0.75,0.83,0.83, 0.37,0.56,0.68,0.92, 0.95,0.89,0.95,0.95, 0.69,0.71,0.94,0.94, 0.53,0.48,0.47,0.69);


float random (vec2 st) {
    return fract(sin(dot(st.xy,vec2(12.9898,78.233)))* 43758.5453123);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// Set up
	vec2 uv = (texCoord - center + vec2(-offset.x, offset.y)) * scale;
	vec3 color = backgroundColor.rgb;
	float noiseTime = mat_noise_time;
	float noiseColorTime = mat_noise_color_time;
	int squareNumber = 0;
	
	// Dividing (to get a range between 0 and 1 in labels)
	float spaceXBetween = spaceX * 0.1;
	float spaceYBetween = spaceY * 0.1;
	float squareSizeOffset = offsetSize * 0.1;
	vec2 squarePosOffset = offsetPos * 0.02;

	for(int c = 0; c < cols; c++)
	{
		for(int r = 0; r < rows; r++)
		{	
			float size = squareSize * 0.5;

			vec2 doffset;			
			doffset.x = flowNoise(vec2(c*1.2356+12.3,r*0.159+2.3),offsetPos.x)  * 0.02;
			doffset.y = flowNoise(vec2(c*1.2356+5.69,r*0.159+9.8),offsetPos.y)  * 0.02;

			for(int i = 1; i < iterations+1; i++)
			{
				// Offset squares
				vec2 randOffset = squarePosOffset;
				randOffset.x *= sign(random(vec2(c, r))* 2 -1) ;
				randOffset.y *= sign(random(vec2(r+3.124, c + 12.345))* 2 -1);
				
				vec2 testOffset = squarePosOffset;
				if(isRandomised){
					testOffset = randOffset;
				}

				vec2 offset = vec2(squareSize*r + spaceXBetween*r , squareSize*c + spaceYBetween*c);
				offset += mix(testOffset, doffset, vec2(decal)) * (i-1);
				
				// Offset noise 					
				float noiseX = flowNoise(vec2(noiseTime, c),  0);
				float noiseY = flowNoise(vec2(noiseTime, r), 0);
				vec2 noise = vec2(noiseX, noiseY);
				offset += noise * noisePower * (i-1) * 0.03;
				
				// Center grid
				vec2 position = uv ;

				vec2 gridSize = vec2(squareSize*rows + spaceXBetween*(rows-1), 
								     squareSize*cols + spaceYBetween*(cols-1));

				position -= squareSize*0.5;
				position += gridSize*0.5;

				// Square size (reduce each iteration)
				if(i != 1) size -= squareSizeOffset;
				
				// Final distance field
				float square = rectangle(position, offset, size);
				
				// Square color		
				vec3 squareColor = hsv2rgb(vec3(fract(HUES[squareNumber] + colorAnimation), 
													  SATURATIONS[squareNumber], 
													  VALUES[squareNumber]));
				
				// Final square
				if(isShapeStroked){
					color = fill(color, squareColor, square);
					color = stroke(color, strokeColor.rgb, square, strokeWeight * 0.02);
				} else {
					color = fill(color, squareColor, square);
				}	

				squareNumber += 1;			
			}	
		}	
	}
	
	return vec4(color,1);
}