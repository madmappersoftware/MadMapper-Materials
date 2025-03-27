/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Demo of a few vector shapes",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.8 }, 
        {"LABEL": "Global/Shape", "NAME": "mat_shape", "TYPE": "long", "DEFAULT": "Triangle", "VALUES": ["Triangle","Square","Hexagon","Circle"] },
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": 100
    }
}*/

#include "MadCommon.glsl"

vec3 triangle(int pointNumber, int pointCount)
{
    if (pointNumber == 0) {
        return vec3(-mat_size,-mat_size,0);
    } else if (pointNumber == 1) {
        return vec3(0,mat_size,0);
    } else if (pointNumber == 2) {
        return vec3(mat_size,-mat_size,0);
    } else  if (pointNumber == 3) {
        return vec3(-mat_size,-mat_size,0);
    } else {
        return vec3(0,0,-1);
    }
}

vec3 square(int pointNumber, int pointCount)
{
    if (pointNumber == 0) {
        return vec3(-mat_size,-mat_size,0);
    } else if (pointNumber == 1) {
        return vec3(-mat_size,mat_size,0);
    } else if (pointNumber == 2) {
        return vec3(mat_size,mat_size,0);
    } else if (pointNumber == 3) {
        return vec3(mat_size,-mat_size,0);
    } else if (pointNumber == 4) {
        return vec3(-mat_size,-mat_size,0);
    } else {
        return vec3(0,0,-1);
    }
}

vec3 polygon(int polygonSteps, int pointNumber, int pointCount)
{
    if (pointNumber > polygonSteps) {
        return vec3(0,0,-1);
    }
    float angle = pointNumber *  2*3.141592654 / polygonSteps;
    vec2 pos = mat_size * vec2(cos(angle), sin(angle));
    return vec3(pos,0);
}

vec3 circle(int pointNumber, int pointCount)
{
    return vec3(mat_size*cos(pointNumber*2*PI/(pointCount-1)), mat_size*sin(pointNumber*2*PI/(pointCount-1)),0);
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    vec3 value;
    if (mat_shape == 0) {
        value = triangle(pointNumber, pointCount);
    }
    else if (mat_shape == 1) {
        value = square(pointNumber, pointCount);
    }
    else if (mat_shape == 2) {
        value = polygon(6, pointNumber, pointCount);
    }
    else if (mat_shape == 3) {
        value = circle(pointNumber, pointCount);
    }
    pos = value.xy;
    shapeNumber = int(value.z);
    color = vec4(1);
}
