/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Created by Alex",
    "DESCRIPTION": "Switch between multiple shapes (square, circle, triangle, star, cross, line) with optional symmetric color gradient and animated movement. Auto-scaling, rotation, and automatic/manual Y and X movement with phase offset. Rotation is always around the center of movement. Includes strobe effect with delay.",
    "TAGS": "laser, tool, square, circle, triangle, star, cross, line, gradient, animation, scaling, rotation, movement, strobe",
    "VSN": "3.0",
    "INPUTS": [
        // Shape Selection
        {"LABEL": "Shape Type", "NAME": "shapeType", "TYPE": "int", "DEFAULT": 0, "MIN": 0, "MAX": 5, "VALUES": ["Square", "Circle", "Triangle", "Star", "Cross", "Line"]},

        // Colors
        {"LABEL": "Shape Color", "NAME": "shapeColor", "TYPE": "color", "DEFAULT": [1,1,1,1]},

        // Gradient
        {"LABEL": "Use Gradient", "NAME": "useGradient", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Gradient Start Color", "NAME": "gradientStartColor", "TYPE": "color", "DEFAULT": [1,0,0,1]},
        {"LABEL": "Gradient End Color", "NAME": "gradientEndColor", "TYPE": "color", "DEFAULT": [0,0,1,1]},
        {"LABEL": "Gradient Iterations", "NAME": "gradientIterations", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 10},
        {"LABEL": "Gradient Speed", "NAME": "gradientSpeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0},

        // Scaling
        {"LABEL": "Auto Scale", "NAME": "autoScale", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Scale Speed", "NAME": "scaleSpeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0},
        {"LABEL": "Scale Amount", "NAME": "scaleAmount", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 1.0},
        {"LABEL": "Manual Scale", "NAME": "manualScale", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 2.0},

        // Rotation
        {"LABEL": "Auto Rotate", "NAME": "autoRotate", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Rotate Speed", "NAME": "rotateSpeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0},

        // Movement (Y)
        {"LABEL": "Auto Move Y", "NAME": "autoMoveY", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Move Y Speed", "NAME": "moveYSpeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0},
        {"LABEL": "Move Y Amount", "NAME": "moveYAmount", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 0.9},
        {"LABEL": "Phase Offset Y", "NAME": "phaseOffsetY", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0},

        // Movement (X)
        {"LABEL": "Auto Move X", "NAME": "autoMoveX", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Move X Speed", "NAME": "moveXSpeed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0},
        {"LABEL": "Move X Amount", "NAME": "moveXAmount", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 0.5},
        {"LABEL": "Phase Offset X", "NAME": "phaseOffsetX", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0},

        // Manual Movement
        {"LABEL": "Manual Move X", "NAME": "manualMoveX", "TYPE": "float", "DEFAULT": 0.0, "MIN": -1.0, "MAX": 1.0},
        {"LABEL": "Manual Move Y", "NAME": "manualMoveY", "TYPE": "float", "DEFAULT": 0.0, "MIN": -1.0, "MAX": 1.0},

        // Strobe Effect
        {"LABEL": "Strobe Enabled", "NAME": "strobeEnabled", "TYPE": "bool", "DEFAULT": false},
        {"LABEL": "Strobe Speed", "NAME": "strobeSpeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 10.0},
        {"LABEL": "Strobe Delay", "NAME": "strobeDelay", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0}
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4096 // Default point count for the shape
    }
}*/
#define PI 3.1415926535897932384626433832795

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    // Automatic scaling
    float autoScaleValue = 1.0;
    if (autoScale) {
        autoScaleValue = 1.0 + scaleAmount * sin(TIME * scaleSpeed * 2.0 * PI); // Smooth scaling animation
    }

    // Combine automatic and manual scaling
    float scale = autoScaleValue * manualScale;

    // Automatic rotation
    float rotation = 0.0;
    if (autoRotate) {
        rotation = mod(TIME * rotateSpeed * 360.0, 360.0); // Linear rotation from 0 to 360 degrees
        rotation = radians(rotation); // Convert degrees to radians
    }

    // Automatic movement along Y
    float moveY = 0.0;
    if (autoMoveY) {
        moveY = moveYAmount * sin(TIME * moveYSpeed * 2.0 * PI + phaseOffsetY * 2.0 * PI); // Add phase offset
    }

    // Automatic movement along X
    float moveX = 0.0;
    if (autoMoveX) {
        moveX = moveXAmount * sin(TIME * moveXSpeed * 2.0 * PI + phaseOffsetX * 2.0 * PI); // Add phase offset
    }

    // Add manual movement to X and Y
    moveX += manualMoveX;
    moveY += manualMoveY;

    // Strobe effect
    float strobe = 1.0;
    if (strobeEnabled) {
        float strobePhase = mod(TIME * strobeSpeed, 1.0 + strobeDelay); // Add delay to strobe
        strobe = strobePhase < 1.0 ? 1.0 : 0.0; // Blink effect with delay
    }

    // Shape selection
    if (shapeType == 0) { // Square
        // Define the number of points for each side of the square
        int pointsPerSide = pointCount / 4; // Equal number of points for each side
        int sideIndex = pointNumber / pointsPerSide; // Determine which side we're on (0 to 3)
        float t = float(pointNumber % pointsPerSide) / float(pointsPerSide - 1); // Normalized position along the side

        vec2 originalPos;
        if (sideIndex == 0) {
            // Top side (from -0.5 to 0.5 on X, Y = 0.5)
            originalPos = vec2(mix(-0.5, 0.5, t), 0.5);
        } else if (sideIndex == 1) {
            // Right side (X = 0.5, from 0.5 to -0.5 on Y)
            originalPos = vec2(0.5, mix(0.5, -0.5, t));
        } else if (sideIndex == 2) {
            // Bottom side (from 0.5 to -0.5 on X, Y = -0.5)
            originalPos = vec2(mix(0.5, -0.5, t), -0.5);
        } else {
            // Left side (X = -0.5, from -0.5 to 0.5 on Y)
            originalPos = vec2(-0.5, mix(-0.5, 0.5, t));
        }

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to square color
        }

        // Set shape number for each side
        shapeNumber = sideIndex;
    } else if (shapeType == 1) { // Circle
        // Normalized position of the point along the circle (from 0 to 1)
        float t = float(pointNumber) / float(pointCount - 1);

        // Calculate position on the circle
        float angle = t * 2.0 * PI; // Angle in radians
        vec2 originalPos = vec2(cos(angle), sin(angle)) * 0.5; // Circle with radius 0.5

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to circle color
        }

        // Set shape number for the circle
        shapeNumber = 0;
    } else if (shapeType == 2) { // Triangle
        // Define the number of points for each side of the triangle
        int pointsPerSide = pointCount / 3; // Equal number of points for each side
        int sideIndex = pointNumber / pointsPerSide; // Determine which side we're on (0 to 2)
        float t = float(pointNumber % pointsPerSide) / float(pointsPerSide - 1); // Normalized position along the side

        vec2 originalPos;
        if (sideIndex == 0) {
            // Bottom side (from -0.5 to 0.5 on X, Y = -0.5)
            originalPos = vec2(mix(-0.5, 0.5, t), -0.5);
        } else if (sideIndex == 1) {
            // Right side (from 0.5 to 0.0 on X, Y = 0.5)
            originalPos = vec2(mix(0.5, 0.0, t), mix(-0.5, 0.5, t));
        } else {
            // Left side (from -0.5 to 0.0 on X, Y = 0.5)
            originalPos = vec2(mix(-0.5, 0.0, t), mix(-0.5, 0.5, t));
        }

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to triangle color
        }

        // Set shape number for each side
        shapeNumber = sideIndex;
    } else if (shapeType == 3) { // Star
        // Define the number of points for each star arm
        int pointsPerArm = pointCount / 5; // Equal number of points for each arm
        int armIndex = pointNumber / pointsPerArm; // Determine which arm we're on (0 to 4)
        float t = float(pointNumber % pointsPerArm) / float(pointsPerArm - 1); // Normalized position along the arm

        // Calculate position on the star
        float angle = armIndex * (2.0 * PI / 5.0); // Angle for each arm
        vec2 outerPoint = vec2(cos(angle), sin(angle)) * 0.5; // Outer point of the star
        vec2 innerPoint = vec2(cos(angle + PI / 5.0), sin(angle + PI / 5.0)) * 0.25; // Inner point of the star

        // Alternate between outer and inner points
        vec2 originalPos = mix(outerPoint, innerPoint, t); // Interpolate between outer and inner points based on t

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to star color
        }

        // Set shape number for each arm
        shapeNumber = armIndex;
    } else if (shapeType == 4) { // Cross
        // Define the number of points for each part of the cross
        int pointsPerPart = pointCount / 4; // Equal number of points for each part
        int partIndex = pointNumber / pointsPerPart; // Determine which part we're on (0 to 3)
        float t = float(pointNumber % pointsPerPart) / float(pointsPerPart - 1); // Normalized position along the part

        vec2 originalPos;
        if (partIndex == 0) {
            // Vertical part (top to bottom)
            originalPos = vec2(0.0, mix(0.5, -0.5, t));
        } else if (partIndex == 1) {
            // Horizontal part (left to right)
            originalPos = vec2(mix(-0.5, 0.5, t), 0.0);
        } else if (partIndex == 2) {
            // Vertical part (bottom to top)
            originalPos = vec2(0.0, mix(-0.5, 0.5, t));
        } else {
            // Horizontal part (right to left)
            originalPos = vec2(mix(0.5, -0.5, t), 0.0);
        }

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to cross color
        }

        // Set shape number for each part
        shapeNumber = partIndex;
    } else if (shapeType == 5) { // Line
        // Normalized position of the point along the line (from 0 to 1)
        float t = float(pointNumber) / float(pointCount - 1);

        // Calculate position on the line
        vec2 originalPos = vec2(mix(-0.5, 0.5, t), 0.0); // Horizontal line

        // Apply rotation and scaling
        pos = vec2(
            originalPos.x * cos(rotation) - originalPos.y * sin(rotation),
            originalPos.x * sin(rotation) + originalPos.y * cos(rotation)
        ) * scale + vec2(moveX, moveY);

        // Gradient calculation
        if (useGradient) {
            float d = abs(t - 0.5) * 2.0; // Distance from the center (0.0 - 1.0)
            float gradIndex = d * float(gradientIterations); // Gradient repetition

            // Add animation using TIME and gradientSpeed
            float timeOffset = TIME * gradientSpeed; // Control animation speed
            float fractionalIndex = fract(gradIndex + timeOffset); // Fractional part for smooth transition

            color = mix(gradientStartColor, gradientEndColor, fractionalIndex) * strobe; // Apply strobe effect
        } else {
            color = shapeColor * strobe; // Apply strobe effect to line color
        }

        // Set shape number for the line
        shapeNumber = 0;
    }

    // Pass user data (if needed)
    userData = vec4(0.0); // Default user data (can be customized)
}