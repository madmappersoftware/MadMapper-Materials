/*
{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Derived from Nasana https://twitter.com/nasana_x on Shadertoy by Tim Brassey",
    "DESCRIPTION": "Crumpled",
    "VSN": "1.0",    
	"CATEGORIES": [],
    "IMPORTED": {},
    "INPUTS": [
		{"LABEL": "Resolution", "NAME": "resolution", "TYPE": "float", "MIN": 1.0, "MAX": 12.0, "DEFAULT": 8.0 },
		{"LABEL": "Detail", "NAME": "detail", "TYPE": "float", "MIN": 0.0, "MAX": 0.2, "DEFAULT": 0.1 },
		{"LABEL": "Red", "NAME": "red", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.1 },
		{"LABEL": "Green", "NAME": "green", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.3 },
		{"LABEL": "Blue", "NAME": "blue", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.95 },
		{"LABEL": "Brightness", "NAME": "brightness", "TYPE": "float", "MIN": 0.2, "MAX": 1.0, "DEFAULT": 1.0 },
		{"LABEL": "Movement", "NAME": "movement", "TYPE": "float", "MIN": 0.1, "MAX": 1.0, "DEFAULT": 0.95 },

    ]
}
*/
vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 uv =  (0.3 * texCoord.yx - 1);
    for(float i = movement * movement; i < resolution; i++){
    uv.y -= i * detail / i * 
    sin(uv.x * i * i + TIME * 0.3) * cos(uv.y * i * i + TIME * 0.3);
}
   vec3 col;
   col.r  = uv.y + red;   col.g = uv.y + green;   col.b = uv.y + blue;
   return vec4(col,brightness);
}