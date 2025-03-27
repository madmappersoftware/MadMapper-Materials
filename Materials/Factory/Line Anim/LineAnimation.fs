/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Lines animation patterns with a few controls.",
    "VSN": "1.0",
    "TAGS": "line,graphic",
	"INPUTS": [
	    { "LABEL": "Line Width", "NAME": "line_width", "TYPE": "float", "DEFAULT": 0.03, "MIN": 0.0, "MAX": 0.5 },
	    { "LABEL": "Smoothness", "NAME": "smoothness", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.001, "MAX": 1.0 },
		{ "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Decay", "NAME": "decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Release", "NAME": "release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Animation", "NAME": "animation", "TYPE": "long", "VALUES": ["Cross", "Centered Filled","Single","Filled"], "DEFAULT": "Cross", "FLAGS": "generate_as_define" },
        { "LABEL": "Cells/Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Offset", "NAME": "offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Cells/Random Off.", "NAME": "random_offset", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }
	],
    "GENERATORS": [
        { "NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true} }
    ]
}*/

// procedural white noise   
float hash( vec2 p ) {
    return fract(sin(dot(p,vec2(127.1,311.7)))*43758.5453);
}

vec4 materialColorForPixel(vec2 texCoord)
{
	if (line_width==0) return vec4(0,0,0,1);

	float pixelPos=mod(texCoord.x*cells_x,1);
	vec2 cellId = vec2(int(texCoord.x * cells_x), int(texCoord.y * cells_y));

    float normalizedCellId; // Value from 0.0 to 1.0
    if (random_offset)
        normalizedCellId = hash(cellId * 0.5);
    else
        normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0

	float adjusted_bpm_pos = mod(animation_time - offset * normalizedCellId,1); // Get full value on beat
	if (adjusted_bpm_pos < decay) {
		adjusted_bpm_pos = 1;
	} else {
		// get back a value from 0-1 (from end of decay to 1 - end of beat)
		adjusted_bpm_pos = (adjusted_bpm_pos - decay) * 1 / (1 - decay);
		if (adjusted_bpm_pos < release) {
			adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release;
		} else {
			adjusted_bpm_pos = 0;
		}
	}

	float value;

	#if defined(animation_IS_Cross)
		value = smoothstep(line_width,line_width*(1-smoothness),abs(pixelPos-adjusted_bpm_pos));
		value += smoothstep(line_width,line_width*(1-smoothness),abs((1-pixelPos)-adjusted_bpm_pos));
	#elif defined(animation_IS_Centered_Filled)
		value = smoothstep(0,-smoothness,(2*abs(0.5-pixelPos)-adjusted_bpm_pos));
	#elif defined(animation_IS_Single)
		float dist = mod(abs(pixelPos-adjusted_bpm_pos),1);
		value = smoothstep(line_width,line_width*(1-smoothness),dist);
	#else // defined(animation_IS_Filled)
		//value = (pixelPos < adjusted_bpm_pos)?1:0;
		value = smoothstep(0,-smoothness,pixelPos-adjusted_bpm_pos);
	#endif

	return vec4(vec3(value),1);
}

