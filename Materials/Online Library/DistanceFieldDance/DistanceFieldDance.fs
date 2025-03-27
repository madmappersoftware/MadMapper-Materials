/*
{
  "CATEGORIES" : [
    "Shadertoy"
  ],
  "CREDIT": "Sleng; adapted by Jason Beyers",
  "DESCRIPTION" : "Adapted from https:\/\/www.shadertoy.com\/view\/XttBRX by Sleng.  Variation of distance field explanation from https:\/\/thebookofshaders.com\/07\/?lan=ru.  NOTE: avoid animating rotation and position at the same time.",
  "VSN": "1.0",
  "INPUTS" : [


    {
        "LABEL": "Scale/Animate Scale",
        "NAME": "mat_animate_scale",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scale/Shape",
        "NAME": "mat_scale_shape",
        "TYPE": "long",
        "VALUES": ["Linear","In","Out","Smooth"], "DEFAULT": "Smooth"
    },

    {
        "LABEL": "Scale/BPM Sync",
        "NAME": "mat_scale_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scale/Reverse",
        "NAME": "mat_scale_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scale/Speed",
        "NAME": "mat_scale_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Scale/Amount",
        "NAME": "mat_scale_amount",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Scale/Range",
        "NAME": "mat_scale_range",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scale/Strob",
        "NAME": "mat_scale_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },






    {
        "LABEL": "Rotate/Animate Rotation",
        "NAME": "mat_animate_rotation",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },

    {
        "LABEL": "Rotate/BPM Sync",
        "NAME": "mat_rot_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Reverse",
        "NAME": "mat_rot_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Speed",
        "NAME": "mat_rot_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Rotate/Amount",
        "NAME": "mat_rot_offset",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Rotate/Strob",
        "NAME": "mat_rot_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Flow/Animate Flow",
        "NAME": "mat_animate_flow",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },

    {
        "LABEL": "Flow/BPM Sync",
        "NAME": "mat_flow_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Reverse",
        "NAME": "mat_flow_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Speed",
        "NAME": "mat_flow_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Flow/Offset",
        "NAME": "mat_flow_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Flow/Strob",
        "NAME": "mat_flow_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "LABEL": "Scroll/Animate Position",
        "NAME": "mat_animate_scroll",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/BPM Sync",
        "NAME": "mat_scroll_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Reverse",
        "NAME": "mat_scroll_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Speed",
        "NAME": "mat_scroll_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },

    {
        "Label": "Scroll/Angle",
        "NAME": "mat_scroll_angle",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "Label": "Scroll/Offset X",
        "NAME": "mat_scroll_offset_x",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Scroll/Offset Y",
        "NAME": "mat_scroll_offset_y",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Scroll/Strob",
        "NAME": "mat_scroll_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Color/Animate Color",
        "NAME": "mat_animate_color",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },

    {
        "LABEL": "Color/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Color/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },






  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_rot_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_rot_speed",
            "speed_curve":2,
            "strob" : "mat_rot_strob",
            "reverse": "mat_rot_reverse",
            "bpm_sync": "mat_rot_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_flow_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_flow_speed",
            "speed_curve":2,
            "strob" : "mat_flow_strob",
            "reverse": "mat_flow_reverse",
            "bpm_sync": "mat_flow_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_scroll_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_scroll_speed",
            "speed_curve":2,
            "strob" : "mat_scroll_strob",
            "reverse": "mat_scroll_reverse",
            "bpm_sync": "mat_scroll_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },

    {
        "NAME": "mat_scale_time",
        "TYPE": "animator",
        "PARAMS": {
            "speed": "mat_scale_speed",
            "speed_curve":2,
            "strob" : "mat_scale_strob",
            "reverse": "mat_scale_reverse",
            "bpm_sync": "mat_scale_bpm_sync",
            "shape": "mat_scale_shape",
            "link_speed_to_global_bpm":true
        }
    }

]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"





// Mark Serdtse
// Variation of distance field explanation
// from https://thebookofshaders.com/07/?lan=ru


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 st = texCoord - vec2(0.5);

    // st.x -= mat_scroll_offset_x;
    // st.y -= mat_scroll_offset_y;

    // st *= mat_zoom;

    // vec2 st = texCoord;

    // st -= center;

    // st.x -= 0.2;

    float scale_time = 0.;
    if (mat_animate_scale) {
        scale_time = mat_scale_time;
    } else {
        scale_time = 1.;
    }
    scale_time *= 2 * mat_scale_amount;

    st *= 2 * mat_scale_amount + (mat_scale_range * scale_time);

    float color_time = 0.;
    if (mat_animate_color) {
        color_time = mat_time;
    }
    color_time -= mat_offset * 10;

    vec3 color = 0.5 + 0.5*cos(color_time+st.xyx+vec3(0,2,4));
    float d = 0.0;

    // st = st *2.-1.;

    float rot_time = 0.;
    float flow_time = 0.;
    float scroll_time = 0.;

    if (mat_animate_rotation) {
        rot_time = mat_rot_time;
    }

    // Add the rotation offset even if rotation is not animated
    rot_time *= 0.1;
    rot_time -= mat_rot_offset;

    if (mat_animate_flow) {
        flow_time = mat_flow_time;
    }

    // Add the flow offset even if flow is not animated
    flow_time -= mat_flow_offset * 10;

    if (mat_animate_scroll) {
        scroll_time = mat_scroll_time;
    }

    float scroll_time_x = scroll_time * cos(PI * mat_scroll_angle);

    float scroll_time_y = scroll_time * sin(PI * mat_scroll_angle);


    st.x -= scroll_time_x + mat_scroll_offset_x;
    st.y -= scroll_time_y + mat_scroll_offset_y;

    // Circular movement
    // st.x -= mat_scroll_offset_x + cos(PI* (scroll_time));
    // st.y -= mat_scroll_offset_y + sin(PI* (scroll_time));

    float sin_factor = sin(PI*rot_time);
    float cos_factor = cos(PI*rot_time);

    st = st* mat2(cos_factor, sin_factor, -sin_factor, cos_factor);

    d = length(abs(sin(abs(st*2.)+flow_time))*(sin(abs(cos(st.x)*sin(st*5.))*.8)/2.));

    float mask = sin(d*50.0);

    color = color*mask;

    return vec4(color,1.0);




}