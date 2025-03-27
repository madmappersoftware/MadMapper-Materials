/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Stephane Cuillerdier\nshadertoy ld2czw\nported by franz",
    "DESCRIPTION": "Polygonal Voronoi SDF",
    "TAGS": "polygon",
    "VSN": "1.0",
    "INPUTS": [ 
	    { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.6 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Segments", "NAME": "uSegments", "TYPE": "int", "MIN": 3, "MAX": 20, "DEFAULT": 20 },
				{
            "NAME": "brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
		{ "LABEL": "Color/Color A", "NAME": "uColorA", "TYPE": "color", "DEFAULT": [ 1.0, 0.8, 0.3, 1.0 ] },
      ],
	"GENERATORS": [
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
float n;

float df(vec2 p, float z)
{
    //https://www.shadertoy.com/view/4lVGWV
    float a = atan(p.x,p.y);
    float b = 6.2831/n;
    return cos(floor(.5+a/b)*b-a)*length(p) * cos(z*1.5);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	
	n = uSegments;
	vec4 f;
    vec2 uv = texCoord*vec2(1024.0,-1024)*uScale;
    vec2 p = uv /= 80., a; f-=f-9.;
       	
    for(int x=-3;x<=3;x++)
    for(int y=-3;y<=3;y++)
        p = vec2(x,y),
        a = sin( 123.4 + 9. * fract(sin((floor(uv)+p+animation_time*0.02)*mat2(2,5,5,2)))),
        p += .5 + .5 * a - fract(uv),
        f = min(f, df(p,a.x));
    	f *= 8.;
		f *= uColorA;
//    f = sqrt(vec4(10,5,2,1)*f);
	
	    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(f.rgb, LumCoeff));
    f.rgb = mix(AvgLumin, f.rgb, contrast);

    // Apply brightness
    f.rgb += brightness;
	
	return vec4(f.rgb,1);
}
