/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "nimitz, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https://www.shadertoy.com/view/lsSGzy",
  "VSN": "1.0",
  "IMPORTED": [
        {
            "NAME": "iChannel0",
            "PATH": "Flaring.png"
        },
  ],
  "INPUTS" : [

    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },

    {
        "Label": "Brightness",
        "NAME": "brightness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 3.0
    },

    {
        "Label": "Ray Brightness",
        "NAME": "ray_brightness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 20.0,
        "DEFAULT": 5.0
    },

    {
        "Label": "Gamma",
        "NAME": "gamma",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 12.0,
        "DEFAULT": 6.0
    },

    {
        "Label": "Spot Brightness",
        "NAME": "spot_brightness",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 15.0,
        "DEFAULT": 1.5
    },

    {
        "Label": "Ray Density",
        "NAME": "ray_density",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 30.0,
        "DEFAULT": 6.0
    },

    {
        "Label": "Curvature",
        "NAME": "curvature",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 200.0,
        "DEFAULT": 98.0
    },

    {
        "Label": "Red",
        "NAME": "red",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 1.8
    },

    {
        "Label": "Green",
        "NAME": "green",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 3.0
    },

    {
        "Label": "Blue",
        "NAME": "blue",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 0.5
    },

    {
        "Label": "Noise Type",
        "NAME": "noisetype",
        "TYPE": "int",
        "MIN": 1,
        "MAX": 2,
        "DEFAULT": 1
    },

    {
        "Label": "Sin Freq",
        "NAME": "sin_freq",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 12.0,
        "DEFAULT": 6.0
    },

    {
        "LABEL": "Distort",
        "NAME": "distort",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },



    {
        "LABEL": "Motion/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Motion/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Motion/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Motion/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Motion/Strob",
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
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float mat_iTime = mat_time - mat_offset * 10;


// brightness = 3.;
//     ray_brightness = 5.;
//     gamma = 6.;
//     spot_brightness = 1.5;
//     ray_density = 6.;
//     curvature = 90.;
//     red   = 1.8;
//     green = 3.;
//     blue  = .5;
//     noisetype = 1;
//     sin_freq = 6.;
#define YO_DAWG

//Flaring by nimitz (twitter: @stormoid)

//change this value (1 to 5) or tweak the settings yourself.
//the gamma and spot brightness parameters can use negative values
#define TYPE 4


// #if TYPE == 1
//     #define brightness 1.
//     #define ray_brightness 11.
//     #define gamma 5.
//     #define spot_brightness 4.
//     #define ray_density 1.5
//     #define curvature .1
//     #define red   7.
//     #define green 1.3
//     #define blue  1.
//     //1 -> ridged, 2 -> sinfbm, 3 -> pure fbm
//     #define noisetype 2
//     #define sin_freq 50. //for type 2
// #elif TYPE == 2
//     #define brightness 1.5
//     #define ray_brightness 10.
//     #define gamma 8.
//     #define spot_brightness 15.
//     #define ray_density 3.5
//     #define curvature 15.
//     #define red   4.
//     #define green 1.
//     #define blue  .1
//     #define noisetype 1
//     #define sin_freq 13.
// #elif TYPE == 3
//     #define brightness 1.5
//     #define ray_brightness 20.
//     #define gamma 4.
//     #define spot_brightness .95
//     #define ray_density 3.14
//     #define curvature 17.
//     #define red   2.9
//     #define green .7
//     #define blue  3.5
//     #define noisetype 2
//     #define sin_freq 15.
// #elif TYPE == 4
//     #define brightness 3.
//     #define ray_brightness 5.
//     #define gamma 6.
//     #define spot_brightness 1.5
//     #define ray_density 6.
//     #define curvature 90.
//     #define red   1.8
//     #define green 3.
//     #define blue  .5
//     #define noisetype 1
//     #define sin_freq 6.
//     #define YO_DAWG
// #elif TYPE == 5
//     #define brightness 2.
//     #define ray_brightness 5.
//     #define gamma 5.
//     #define spot_brightness 1.7
//     #define ray_density 30.
//     #define curvature 1.
//     #define red   1.
//     #define green 4.0
//     #define blue  4.9
//     #define noisetype 2
//     #define sin_freq 5. //for type 2
// #endif

// float brightness = 1.5;
// float ray_brightness = 10.;
// float gamma = 8.;
// float spot_brightness = 15.;
// float ray_density = 3.5;
// float curvature = 15.;
// float red   = 4.;
// float green = 1.;
// float blue  = .1;
// float noisetype = 1;
// float sin_freq = 13.;


// if defined(preset_IS_Preset1) {
//     brightness = 1.;
//     ray_brightness = 11.;
//     gamma = 5.;
//     spot_brightness = 4.;
//     ray_density = 1.5;
//     curvature = .1;
//     red   = 7.;
//     green = 1.3;
//     blue  = 1.;
//     //1 -> ridged, 2 -> sinfbm, 3 -> pure fbm
//     noisetype = 2;
//     sin_freq = 50.; //for type 2
// } else if (preset == "Preset2" ) {
//     brightness = 1.5;
//     ray_brightness = 10.;
//     gamma = 8.;
//     spot_brightness = 15.;
//     ray_density = 3.5;
//     curvature = 15.;
//     red   = 4.;
//     green = 1.;
//     blue  = .1;
//     noisetype = 1;
//     sin_freq = 13.;
// } else if (preset == "Preset3" ) {
//     brightness = 1.5;
//     ray_brightness = 20.;
//     gamma = 4.;
//     spot_brightness = .95;
//     ray_density = 3.14;
//     curvature = 17.;
//     red   = 2.9;
//     green = .7;
//     blue  = 3.5;
//     noisetype = 2;
//     sin_freq = 15.;
// } else if (preset == "Preset4" ) {
//     brightness = 3.;
//     ray_brightness = 5.;
//     gamma = 6.;
//     spot_brightness = 1.5;
//     ray_density = 6.;
//     curvature = 90.;
//     red   = 1.8;
//     green = 3.;
//     blue  = .5;
//     noisetype = 1;
//     sin_freq = 6.;
//     #define YO_DAWG
// } else {
//     brightness = 2.;
//     ray_brightness = 5.;
//     gamma = 5.;
//     spot_brightness = 1.7;
//     ray_density = 30.;
//     curvature = 1.;
//     red   = 1.;
//     green = 4.0;
//     blue  = 4.9;
//     noisetype = 2;
//     sin_freq = 5.; //for type 2
// }


// #define PROCEDURAL_NOISE
//#define YO_DAWG


float mat_hash( float n ){return fract(sin(n)*43758.5453);}

float mat_noise( in vec2 x )
{
    #ifdef PROCEDURAL_NOISE
    x *= 1.75;
    vec2 p = floor(x);
    vec2 f = fract(x);

    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*57.0;

    float res = mix(mix( mat_hash(n+  0.0), mat_hash(n+  1.0),f.x),
                    mix( mat_hash(n+ 57.0), mat_hash(n+ 58.0),f.x),f.y);
    return res;
    #else
    return texture(iChannel0, x*.01).x;
    #endif
}

mat2 m2 = mat2( 0.80,  0.60, -0.60,  0.80 );
float mat_fbm( in vec2 p )
{
    float z=2.;
    float rz = 0.;
    p *= 0.25;
    for (float i= 1.;i < 6.;i++ )
    {
        if (noisetype == 1) {
            rz+= abs((mat_noise(p)-0.5)*2.)/z;
        } else if (noisetype == 2) {
            rz+= (sin(mat_noise(p)*sin_freq)*0.5+0.5) /z;
        } else {
            rz+= mat_noise(p)/z;
        }

        z = z*2.;
        p = p*2.*m2;
    }
    return rz;
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    float t = -mat_iTime*0.03;

    uv*= curvature*.05+0.0001;

    float r  = sqrt(dot(uv,uv));
    float x = dot(normalize(uv), vec2(.5,0.))+t;
    float y = dot(normalize(uv), vec2(.0,.5))+t;

    if (distort) {
        x  = mat_fbm(vec2(y*ray_density*0.5,r+x*ray_density*.2));
        y = mat_fbm(vec2(r+y*ray_density*0.1,x*ray_density*.5));
    }

    float val;
    val = mat_fbm(vec2(r+y*ray_density,r+x*ray_density-y));
    val = smoothstep(gamma*.02-.1,ray_brightness+(gamma*0.02-.1)+.001,val);
    val = sqrt(val);

    vec3 col = val/vec3(red,green,blue);
    col = clamp(1.-col,0.,1.);
    col = mix(col,vec3(1.),spot_brightness-r/0.1/curvature*200./brightness);

    return vec4(col,1.0);




}