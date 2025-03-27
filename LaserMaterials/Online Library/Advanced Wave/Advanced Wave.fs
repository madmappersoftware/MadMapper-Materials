/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Lomax / PixelSlant Creative",
    "DESCRIPTION": "Laser Wave Shader with Color Function Selector",
    "TAGS": "laser, wave, color",
    "VSN": "1.8",
    "INPUTS": [
        {"LABEL": "Wave Count", "NAME": "wave_count", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 3},
        {"LABEL": "Wave Speed", "NAME": "wave_speed", "TYPE": "float", "MIN": 0.1, "MAX": 5.0, "DEFAULT": 1.0},
        {"LABEL": "Wave Amplitude", "NAME": "wave_amplitude", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1.0},
        {"LABEL": "Wave Phase Shift", "NAME": "wave_phase_shift", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
        {"LABEL": "Wave Frequency", "NAME": "wave_frequency", "TYPE": "float", "MIN": 0.1, "MAX": 10.0, "DEFAULT": 1.0},
        {"LABEL": "Wave Distortion", "NAME": "wave_distortion", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
        {"LABEL": "Wave Brightness", "NAME": "wave_brightness", "TYPE": "float", "MIN": 0.1, "MAX": 2.0, "DEFAULT": 1.0},
        {"LABEL": "Color Function", "NAME": "color_function", "TYPE": "long", "DEFAULT": "Gradient", "VALUES": ["Gradient", "Solid"]},
        {"LABEL": "Solid Color", "NAME": "solid_color", "TYPE": "color", "DEFAULT": [1, 1, 1, 1]},
        {"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 0.5 },
        {"LABEL": "Gradient Color/Color 1", "NAME": "color_1", "TYPE": "color", "DEFAULT": [1, 1, 1, 1] },
        {"LABEL": "Gradient Color/Color 2", "NAME": "color_2", "TYPE": "color", "DEFAULT": [0, 0, 0, 0] },
        {"LABEL": "Gradient Color/Color 3", "NAME": "color_3", "TYPE": "color", "DEFAULT": [0, 1, 0, 1] },
        {"LABEL": "Gradient Color/Color 4", "NAME": "color_4", "TYPE": "color", "DEFAULT": [0, 0, 1, 1] },
        {"LABEL": "Gradient Color/Color 5", "NAME": "color_5", "TYPE": "color", "DEFAULT": [1, 1, 0, 1] },
        {"LABEL": "Gradient Color/Color 6", "NAME": "color_6", "TYPE": "color", "DEFAULT": [0, 1, 1, 1] },
        {"LABEL": "Gradient Color/Color 7", "NAME": "color_7", "TYPE": "color", "DEFAULT": [1, 0, 1, 1] },
        {"LABEL": "Gradient Color/Color 8", "NAME": "color_8", "TYPE": "color", "DEFAULT": [0.5, 0.5, 0.5, 1] },
        {"LABEL": "Wave Type", "NAME": "wave_type", "TYPE": "long", "DEFAULT": "Sine", "VALUES": ["Sine", "Square", "Triangle", "Sawtooth", "Random"]},
        {"LABEL": "Draw Mode", "NAME": "draw_mode", "TYPE": "long", "DEFAULT": "Solid", "VALUES": ["Solid", "Dashed"]},
        {"LABEL": "Dash Width", "NAME": "dash_width", "TYPE": "float", "MIN": 0.01, "MAX": 0.5, "DEFAULT": 0.1},
        {"LABEL": "Number of Dashes", "NAME": "num_dashes", "TYPE": "int", "MIN": 1, "MAX": 20, "DEFAULT": 5}
    ]
}*/

float waveFunction(float x, float amplitude, float frequency, float phaseShift, float time) {
    frequency *= wave_frequency;
    float distortedX = x + wave_distortion * sin(frequency * x - phaseShift * 2.0 * 3.14159 - time);
    switch (wave_type) {
        case 0: // Sine wave
            return amplitude * sin(frequency * distortedX - phaseShift * 2.0 * 3.14159 - time);
        case 1: // Square wave
            return amplitude * sign(sin(frequency * distortedX - phaseShift * 2.0 * 3.14159 - time));
        case 2: // Triangle wave
            return amplitude * asin(sin(frequency * distortedX - phaseShift * 2.0 * 3.14159 - time)) * 2.0 / 3.14159;
        case 3: // Sawtooth wave
            return amplitude * (2.0 / 3.14159) * (atan(1.0 / tan(0.5 * 3.14159 * (frequency * distortedX - phaseShift - time))));
        case 4: // Random wave
            return amplitude * (fract(sin(dot(vec2(distortedX,time), vec2(12.9898, 78.233))) * 43758.5453) - 0.5);
        default:
            return amplitude * sin(frequency * distortedX - phaseShift * 2.0 * 3.14159 - time);
    }
}

vec3 getColor(int index) {
    if (index == 0) {
        return color_1.rgb;
    } else if (index == 1) {
        return color_2.rgb;
    } else if (index == 2) {
        return color_3.rgb;
    } else if (index == 3) {
        return color_4.rgb;
    } else if (index == 4) {
        return color_5.rgb;
    } else if (index == 5) {
        return color_6.rgb;
    } else if (index == 6) {
        return color_7.rgb;
    } else {
        return color_8.rgb;
    }
}

vec3 gradient(float t, float phaseShift) {
    float shiftedT = fract(t - phaseShift);
    float index = shiftedT * 8.0;
    int colorIndex = int(index);
    float tRemainder = fract(index);

    vec3 colorA = getColor(colorIndex);
    vec3 colorB = getColor((colorIndex + 1) % 8);

    return mix(colorA, colorB, tRemainder);
}

void drawDashedWave(float x, float y, float dashLength, float gapLength, float dashWidth, out float dashAlpha) {
    float totalLength = dashLength + gapLength;
    float posMod = mod(x, totalLength);
    float dashIndex = floor(posMod / (dashLength + gapLength));
    float posModWithinDash = mod(posMod, dashLength + gapLength);
    dashAlpha = smoothstep(dashLength - dashWidth, dashLength + dashWidth, posModWithinDash) - smoothstep(gapLength - dashWidth, gapLength + dashWidth, posModWithinDash);
    dashAlpha *= step(dashIndex, num_dashes);
    y *= dashAlpha;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData) {
    float time = TIME * wave_speed;
    float normalizedPos = float(pointNumber) / float(pointCount - 1);
    float x = normalizedPos * 2.0 - 1.0;
    float y = 0.0;
    float dashAlpha;

    for (int i = 0; i < wave_count; i++) {
        float frequency = float(i + 1) * 2.0;
        float amplitude = wave_amplitude / float(i + 1);
        float phaseShift = wave_phase_shift * float(i + 1);
        y += waveFunction(x, amplitude, frequency, phaseShift, time);
    }

    y *= mat_scale;

    if (draw_mode == 0) { // Solid
        pos = vec2(x, y);
        if (color_function == 0) { // Gradient
            color = vec4(gradient(normalizedPos, wave_phase_shift), wave_brightness);
        } else { // Solid color
            color = vec4(solid_color.rgb, wave_brightness);
        }
    } else { // Dashed
        float dashWidth = dash_width;
        drawDashedWave(x, y, 0.2, 0.3, dashWidth, dashAlpha);
        pos = vec2(x, y);
        if (color_function == 0) { // Gradient
            color = vec4(gradient(normalizedPos, wave_phase_shift), wave_brightness * dashAlpha);
        } else { // Solid color
            color = vec4(solid_color.rgb, wave_brightness * dashAlpha);
        }
    }

    shapeNumber = 0;
    userData = vec4(0.0);
}
