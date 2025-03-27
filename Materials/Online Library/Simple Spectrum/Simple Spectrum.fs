/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Simple AAudio Spectrum",
    "VSN": "1.0",
    "TAGS": "audio,reactive",
    "INPUTS": [
        {
            "NAME": "spectrum_512_decay",
            "TYPE": "audioFFT",
            "SIZE": 512,
            "ATTACK": 0.0,
            "DECAY": "mat_peaks_decay",
            "RELEASE": 0.0
        },
        {
            "NAME": "spectrum_512",
            "TYPE": "audioFFT",
            "SIZE": 512,
            "ATTACK": 0.0,
            "DECAY": 0.0,
            "RELEASE": "mat_release"
        },
        {
            "LABEL": "Thickness",
            "NAME": "mat_thickness",
            "TYPE": "float",
            "DEFAULT": 0.2,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Amplitude",
            "NAME": "mat_amplitude",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "LABEL": "Release",
            "NAME": "mat_release",
            "TYPE": "float",
            "DEFAULT": 0.2,
            "MIN": 0.0,
            "MAX": 1.0,
            "FLAGS": "generate_as_define,button"
        },
        {
            "LABEL": "Peaks/Show Peaks",
            "NAME": "mat_show_peaks",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define,button"
        },
        {
            "LABEL": "Peaks/Decay",
            "NAME": "mat_peaks_decay",
            "TYPE": "float",
            "DEFAULT": 0.4,
            "MIN": 0.0,
            "MAX": 1.0,
            "FLAGS": "generate_as_define"
        },
        {
            "LABEL": "Peaks/Peaks Color",
            "NAME": "mat_peaks_color",
            "TYPE": "color",
            "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ]
        }
    ]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
	texCoord.y = 1-texCoord.y;
	
    vec4 finalColor = vec4(vec3(0),1);
	float thickness = pow(mat_thickness,3);

    float posY = IMG_NORM_PIXEL(spectrum_512,vec2(texCoord.x,0)).r * mat_amplitude * mat_amplitude;
	float dist = abs(posY-texCoord.y);
	if (dist < thickness) {
		finalColor.rgb = vec3(1-dist/(thickness));
	}

    #if defined(mat_show_peaks_IS_true)
        float posY_decay = IMG_NORM_PIXEL(spectrum_512_decay,vec2(texCoord.x,0)).r * mat_amplitude * mat_amplitude;
		float dist_decay = abs(posY_decay-texCoord.y);
        if (dist_decay<thickness) {
			finalColor += mat_peaks_color * (1-dist_decay/(thickness));
        }
    #endif

    return finalColor;
}
