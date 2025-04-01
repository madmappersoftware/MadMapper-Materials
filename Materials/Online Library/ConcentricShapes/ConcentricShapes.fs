/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Henry Florence",
    "DESCRIPTION": "Creates moving concentric bands of alternating colours. Mainly intended for pixel mapping dmx fixtures. Largely created with chatGPT",
    "TAGS": "pixelmap,color,gradient",
    "VSN": "0.95",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.3 },
		{"LABEL": "Shape", "NAME": "shape_type", "TYPE": "int", "MIN": 0, "MAX": 5, "DEFAULT": 3 },
		{"LABEL": "Centre X", "NAME": "mat_centerX", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{"LABEL": "Centre Y", "NAME": "mat_centerY", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{"LABEL": "Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 360.0, "DEFAULT": 0.0 },
		{"LABEL": "Rand Centre", "NAME": "mat_use_random_center", "TYPE": "bool", "DEFAULT": false },
		{"LABEL": "Ring Width", "NAME": "mat_ringWidth", "TYPE": "float", "MIN": 0.01, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Gradient Width", "NAME": "mat_gradient_width", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Colour Count", "NAME": "mat_color_count", "TYPE": "int", "MIN": 2, "MAX": 4, "DEFAULT": 2 },
		{"LABEL": "Colour 1", "NAME": "mat_colour1", "TYPE": "color", "DEFAULT": [1.0,0.0,0.0,1.0] },
		{"LABEL": "Colour 2", "NAME": "mat_colour2", "TYPE": "color", "DEFAULT": [0.0,0.0,1.0,1.0] },
		{"LABEL": "Colour 3", "NAME": "mat_colour3", "TYPE": "color", "DEFAULT": [0.0,1.0,0.0,1.0] },
		{"LABEL": "Colour 4", "NAME": "mat_colour4", "TYPE": "color", "DEFAULT": [1.0,0.55,0.0,1.0] },
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

vec4 pickColour(float ringIndex) {
// Assign colors based on the ring index
    vec4 selectedColor;
    if (ringIndex < 1.0) {
        selectedColor = mat_colour1;
    } else if (ringIndex < 2.0) {
        selectedColor = mat_colour2;
    } else if (ringIndex < 3.0 && mat_color_count > 2) {
        selectedColor = mat_colour3;
    } else {
        selectedColor = mat_colour4;
    }
	return selectedColor;
}
vec4 materialColorForPixel( vec2 uv )
{
    // Define the center of the shape
    vec2 center;
    if (mat_use_random_center) {
        // Generate random center positions for the shapes
        float seed = floor(mat_time * 0.5); // Use time as a seed for randomness
        center = vec2(
            fract(sin(seed) * 43758.5453123) * 2.0 - 1.0, // Random X in range [-1, 1]
            fract(sin(seed * 1.324) * 43758.5453123) * 2.0 - 1.0 // Random Y in range [-1, 1]
        );
    } else {
        // Use fixed center
        center = vec2(mat_centerX, mat_centerY);
    }

    // Offset the UV coordinates by the shape center
    vec2 offsetUV = uv - center;

	// Apply rotation
	float rads = mat_rotation / 360.0 * 2 * 3.14159;
    float cosAngle = cos(rads);
    float sinAngle = sin(rads);
    vec2 rotatedUV = vec2(
        offsetUV.x * cosAngle - offsetUV.y * sinAngle,
        offsetUV.x * sinAngle + offsetUV.y * cosAngle
    );

	offsetUV = rotatedUV;

    // Calculate the radius growth factor
    float growth = mat_time * 0.5;

    // Distance from the center
    float dist;

    // Shape logic
    if (shape_type == 0) {
        // Circle
        dist = length(offsetUV);
    } else if (shape_type == 1) {
        // Diamond
        dist = abs(offsetUV.x) + abs(offsetUV.y);
    } else if (shape_type == 2) {
        // Square
        dist = max(abs(offsetUV.x), abs(offsetUV.y));
    } else if (shape_type == 3) {
        // Random pointed star
        float angle = atan(offsetUV.y, offsetUV.x);
        float starFactor = sin(angle * 5.0 + mat_time * 2.0) * 0.2 + 1.0;
        dist = length(offsetUV) * starFactor;
    } else if (shape_type == 4) {
        // Straight line
        dist = abs(offsetUV.x);
    } else if (shape_type == 5) {
        // Chevron
        float chevronWidth = 0.3;
        float chevronAngle = abs(offsetUV.x) + abs(offsetUV.y / chevronWidth);
        dist = chevronAngle;
    } else {
        // Default to circle
        dist = length(offsetUV);
    }

    // Determine which ring the fragment belongs to
    float ringWidth = mat_ringWidth;
    float ringIndex = mod(floor((dist - growth) / ringWidth), float(mat_color_count));
	float gradient_width = mat_gradient_width * ringWidth;

    // Calculate gradient factor for both edges of the shape with different patterns
  	float ringPosition = mod(dist - growth, ringWidth);
    float gradientFactor = smoothstep(0.0, gradient_width, ringPosition) * (1.0 - smoothstep(ringWidth - gradient_width, ringWidth, ringPosition));

			
    // Alternate colors for shapes with gradient blending
    vec4 innerColor = pickColour(mod(ringIndex, float(mat_color_count)));
    vec4 outerColor = pickColour(mod(ringIndex + 1.0, float(mat_color_count)));

	vec4 color = mix(innerColor, outerColor, gradientFactor);


    // Set the final color
    return color;
}
