/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team, modified by Jason Beyers",
    "DESCRIPTION": "Stroboscope - with BPM sync and cut threshold",
    "VSN": "1.0",
    "TAGS": "graphic",
	"INPUTS": [
	    { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "FLAGS": "button", "DEFAULT": true },
         { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "Label": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "Label": "Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Decay", "NAME": "decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Release", "NAME": "release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Out","In","Smooth"], "DEFAULT": "Out" },
        { "LABEL": "Cut", "NAME": "cut", "TYPE": "bool", "FLAGS": "button", "DEFAULT": false },
        { "Label": "Cut Threshold", "NAME": "cut_threshold", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },
	],
    "GENERATORS": [
        {
            "NAME": "strob_position",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed", "reverse": "mat_reverse", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true}
        }
    ]
}*/

// in float adjusted_bpm_pos;

#define PI 3.1415926535897932384626433832795

vec4 materialColorForPixel(vec2 texCoord)
{


	vec4 backColor = vec4(backgroundColor.rgb * brightness, backgroundColor.a);
	vec4 frontColor = vec4(foregroundColor.rgb * brightness, foregroundColor.a);


    float adjusted_bpm_pos = mod(strob_position + mat_offset,1); // Get full value on beat
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

    if (shape == 0) { // Out
        adjusted_bpm_pos = adjusted_bpm_pos;
    } else if (shape == 1) { // In
        adjusted_bpm_pos = 1-adjusted_bpm_pos;
    } else { // Smooth
        adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
    }

    if (cut) {

        if (adjusted_bpm_pos > cut_threshold) {
            adjusted_bpm_pos = 1.;
        } else {
            adjusted_bpm_pos = 0.;
        }


    }


	return mix(backColor,frontColor,adjusted_bpm_pos);
}

