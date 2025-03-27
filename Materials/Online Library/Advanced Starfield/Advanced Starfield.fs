/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Holden",
    "DESCRIPTION": "Advanced Starfield - Modified version of Warp Drive by Joe Griffith",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "Warp", "NAME": "warp", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 1 },  			  
        {"LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.001, "MAX": 1.0, "DEFAULT": 0.8 },  
        {"LABEL": "Pos/X pos", "NAME": "xpos", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": .6 }, 
        {"LABEL": "Pos/Y pos", "NAME": "ypos", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
        {"LABEL": "Star Density", "NAME": "starDensity", "TYPE": "float", "MIN": 0.08, "MAX": 4.0, "DEFAULT": 1 },
        {"LABEL": "Star Color 1", "NAME": "starColor1", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] }, 
        {"LABEL": "Star Color 2", "NAME": "starColor2", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] }, 
        {"LABEL": "Star Color 3", "NAME": "starColor3", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] }, 
        {"LABEL": "Tail Color", "NAME": "tailColor", "TYPE": "color", "DEFAULT": [0.9, 0.9, 0.9, 1.0] }, 
        {"LABEL": "Nebula Color", "NAME": "nebulaColor", "TYPE": "color", "DEFAULT": [0.5, 0.0, 0.5, 1.0] }, 
        {"LABEL": "Streaks", "NAME": "streaks", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
        {"LABEL": "Tail Length", "NAME": "tailLength", "TYPE": "float", "MIN": 0.1, "MAX": 3.0, "DEFAULT": 1.3 },
        {"LABEL": "Speed Mode", "NAME": "speedMode", "TYPE": "int", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
        {"LABEL": "Direction Lerp", "NAME": "direction", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": -1.0 },
        {"LABEL": "Reverse", "NAME": "distMode", "TYPE": "int", "MIN": 0, "MAX": 1, "DEFAULT": 0 }
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

// Function to generate a random value based on input
float random(float x) {
    return fract(sin(x) * 43758.5453);
}

// Function to generate a more random star color distribution
vec3 getRandomStarColor(float angleRnd1, float angleRnd2) {
    float colorMixFactor1 = random(angleRnd1);
    float colorMixFactor2 = random(angleRnd2);

    // Extract only the RGB (vec3) components from the star colors
    vec3 col1 = vec3(starColor1.rgb);
    vec3 col2 = vec3(starColor2.rgb);
    vec3 col3 = vec3(starColor3.rgb);

    vec3 starColor = mix(col1, mix(col2, col3, colorMixFactor1), colorMixFactor2);
    return starColor;
}

vec4 materialColorForPixel(vec2 texCoord) {
    // Convert UV coordinates to starfield coordinate system
    vec2 position = vec2(texCoord.x * 2.0 - xpos, texCoord.y * 2.0 - ypos);
    
    // Calculate polar coordinates
    float angle = atan(position.y, position.x) / 3.14159265359;
    float rad = length(position);
    
    // Initialize colors
    vec3 tailColor = vec3(tailColor.rgb); // Color for the tails
    vec3 nebulaColor = vec3(nebulaColor.rgb); // Color for the nebula
    
    // Randomness for each star
    float angleRnd = floor(angle * 333.0 * starDensity); // Adjust star density
    float angleRnd1 = fract(angleRnd * fract(angleRnd * 0.55) * 45.1);
    float angleRnd2 = fract(angleRnd * fract(angleRnd * 0.5) * 13.724);
    
    // Get a random star color
    vec3 starColor = getRandomStarColor(angleRnd1, angleRnd2);
    
    // Time-based adjustments
    float t2 = TIME + angleRnd1 * 111.0;
    float radDist = sqrt(angleRnd2);
    float adist;
    if (distMode == 0) {
        adist = radDist / rad * warp; // Mode 0
    } else {
        adist = radDist * rad * warp; // Mode 1
    }

    float dist;
    if (speedMode == 0) {
        dist = abs(fract((t2 * speed + adist) * direction) - 0.001); // Mode 0
    } else if (speedMode == 1) {
        float expSpeed = speed * direction;
        dist = abs(fract(t2 * exp(expSpeed) + adist) - 0.001); // Mode 2
    }
    
    // Adjust tail length with a single parameter using logarithmic scaling
    float sizeFactor = exp(-tailLength); // Logarithmic mapping
    float sizeX = 1.0 / sizeFactor;
    float sizeY = 1.0 / sizeFactor;
    
    // Calculate distance considering tail's length
    float tailLengthAdjusted = length(vec2(sizeX * position.x, sizeY * position.y));
    
    // Add nebula effect with reduced impact
    float nebulaFactor = smoothstep(0.1, 0.5, radDist); // Adjusted range for nebula effect
    vec3 color = mix(starColor, tailColor, adist / radDist);
    color = mix(color, nebulaColor, nebulaFactor * 0.3); // Reduced impact of nebulaColor
    
    // Calculate alpha with streaks effect
    float streaksFactor = smoothstep(0.0, 1.0, streaks); // Apply streaks as a factor
    float alpha = (2.2 / dist) * tailLengthAdjusted * adist / radDist / 50.0;
    alpha *= streaksFactor; // Apply streaks factor to alpha
    
    // Ensure output is a vec4 with explicit handling
    return vec4(color, alpha); // Explicitly use color and alpha
}