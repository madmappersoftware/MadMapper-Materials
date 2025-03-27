/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture | remix by Lomax |",
    "DESCRIPTION": "Simple vector template",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1 },
        {"LABEL": "Speed_Sperm", "NAME": "mat_speed_sperm", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0 },

        { "LABEL": "Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 30, "DEFAULT": 8 },
        { "LABEL": "Azimut", "NAME": "mat_azimut", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.6 },
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.6 },
        {"LABEL": "Sperm", "NAME": "mat_sperm", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
        {"LABEL": "Sperm Scale", "NAME": "mat_sperm_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0 },

        { "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, 1.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 0.0, 0.0 ] },
        { "LABEL": "Noise Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.5 },
        { "LABEL": "Noise Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 2.5 },
        { "LABEL": "Simple In", "NAME": "mat_simplein", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 },
        { "LABEL": "Simple Out", "NAME": "mat_simpleout", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 },

        { "LABEL": "Color/Mode", "NAME": "mat_color_mode", "TYPE": "long", "DEFAULT": "Mix ABCD", "VALUES": ["Mix ABCD","Auto"] },
        { "LABEL": "Color/Color A", "NAME": "mat_color_a", "TYPE": "color", "DEFAULT": [1,1,1,1], "FLAGS": "no_alpha" },
        { "LABEL": "Color/Color B", "NAME": "mat_color_b", "TYPE": "color", "DEFAULT": [1,0,0,1], "FLAGS": "no_alpha" },
        { "LABEL": "Color/Color C", "NAME": "mat_color_c", "TYPE": "color", "DEFAULT": [0,1,0,1], "FLAGS": "no_alpha" },
        { "LABEL": "Color/Color D", "NAME": "mat_color_d", "TYPE": "color", "DEFAULT": [0,0,1,1], "FLAGS": "no_alpha" },

        { "LABEL": "Color/Flip gradient", "NAME": "mat_flipgradient", "TYPE": "bool",  "DEFAULT": 0 ,"FLAGS": "button"},
        { "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
        { "LABEL": "Vertical Separation", "NAME": "mat_vertical_separation", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.05 },
        { "LABEL": "Rotation X", "NAME": "mat_rotation_x", "TYPE": "float", "MIN": -3.14, "MAX": 3.14, "DEFAULT": 0.0 },
        { "LABEL": "Rotation Y", "NAME": "mat_rotation_y", "TYPE": "float", "MIN": -3.14, "MAX": 3.14, "DEFAULT": 0.0 },
        { "LABEL": "Rotation Z", "NAME": "mat_rotation_z", "TYPE": "float", "MIN": -3.14, "MAX": 3.14, "DEFAULT": 0.0 }
    ],
    "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
        {"NAME": "mat_sperm_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_sperm", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4096
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec3 mat_hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

mat4 CreatePerspectiveMatrix(in float fov, in float aspect, in float near, in float far)
{
    mat4 m = mat4(0.0);
    float angle = (fov / 180.0) * PI;
    float f = 1.0 / tan(angle * 0.5);
    m[0][0] = f / aspect;
    m[1][1] = f;
    m[2][2] = (far + near) / (near - far);
    m[2][3] = -1.0;
    m[3][2] = (2.0 * far * near) / (near - far);
    return m;
}

mat4 RotateX(float angle)
{
    float c = cos(angle);
    float s = sin(angle);
    return mat4(
        vec4(1.0, 0.0, 0.0, 0.0),
        vec4(0.0, c, -s, 0.0),
        vec4(0.0, s, c, 0.0),
        vec4(0.0, 0.0, 0.0, 1.0)
    );
}

mat4 RotateY(float angle)
{
    float c = cos(angle);
    float s = sin(angle);
    return mat4(
        vec4(c, 0.0, s, 0.0),
        vec4(0.0, 1.0, 0.0, 0.0),
        vec4(-s, 0.0, c, 0.0),
        vec4(0.0, 0.0, 0.0, 1.0)
    );
}

mat4 RotateZ(float angle)
{
    float c = cos(angle);
    float s = sin(angle);
    return mat4(
        vec4(c, -s, 0.0, 0.0),
        vec4(s, c, 0.0, 0.0),
        vec4(0.0, 0.0, 1.0, 0.0),
        vec4(0.0, 0.0, 0.0, 1.0)
    );
}

mat4 CamControl(vec3 eye, float azimut)
{
    float pitch = (azimut - -5.0) / (5.0 - -5.0) * (0.7 - -0.25) + -0.25;
    float cosPitch = cos(pitch);
    float sinPitch = sin(pitch);
    vec3 xaxis = vec3(1, 0, 0.);
    vec3 yaxis = vec3(0., cosPitch, sinPitch);
    vec3 zaxis = vec3(0., -sinPitch, cosPitch);

    mat4 viewMatrix = mat4(
        vec4(xaxis.x, yaxis.x, zaxis.x, 0),
        vec4(xaxis.y, yaxis.y, zaxis.y, 0),
        vec4(xaxis.z, yaxis.z, zaxis.z, 0),
        vec4(-dot(xaxis, eye), -dot(yaxis, eye), -dot(zaxis, eye), 1)
    );
    return viewMatrix;
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float normalizedPos = float(pointNumber) / pointCount;
    shapeNumber = int(normalizedPos * mat_amount);
    float normalizedPosInShape = -1.0 + 2.0 * fract(normalizedPos * mat_amount);

    float px = normalizedPosInShape * 2.0 + mat_offset.x;
    float py = normalizedPosInShape * 2.0 + mat_offset.y;

    vec3 eye = vec3(0.0, -0.25 + mat_azimut, -1.0);
    mat4 projmat = CreatePerspectiveMatrix(50.0, 1.7, 0.1, 10.0);
    mat4 viewmat = CamControl(eye, mat_azimut);
    mat4 vpmat = viewmat * projmat;

    // Apply rotation transformations
    mat4 rotationMatrix = RotateX(mat_rotation_x) * RotateY(mat_rotation_y) * RotateZ(mat_rotation_z);
    vpmat = rotationMatrix * vpmat;

    vec3 col = vec3(0.0);
    vec3 acc = vec3(0.0);
    float d;

    float lh = -1024.0;
    float off = 0.0;
    float h = 0.0;
    float z = 0.1;
    float zi = 0.05;

    float simpleout = clamp(2.0 - (px - mat_offset.x) - mat_simpleout * 2.0, 0.0, 1.0);
    float simplein = clamp(-2.0 - (px - mat_offset.x) + mat_simplein * 2.0, -1.0, 0.0);
    float simple = simplein * simpleout;

    z += zi * shapeNumber;
    vec4 P = vec4(
        px,
        py + mat_vertical_separation * shapeNumber,
        simple * mat_noisepower * fBm(vec3(0.5 * vec2(eye.x + px, z + off) * mat_noisescale, mat_animation_time)),
        eye.z + z
    );
    P = vpmat * P;
    pos = P.xy * mat_scale / 2.0 + vec2(0.0, 0.5) + mat_offset;

    float S = fract(P.x * 10.0 * mat_sperm_scale + shapeNumber * 0.33 + mat_sperm_time);
    S = mix(pow(S, 2.2), 1.0, 1.0 - mat_sperm);

    if (mat_color_mode == 1) {
        col = mat_hsv2rgb(vec3(normalizedPos, 1.0, 0.5));
    } else {
        float mixValue = fract(normalizedPos * 4.0);
        vec3 colorA = mat_color_a.rgb;
        vec3 colorB = mat_color_b.rgb;
        vec3 colorC = mat_color_c.rgb;
        vec3 colorD = mat_color_d.rgb;

        if (mat_flipgradient) {
            colorA = mat_color_d.rgb;
            colorB = mat_color_c.rgb;
            colorC = mat_color_b.rgb;
            colorD = mat_color_a.rgb;
        }

        if (mixValue <= 0.25) {
            col = mix(colorA, colorB, mixValue * 4.0);
        } else if (mixValue <= 0.5) {
            col = mix(colorB, colorC, (mixValue - 0.25) * 4.0);
        } else if (mixValue <= 0.75) {
            col = mix(colorC, colorD, (mixValue - 0.5) * 4.0);
        } else {
            col = mix(colorD, colorA, (mixValue - 0.75) * 4.0);
        }
    }

    col *= S;

    col *= mat_tint.rgb;

    color = vec4(col, 1.0);
}
