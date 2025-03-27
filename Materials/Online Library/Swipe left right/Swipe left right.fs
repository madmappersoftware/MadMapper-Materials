/*{
    "CREDIT": "Tim Brassey edit from Fill by Mad Team",
    "CATEGORIES": [ "Graphic, Tool" ],
    "DESCRIPTION": "Swipes a colour left to right, for use with an LED string",
    "TAGS": "grid,geometry",
    "VSN": "1.0",
    "INPUTS": [
        {
            "NAME": "swipe",
            "Label": "Swipe",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
        },
     
        { "LABEL": "Color/Foreground", "NAME": "foreground_color", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Background", "NAME": "background_color", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
    ],

}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    float x = texCoord.x;
    
    x= fract(x);
    float value;
    if (x<swipe) {
        value = 1;
        float normalizedPosInActivePart = x/swipe;
       if (normalizedPosInActivePart < 0) {
           value *= normalizedPosInActivePart/0;
        }
        if (1-normalizedPosInActivePart < 0) {
            value *= (1-normalizedPosInActivePart)/0;
        }
    } else {
        value = 0;
    }
    return mix(background_color,foreground_color,value);
}