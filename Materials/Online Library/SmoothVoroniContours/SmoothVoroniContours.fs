/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Shane, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https:\/\/www.shadertoy.com\/view\/4sdXDX by Shane",
  "VSN": "1.2",
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
            "LABEL": "Morph",
            "NAME": "mat_morph",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

    },
    {
        "LABEL": "Glossy",
        "NAME": "mat_glossy",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Animation/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },

    {
        "LABEL": "Animation/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Animation/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },

    {
        "Label": "Animation/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Animation/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_hue_shift",
            "LABEL": "Color/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        }
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

float iTime = mat_time - mat_offset * 10;

vec2 render_size = vec2(1024.);

vec2 morph = 2. * mat_morph + vec2(1.);


/*

    Smooth Voronoi Contours
    -----------------------

    Using a numerical gradient to produce smooth "fract" contours on 2D Voronoi.

    Shadertoy user "drone1" was kind enough to help me problem shoot some AA code
    yesterday on an image similar to this one, but I wanted to produce it without
    AA for realtime usage. There might be better methods, but this is the one I
    chose. It's partly based off of IQ's "Ellipse - Distance Estimation" example.

    If you press pause, you should notice that the contour lines are smooth and
    precise, regardless of the shape of the curve.

    For anyone wondering, the weird abstract image is just an amalgamation of two
    layers of smooth 2D Voronoi and an old concentric circle trick. In pseudo code:

    float val = Vor(p*freq)*A1 + Vor(p*freq*3.)*A2;
    val = clamp(cos(val*freq2*PI)*contrast, 0., 1.);

    See IQ's distance estimation example for a good explanation regarding the
    gradient related contour snippet:

    Ellipse - Distance Estimation - https://www.shadertoy.com/view/MdfGWn
    There's an accompanying articles, which is really insightful here:
    http://www.iquilezles.org/www/articles/distance/distance.htm

    Another example using the technique.
    2D Noise Contours - Shane
    https://www.shadertoy.com/view/XdcGzB

*/


// Glossy version. It's there to show that the method works with raised surfaces too.



// Standard 2x2 hash algorithm.
vec2 mat_hash22(vec2 p) {

    // Faster, but probaly doesn't disperse things as nicely as other methods.
    float n = sin(dot(p, vec2(41, 289)));
    p = fract(vec2(2097152, 262144)*n);
    return cos(p*6.283 + iTime)*.5;
    //return abs(fract(p+ iTime*.25)-.5)*2. - .5; // Snooker.
    //return abs(cos(p*6.283 + iTime))*.5; // Bounce.

}

// Smooth Voronoi. I'm not sure who came up with the original, but I think IQ
// was behind this particular algorithm. It's just like the regular Voronoi
// algorithm, but instead of determining the minimum distance, you accumulate
// values - analogous to adding metaball field values. The result is a nice
// smooth pattern. The "falloff" variable is a smoothing factor of sorts.
//
float mat_smoothVoronoi(vec2 p, float falloff) {

    vec2 ip = floor(p); p -= ip;

    float d = 1., res = 0.0;

    for(int i = -1; i <= 2; i++) {
        for(int j = -1; j <= 2; j++) {

            vec2 b = vec2(i, j);

            vec2 v = b - p + mat_hash22(ip + b);

            d = max(dot(v,v), 1e-4);

            res += 1.0/pow( d, falloff );
        }
    }

    return pow( 1./res, .5/falloff );
}

// 2D function we'll be producing the contours for.
float mat_func2D(vec2 p){


    float d = mat_smoothVoronoi(p*2., 4.)*.66*morph.x + mat_smoothVoronoi(p*6., 4.)*.34*morph.y;

    return sqrt(d);

}

// Smooth fract function. A bit hacky, but it works. Handy for all kinds of things.
// The final value controls the smoothing, so to speak. Common sense dictates that
// tighter curves, require more blur, and straighter curves require less. The way
// you do that is by passing in the function's curve-related value, which in this case
// will be the function value divided by the length of the function's gradient.
//
// IQ's distance estimation example will give you more details:
// Ellipse - Distance Estimation - https://www.shadertoy.com/view/MdfGWn
// There's an accompanying article, which is really insightful, here:
// http://www.iquilezles.org/www/articles/distance/distance.htm
float mat_smoothFract(float x, float sf){

    x = fract(x); return min(x, x*(1.-x)*sf);

}



vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    // Standard epsilon, used to determine the numerical gradient.
    vec2 e = vec2(0.001, 0);
    // The 2D function value. In this case, it's a couple of layers of 2D simplex-like noise.
    // In theory, any function should work.
    float f = mat_func2D(uv); // Range [0, 1]

    // Length of the numerical gradient of the function above. Pretty standard. Requires two extra function
    // calls, which isn't too bad.
    float g = length( vec2(f - mat_func2D(uv-e.xy), f - mat_func2D(uv-e.yx)) )/(e.x);

    // Dividing a constant by the length of its gradient. Not quite the same, but related to IQ's
    // distance estimation example: Ellipse - Distance Estimation - https://www.shadertoy.com/view/MdfGWn
    g = 1./max(g, 0.001);

    // This is the crux of the shader. Taking a function value and producing some contours. In this case,
    // there are twelve. If you don't care about aliasing, it's as simple as: c = fract(f*12.);
    // If you do, and who wouldn't, you can use the following method. For a quick explanation, refer to the
    // "mat_smoothFract" function or look up a concetric circle (bullseye) function.
    //
    // For a very good explanation, see IQ's distance estimation example:
    // Ellipse - Distance Estimation - https://www.shadertoy.com/view/MdfGWn
    //
    // There's an accompanying articles, which is really insightful, here:
    // http://www.iquilezles.org/www/articles/distance/distance.htm
    //
    float freq = 12.;
    // Smoothing factor. Hand picked. Ties in with the frequency above. Higher frequencies
    // require a lower value, and vice versa.
    float smoothFactor = render_size.y*0.0125;


    float c;
    if (mat_glossy) {
        c = mat_smoothFract(f*freq, g*render_size.y/16.); // Range [0, 1]
        //float c = fract(f*freq); // Aliased version, for comparison.
    } else {
        c = clamp(cos(f*freq*3.14159*2.)*g*smoothFactor, 0., 1.); // Range [0, 1]
        //float c = clamp(cos(f*freq*3.14159*2.)*2., 0., 1.); // Blurry contours, for comparison.
    }


    // Coloring.
    //
    // Convert "c" above to the greyscale and green colors.
    vec3 col = vec3(c);
    vec3 col2 = vec3(c*0.64, c, c*c*0.1);

    if (mat_glossy) {
        col = mix(col, col2, -uv.y + clamp(fract(f*freq*0.5)*2.-1., 0., 1.0));
    } else {
        col = mix(col, col2, -uv.y + clamp(cos(f*freq*3.14159)*2., 0., 1.0));
    }

    // Color in a couple of thecontours above. Not madatory, but it's pretty simple, and an interesting
    // way to pretty up functions. I use it all the time.
    f = f*freq;

    if (mat_glossy) {
        if(f>8. && f<9.) col *= vec3(1, 0, .1);
    } else {
        if(f>8.5 && f<9.5) col *= vec3(1, 0, .1);
    }

    // Since we have the gradient related value, we may as well use it for something. In this case, we're
    // adding a bit of highlighting. It's calculated for the contourless noise, so doesn't match up perfectly,
    // but it's good enough. Comment it out to see the texture on its own.
    if (mat_glossy) {
        col += g*g*g*vec3(.3, .5, 1)*.25*.25*.25*.1;
    }


    //col = c * vec3(g*.25); // Just the function and gradient. Has a plastic wrap feel.

    // Done.
    vec4 out_color = vec4( sqrt(clamp(col, 0., 1.)), 1.0 );

    // Apply invert
    if (mat_invert) out_color.rgb=1-out_color.rgb;

    // Apply Hue Shift and saturation
    if (mat_hue_shift > 0.01 || mat_saturation != 0) {
        vec3 hsv = rgb2hsv(out_color.rgb);
        hsv.x = fract(0.9999999*(hsv.x+mat_hue_shift));
        hsv.y = max(hsv.y + mat_saturation, 0);
        out_color.rgb = hsv2rgb(hsv);
    }

    // Apply contrast
    const vec3 LumCoeff = vec3(0.2125, 0.7154, 0.0721);
    const vec3 AvgLumin = vec3(0.5, 0.5, 0.5);
    vec3 intensity = vec3(dot(out_color.rgb, LumCoeff));
    out_color.rgb = mix(AvgLumin, out_color.rgb, mat_contrast);

    // Apply brightness
    out_color.rgb += mat_brightness;

    return out_color;





}