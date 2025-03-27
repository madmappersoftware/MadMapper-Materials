
/*{
    "CREDIT": "Jason Beyers",
    "DESCRIPTION": "Lines animation patterns with a tons of controls, including line-aware color gradients",
    "VSN": "1.1",
    "TAGS": "line,graphic",
	"INPUTS": [

        { "LABEL": "Restart", "NAME": "restart", "TYPE": "event" },
	    { "LABEL": "Line Width", "NAME": "line_width", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0 },
	    { "LABEL": "Smoothness", "NAME": "smoothness", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.001, "MAX": 1.0 },
		{ "LABEL": "Repeat", "NAME": "repeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },
		{ "LABEL": "Line Tilt", "NAME": "line_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "BPM Phase Sync", "NAME": "bpm_phase_sync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },

        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
		{ "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },


		{ "Label": "Offset", "NAME": "bpm_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },


        { "Label": "Offset Alt", "NAME": "bpm_offset_alt", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "Label": "Strob", "NAME": "bpm_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },


        { "LABEL": "Decay", "NAME": "decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Release", "NAME": "release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },



        { "LABEL": "Strobe On", "NAME": "strobe_on", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },

        { "Label": "Strobe Divisions", "NAME": "strobe_div", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 16 },

        { "Label": "Strobe Offset", "NAME": "strobe_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },


        { "LABEL": "Strobe Audio Enable", "NAME": "strobe_audio_enable", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },


        { "Label": "Strobe Audio Thresh", "NAME": "strobe_audio_thresh", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0.0 },










        {
            "NAME": "spectrum",
            "TYPE": "audioFFT",
            "SIZE": 16,
            "ATTACK": 0.0,
            "DECAY": 0.0, //0.0
            "RELEASE": 0.4 //0.2
        },

        {
            "NAME": "spectrum_16_decay",
            "TYPE": "audioFFT",
            "SIZE": 16,
            "ATTACK": 0.0,
            "DECAY": 0.4,
            "RELEASE": 0.0
        },



        {
            "Label": "Audio/Bass Scale",
            "NAME": "bass_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Audio/Medium Scale",
            "NAME": "medium_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Audio/Treble Scale",
            "NAME": "treble_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Audio/Master Scale",
            "NAME": "master_scale",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 10.0
        },

		{ "LABEL": "Animation", "NAME": "animation", "TYPE": "long", "VALUES": ["Cross", "Centered Filled","Single","Filled", "Fill Sin Up", "Fill Sin Down","Fill Up","Fill Down","Empty Up","Empty Down","Chase","Centered","Centered Sin","None" ], "DEFAULT": "Single", "FLAGS": "generate_as_define" },

		{ "LABEL": "Global Rotate", "NAME": "rotate", "TYPE": "float", "MIN": -1.0, "MAX": 2.0, "DEFAULT": 0.0 },

        { "LABEL": "Post-effect Rotate", "NAME": "post_rotate", "TYPE": "float", "MIN": -1.0, "MAX": 2.0, "DEFAULT": 0.0 },

		{ "LABEL": "Mirror X", "NAME": "mirror_x", "TYPE": "long", "VALUES": ["None", "Mirror Shape","Mirror All"], "DEFAULT": "None", "FLAGS": "generate_as_define" },

        { "LABEL": "Slide X", "NAME": "slide_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "LABEL": "Mirror Y", "NAME": "mirror_y", "TYPE": "long", "VALUES": ["None", "Mirror Shape","Mirror All"], "DEFAULT": "None", "FLAGS": "generate_as_define" },

        { "LABEL": "Slide Y", "NAME": "slide_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },



        { "LABEL": "X Border", "NAME": "border_x", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "LABEL": "Y Border", "NAME": "border_y", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },


		//{ "LABEL": "Soft Front Edge", "NAME": "soft_front", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
		//{ "LABEL": "Soft Back Edge", "NAME": "soft_back", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },

		//{ "LABEL": "Super Smooth", "NAME": "super_smooth", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

		{ "LABEL": "Front Edge", "NAME": "front_edge_mode", "TYPE": "long", "VALUES": ["Soft","Hard","Fade"], "DEFAULT": "Soft" },
		{ "LABEL": "Back Edge", "NAME": "back_edge_mode", "TYPE": "long", "VALUES": ["Soft","Hard","Fade"], "DEFAULT": "Soft" },

		{ "LABEL": "Alternate Edges", "NAME": "alternate_edges", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Cells/Cells X", "NAME": "cells_x", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Cells Y", "NAME": "cells_y", "TYPE": "int", "MIN": 1, "MAX": 32, "DEFAULT": 1 },
        { "LABEL": "Cells/Offset", "NAME": "offset", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Cells/Random Off", "NAME": "random_offset", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Cells/Alternate Offset", "NAME": "alternate_offset", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Cells/Alternate Direction", "NAME": "alternate_direction", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

		{ "Label": "Strobe/Strobe Level", "NAME": "strobe_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Strobe/BPM Sync", "NAME": "strob_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Strobe/Reverse", "NAME": "strob_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

		{ "Label": "Strobe/Speed", "NAME": "strob_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },

        { "Label": "Strobe/Offset", "NAME": "strob_bpm_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "Label": "Strobe/Strob", "NAME": "strob_bpm_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Strobe/Decay", "NAME": "strob_decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "Label": "Strobe/Release", "NAME": "strob_release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
        { "LABEL": "Strobe/Shape", "NAME": "strob_shape", "TYPE": "long", "VALUES": ["Out","In","Smooth"], "DEFAULT": "Out" },

        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 1.0 },

        { "Label": "Color/Color Level", "NAME": "grad_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "LABEL": "Color/Type", "NAME": "grad_type", "TYPE": "long", "VALUES": ["Auto Color Cycle","Custom Gradient","Custom Cycle"], "DEFAULT": "Auto Color Cycle", "FLAGS": "generate_as_define" },

		{ "LABEL": "Color/Color Count", "NAME": "grad_color_count", "TYPE": "int", "DEFAULT": 2, "MIN": 1, "MAX": 10 },
        { "LABEL": "Color/Color 1", "NAME": "grad_color1", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 2", "NAME": "grad_color2", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
        { "LABEL": "Color/Color 3", "NAME": "grad_color3", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
        { "LABEL": "Color/Color 4", "NAME": "grad_color4", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Color 5", "NAME": "grad_color5", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },

		{ "LABEL": "Color/Color 6", "NAME": "grad_color6", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
		{ "LABEL": "Color/Color 7", "NAME": "grad_color7", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
		{ "LABEL": "Color/Color 8", "NAME": "grad_color8", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
		{ "LABEL": "Color/Color 9", "NAME": "grad_color9", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },
		{ "LABEL": "Color/Color 10", "NAME": "grad_color10", "TYPE": "color", "DEFAULT": [ 0.027, 0.64, 0.72, 1.0 ] },

        { "LABEL": "Color/BPM Sync", "NAME": "grad_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Color/BPM Phase Sync", "NAME": "grad_bpm_phase_sync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },


        { "LABEL": "Color/Reverse", "NAME": "grad_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Color/Speed", "NAME": "grad_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.5 },

        { "LABEL": "Color/Offset", "NAME": "grad_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "LABEL": "Color/Strob", "NAME": "grad_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 },

        { "LABEL": "Color/Decay", "NAME": "grad_decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Release", "NAME": "grad_release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

        { "LABEL": "Color/Shape", "NAME": "mat_anim2_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear"], "DEFAULT": "Smooth" },

        { "LABEL": "Color/Repeat", "NAME": "grad_repeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 16 },

        { "LABEL": "Color/Rotate", "NAME": "grad_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Scale", "NAME": "grad_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Color/Size", "NAME": "grad_size", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },

        { "LABEL": "Color/Blend Curve", "NAME": "grad_curve", "TYPE": "float", "DEFAULT": 1, "MIN": 1, "MAX": 10 },
        { "LABEL": "Color/Mode", "NAME": "grad_mode", "TYPE": "long", "VALUES": ["Linear","Circular"], "DEFAULT": "Linear" },

		{ "LABEL": "Color/Match Shape Offset", "NAME": "grad_match_offset", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Color/Match Shape Direction", "NAME": "grad_match_direction", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
		{ "LABEL": "Color/Match Line Tilt", "NAME": "grad_match_tilt", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }


	],
    "GENERATORS": [
        { "NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "strob": "bpm_strob", "bpm_sync": "bpmsync", "speed_curve":2, "bpm_phase_sync" : "bpm_phase_sync", "link_speed_to_global_bpm":true, "reset" : "restart"} },
		 {
            "NAME": "strob_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "strob_speed", "strob": "strob_bpm_strob", "bpm_sync": "strob_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "reset" : "restart"}
        },
		{ "NAME": "grad_time", "TYPE": "time_base", "PARAMS": {"speed": "grad_speed", "reverse": "grad_reverse", "strob": "grad_strob", "bpm_sync": "grad_bpmsync", "bpm_phase_sync" : "grad_bpm_phase_sync", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ]
}*/

