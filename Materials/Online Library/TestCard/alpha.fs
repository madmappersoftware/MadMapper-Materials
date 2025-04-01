/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "github.com/keijiro",
    "DESCRIPTION": "TestCard Material",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Res_X", "NAME": "res_x", "TYPE": "int", "MIN": 16, "MAX": 2048, "DEFAULT": 1024 },
		{ "LABEL": "Res_Y", "NAME": "res_y", "TYPE": "int", "MIN": 16, "MAX": 2048, "DEFAULT": 1024 },
      ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec3 hue2rgb(float h)
{
    h = fract(saturate(h)) * 6 - 2;
    return saturate(vec3(abs(h - 1) - 1, 2 - abs(h), 2 - abs(h - 2)));
}	

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 res = vec2( float(res_x), float(res_y));
	float scale = 27.0 / res.y;                             // grid scale

    vec2 area = vec2(floor(13 / res.y * res.x), 13); // size of inner area
 //   vec2 p0 = gl_FragCoord.xy - res / 2; // position (pixel)
	vec2 p0 = (texCoord.xy-vec2(0.5)) * (res); // position (pixel)
    vec2 p1 = p0 * scale;                // position (grid)

    // gray background with crosshair
    float c1 = 1 - step(2, min(abs(p0.x), abs(p0.y))) * 0.5;

    // grid lines
    vec2 grid = step(scale, abs(0.5 - fract(p1 * 0.5)));
    c1 = saturate(c1 + 2 - grid.x - grid.y);

    // outer area checker
    vec2 checker = step(0.49999, fract(floor(p1 * 0.5 + 0.5) * 0.5));
	
	if (any(greaterThan(abs(p1), area))) c1 = abs(checker.x - checker.y);

    float corner = sqrt(8) - length(abs(p1) - area + 4); // corner circles
    float circle = 12 - length(p1);                      // big center circle
    float mask = saturate(circle / scale);               // center circls mask

    // grayscale bars
    float bar1 = saturate(p1.y < 5 ? floor(p1.x / 4 + 3) / 5 : p1.x / 16 + 0.5);
    c1 = mix(c1, bar1, mask * saturate(ceil(4 - abs(5 - p1.y))));

    // basic color bars
    vec3 bar2 = hue2rgb((p1.y > -5 ? floor(p1.x / 4) / 6 : p1.x / 16) + 0.5);
    vec3 c2 = mix(vec3(c1), bar2, mask * saturate(ceil(4 - abs(-5 - p1.y))));
	
	// big circle line
    c2 = mix(c2, vec3(1), saturate(2 - abs(max(circle, corner)) / scale));
	
	return vec4(c2,1.0);
}
