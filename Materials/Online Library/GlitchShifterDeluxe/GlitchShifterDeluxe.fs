/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers, original by VIDVOX",
    "DESCRIPTION": "Glitch filter, with amplitude controlled by BPM and/or audio levels.",
    "TAGS": "converted_from_isf",
    "VSN": "1.0",
    "INPUTS": [

        {   "LABEL" : "Input",
            "NAME": "mat_input_image",
            "TYPE": "image"
        },
        {
            "LABEL": "Mix",
            "NAME": "filter_mix",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Amplitude",
            "NAME": "amplitude",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.001
        },


        {
            "NAME": "glitch_size",
            "LABEL": "Size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.5,
            "DEFAULT": 0.1
        },
        {
            "NAME": "glitch_horizontal_factor",
            "LABEL": "Horizontal Factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.2
        },
        {
            "NAME": "glitch_vertical_factor",
            "LABEL": "Vertical Factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "randomize_size",
            "LABEL": "Randomize Size",
            "TYPE": "bool",
            "DEFAULT": 1.0
        },
        {
            "NAME": "randomize_zoom",
            "LABEL": "Randomize Zoom",
            "TYPE": "bool",
            "DEFAULT": 0.0
        },
        {
            "NAME": "offset",
            "LABEL": "Offset",
            "TYPE": "point2D",
            "DEFAULT": [
                0,
                0
            ]
        },







        {
            "LABEL": "Auto Move Type",
            "NAME": "mat_move_type",
            "TYPE": "long",
            "VALUES": ["Manual", "Steady", "Steady With Audio","Steady With Bass Audio","Steady With Treble Audio","Audio Only","Bass Audio Only","Treble Audio Only","Audio Multiplied", "Bass Audio Multiplied", "Treble Audio Multiplied",],
            "DEFAULT": "Manual",
            "FLAGS": "generate_as_define"
        },
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
        {
            "LABEL": "Steady/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Steady/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Steady/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Steady/Shape",
            "NAME": "mat_steady_shape",
            "TYPE": "long",
            "VALUES": ["In","Out","Smooth"], "DEFAULT": "Smooth"
        },
        {
            "Label": "Steady/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Steady/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Steady/Decay",
            "NAME": "mat_decay",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Steady/Release",
            "NAME": "mat_release",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

      ],
     "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "animator",
            "PARAMS": {
                "speed": "mat_speed", "speed_curve":2,"strob" : "mat_strob", "bpm_sync": "mat_bpm_sync", "shape": "mat_steady_shape", "link_speed_to_global_bpm":true
            }
        },
    ]
}*/


vec3 hsv2rgb(vec3 c)
{
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

vec3 rgb2hsv(vec3 c)    {
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    //vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    //vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));
    vec4 p = c.g < c.b ? vec4(c.bg, K.wz) : vec4(c.gb, K.xy);
    vec4 q = c.r < p.x ? vec4(p.xyw, c.r) : vec4(c.r, p.yzx);

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}


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
        // amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Audio)
        amount = fract(mat_time - mat_offset) *  audio_value * amplitude;
        // amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Bass_Audio)
        amount = fract(mat_time - mat_offset) * bass_value * 3 * amplitude;
        // amount *= 0.1;

    #elif defined(mat_move_type_IS_Steady_With_Treble_Audio)
        amount = fract(mat_time - mat_offset) * treble_value * 3 * amplitude;
        // amount *= 0.1;


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



    vec2 xy;
    xy.x = texCoord.x;
    xy.y = texCoord.y;

    //  quantize the xy to the glitch_amount size
    //xy = floor(xy / glitch_size) * glitch_size;
    vec2 random;

    float local_glitch_size = glitch_size;
    float random_offset = 0.0;


    float glitch_horizontal = amount * glitch_horizontal_factor;

    float glitch_vertical = amount * glitch_vertical_factor;

    if (randomize_size) {
        random_offset = mod(rand(vec2(mat_time,mat_time)), 1.0);
        local_glitch_size = random_offset * glitch_size;
    }

    if (local_glitch_size > 0.0)    {
        random.x = rand(vec2(floor(random_offset + xy.y / local_glitch_size) * local_glitch_size, mat_time));
        random.y = rand(vec2(floor(random_offset + xy.x / local_glitch_size) * local_glitch_size, mat_time));
    }
    else    {
        random.x = rand(vec2(xy.x, mat_time));
        random.y = rand(vec2(xy.y, mat_time));
    }

    if (randomize_zoom) {
        if ((random.x < glitch_horizontal)&&(random.y < glitch_vertical))   {
            float level = rand(vec2(random.x, random.y)) / 5.0 + 0.90;
            // level *= glitch_size;

            // level = 0.;
            xy = (xy - vec2(0.5))*(1.0/level) + vec2(0.5);
        }
        else if (random.x < glitch_horizontal)  {
            float level = (random.x) + 0.98;
            // level *= glitch_size;
            // level = 0.;
            xy = (xy - vec2(0.5))*(1.0/level) + vec2(0.5);
        }
        else if (random.y < glitch_vertical)    {
            float level = (random.y) + 0.98;

            level *= glitch_size;
            xy = (xy - vec2(0.5))*(1.0/level) + vec2(0.5);
        }
    }

    //  if doing a horizontal glitch do a random shift
    if ((random.x < glitch_horizontal)&&(random.y < glitch_vertical))   {
        vec2 shift = (offset - 0.5);
        shift = shift * rand(shift + random);
        xy.x = mod(xy.x + random.x, 1.0);
        xy.y = mod(xy.y + random.y, 1.0);
        xy = xy + shift;
    }
    else if (random.x < glitch_horizontal)  {
        vec2 shift = (offset - 0.5);
        shift = shift * rand(shift + random);
        xy = mod(xy + vec2(0.0, random.x) + shift, 1.0);
    }
    else if (random.y < glitch_vertical)    {
        vec2 shift = (offset - 0.5);
        shift = shift * rand(shift + random);
        xy = mod(xy + vec2(random.y, 0.0) + shift, 1.0);
    }

    return mix(IMG_NORM_PIXEL(mat_input_image, texCoord), IMG_NORM_PIXEL(mat_input_image, xy), filter_mix);





}