/*{
    "CREDIT": "Simon Holden",
    "DESCRIPTION": "Test modes for LED configuration",
    "TAGS": "utility,color,gradient",
    "VSN": "1.5",
    "INPUTS": [
        {"LABEL": "Mode", "NAME": "mat_color", "TYPE": "long", "DEFAULT": "Change RGB", "VALUES": ["Red", "Green", "Blue", "White", "Change RGB", "Change RGBW", "Jump RGB", "Jump RGBW", "Scroll"]},
        {"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        {"LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button"},
        {"LABEL": "Scroll/Color Count", "NAME": "mat_color_count", "TYPE": "int", "DEFAULT": 3, "MIN": 1, "MAX": 4 },
        {"LABEL": "Scroll/Color 1", "NAME": "mat_color1", "TYPE": "color", "DEFAULT": [1.0, 0.0, 0.0, 1.0] },
        {"LABEL": "Scroll/Color 2", "NAME": "mat_color2", "TYPE": "color", "DEFAULT": [0.0, 1.0, 0.0, 1.0] },
        {"LABEL": "Scroll/Color 3", "NAME": "mat_color3", "TYPE": "color", "DEFAULT": [0.0, 0.0, 1.0, 1.0] },
        {"LABEL": "Scroll/Color 4", "NAME": "mat_color4", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] },
        {"LABEL": "Scroll/Rotate", "NAME": "mat_base_rotate", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
        {"LABEL": "Scroll/Scale", "NAME": "mat_base_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
        {"LABEL": "Scroll/Blend Curve", "NAME": "mat_curve", "TYPE": "float", "DEFAULT": 2, "MIN": 1, "MAX": 20 }
    ],
    "GENERATORS": [
        {"NAME": "mat_anim", "TYPE": "animator", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "shape": "linear"}},
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse"}}
    ]
}
*/


#include "MadCommon.glsl"
#include "MadNoise.glsl"

// Blend curve function with sharp and smooth ends
float blendCurve(float t, float curve) {
    float result;
    if (curve < 1.0) {
        result = pow(t, 1.0 / (1.0 - curve)); // Sharp start, smooth end
    } else {
        result = pow(t, curve); // Smooth start, sharp end
    }
    return result;
}

vec4 materialColorForPixel(vec2 texCoord) {
    vec3 color;
    float pos, stepSize;
    float animPosition = mod(mat_time * mat_speed, 1.0); // Use animator for continuous scroll

    if (mat_color == 0) { // Red
        color = vec3(mat_color1);
    } else if (mat_color == 1) { // Green
        color = vec3(mat_color2);
    } else if (mat_color == 2) { // Blue
        color = vec3(mat_color3);
    } else if (mat_color == 3) { // White
        color = vec3(mat_color4);
    } else if (mat_color == 4) { // Change RGB
        float fadePos = mod(mat_time * mat_speed * 0.1, 3.0); // Speed of change
        if (fadePos < 1.0) {
            color = mix(vec3(mat_color1), vec3(mat_color2), fadePos); // Red to Green
        } else if (fadePos < 2.0) {
            color = mix(vec3(mat_color2), vec3(mat_color3), fadePos - 1.0); // Green to Blue
        } else {
            color = mix(vec3(mat_color3), vec3(mat_color1), fadePos - 2.0); // Blue to Red
        }
    } else if (mat_color == 5) { // Change RGBW
        float fadePos = mod(mat_time * mat_speed * 0.1, 4.0); // Speed of fade
        if (fadePos < 1.0) {
            color = mix(vec3(mat_color1), vec3(mat_color2), fadePos); // Red to Green
        } else if (fadePos < 2.0) {
            color = mix(vec3(mat_color2), vec3(mat_color3), fadePos - 1.0); // Green to Blue
        } else if (fadePos < 3.0) {
            color = mix(vec3(mat_color3), vec3(mat_color4), fadePos - 2.0); // Blue to White
        } else {
            color = mix(vec3(mat_color4), vec3(mat_color1), fadePos - 3.0); // White to Red
        }
    } else if (mat_color == 6) { // Jump RGB
        float cycleTime = mod(mat_time * mat_speed, 3.0); // Animation cycle time
        if (cycleTime < 1.0) {
            color = vec3(mat_color1); // Red
        } else if (cycleTime < 2.0) {
            color = vec3(mat_color2); // Green
        } else {
            color = vec3(mat_color3); // Blue
        }
    } else if (mat_color == 7) { // Jump RGBW
        float cycleTime = mod(mat_time * mat_speed, 4.0); // Animation cycle time
        if (cycleTime < 1.0) {
            color = vec3(mat_color1); // Red
        } else if (cycleTime < 2.0) {
            color = vec3(mat_color2); // Green
        } else if (cycleTime < 3.0) {
            color = vec3(mat_color3); // Blue
        } else {
            color = vec3(mat_color4); // White
        }
    } else if (mat_color == 8) { // Scroll
        float angle = mat_base_rotate * 2.0 * PI / 360.0;
        float sinFactor = sin(angle);
        float cosFactor = cos(angle);
        vec2 uv = (texCoord - vec2(0.5)) * mat2(cosFactor, sinFactor, -sinFactor, cosFactor);
        
        // Continuous scrolling
        pos = uv.x * mat_base_scale; // Apply base scale and animation position
        pos = fract(pos); // Ensure the position is between 0 and 1
 	    pos += animPosition;
  
        vec4 colors[4];
        colors[0] = mat_color1;
        colors[1] = mat_color2;
        colors[2] = mat_color3;
        colors[3] = mat_color4;

        stepSize = 1.0 / float(mat_color_count);
        int stepId = int(pos / stepSize);
        float localPos = (pos / stepSize) - float(stepId);

        // Apply the improved blend curve
        float curveValue = blendCurve(localPos, mat_curve);
      color = mix(colors[stepId%mat_color_count].rgb, colors[(stepId + 1) % mat_color_count].rgb, curveValue);

    }

    return vec4(color, 1.0);
}
