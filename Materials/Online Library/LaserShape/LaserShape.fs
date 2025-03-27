/*{
    "CREDIT": "Tristan Le Moigne	",
    "DESCRIPTION": "Test Laser Material",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{"LABEL": "Stroke", "NAME": "mat_stroke", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": .1 },
		{"LABEL": "Stroke Inner", "NAME": "mat_stroke_inner", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": .1 },  
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#define PI 3.141592
#define TAU 6.283185

float fillSpiritDeck(float x, float size)
{
    return 1.-step(size, x);
}

float polySDFSpiritDeck(vec2 st, int V)
{
    //st = st * 2. - 1.;
    float a = atan(st.x, st.y) + PI;
    float r = length(st);
    float v = TAU/float(V);
    return cos(floor(.5+a/v)*v-a)*r;
}

vec4 materialColorForPixel( vec2 texCoord )
{

  	vec2 uv = texCoord - .5;
    uv /= mat_scale;
    
    float t = mat_time *.1;
    vec2 rng = vec2(3., 10.);
    
    int poly = int((sin(t *TAU ) * .5 + .5) * (rng.y-rng.x) + rng.x);

    float d1 = polySDFSpiritDeck(uv, poly);
    vec2 domain = vec2(uv.x, -uv.y);
    float d2 = polySDFSpiritDeck(domain, poly);

    vec3 col = vec3(0.);
    col += fillSpiritDeck(d1, .7) * fillSpiritDeck(fract(d1 * cos(t*PI * 2.) * 8.), mat_stroke);
    col -= fillSpiritDeck(d2, .6) * fillSpiritDeck(fract(d2 * sin(t*PI * 2.) * 8.), mat_stroke_inner);
    
    return vec4(col,1.0);
	//return vec4(0.,texCoord,1.);
}