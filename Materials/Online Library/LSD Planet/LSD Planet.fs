/*{
    "DESCRIPTION": "Psychedelic Planet Raymarching with Background",
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Anonyme",
    "CATEGORIES": [
        "Generator"
    ],
    "INPUTS": [
        {
            "NAME": "mat_speed",
            "LABEL": "Speed",
            "TYPE": "float",
            "DEFAULT": 2.0,
            "MIN": 0.0,
            "MAX": 10.0
        },
        {
            "NAME": "mat_distortion",
            "LABEL": "Distortion",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 2.0
        },
        {
            "NAME": "mat_color_shift",
            "LABEL": "Color Shift",
            "TYPE": "float",
            "DEFAULT": 0.6,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_zoom",
            "LABEL": "Zoom",
            "TYPE": "float",
            "DEFAULT": 0.9,
            "MIN": 0.1,
            "MAX": 5.0
        },
        {
            "NAME": "mat_frequency",
            "LABEL": "Distortion Frequency",
            "TYPE": "float",
            "DEFAULT": 6.0,
            "MIN": 1.0,
            "MAX": 10.0
        },
        {
            "NAME": "mat_glow",
            "LABEL": "Glow Intensity",
            "TYPE": "float",
            "DEFAULT": 0.4,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_orbit_speed",
            "LABEL": "Orbit Speed",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 2.0
        },
        {
            "NAME": "mat_planet_size",
            "LABEL": "Planet Size",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.1,
            "MAX": 2.0
        },
        {
            "NAME": "mat_bg_gradient_angle",
            "LABEL": "Backg Rot",
            "TYPE": "float",
            "DEFAULT": 45.0,
            "MIN": 0.0,
            "MAX": 360.0
        },
        {
            "NAME": "mat_bg_star_density",
            "LABEL": "Backg Density",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": 0.0,
            "MAX": 1.0
        },
        {
            "NAME": "mat_color_1",
            "LABEL": "Color/Color 1",
            "TYPE": "color",
            "DEFAULT": [1.0, 0.5, 0.2, 1.0]
        },
        {
            "NAME": "mat_color_2",
            "LABEL": "Color/Color 2",
            "TYPE": "color",
            "DEFAULT": [0.1, 0.3, 0.7, 1.0]
        },
        {
            "NAME": "mat_bg_color_1",
            "LABEL": "Color/Background Color 1",
            "TYPE": "color",
            "DEFAULT": [0.1, 0.0, 0.2, 1.0]
        },
        {
            "NAME": "mat_bg_color_2",
            "LABEL": "Color/Background Color 2",
            "TYPE": "color",
            "DEFAULT": [0.3, 0.0, 0.5, 1.0]
        },
    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve": 2,
                "link_speed_to_global_bpm": true
            }
        }
    ]
}*/

#define MAX_STEPS 100
#define MAX_DIST 100.
#define SURF_DIST .001

float sdSphere(vec3 p, float r) {
    return length(p) - r;
}

float getNoise(vec3 p) {
    return fract(sin(dot(p, vec3(12.9898, 78.233, 45.5432))) * 43758.5453);
}

float getDistortion(vec3 p) {
    return sin(p.x * mat_frequency + mat_time) * 
           sin(p.y * mat_frequency + mat_time) * 
           sin(p.z * mat_frequency + mat_time) * mat_distortion;
}

float getDist(vec3 p) {
    float sphere = sdSphere(p, mat_planet_size);
    float distortion = getDistortion(p);
    return sphere + distortion;
}

float rayMarch(vec3 ro, vec3 rd) {
    float dO = 0.;
    for(int i = 0; i < MAX_STEPS; i++) {
        vec3 p = ro + rd * dO;
        float dS = getDist(p);
        dO += dS;
        if(dO > MAX_DIST || abs(dS) < SURF_DIST) break;
    }
    return dO;
}

