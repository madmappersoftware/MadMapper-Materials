/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "AutoMorph/Speed", "NAME": "automorphspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.23 },
        { "LABEL": "AutoMorph/BPM Sync", "NAME": "bpmsync", "TYPE": "bool", "DEFAULT": true }, 
        { "LABEL": "Radius", "NAME": "shape_radius", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 0.5 },
        { "LABEL": "Repeat", "NAME": "shape_repeat", "TYPE": "int", "MIN" : 1.0, "MAX" : 16.0, "DEFAULT": 1.0 },
        { "LABEL": "Dither", "NAME": "mat_dither", "TYPE": "bool", "DEFAULT": true },
    ],
    "GENERATORS": [
        {"NAME": "morph_position", "TYPE": "time_base", "PARAMS": {"speed": "automorphspeed","bpm_sync": "bpmsync", "speed_curve": 3}},
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadSDF.glsl"

#define M_PI 3.141592654

// A single iteration of Bob Jenkins' One-At-A-Time hashing algorithm.
int hash( int x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    x ^= ( x >> 11u );
    x += ( x << 15u );
    return x;
}

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
    vec2 center = texCoord;
    vec2 cellId;
    vec2 rectCenter = repeat(center,vec2(1.0/shape_repeat),cellId);

    float auto_morph_value = (1-mod(morph_position,1));

    #define SHAPE_COUNT 5
    float stepSize = 1.0/SHAPE_COUNT;
    float pos = auto_morph_value*SHAPE_COUNT + (cellId.x + cellId.y * shape_repeat) / (shape_repeat*shape_repeat);
    int stepId = int(pos/stepSize);
    int randInt = hash(stepId+int(cellId.x));
    int randInt2 = hash(stepId+2+int(cellId.y));
    float auto_radius_value = 0;//-(1+cos(randInt))/5;

    rectCenter = rotate(rectCenter,randInt/60.0f);

    float shapes[SHAPE_COUNT];
    shapes[0] = rectangle(rectCenter,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[1] = circle(rectCenter,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[2] = triangle(rectCenter,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[3] = arc(rectCenter,(shape_radius+auto_radius_value)/shape_repeat,randInt%360,0.3/shape_repeat);
    shapes[4] = rectangle(rotate(rectCenter,randInt2)*vec2(0.7-mod(randInt2,0.5),0.9-mod(randInt,0.8)),shape_radius/shape_repeat);

    float dist1 = mix(shapes[(stepId+0)%SHAPE_COUNT],shapes[(stepId+1)%SHAPE_COUNT],pos/stepSize-stepId);

    float dist2 = mix(shapes[(stepId+randInt)%SHAPE_COUNT],shapes[(stepId+randInt2)%SHAPE_COUNT],pos/stepSize-stepId);

    // Pattern 1
    float dist = mix(dist1, dist2, 0.5);
    //float dist = dist1/(1+atan(dist2*20));
    //float dist = mix(dist1+dist2, shapes[4], dist2-dist1);
    //float dist = mix(dist1+dist2, -dist1*dist2, dist2-dist1);

    float luma = -dist * 2 * shape_repeat;

    if (mat_dither)
      luma = mat_rand(texCoord + fract(TIME)) < luma ? 1 : 0;

    // Points
    return vec4(vec3(luma), 1);
}