#include "MadCommon.glsl"

// #define PI 3.14159265359
#define TWO_PI 6.28318530718


// procedural white noise
float hash( vec2 p ) {
    return fract(sin(dot(p,vec2(127.1,311.7)))*43758.5453);
}

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// vec3 hsv2rgb(vec3 c)
// {
//     vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
//     vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
//     return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
// }


float adjusted_time(float time, float decay, float release)
{

    float adjusted_time;

    if (time < decay) {
        adjusted_time = 1;
    } else {
        // get back a value from 0-1 (from end of decay to 1 - end of beat)
        adjusted_time = (time - decay) * 1 / (1 - decay);
        if (time < release) {
            adjusted_time = 1 - adjusted_time * 1 / release;
        } else {
            adjusted_time = 0;
        }
    }

    return adjusted_time;

}

vec4 materialColorForPixel(vec2 texCoord)
{

    float audio_value = 0.0;
    float bass_value = 0.3 * master_scale * bass_level * IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r;
    float medium_value = 0.3 * master_scale * medium_level * IMG_NORM_PIXEL(spectrum,vec2(0.5,0)).r;
    float treble_value = 0.3 * master_scale * treble_level * IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r;
    audio_value += bass_value;
    audio_value += medium_value;
    audio_value += treble_value;


	if (line_width==0) return vec4(0,0,0,1);


	// Mirror all
	#if defined(mirror_x_IS_Mirror_All)
		if (texCoord.x > 0.5) {
			texCoord.x = 1.0-texCoord.x;
		}
	#endif


    #if defined(mirror_y_IS_Mirror_All)
        if (texCoord.y > 0.5) {
            texCoord.y = 1.0-texCoord.y;
        }
    #endif

    texCoord.x -= slide_x;
    texCoord.y -= slide_y;

	// Global rotate
	texCoord = rotate2D(texCoord,PI*rotate);

	float pixelPos=mod(texCoord.x*cells_x,1);
	vec2 cellId = vec2(int(texCoord.x * cells_x), int(texCoord.y * cells_y));

    float normalizedCellId; // Value from 0.0 to 1.0
    if (random_offset)
        normalizedCellId = hash(cellId * 0.5);
    else
        normalizedCellId = (cellId.x + cellId.y*cells_x) / (cells_x*cells_y); // Number from 0.0 to 1.0


    float animation_time_strobed = animation_time;

    bool adjusted_strobe_on = strobe_on;

    if (strobe_audio_enable && strobe_on) {

        if (audio_value < strobe_audio_thresh) {
            adjusted_strobe_on = false;
        }

    }


    if (adjusted_strobe_on) {
        // stick to one of $strobe_div$ divisions in incoming time signal, between 0 and 1
        // ex. for 4 divisions, the time output will be 0(1.0), 0.25, 0.5, or 0.75, assuming no decay/release/timeoffset is used
        // strobe_offset adjusts *where* those divisions lie

        animation_time_strobed = (round(strobe_div * animation_time_strobed) + strobe_offset) / strobe_div;

        // animation_time_strobed = mod(animation_time_strobed, 1 / strobe_div);
    }


	float adjusted_bpm_pos;
	float adjusted_animation_time = mod(animation_time_strobed + bpm_offset + bpm_offset_alt,1);
	float adjusted_offset = offset;
	float adjusted_grad_time = grad_time;
	float adjusted_line_rotate = line_rotate;
	float adjusted_grad_offset = grad_offset;

	// Alternate direction in every other Y cell
	if (alternate_direction) {
		if (mod(texCoord.y, 2.0/cells_y) > 1.0/(cells_y)) {
			adjusted_animation_time = 1-adjusted_animation_time;

			if (grad_match_direction) {
				adjusted_grad_time = 1-grad_time;
			}
		}
	}

	// Vary between a non-normalized offset and no offset, across Y cells
	if (alternate_offset) {
		if (mod(texCoord.y, 2.0/cells_y) > 1.0/(cells_y)) {
			adjusted_offset = 0.0;
		}

		adjusted_bpm_pos = mod(adjusted_animation_time - adjusted_offset,1);

		if (grad_match_offset) {
			adjusted_grad_offset -= adjusted_offset;
		}


	} else {

		adjusted_bpm_pos = mod(adjusted_animation_time - adjusted_offset * normalizedCellId,1);

		adjusted_grad_offset -= adjusted_offset * normalizedCellId;
		//if ((mod(texCoord.y, 2.0/cells_y) > 1.0/(cells_y)) && grad_match_offset) {
		//	adjusted_grad_offset -= offset * normalizedCellId;
		//	adjusted_grad_offset = fract(adjusted_grad_offset);
		//}
	}

	// Mirror shape only
	#if defined(mirror_x_IS_Mirror_Shape)

		if (texCoord.x > 0.5) {
			adjusted_bpm_pos = 1-adjusted_bpm_pos;
			//adjusted_grad_time = 1-adjusted_grad_time;
			adjusted_line_rotate =1-adjusted_line_rotate;
		}
	#endif

    #if defined(mirror_y_IS_Mirror_Shape)

        if (texCoord.y > 0.5) {
            adjusted_bpm_pos = 1-adjusted_bpm_pos;
            //adjusted_grad_time = 1-adjusted_grad_time;
            adjusted_line_rotate =1-adjusted_line_rotate;
        }
    #endif

    adjusted_bpm_pos = adjusted_time(adjusted_bpm_pos, decay, release);


	float strob_adjusted_bpm_pos = mod(strob_time + strob_bpm_offset,1); // Get full value on beat


    strob_adjusted_bpm_pos = adjusted_time(strob_adjusted_bpm_pos, strob_decay, strob_release);


    if (strob_shape == 0) { // Out
        strob_adjusted_bpm_pos = strob_adjusted_bpm_pos;
    } else if (strob_shape == 1) { // In
        strob_adjusted_bpm_pos = 1-strob_adjusted_bpm_pos;
    } else { // Smooth
        strob_adjusted_bpm_pos = (1+sin((-0.25+strob_adjusted_bpm_pos)*2*PI))/2;
    }

	float widthValue = 1.0;
	float minSmoothness = 0.001 * repeat;
    float finalSmoothness = minSmoothness + smoothness/2;
    float halfFinalWidth = line_width * widthValue / 2;

	float a = (adjusted_line_rotate) * PI;
	float edge_adjustment = mod(texCoord.y, 2.0/cells_y) * cos(a);

	float value;

	#if defined(animation_IS_Cross)

		edge_adjustment = 0.0; // disable for now, since it makes the lines rotate when they turn around
		value = smoothstep(line_width,line_width*(1-smoothness),abs(pixelPos-adjusted_bpm_pos - edge_adjustment));
		value += smoothstep(line_width,line_width*(1-smoothness),abs((1-pixelPos)-adjusted_bpm_pos - edge_adjustment));
	#elif defined(animation_IS_Centered_Filled)
		value = smoothstep(0,-smoothness,(2*abs(0.5-pixelPos)-adjusted_bpm_pos-edge_adjustment));
	#elif defined(animation_IS_Single)

		float dist=fract((pixelPos  - adjusted_bpm_pos - edge_adjustment) *repeat + halfFinalWidth) - halfFinalWidth;

		// Be sure we can fill the screen with lines (when width==1) even with this min smoothness (that avoids aliasing on edges)
		dist *= (1-2*minSmoothness);

		int back_orig = back_edge_mode;
		int front_orig = front_edge_mode;

		int adjusted_front_edge_mode = front_edge_mode;
		int adjusted_back_edge_mode = back_edge_mode;

		if (alternate_edges && (mod(texCoord.y, 2.0/cells_y) > 1.0/(cells_y))) {
			adjusted_front_edge_mode = back_orig;
			adjusted_back_edge_mode = front_orig;
		}

		if (dist>0) {
			if (adjusted_back_edge_mode==0) {
				value=1-smoothstep(halfFinalWidth-finalSmoothness,halfFinalWidth,dist);
			} else if (adjusted_back_edge_mode==1) {
				value=halfFinalWidth;
			} else {
				value=1-dist;
			}

		} else {

			if (adjusted_front_edge_mode==0) {
				value=1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);
			} else if (adjusted_front_edge_mode==1) {
				value=-halfFinalWidth;
			} else {
				value=1-dist;
			}


			//value=1-smoothstep(-halfFinalWidth+finalSmoothness,-halfFinalWidth,dist);
		}

    #elif defined(animation_IS_Fill_Up)
        float diff = surf_anim1_limit*anim_pos-(1-pixelPos/surf_anim1_limit);
        //float value = smoothstep(0,0,-diff);

        //diff = clamp(diff, 1-surf_anim1_limit,1.0);
        value = step(diff,0);

    #elif defined(animation_IS_Fill_Down)     // TODO: use limit for other side too
        float diff = adjusted_bpm_pos-pixelPos;
        value = step(diff,0);

    #elif defined(animation_IS_Empty_Up)
        float diff = (1-pixelPos)-adjusted_bpm_pos;
        value = smoothstep(0,0,-diff);


    #elif defined(animation_IS_Empty_Down)     // TODO: use limit for other side too
        float diff = pixelPos-adjusted_bpm_pos;
        value = step(diff,0);

    #elif defined(animation_IS_Fill_Sin_Up)

        float diff = pixelPos-(1+sin(adjusted_bpm_pos*2*PI))/2;
        value = step(diff,0);

    #elif defined(animation_IS_Fill_Sin_Down) // TODO: use limit for other side too
        float diff = (1+sin(adjusted_bpm_pos*2*PI))/2-pixelPos;
        value = step(diff,0);

    #elif defined(animation_IS_Chase)
        // float a = surf_anim1_bar_width*0.5;
        // float diff = abs(pixelPos-0.5-sin(anim_pos*2*PI)/(4*1/surf_anim1_limit));
        // float value = step(min(diff,diff),0.25*(surf_anim1_bar_width+0.5));


        float diff = abs(pixelPos-0.5-sin(adjusted_bpm_pos*2*PI)/4);
        value = step(min(diff,diff),0.25*(line_width+0.5));


    #elif defined(animation_IS_Centered)
            float diff = abs(pixelPos-0.5);
            float value = step(diff,line_width*adjusted_bpm_pos/2);

    #elif defined(animation_IS_Centered_Sin)
        float diff = abs(pixelPos-0.5);
        value = step(diff,(1+sin(adjusted_bpm_pos*2*PI))/4);






	#elif defined(animation_IS_Filled)
		//value = (pixelPos < adjusted_bpm_pos)?1:0;
		value = smoothstep(0,-smoothness,pixelPos-adjusted_bpm_pos);

    #else // None

        value = 1.0;
	#endif


    // if ((mod(texCoord.y, 2.0/cells_y) < border_y)) {
    //     value = 0.0;
    // }

    if ((mod(texCoord.y, 2.0/cells_y) < border_y / 2.) || (mod(texCoord.y, 2.0/cells_y) > 1. - border_y / 2.)) {
        value = 0.0;
    }

    if ((mod(texCoord.x, 2.0/cells_x) < border_x / 2.) || (mod(texCoord.x, 2.0/cells_x) > 1. - border_x / 2.)) {
        value = 0.0;
    }





	vec4 backColor = vec4(backgroundColor.rgb * brightness, backgroundColor.a);
	vec4 frontColor = vec4(foregroundColor.rgb * brightness, foregroundColor.a);
	//float color =  mix(backColor,frontColor*value,strob_adjusted_bpm_pos);

    adjusted_grad_time = mod(adjusted_grad_time - grad_offset, 1);

    adjusted_grad_time = adjusted_time(adjusted_grad_time, grad_decay, grad_release);



	float angle = (grad_rotate) * 2*PI / 360;
    float sin_factor = sin(angle);
    float cos_factor = cos(angle);
    vec2 uv = ((texCoord-vec2(0.5)) * mat2(cos_factor, sin_factor, -sin_factor, cos_factor));
    float color_pos;
    if (grad_mode==0)
        color_pos = uv.x;
    else
        color_pos = -length(uv);
    color_pos *= pow(grad_scale,3) + grad_size*grad_size;
	color_pos = fract(color_pos);
    //pos += 0.5 + adjusted_grad_offset + adjusted_grad_time;
	// color_pos += adjusted_grad_offset + adjusted_grad_time;

    color_pos += adjusted_grad_time;

	if (grad_match_tilt) {
		color_pos -= edge_adjustment;
	}
	color_pos = fract(color_pos*grad_repeat);
	//pos *= pow(grad_scale,3);
    //pos = fract(pos);

    vec4 colors[10];
    colors[0] = grad_color1;
    colors[1] = grad_color2;
    colors[2] = grad_color3;
    colors[3] = grad_color4;
    colors[4] = grad_color5;

	colors[5] = grad_color6;
    colors[6] = grad_color7;
    colors[7] = grad_color8;
    colors[8] = grad_color9;
    colors[9] = grad_color10;

    float stepSize = 1.0/grad_color_count;
    int stepId = int(color_pos/stepSize);
    //return mix(colors[stepId%grad_color_count],colors[(stepId+1)%grad_color_count],pow(pos/stepSize-stepId,pow(grad_curve,6)));

	// if (!strobe) {
	// 	strob_adjusted_bpm_pos = 1.0;
	// }

	vec4 color;

	#if defined(grad_type_IS_Auto_Color_Cycle)
		color = vec4(hsv2rgb(vec3(color_pos, 1.0, 1.0)), 1.0);
	#elif defined(grad_type_IS_Custom_Gradient)
		color = mix(colors[stepId%grad_color_count],colors[(stepId+1)%grad_color_count], pow(color_pos/stepSize-stepId, pow(grad_curve,6)));
	#else // Custom Cycle
        color = colors[stepId%grad_color_count];
    #endif

    color = mix(frontColor, color, grad_level);

    float strobe_value;

    strobe_value = mix(1.0, 1.0 * strob_adjusted_bpm_pos, strobe_level);

    color.rgb *= strobe_value;

    // color.a = 1.0;


	return mix(backgroundColor, color * brightness, value);
}