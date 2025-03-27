/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Ctrl-Z",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
        {
          "NAME": "mat_waveform",
          "TYPE": "audio",
        },
        {"LABEL": "Global/Count", "NAME": "mat_shape_count", "TYPE": "int", "DEFAULT": 4, "MIN": 0, "MAX": 50 }, 
        {"LABEL": "Global/V Spacing", "NAME": "mat_v_spacing", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 }, 
        {"LABEL": "Global/Translation Y", "NAME": "mat_base_transitiony", "TYPE": "float", "DEFAULT": -1.0, "MIN": -1.0, "MAX": 1.0 },
        {"LABEL": "Audio/Power", "NAME": "mat_audiopower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        {"LABEL": "Audio/Scale", "NAME": "mat_audioscale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
        {"LABEL": "Noise/Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        {"LABEL": "Noise/Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
        {"LABEL": "Noise/Speed", "NAME": "mat_noisespeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        {"LABEL": "Color/Out", "NAME": "outColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.9, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
        {"LABEL": "Color/Center", "NAME": "centerColor", "TYPE": "color", "DEFAULT": [ 0.75, .0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
        {"LABEL": "Color/Center Width", "NAME": "center_color_width", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.306 },
        {"LABEL": "Color/Gradient Intensity", "NAME": "gradient_intensity", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.556 }
    ],
    "GENERATORS": [
        {"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noisespeed", "speed_curve":2, "bpm_sync": false, "link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4000
    }
}*/

#include "auto_all.glsl"
vec3 staticCol = vec3(1.);

float parabola(float x, float k) {
    return pow(4.0 * x * (1.0 - x), k);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
    int pointsPerShape = pointCount / mat_shape_count;
    shapeNumber = pointNumber / pointsPerShape;
    if (shapeNumber >= mat_shape_count) {
        shapeNumber = -1; // point will be ignored if shape number is negative
        return;
    }
    float normalizedShapeId = float(shapeNumber) / mat_shape_count;

    // Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
    float normalizedPosInShape = float(pointNumber - shapeNumber * pointsPerShape) / (pointsPerShape - 1);

    // 2 points per line
    int lineNumber = shapeNumber;

    float size = getSizeValue(normalizedShapeId);

    pos = vec2(size * (-1 + 2 * normalizedPosInShape), mat_v_spacing * (-1 + 2 * normalizedShapeId + mat_base_transitiony));

    // Move
    float translateX = 0;
    if (mat_automoveactive) {
        if (mat_automoveactive) {
            // "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
            if (mat_automoveshape == 0) {
                translateX = mat_automovesize * sin((mat_move_position + normalizedShapeId * mat_automoveoffset) * 2 * PI) / 2;
            } else if (mat_automoveshape == 1) {
                translateX = mat_automovesize * fract(mat_move_position + normalizedShapeId * mat_automoveoffset);
            } else if (mat_automoveshape == 2) {
                translateX = mat_automovesize * (0.5 - abs(mod((mat_move_position + normalizedShapeId * mat_automoveoffset) * 2 + 1, 2) - 1));
            } else if (mat_automoveshape == 3) {
                translateX = mat_automovesize * (0.5 - step(0.5, mod((mat_move_position + normalizedShapeId * mat_automoveoffset), 1)));
            } else if (mat_automoveshape == 4) {
                translateX = mat_automovesize * (0.5 * noise(vec2((mat_move_position + normalizedShapeId * mat_automoveoffset * 99.5), 0)));
            } else {
                translateX = mat_automovesize * (-0.5 * sin(-PI / 2 + mod((mat_move_position + normalizedShapeId * mat_automoveoffset), 1) * PI));
            }
        }
    }
    pos.y = -1 + 2 * (fract((pos.y + 1) / 2 + translateX));

    if (mat_audiopower > 0) {
        pos.y += IMG_NORM_PIXEL(mat_waveform, vec2(normalizedPosInShape * mat_audioscale, 0)).r * mat_audiopower * mat_audiopower;
    }

    if (mat_noisepower > 0) {
        pos.y += mat_noisepower * noise(mat_noisescale * vec2(normalizedPosInShape, normalizedShapeId) + vec2(0, mat_noise_time));
    }

    mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
    pos = (vec3(pos, 1) * transformMatrix).xy;

    // Calculate the distance from the center and determine the color based on this distance
    float centerDist = abs(pos.x / size);
    float colorBlend = smoothstep(center_color_width, center_color_width + gradient_intensity, centerDist); // Adjust the gradient intensity

    color = vec4(mix(staticCol.rgb, centerColor.rgb, colorBlend)*getColorValue(normalizedShapeId).rgb*getLightValue(normalizedShapeId), 1.0);
}
