/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
    ],
    "GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "strob" : "mat_strob",
                "reverse": "mat_reverse",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#51412.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/wdlGWn
 */



#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy globals
// float iTime = 0.0;
// vec3  iResolution = vec3(0.0);

float iTime = mat_time - mat_offset * 10;


// --------[ Original ShaderToy begins here ]---------- //
float PI = 3.1415926535;
float sections = 3.0;

float atan2(in float y, in float x) {
    return x == 0.0 ? sign(y) * PI / 2.0 : atan(y, x);
}

bool belongs(float iTime, vec2 uv, float near, float far) {
    near += sin(uv.x - iTime * 8.0) / 50.0;
    far += cos(uv.y - iTime * 8.0) / 50.0;
    vec2 center = vec2(0.5, 0.5) * mat_zoom;
    vec2 xy = uv - center;

    float dist = distance(xy, vec2(0.0, 0.0));
    float angle = mod(atan2(xy.y, xy.x) + iTime * 2.5 + sin(iTime * 4.0) / 1.0, PI * 2.0);
    float oddity = mod(angle / (2.0 * PI) * sections * 2.0, 2.0);
    if (dist > near && dist < far && floor(mod(oddity, 2.0)) == 0.0) {
        return true;
    } else {
        return false;
    }
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // vec2 UV = fragCoord/iResolution.yy;
    // UV.x -= (iResolution.x / iResolution.y - 1.0) / 2.0;

    vec2 UV = fragCoord;

    UV *= mat_zoom;

    // float iTime = iTime;

    if (belongs(iTime, UV, 0.2, 0.25) || belongs(iTime + 0.5, UV, 0.3, 0.35) || belongs(iTime + 1.0, UV, 0.4, 0.45)) {
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    } else {
        fragColor = vec4(0.0, 0.0, 0.0, 0.0);
    }
}

// --------[ Original ShaderToy ends here ]---------- //

vec4 materialColorForPixel(vec2 texCoord)
{
    // iTime = iTime;
    // iResolution = vec3(RENDERSIZE, 0.0);
    vec4 color = vec4(0);
    mainImage(color, texCoord);
    return color;
}