/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "DESCRIPTION": "Horizontal effect combining two animation patterns that can be divided vertically with an offset. Perfect for controlling textured lines.",
    "TAGS": "geometry,shape,line",
    "VSN": "1.0",
	"INPUTS": [
        { "LABEL": "Anim1/Anim 1", "NAME": "anim1_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim1/Repeat", "NAME": "anim1_repeat", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 1 },
        { "LABEL": "Anim1/Speed", "NAME": "anim1_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Anim1/Reverse", "NAME": "anim1_reverse", "TYPE": "bool", "DEFAULT": 1, "FLAGS": "button" },
        { "LABEL": "Anim1/Shape", "NAME": "anim1_shape", "TYPE": "long", "VALUES": ["Serpentine","Fill","Fill Sin","Chase","Centered","Centered Sin","Luminosity","Flow"], "DEFAULT": "Serpentine", "FLAGS": "generate_as_define" },
        { "LABEL": "Anim1/Smoothness", "NAME": "anim1_smoothness", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim1/Restart", "NAME": "mat_anim1_restart", "TYPE": "event" },

        { "LABEL": "Anim2/Anim 2", "NAME": "anim2_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Anim2/Repeat", "NAME": "anim2_repeat", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 22 },
        { "LABEL": "Anim2/Speed", "NAME": "anim2_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 },
        { "LABEL": "Anim2/Reverse", "NAME": "anim2_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Anim2/Shape", "NAME": "anim2_shape", "TYPE": "long", "VALUES": ["Serpentine","Fill","Fill Sin","Chase","Centered","Centered Sin","Luminosity","Flow"], "DEFAULT": "Serpentine", "FLAGS": "generate_as_define" },
        { "LABEL": "Anim2/Smoothness", "NAME": "anim2_smoothness", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Anim2/Restart", "NAME": "mat_anim2_restart", "TYPE": "event" },

        { "LABEL": "Cells/Repeat Y", "NAME": "repeat_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Offset", "NAME": "offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Cells/Random Off.", "NAME": "random_offset", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
	],
    "GENERATORS": [
        { "NAME": "anim1_time", "TYPE": "time_base", "PARAMS": {"speed": "anim1_speed", "reverse": "anim1_reverse", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "reset": "mat_anim1_restart"} },
        { "NAME": "anim2_time", "TYPE": "time_base", "PARAMS": {"speed": "anim2_speed", "reverse": "anim2_reverse", "strob": 0, "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "reset": "mat_anim2_restart"} },
    ]
}*/

#include "MadCommon.glsl"

// procedural white noise   
float mat_hash( vec2 p ) {
    return fract(sin(dot(p,vec2(127.1,311.7)))*43758.5453);
}

vec4 materialColorForPixel(vec2 texCoord)
{
	int cellId = int(texCoord.y * repeat_y);

    float normalizedCellId; // Value from 0.0 to 1.0
    if (random_offset)
        normalizedCellId = mat_hash(vec2(0,cellId) * 0.5);
    else
        normalizedCellId = float(cellId) / repeat_y; // Number from 0.0 to 1.0

	float out_value = 1;

	if (anim1_level > 0) {
		float pixelPos = fract(texCoord.x*anim1_repeat);
		float anim_pos = fract(anim1_time - offset * normalizedCellId); // Get full value on beat
		float smoothness = anim1_smoothness;

		#if defined(anim1_shape_IS_Serpentine)
			float diff = abs(pixelPos-anim_pos);
			float value = smoothstep(0.25*(1-smoothness),0.25*(1+smoothness),min(diff,1-diff));
		#elif defined(anim1_shape_IS_Fill)
			float diff = (1-anim_pos)*(1+smoothness)-pixelPos;
			float value = smoothstep(0,smoothness+0.000001,diff);
		#elif defined(anim1_shape_IS_Fill_Sin)
			float diff = pixelPos-(1+sin(anim_pos*2*PI))*(1+smoothness)/2;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim1_shape_IS_Chase)
			float diff = abs(pixelPos-0.5-sin(anim_pos*2*PI)/4);
			float value = 1-smoothstep(0.25*(1-smoothness),0.25*(1+smoothness),diff);
		#elif defined(anim1_shape_IS_Centered)
			float diff = abs(pixelPos-0.5)-anim_pos*(1+smoothness)/2;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim1_shape_IS_Centered_Sin)
			float diff = abs(pixelPos-0.5) - (1+sin((anim_pos - offset * normalizedCellId)*2*PI))*(1+smoothness)/4;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim1_shape_IS_Flow)
			int lineID = int(texCoord.x*anim1_repeat);
        	float startStroke = (1+cos(anim_pos*2*PI+1234.341*mat_hash(vec2(0,lineID) * 0.5)))/2;
        	float endStroke = (1+cos(anim_pos*2*PI+1234.341*mat_hash(vec2(0,lineID+1) * 0.5)))/2;
			float value = mix(startStroke,endStroke,pixelPos);
		#else // defined(anim1_shape_IS_Luminosity)
			int lineID = int(texCoord.x*anim1_repeat);
			float value = cos(anim1_time*2*PI - offset * normalizedCellId+10*lineID);
			float cutValue = value<0?1:0;
			value = mix(cutValue,value,smoothness);
		#endif

		out_value = mix(out_value, out_value * value, anim1_level);
	}

	if (anim2_level > 0) {
		float pixelPos = fract(texCoord.x*anim2_repeat);
		float anim_pos = fract(anim2_time - offset * normalizedCellId); // Get full value on beat
		float smoothness = anim2_smoothness;

		#if defined(anim2_shape_IS_Serpentine)
			float diff = abs(pixelPos-anim_pos);
			float value = smoothstep(0.25*(1-smoothness),0.25*(1+smoothness),min(diff,1-diff));
		#elif defined(anim2_shape_IS_Fill)
			float diff = (1-anim_pos)*(1+smoothness)-pixelPos;
			float value = smoothstep(0,smoothness+0.000001,diff);
		#elif defined(anim2_shape_IS_Fill_Sin)
			float diff = pixelPos-(1+sin(anim_pos*2*PI))*(1+smoothness)/2;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim2_shape_IS_Chase)
			float diff = abs(pixelPos-0.5-sin(anim_pos*2*PI)/4);
			float value = 1-smoothstep(0.25*(1-smoothness),0.25*(1+smoothness),diff);
		#elif defined(anim2_shape_IS_Centered)
			float diff = abs(pixelPos-0.5)-anim_pos*(1+smoothness)/2;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim2_shape_IS_Centered_Sin)
			float diff = abs(pixelPos-0.5) - (1+sin((anim_pos - offset * normalizedCellId)*2*PI))*(1+smoothness)/4;
			float value = smoothstep(0,smoothness+0.000001,-diff);
		#elif defined(anim2_shape_IS_Flow)
			int lineID = int(texCoord.x*anim2_repeat);
        	float startStroke = (1+cos(anim_pos*2*PI+1234.341*mat_hash(vec2(0,lineID) * 0.5)))/2;
        	float endStroke = (1+cos(anim_pos*2*PI+1234.341*mat_hash(vec2(0,lineID+1) * 0.5)))/2;
			float value = mix(startStroke,endStroke,pixelPos);
		#else // defined(anim2_shape_IS_Luminosity)
			int lineID = int(texCoord.x*anim2_repeat);
			float value = cos(anim2_time*2*PI - offset * normalizedCellId+10*lineID);
			float cutValue = value<0?1:0;
			value = mix(cutValue,value,smoothness);
		#endif

		out_value = mix(out_value, out_value * value, anim2_level);
	}

	return vec4(vec3(out_value),1);
}

