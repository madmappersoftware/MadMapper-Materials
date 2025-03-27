/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Adapted by Jason Beyers",
  "VSN": "1.0",
  "INPUTS" : [
    { "Label": "Radius1", "NAME": "_Radius", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.206 },
    { "Label": "Radius2", "NAME": "_Radius2", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.2 },
    {
        "Label": "Intensity",
        "NAME": "_Intensity",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 0.1,
        "DEFAULT": 0.01
    },

    {
        "Label": "Frequency",
        "NAME": "_Frequency",
        "TYPE": "float",
        "MIN": 10.0,
        "MAX": 50.0,
        "DEFAULT": 30.0
    },

    {
        "Label": "Spread",
        "NAME": "spread",
        "TYPE": "float",
        "MIN": 0.01,
        "MAX": 1.0,
        "DEFAULT": 0.15
    },

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
  "DESCRIPTION" : "Adjustable rings with subtle animation. From http:\/\/glslsandbox.com\/e#30475.3"
}
*/


//Will Jack @SkrillJam
//shield shader for bombasm
//specail thanks to Brandon Fogerty: check out http://xdpixel.com/energy-field/

#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define _Color vec3( 1.0,0.9, 0.4 );
// #define _Radius 0.206
// #define _Radius2 0.2
#define _PulseSpeed 0.5
// #define _Intensity 0.01
// #define _Frequency 30.0

float mat_iTime = mat_time - mat_offset;


float mat_hash( float n ) { return fract(sin(n)*753.5453123); }

// Slight modification of iq's noise function.
float mat_noise( in vec2 x )
{
    vec2 p = floor(x);
    vec2 f = fract(x);
    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*157.0;
    return mix(
                    mix( mat_hash(n+  0.0), mat_hash(n+  1.0),f.x),
                    mix( mat_hash(n+157.0), mat_hash(n+158.0),f.x),
            f.y);
}

float mat_fbm(vec2 p, vec3 a)
{
     float v = 0.0;
     v += mat_noise(p*a.x)*.5;
     v += mat_noise(p*a.y)*.25;
     v += mat_noise(p*a.z)*.125;
     return v;
}

vec3 WeirdCircle(vec2 uv, float radius, float thickness, float intensity, vec3 fbmOffset, float t2, vec3 color)
{
   float dist = length(uv);

    float aTan = atan(uv.y/uv.x);
    float t1 = intensity *sin(aTan * _Frequency + mat_iTime *t2) * 0.5 + 0.5 + mat_fbm(uv,fbmOffset) * spread; // weird scaling going on here
    float warp = mix(0.0, 0.5, t1);

    float effector = thickness*abs(0.003/((dist) - (radius + warp))); // the effector dithers out the color so it looks lasery.

    return color * effector;

}


vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 uv = texCoord * 2.0 - 1.0;

    uv *= mat_zoom;
    // vec2 uv = ( gl_FragCoord.xy / RENDERSIZE.xy ) * 2.0 - 1.0;
    // uv.x *= RENDERSIZE.x /RENDERSIZE.y;

    vec3 finalColor = vec3( 0.0 );

    // finalColor += WeirdCircle(uv, _Radius, 1.0, _Intensity, vec3( 200, 100, 50),-10.0,vec3( 0.3, 0.5, 2.5 ));
    // finalColor += WeirdCircle(uv, _Radius, 1.0, _Intensity, vec3( 90, 15, 1),1.0,vec3( 0.8,0.5, 0.0 ));

    finalColor += WeirdCircle(uv, _Radius, 1.0, _Intensity, vec3( 200, 100, 50),-10.0,vec3( 0.3, 0.5, 2.5 ));
    finalColor += WeirdCircle(uv, _Radius, 1.0, _Intensity, vec3( 90, 15, 1),1.0,vec3( 0.8,0.5, 0.0 ));


    finalColor += WeirdCircle(uv, _Radius2, 1.0, _Intensity , vec3(30, 6, 5),5.0, vec3( 2.3, 0.5, .5 ));
    finalColor += WeirdCircle(uv, _Radius2-0.04, 1.0, _Intensity * 2.0, vec3(100,100,100),4.0,vec3( 0.12, 0.15, 2.1 ));
    finalColor += WeirdCircle(uv, _Radius2-0.1, 0.1, _Intensity , vec3(200,200,200),4.0,vec3( 0.1, 0.1, 0.2 ));
    return vec4( finalColor, 1.0);
}