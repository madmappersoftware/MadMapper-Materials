/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus",
    "DESCRIPTION": "Bar Code",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {
          "NAME": "mat_animation_time",
          "TYPE": "time_base",
          "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "link_speed_to_global_bpm":true }
        }
    ]
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = texCoord;
    float r=fract(4.789*sin(mat_size*uv.x + mat_animation_time));

    vec3 color = mat_foregroundColor.rgb * r;

     // Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);

    // Apply brightness
    color += vec3(mat_brightness);

    return vec4( color, 1.0f );
}
