/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Adapted by Jason Beyers, original by mojovideotech",

  "DESCRIPTION": "shadertoy.com\/4dB3DV by steverock.  Texture-less Perlin shader code : github.com/ashima/webgl-noise",

  "VSN": "1.0",
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
        "Label": "Warp",
        "NAME": "warp",
        "TYPE": "float",
        "DEFAULT": 0.05,
        "MIN": 0.01,
        "MAX": 1.5
    },

    {
        "Label": "Hue",
        "NAME": "hue",
        "TYPE": "float",
        "DEFAULT": 0.05,
        "MIN": 0,
        "MAX": 0.6
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
        "LABEL": "Noise/Noise Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.5
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
        "LABEL": "Sweep/BPM Sync",
        "NAME": "mat_sweep_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Sweep/Reverse",
        "NAME": "mat_sweep_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Sweep/Sweep Speed",
        "NAME": "mat_sweep_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Sweep/Offset",
        "NAME": "mat_sweep_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Sweep/Strob",
        "NAME": "mat_sweep_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    }

  ],
 "GENERATORS": [
    {
        "NAME": "mat_noise_time",
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
        "NAME": "mat_sweep_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_sweep_speed",
            "speed_curve":2,
            "strob" : "mat_sweep_strob",
            "reverse": "mat_sweep_reverse",
            "bpm_sync": "mat_sweep_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
],

}
*/


#include "MadCommon.glsl"
// #include "MadNoise.glsl"
#include "MadSDF.glsl"

////////////////////////////////////////////////////////////
// PerlinNoiseFun  by mojovideotech
//
// mod of :
// shadertoy.com\/4dB3DV by steverock
//
// Creative Commons Attribution-NonCommercial-ShareAlike 3.0
////////////////////////////////////////////////////////////

float iTime = mat_noise_time - mat_offset * 10;

float sweep_time = mat_sweep_time - mat_sweep_offset * 10;


vec3 mod289(vec3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

vec2 mod289(vec2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

vec4 mod289(vec4 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

vec4 permute(vec4 x) { return mod289(((x*34.0)+1.0)*x); }

vec3 permute(vec3 x) { return mod289(((x*34.0)+1.0)*x); }

float snoise2d(vec2 v) {
  const vec4 C = vec4(0.211324865405187,  // (3.0-sqrt(3.0))/6.0
                      0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                     -0.577350269189626,  // -1.0 + 2.0 * C.x
                      0.024390243902439); // 1.0 / 41.0
// First corner
  vec2 i  = floor(v + dot(v, C.yy) );
  vec2 x0 = v -   i + dot(i, C.xx);
// Other corners
  vec2 i1;
  i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
  vec4 x12 = x0.xyxy + C.xxzz;
  x12.xy -= i1;
// Permutations
  i = mod289(i); // Avoid truncation effects in permutation
  vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 )) + i.x + vec3(0.0, i1.x, 1.0 ));
  vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
  m = m*m ;
  m = m*m ;
// Gradients: 41 points uniformly over a line, mapped onto a diamond.
// The ring size 17*17 = 289 is close to a multiple of 41 (41*7 = 287)
  vec3 x = 2.0 * fract(p * C.www) - 1.0;
  vec3 h = abs(x) - 0.5;
  vec3 ox = floor(x + 0.5);
  vec3 a0 = x - ox;
// Normalise gradients implicitly by scaling m
// Approximation of: m *= inversesqrt( a0*a0 + h*h );
  m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );
// Compute final noise value at P
  vec3 g;
  g.x  = a0.x  * x0.x  + h.x  * x0.y;
  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
  return 130.0 * dot(m, g);
}

vec4 taylorInvSqrt(vec4 r) { return 1.79284291400159 - 0.85373472095314 * r; }

float snoise3d(vec3 v) {
  const vec2  C = vec2(1.0/6.0, 1.0/3.0) ;
  const vec4  D = vec4(0.0, 0.5, 1.0, 2.0);
// First corner
  vec3 i  = floor(v + dot(v, C.yyy) );
  vec3 x0 =   v - i + dot(i, C.xxx) ;
// Other corners
  vec3 g = step(x0.yzx, x0.xyz);
  vec3 l = 1.0 - g;
  vec3 i1 = min( g.xyz, l.zxy );
  vec3 i2 = max( g.xyz, l.zxy );
  vec3 x1 = x0 - i1 + C.xxx;
  vec3 x2 = x0 - i2 + C.yyy; // 2.0*C.x = 1/3 = C.y
  vec3 x3 = x0 - D.yyy;      // -1.0+3.0*C.x = -0.5 = -D.y
// Permutations
  i = mod289(i);
  vec4 p = permute( permute( permute(
             i.z + vec4(0.0, i1.z, i2.z, 1.0 ))
           + i.y + vec4(0.0, i1.y, i2.y, 1.0 ))
           + i.x + vec4(0.0, i1.x, i2.x, 1.0 ));
// Gradients: 7x7 points over a square, mapped onto an octahedron.
// The ring size 17*17 = 289 is close to a multiple of 49 (49*6 = 294)
  float n_ = 0.142857142857; // 1.0/7.0
  vec3  ns = n_ * D.wyz - D.xzx;
  vec4 j = p - 49.0 * floor(p * ns.z * ns.z);  //  mod(p,7*7)
  vec4 x_ = floor(j * ns.z);
  vec4 y_ = floor(j - 7.0 * x_ );    // mod(j,N)
  vec4 x = x_ *ns.x + ns.yyyy;
  vec4 y = y_ *ns.x + ns.yyyy;
  vec4 h = 1.0 - abs(x) - abs(y);
  vec4 b0 = vec4( x.xy, y.xy );
  vec4 b1 = vec4( x.zw, y.zw );
  vec4 s0 = floor(b0)*2.0 + 1.0;
  vec4 s1 = floor(b1)*2.0 + 1.0;
  vec4 sh = -step(h, vec4(0.0));
  vec4 a0 = b0.xzyw + s0.xzyw*sh.xxyy ;
  vec4 a1 = b1.xzyw + s1.xzyw*sh.zzww ;
  vec3 p0 = vec3(a0.xy,h.x);
  vec3 p1 = vec3(a0.zw,h.y);
  vec3 p2 = vec3(a1.xy,h.z);
  vec3 p3 = vec3(a1.zw,h.w);
//Normalise gradients
  vec4 norm = taylorInvSqrt(vec4(dot(p0,p0), dot(p1,p1), dot(p2, p2), dot(p3,p3)));
  p0 *= norm.x;
  p1 *= norm.y;
  p2 *= norm.z;
  p3 *= norm.w;
// Mix final noise value
  vec4 m = max(0.6 - vec4(dot(x0,x0), dot(x1,x1), dot(x2,x2), dot(x3,x3)), 0.0);
  m = m * m;
  return 42.0 * dot( m*m, vec4( dot(p0,x0), dot(p1,x1), dot(p2,x2), dot(p3,x3)));
}


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);

    uv *= mat_zoom;

    // float hz = bpm*1.0/60.0;

    // warp the UV -  sin curve to modulate with time
    uv += warp*(sin(6.3/20.)*2.-1.) * vec2(snoise2d(uv*4.),snoise2d(uv*2.));
    float sweeper = sin(uv.x*6.3/1.+sweep_time) * 0.5 + 0.5;
    float sharpness = mix(0.3, 1.2, sweeper);
    vec3 col = vec3(1.0-hue,hue+0.4, mix(0.5, 1.0, sweeper));
    col *= smoothstep(0.4, 0.5, pow(snoise3d( vec3(uv*10., iTime )),sharpness));
    return vec4(col,1.0);


}