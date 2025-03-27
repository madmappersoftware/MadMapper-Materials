/*{
    "CREDIT": "Matt Beghin",
    "CATEGORIES": ["Graphic"],
    "VSN": "1.0",
    "INPUTS": [
        {
          "Label": "Repeat",
          "NAME": "shape_repeat",
          "TYPE": "int",
          "MIN" : 1.0,
          "MAX" : 16.0,
          "DEFAULT": 3.0
        },
        {
          "NAME": "thickness",
          "Label": "Thickness",
          "TYPE": "float",
          "DEFAULT": 0.01,
          "MIN": 0.001,
          "MAX": 0.5
        },
        {
          "Label": "Radius 1",
          "NAME": "shape1_radius",
          "TYPE": "float",
          "DEFAULT": 0.146,
          "MIN": 0.0,
          "MAX": 0.5
        },
        {
          "Label": "Radius 2",
          "NAME": "shape2_radius",
          "TYPE": "float",
          "DEFAULT": 0.522,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "Label": "Distort 1",
          "NAME": "shape1_distort",
          "TYPE": "float",
          "DEFAULT": 1.65,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "Label": "Distort 2",
          "NAME": "shape2_distort",
          "TYPE": "float",
          "DEFAULT": 2.0,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "NAME": "bpmsync",
          "LABEL": "BPM Sync",
          "TYPE": "bool",
          "DEFAULT": true
        },
        {
          "NAME": "automorphactive",
          "LABEL": "AutoMorph/Auto Morph",
          "TYPE": "bool",
          "DEFAULT": true,
          "FLAGS": "button"
        },
        {
          "NAME": "automorphspeed",
          "Label": "AutoMorph/Speed",
          "TYPE": "float",
          "MIN" : 0.0,
          "MAX" : 1.0,
          "DEFAULT": 0.35
        },
        {
          "NAME": "automorphstrob",
          "LABEL": "AutoMorph/Strob",
          "TYPE": "float",
          "DEFAULT": 0.0,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "automorphshape",
          "LABEL": "AutoMorph/Shape",
          "TYPE": "long",
          "VALUES": ["Smooth","In","Out","Linear","Cut"],
          "DEFAULT": "In",
          "FLAGS": "generate_as_define"
        },
        {
          "NAME": "autoradiusactive",
          "LABEL": "AutoRadius/Auto Radius",
          "TYPE": "bool",
          "DEFAULT": false,
          "FLAGS": "button"
        },
        {
          "NAME": "autoradiussize",
          "LABEL": "AutoRadius/Size",
          "TYPE": "float",
          "DEFAULT": 0.17,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "autoradiusspeed",
          "LABEL": "AutoRadius/Speed",
          "TYPE": "float",
          "DEFAULT": 0.8,
          "MIN": 0.0,
          "MAX": 2.0
        },
        {
          "NAME": "autoradiusstrob",
          "LABEL": "AutoRadius/Strob",
          "TYPE": "float",
          "DEFAULT": 0.0,
          "MIN": 0.0,
          "MAX": 1.0
        },
        {
          "NAME": "autoradiusshape",
          "LABEL": "AutoRadius/Shape",
          "TYPE": "long",
          "VALUES": ["Smooth","In","Out","Linear","Cut"],
          "DEFAULT": "In",
          "FLAGS": "generate_as_define"
        }, 
    ],
    "GENERATORS": [
        {"NAME": "auto_morph_value", "TYPE": "animator", "PARAMS": {"active": "automorphactive", "speed": "automorphspeed","strob": "automorphstrob", "bpm_sync": "bpmsync", "speed_curve": 3, "shape": "automorphshape"}},
        {"NAME": "auto_radius_value", "TYPE": "animator", "PARAMS": {"active": "autoradiusactive", "speed": "autoradiusspeed","strob": "autoradiusstrob", "bpm_sync": "bpmsync", "speed_curve": 3, "shape": "autoradiusshape"}},
    ]
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadSDF.glsl"
#include "MadCommon.glsl"

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 center = texCoord;
    vec2 cellId;
    vec2 rectCenter = repeat(center,vec2(1.0/shape_repeat),cellId);
	
    #define SHAPE_COUNT 5
    float shapes[SHAPE_COUNT];
    float autoRadiusValue = autoradiussize * auto_radius_value;
    shapes[0] = rectangle(rectCenter,(shape1_radius+autoRadiusValue)/shape_repeat);
    shapes[1] = circle(rectCenter,(shape1_radius+autoRadiusValue)/shape_repeat);
    shapes[2] = triangle(rectCenter,(shape1_radius+autoRadiusValue)/shape_repeat);
    shapes[3] = circle(rectCenter,(shape1_radius+autoRadiusValue)/shape_repeat);
    shapes[4] = hexagon(rectCenter,(shape1_radius+autoRadiusValue)/shape_repeat);

    float stepSize = 1.0/SHAPE_COUNT;
    float mm_mat_curve = 1.0;
    float pos = auto_morph_value *2+ (cellId.x + cellId.y * shape_repeat) / (shape_repeat*shape_repeat);
    int stepId = int(pos/stepSize + shape1_distort);
    float dist1 = mix(shapes[stepId%SHAPE_COUNT],shapes[(stepId+1)%SHAPE_COUNT],pow(pos/stepSize-stepId,mm_mat_curve));

    stepId = int(pos/stepSize + shape2_distort);
    float dist2 = (0.5-shape2_radius)/shape_repeat + mix(shapes[stepId%SHAPE_COUNT],shapes[(stepId+1)%SHAPE_COUNT],pow(pos/stepSize-stepId,mm_mat_curve));

    return vec4(stroke(vec3(0,0,0),vec3(1,1,1),dist1,thickness / shape_repeat), 1) + vec4(stroke(vec3(0,0,0),vec3(1,1,1),dist2,thickness / shape_repeat), 1);
}

