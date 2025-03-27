/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "DESCRIPTION": "Experiment drawing geometric shapes, merging between rectangles and circles.",
    "TAGS": "geometry,morph,shape",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.55 },
        { "LABEL": "Radius", "NAME": "shape_radius", "TYPE": "float", "DEFAULT": 0.35, "MIN": 0.0, "MAX": 0.5 },
        { "LABEL": "Dither", "NAME": "mat_dither", "TYPE": "bool", "DEFAULT": true },
        { "LABEL": "Fixed Circle", "NAME": "mat_anim1", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Distorted Line", "NAME": "mat_anim2", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Moving Circle", "NAME": "mat_anim3", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Weird Mix", "NAME": "mat_weird_mix", "TYPE": "bool", "DEFAULT": true },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}}
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadSDF.glsl"
#include "MadNoise.glsl"

#define M_PI 3.141592654

highp float mat_rand(vec2 co)
{
    highp float a = 12.9898;
    highp float b = 78.233;
    highp float c = 43758.5453;
    highp float dt= dot(co.xy ,vec2(a,b));
    highp float sn= mod(dt,3.14);
    return fract(sin(sn) * c);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec3 noise3D = dvnoise(vec2(animation_time,animation_time*1.1));

    vec2 center1 = texCoord-vec2(0.5);
    vec2 center2 = center1 + 0.3 * vec2(cos(noise3D.x*3),sin(noise3D.z*0.9));

    vec2 scaleTranslatedRect = vec2(1.5+0.5*cos(texCoord.y*3+animation_time),1);
    float dist_circle = circle(center1,shape_radius-0.3+0.5*noise3D.x);
    float dist_translated_rect = circle(center2*scaleTranslatedRect,shape_radius);
    vec2 lineSize = vec2(0.8,0.1) + 0.5 * abs(noise3D.xy);//vec2(1.5+cos(animation_time*1.33),0.3*(1+0.7*cos(animation_time*1.1)));
    float lineYDistort = 2 * (-0.5 + vnoise(vec2(center1.x*10*cos(animation_time),0))); // 0.2*(1.5+cos(5*noise3D.z * center1.x*9+animation_time));
    float rotation = noise3D.z * 2;
    float dist_line = rectangle(rotate(center1,rotation)/lineSize + vec2(0,lineYDistort),shape_radius);
    
    float dist = mat_anim1 * dist_circle + mat_anim2 * dist_line + mat_anim3 * dist_translated_rect;

    if (mat_weird_mix) {
        dist = min(mix(dist,dist*dist_circle,mat_anim1),mat_anim1 * dist_circle + mat_anim2 *dist_line);
    }

    float luma = max(-dist * 0.2, 0) * 30;
    if (mat_dither)
        luma = mat_rand(texCoord + fract(TIME)) < luma ? 1 : 0;

    return vec4(vec3(luma), 1);
}
