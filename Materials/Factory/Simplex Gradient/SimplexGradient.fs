/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Simplex Gradient.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [ 
        { "NAME": "GRADIENT", "Label": "Gradient", "TYPE": "long", "DEFAULT": "Gray Pattern", "FLAGS": "generate_as_define", "VALUES": [ "Linear", "BW_Pattern", "BW", "Gray Pattern", "Gray Noise", "Color_Cut" ] },
        { "Label": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.00001, "MAX": 35.0, "DEFAULT": 2.0 },
        { "Label": "Speed X", "NAME": "x_speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
        { "Label": "Speed Y", "NAME": "y_speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
        { "Label": "Noise Speed", "NAME": "noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1 }, 
        { "Label": "Noise Strop", "NAME": "noise_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 }, 
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {"NAME": "x_time", "TYPE": "time_base", "PARAMS": {"speed": "x_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "y_time", "TYPE": "time_base", "PARAMS": {"speed": "y_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "noise_time", "TYPE": "time_base", "PARAMS": {"speed": "noise_speed", "reverse": "reverse", "speed_curve": 2, "strob": "noise_strob", "link_speed_to_global_bpm":true}}
    ],
    "IMPORTED": [
        {"NAME": "gradient0", "PATH": "linearGradient.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "texture_gray_pattern", "PATH": "gradientGrayPattern.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "texture_gray_noise", "PATH": "gradientGrayNoise.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "texture_color_cut", "PATH": "gradientColorCut.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "texture_bw", "PATH": "gradientBWBars.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "texture_bw_pattern", "PATH": "gradientBWPattern.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
    // Calculate UVs
    vec3 uv     = vec3( vec2(0.5) + (texCoord-vec2(0.5)) * scale + vec2( x_time, y_time ), noise_time );

    // Simplex Noise
    float n     = noise( uv ) * 0.5f + 0.5f;
 
    // Apply contrast, saturation and brightness
    vec3 color;

    // Interpolate Color
#if defined( GRADIENT_IS_Linear )
     color  = texture( gradient0, vec2( n, 0.5f ) ).rgb;
#elif defined( GRADIENT_IS_Gray_Pattern )
     color  = texture( texture_gray_pattern, vec2( n, 0.5f ) ).rgb;
#elif defined( GRADIENT_IS_Gray_Noise )
     color  = texture( texture_gray_noise, vec2( n, 0.5f ) ).rgb;
#elif defined( GRADIENT_IS_Color_Cut )
     color  = texture( texture_color_cut, vec2( n, 0.5f ) ).rgb;
#elif defined( GRADIENT_IS_BW )
     color  = texture( texture_bw, vec2( n, 0.5f ) ).rgb;
#elif defined( GRADIENT_IS_BW_Pattern )
     color  = texture( texture_bw_pattern, vec2( n, 0.5f ) ).rgb;
#endif

     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4( color, 1.0f );
}
