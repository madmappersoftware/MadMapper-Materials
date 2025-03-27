/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https://www.shadertoy.com/view/MldSz8.  Disable scrolling for more logical zooming.",
  "VSN": "1.1",
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
        "Label": "Iterations",
        "NAME": "iterations",
        "TYPE": "int",
        "MIN": 1,
        "MAX": 6,
        "DEFAULT": 4
    },
    {
        "Label": "Glow",
        "NAME": "glow",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 10.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Colored",
        "NAME": "mat_colored",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Noise/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Noise/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Noise/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Scroll/Animate",
        "NAME": "mat_scroll",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/BPM Sync",
        "NAME": "mat_scroll_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Reverse",
        "NAME": "mat_scroll_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Speed",
        "NAME": "mat_scroll_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Angle",
        "NAME": "mat_scroll_angle",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "Label": "Scroll/Offset",
        "NAME": "mat_scroll_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Strob",
        "NAME": "mat_scroll_strob",
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
    },
    {
        "NAME": "mat_scroll_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_scroll_speed",
            "speed_curve":2,
            "strob" : "mat_scroll_strob",
            "reverse": "mat_scroll_reverse",
            "bpm_sync": "mat_scroll_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

#define ANIMATE     0.5     // animation speed
// #define COLORED



vec2 Hash22 (vec2 p)
{
    vec2 q = vec2( dot( p, vec2(127.1,311.7) ),
                   dot( p, vec2(269.5,183.3) ) );

    return fract( sin(q) * 43758.5453 );
}

float Hash21 (vec2 p)
{
    return fract( sin( p.x + p.y * 64.0 ) * 104003.9);
}

vec2 Hash12 (float f)
{
    return fract( cos(f) * vec2(10003.579, 37049.7) );
}

float Hash11 (float a)
{
    return Hash21( vec2( fract(a * 2.0), fract(a * 4.0) ) );
    //return fract( sin(a) * 54833.56 );
}


// from https://www.shadertoy.com/view/ldl3W8
vec4 voronoi (in vec2 x)
{
    vec2 n = floor(x);
    vec2 f = fract(x);

    //----------------------------------
    // first pass: regular voronoi
    //----------------------------------
    vec2 mg, mr;

    float md = 8.0;
    for( int j=-1; j<=1; j++ )
    for( int i=-1; i<=1; i++ )
    {
        vec2 g = vec2(float(i),float(j));
        vec2 o = Hash22( n + g );
        #ifdef ANIMATE
        o = 0.5 + 0.5*sin( iTime * ANIMATE + 6.2831*o );
        #endif
        vec2 r = g + o - f;
        float d = dot(r,r);

        if( d<md )
        {
            md = d;
            mr = r;
            mg = g;
        }
    }

    //----------------------------------
    // second pass: distance to borders
    //----------------------------------
    md = 8.0;
    for( int j=-2; j<=2; j++ )
    for( int i=-2; i<=2; i++ )
    {
        vec2 g = mg + vec2(float(i),float(j));
        vec2 o = Hash22( n + g );
        #ifdef ANIMATE
        o = 0.5 + 0.5*sin( iTime * ANIMATE + 6.2831*o );
        #endif
        vec2 r = g + o - f;

        if( dot(mr-r,mr-r)>0.00001 )
            md = min( md, dot( 0.5*(mr+r), normalize(r-mr) ) );
    }

    return vec4( x - (n + mr + f), md, Hash21( mg + n ) );
}


vec3 HSVtoRGB (vec3 hsv)
{
    vec3 col = vec3( abs( hsv.x * 6.0 - 3.0 ) - 1.0,
                     2.0 - abs( hsv.x * 6.0 - 2.0 ),
                     2.0 - abs( hsv.x * 6.0 - 4.0 ) );

    return (( clamp( col, vec3(0.0), vec3(1.0) ) - 1.0 ) * hsv.y + 1.0 ) * hsv.z;
}

vec3 Rainbow (float color, float dist)
{
    dist = pow( dist, 8.0 );
    return mix( vec3(1.0), HSVtoRGB( vec3( color, 1.0, 1.0 ) ), 1.0 - dist );
}


vec3 VoronoiFactal (in vec2 coord, float time)
{
    const float freq = 4.0;
    const float freq2 = 6.0;
    // const int iterations = 4;

    coord *= mat_zoom;

    vec2 uv = coord * freq;

    vec3 color = vec3(0.0);
    float alpha = 0.0;
    float value = 0.0;

    for (int i = 0; i < iterations; ++i)
    {
        vec4 v = voronoi( uv );

        uv = ( v.xy * 0.5 + 0.5 ) * freq2 + Hash12( v.w );

        float f = pow( 0.01 * float(iterations - i), 3.0 );
        float a = 1.0 - smoothstep( 0.0, glow * 0.08 + f, v.z );

        vec3 c = Rainbow( Hash11( float(i+1) / float(iterations) + value * 1.341 ), i > 1 ? 0.0 : a );

        color = color * alpha + c * a;
        alpha = max( alpha, a );
        value = v.w;
    }

    if (mat_colored) {
        return color;
    } else {
        return vec3( alpha ) * Rainbow( 0.06, alpha );
    }

}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    float scroll_angle = mat_scroll_angle * 0.5;
    float scroll_time = mat_scroll_time - mat_scroll_offset * 10.;
    if (!mat_scroll) {
        scroll_time = 0.;
    }
    float scroll_time_x = scroll_time * cos(PI * scroll_angle);
    float scroll_time_y = scroll_time * sin(PI * scroll_angle);
    uv.x -= scroll_time_x;
    uv.y -= scroll_time_y;

    uv *= mat_zoom * 0.5;

    vec3 color = VoronoiFactal( uv, iTime );
    return vec4( color, 1.0 );

}