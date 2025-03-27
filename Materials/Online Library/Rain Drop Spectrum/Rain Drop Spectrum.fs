/*{
    "DESCRIPTION": "Height Map Ray Marching with Audio Spectrum, Geometric Noise, Grain, Noise Speed, and Ambient Occlusion",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Created by AI",
    "INPUTS": [
        { "NAME": "waveformFFT", "TYPE": "audioFFT", "SIZE": 256, "ATTACK": 0.1, "DECAY": 0.05, "RELEASE": 0.1 },

        { "LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "DEFAULT": 3.7, "MIN": 0.1, "MAX": 5.0 },
        { "LABEL": "Global/Height", "NAME": "mat_height", "TYPE": "float", "DEFAULT": 2.0, "MIN": 0.1, "MAX": 4.0 },
        { "LABEL": "Global/Radius", "NAME": "mat_spectrum_radius", "TYPE": "float", "DEFAULT": 2.6, "MIN": 0.1, "MAX": 5.0 },
        { "LABEL": "Global/Smoothness", "NAME": "mat_smoothness", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.1, "MAX": 1.0, "FLAGS": "hidden" },
        { "LABEL": "Global/Shininess", "NAME": "mat_shininess", "TYPE": "float", "DEFAULT": 32.0, "MIN": 1.0, "MAX": 128.0 },
        { "LABEL": "Global/AO Strength", "NAME": "mat_ao_strength", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 },

        { "LABEL": "Noise/Scale", "NAME": "mat_noise_scale", "TYPE": "float", "DEFAULT": 20.0, "MIN": 0.1, "MAX": 20.0 },
        { "LABEL": "Noise/Strength", "NAME": "mat_noise_strength", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "Noise/Speed", "NAME": "mat_noise_speed", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 5.0 },

        { "LABEL": "Camera/Height", "NAME": "mat_camera_height", "TYPE": "float", "DEFAULT": 1.4, "MIN": 0.1, "MAX": 10.0 },
        { "LABEL": "Camera/Focal", "NAME": "mat_focal_length", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.1, "MAX": 5.0 },

        { "LABEL": "Color/Base Color", "NAME": "mat_base_color", "TYPE": "color", "DEFAULT": [0.37, 0.35, 0.33, 1.0] },
        { "LABEL": "Color/Specular Color", "NAME": "mat_specular_color", "TYPE": "color", "DEFAULT": [1.0, 1.0, 1.0, 1.0] },
        { "LABEL": "Color/Background Color", "NAME": "mat_background_color", "TYPE": "color", "DEFAULT": [0.0, 0.0, 0.0, 1.0] },
    ],
    "GENERATORS": [
        { "NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed", "speed_curve": 2} }
    ]
}*/

#define MAX_STEPS 100
#define MAX_DIST 100.0
#define SURF_DIST 0.01

vec3 hash3(vec3 p) {
    p = vec3(dot(p, vec3(127.1, 311.7, 74.7)),
             dot(p, vec3(269.5, 183.3, 246.1)),
             dot(p, vec3(113.5, 271.9, 124.6)));
    return -1.0 + 2.0 * fract(sin(p) * 43758.5453123);
}

float noise(vec3 p) {
    vec3 i = floor(p);
    vec3 f = fract(p);
    vec3 u = f * f * (3.0 - 2.0 * f);
    return mix(mix(mix(dot(hash3(i + vec3(0.0, 0.0, 0.0)), f - vec3(0.0, 0.0, 0.0)),
                       dot(hash3(i + vec3(1.0, 0.0, 0.0)), f - vec3(1.0, 0.0, 0.0)), u.x),
                   mix(dot(hash3(i + vec3(0.0, 1.0, 0.0)), f - vec3(0.0, 1.0, 0.0)),
                       dot(hash3(i + vec3(1.0, 1.0, 0.0)), f - vec3(1.0, 1.0, 0.0)), u.x), u.y),
               mix(mix(dot(hash3(i + vec3(0.0, 0.0, 1.0)), f - vec3(0.0, 0.0, 1.0)),
                       dot(hash3(i + vec3(1.0, 0.0, 1.0)), f - vec3(1.0, 0.0, 1.0)), u.x),
                   mix(dot(hash3(i + vec3(0.0, 1.0, 1.0)), f - vec3(0.0, 1.0, 1.0)),
                       dot(hash3(i + vec3(1.0, 1.0, 1.0)), f - vec3(1.0, 1.0, 1.0)), u.x), u.y), u.z);
}

