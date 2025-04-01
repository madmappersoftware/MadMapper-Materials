/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "shadertoy drift\nport by mad-matt",
    "DESCRIPTION": "2D Clouds - https://www.shadertoy.com/view/4tdSWr",
    "TAGS": "Noise,graphic",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
        { "LABEL": "Reverse", "NAME": "mat_reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4.0 },
        { "LABEL": "Direction", "NAME": "mat_direction", "TYPE": "point2D", "DEFAULT": [1.0,1.0], "MIN": [-1.0,-1.0], "MAX": [1.0,1.0] },
        { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        { "NAME": "mat_animation_time",
          "TYPE": "time_base",
          "PARAMS": {"speed": "mat_speed", "reverse": "mat_reverse", "link_speed_to_global_bpm":true, "speed_curve": 3}
        }
    ]
}*/

//#include "MadCommon.glsl"
//#include "MadNoise.glsl"

const float clouddark = 0.5;
const float cloudlight = 0.3;
const float cloudcover = 0.2;
const float cloudalpha = 8.0;
const float skytint = 0.5;
const vec3 skycolour1 = vec3(0.2, 0.4, 0.6);
const vec3 skycolour2 = vec3(0.4, 0.7, 1.0);

const mat2 m = mat2( 1.6,  1.2, -1.2,  1.6 );

vec2 mat_hash( vec2 p ) {
    p = vec2(dot(p,vec2(127.1,311.7)), dot(p,vec2(269.5,183.3)));
    return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float mat_noise( in vec2 p ) {
    const float K1 = 0.366025404; // (sqrt(3)-1)/2;
    const float K2 = 0.211324865; // (3-sqrt(3))/6;
    vec2 i = floor(p + (p.x+p.y)*K1);   
    vec2 a = p - i + (i.x+i.y)*K2;
    vec2 o = (a.x>a.y) ? vec2(1.0,0.0) : vec2(0.0,1.0); //vec2 of = 0.5 + 0.5*vec2(sign(a.x-a.y), sign(a.y-a.x));
    vec2 b = a - o + K2;
    vec2 c = a - 1.0 + 2.0*K2;
    vec3 h = max(0.5-vec3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );
    vec3 n = h*h*h*h*vec3( dot(a,mat_hash(i+0.0)), dot(b,mat_hash(i+o)), dot(c,mat_hash(i+1.0)));
    return dot(n, vec3(70.0));  
}

float mat_fbm(vec2 n) {
    float total = 0.0, amplitude = 0.1;
    for (int i = 0; i < 7; i++) {
        total += mat_noise(n) * amplitude;
        n = m * n;
        amplitude *= 0.4;
    }
    return total;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 p = vec2(0.5) + (texCoord-0.5) * mat_scale;
    vec2 uv = p;
    float time = mat_animation_time / 10;
    float q = mat_fbm(uv * 0.5);
    
    //ridged noise shape
    float r = 0.0;
    uv *= mat_scale;
    uv -= q - time*mat_direction;
    float weight = 0.8;
    for (int i=0; i<8; i++){
        r += abs(weight*mat_noise( uv ));
        uv = m*uv + time*mat_direction;
        weight *= 0.7;
    }
    
    //noise shape
    float f = 0.0;
    uv = p;
    uv *= mat_scale;
    uv -= q - time*mat_direction;
    weight = 0.7;
    for (int i=0; i<8; i++){
        f += weight*mat_noise( uv );
        uv = m*uv + time*mat_direction;
        weight *= 0.6;
    }
    
    f *= r + f;
    
    //noise colour
    float c = 0.0;
    time = time * 2.0;
    uv = p;
    uv *= mat_scale*2.0;
    uv -= q - time*mat_direction;
    weight = 0.4;
    for (int i=0; i<7; i++){
        c += weight*mat_noise( uv );
        uv = m*uv + time*mat_direction;
        weight *= 0.6;
    }
    
    //noise ridge colour
    float c1 = 0.0;
    time = time * 3.0;
    uv = p;
    uv *= mat_scale*3.0;
    uv -= q - time*mat_direction;
    weight = 0.4;
    for (int i=0; i<7; i++){
        c1 += abs(weight*mat_noise( uv ));
        uv = m*uv + time*mat_direction;
        weight *= 0.6;
    }
    
    c += c1;
    
    vec3 skycolour = mix(skycolour2, skycolour1, p.y);
    vec3 cloudcolour = vec3(1.1, 1.1, 0.9) * clamp((clouddark + cloudlight*c), 0.0, 1.0);
   
    f = cloudcover + cloudalpha*f*r;
    
    //vec3 result = mix(skycolour, clamp(skytint * skycolour + cloudcolour, 0.0, 1.0), clamp(f + c, 0.0, 1.0));
    vec3 result = mix(skycolour, clamp(skytint * skycolour + cloudcolour, 0.0, 1.0), clamp(f + c, 0.0, 1.0));
    
    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    result = mix(AvgLumin, result, mat_contrast);

    // Apply brightness
    result += mat_brightness;

    return vec4( result, 1.0 );
}
