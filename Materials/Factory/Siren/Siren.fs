/*{
    "CREDIT": "Mad Team",
    "DESCRIPTION": "Siren - rotating shape.",
    "VSN": "1.0",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Shape", "NAME": "shape", "TYPE": "long", "VALUES": ["Sin", "Out", "In", "Cut"], "DEFAULT": "Sin", "FLAGS": "generate_as_define" },
        { "LABEL": "Size", "NAME": "size", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Repeat", "NAME": "repeat", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 1 },
        { "LABEL": "Center X", "NAME": "posx", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Center Y", "NAME": "posy", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 1.0, "MAX" : 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {
            "NAME": "animation_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed", "bpm_sync": "bpmsync", "speed_curve": 2, "reverse": "reverse", "link_speed_to_global_bpm":true}
        }
    ]
}*/


#ifndef M_PI
  #define M_PI 3.1415926535897932384626433832795
#endif

float computeLuma(vec2 uv)
{
    //angle of the line
    float theta0 = animation_time * repeat;

    //compute gradient based on angle difference to theta0
    float value;
    #if defined(shape_IS_Out)
        // OUT
        value = (1/size) * mod(atan(uv.y,uv.x)*repeat/M_PI+theta0,2) / 2;
    #elif defined(shape_IS_In)
        // IN
        value = (1/size) * (1 - mod(atan(uv.y,uv.x)*repeat/M_PI+theta0,2) / 2);
    #elif defined(shape_IS_Cut)
        // CUT
        value = smoothstep(0.99,1,1+sin(atan(uv.y,uv.x)*repeat+theta0*M_PI)+(1-2*size*(1/0.99)));
    #else
        // SIN
        value = (1/size) * (1+sin(atan(uv.y,uv.x)*repeat+theta0*M_PI))/2;
    #endif
    return 1 - value;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    if (size==0) return vec4(0,0,0,1);

    vec4 color = mix(backgroundColor, foregroundColor, clamp(computeLuma(texCoord-vec2(posx,posy)),0,1));

    // Apply brightness
    color.rgb += vec3(brightness);

    // Apply contrast
    color.rgb = mix(vec3(0.5), color.rgb, contrast);

    return color;
}

