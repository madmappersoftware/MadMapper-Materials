/*{
    "CREDIT": "Dr Algo",
    "DESCRIPTION": "Comètes lumineuses multicolores avec traces, chemins variables et vitesses différentes",
    "CATEGORIES": [
        "Generator"
    ],
    "INPUTS": [
        {
            "NAME": "mat_comet_count",
            "LABEL": "Nombre de comètes",
            "TYPE": "int",
            "MIN": 10,
            "MAX": 300,
            "DEFAULT": 50
        },
        {
            "NAME": "mat_color",
            "LABEL": "Couleur",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "NAME": "mat_multicolor",
            "LABEL": "Multicolore",
            "TYPE": "bool",
            "DEFAULT": false
        },
        {
            "NAME": "mat_head_intensity",
            "LABEL": "Intensité tête",
            "TYPE": "float",
            "MIN": 1.0,
            "MAX": 20.0,
            "DEFAULT": 5.0
        },
        {
            "NAME": "mat_head_size",
            "LABEL": "Taille tête",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 0.1,
            "DEFAULT": 0.03
        },
        {
            "NAME": "mat_tail_length",
            "LABEL": "Longueur trace",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 2.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "mat_speed",
            "LABEL": "Vitesse globale",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "NAME": "mat_speed_variation",
            "LABEL": "Variation de vitesse",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "mat_desync",
            "LABEL": "Désynchronisation",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "mat_path_type",
            "LABEL": "Type de chemin",
            "TYPE": "long",
            "VALUES": [
                "Circulaire",
                "Rectiligne",
                "Aléatoire",
                "Zig Zag"
            ],
            "DEFAULT": "Circulaire"
        },
        {
            "NAME": "mat_zigzag_amplitude",
            "LABEL": "Amplitude Zig Zag",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 1.0,
            "DEFAULT": 0.5
        },
        {
            "NAME": "mat_tail_follow",
            "LABEL": "Suivi de la queue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        }
    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed"
            }
        }
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec2 getPosition(float t, float i, int pathType) {
    if (pathType == 0) { // Circulaire
        return vec2(sin(t * 0.5 + i * 1.1), cos(t * 0.7 + i * 0.9)) * 0.4;
    } else if (pathType == 1) { // Rectiligne
        return vec2(mod(t * 0.2 + i * 0.1, 2.0) - 1.0, sin(i * 3.14159 * 2.0 / float(mat_comet_count)) * 0.8);
    } else if (pathType == 2) { // Aléatoire
        vec2 n = vec2(noise(vec2(t * 0.1 + i, t * 0.15)), noise(vec2(t * 0.12 + i, t * 0.17)));
        return n * 0.5;
    } else { // Zig Zag
        float x = mod(t * 0.2 + i * 0.1, 2.0) - 1.0;
        return vec2(x, sin(x * 3.14159 * 4.0) * mat_zigzag_amplitude) * 0.5;
    }
}

float getTailIntensity(vec2 uv, vec2 tailStart, vec2 tailEnd, float tailLength) {
    vec2 dir = tailEnd - tailStart;
    float projDist = clamp(dot(uv - tailStart, dir) / dot(dir, dir), 0.0, 1.0) * tailLength;
    float perpDist = length((uv - tailStart) - projDist * normalize(dir));
    float tailWidth = mix(0.02, 0.005, projDist / tailLength);
    return smoothstep(tailWidth, 0.0, perpDist) * smoothstep(1.0, 0.0, projDist / tailLength);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = texCoord - 0.5;
    vec3 finalColor = vec3(0.0);
    
    for (int i = 0; i < mat_comet_count; i++) {
        float speedFactor = 1.0 + (noise(vec2(float(i) * 0.1, 23.45)) * 2.0 - 1.0) * mat_speed_variation;
        float t = mat_time * speedFactor + float(i) * mat_desync;
        vec2 pos = getPosition(t, float(i), int(mat_path_type));
        
        // Calcul de la queue
        float tailLength = mat_tail_length;
        vec2 prevPos = getPosition(t - tailLength / speedFactor, float(i), int(mat_path_type));
        vec2 straightTailEnd = pos + normalize(prevPos - pos) * tailLength;
        
        vec2 tailEnd = mix(straightTailEnd, prevPos, mat_tail_follow);
        float tailIntensity = getTailIntensity(uv, pos, tailEnd, tailLength);
        
        vec3 color = mat_multicolor ? hsv2rgb(vec3(t * 0.1 + float(i) * 0.1, 1.0, 1.0)) : mat_color.rgb;
        finalColor += tailIntensity * color;
        
        // Tête de la comète
        float headDist = length(uv - pos);
        float head = pow(smoothstep(mat_head_size, 0.0, headDist), 2.0);
        finalColor += head * color * mat_head_intensity;
    }
    
    finalColor = min(finalColor, vec3(1.0));
    return vec4(finalColor, 1.0);
}
