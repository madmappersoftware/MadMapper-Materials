/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "VSN": "1.0",
    "INPUTS": [
        {
          "LABEL": "Repeat",
          "NAME": "shape_repeat",
          "TYPE": "int",
          "MIN" : 1.0,
          "MAX" : 16.0,
          "DEFAULT": 3.0
        },
        {
          "LABEL": "Thickness",
          "NAME": "thickness",
          "TYPE": "float",
          "DEFAULT": 0.02,
          "MIN": 0.001,
          "MAX": 0.5
        },
        {
          "LABEL": "Radius",
          "NAME": "shape_radius",
          "TYPE": "float",
          "DEFAULT": 0.25,
          "MIN": 0.0,
          "MAX": 0.5
        },
        {
          "LABEL": "AutoMorph/Speed",
          "NAME": "automorphspeed",
          "TYPE": "float",
          "MIN" : 0.0,
          "MAX" : 1.0,
          "DEFAULT": 0.27
        }, 
        {
          "LABEL": "AutoMorph/Transition",
          "NAME": "automorphtransitiontime",
          "TYPE": "float",
          "DEFAULT": 0.5,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "LABEL": "AutoMorph/Distort",
          "NAME": "shape_distort",
          "TYPE": "float",
          "DEFAULT": 2,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "LABEL": "AutoMorph/BPM Sync",
          "NAME": "bpmsync",
          "TYPE": "bool",
          "DEFAULT": true
        }, 
    ],
    "GENERATORS": [
        {"NAME": "morph_position", "TYPE": "time_base", "PARAMS": {"speed": "automorphspeed","bpm_sync": "bpmsync", "speed_curve": 3}},
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadSDF.glsl"
#include "MadCommon.glsl"

// A single iteration of Bob Jenkins' One-At-A-Time hashing algorithm.
int hash( int x ) {
    x += ( x << 10u );
    x ^= ( x >>  6u );
    x += ( x <<  3u );
    x ^= ( x >> 11u );
    x += ( x << 15u );
    return x;
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
    int stepId1 = int(pos/stepSize + shape_distort);
    int stepId2 = int(pos/stepSize + 2 + shape_distort);
    float animOffset = pow(abs(pos/stepSize-stepId1),automorphtransitiontime);
    int randInt11 = hash(stepId1+int(cellId.x));
    int randInt12 = hash(stepId1+1+int(cellId.x));
    int randInt21 = hash(stepId2+int(cellId.y));
    int randInt22 = hash(stepId2+1+int(cellId.y));
    float auto_radius_value = 0;//-(1+cos(randInt11))/5;

    vec2 rectCenter1 = rotate(rectCenter,mix(randInt11%180,randInt12%180,animOffset)/60.0f);
    vec2 rectCenter2 = rotate(rectCenter,mix(randInt21%180,randInt22%180,animOffset)/60.0f);
    float rotate3 = mix(randInt21%180,-randInt22%180,animOffset)/360.0f;

    float shapes[SHAPE_COUNT];
    shapes[0] = rectangle(rectCenter1,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[1] = circle(rectCenter1,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[2] = triangle(rectCenter2,(shape_radius+auto_radius_value)/shape_repeat);
    shapes[3] = arc(rectCenter1,(shape_radius+auto_radius_value)/shape_repeat,rotate3,0.1/shape_repeat);
    shapes[4] = hexagon(rectCenter2,(shape_radius+auto_radius_value)/shape_repeat);

    float shape1 = mix(shapes[abs(randInt11)%SHAPE_COUNT],shapes[abs(randInt12)%SHAPE_COUNT],animOffset);
    float shape2 = mix(shapes[stepId2%SHAPE_COUNT],shapes[(stepId2+1)%SHAPE_COUNT],animOffset);

    // Draw both shapes
    return vec4(  stroke(vec3(0,0,0),vec3(1,1,1),shape1,thickness / shape_repeat)
                + stroke(vec3(0,0,0),vec3(1,1,1),shape2,thickness / shape_repeat)
                , 1);
}

