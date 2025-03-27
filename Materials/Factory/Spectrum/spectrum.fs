/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Audio Spectrum - vertical & with a few presets.",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "NAME": "spectrum_16_decay",
            "TYPE": "audioFFT",
            "SIZE": 16,
            "ATTACK": 0.0,
            "DECAY": 0.4,
            "RELEASE": 0.0
        },
        {
            "NAME": "spectrum_16",
            "TYPE": "audioFFT",
            "SIZE": 16,
            "ATTACK": 0.0,
            "DECAY": 0.0,
            "RELEASE": 0.2
        },
        {
            "LABEL": "Amplitude",
            "NAME": "amplitude",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 4.0
        },
        {
            "Label": "Divs/Div X",
            "NAME": "div_x",
            "TYPE": "int",
            "DEFAULT": 12,
            "MIN": 2,
            "MAX": 16
        },
        {
            "Label": "Divs/Div Y",
            "NAME": "div_y",
            "TYPE": "int",
            "DEFAULT": 12,
            "MIN": 2,
            "MAX": 16
        },
        {
            "LABEL": "Divs/Dist",
            "NAME": "sep_width",
            "TYPE": "float",
            "DEFAULT": 0.05,
            "MIN": 0.0,
            "MAX": 0.5
        },
        {
            "LABEL": "Color/Color Mode",
            "NAME": "color_mode",
            "TYPE": "long",
            "VALUES": ["Gray","Mad EQ"],
            "DEFAULT": "Gray",
            "FLAGS": "generate_as_define"
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "hue",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Color/Hue Offset",
            "NAME": "hue_offset",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Peaks/Show Peaks",
            "NAME": "show_peaks",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define,button"
        },
        {
            "LABEL": "Peaks/Peaks Size",
            "NAME": "peaks_size",
            "TYPE": "float",
            "DEFAULT": 0.2,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Peaks/Peaks Color",
            "NAME": "peaks_color",
            "TYPE": "color",
            "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ]
        }
    ],
    "IMPORTED": [
        {"NAME": "hue_texture", "PATH": "hue_texture.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
        {"NAME": "white_texture", "PATH": "white_texture.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    texCoord.y = 1-texCoord.y;

    if (fract(sep_width/2+texCoord.x*div_x)<sep_width) return vec4(0,0,0,1);
    if (fract(sep_width/2+texCoord.y*div_y)<sep_width) return vec4(0,0,0,1);

    float quantizedX = int(texCoord.x*div_x)/float(div_x);
    float quantizedY = int(texCoord.y*div_y)/float(div_y);

    float index = (int(quantizedX*IMG_SIZE(spectrum_16).x)) / IMG_SIZE(spectrum_16).x;
    float posY = IMG_NORM_PIXEL(spectrum_16,vec2(index,0)).r * amplitude * amplitude - 0.1;

    float value = posY-quantizedY;
    value = step(0,value);

    vec4 finalColor;

    #if defined(color_mode_IS_Mad_EQ)
        finalColor = vec4(IMG_NORM_PIXEL(hue_texture,vec2(0.5/div_x + hue + hue_offset*quantizedX, 1-(1.0/div_y)-quantizedY*div_y/(div_y+1.0))).rgb*step(0.01,value), 1);
    #else // defined (color_mode_IS_Gray)
        finalColor = vec4(IMG_NORM_PIXEL(white_texture,vec2(0.5/div_x + hue + hue_offset*quantizedX, 1-(1.0/div_y)-quantizedY*div_y/(div_y+1.0))).rgb*step(0.01,value), 1);
    #endif

    #if defined(show_peaks_IS_true)
        float posY_decay = IMG_NORM_PIXEL(spectrum_16_decay,vec2(index,0)).r * amplitude * amplitude;
        float value_decay = posY_decay-quantizedY;
        if (value_decay>0.0 && value_decay<1.0/div_y && fract(texCoord.y*div_y)<peaks_size) {
            finalColor = peaks_color;
        }
    #endif

    return finalColor;
}
