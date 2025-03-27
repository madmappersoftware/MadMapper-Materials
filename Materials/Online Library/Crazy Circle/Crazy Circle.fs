/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "DESCRIPTION": "Experiment drawing geometric shapes, merging between rectangles and circles.",
    "TAGS": "geometry,morph,shape",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 0.55 },
        { "LABEL": "BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": true },
        { "LABEL": "Thickness", "NAME": "thickness", "TYPE": "float", "DEFAULT": 0.005, "MIN": 0.005, "MAX": 1 },
        { "LABEL": "Radius", "NAME": "shape_radius", "TYPE": "float", "DEFAULT": 0.25, "MIN": 0.0, "MAX": 0.5 },
        { "LABEL": "Move Ampl", "NAME": "move_size", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "BassFX/Bass FX1", "NAME": "bass_fx1", "TYPE": "bool", "DEFAULT": false, "FLAGS": "generate_as_define,button" },
        { "LABEL": "BassFX/Bass FX2", "NAME": "bass_fx2", "TYPE": "bool", "DEFAULT": true, "FLAGS": "generate_as_define,button" },
        { "LABEL": "BassFX/Bass FX3", "NAME": "bass_fx3", "TYPE": "bool", "DEFAULT": true, "FLAGS": "generate_as_define,button" },
        { "LABEL": "BassFX/Bass Amplify", "NAME": "bass_amlpify", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4.0 },
        { "NAME": "spectrum", "TYPE": "audioFFT", "SIZE": 3 },
    ],
    "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "bpm_sync": "bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true}}
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadSDF.glsl"

#define M_PI 3.141592654

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 center1 = repeat(texCoord,vec2(1));
    vec2 center2 = repeat(texCoord+move_size*move_size*vec2(cos(animation_time),1-sin(animation_time*0.9)),vec2(1));

    float shapeMorph = (1+cos(animation_time*M_PI))/2;

    float dist_circle = max(0,shapeMorph) * circle(center1,shape_radius);
    float dist_rect = max(0,1-shapeMorph) * circle(center2,shape_radius);
    float dist = dist_circle + dist_rect;

    #if defined(bass_fx1_IS_true) || defined(bass_fx2_IS_true) || defined(bass_fx3_IS_true)
        float theta = mod(atan(center1.y,center1.x)/M_PI,2);
        theta = min(theta,2-theta);
    #endif
    #if defined(bass_fx1_IS_true)
        dist += 0.01 * bass_amlpify * bass_amlpify * bass_amlpify * (IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r * cos(12*theta * M_PI) + IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r * cos(40*theta * M_PI));
    #endif
    #if defined(bass_fx2_IS_true)
        float waveformEffect2 = max(0,1+mod(animation_time,4)-4);
        dist += waveformEffect2 * bass_amlpify * bass_amlpify * IMG_NORM_PIXEL(spectrum,vec2(0,0)).r * (1+cos(10*theta * M_PI)/2);
    #endif
    #if defined(bass_fx3_IS_true)
        float waveformEffect3 = 2 * max(0,1+mod(1+animation_time,4)-4);
        dist *= (1 - waveformEffect3 * bass_amlpify * bass_amlpify * IMG_NORM_PIXEL(spectrum,vec2(0,0)).r * (1+cos(10*theta * M_PI)/2));
    #endif

    return vec4(stroke(vec3(0,0,0),vec3(1,1,1),dist,thickness*shape_radius), 1);
}
