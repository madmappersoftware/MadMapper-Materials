/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Collectif Scale | Furtive Vision",
    "TAGS": "Carnaval sequencer",
    "DESCRIPTION": "Sequencer, bpm sync, single step",
    "VSN": "0.6",
    "INPUTS": [  
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
				{ "LABEL": "Bpm sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" }, 
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1 },
        { "LABEL": "Attack", "NAME": "mat_attack", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0. },
        { "LABEL": "Decay", "NAME": "mat_decay", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.1 },
        { "LABEL": "Release", "NAME": "mat_release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
        { "LABEL": "Cell X", "NAME": "cell_x", "TYPE": "float", "MIN": 1.0, "MAX": 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Cell Y", "NAME": "cell_y", "TYPE": "float", "MIN": 1.0, "MAX": 10.0, "DEFAULT": 6.0 },

				{ "LABEL": "Shapes/Shape A", "NAME": "mat_A", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
				{ "LABEL": "Shapes/Shape B", "NAME": "mat_B", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
				{ "LABEL": "Shapes/Shape C", "NAME": "mat_C", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
				{ "LABEL": "Shapes/Shape D", "NAME": "mat_D", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
				{ "LABEL": "Shapes/Shape E", "NAME": "mat_E", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
				{ "LABEL": "Shapes/Shape F", "NAME": "mat_F", "TYPE": "bool", "DEFAULT": true, "FLAGS": "" },
    ],
    "GENERATORS": [
			{ "NAME": "mat_global_bpm", "TYPE": "pass_thru", "PARAMS": {"input_value": "/custom/BPM/bpm"}}, //to mult by 1000
			{"NAME": "anim_time", "TYPE": "time_base", "PARAMS": {"speed": "speed" , "reverse": "reverse", "bpm_sync": "mat_bpm_sync", "speed_curve": 1, "link_speed_to_global_bpm":true}},
    ],
    "RASTERISATION_SETTINGS": {
        "DEFAULT_RENDER_TO_TEXTURE": true,
        "DEFAULT_WIDTH": 64,
        "DEFAULT_HEIGHT": 64,
        "DEFAULT_PIXEL_FORMAT": "PF_U8_BGRA",
        "REQUIRES_LAST_FRAME": true
    },
}*/

// #define NOISE_TEXTURE_BASED
precision highp float;

// #define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

float map(float value, float min1, float max1, float min2, float max2) {
	return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
  // return clamp(min2 + (value - min1) * (max2 - min2) / (max1 - min1), min2, max2);
}

float computeStep(float step_value, float attack, float is_attack, float release, float is_release, float is_decay) {
	float value = 0.;

	value += is_attack * step_value * (attack);
	value += is_decay * step_value;
	value -= is_release * step_value * release;

	if(1. - step_value == 1.) {
		value -= release;
	}

	return value;
}

vec4 materialColorForPixel(vec2 texCoord) {

    // first grid
	vec2 posId;
	vec2 pos = texCoord; // 0. -> 1.
	vec2 cells = vec2(cell_x, cell_y);

    // speed 
	float speed = speed;
	float anim_time = anim_time * 1.;

	if(mat_bpm_sync != true) {
		speed *= (mat_global_bpm * 1000.) / 120.;
		anim_time *= 2.;
	} else {
		speed = pow(2., floor(log2(speed))); // todo if < 1
	}

	vec2 is_more_than_2 = step(vec2(2.), cells);
	cells.x = is_more_than_2.x * cells.x + (1. - is_more_than_2.x) * map(cells.x, 1., 2., 0., 2.);
	cells.y = is_more_than_2.y * cells.y + (1. - is_more_than_2.y) * map(cells.y, 1., 2., 0., 2.);

	vec4 lastFrame = texture(mm_LastFrame, vec2(texCoord.xy));
	pos = repeat(pos, vec2(1. / (cells.x), 1. / (cells.y)), posId);

	vec3 color = vec3(0.0);
    // float attack = (1. - mat_attack);
	float attack = 10. * pow(1. - (0.001 + 0.999 * (mat_attack * 0.5)), 3.) * TIMEDELTA * speed;
	float release = 10. * pow(1. - (0.001 + 0.999 * mat_release * .8), 3.) * TIMEDELTA * speed;
	float is_release_null = step(mat_release, 0.);
	float is_attack_null = step(mat_attack, 0.);
	release = is_release_null * 1. + (1. - is_release_null) * release;
	attack = is_attack_null * 1. + (1. - is_attack_null) * attack;

	float step_time = fract(anim_time);
	float attack_duration = speed * TIMEDELTA / (attack);

	float is_attack = step(step_time, attack_duration);
	float is_decay = (1. - step(step_time, attack_duration)) * step(step_time, attack_duration + mat_decay);
	float is_release = (1. - step(step_time, attack_duration + mat_decay)) * step(step_time, 1.);

	color.rgb += lastFrame.rgb;

	if(posId.y == 0) {
		float stepA = float(mat_A);
		color.rgb += computeStep(stepA, attack, is_attack, release, is_release, is_decay);
	}
	if(posId.y == 1) {
		float stepB = float(mat_B);
		color.rgb += computeStep(stepB, attack, is_attack, release, is_release, is_decay);
	}
	if(posId.y == 2) {
		float stepC = float(mat_C);
		color.rgb += computeStep(stepC, attack, is_attack, release, is_release, is_decay);
	}
	if(posId.y == 3) {
		float stepD = float(mat_D);
		color.rgb += computeStep(stepD, attack, is_attack, release, is_release, is_decay);
	}
	if(posId.y == 4) {
		float stepE = float(mat_E);
		color.rgb += computeStep(stepE, attack, is_attack, release, is_release, is_decay);
	}
	if(posId.y == 5) {
		float stepF = float(mat_F);
		color.rgb += computeStep(stepF, attack, is_attack, release, is_release, is_decay);
	}

	color = clamp(color, 0., 1.);

	return vec4(color, 1.0);
}
