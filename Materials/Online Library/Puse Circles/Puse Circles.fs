/*{
    "DESCRIPTION": "Multiple animated circles with blurred edges",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mad-matt",
    "INPUTS": [
        { "LABEL": "Circles/Number", "NAME": "mat_nbCircles", "TYPE": "int", "DEFAULT": 25, "MIN": 1, "MAX": 100 },
        { "LABEL": "Circles/Radius", "NAME": "mat_radius", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0.01, "MAX": 0.3 },
        { "LABEL": "Circles/Blur", "NAME": "mat_blur", "TYPE": "float", "DEFAULT": 0.01, "MIN": 0.0, "MAX": 0.1 },
        { "LABEL": "Animation/Speed", "NAME": "mat_speed", "TYPE": "float", "DEFAULT": 2, "MIN": 0.0, "MAX": 20.0 },
        { "LABEL": "Animation/PulseSize", "NAME": "mat_pulseSize", "TYPE": "float", "DEFAULT": 0.05, "MIN": 0.0, "MAX": 0.2 },
        { "LABEL": "Animation/PulseSpeed", "NAME": "mat_pulseSpeed", "TYPE": "float", "DEFAULT": 0.7, "MIN": 0.0, "MAX": 20.0 },
        { "LABEL": "Animation/Amplitude", "NAME": "mat_amplitude", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 0.5 },
        { "LABEL": "Color/Base", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [ 0.5, 0.8, 1.0, 1.0 ] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Color/HueOffset", "NAME": "mat_hueOffset", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Color/ColorMode", "NAME": "mat_colorMode", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },
        { "LABEL": "Controls/Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }
    ],
    "GENERATORS": [
        { "NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "speed_curve": 1, "link_speed_to_global_bpm":true} },
        { "NAME": "mat_pulsetime", "TYPE": "time_base", "PARAMS": {"speed": "mat_pulseSpeed", "reverse": "mat_reverse", "speed_curve": 1, "link_speed_to_global_bpm":true} }
    ]
}*/

// HSV to RGB conversion
vec3 hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

// RGB to HSV conversion
vec3 rgb2hsv(vec3 c) {
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));
    
    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

vec4 materialColorForPixel(vec2 texCoord) {
    // Invert Y coordinate
    texCoord.y = 1.0 - texCoord.y;
    
    // Initialize final color
    vec3 finalColor = vec3(0.0);
    
    // Convert base color to HSV
    vec3 baseColorHSV = rgb2hsv(mat_color.rgb);
    
    // Create multiple circles
    for (float i = 0.0; i < 100.0; i++) {
        if (i >= mat_nbCircles) break;
        
        // Circle position with phase offset
        float phase = i * 6.28 / mat_nbCircles;
        float posX = 0.5 + mat_amplitude * cos(mat_time + phase);
        float posY = 0.5 + mat_amplitude * sin(mat_time * 0.7 + phase);
        
        // Animated radius with offset per circle
        float animRadius = mat_radius + mat_pulseSize * sin(mat_pulsetime + phase);
        
        // Calculate distance and apply blur
        float dist = distance(texCoord, vec2(posX, posY));
        float blurFactor = max(0.001, mat_blur); // Prevent division by zero
        float circleValue = smoothstep(animRadius, animRadius - blurFactor, dist) * 0.5;
        
        // Apply soft edge with configurable blur
        if (mat_blur > 0.0) {
            circleValue = 0.5 * (1.0 - smoothstep(animRadius - blurFactor, animRadius + blurFactor, dist));
        }
        
        // Apply hue offset for each circle
        vec3 circleHSV = baseColorHSV;
        circleHSV.x = fract(circleHSV.x + i * pow(mat_hueOffset,3));
        vec3 circleRGB = hsv2rgb(circleHSV);
        
        // Add contribution (additive blending)
        finalColor += circleValue * mat_brightness * (mat_colorMode ? circleRGB : vec3(1.0));
    }
    
    // Clamp final color
    finalColor = min(finalColor, vec3(1.0));
    
    return vec4(finalColor, 1.0);
}