/*{
    "CREDIT": "Shadertoy XsVGz3 by candycat\nported by frz",
    "DESCRIPTION": "Density raymarched volume",
    "TAGS": "physical",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Density", "NAME": "mat_density", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ -1.0, 1.0 ] },

        {
            "Label": "Color/Enable Alpha",
            "NAME": "mat_alpha",
            "TYPE": "bool",
            "DEFAULT": 1,
            "FLAGS": "button"
        },
	
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	


#define MIN_HEIGHT 2.0
#define MAX_HEIGHT 4.5
#define WIND vec2(0.2, 0.1)

vec3 sundir = normalize(vec3(1.0,0.75,1.0));

float noisee( in vec3 x )
{
	return billowedNoise(x);
}

float fractal_noise(vec3 p)
{
    float f = 0.0;
    // add animation
	p *= mat_scale;
    p = p + vec3(1.0, 0.1, 0.0) * mat_animation_time * 0.1;
    p = p * 3.0;
    f += 0.50000 * noisee(p); p = 2.0 * p;
	f += 0.25000 * noisee(p); p = 2.0 * p;
	f += 0.12500 * noisee(p); p = 2.0 * p;
	f += 0.06250 * noisee(p); p = 2.0 * p;
    f += 0.03125 * noisee(p);
    
    return f;
}

float density(vec3 pos)
{    
    float den = 3.0 * fractal_noise(pos * 0.3) - 2.0 + (pos.y - MIN_HEIGHT);
    float edge = (0.9+mat_density) - smoothstep(MIN_HEIGHT, MAX_HEIGHT, pos.y);
    edge *= edge;
    den *= edge;
    den = clamp(den, 0.0, 1.0);
    
    return den;
}

vec3 raymarching(vec3 ro, vec3 rd, float t, vec3 backCol)
{   
    vec4 sum = vec4(0.0);
    vec3 pos = ro + rd * t;
    for (int i = 0; i < 40; i++) {
        if (sum.a > 0.99 || 
            pos.y < (MIN_HEIGHT-1.0) || 
            pos.y > (MAX_HEIGHT+1.0)) break;
        
        float den = density(pos);
        
        if (den > 0.01) {
            float dif = clamp((den - density(pos+0.3*sundir))/0.6, 0.0, 1.0);

            vec3 lin = vec3(0.65,0.7,0.75)*1.5 + vec3(1.0, 0.6, 0.3)*dif;        
            vec4 col = vec4( mix( vec3(1.0,0.95,0.8)*1.1, vec3(0.35,0.4,0.45), den), den);
            col.rgb *= lin;

            // front to back blending    
            col.a *= 0.5;
            col.rgb *= col.a;

            sum = sum + col*(1.0 - sum.a); 
        }
        
        t += max(0.05, 0.02 * t);
        pos = ro + rd * t;
    }
    
    sum = clamp(sum, 0.0, 1.0);
    
    float h = rd.y;
    sum.rgb = mix(sum.rgb, backCol, exp(-20.*h*h) );
    
    return mix(backCol, sum.xyz, sum.a);
}

float planeIntersect( vec3 ro, vec3 rd, float plane)
{
    float h = plane - ro.y;
    return h/rd.y;
}

mat3 setCamera(vec3 ro, vec3 ta, float cr)
{
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(sin(cr), cos(cr),0.0);
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	vec2 p = uv*2.-1.;
	p.y *= -1.;
    vec2 mo = mat_offset*2.;
    vec3 ro = vec3(0.0, 0.0, -2.0);
    
    // Rotate the camera
    vec3 target = vec3(ro.x+10., 1.0+mo.y*3.0, ro.z);
    
    vec2 cossin = vec2(cos(mo.x), sin(mo.x));
    mat3 rot = mat3(cossin.x, 0.0, -cossin.y,
                   	0.0, 1.0, 0.0,
                   	cossin.y, 0.0, cossin.x);
    target = rot * (target - ro) + ro;

   // Compute the ray
    vec3 rd = setCamera(ro, target, 0.0) * normalize(vec3(p.xy, 1.5));  
    float dist = planeIntersect(ro, rd, MIN_HEIGHT);
    
	vec3 col = vec3(0.);
    if (dist > 0.0) {
        col = raymarching(ro, rd, dist, col);
    }
  	
	// make a color out of it
	vec3 color = col;
	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,mix(1.,col.r,mat_alpha));	
	return final_color;
}
