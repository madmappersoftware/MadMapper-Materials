/*{
    "CREDIT": "1024 architecture\noriginal by mu6k\nShadertoy 4ttGWM",
    "DESCRIPTION": "Dancing fire of hell\n original code by\nCaliCoastReplay and 301 + mu6k",
    "TAGS": "texture",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Speed", "NAME": "uSpeed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Speed X", "NAME": "uSpeedx", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 0.1, "MAX": 7.0, "DEFAULT": 2 },
      ],
	 "GENERATORS": [
         {"NAME": "animation_timex", "TYPE": "time_base", "PARAMS": {"speed": "uSpeedx", "speed_curve":2, "link_speed_to_global_bpm":true}},
		 {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "uSpeed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

float rand(vec2 n) {
    return fract(sin(cos(dot(n, vec2(12.9898,12.1414)))) * 83758.5453);
}

float knoise(vec2 n) {
    const vec2 d = vec2(0.0, 1.0);
    vec2 b = floor(n), f = smoothstep(vec2(0.0), vec2(1.0), fract(n));
    return mix(mix(rand(b), rand(b + d.yx), f.x), mix(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
}

float fbm(vec2 n) {
    float total = 0.0, amplitude = 1.0;
    for (int i = 0; i <5; i++) {
        total += knoise(n) * amplitude;
        n += n*1.7;
        amplitude *= 0.47;
    }
    return total;
}	
		
vec4 materialColorForPixel( vec2 texCoord )
{
	
	const vec3 c1 = vec3(0.5, 0.0, 0.1);
    const vec3 c2 = vec3(0.9, 0.1, 0.0);
    const vec3 c3 = vec3(0.2, 0.1, 0.7);
    const vec3 c4 = vec3(1.0, 0.9, 0.1);
    const vec3 c5 = vec3(0.1);
    const vec3 c6 = vec3(0.9);

    vec2 speed = vec2(0.1, 0.9);
    float shift = 1.327+sin(animation_time*2.0)/2.4;
    float alpha = 1.0;
    
	float dist = 3.5-sin(animation_time*0.4)/1.89;
    
    vec2 uv = vec2(texCoord.x,1.0 - texCoord.y) *0.7;
    vec2 p = vec2(texCoord.x,1.0 - texCoord.y) * dist  *0.7;
    p += sin(p.yx*4.0+vec2(.2,-.3)*animation_time)*0.04;
    p -= sin(p.yx*8.0+vec2(.6,+.1)*animation_time)*0.01;
	
	p.x -= animation_timex;
    float q = fbm(p - animation_time * 0.3+1.0*sin(animation_time+0.5)/2.0);
    float qb = fbm(p - animation_time * 0.4+0.1*cos(animation_time)/2.0);
    float q2 = fbm(p - animation_time * 0.44 - 5.0*cos(animation_time)/2.0) - 6.0;
    float q3 = fbm(p - animation_time * 0.9 - 10.0*cos(animation_time)/15.0)-4.0;
    float q4 = fbm(p - animation_time * 1.4 - 20.0*sin(animation_time)/14.0)+2.0;
    q = (q + qb - .4 * q2 -2.0*q3  + .6*q4)/3.8;
    vec2 r = vec2(fbm(p + q /2.0 + animation_time * speed.x - p.x - p.y), fbm(p + q - animation_time * speed.y));
    vec3 c = mix(c1, c2, fbm(p + r)) + mix(c3, c4, r.x) - mix(c5, c6, r.y);
    vec3 color = vec3(1.0/(pow(c+1.61,vec3(4.0))) * cos(shift * uv.y));
    
    color=vec3(1.0,.2,.05)/(pow((r.y+r.y)* max(.0,p.y)+0.1, 4.0));;
    color = color/(1.0+max(vec3(0),color));
	
	color = pow(saturate(color + brightness),vec3(contrast));
	
	return vec4(color,01.0);
}
