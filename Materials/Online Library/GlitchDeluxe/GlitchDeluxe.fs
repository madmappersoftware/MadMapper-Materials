/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers, original by nshelton",
    "DESCRIPTION": "Glitch filter, with amplitude controlled by BPM and/or audio levels.",
    "TAGS": "converted_from_isf",
    "VSN": "1.0",

    "IMPORTED" : [
    {
      "NAME" : "iChannel1",
      "PATH" : "0a40562379b63dfb89227e6d172f39fdce9022cba76623f1054a2c83d6c0ba5d.png"
    }
  ],


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
            "DEFAULT": 1.0
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


float rand(float n){return fract(sin(n) * 43758.5453123);}

float noise(float p){
    float fl = floor(p);
    float fc = fract(p);
    return mix(rand(fl), rand(fl + 1.0), fc);
}

float blockyNoise(vec2 uv, float threshold, float scale, float seed)
{
    float scroll = floor(mat_time + sin(11.0 *  mat_time) + sin(mat_time) ) * 0.77;
    vec2 noiseUV = uv.yy / scale + scroll;
    float noise2 = IMG_NORM_PIXEL(iChannel1,mod(noiseUV,1.0)).r;

    float id = floor( noise2 * 20.0);
    id = noise(id + seed) - 0.5;


    if ( abs(id) > threshold )
        id = 0.0;

    return id;
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


    // originals:


    #if defined(mat_move_type_IS_Manual)

        float rgbIntesnsity = 0.1 + 0.1 * sin(mat_time* 3.7);
        float displaceIntesnsity = 0.2 +  0.3 * pow( sin(mat_time * 1.2), 5.0);

        rgbIntesnsity *= amount;
        displaceIntesnsity *= amount;

    #else
    // revised:
        float rgbIntesnsity = amount * 0.1 + 0.1 * amount * 3.7;
        float displaceIntesnsity = amount * 0.2 +  0.3 * pow(amount * 1.2, 5.0);
    #endif


    float interlaceIntesnsity = 0.01;
    float dropoutIntensity = 0.1;


    // rgbIntesnsity *= amount;
    // displaceIntesnsity *= amount;
    // interlaceIntesnsity *= amount;
    // dropoutIntensity *= amount;

    vec2 uv = texCoord;
    float displace = blockyNoise(uv + vec2(uv.y, 0.0), displaceIntesnsity, 25.0, 66.6);
    displace *= blockyNoise(uv.yx + vec2(0.0, uv.x), displaceIntesnsity, 111.0, 13.7);

    uv.x += displace ;

    vec2 offs = 0.1 * vec2(blockyNoise(uv.xy + vec2(uv.y, 0.0), rgbIntesnsity, 65.0, 341.0), 0.0);

    float colr = IMG_NORM_PIXEL(mat_input_image,mod(uv-offs,1.0)).r;
    float colg = IMG_NORM_PIXEL(mat_input_image,mod(uv,1.0)).g;
    float colb = IMG_NORM_PIXEL(mat_input_image,mod(uv +offs,1.0)).b;

    float line = fract(texCoord.y / 3.0);
    vec3 mask = vec3(3.0, 0.0, 0.0);
        if (line > 0.333)
            mask = vec3(0.0, 3.0, 0.0);
        if (line > 0.666)
            mask = vec3(0.0, 0.0, 3.0);


    float maskNoise = blockyNoise(uv, interlaceIntesnsity, 90.0, mat_time) * max(displace, offs.x);

    maskNoise = 1.0 - maskNoise;
    if ( maskNoise == 1.0)
        mask = vec3(1.0);

    float dropout = blockyNoise(uv, dropoutIntensity, 11.0, mat_time) * blockyNoise(uv.yx, dropoutIntensity, 90.0, mat_time);
    mask *= (1.0 - 5.0 * dropout);




    return mix(IMG_NORM_PIXEL(mat_input_image, texCoord), vec4(mask * vec3(colr, colg, colb), 1.0), filter_mix);





}