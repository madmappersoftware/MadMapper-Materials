/*{
    "CREDIT": "luiscript",
    "DESCRIPTION": "Sine waves that represents music notes",
    "TAGS": "lines, waves, audio, sine, reactive",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "C", "NAME": "c", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "C#", "NAME": "cb", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "D", "NAME": "d", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "D#", "NAME": "db", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "E", "NAME": "e", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "F", "NAME": "f", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "F#", "NAME": "fb", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "G", "NAME": "g", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "G#", "NAME": "gb", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "A", "NAME": "a", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "A#", "NAME": "ab", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "B", "NAME": "b", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
		{ "LABEL": "C1", "NAME": "c1", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.0 },
      ],
}

*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"
	
 
vec4 materialColorForPixel( vec2 texCoord ) {
	float offset = 0.5;
	float triggers[16];
	
	triggers[0] = c;
	triggers[1] = cb;
	triggers[2] = d;
	triggers[3] = db;
	triggers[4] = e;
	triggers[5] = f;
	triggers[6] = fb;
	triggers[7] = g;
	triggers[8] = gb;
	triggers[9] = a;
	triggers[10] = ab;
	triggers[11] = b;
	triggers[12] = c1;
	triggers[13] = 0;
	triggers[14] = 0;
	triggers[15] = 0;
	
	
	vec3 color = vec3(0.0);
	
	for(int i = 0; i < 13; i++){
		
		vec2 p = texCoord - (i + 0.5) * 1/13;
		p.y += sin(10.5 * p.x/0.1 + 20*i) * 0.035 * triggers[i];
		//p.x += TIME;
		float fTemp = abs( 0.05 / p.y / 100);
		color += vec3( fTemp, fTemp, fTemp);
	}
    
	return vec4( color, 1.0 );

}
