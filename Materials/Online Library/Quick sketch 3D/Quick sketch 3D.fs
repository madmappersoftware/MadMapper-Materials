/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Anaglyph 3D Effect",
    "DESCRIPTION": "Stereoscopic red/cyan 3D visualization with rotating geometry",
    "TAGS": ["3D", "anaglyph", "generative"],
    "VSN": "1.0",
    "INPUTS": [
        {
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 0.4
        },
        {
            "LABEL": "Range",
            "NAME": "range",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Divergence",
            "NAME": "divergence",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 0.5,
            "DEFAULT": 0.1
        },
        {
            "LABEL": "Rotation",
            "NAME": "iMouse",
            "TYPE": "point2D"
        },
        {
            "LABEL": "Complexity",
            "NAME": "iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 10,
            "DEFAULT": 5
        },
        {
            "LABEL": "Size",
            "NAME": "radius",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 1.0,
            "DEFAULT": 0.3
        }
    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "mat_speed"}
        }
    ]
}*/

// ... rest of the shader code remains the same ...

const float fieldOfView = 1.5;
const float blend = 1.5;
const float balance = 1.5;
const float falloff = 1.9;

float random(vec2 p) { return fract(1e4 * sin(17.0 * p.x + p.y * 0.1) * (0.1 + abs(sin(p.y * 13.0 + p.x)))); }
mat2 rot(float a) { float c=cos(a),s=sin(a); return mat2(c,-s,s,c); }
float smoothmin (float a, float b, float r) { float h = clamp(.5+.5*(b-a)/r, 0., 1.); return mix(b, a, h)-r*h*(1.-h); }

vec3 look (vec3 eye, vec3 target, vec2 anchor, float fov) {
    vec3 forward = normalize(target-eye);
    vec3 right = normalize(cross(forward, vec3(0,1,0)));
    vec3 up = normalize(cross(right, forward));
    return normalize(forward * fov + right * anchor.x + up * anchor.y);
}

vec3 camera (vec3 eye) {
    vec2 mouse = iMouse/RENDERSIZE.xy*2.-1.;
    if (length(iMouse) > 0.0) {
        eye.yz *= rot(mouse.y*3.1415);
        eye.xz *= rot(mouse.x*3.1415);
    } else {
        eye.yz *= rot(-3.1415/4.);
        eye.xz *= rot(-3.1415/2.);
    }
    return eye;
}

float geometry (vec3 pos) {
    pos = camera(pos);
    float a = 1.0;
    float scene = 1.;
    float t = mat_time*0.2;
    float wave = 1.0+0.2*sin(t*8.-length(pos)*2.);
    t = floor(t)+pow(fract(t),.5);
    for (int i = iterations; i > 0; --i) {
        pos.xy *= rot(cos(t)*balance/a+a*2.+t);
        pos.zy *= rot(sin(t)*balance/a+a*2.+t);
        pos.zx *= rot(sin(t)*balance/a+a*2.+t);
        pos = abs(pos)-range*a*wave;
        scene = smoothmin(scene, length(pos)-radius*a, blend*a);
        a /= falloff;
    }
    return scene;
}

float raymarch ( vec3 eye, vec3 ray ) {
    float dither = random(ray.xy+fract(mat_time));
    float total = dither;
    const int count = 30;
    for (int index = count; index > 0; --index) {
        float dist = geometry(eye+ray*total);
        dist *= 0.9+.1*dither;
        total += dist;
        if (dist < 0.001 * total)
            return float(index)/float(count);
    }
    return 0.;
}

vec4 materialColorForPixel(vec2 texCoord) {
    vec2 uv = 2.0*(texCoord*RENDERSIZE.xy-0.5*RENDERSIZE.xy)/RENDERSIZE.y;
    
    vec3 eyeLeft = vec3(-divergence,0,5.);
    vec3 eyeRight = vec3(divergence,0,5.);
    vec3 rayLeft = look(eyeLeft, vec3(0), uv, fieldOfView);
    vec3 rayRight = look(eyeRight, vec3(0), uv, fieldOfView);
    
    float red = raymarch(eyeLeft, rayLeft);
    float cyan = raymarch(eyeRight, rayRight);
    
    return vec4(red, cyan, cyan, 1.0);
}