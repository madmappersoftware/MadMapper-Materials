/*{
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


// vec3 hsv2rgb(vec3 c)
// {
//     vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
//     vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
//     return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
// }

// vec3 rgb2hsv(vec3 c)    {
//     vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
//     //vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
//     //vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));
//     vec4 p = c.g < c.b ? vec4(c.bg, K.wz) : vec4(c.gb, K.xy);
//     vec4 q = c.r < p.x ? vec4(p.xyw, c.r) : vec4(c.r, p.yzx);

//     float d = q.x - min(q.w, q.y);
//     float e = 1.0e-10;
//     return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
// }

float sat( float t ) {
    return clamp( t, 0.0, 1.0 );
}
vec2 sat( vec2 t ) {
    return clamp( t, 0.0, 1.0 );
}
// vec3 sat( vec3 v ) {
    // return clamp( v, 0.0f, 1.0f );
// }

//remaps inteval [a;b] to [0;1]
float remap( float t, float a, float b ) {
    return sat( (t - a) / (b - a) );
}

//note: /\ t=[0;0.5;1], y=[0;1;0]
float linterp( float t ) {
    return sat( 1.0 - abs( 2.0*t - 1.0 ) );
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
vec2 mytrunc( vec2 x, vec2 num_levels )
{
    return floor(x*num_levels) / num_levels;
}

vec3 rgb2yuv( vec3 rgb )
{
    vec3 yuv;
    yuv.x = dot( rgb, vec3(0.299,0.587,0.114) );
    yuv.y = dot( rgb, vec3(-0.14713, -0.28886, 0.436) );
    yuv.z = dot( rgb, vec3(0.615, -0.51499, -0.10001) );
    return yuv;
 }
 vec3 yuv2rgb( vec3 yuv )
 {
    vec3 rgb;
    rgb.r = yuv.x + yuv.z * 1.13983;
    rgb.g = yuv.x + dot( vec2(-0.39465, -0.58060), yuv.yz );
    rgb.b = yuv.x + yuv.y * 2.03211;
    return rgb;
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


    // float THRESHOLD = 0.1 + iMouse.x / RENDERSIZE.x;

    float THRESHOLD = amount;
    float time_s = mod( TIME, 32.0 );
    float glitch_threshold = 1.0 - THRESHOLD;
    const float max_ofs_siz = 0.1; //TOOD: input
    const float yuv_threshold = 0.5; //TODO: input, >1.0f == no distort
    const float time_frq = 16.0;
    // vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;

    vec2 uv = texCoord;
    // uv.y = 1.0 -  uv.y;

    const float min_change_frq = 4.0;
    float ct = mytrunc( time_s, min_change_frq );
    float change_rnd = rand( mytrunc(uv.yy,vec2(16)) + 150.0 * ct );
    float tf = time_frq*change_rnd;
    float t = 5.0 * mytrunc( time_s, tf );
    float vt_rnd = 0.5*rand( mytrunc(uv.yy + t, vec2(11)) );
    vt_rnd += 0.5 * rand(mytrunc(uv.yy + t, vec2(7)));
    vt_rnd = vt_rnd*2.0 - 1.0;
    vt_rnd = sign(vt_rnd) * sat( ( abs(vt_rnd) - glitch_threshold) / (1.0-glitch_threshold) );
    vec2 uv_nm = uv;
    uv_nm = sat( uv_nm + vec2(max_ofs_siz*vt_rnd, 0) );
    float rnd = rand( vec2( mytrunc( time_s, 8.0 )) );
    uv_nm.y = (rnd>mix(1.0, 0.975, sat(THRESHOLD))) ? 1.0-uv_nm.y : uv_nm.y;
    vec4 smpl = IMG_NORM_PIXEL(mat_input_image,mod(uv_nm,1.0));
    vec3 smpl_yuv = rgb2yuv( smpl.rgb );
    smpl_yuv.y /= 1.0-3.0*abs(vt_rnd) * sat( yuv_threshold - vt_rnd );
    smpl_yuv.z += 0.125 * vt_rnd * sat( vt_rnd - yuv_threshold );


    return mix(IMG_NORM_PIXEL(mat_input_image, texCoord), vec4( yuv2rgb(smpl_yuv), smpl.a ), filter_mix);





}