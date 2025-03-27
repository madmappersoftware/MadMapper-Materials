/*{
    "CREDIT": "WillKirkbyl",
    "DESCRIPTION": "Welcome to the Matrix",
    "TAGS": "eyecandy",
    "VSN": "1.0",
    "INPUTS": [ 
	{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
	{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
    {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
    "IMPORTED": [
    {"NAME": "texture_letters", "PATH": "letters.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/
// https://www.shadertoy.com/view/ldccW4

float res = 1024.;
float text(vec2 fragCoord)
{
    vec2 uv = mod(fragCoord.xy, 16.)*.0625;
    vec2 block = fragCoord*.0625 - uv;
    uv = uv*.8+.1; // scale the letters up a bit
    uv += floor(texture(texture_letters, block/vec2(res) + mat_time*.002).xy * 16.); // randomize letters
    uv *= .0625; // bring back into 0-1 range
    uv.x = -uv.x; // flip letters horizontally
    return texture(texture_letters, uv).r;
}

vec3 rain(vec2 fragCoord)
{
	fragCoord.x -= mod(fragCoord.x, 16.);
    //fragCoord.y -= mod(fragCoord.y, 16.);
    
    float offset=sin(fragCoord.x*15.);
    float speed=cos(fragCoord.x*3.)*.3+.7;
   
    float y = fract(fragCoord.y/res + mat_time*speed + offset);
    return vec3(.1,1,.35) / (y*20.);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	res = 1024.*mat_scale;
	vec2 uv = texCoord.xy *res;
	uv.y = res - uv.y;
	return vec4(text(uv)*rain(uv),1.0);
}