float getHeight(vec2 p) {
    float freq = length(p) * 0.1;
    vec4 spectrum = texture(waveformFFT, vec2(freq, 0.0));
    return spectrum.r * mat_height;
}

float getDist(vec3 p) {
    vec2 uv = p.xz * mat_scale;
    float height = getHeight(uv);
    float noiseValue = noise(p * mat_noise_scale * 10 + vec3(0, mat_noise_time, 0)) * mat_noise_strength;
    return p.y - height - noiseValue;
}

vec3 getNormal(vec3 p) {
    float d = getDist(p);
    vec2 e = vec2(0.01, 0);
    vec3 n = d - vec3(
        getDist(p - e.xyy),
        getDist(p - e.yxy),
        getDist(p - e.yyx)
    );
    return normalize(n);
}

float rayMarch(vec3 ro, vec3 rd) {
    float dO = 0.0;
    for(int i = 0; i < MAX_STEPS; i++) {
        vec3 p = ro + rd * dO;
        float dS = getDist(p) * mat_smoothness;
        dO += dS;
        if(dO > MAX_DIST || abs(dS) < SURF_DIST) break;
    }
    return dO;
}

float random(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

float calcAO(vec3 p, vec3 n) {
    float occ = 0.0;
    float sca = 1.0;
	 const int mat_ao_steps = 5;
    for(int i = 0; i < int(mat_ao_steps); i++) {
        float h = 0.01 + 0.12 * float(i) / mat_ao_steps;
        float d = getDist(p + h * n);
        occ += (h - d) * sca;
        sca *= 0.95;
    }
    return clamp(1.0 - 3.0 * occ * mat_ao_strength, 0.0, 1.0);
}

vec4 materialColorForPixel(vec2 texCoord) {
    vec2 uv = texCoord;
    uv.y = 1.0 - uv.y;
    uv = (uv - 0.5) * 2.0;

    vec3 ro = vec3(0, mat_camera_height, -3);
    vec3 lookAt = vec3(0, 0, 0);
    vec3 forward = normalize(lookAt - ro);
    vec3 right = normalize(cross(vec3(0, 1, 0), forward));
    vec3 up = cross(forward, right);
    vec3 rd = normalize(uv.x * right + uv.y * up + mat_focal_length * forward);

    float d = rayMarch(ro, rd);
    vec3 p = ro + rd * d;
    vec3 n = getNormal(p);
    vec3 light = normalize(vec3(1, 2, -2));

    float distFromCenter = length(p.xz);
    float spectrumMask = smoothstep(mat_spectrum_radius + 0.1, mat_spectrum_radius - 0.1, distFromCenter);

    float dif = dot(n, light) * 0.5 + 0.5;
    vec3 diffuse = dif * mat_base_color.rgb;

    vec3 viewDir = normalize(ro - p);
    vec3 reflectDir = reflect(-light, n);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), mat_shininess);
    vec3 specular = mat_specular_color.rgb * spec;

    float ao = calcAO(p, n);
    vec3 col = (diffuse + specular) * spectrumMask * ao;

    col += noise(p * mat_noise_scale * 20.0 + vec3(0, mat_noise_time, 0)) * mat_noise_strength * 0.5;

    col = mix(col, mat_background_color.rgb, 1.0 - exp(-0.0005 * d * d));

    return vec4(col, 1.0);
}