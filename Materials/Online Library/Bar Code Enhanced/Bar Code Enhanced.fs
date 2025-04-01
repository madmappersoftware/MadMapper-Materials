/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Simon Geilfus modified Simon Holden",
    "DESCRIPTION": "Bar Coder with Linear, Radial, or Polar Pattern",
    "TAGS": "graphic",
    "VSN": "1.4",
    "INPUTS": [
        { "LABEL": "Global/Rotate", "NAME": "mat_rotate", "TYPE": "float", "MIN": 0, "MAX": 360.0, "DEFAULT": 0.0 },
        { "LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Global/Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
        { "LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Global/Type", "NAME": "mat_pattern_type", "TYPE": "long", "VALUES": ["Linear", "Radial", "Polar"], "DEFAULT": "Linear" },
        { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] },
        { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [0.0, 0.0, 0.0, 1.0] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.0 }
    ],
    "GENERATORS": [
        {
            "NAME": "mat_animation_time",
            "TYPE": "time_base",
            "PARAMS": { "speed": "mat_speed", "reverse": "mat_reverse", "link_speed_to_global_bpm": true }
        }
    ]
}
*/a
vec2 rotate2D(vec2 v, float angle) {
    float s = sin(angle);
    float c = cos(angle);
    return vec2(v.x * c - v.y * s, v.x * s + v.y * c);
}


vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 center = vec2(0.5, 0.5);
    vec2 uv = texCoord - center;

    // Apply pattern based on the selected type
    if (mat_pattern_type == 0) { // Linear pattern
        float angle = radians(mat_rotate);
        uv = vec2(uv.x * cos(angle) - uv.y * sin(angle), uv.x * sin(angle) + uv.y * cos(angle)) + center;
    } else if (mat_pattern_type == 1) { // Radial pattern
        float angle = radians(mat_rotate);
        float radius = length(uv);
        float theta = atan(uv.y, uv.x) + angle;
        float new_radius = radius + mat_animation_time * mat_speed;
        uv = center + vec2(new_radius * cos(theta), new_radius * sin(theta));
    } else { // Polar pattern
        float angle = radians(mat_rotate);
        float radius = length(uv);
        float new_angle = angle + mat_animation_time * mat_speed;
        uv = center + vec2(radius * cos(new_angle), radius * sin(new_angle));
    }

    // Calculate color based on the front color and pattern
    float r = fract(4.789 * sin(mat_size * uv.x + mat_animation_time));
    vec3 color = mix(mat_backgroundColor.rgb, mat_foregroundColor.rgb, r);

    // Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);

    // Apply brightness
    color += vec3(mat_brightness);

    return vec4(color, 1.0);
}
