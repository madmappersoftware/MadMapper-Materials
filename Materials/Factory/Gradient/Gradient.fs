/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Gradient noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN" : 0.0, "MAX" : 10.0, "DEFAULT": 1.0 },
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {
            "NAME": "animation_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed", "reverse": "reverse", "link_speed_to_global_bpm":true}
        }
    ]
}*/

float range(float val, float mi, float ma) {
    return val * (ma - mi) + mi;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 p = vec2(0.5,0.5) + (texCoord-vec2(0.5,0.5)) * scale;
    float t = animation_time;
    
    // main code, *original shader by: 'Plasma' by Viktor Korsun (2011)
    float x = p.x;
    float y = p.y;
    
    float mov0 = x+y+cos(sin(t)*2.0)*100.+sin(x/100.)*1000.;
    float mov1 = -y / 0.5 +  t;
    float mov2 = -x / 0.82;
    
    float c1 = abs(sin(mov1+t)/2.+mov2/2.-mov1-mov2+t);
    float c2 = abs(sin(c1+sin(mov0/1000.+t)+sin(y/40.+t)+sin((x+y)/100.)*3.));
    float c3 = abs(sin(c2+cos(mov1+mov2+c2)+cos(mov2)+sin(x/1000.)));
    
    float value = -1 + 2 * (range(c2, 0.55, 0.65) + range(c3, 0.5, 0.55) + range(c3, 1., 0.75)) / 3;
    vec3 color = foregroundColor.rgb * value;

     // Apply contrast
    color = mix(vec3(0.5), color, contrast);

    // Apply brightness
    color += vec3(brightness);

    return vec4(color,1.0);
}
