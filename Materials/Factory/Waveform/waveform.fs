/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Audio Waveform",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "NAME": "mat_waveform",
            "TYPE": "audio",
        },
        {
            "NAME": "mat_mode",
            "LABEL": "Mode",
            "TYPE": "long",
            "VALUES": ["Lost","Filled"],
            "DEFAULT": "Filled",
            "FLAGS": "generate_as_define"
        },
        {
            "Label": "Thickness",
            "NAME": "mat_waveform_thickness",
            "TYPE": "float",
            "DEFAULT": 0.05,
            "MIN": 0.05,
            "MAX": 0.25 
        },
        {
            "Label": "Smooth",
            "NAME": "mat_waveform_smooth",
            "TYPE": "float",
            "DEFAULT": 0.1,
            "MIN": 0.0001,
            "MAX": 1 
        },
        {
            "Label": "Time Scale",
            "NAME": "mat_time_scale",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "Label": "Levels/Amplitude",
            "NAME": "mat_waveform_amplitude",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 10.0
        },
    ]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    float width = mat_waveform_thickness * mat_waveform_thickness;

    #if defined(mat_mode_IS_Lost)
        float posY = 0.5;
        posY += (IMG_NORM_PIXEL(mat_waveform,vec2(texCoord.x*mat_time_scale,0)).r * mat_waveform_amplitude) / 6;
        posY += (IMG_NORM_PIXEL(mat_waveform,vec2(texCoord.x*mat_time_scale - 1.0/mat_waveform_size.x,0)).r * mat_waveform_amplitude) / 6;
        posY += (IMG_NORM_PIXEL(mat_waveform,vec2(texCoord.x*mat_time_scale + 1.0/mat_waveform_size.x,0)).r * mat_waveform_amplitude) / 6;
        float value = smoothstep(width,0,abs(texCoord.y-posY))*4;
    #else // defined(mat_mode_IS_Filled)
        float posY1 = 0.5 + (IMG_NORM_PIXEL(mat_waveform,vec2(texCoord.x*mat_time_scale,0)).r * mat_waveform_amplitude);
        float posY2 = 0.5 + (IMG_NORM_PIXEL(mat_waveform,vec2((texCoord.x+width)*mat_time_scale,0)).r * mat_waveform_amplitude);

        float minY = min(posY1,posY2)-1./800.;
        float maxY = max(posY1,posY2)+1./800.;
        float centerY = (minY+maxY)/2;

        float value;
        if (texCoord.y>=centerY)
            value = 1-smoothstep(maxY+(0.99999-mat_waveform_smooth)*width,maxY+width,texCoord.y);
        else
            value = 1-smoothstep(minY-(0.99999-mat_waveform_smooth)*width,minY-width,texCoord.y);
    #endif

    return vec4(vec3(value),1);
}
