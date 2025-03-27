#ifndef M_PI
  #define M_PI 3.14159265358979323846
#endif

/*{
  "CREDIT": "Maxime Wagner",
  "DESCRIPTION": "Ligne avec défilement horizontal (dash), répétition verticale (colonnes de lignes) avec espace réglable entre colonnes et rotation. L'espacement est géré comme un offset continu.",
  "VSN": "1.6",
  "INPUTS": [
    { "LABEL": "Cells/Cells X", "NAME": "line_repeat2", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 32 },
    { "LABEL": "Cells/Cells Y", "NAME": "line_repeat", "TYPE": "int", "DEFAULT": 1, "MIN": 1, "MAX": 32 },
    { "LABEL": "Global/Line Width", "NAME": "line_thickness", "TYPE": "float", "DEFAULT": 0.06, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Line Length","NAME": "dash_length", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Global/Rotation", "NAME": "line_rotation", "TYPE": "float", "DEFAULT": 90.0, "MIN": -360.0, "MAX": 360.0 },
    { "LABEL": "Global/Center Y", "NAME": "line_y", "TYPE": "float", "DEFAULT": 0.504, "MIN": 0.0, "MAX": 1.0 },
    { "LABEL": "Master/speed", "NAME": "scroll_speed", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 3.0 },
    { "LABEL": "Master/BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Effect/decay", "NAME": "col_spacing", "TYPE": "float", "DEFAULT": 0.0, "MIN": -1.0, "MAX": 1.0 },
    { "LABEL": "Effect/Decay style", "NAME": "mat_shape", "TYPE": "long", "VALUES": ["Default", "Symmetrical", "Circle"], "DEFAULT": "Default", "FLAGS": "generate_as_define" },
    { "LABEL": "Master/smooth", "NAME": "smoothness", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 2.0 },
    { "LABEL": "Effect/Symmetry", "NAME": "symmetry", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Effect/Polar Control", "NAME": "polar_control", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Effect/Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
    { "LABEL": "Colors/Front Color", "NAME": "line_color", "TYPE": "color", "DEFAULT": [1.0,1.0,1.0,1.0] },
    { "LABEL": "Colors/Back Color", "NAME": "bg_color", "TYPE": "color", "DEFAULT": [0.0,0.0,0.0,1.0] }
  ],
  "RASTERISATION_SETTINGS": {
    "DEFAULT_RENDER_TO_TEXTURE": true,
    "DEFAULT_WIDTH": 512,
    "DEFAULT_HEIGHT": 512
  },
  "ILLUSTRATION": "/mnt/data/madmad.jpg"
}*/

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = texCoord - 0.5;
    
    // Appliquer la rotation globale correctement
    float angle = line_rotation * M_PI / 180.0;
    mat2 rot = mat2(cos(angle), -sin(angle),
                    sin(angle),  cos(angle));
    uv = rot * uv + 0.5;
    
    // Appliquer l'inversion si activée
    if (reverse) {
        uv.x = 1.0 - uv.x;
    }
    
    // Appliquer la symétrie si activée
    if (symmetry) {
        uv.x = abs(uv.x - 0.5) * 2.0;
    }
    
    // Appliquer la subdivision verticale (Cells X) avec espacement ajustable
    float v;
    float cellIndex = floor(uv.y * float(line_repeat2));
    v = fract(uv.y * float(line_repeat2));

    // Appliquer le décalage entre les colonnes selon le mode sélectionné
    float finalX;
    if (mat_shape == 0) { // Default
        float idealX = (line_repeat2 > 1) ? (cellIndex / (float(line_repeat2) - 1.0)) : 0.5;
        finalX = mix(0.5, idealX, col_spacing);
    } else if (mat_shape == 1) { // Symmetrical
        finalX = ((int(cellIndex) % 2 == 0) ? 1.0 : -1.0) * col_spacing;
    } else if (mat_shape == 2) { // Circle
        finalX = sin((cellIndex / float(line_repeat2)) * M_PI) * col_spacing;
    }
    uv.x += finalX;

    // Appliquer la transformation polaire si activée
    if (polar_control) {
        float r = length(uv - 0.5);
        float theta = atan(uv.y - 0.5, uv.x - 0.5);
        uv.x = r;
        uv.y = theta / M_PI;
    }

    // Appliquer le mouvement automatique en fonction de la vitesse
    float bpm_factor = mat_bpmsync ? 2.0 : 1.0;
    if (scroll_speed > 0.0) {
        uv.x += TIME * scroll_speed * bpm_factor;
    }

    float smooth_factor = smoothness * 0.2;
    float vertical_alpha = 1.0 - smoothstep(line_thickness - smooth_factor, line_thickness + smooth_factor, abs(v - line_y));
    float dash_pattern = fract(uv.x * float(line_repeat));
    float edge = smooth_factor;
    float dash_alpha = 1.0 - smoothstep(dash_length - edge, dash_length, dash_pattern);
    float final_alpha = vertical_alpha * dash_alpha;
    return mix(bg_color, line_color, final_alpha);
}
