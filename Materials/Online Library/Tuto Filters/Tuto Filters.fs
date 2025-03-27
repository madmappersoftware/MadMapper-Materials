/*{
  "CREDIT": "Mad Team",
  "DESCRIPTION": "Filters Demo",
  "VSN": "1.0",
  "TAGS": "demo",
  "INPUTS": [
    { "LABEL": "TimeBase/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 0.25 },
    { "LABEL": "Animator/Shape", "NAME": "mat_animator_shape", "TYPE": "long", "VALUES": ["Smooth","In","Out","Linear","Cut","Noise"], "DEFAULT": "Smooth" },
    { "LABEL": "Damper/Hardness", "NAME": "mat_damper_hardness", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.2 },
    { "LABEL": "Damper/Damping", "NAME": "mat_damper_damping", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },
    { "LABEL": "ADSR/Attack", "NAME": "mat_adsr_attack", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.4 },
    { "LABEL": "ADSR/Decay", "NAME": "mat_adsr_decay", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.0 },
    { "LABEL": "ADSR/Release", "NAME": "mat_adsr_release", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.4 },
    { "LABEL": "Linear/Duration", "NAME": "mat_linear_duration", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 1 },
    { "LABEL": "Incrementer/Increment", "NAME": "mat_incrementer_plus_one", "TYPE": "event" },
    { "LABEL": "Incrementer/Decrement", "NAME": "mat_incrementer_less_one", "TYPE": "event" },
    { "LABEL": "Ease/Duration", "NAME": "mat_ease_duration", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 1 },
    { "LABEL": "Ease/Curve", "NAME": "mat_ease_curve", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 3 },
    { "LABEL": "Ease/Type", "NAME": "mat_ease_type", "TYPE": "long", "DEFAULT": "EaseInOut", "VALUES": ["EaseInOut","EaseIn","EaseOut"] },
  ],
  "GENERATORS": [
    { "NAME": "mat_time_base",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_speed",
              "reverse": false,
              "speed_curve": 3,
              "strob": 0,
              "bpm_sync": false,
              "link_speed_to_global_bpm": false }
    },
    { "NAME": "mat_animator",
      "TYPE": "animator",
      "PARAMS": {"speed": "mat_speed",
              "shape": "mat_animator_shape",
              "reverse": false,
              "speed_curve": 3,
              "strob": 0,
              "bpm_sync": false,
              "link_speed_to_global_bpm": false }
    },
    { "NAME": "mat_damper",
      "TYPE": "damper",
      "PARAMS": {"input_value": "mat_animator",
                 "hardness": "mat_damper_hardness",
                 "damping": "mat_damper_damping" }
    },
    { "NAME": "mat_adsr",
      "TYPE": "adsr",
      "PARAMS": {"input_value": "mat_animator",
                 "attack": "mat_adsr_attack",
                 "release":  "mat_adsr_release",
                 "decay": "mat_adsr_decay" }
    },
    { "NAME": "mat_linear",
      "TYPE": "linear_filter",
      "PARAMS": {"input_value": "mat_animator",
                 "duration": "mat_linear_duration"}
    },
    { "NAME": "mat_multiplier",
      "TYPE": "multiplier",
      "PARAMS": {"value1": "mat_animator",
                 "value2": 0.5,
                 "value3": 1,
                 "value4": 1}
    },
    { "NAME": "mat_incrementer",
      "TYPE": "incrementer",
      "PARAMS": {"increment": "mat_incrementer_plus_one",
                 "decrement": "mat_incrementer_less_one"}
    },
    { "NAME": "mat_ease",
      "TYPE": "ease_filter",
      "PARAMS": {"input_value": "mat_incrementer",
                 "type": "mat_ease_type",
                 "duration": "mat_ease_duration",
                 "curve": "mat_ease_curve"}
    },
  ]
}*/


vec4 materialColorForPixel(vec2 texCoord)
{
    int filterNum = int(texCoord.y * 8);

	if (filterNum == 0) {
		// Just time_base
		return abs(fract(mat_time_base) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 1) {
		// animator (time base with a shape)
		return abs(fract(mat_animator) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 2) {
		// damper
		return abs(fract(mat_damper) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 3) {
		// adsr
		return abs(fract(mat_adsr) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 4) {
		// linear_filter
		return abs(fract(mat_linear) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 5) {
		// multiplier
		return abs(fract(mat_multiplier) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 6) {
		// incrementer
		return abs(fract(mat_incrementer/10) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else if (filterNum == 7) {
		// ease_filter
		return abs(fract(mat_ease/10) - texCoord.x)<0.01 ? vec4(1) : vec4(0);
	} else {
		return vec4(0);
	}
}
