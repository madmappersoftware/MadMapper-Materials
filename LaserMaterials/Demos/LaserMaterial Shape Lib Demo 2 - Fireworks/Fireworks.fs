/*{
    "DESCRIPTION": "Laser Fireworks",
    "CREDIT": "Made with MadLaserMaterialShapeLibrary",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "DEFAULT": 0.1, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Sparks", "NAME": "mat_sparks", "TYPE": "int", "DEFAULT": 15, "MIN": 5, "MAX": 30 },
        { "LABEL": "Fireworks", "NAME": "mat_fireworks", "TYPE": "int", "DEFAULT": 4, "MIN": 1, "MAX": 8 },
        { "LABEL": "Explosion Size", "NAME": "mat_size", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.1, "MAX": 1.5 },
        { "LABEL": "Gravity", "NAME": "mat_gravity", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Trail Length", "NAME": "mat_trail_len", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 0.7 },
        { "LABEL": "Color/Red", "NAME": "mat_red", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Color/Green", "NAME": "mat_green", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Color/Blue", "NAME": "mat_blue", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Color/Random", "NAME": "mat_random", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "BPM Sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": false }
    ],
    "GENERATORS": [
        { "NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "speed_curve": 1, "link_speed_to_global_bpm": "mat_bpm_sync"} }
    ]
}*/

#ifndef PI
#define PI 3.14159265359
#endif

#include "MadLaserMaterialShapeLibrary.glsl"

float rand(vec2 co) {
    return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

float rand(float x) {
    return rand(vec2(x, x * 1.234));
}

vec3 hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
    // Initialize
    pos = vec2(0.0);
    color = vec4(1.0);
    shapeNumber = 0;
    userData = vec4(0.0);
    float normalizedPosInShape;
    
    // Initialize shape library
    sl_init(pointNumber);
    
    // Time control
    float time = mat_time * 2.0;
    int cycleTime = int(time);
    float localTime = fract(time);
    
    // Firework parameters
    int numFireworks = mat_fireworks;
    int sparksPerFirework = mat_sparks;
    float gravity = mat_gravity * 0.5;
    float trailLength = mat_trail_len;
    float explosionSize = mat_size;
    
    // Color base
    vec3 baseColor = vec3(mat_red, mat_green, mat_blue);
    
    // Process each firework
    for (int f = 0; f < numFireworks; f++) {
        // Generate consistent random values for this firework
        float fSeed = float(f) + float(cycleTime) * 100.0;
        float launchTime = rand(fSeed + 42.0) * 0.8;
        vec2 originPos = vec2(rand(fSeed) * 1.8 - 0.9, -1.1);
        vec2 targetPos = vec2(originPos.x + (rand(fSeed + 10.0) * 0.4 - 0.2), rand(fSeed + 20.0) * 0.8 + 0.2);
        
        // Color for this firework
        float hue = rand(fSeed + 30.0);
        float colorRand = rand(fSeed + 40.0);
        vec3 fireworkColor = mix(baseColor, hsv2rgb(vec3(hue, 0.9, 1.0)), mat_random);
        
        // Launch phase
        if (localTime < launchTime) {
            // Not yet launched
            continue;
        } else if (localTime < launchTime + 0.2) {
            // Launching trail
            float launchProgress = (localTime - launchTime) / 0.2;
            vec2 rocketPos = mix(originPos, targetPos, launchProgress);
            
            // Draw launch trail
            sl_beginPath();
            sl_moveTo(originPos);
            sl_lineTo(rocketPos);
            if (sl_endPath(normalizedPosInShape)) {
                shapeNumber = sl_getThisPointShapeNumber();
                pos = sl_getThisPointPosition();
                float fade = 1.0 - normalizedPosInShape;
                color = vec4(fireworkColor * fade, 1.0);
                return;
            }
        } else {
            // Explosion phase
            float explosionTime = (localTime - launchTime - 0.2) * 2.0;
            if (explosionTime > 1.0) continue; // Explosion completed
            
            // Draw explosion sparks
            for (int s = 0; s < sparksPerFirework; s++) {
                float sSeed = fSeed + float(s) * 10.0;
                float angle = rand(sSeed) * 2.0 * PI;
                float speed = (rand(sSeed + 5.0) * 0.5 + 0.5) * explosionSize;
                
                // Spark trajectory considering gravity
                vec2 sparkDirection = vec2(cos(angle), sin(angle)) * speed;
                vec2 sparkPos = targetPos + sparkDirection * explosionTime;
                sparkPos.y -= gravity * explosionTime * explosionTime; // Gravity effect
                
                // Draw spark with trail
                if (explosionTime > trailLength) {
                    vec2 trailStart = targetPos + sparkDirection * (explosionTime - trailLength);
                    trailStart.y -= gravity * (explosionTime - trailLength) * (explosionTime - trailLength);
                    
                    sl_beginPath();
                    sl_moveTo(trailStart);
                    sl_lineTo(sparkPos);
                    if (sl_endPath(normalizedPosInShape)) {
                        shapeNumber = sl_getThisPointShapeNumber();
                        pos = sl_getThisPointPosition();
                        float brightness = 1.0 - explosionTime;
                        color = vec4(fireworkColor * (brightness * (1.0 - normalizedPosInShape)), 1.0);
                        return;
                    }
                } else {
                    sl_beginPath();
                    sl_moveTo(targetPos);
                    sl_lineTo(sparkPos);
                    if (sl_endPath(normalizedPosInShape)) {
                        shapeNumber = sl_getThisPointShapeNumber();
                        pos = sl_getThisPointPosition();
                        float brightness = 1.0 - explosionTime;
                        color = vec4(fireworkColor * (brightness * (1.0 - normalizedPosInShape)), 1.0);
                        return;
                    }
                }
            }
        }
    }
    
    // If no firework drawn for this point
    shapeNumber = -1;
}