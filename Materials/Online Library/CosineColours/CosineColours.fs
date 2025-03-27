/*{
    "CREDIT": "Henry",
    "DESCRIPTION": "Renders the 7 different cosine colour palettes created by IQ and described here: https://iquilezles.org/articles/palettes/. Intended for use in pixelmapping or as an input to filters.",
    "TAGS": "color,pixelmap",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed X", "NAME": "mat_speedx", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
		{"LABEL": "Zoom X", "NAME": "mat_zoomx", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{"LABEL": "Pos X", "NAME": "mat_posx", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Speed Y", "NAME": "mat_speedy", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{"LABEL": "Zoom Y", "NAME": "mat_zoomy", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{"LABEL": "Pos Y", "NAME": "mat_posy", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{"LABEL": "Crossfade", "NAME": "mat_crossfade", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },   
    ],
	"GENERATORS": [
        {"NAME": "mat_timex", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedx"} },
		{"NAME": "mat_timey", "TYPE": "time_base", "PARAMS": {"speed": "mat_speedy"} },
    ],
}*/
// The MIT License
// https://www.youtube.com/c/InigoQuilez
// https://iquilezles.org/
// Copyright Ãƒâ€šÃ‚Â© 2015 Inigo Quilez
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.



// A simple way to create color variation in a cheap way (yes, trigonometrics ARE cheap
// in the GPU, don't try to be smart and use a triangle wave instead).

// See https://iquilezles.org/articles/palettes for more information
#include "MadNoise.glsl"

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

vec3 pal( in float t, in float i )
{	
	i = mod(i,7.);
	if(i < 1.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67));
	if(i < 2.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.10,0.20));
	if(i < 3.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.3,0.20,0.20));
	if(i < 4.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,0.5),vec3(0.8,0.90,0.30));
	if(i < 5.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,0.7,0.4),vec3(0.0,0.15,0.20));
	if(i < 6.0) return pal( t, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(2.0,1.0,0.0),vec3(0.5,0.20,0.25));
	return pal( t, vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.0,1.0,1.0),vec3(0.0,0.25,0.25));
}


vec4 materialColorForPixel( vec2 p )
{   
    // animate
	float xin = p.x;
	float yin = p.y;
	p.x = p.x * mat_zoomx + mat_posx;
    p.x += 0.1 * mat_timex;
    
	p.y = p.y * mat_zoomy + mat_posy;
	p.y += 0.1 * mat_timey;
	p.y -= floor(p.y); 

    // compute colors
	float i = p.y * 7. + .5;
	float f = fract(i);
	float iq = floor(i); // band
	float b = 1. - mat_crossfade;
    vec3 col = mix(pal( p.x, iq - 1 ), pal( p.x, iq), smoothstep(b / 2., 1. - b / 2., f));
	
	//if(xin > .5) col = pal( xin, int(yin * 7.0));

	return vec4(col,1.);
}