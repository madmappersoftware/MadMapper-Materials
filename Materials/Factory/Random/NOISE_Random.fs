/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Random - white noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [ 
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ]
}*/

highp float rand(vec2 co)
{
    highp float a = 12.9898;
    highp float b = 78.233;
    highp float c = 43758.5453;
    highp float dt= dot(co.xy ,vec2(a,b));
    highp float sn= mod(dt,3.14);
    return fract(sin(sn) * c);
}

vec4 materialColorForPixel(vec2 texCoord)
{
	float noise_value = rand(texCoord.xy*TIME);

     // Apply contrast
    noise_value = mix(0.5, noise_value, contrast);

    // Apply brightness
    noise_value += brightness;

    return vec4( vec3(noise_value),1.0);
}
