/*{
    "CREDIT": "Jason Beyers, original by carter rosenberg",
    "DESCRIPTION": "Dilation effect with audio-reactivity and beat sync, which can be applied to any input.  Converted from ISF, originally by carter rosenberg",
    "TAGS": "converted_from_isf",
    "VSN": "1.0",
    "INPUTS": [

        {   "LABEL" : "Input",
            "NAME": "mat_inputImage",
            "TYPE": "image"
        },

        { "LABEL": "Mix", "NAME": "filter_mix", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

        { "LABEL": "Amplitude", "NAME": "amplitude", "TYPE": "float", "MIN": 0.0, "MAX": 0.2, "DEFAULT": 0.001 },


        { "LABEL": "Auto Move Type", "NAME": "mat_move_type", "TYPE": "long", "VALUES": ["Manual", "Steady", "Steady With Audio","Steady With Bass Audio","Steady With Treble Audio","Audio Only","Bass Audio Only","Treble Audio Only","Audio Multiplied", "Bass Audio Multiplied", "Treble Audio Multiplied",], "DEFAULT": "Manual", "FLAGS": "generate_as_define" },

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

        { "LABEL": "Steady/BPM Sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Steady/Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },

        { "LABEL": "Steady/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },


        { "LABEL": "Steady/Shape", "NAME": "mat_steady_shape", "TYPE": "long", "VALUES": ["In","Out","Smooth"], "DEFAULT": "Smooth" },

        { "Label": "Steady/Offset", "NAME": "mat_offset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

        { "Label": "Steady/Strob", "NAME": "mat_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },


        { "LABEL": "Steady/Decay", "NAME": "mat_decay", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Steady/Release", "NAME": "mat_release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },


        { "LABEL": "Down Right", "NAME": "dilation1", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Down", "NAME": "dilation2", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Down Left", "NAME": "dilation3", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Right", "NAME": "dilation4", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Left", "NAME": "dilation5", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Up Right", "NAME": "dilation6", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Up", "NAME": "dilation7", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Up Left", "NAME": "dilation8", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },


        { "LABEL": "Hue Shift", "NAME": "hue_shift", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

        { "LABEL": "Hue Shfit Amount", "NAME": "hue_shift_amount", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

      ],
     "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "animator", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"strob" : "mat_strob", "bpm_sync": "mat_bpm_sync", "shape": "mat_steady_shape", "link_speed_to_global_bpm":true}},
    ]
}*/

#include "MadCommon.glsl"

vec4 materialColorForPixel( vec2 texCoord ) {

    vec2 texcoord = texCoord;

    float amount;

    float audio_value = 0.0;
    float bass_value = 0.3 * master_scale * bass_level * IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r;
    float medium_value = 0.3 * master_scale * medium_level * IMG_NORM_PIXEL(spectrum,vec2(0.5,0)).r;
    float treble_value = 0.3 * master_scale * treble_level * IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r;
    audio_value += bass_value;
    audio_value += medium_value;
    audio_value += treble_value;

    #if defined(mat_move_type_IS_Steady)
        amount = fract(mat_time - mat_offset) * amplitude;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Audio)
        amount = fract(mat_time - mat_offset) *  audio_value;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Bass_Audio)
        amount = fract(mat_time - mat_offset) * bass_value * 3;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Treble_Audio)
        amount = fract(mat_time - mat_offset) * treble_value * 3;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Audio_Only)
        amount = audio_value;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Bass_Audio_Only)
        amount = bass_value * 3;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Treble_Audio_Only)
        amount = treble_value * 3;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Audio_Multiplied)
        amount = audio_value * amplitude * 5;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Bass_Audio_Multiplied)
        amount = bass_value * 3* amplitude * 5;
        amount *= 0.1;

    #elif defined(mat_move_type_IS_Treble_Audio_Multiplied)
        amount = treble_value * 3* amplitude * 5;
        amount *= 0.1;

    #else // Manual
        amount = amplitude;

    #endif


    // get sample positions
    vec2 texcoord0 = texcoord + vec2(-amount, -amount);
    vec2 texcoord1 = texcoord + vec2( 0,      -amount);
    vec2 texcoord2 = texcoord + vec2( amount, -amount);
    vec2 texcoord3 = texcoord + vec2(-amount,  0);
    vec2 texcoord4 = texcoord + vec2( amount,  0);
    vec2 texcoord5 = texcoord + vec2(-amount,  amount);
    vec2 texcoord6 = texcoord + vec2( 0,       amount);
    vec2 texcoord7 = texcoord + vec2( amount,  amount);

    vec4 dilate = IMG_NORM_PIXEL(mat_inputImage, 0.5 * (texcoord3 + texcoord4));

    // if (dilation1) {

    //     vec4 origin = vec4(1.0);

    //     // float val = 0.5;

    //     origin.rgb = rgb2hsv(IMG_NORM_PIXEL(mat_inputImage, texcoord0).rgb);

    //     origin.rgb = fract(IMG_NORM_PIXEL(mat_inputImage, texcoord0).rgb * hue_shift_amount);
    //     // origin.rgb = hsv2rgb(IMG_NORM_PIXEL(mat_inputImage, texcoord0).rgb);


    //     if (hue_shift) {
    //         dilate = max(dilate, origin);

    //     } else {
    //         dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord0));
    //     }
    // }

    if (dilation1) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord0));

    }

    if (dilation2) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord1));
    }
    if (dilation3) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord2));
    }
    if (dilation4) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord3));
    }
    if (dilation5) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord4));
    }
    if (dilation6) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord5));
    }
    if (dilation7) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord6));
    }
    if (dilation8) {
        dilate = max(dilate, IMG_NORM_PIXEL(mat_inputImage, texcoord7));
    }

    return mix(IMG_NORM_PIXEL(mat_inputImage, texCoord), dilate, filter_mix);
}