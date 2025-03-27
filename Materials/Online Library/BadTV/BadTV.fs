/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Jason Beyers, originally by VIDVOX",
    "ISFVSN": "2",
    "DESCRIPTION" : "A Bad TV effect with audio reactivity, which can be applied to any input",
    "CATEGORIES": [
        "Glitch"
    ],
    "VSN": "1.0",
    "INPUTS": [
        {
            "LABEL" : "Input",
            "NAME": "inputImage",
            "TYPE": "image"
        },
        {
            "LABEL": "Noise Type",
            "NAME": "noise_type",
            "TYPE": "long",
            "VALUES": ["Manual", "Audio Only", "Bass Audio Only", "Treble Audio Only", "Audio Multiplied", "Bass Audio Multiplied", "Treble Audio Multiplied"],
            "DEFAULT": "Manual",
            "FLAGS": "generate_as_define"
        },
        {
            "NAME": "noiseLevel",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Distortion1 Type",
            "NAME": "distortion1_type",
            "TYPE": "long",
            "VALUES": ["Manual", "Audio Only", "Bass Audio Only", "Treble Audio Only", "Audio Multiplied", "Bass Audio Multiplied", "Treble Audio Multiplied"],
            "DEFAULT": "Manual",
            "FLAGS": "generate_as_define"
        },
        {
            "LABEL": "Distortion1 Reverse",
            "NAME": "distortion1_reverse",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "NAME": "distortion1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Distortion2 Type",
            "NAME": "distortion2_type",
            "TYPE": "long",
            "VALUES": ["Manual", "Audio Only", "Bass Audio Only", "Treble Audio Only", "Audio Multiplied", "Bass Audio Multiplied", "Treble Audio Multiplied"],
            "DEFAULT": "Manual",
            "FLAGS": "generate_as_define"
        },
        {
            "NAME": "distortion2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 50.0,
            "DEFAULT": 5.0
        },
        {
            "NAME": "speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.3
        },
        {
            "NAME": "scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "scroll_offset",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "scanLineThickness",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 50.0,
            "DEFAULT": 25.0
        },
        {
            "NAME": "scanLineIntensity",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "scanLineOffset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Shape",
            "NAME": "mat_shape",
            "TYPE": "long",
            "VALUES": ["In","Out","Smooth"],
            "DEFAULT": "Smooth"
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

    ],

     "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "animator",
            "PARAMS":
                {
                    "speed": "speed",
                    "speed_curve":2,
                    "strob" : "mat_strob",
                    "bpm_sync": "mat_bpm_sync",
                    "shape": "mat_shape", "link_speed_to_global_bpm":true
                }
        },

        {
            "NAME": "scroll_time",
            "TYPE": "animator",
            "PARAMS":
                {
                    "speed": "scroll_speed",
                    "speed_curve":2,
                    "strob" : "mat_strob",
                    "bpm_sync": "mat_bpm_sync",
                    "shape": "mat_shape", "link_speed_to_global_bpm":true
                }
        },
    ]
}*/

//  Adapted from http://www.airtightinteractive.com/demos/js/badtvshader/js/BadTVShader.js
//  Also uses adopted Ashima WebGl Noise: https://github.com/ashima/webgl-noise

// Start Ashima 2D Simplex Noise

const vec4 C = vec4(0.211324865405187,0.366025403784439,-0.577350269189626,0.024390243902439);

vec3 mod289(vec3 x) {
    return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec2 mod289(vec2 x) {
    return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec3 permute(vec3 x) {
    return mod289(((x*34.0)+1.0)*x);
}

float snoise(vec2 v)    {
    vec2 i  = floor(v + dot(v, C.yy) );
    vec2 x0 = v -   i + dot(i, C.xx);

    vec2 i1;
    i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
    vec4 x12 = x0.xyxy + C.xxzz;
    x12.xy -= i1;

    i = mod289(i); // Avoid truncation effects in permutation
    vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))+ i.x + vec3(0.0, i1.x, 1.0 ));

    vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
    m = m*m ;
    m = m*m ;

    vec3 x = 2.0 * fract(p * C.www) - 1.0;
    vec3 h = abs(x) - 0.5;
    vec3 ox = floor(x + 0.5);
    vec3 a0 = x - ox;

    m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );

    vec3 g;
    g.x  = a0.x  * x0.x  + h.x  * x0.y;
    g.yz = a0.yz * x12.xz + h.yz * x12.yw;
    return 130.0 * dot(m, g);
}

// End Ashima 2D Simplex Noise

const float tau = 6.28318530718;

//  use this pattern for scan lines

vec2 pattern(vec2 pt) {
    float s = 0.0;
    float c = 1.0;
    vec2 tex = pt * RENDERSIZE;
    vec2 point = vec2( c * tex.x - s * tex.y, s * tex.x + c * tex.y ) * (1.0/scanLineThickness);
    float d = point.y;

    return vec2(sin(d + scanLineOffset * tau + cos(pt.x * tau)), cos(d + scanLineOffset * tau + sin(pt.y * tau)));
}

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

