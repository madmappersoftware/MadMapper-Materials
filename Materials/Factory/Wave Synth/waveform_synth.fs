/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Audio Waveform - not the real one, recomposed with 3 sinusoids from spectrum",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "NAME": "mode",
            "LABEL": "Mode",
            "TYPE": "long",
            "VALUES": ["Lost","Filled"],
            "DEFAULT": "Filled",
            "FLAGS": "generate_as_define"
        },
        {
            "NAME": "spectrum",
            "TYPE": "audioFFT",
            "SIZE": 3,
            "ATTACK": 0.0,
            "DECAY": 0.0,
            "RELEASE": 0.2
        },
        {
            "Label": "Thickness",
            "NAME": "waveform_thickness",
            "TYPE": "float",
            "DEFAULT": 0.05,
            "MIN": 0.05,
            "MAX": 0.25 
        },
        {
            "Label": "Smooth",
            "NAME": "waveform_smooth",
            "TYPE": "float",
            "DEFAULT": 0.1,
            "MIN": 0.0001,
            "MAX": 1 
        },
        {
            "Label": "Time Scale",
            "NAME": "time_scale",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Levels/Audio React.",
            "NAME": "audio_reactive",
            "TYPE": "bool",
            "DEFAULT": 1,
            "FLAGS": "generate_as_define,button"
        },
        {
            "Label": "Levels/Bass Scale",
            "NAME": "bass_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Levels/Medium Scale",
            "NAME": "medium_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Levels/Treble Scale",
            "NAME": "treble_level",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 3.0
        },
        {
            "Label": "Levels/Master Scale",
            "NAME": "master_scale",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 10.0
        },
        {
          "NAME": "autoscrollactive",
          "LABEL": "Scroll/Auto Scroll",
          "TYPE": "bool",
          "DEFAULT": true,
          "FLAGS": "button"
        },
        {
          "NAME": "autoscrollspeed",
          "LABEL": "Scroll/Speed",
          "TYPE": "float",
          "DEFAULT": 1,
          "MIN": 0.0,
          "MAX": 4.0
        }
    ],
    "GENERATORS": [
        {
            "NAME": "scroll_position",
            "TYPE": "time_base",
            "PARAMS": {"speed": "autoscrollspeed", "speed_curve":2, "link_speed_to_global_bpm":true }
        }
    ]
}*/

#include "MadCommon.glsl"

float valueForWavTime(float wavTime)
{
    float value = 0.5;
    #ifdef audio_reactive_IS_true
        value += 0.3 * master_scale * bass_level * IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r * cos(wavTime*5);
        value += 0.3 * master_scale * medium_level * IMG_NORM_PIXEL(spectrum,vec2(0.5,0)).r * cos(wavTime*19);
        value += 0.3 * master_scale * treble_level * IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r * cos(wavTime*149);
    #else
        value += 0.2 * master_scale * bass_level * cos(wavTime*5);
        value += 0.2 * master_scale * medium_level * cos(wavTime*19);
        value += 0.2 * master_scale * treble_level * cos(wavTime*149);
    #endif
    return value;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    float width=waveform_thickness*waveform_thickness;
    float scrollValue = autoscrollactive?fract(scroll_position/10)*2*PI:0;
    #if defined(mode_IS_Lost)
        float posY = valueForWavTime(scrollValue+texCoord.x*time_scale);
        float value = smoothstep(width,0,abs(texCoord.y-posY))*4;
    #else // defined(mode_IS_Filled)
        float posY1 = valueForWavTime(scrollValue+(texCoord.x)*time_scale);
        float posY2 = valueForWavTime(scrollValue+(texCoord.x+width)*time_scale);

        float minY = min(posY1,posY2)-1./800.;
        float maxY = max(posY1,posY2)+1./800.;
        float centerY = (minY+maxY)/2;

        float value;
        if (texCoord.y>=centerY)
            value = 1-smoothstep(maxY+(0.99999-waveform_smooth)*width,maxY+width,texCoord.y);
        else
            value = 1-smoothstep(minY-(0.99999-waveform_smooth)*width,minY-width,texCoord.y);
    #endif

    return vec4(vec3(value),1);
}
