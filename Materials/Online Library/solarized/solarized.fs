/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Ethan Schoonover, adapted by Christian Prior-Mamulyan",
    "DESCRIPTION": "Input flags returning the 16 solarized colors",
    "TAGS": "color",
    "VSN": "1.0",
    "INPUTS": [
        {
            "NAME": "solarized",
            "LABEL": "Solarized",
            "TYPE": "long",
            "VALUES": ["base03","base02","base01","base00","base0","base1","base2","base3","yellow","orange","red","magenta","violet","blue","cyan","green"],
            "DEFAULT": "base03",
            "FLAGS": "generate_as_define"
        }
    ],
}*/

vec4 materialColorForPixel( vec2 texCoord )
{
vec4 color;
    #if defined(solarized_IS_base03)
        color = vec4 (  0./255,  43./255.,  54./255., 1);
    #elif defined(solarized_IS_base02)
        color = vec4 (  7./255,  54./255.,  66./255., 1);
    #elif defined(solarized_IS_base01)
        color = vec4 ( 88./255, 110./255., 117./255., 1);
    #elif defined(solarized_IS_base00)
        color = vec4 (101./255, 123./255., 131./255., 1);
    #elif defined(solarized_IS_base0)
        color = vec4 (131./255, 148./255., 150./255., 1);
    #elif defined(solarized_IS_base1)
        color = vec4 (147./255, 161./255., 161./255., 1);
    #elif defined(solarized_IS_base2)
        color = vec4 (238./255, 232./255., 213./255., 1);
    #elif defined(solarized_IS_base3)
        color = vec4 (253./255, 246./255., 227./255., 1);

    #elif defined(solarized_IS_yellow)
        color = vec4 (181./255, 137./255.,   0./255., 1);
    #elif defined(solarized_IS_orange)
        color = vec4 (203./255,  75./255.,  22./255., 1);
    #elif defined(solarized_IS_red)
        color = vec4 (220./255,  50./255.,  47./255., 1);
    #elif defined(solarized_IS_magenta)
        color = vec4 (211./255,  54./255., 130./255., 1);
    #elif defined(solarized_IS_violet)
        color = vec4 (108./255, 113./255., 196./255., 1);
    #elif defined(solarized_IS_blue)
        color = vec4 ( 38./255, 139./255., 210./255., 1);
    #elif defined(solarized_IS_cyan)
        color = vec4 ( 42./255, 161./255., 152./255., 1);
    #elif defined(solarized_IS_green)
        color = vec4 (133./255, 153./255.,   0./255., 1);
    #else
        color = vec4 (  0./255, 43./255., 54./255., 1);
    #endif
	return color;
}