vec4 materialColorForPixel( vec2 texCoord ) {

    vec2 texcoord = texCoord;

    float amount;

    vec2 p = texCoord;

    //amount = manual_amount;

    float audio_value = 0.0;

    float bass_value = 0.3 * master_scale * bass_level * IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r;

    float medium_value = 0.3 * master_scale * medium_level * IMG_NORM_PIXEL(spectrum,vec2(0.5,0)).r;

    float treble_value = 0.3 * master_scale * treble_level * IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r;

    audio_value += bass_value;
    audio_value += medium_value;
    audio_value += treble_value;

    float adj_distortion1;
    float adj_distortion2;
    float adj_noiseLevel;

    #if defined(noise_type_IS_Audio_Only)
        adj_noiseLevel = audio_value / 10;

    #elif defined(noise_type_IS_Bass_Audio_Only)
        adj_noiseLevel = (bass_value * 3) / 10;

    #elif defined(noise_type_IS_Treble_Audio_Only)
        adj_noiseLevel = (treble_value * 3) / 10;

    #elif defined(noise_type_IS_Audio_Multiplied)
        adj_noiseLevel = noiseLevel * audio_value;
    #elif defined(noise_type_IS_Bass_Audio_Multiplied)
        adj_noiseLevel = noiseLevel * bass_value * 3;

    #elif defined(noise_type_IS_Treble_Audio_Multiplied)
        adj_noiseLevel = noiseLevel * treble_value * 3;
    #else // Manual
        adj_noiseLevel = noiseLevel;
    #endif


    #if defined(distortion1_type_IS_Audio_Only)
        adj_distortion1 = audio_value * 5;

    #elif defined(distortion1_type_IS_Bass_Audio_Only)
        adj_distortion1 = bass_value * 3 * 5;

    #elif defined(distortion1_type_IS_Treble_Audio_Only)
        adj_distortion1 = treble_value * 3 * 5;

    #elif defined(distortion1_type_IS_Audio_Multiplied)
        adj_distortion1 = distortion1 * audio_value;

    #elif defined(distortion1_type_IS_Bass_Audio_Multiplied)
        adj_distortion1 = distortion1 * bass_value * 3;

    #elif defined(distortion1_type_IS_Treble_Audio_Multiplied)
        adj_distortion1 = distortion1 * treble_value * 3;

    #else // Manual
        adj_distortion1 = distortion1;
    #endif


    #if defined(distortion2_type_IS_Audio_Only)
        adj_distortion2 = audio_value * 50;

    #elif defined(distortion2_type_IS_Bass_Audio_Only)
        adj_distortion2 = bass_value * 3 * 50;

    #elif defined(distortion2_type_IS_Treble_Audio_Only)
        adj_distortion2 = treble_value * 3 * 50;

    #elif defined(distortion2_type_IS_Audio_Multiplied)
        adj_distortion2 = distortion2 * audio_value;

    #elif defined(distortion2_type_IS_Bass_Audio_Multiplied)
        adj_distortion2 = distortion2 * bass_value * 3;

    #elif defined(distortion2_type_IS_Treble_Multiplied)
        adj_distortion2 = distortion2 * treble_value * 3;

    #else // Manual
        adj_distortion2 = distortion2;
    #endif


    // if (distortion1_audio_react) {
    //     adj_distortion1 = audio_value * 5;
    //     // adj_distortion1 = audio_value;
    // }

    // if (distortion2_audio_react) {
    //     adj_distortion2 = 0.3 * master_scale * bass_level * IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r * 50; //50
    //     // adj_distortion2 = audio_value; //50
    // }

    // if (noise_audio_react) {
    //     adj_noiseLevel = audio_value / 10; // /10;
    //     // adj_noiseLevel = audio_value; // /10;
    // }




    float ty = mat_time;
    float yt = p.y - ty;

    //smooth distortion
    float offset = snoise(vec2(yt*3.0,0.0))*0.2;
    // boost distortion
    offset = pow( offset*adj_distortion1,3.0)/max(adj_distortion1,0.001);
    //add fine grain distortion
    offset += snoise(vec2(yt*50.0,0.0))*adj_distortion2*0.001;
    //combine distortion on X with roll on Y
    vec2 adjusted;

    // float scroll_pos = fract(scroll_time - scroll_offset);

    float scroll_pos = fract(scroll_time) - scroll_offset;

    if (distortion1_reverse) {
        adjusted = vec2(fract(p.x - offset),fract(p.y+scroll_pos) );

    } else {
        adjusted = vec2(fract(p.x + offset),fract(p.y-scroll_pos) );
    }


    vec4 result = IMG_NORM_PIXEL(inputImage, adjusted);
    vec2 pat = pattern(adjusted);
    vec3 shift = scanLineIntensity * vec3(0.3 * pat.x, 0.59 * pat.y, 0.11) / 2.0;
    result.rgb = (1.0 + scanLineIntensity / 2.0) * result.rgb + shift + (rand(adjusted * TIME) - 0.5) * adj_noiseLevel;
    return result;
}