vec3 getNormal(vec3 p) {
    float d = getDist(p);
    vec2 e = vec2(.001, 0);
    vec3 n = d - vec3(
        getDist(p - e.xyy),
        getDist(p - e.yxy),
        getDist(p - e.yyx)
    );
    return normalize(n);
}

vec3 getRayDirection(vec2 uv, vec3 p, vec3 l, float z) {
    vec3 f = normalize(l - p),
        r = normalize(cross(vec3(0, 1, 0), f)),
        u = cross(f, r),
        c = f * z,
        i = c + uv.x * r + uv.y * u;
    return normalize(i);
}

float star(vec2 uv, float flare) {
    float d = length(uv);
    float m = .05 / d;
    float rays = max(0., 1. - abs(uv.x * uv.y * 1000.));
    m += rays * flare;
    return m;
}

vec3 starField(vec2 uv) {
    vec3 col = vec3(0);
    vec2 gv = fract(uv * 100.) - 0.5;
    vec2 id = floor(uv * 100.);
    
    for(int y = -1; y <= 1; y++) {
        for(int x = -1; x <= 1; x++) {
            vec2 offs = vec2(x, y);
            float n = getNoise(vec3(id + offs, mat_time * 0.1));
            float size = fract(n * 345.32);
            float star = star(gv - offs - vec2(n, fract(n * 34.)) + 0.5, smoothstep(0.9, 1., size) * 0.6);
            vec3 color = sin(vec3(0.2, 0.3, 0.9) * fract(n * 2345.2) * 123.2) * 0.5 + 0.5;
            color = color * vec3(1, 0.1, 1. + size);
            star *= sin(mat_time * 3. + n * 6.2831) * 0.5 + 1.;
            col += star * size * color;
        }
    }
    return col * mat_bg_star_density;
}

vec4 materialColorForPixel(vec2 texCoord) {
    vec2 uv = texCoord;
    uv.y = 1.0 - uv.y;
    vec2 originalUV = uv;
    uv = (uv * 2.0 - 1.0) / mat_zoom;
    
    float orbit = mat_time * mat_orbit_speed;
    vec3 ro = vec3(sin(orbit) * 3.0, cos(orbit) * 1.5, cos(orbit) * 3.0);
    vec3 rd = getRayDirection(uv, ro, vec3(0, 0, 0), 1.);

    float d = rayMarch(ro, rd);
    
    vec3 bgGradient = mix(mat_bg_color_1.rgb, mat_bg_color_2.rgb, 
                          dot(originalUV - 0.5, vec2(cos(radians(mat_bg_gradient_angle)), 
                                                     sin(radians(mat_bg_gradient_angle)))) + 0.5);
    vec3 stars = starField(originalUV);
    vec3 backgroundColor = bgGradient + stars;
    
    if(d >= MAX_DIST) {
        return vec4(backgroundColor, 1.0);
    }
    
    vec3 p = ro + rd * d;
    vec3 n = getNormal(p);
    
    vec3 color = 0.5 + 0.5 * cos(mat_time * 0.5 + p.xyx + vec3(0, 2, 4));
    color += mat_color_shift * sin(mat_time * 0.1 + p.y * 10.0);
    
    float fresnel = pow(1.0 + dot(rd, n), 3.0);
    color += vec3(mat_color_1.rgb) * fresnel * 0.5;

    // Add glow
    color += vec3(mat_color_2.rgb) * (1.0 / (d * d + 0.1)) * mat_glow;

    // Enhance contrast
    color = pow(color, vec3(1.2));

    // Color mixing
    color = mix(color, vec3(mat_color_1.rgb), color.r * 0.5);
    color = mix(color, vec3(mat_color_2.rgb), color.b * 0.5);

    // Blend with background
    color = mix(backgroundColor, color, smoothstep(MAX_DIST - 5.0, MAX_DIST - 10.0, d));

    return vec4(color, 1.0);
}