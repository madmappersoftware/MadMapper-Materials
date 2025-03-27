/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers, original by VIDVOX",
    "DESCRIPTION": "Glitch effect with audio-reactivity and beat sync, which can be applied to any input.",
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


float sat( float t ) {
    return clamp( t, 0.0, 1.0 );
}

vec2 sat( vec2 t ) {
    return clamp( t, 0.0, 1.0 );
}

//remaps inteval [a;b] to [0;1]
float remap  ( float t, float a, float b ) {
    return sat( (t - a) / (b - a) );
}

//note: /\ t=[0;0.5;1], y=[0;1;0]
float linterp( float t ) {
    return sat( 1.0 - abs( 2.0*t - 1.0 ) );
}

vec3 spectrum_offset( float t ) {
    vec3 ret;
    float lo = step(t,0.5);
    float hi = 1.0-lo;
    float w = linterp( remap( t, 1.0/6.0, 5.0/6.0 ) );
    float neg_w = 1.0-w;
    ret = vec3(lo,1.0,hi) * vec3(neg_w, w, neg_w);
    return pow( ret, vec3(1.0/2.2) );
}

//note: [0;1]
float rand( vec2 n ) {
  return fract(sin(dot(n.xy, vec2(12.9898, 78.233)))* 43758.5453);
}

//note: [-1;1]
float srand( vec2 n ) {
    return rand(n) * 2.0 - 1.0;
}

float mytrunc( float x, float num_levels )
{
    return floor(x*num_levels) / num_levels;
}
vec2 mytrunc( vec2 x, float num_levels )
{
    return floor(x*num_levels) / num_levels;
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


    vec2 uv = texCoord;

    float time = mod(mat_time*100.0, 32.0)/110.0; // + modelmat[0].x + modelmat[0].z;
    float GLITCH = amount;

    float gnm = sat( GLITCH );
    float rnd0 = rand( mytrunc( vec2(time, time), 6.0 ) );
    float r0 = sat((1.0-gnm)*0.7 + rnd0);
    float rnd1 = rand( vec2(mytrunc( uv.x, 10.0*r0 ), time) ); //horz
    //float r1 = 1.0f - sat( (1.0f-gnm)*0.5f + rnd1 );
    float r1 = 0.5 - 0.5 * gnm + rnd1;
    r1 = 1.0 - max( 0.0, ((r1<1.0) ? r1 : 0.9999999) ); //note: weird ass bug on old drivers
    float rnd2 = rand( vec2(mytrunc( uv.y, 40.0*r1 ), time) ); //vert
    float r2 = sat( rnd2 );
    float rnd3 = rand( vec2(mytrunc( uv.y, 10.0*r0 ), time) );
    float r3 = (1.0-sat(rnd3+0.8)) - 0.1;
    float pxrnd = rand( uv + time );
    float ofs = 0.05 * r2 * GLITCH * ( rnd0 > 0.5 ? 1.0 : -1.0 );
    ofs += 0.5 * pxrnd * ofs;
    uv.y += 0.1 * r3 * GLITCH;
    const int NUM_SAMPLES = 20;
    const float RCP_NUM_SAMPLES_F = 1.0 / float(NUM_SAMPLES);

    vec4 sum = vec4(0.0);
    vec3 wsum = vec3(0.0);
    for( int i=0; i<NUM_SAMPLES; ++i )
    {
        float t = float(i) * RCP_NUM_SAMPLES_F;
        uv.x = sat( uv.x + ofs * t );
        vec4 samplecol = IMG_NORM_PIXEL(mat_input_image,mod(uv,1.0));
        vec3 s = spectrum_offset( t );
        samplecol.rgb = samplecol.rgb * s;
        sum += samplecol;
        wsum += s;
    }
    sum.rgb /= wsum;
    sum.a *= RCP_NUM_SAMPLES_F;


    return mix(IMG_NORM_PIXEL(mat_input_image, texCoord), sum, filter_mix);





}