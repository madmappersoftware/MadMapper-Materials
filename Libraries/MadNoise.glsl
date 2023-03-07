/*
MadNoise Glsl Library

Copyright (c) 2016, Garage Cube, All rights reserved.
Copyright (c) 2016, Simon Geilfus, All rights reserved.

Portions of code adapted from Brian Sharpe Noise Library
Portions of code adapted from Stefan Gustavson Simplex Noise Public Domain implementation
Curl noise adapted from Robert Bridson papers

Redistribution and use in source and binary forms, with or without modification, are permitted provided that
the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and
 the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
 the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.
*/

//#define NOISE_TEXTURE_BASED
//#define NOISE_WORLEY_ASHIMA_IMPL

#ifndef NOISE_LUT_INV_SIZE
    #define NOISE_LUT_INV_SIZE 1.0 / 256
#endif

//! Returns a 2D value noise in the range [0,1]
float vnoise( vec2 p );
//! Returns a 3D value noise in the range [0,1]
float vnoise( vec3 p );

//! Returns a 2D value noise with derivatives in the range [0,1] 
vec3 dvnoise( vec2 p );
//! Returns a 3D value noise with derivatives in the range [0,1]
vec4 dvnoise( vec3 p );

//! Returns a 2D simplex noise in the range [-1,1]
float noise( vec2 p );
//! Returns a 3D simplex noise in the range [-1,1]
float noise( vec3 p );

//! Returns a 2D simplex noise with derivatives in the range [-1,1]
vec3 dnoise( vec2 p );
//! Returns a 3D simplex noise with derivatives in the range [-1,1]
vec4 dnoise( vec3 p );

#if ! defined( NOISE_WORLEY_ASHIMA_IMPL )
//! Returns a 2D worley/cellular noise in the range [0,1]
float worleyNoise( vec2 p );
//! Returns a 3D worley/cellular noise in the range [0,1]
float worleyNoise( vec3 p );
#else 
//! Returns a 2D worley/cellular noise F1 and F2 in the range [0,1]
vec2 worleyNoise( vec2 p );
//! Returns a 3D worley/cellular noise F1 and F2 in the range [0,1]
vec2 worleyNoise( vec3 p );
#endif

//! Returns a 2D worley/cellular noise with derivatives in the range [0,1]
vec3 dWorleyNoise( vec2 p );
//! Returns a 3D worley/cellular noise with derivatives in the range [0,1]
vec4 dWorleyNoise( vec3 p );

//! Returns a 2D simplex noise with rotating gradients in the range [-1,-1]
float flowNoise( vec2 pos, float rot );
//! Returns a 2D simplex noise with rotating gradients and derivatives in the range [-1,-1]
vec3 dFlowNoise( vec2 pos, float rot );

//! Returns a 2D Billowed Noise in the range [0,1]
float billowedNoise( vec2 p );
//! Returns a 3D Billowed Noise in the range [0,1]
float billowedNoise( vec3 p );

//! Returns a 2D Billowed Noise with Derivatives in the range [0,1]
vec3 dBillowedNoise( vec2 p );
//! Returns a 3D Billowed Noise with Derivatives in the range [0,1]
vec4 dBillowedNoise( vec3 p );

//! Returns a 2D Ridged Noise in the range [0,1]
float ridgedNoise( vec2 p );
//! Returns a 2D Ridged Noise in the range [0,1]
float ridgedNoise( vec2 p, float ridge );
//! Returns a 3D Ridged Noise in the range [0,1]
float ridgedNoise( vec3 p );
//! Returns a 3D Ridged Noise in the range [0,1]
float ridgedNoise( vec3 p, float ridge );

//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
vec3 dRidgedNoise( vec2 p );
//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
vec3 dRidgedNoise( vec2 p, float ridge );
//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
vec4 dRidgedNoise( vec3 p );
//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
vec4 dRidgedNoise( vec3 p, float ridge );

//! Returns a 2D Curl of a Simplex Noise in the range [-1,1]
vec2 curlNoise( vec2 v );
//! Returns a 2D Curl of a Simplex Flow Noise in the range [-1,1]
vec2 curlNoise( vec2 v, float t );

//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec2 p );
//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec2 p, int octaves, float lacunarity, float gain );
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec3 p );
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec3 p, int octaves, float lacunarity, float gain );

//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
vec3 dfBm( vec2 p );
//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dfBm( vec2 p, int octaves, float lacunarity, float gain );
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dfBm( vec3 p );
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dfBm( vec3 p, int octaves, float lacunarity, float gain );

//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec2 p );
//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec2 p, int octaves, float lacunarity, float gain );
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec3 p );
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec3 p, int octaves, float lacunarity, float gain );

//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec3 dBillowyTurbulence( vec2 p );
//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec3 dBillowyTurbulence( vec2 p, int octaves, float lacunarity, float gain );
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec4 dBillowyTurbulence( vec3 p );
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec4 dBillowyTurbulence( vec3 p, int octaves, float lacunarity, float gain );

//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p );
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p, float ridgeOffset );
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p, float ridgeOffset );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain );

//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
vec3 dRidgedMF( vec2 p );
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dRidgedMF( vec2 p, float ridgeOffset );
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dRidgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p, float ridgeOffset );
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain );

// Implementation

// Adapted from Brian Sharpe Noise Library
// https://github.com/BrianSharpe/Wombat/

//! Returns a 2D value noise in the range [0,1]
#ifndef NOISE_TEXTURE_BASED
float vnoise( vec2 p )
{
    //  establish our grid cell and unit position
    vec2 Pi = floor(p);
    vec2 Pf = p - Pi;

    //  calculate the hash.
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash = fract( Pt * ( 1.0 / 951.135664 ) );

    //  blend the results and return
    vec2 blend = Pf * Pf * Pf * (Pf * (Pf * 6.0 - 15.0) + 10.0);
    vec4 blend2 = vec4( blend, vec2( 1.0 - blend ) );
    return dot( hash, blend2.zxzx * blend2.wwyy );
}
#else
float vnoise( vec2 p )
{
    vec2 ip = floor( p );
    vec2 fp = p - ip;
    vec2 uv = ( ip + ( fp * fp * ( 3.0f - 2.0f * fp ) ) + 0.5f ) * NOISE_LUT_INV_SIZE;
    return texture( noiseLUT, uv ).x;
}
#endif

//! Returns a 2D value noise with derivatives in the range [0,1] 
//#ifndef NOISE_TEXTURE_BASED
vec3 dvnoise( vec2 p )
{
    //  establish our grid cell and unit position
    vec2 Pi = floor(p);
    vec2 Pf = p - Pi;

    //  calculate the hash.
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash = fract( Pt * ( 1.0 / 951.135664 ) );

    //  blend the results and return
    vec4 blend = Pf.xyxy * Pf.xyxy * ( Pf.xyxy * ( Pf.xyxy * ( Pf.xyxy * vec2( 6.0, 0.0 ).xxyy + vec2( -15.0, 30.0 ).xxyy ) + vec2( 10.0, -60.0 ).xxyy ) + vec2( 0.0, 30.0 ).xxyy );
    vec4 res0 = mix( hash.xyxz, hash.zwyw, blend.yyxx );
    return vec3( res0.x, 0.0, 0.0 ) + ( res0.yyw - res0.xxz ) * blend.xzw;
}
//#else 
//#endif

//! Returns a 3D value noise in the range [0,1]
#ifndef NOISE_TEXTURE_BASED
float vnoise( vec3 p )
{
    // establish our grid cell and unit position
    vec3 Pi = floor(p);
    vec3 Pf = p - Pi;
    vec3 Pf_min1 = Pf - 1.0;

    // clamp the domain
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    // calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec2 hash_mod = vec2( 1.0 / ( 635.298681 + vec2( Pi.z, Pi_inc1.z ) * 48.500388 ) );
    vec4 hash_lowz = fract( Pt * hash_mod.xxxx );
    vec4 hash_highz = fract( Pt * hash_mod.yyyy );

    //  blend the results and return
    vec3 blend = Pf * Pf * Pf * (Pf * (Pf * 6.0 - 15.0) + 10.0);
    vec4 res0 = mix( hash_lowz, hash_highz, blend.z );
    vec4 blend2 = vec4( blend.xy, vec2( 1.0 - blend.xy ) );
    return dot( res0, blend2.zxzx * blend2.wwyy );
}
#else
float vnoise( vec3 p )
{
    vec3 ip = floor( p );
    vec3 fp = p - ip;
    fp      = ( fp * fp * ( 3.0f - 2.0f * fp ) );
    
    vec2 uv = ( ip.xy + vec2( 37.0f, 17.0f ) * ip.z ) + fp.xy;
    vec2 rg = texture( noiseLUT, ( uv + 0.5f ) * NOISE_LUT_INV_SIZE, -100.0f ).yx;
    return mix( rg.x, rg.y, fp.z );
}
#endif

//! Returns a 3D value noise with derivatives in the range [0,1]
vec4 dvnoise( vec3 p )
{
    // establish our grid cell and unit position
    vec3 Pi = floor(p);
    vec3 Pf = p - Pi;
    vec3 Pf_min1 = Pf - 1.0;

    // clamp the domain
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    // calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec2 hash_mod = vec2( 1.0 / ( 635.298681 + vec2( Pi.z, Pi_inc1.z ) * 48.500388 ) );
    vec4 hash_lowz = fract( Pt * hash_mod.xxxx );
    vec4 hash_highz = fract( Pt * hash_mod.yyyy );

    //  blend the results and return
    vec3 blend = Pf * Pf * Pf * (Pf * (Pf * 6.0 - 15.0) + 10.0);
    vec3 blendDeriv = Pf * Pf * (Pf * (Pf * 30.0 - 60.0) + 30.0);
    vec4 res0 = mix( hash_lowz, hash_highz, blend.z );
    vec4 res1 = mix( res0.xyxz, res0.zwyw, blend.yyxx );
    vec4 res3 = mix( vec4( hash_lowz.xy, hash_highz.xy ), vec4( hash_lowz.zw, hash_highz.zw ), blend.y );
    vec2 res4 = mix( res3.xz, res3.yw, blend.x );
    return vec4( res1.x, 0.0, 0.0, 0.0 ) + ( vec4( res1.yyw, res4.y ) - vec4( res1.xxz, res4.x ) ) * vec4( blend.x, blendDeriv );
}

//! Returns a 2D simplex noise in the range [-1,1]
float noise( vec2 p )
{
    //  simplex math constants
    const float SKEWFACTOR = 0.36602540378443864676372317075294;            // 0.5*(sqrt(3.0)-1.0)
    const float UNSKEWFACTOR = 0.21132486540518711774542560974902;          // (3.0-sqrt(3.0))/6.0
    const float SIMPLEX_TRI_HEIGHT = 0.70710678118654752440084436210485;    // sqrt( 0.5 )  height of simplex triangle
    const vec3 SIMPLEX_POINTS = vec3( 1.0-UNSKEWFACTOR, -UNSKEWFACTOR, 1.0-2.0*UNSKEWFACTOR );  //  simplex triangle geo

    //  establish our grid cell.
    p *= SIMPLEX_TRI_HEIGHT;    // scale space so we can have an approx feature size of 1.0
    vec2 Pi = floor( p + dot( p, vec2( SKEWFACTOR ) ) );

    // calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash_x = fract( Pt * ( 1.0 / 951.135664 ) );
    vec4 hash_y = fract( Pt * ( 1.0 / 642.949883 ) );

    //  establish vectors to the 3 corners of our simplex triangle
    vec2 v0 = Pi - dot( Pi, vec2( UNSKEWFACTOR ) ) - p;
    vec4 v1pos_v1hash = (v0.x < v0.y) ? vec4(SIMPLEX_POINTS.xy, hash_x.y, hash_y.y) : vec4(SIMPLEX_POINTS.yx, hash_x.z, hash_y.z);
    vec4 v12 = vec4( v1pos_v1hash.xy, SIMPLEX_POINTS.zz ) + v0.xyxy;

    //  calculate the dotproduct of our 3 corner vectors with 3 random normalized vectors
    vec3 grad_x = vec3( hash_x.x, v1pos_v1hash.z, hash_x.w ) - 0.49999;
    vec3 grad_y = vec3( hash_y.x, v1pos_v1hash.w, hash_y.w ) - 0.49999;
    vec3 grad_results = inversesqrt( grad_x * grad_x + grad_y * grad_y ) * ( grad_x * vec3( v0.x, v12.xz ) + grad_y * vec3( v0.y, v12.yw ) );

    //  Normalization factor to scale the final result to a strict 1.0->-1.0 range
    //  http://briansharpe.wordpress.com/2012/01/13/simplex-noise/#comment-36
    const float FINAL_NORMALIZATION = 99.204334582718712976990005025589;

    //  evaluate and return
    vec3 m = vec3( v0.x, v12.xz ) * vec3( v0.x, v12.xz ) + vec3( v0.y, v12.yw ) * vec3( v0.y, v12.yw );
    m = max(0.5 - m, 0.0);
    m = m*m;
    return dot(m*m, grad_results) * FINAL_NORMALIZATION;
}

//! Returns a 2D simplex noise with derivatives in the range [-1,1]
vec3 dnoise( vec2 p )
{
    //  simplex math constants
    const float SKEWFACTOR = 0.36602540378443864676372317075294;            // 0.5*(sqrt(3.0)-1.0)
    const float UNSKEWFACTOR = 0.21132486540518711774542560974902;          // (3.0-sqrt(3.0))/6.0
    const float SIMPLEX_TRI_HEIGHT = 0.70710678118654752440084436210485;    // sqrt( 0.5 )  height of simplex triangle
    const vec3 SIMPLEX_POINTS = vec3( 1.0-UNSKEWFACTOR, -UNSKEWFACTOR, 1.0-2.0*UNSKEWFACTOR );  //  simplex triangle geo

    //  establish our grid cell.
    p *= SIMPLEX_TRI_HEIGHT;    // scale space so we can have an approx feature size of 1.0
    vec2 Pi = floor( p + dot( p, vec2( SKEWFACTOR ) ) );

    // calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash_x = fract( Pt * ( 1.0 / 951.135664 ) );
    vec4 hash_y = fract( Pt * ( 1.0 / 642.949883 ) );

    //  establish vectors to the 3 corners of our simplex triangle
    vec2 v0 = Pi - dot( Pi, vec2( UNSKEWFACTOR ) ) - p;
    vec4 v1pos_v1hash = (v0.x < v0.y) ? vec4(SIMPLEX_POINTS.xy, hash_x.y, hash_y.y) : vec4(SIMPLEX_POINTS.yx, hash_x.z, hash_y.z);
    vec4 v12 = vec4( v1pos_v1hash.xy, SIMPLEX_POINTS.zz ) + v0.xyxy;

    //  calculate the dotproduct of our 3 corner vectors with 3 random normalized vectors
    vec3 grad_x = vec3( hash_x.x, v1pos_v1hash.z, hash_x.w ) - 0.49999;
    vec3 grad_y = vec3( hash_y.x, v1pos_v1hash.w, hash_y.w ) - 0.49999;
    vec3 norm = inversesqrt( grad_x * grad_x + grad_y * grad_y );
    grad_x *= norm;
    grad_y *= norm;
    vec3 grad_results = grad_x * vec3( v0.x, v12.xz ) + grad_y * vec3( v0.y, v12.yw );

    //  evaluate the kernel
    vec3 m = vec3( v0.x, v12.xz ) * vec3( v0.x, v12.xz ) + vec3( v0.y, v12.yw ) * vec3( v0.y, v12.yw );
    m = max(0.5 - m, 0.0);
    vec3 m2 = m*m;
    vec3 m4 = m2*m2;

    //  calc the derivatives
    vec3 temp = 8.0 * m2 * m * grad_results;
    float xderiv = dot( temp, vec3( v0.x, v12.xz ) ) - dot( m4, grad_x );
    float yderiv = dot( temp, vec3( v0.y, v12.yw ) ) - dot( m4, grad_y );

    //  Normalization factor to scale the final result to a strict 1.0->-1.0 range
    //  http://briansharpe.wordpress.com/2012/01/13/simplex-noise/#comment-36
    const float FINAL_NORMALIZATION = 99.204334582718712976990005025589;

    //  sum and return all results as a vec3
    return vec3( dot( m4, grad_results ), xderiv, yderiv ) * FINAL_NORMALIZATION;
}

//! Returns a 3D simplex noise in the range [-1,1]
float noise( vec3 p )
{
    //  simplex math constants
    const float SKEWFACTOR = 1.0/3.0;
    const float UNSKEWFACTOR = 1.0/6.0;
    const float SIMPLEX_CORNER_POS = 0.5;
    const float SIMPLEX_TETRAHADRON_HEIGHT = 0.70710678118654752440084436210485;    // sqrt( 0.5 )

    //  establish our grid cell.
    p *= SIMPLEX_TETRAHADRON_HEIGHT;    // scale space so we can have an approx feature size of 1.0
    vec3 Pi = floor( p + dot( p, vec3( SKEWFACTOR) ) );

    //  Find the vectors to the corners of our simplex tetrahedron
    vec3 x0 = p - Pi + dot(Pi, vec3( UNSKEWFACTOR ) );
    vec3 g = step(x0.yzx, x0.xyz);
    vec3 l = 1.0 - g;
    vec3 Pi_1 = min( g.xyz, l.zxy );
    vec3 Pi_2 = max( g.xyz, l.zxy );
    vec3 x1 = x0 - Pi_1 + UNSKEWFACTOR;
    vec3 x2 = x0 - Pi_2 + SKEWFACTOR;
    vec3 x3 = x0 - SIMPLEX_CORNER_POS;

    //  pack them into a parallel-friendly arrangement
    vec4 v1234_x = vec4( x0.x, x1.x, x2.x, x3.x );
    vec4 v1234_y = vec4( x0.y, x1.y, x2.y, x3.y );
    vec4 v1234_z = vec4( x0.z, x1.z, x2.z, x3.z );

    // clamp the domain of our grid cell
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    //  generate the random vectors
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    vec4 V1xy_V2xy = mix( Pt.xyxy, Pt.zwzw, vec4( Pi_1.xy, Pi_2.xy ) );
    Pt = vec4( Pt.x, V1xy_V2xy.xz, Pt.z ) * vec4( Pt.y, V1xy_V2xy.yw, Pt.w );
    const vec3 SOMELARGEFLOATS = vec3( 635.298681, 682.357502, 668.926525 );
    const vec3 ZINC = vec3( 48.500388, 65.294118, 63.934599 );
    vec3 lowz_mods = vec3( 1.0 / ( SOMELARGEFLOATS.xyz + Pi.zzz * ZINC.xyz ) );
    vec3 highz_mods = vec3( 1.0 / ( SOMELARGEFLOATS.xyz + Pi_inc1.zzz * ZINC.xyz ) );
    Pi_1 = ( Pi_1.z < 0.5 ) ? lowz_mods : highz_mods;
    Pi_2 = ( Pi_2.z < 0.5 ) ? lowz_mods : highz_mods;
    vec4 hash_0 = fract( Pt * vec4( lowz_mods.x, Pi_1.x, Pi_2.x, highz_mods.x ) ) - 0.49999;
    vec4 hash_1 = fract( Pt * vec4( lowz_mods.y, Pi_1.y, Pi_2.y, highz_mods.y ) ) - 0.49999;
    vec4 hash_2 = fract( Pt * vec4( lowz_mods.z, Pi_1.z, Pi_2.z, highz_mods.z ) ) - 0.49999;

    //  evaluate gradients
    vec4 grad_results = inversesqrt( hash_0 * hash_0 + hash_1 * hash_1 + hash_2 * hash_2 ) * ( hash_0 * v1234_x + hash_1 * v1234_y + hash_2 * v1234_z );

    //  Normalization factor to scale the final result to a strict 1.0->-1.0 range
    //  http://briansharpe.wordpress.com/2012/01/13/simplex-noise/#comment-36
    const float FINAL_NORMALIZATION = 37.837227241611314102871574478976;

    //  evaulate the kernel weights ( use (0.5-x*x)^3 instead of (0.6-x*x)^4 to fix discontinuities )
    vec4 kernel_weights = v1234_x * v1234_x + v1234_y * v1234_y + v1234_z * v1234_z;
    kernel_weights = max(0.5 - kernel_weights, 0.0);
    kernel_weights = kernel_weights*kernel_weights*kernel_weights;

    //  sum with the kernel and return
    return dot( kernel_weights, grad_results ) * FINAL_NORMALIZATION;
}

//! Returns a 3D simplex noise with derivatives in the range [-1,1]
vec4 dnoise( vec3 p )
{
    //  simplex math constants
    const float SKEWFACTOR = 1.0/3.0;
    const float UNSKEWFACTOR = 1.0/6.0;
    const float SIMPLEX_CORNER_POS = 0.5;
    const float SIMPLEX_TETRAHADRON_HEIGHT = 0.70710678118654752440084436210485;    // sqrt( 0.5 )

    //  establish our grid cell.
    p *= SIMPLEX_TETRAHADRON_HEIGHT;    // scale space so we can have an approx feature size of 1.0
    vec3 Pi = floor( p + dot( p, vec3( SKEWFACTOR) ) );

    //  Find the vectors to the corners of our simplex tetrahedron
    vec3 x0 = p - Pi + dot(Pi, vec3( UNSKEWFACTOR ) );
    vec3 g = step(x0.yzx, x0.xyz);
    vec3 l = 1.0 - g;
    vec3 Pi_1 = min( g.xyz, l.zxy );
    vec3 Pi_2 = max( g.xyz, l.zxy );
    vec3 x1 = x0 - Pi_1 + UNSKEWFACTOR;
    vec3 x2 = x0 - Pi_2 + SKEWFACTOR;
    vec3 x3 = x0 - SIMPLEX_CORNER_POS;

    //  pack them into a parallel-friendly arrangement
    vec4 v1234_x = vec4( x0.x, x1.x, x2.x, x3.x );
    vec4 v1234_y = vec4( x0.y, x1.y, x2.y, x3.y );
    vec4 v1234_z = vec4( x0.z, x1.z, x2.z, x3.z );

    // clamp the domain of our grid cell
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    //  generate the random vectors
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    vec4 V1xy_V2xy = mix( Pt.xyxy, Pt.zwzw, vec4( Pi_1.xy, Pi_2.xy ) );
    Pt = vec4( Pt.x, V1xy_V2xy.xz, Pt.z ) * vec4( Pt.y, V1xy_V2xy.yw, Pt.w );
    const vec3 SOMELARGEFLOATS = vec3( 635.298681, 682.357502, 668.926525 );
    const vec3 ZINC = vec3( 48.500388, 65.294118, 63.934599 );
    vec3 lowz_mods = vec3( 1.0 / ( SOMELARGEFLOATS.xyz + Pi.zzz * ZINC.xyz ) );
    vec3 highz_mods = vec3( 1.0 / ( SOMELARGEFLOATS.xyz + Pi_inc1.zzz * ZINC.xyz ) );
    Pi_1 = ( Pi_1.z < 0.5 ) ? lowz_mods : highz_mods;
    Pi_2 = ( Pi_2.z < 0.5 ) ? lowz_mods : highz_mods;
    vec4 hash_0 = fract( Pt * vec4( lowz_mods.x, Pi_1.x, Pi_2.x, highz_mods.x ) ) - 0.49999;
    vec4 hash_1 = fract( Pt * vec4( lowz_mods.y, Pi_1.y, Pi_2.y, highz_mods.y ) ) - 0.49999;
    vec4 hash_2 = fract( Pt * vec4( lowz_mods.z, Pi_1.z, Pi_2.z, highz_mods.z ) ) - 0.49999;

    //  normalize random gradient vectors
    vec4 norm = inversesqrt( hash_0 * hash_0 + hash_1 * hash_1 + hash_2 * hash_2 );
    hash_0 *= norm;
    hash_1 *= norm;
    hash_2 *= norm;

    //  evaluate gradients
    vec4 grad_results = hash_0 * v1234_x + hash_1 * v1234_y + hash_2 * v1234_z;

    //  evaulate the kernel weights ( use (0.5-x*x)^3 instead of (0.6-x*x)^4 to fix discontinuities )
    vec4 m = v1234_x * v1234_x + v1234_y * v1234_y + v1234_z * v1234_z;
    m = max(0.5 - m, 0.0);
    vec4 m2 = m*m;
    vec4 m3 = m*m2;

    //  calc the derivatives
    vec4 temp = -6.0 * m2 * grad_results;
    float xderiv = dot( temp, v1234_x ) + dot( m3, hash_0 );
    float yderiv = dot( temp, v1234_y ) + dot( m3, hash_1 );
    float zderiv = dot( temp, v1234_z ) + dot( m3, hash_2 );

    //  Normalization factor to scale the final result to a strict 1.0->-1.0 range
    //  http://briansharpe.wordpress.com/2012/01/13/simplex-noise/#comment-36
    const float FINAL_NORMALIZATION = 37.837227241611314102871574478976;

    //  sum and return all results as a vec3
    return vec4( dot( m3, grad_results ), xderiv, yderiv, zderiv ) * FINAL_NORMALIZATION;
}

// Modulo 289, optimizes to code without divisions
float mod289(float x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec2 mod289(vec2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec3 mod289(vec3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }

// Permutation polynomial (ring size 289 = 17*17)
float permute(float x) { return mod289(((x*34.0)+1.0)*x); }
vec3 permute(vec3 x) { return mod289(((x*34.0)+1.0)*x); }

// Modulo 7 without a division
vec3 mod7(vec3 x) {  return x - floor(x * (1.0 / 7.0)) * 7.0; }

#if ! defined( NOISE_WORLEY_ASHIMA_IMPL )
//! Returns a 2D worley/cellular noise in the range [0,1]
float worleyNoise( vec2 p )
{
    //  establish our grid cell and unit position
    vec2 Pi = floor(p);
    vec2 Pf = p - Pi;

    //  calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash_x = fract( Pt * ( 1.0 / 951.135664 ) );
    vec4 hash_y = fract( Pt * ( 1.0 / 642.949883 ) );

    //  generate the 4 points
    hash_x = hash_x * 2.0 - 1.0;
    hash_y = hash_y * 2.0 - 1.0;
    const float JITTER_WINDOW = 0.25;   // 0.25 will guarentee no artifacts
    hash_x = ( ( hash_x * hash_x * hash_x ) - sign( hash_x ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y = ( ( hash_y * hash_y * hash_y ) - sign( hash_y ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );

    //  return the closest squared distance
    vec4 dx = Pf.xxxx - hash_x;
    vec4 dy = Pf.yyyy - hash_y;
    vec4 d = dx * dx + dy * dy;
    d.xy = min(d.xy, d.zw);
    return min(d.x, d.y) * ( 1.0 / 1.125 ); // return a value scaled to 0.0->1.0
}
#else

// Cellular noise ("Worley noise") in 2D in GLSL.
// Copyright (c) Stefan Gustavson 2011-04-19. All rights reserved.
// This code is released under the conditions of the MIT license.
// See LICENSE file for details.
// https://github.com/stegu/webgl-noise
// Cellular noise, returning F1 and F2 in a vec2.
// Standard 3x3 search window for good F1 and F2 values
vec2 worleyNoise( vec2 P ) 
{
    const float K = 0.142857142857; // 1/7
    const float Ko = 0.428571428571; // 3/7
    const float jitter = 1.0; // Less gives more regular pattern
    vec2 Pi = mod289(floor(P));
    vec2 Pf = fract(P);
    vec3 oi = vec3(-1.0, 0.0, 1.0);
    vec3 of = vec3(-0.5, 0.5, 1.5);
    vec3 px = permute(Pi.x + oi);
    vec3 p = permute(px.x + Pi.y + oi); // p11, p12, p13
    vec3 ox = fract(p*K) - Ko;
    vec3 oy = mod7(floor(p*K))*K - Ko;
    vec3 dx = Pf.x + 0.5 + jitter*ox;
    vec3 dy = Pf.y - of + jitter*oy;
    vec3 d1 = dx * dx + dy * dy; // d11, d12 and d13, squared
    p = permute(px.y + Pi.y + oi); // p21, p22, p23
    ox = fract(p*K) - Ko;
    oy = mod7(floor(p*K))*K - Ko;
    dx = Pf.x - 0.5 + jitter*ox;
    dy = Pf.y - of + jitter*oy;
    vec3 d2 = dx * dx + dy * dy; // d21, d22 and d23, squared
    p = permute(px.z + Pi.y + oi); // p31, p32, p33
    ox = fract(p*K) - Ko;
    oy = mod7(floor(p*K))*K - Ko;
    dx = Pf.x - 1.5 + jitter*ox;
    dy = Pf.y - of + jitter*oy;
    vec3 d3 = dx * dx + dy * dy; // d31, d32 and d33, squared
    // Sort out the two smallest distances (F1, F2)
    vec3 d1a = min(d1, d2);
    d2 = max(d1, d2); // Swap to keep candidates for F2
    d2 = min(d2, d3); // neither F1 nor F2 are now in d3
    d1 = min(d1a, d2); // F1 is now in d1
    d2 = max(d1a, d2); // Swap to keep candidates for F2
    d1.xy = (d1.x < d1.y) ? d1.xy : d1.yx; // Swap if smaller
    d1.xz = (d1.x < d1.z) ? d1.xz : d1.zx; // F1 is in d1.x
    d1.yz = min(d1.yz, d2.yz); // F2 is now not in d2.yz
    d1.y = min(d1.y, d1.z); // nor in  d1.z
    d1.y = min(d1.y, d2.x); // F2 is in d1.y, we're done.
    return sqrt(d1.xy);
}

#endif

//! Returns a 2D worley/cellular noise with derivatives in the range [0,1]
vec3 dWorleyNoise( vec2 p )
{
    //  establish our grid cell and unit position
    vec2 Pi = floor(p);
    vec2 Pf = p - Pi;

    //  calculate the hash
    vec4 Pt = vec4( Pi.xy, Pi.xy + 1.0 );
    Pt = Pt - floor(Pt * ( 1.0 / 71.0 )) * 71.0;
    Pt += vec2( 26.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    vec4 hash_x = fract( Pt * ( 1.0 / 951.135664 ) );
    vec4 hash_y = fract( Pt * ( 1.0 / 642.949883 ) );

    //  generate the 4 points
    hash_x = hash_x * 2.0 - 1.0;
    hash_y = hash_y * 2.0 - 1.0;
    const float JITTER_WINDOW = 0.25;   // 0.25 will guarentee no artifacts
    hash_x = ( ( hash_x * hash_x * hash_x ) - sign( hash_x ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y = ( ( hash_y * hash_y * hash_y ) - sign( hash_y ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );

    //  return the closest squared distance + derivatives ( thanks to Jonathan Dupuy )
    vec4 dx = Pf.xxxx - hash_x;
    vec4 dy = Pf.yyyy - hash_y;
    vec4 d = dx * dx + dy * dy;
    vec3 t1 = d.x < d.y ? vec3( d.x, dx.x, dy.x ) : vec3( d.y, dx.y, dy.y );
    vec3 t2 = d.z < d.w ? vec3( d.z, dx.z, dy.z ) : vec3( d.w, dx.w, dy.w );
    return ( t1.x < t2.x ? t1 : t2 ) * vec3( 1.0, 2.0, 2.0 ) * ( 1.0 / 1.125 ); // return a value scaled to 0.0->1.0
}

#if ! defined( NOISE_WORLEY_ASHIMA_IMPL )
//! Returns a 3D worley/cellular noise in the range [0,1]
float worleyNoise( vec3 p )
{
    //  establish our grid cell and unit position
    vec3 Pi = floor(p);
    vec3 Pf = p - Pi;

    // clamp the domain
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    // calculate the hash ( over -1.0->1.0 range )
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    const vec3 SOMELARGEFLOATS = vec3( 635.298681, 682.357502, 668.926525 );
    const vec3 ZINC = vec3( 48.500388, 65.294118, 63.934599 );
    vec3 lowz_mod = vec3( 1.0 / ( SOMELARGEFLOATS + Pi.zzz * ZINC ) );
    vec3 highz_mod = vec3( 1.0 / ( SOMELARGEFLOATS + Pi_inc1.zzz * ZINC ) );
    vec4 hash_x0 = fract( Pt * lowz_mod.xxxx ) * 2.0 - 1.0;
    vec4 hash_x1 = fract( Pt * highz_mod.xxxx ) * 2.0 - 1.0;
    vec4 hash_y0 = fract( Pt * lowz_mod.yyyy ) * 2.0 - 1.0;
    vec4 hash_y1 = fract( Pt * highz_mod.yyyy ) * 2.0 - 1.0;
    vec4 hash_z0 = fract( Pt * lowz_mod.zzzz ) * 2.0 - 1.0;
    vec4 hash_z1 = fract( Pt * highz_mod.zzzz ) * 2.0 - 1.0;

    //  generate the 8 point positions
    const float JITTER_WINDOW = 0.166666666;  // 0.166666666 will guarentee no artifacts.
    hash_x0 = ( ( hash_x0 * hash_x0 * hash_x0 ) - sign( hash_x0 ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y0 = ( ( hash_y0 * hash_y0 * hash_y0 ) - sign( hash_y0 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );
    hash_x1 = ( ( hash_x1 * hash_x1 * hash_x1 ) - sign( hash_x1 ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y1 = ( ( hash_y1 * hash_y1 * hash_y1 ) - sign( hash_y1 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );
    hash_z0 = ( ( hash_z0 * hash_z0 * hash_z0 ) - sign( hash_z0 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 0.0, 0.0 );
    hash_z1 = ( ( hash_z1 * hash_z1 * hash_z1 ) - sign( hash_z1 ) ) * JITTER_WINDOW + vec4( 1.0, 1.0, 1.0, 1.0 );

    //  return the closest squared distance
    vec4 dx1 = Pf.xxxx - hash_x0;
    vec4 dy1 = Pf.yyyy - hash_y0;
    vec4 dz1 = Pf.zzzz - hash_z0;
    vec4 dx2 = Pf.xxxx - hash_x1;
    vec4 dy2 = Pf.yyyy - hash_y1;
    vec4 dz2 = Pf.zzzz - hash_z1;
    vec4 d1 = dx1 * dx1 + dy1 * dy1 + dz1 * dz1;
    vec4 d2 = dx2 * dx2 + dy2 * dy2 + dz2 * dz2;
    d1 = min(d1, d2);
    d1.xy = min(d1.xy, d1.wz);
    return min(d1.x, d1.y) * ( 9.0 / 12.0 ); // return a value scaled to 0.0->1.0
}
#else

// Cellular noise ("Worley noise") in 3D in GLSL.
// Copyright (c) Stefan Gustavson 2011-04-19. All rights reserved.
// This code is released under the conditions of the MIT license.
// See LICENSE file for details.
// https://github.com/stegu/webgl-noise
// Cellular noise, returning F1 and F2 in a vec2.
// 3x3x3 search region for good F2 everywhere, but a lot
// slower than the 2x2x2 version.
// The code below is a bit scary even to its author,
// but it has at least half decent performance on a
// modern GPU. In any case, it beats any software
// implementation of Worley noise hands down.

vec2 worleyNoise( vec3 P ) 
{
    const float K = 0.142857142857; // 1/7
    const float Ko = 0.428571428571; // 1/2-K/2
    const float K2 = 0.020408163265306; // 1/(7*7)
    const float Kz = 0.166666666667; // 1/6
    const float Kzo = 0.416666666667; // 1/2-1/6*2
    const float jitter = 1.0; // smaller jitter gives more regular pattern

    vec3 Pi = mod289(floor(P));
    vec3 Pf = fract(P) - 0.5;

    vec3 Pfx = Pf.x + vec3(1.0, 0.0, -1.0);
    vec3 Pfy = Pf.y + vec3(1.0, 0.0, -1.0);
    vec3 Pfz = Pf.z + vec3(1.0, 0.0, -1.0);

    vec3 p = permute(Pi.x + vec3(-1.0, 0.0, 1.0));
    vec3 p1 = permute(p + Pi.y - 1.0);
    vec3 p2 = permute(p + Pi.y);
    vec3 p3 = permute(p + Pi.y + 1.0);

    vec3 p11 = permute(p1 + Pi.z - 1.0);
    vec3 p12 = permute(p1 + Pi.z);
    vec3 p13 = permute(p1 + Pi.z + 1.0);

    vec3 p21 = permute(p2 + Pi.z - 1.0);
    vec3 p22 = permute(p2 + Pi.z);
    vec3 p23 = permute(p2 + Pi.z + 1.0);

    vec3 p31 = permute(p3 + Pi.z - 1.0);
    vec3 p32 = permute(p3 + Pi.z);
    vec3 p33 = permute(p3 + Pi.z + 1.0);

    vec3 ox11 = fract(p11*K) - Ko;
    vec3 oy11 = mod7(floor(p11*K))*K - Ko;
    vec3 oz11 = floor(p11*K2)*Kz - Kzo; // p11 < 289 guaranteed

    vec3 ox12 = fract(p12*K) - Ko;
    vec3 oy12 = mod7(floor(p12*K))*K - Ko;
    vec3 oz12 = floor(p12*K2)*Kz - Kzo;

    vec3 ox13 = fract(p13*K) - Ko;
    vec3 oy13 = mod7(floor(p13*K))*K - Ko;
    vec3 oz13 = floor(p13*K2)*Kz - Kzo;

    vec3 ox21 = fract(p21*K) - Ko;
    vec3 oy21 = mod7(floor(p21*K))*K - Ko;
    vec3 oz21 = floor(p21*K2)*Kz - Kzo;

    vec3 ox22 = fract(p22*K) - Ko;
    vec3 oy22 = mod7(floor(p22*K))*K - Ko;
    vec3 oz22 = floor(p22*K2)*Kz - Kzo;

    vec3 ox23 = fract(p23*K) - Ko;
    vec3 oy23 = mod7(floor(p23*K))*K - Ko;
    vec3 oz23 = floor(p23*K2)*Kz - Kzo;

    vec3 ox31 = fract(p31*K) - Ko;
    vec3 oy31 = mod7(floor(p31*K))*K - Ko;
    vec3 oz31 = floor(p31*K2)*Kz - Kzo;

    vec3 ox32 = fract(p32*K) - Ko;
    vec3 oy32 = mod7(floor(p32*K))*K - Ko;
    vec3 oz32 = floor(p32*K2)*Kz - Kzo;

    vec3 ox33 = fract(p33*K) - Ko;
    vec3 oy33 = mod7(floor(p33*K))*K - Ko;
    vec3 oz33 = floor(p33*K2)*Kz - Kzo;

    vec3 dx11 = Pfx + jitter*ox11;
    vec3 dy11 = Pfy.x + jitter*oy11;
    vec3 dz11 = Pfz.x + jitter*oz11;

    vec3 dx12 = Pfx + jitter*ox12;
    vec3 dy12 = Pfy.x + jitter*oy12;
    vec3 dz12 = Pfz.y + jitter*oz12;

    vec3 dx13 = Pfx + jitter*ox13;
    vec3 dy13 = Pfy.x + jitter*oy13;
    vec3 dz13 = Pfz.z + jitter*oz13;

    vec3 dx21 = Pfx + jitter*ox21;
    vec3 dy21 = Pfy.y + jitter*oy21;
    vec3 dz21 = Pfz.x + jitter*oz21;

    vec3 dx22 = Pfx + jitter*ox22;
    vec3 dy22 = Pfy.y + jitter*oy22;
    vec3 dz22 = Pfz.y + jitter*oz22;

    vec3 dx23 = Pfx + jitter*ox23;
    vec3 dy23 = Pfy.y + jitter*oy23;
    vec3 dz23 = Pfz.z + jitter*oz23;

    vec3 dx31 = Pfx + jitter*ox31;
    vec3 dy31 = Pfy.z + jitter*oy31;
    vec3 dz31 = Pfz.x + jitter*oz31;

    vec3 dx32 = Pfx + jitter*ox32;
    vec3 dy32 = Pfy.z + jitter*oy32;
    vec3 dz32 = Pfz.y + jitter*oz32;

    vec3 dx33 = Pfx + jitter*ox33;
    vec3 dy33 = Pfy.z + jitter*oy33;
    vec3 dz33 = Pfz.z + jitter*oz33;

    vec3 d11 = dx11 * dx11 + dy11 * dy11 + dz11 * dz11;
    vec3 d12 = dx12 * dx12 + dy12 * dy12 + dz12 * dz12;
    vec3 d13 = dx13 * dx13 + dy13 * dy13 + dz13 * dz13;
    vec3 d21 = dx21 * dx21 + dy21 * dy21 + dz21 * dz21;
    vec3 d22 = dx22 * dx22 + dy22 * dy22 + dz22 * dz22;
    vec3 d23 = dx23 * dx23 + dy23 * dy23 + dz23 * dz23;
    vec3 d31 = dx31 * dx31 + dy31 * dy31 + dz31 * dz31;
    vec3 d32 = dx32 * dx32 + dy32 * dy32 + dz32 * dz32;
    vec3 d33 = dx33 * dx33 + dy33 * dy33 + dz33 * dz33;

    // Sort out the two smallest distances (F1, F2)
#if 0
    // Cheat and sort out only F1
    vec3 d1 = min(min(d11,d12), d13);
    vec3 d2 = min(min(d21,d22), d23);
    vec3 d3 = min(min(d31,d32), d33);
    vec3 d = min(min(d1,d2), d3);
    d.x = min(min(d.x,d.y),d.z);
    return vec2(sqrt(d.x)); // F1 duplicated, no F2 computed
#else
    // Do it right and sort out both F1 and F2
    vec3 d1a = min(d11, d12);
    d12 = max(d11, d12);
    d11 = min(d1a, d13); // Smallest now not in d12 or d13
    d13 = max(d1a, d13);
    d12 = min(d12, d13); // 2nd smallest now not in d13
    vec3 d2a = min(d21, d22);
    d22 = max(d21, d22);
    d21 = min(d2a, d23); // Smallest now not in d22 or d23
    d23 = max(d2a, d23);
    d22 = min(d22, d23); // 2nd smallest now not in d23
    vec3 d3a = min(d31, d32);
    d32 = max(d31, d32);
    d31 = min(d3a, d33); // Smallest now not in d32 or d33
    d33 = max(d3a, d33);
    d32 = min(d32, d33); // 2nd smallest now not in d33
    vec3 da = min(d11, d21);
    d21 = max(d11, d21);
    d11 = min(da, d31); // Smallest now in d11
    d31 = max(da, d31); // 2nd smallest now not in d31
    d11.xy = (d11.x < d11.y) ? d11.xy : d11.yx;
    d11.xz = (d11.x < d11.z) ? d11.xz : d11.zx; // d11.x now smallest
    d12 = min(d12, d21); // 2nd smallest now not in d21
    d12 = min(d12, d22); // nor in d22
    d12 = min(d12, d31); // nor in d31
    d12 = min(d12, d32); // nor in d32
    d11.yz = min(d11.yz,d12.xy); // nor in d12.yz
    d11.y = min(d11.y,d12.z); // Only two more to go
    d11.y = min(d11.y,d11.z); // Done! (Phew!)
    return sqrt(d11.xy); // F1, F2
#endif
}
#endif

//! Returns a 3D worley/cellular noise with derivatives in the range [0,1]
vec4 dWorleyNoise( vec3 p )
{
    //  establish our grid cell and unit position
    vec3 Pi = floor(p);
    vec3 Pf = p - Pi;

    // clamp the domain
    Pi.xyz = Pi.xyz - floor(Pi.xyz * ( 1.0 / 69.0 )) * 69.0;
    vec3 Pi_inc1 = step( Pi, vec3( 69.0 - 1.5 ) ) * ( Pi + 1.0 );

    // calculate the hash ( over -1.0->1.0 range )
    vec4 Pt = vec4( Pi.xy, Pi_inc1.xy ) + vec2( 50.0, 161.0 ).xyxy;
    Pt *= Pt;
    Pt = Pt.xzxz * Pt.yyww;
    const vec3 SOMELARGEFLOATS = vec3( 635.298681, 682.357502, 668.926525 );
    const vec3 ZINC = vec3( 48.500388, 65.294118, 63.934599 );
    vec3 lowz_mod = vec3( 1.0 / ( SOMELARGEFLOATS + Pi.zzz * ZINC ) );
    vec3 highz_mod = vec3( 1.0 / ( SOMELARGEFLOATS + Pi_inc1.zzz * ZINC ) );
    vec4 hash_x0 = fract( Pt * lowz_mod.xxxx ) * 2.0 - 1.0;
    vec4 hash_x1 = fract( Pt * highz_mod.xxxx ) * 2.0 - 1.0;
    vec4 hash_y0 = fract( Pt * lowz_mod.yyyy ) * 2.0 - 1.0;
    vec4 hash_y1 = fract( Pt * highz_mod.yyyy ) * 2.0 - 1.0;
    vec4 hash_z0 = fract( Pt * lowz_mod.zzzz ) * 2.0 - 1.0;
    vec4 hash_z1 = fract( Pt * highz_mod.zzzz ) * 2.0 - 1.0;

    //  generate the 8 point positions
    const float JITTER_WINDOW = 0.166666666;  // 0.166666666 will guarentee no artifacts.
    hash_x0 = ( ( hash_x0 * hash_x0 * hash_x0 ) - sign( hash_x0 ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y0 = ( ( hash_y0 * hash_y0 * hash_y0 ) - sign( hash_y0 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );
    hash_x1 = ( ( hash_x1 * hash_x1 * hash_x1 ) - sign( hash_x1 ) ) * JITTER_WINDOW + vec4( 0.0, 1.0, 0.0, 1.0 );
    hash_y1 = ( ( hash_y1 * hash_y1 * hash_y1 ) - sign( hash_y1 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 1.0, 1.0 );
    hash_z0 = ( ( hash_z0 * hash_z0 * hash_z0 ) - sign( hash_z0 ) ) * JITTER_WINDOW + vec4( 0.0, 0.0, 0.0, 0.0 );
    hash_z1 = ( ( hash_z1 * hash_z1 * hash_z1 ) - sign( hash_z1 ) ) * JITTER_WINDOW + vec4( 1.0, 1.0, 1.0, 1.0 );

    //  return the closest squared distance + derivatives ( thanks to Jonathan Dupuy )
    vec4 dx1 = Pf.xxxx - hash_x0;
    vec4 dy1 = Pf.yyyy - hash_y0;
    vec4 dz1 = Pf.zzzz - hash_z0;
    vec4 dx2 = Pf.xxxx - hash_x1;
    vec4 dy2 = Pf.yyyy - hash_y1;
    vec4 dz2 = Pf.zzzz - hash_z1;
    vec4 d1 = dx1 * dx1 + dy1 * dy1 + dz1 * dz1;
    vec4 d2 = dx2 * dx2 + dy2 * dy2 + dz2 * dz2;
    vec4 r1 = d1.x < d1.y ? vec4( d1.x, dx1.x, dy1.x, dz1.x ) : vec4( d1.y, dx1.y, dy1.y, dz1.y );
    vec4 r2 = d1.z < d1.w ? vec4( d1.z, dx1.z, dy1.z, dz1.z ) : vec4( d1.w, dx1.w, dy1.w, dz1.w );
    vec4 r3 = d2.x < d2.y ? vec4( d2.x, dx2.x, dy2.x, dz2.x ) : vec4( d2.y, dx2.y, dy2.y, dz2.y );
    vec4 r4 = d2.z < d2.w ? vec4( d2.z, dx2.z, dy2.z, dz2.z ) : vec4( d2.w, dx2.w, dy2.w, dz2.w );
    vec4 t1 = r1.x < r2.x ? r1 : r2;
    vec4 t2 = r3.x < r4.x ? r3 : r4;
    return ( t1.x < t2.x ? t1 : t2 ) * vec4( 1.0, vec3( 2.0 ) ) * ( 9.0 / 12.0 ); // return a value scaled to 0.0->1.0
}


// Adapted from Ashima Arts Simplex noise
// https://github.com/ashima/webgl-noise/
// https://github.com/stegu/webgl-noise/

// Copyright (C) 2011 by Ashima Arts (Simplex noise)
// Copyright (C) 2011-2016 by Stefan Gustavson (Classic noise and others)

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

// Hashed 2-D gradients with an extra rotation.
// (The constant 0.0243902439 is 1/41)
vec2 rgrad2(vec2 p, float rot) {
#if 0
// Map from a line to a diamond such that a shift maps to a rotation.
  float u = permute(permute(p.x) + p.y) * 0.0243902439 + rot; // Rotate by shift
  u = 4.0 * fract(u) - 2.0;
  // (This vector could be normalized, exactly or approximately.)
  return vec2(abs(u)-1.0, abs(abs(u+1.0)-2.0)-1.0);
#else
// For more isotropic gradients, sin/cos can be used instead.
  float u = permute(permute(p.x) + p.y) * 0.0243902439 + rot; // Rotate by shift
  u = fract(u) * 6.28318530718; // 2*pi
  return vec2(cos(u), sin(u));
#endif
}

//! Returns a 2D simplex noise with rotating gradients in the range [-1,-1]
float flowNoise( vec2 pos, float rot ) 
{
  // Offset y slightly to hide some rare artifacts
  pos.y += 0.001;
  // Skew to hexagonal grid
  vec2 uv = vec2(pos.x + pos.y*0.5, pos.y);
  
  vec2 i0 = floor(uv);
  vec2 f0 = fract(uv);
  // Traversal order
  vec2 i1 = (f0.x > f0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);

  // Unskewed grid points in (x,y) space
  vec2 p0 = vec2(i0.x - i0.y * 0.5, i0.y);
  vec2 p1 = vec2(p0.x + i1.x - i1.y * 0.5, p0.y + i1.y);
  vec2 p2 = vec2(p0.x + 0.5, p0.y + 1.0);

  // Integer grid point indices in (u,v) space
  i1 = i0 + i1;
  vec2 i2 = i0 + vec2(1.0, 1.0);

  // Vectors in unskewed (x,y) coordinates from
  // each of the simplex corners to the evaluation point
  vec2 d0 = pos - p0;
  vec2 d1 = pos - p1;
  vec2 d2 = pos - p2;

  // Wrap i0, i1 and i2 to the desired period before gradient hashing:
  // wrap points in (x,y), map to (u,v)
  vec3 x = vec3(p0.x, p1.x, p2.x);
  vec3 y = vec3(p0.y, p1.y, p2.y);
  vec3 iuw = x + 0.5 * y;
  vec3 ivw = y;
  
  // Avoid precision issues in permutation
  iuw = mod289(iuw);
  ivw = mod289(ivw);

  // Create gradients from indices
  vec2 g0 = rgrad2(vec2(iuw.x, ivw.x), rot);
  vec2 g1 = rgrad2(vec2(iuw.y, ivw.y), rot);
  vec2 g2 = rgrad2(vec2(iuw.z, ivw.z), rot);

  // Gradients dot vectors to corresponding corners
  // (The derivatives of this are simply the gradients)
  vec3 w = vec3(dot(g0, d0), dot(g1, d1), dot(g2, d2));
  
  // Radial weights from corners
  // 0.8 is the square of 2/sqrt(5), the distance from
  // a grid point to the nearest simplex boundary
  vec3 t = 0.8 - vec3(dot(d0, d0), dot(d1, d1), dot(d2, d2));

  // Set influence of each surflet to zero outside radius sqrt(0.8)
  t = max(t, 0.0);

  // Fourth power of t
  vec3 t2 = t * t;
  vec3 t4 = t2 * t2;
  
  // Final noise value is:
  // sum of ((radial weights) times (gradient dot vector from corner))
  float n = dot(t4, w);
  
  // Rescale to cover the range [-1,1] reasonably well
  return 11.0*n;
}

//! Returns a 2D simplex noise with rotating gradients and derivatives in the range [-1,-1]
vec3 dFlowNoise( vec2 pos, float rot ) 
{
  // Offset y slightly to hide some rare artifacts
  pos.y += 0.001;
  // Skew to hexagonal grid
  vec2 uv = vec2(pos.x + pos.y*0.5, pos.y);
  
  vec2 i0 = floor(uv);
  vec2 f0 = fract(uv);
  // Traversal order
  vec2 i1 = (f0.x > f0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);

  // Unskewed grid points in (x,y) space
  vec2 p0 = vec2(i0.x - i0.y * 0.5, i0.y);
  vec2 p1 = vec2(p0.x + i1.x - i1.y * 0.5, p0.y + i1.y);
  vec2 p2 = vec2(p0.x + 0.5, p0.y + 1.0);

  // Integer grid point indices in (u,v) space
  i1 = i0 + i1;
  vec2 i2 = i0 + vec2(1.0, 1.0);

  // Vectors in unskewed (x,y) coordinates from
  // each of the simplex corners to the evaluation point
  vec2 d0 = pos - p0;
  vec2 d1 = pos - p1;
  vec2 d2 = pos - p2;

  vec3 x = vec3(p0.x, p1.x, p2.x);
  vec3 y = vec3(p0.y, p1.y, p2.y);
  vec3 iuw = x + 0.5 * y;
  vec3 ivw = y;
  
  // Avoid precision issues in permutation
  iuw = mod289(iuw);
  ivw = mod289(ivw);

  // Create gradients from indices
  vec2 g0 = rgrad2(vec2(iuw.x, ivw.x), rot);
  vec2 g1 = rgrad2(vec2(iuw.y, ivw.y), rot);
  vec2 g2 = rgrad2(vec2(iuw.z, ivw.z), rot);

  // Gradients dot vectors to corresponding corners
  // (The derivatives of this are simply the gradients)
  vec3 w = vec3(dot(g0, d0), dot(g1, d1), dot(g2, d2));
  
  // Radial weights from corners
  // 0.8 is the square of 2/sqrt(5), the distance from
  // a grid point to the nearest simplex boundary
  vec3 t = 0.8 - vec3(dot(d0, d0), dot(d1, d1), dot(d2, d2));

  // Partial derivatives for analytical gradient computation
  vec3 dtdx = -2.0 * vec3(d0.x, d1.x, d2.x);
  vec3 dtdy = -2.0 * vec3(d0.y, d1.y, d2.y);

  // Set influence of each surflet to zero outside radius sqrt(0.8)
  if (t.x < 0.0) {
    dtdx.x = 0.0;
    dtdy.x = 0.0;
  t.x = 0.0;
  }
  if (t.y < 0.0) {
    dtdx.y = 0.0;
    dtdy.y = 0.0;
  t.y = 0.0;
  }
  if (t.z < 0.0) {
    dtdx.z = 0.0;
    dtdy.z = 0.0;
  t.z = 0.0;
  }

  // Fourth power of t (and third power for derivative)
  vec3 t2 = t * t;
  vec3 t4 = t2 * t2;
  vec3 t3 = t2 * t;
  
  // Final noise value is:
  // sum of ((radial weights) times (gradient dot vector from corner))
  float n = dot(t4, w);
  
  // Final analytical derivative (gradient of a sum of scalar products)
  vec2 dt0 = vec2(dtdx.x, dtdy.x) * 4.0 * t3.x;
  vec2 dn0 = t4.x * g0 + dt0 * w.x;
  vec2 dt1 = vec2(dtdx.y, dtdy.y) * 4.0 * t3.y;
  vec2 dn1 = t4.y * g1 + dt1 * w.y;
  vec2 dt2 = vec2(dtdx.z, dtdy.z) * 4.0 * t3.z;
  vec2 dn2 = t4.z * g2 + dt2 * w.z;

  return 11.0*vec3(n, dn0 + dn1 + dn2);
}

// Adapted from https://github.com/simongeilfus/SimplexNoise/

// Copyright (c) 2016, Simon Geilfus, All rights reserved.
// Code adapted from Stefan Gustavson Simplex Noise Public Domain implementation
// Curl noise adapted from Robert Bridson papers
// This code also includes variation of noise sums by Iñigo Quilez

// Redistribution and use in source and binary forms, with or without modification, are permitted provided that
// the following conditions are met:

// * Redistributions of source code must retain the above copyright notice, this list of conditions and
//  the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
//  the following disclaimer in the documentation and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.


//! Returns a 2D Billowed Noise in the range [0,1]
float billowedNoise( vec2 p )
{
#ifndef NOISE_TEXTURE_BASED
    return abs( noise( p ) );
#else
    return abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 3D Billowed Noise in the range [0,1]
float billowedNoise( vec3 p )
{
#ifndef NOISE_TEXTURE_BASED
    return abs( noise( p ) );
#else
    return abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 2D Billowed Noise with Derivatives in the range [0,1]
vec3 dBillowedNoise( vec2 p )
{
  return abs( dnoise( p ) );
}

//! Returns a 3D Billowed Noise with Derivatives in the range [0,1]
vec4 dBillowedNoise( vec3 p )
{
  return abs( dnoise( p ) );
}

//! Returns a 2D Ridged Noise in the range [0,1]
float ridgedNoise( vec2 p )
{
#ifndef NOISE_TEXTURE_BASED
    return 1.0f - abs( noise( p ) );
#else
    return 1.0f - abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 2D Ridged Noise in the range [0,1]
float ridgedNoise( vec2 p, float ridge )
{
#ifndef NOISE_TEXTURE_BASED
    return ridge - abs( noise( p ) );
#else
    return ridge - abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 3D Ridged Noise in the range [0,1]
float ridgedNoise( vec3 p )
{
#ifndef NOISE_TEXTURE_BASED
    return 1.0f - abs( noise( p ) );
#else
    return 1.0f - abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 3D Ridged Noise in the range [0,1]
float ridgedNoise( vec3 p, float ridge )
{
#ifndef NOISE_TEXTURE_BASED
    return ridge - abs( noise( p ) );
#else
    return ridge - abs( vnoise( p * 2.0f ) * 2.0f - 1.0f );
#endif
}

//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
vec3 dRidgedNoise( vec2 p )
{
  return vec3( 1.0f ) - abs( dnoise( p ) );
}

//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
vec3 dRidgedNoise( vec2 p, float ridge )
{
  return vec3( ridge ) - abs( dnoise( p ) );
}

//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
vec4 dRidgedNoise( vec3 p )
{
  return vec4( 1.0f ) - abs( dnoise( p ) );
}

//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
vec4 dRidgedNoise( vec3 p, float ridge )
{
  return vec4( ridge ) - abs( dnoise( p ) );
}

//! Returns a 2D Curl of a Simplex Noise in the range [-1,1]
vec2 curlNoise( vec2 v )
{
  vec3 derivative = dnoise( v );
  return vec2( derivative.z, -derivative.y );
}

//! Returns a 2D Curl of a Simplex Flow Noise in the range [-1,1]
vec2 curlNoise( vec2 v, float t )
{
  vec3 derivative = dFlowNoise( v, t );
  return vec2( derivative.z, -derivative.y );
}

//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec2 p )
{
  return fBm( p, 4, 2.0f, 0.5f );
}
//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec2 p, int octaves, float lacunarity, float gain )
{
  float sum   = 0.0f;
#ifndef NOISE_TEXTURE_BASED  
  float freq  = 1.0f;
#else  
  float freq  = 2.0f;
#endif
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
#ifndef NOISE_TEXTURE_BASED
    float n     = noise( p * freq );
#else
    float n     = vnoise( p * freq ) * 2.0f - 1.0f;
#endif
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec3 p )
{
  return fBm( p, 4, 2.0f, 0.5f );
}
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
float fBm( vec3 p, int octaves, float lacunarity, float gain )
{
  float sum   = 0.0f;
#ifndef NOISE_TEXTURE_BASED  
  float freq  = 1.0f;
#else  
  float freq  = 2.0f;
#endif
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
#ifndef NOISE_TEXTURE_BASED
    float n     = noise( p * freq );
#else
    float n     = vnoise( p * freq ) * 2.0f - 1.0f;
#endif
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dfBm( vec2 p )
{
  return dfBm( p, 4, 2.0f, 0.5f );
}
//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dfBm( vec2 p, int octaves, float lacunarity, float gain )
{
  vec3 sum   = vec3( 0.0f );
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    vec3 n     = dnoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dfBm( vec3 p )
{
  return dfBm( p, 4, 2.0f, 0.5f );
}
//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dfBm( vec3 p, int octaves, float lacunarity, float gain )
{
  vec4 sum   = vec4( 0.0f );
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    vec4 n     = dnoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}


//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec2 p )
{
  return billowyTurbulence( p, 4, 2.0f, 0.5f );
}
//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec2 p, int octaves, float lacunarity, float gain )
{
  float sum   = 0.0f;
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    float n     = billowedNoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec3 p )
{
  return billowyTurbulence( p, 4, 2.0f, 0.5f );
}
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
float billowyTurbulence( vec3 p, int octaves, float lacunarity, float gain )
{
  float sum   = 0.0f;
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    float n     = billowedNoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec3 dBillowyTurbulence( vec2 p )
{
  return dBillowyTurbulence( p, 4, 2.0f, 0.5f );
}
//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec3 dBillowyTurbulence( vec2 p, int octaves, float lacunarity, float gain )
{
  vec3 sum   = vec3( 0.0f );
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    vec3 n     = dBillowedNoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec4 dBillowyTurbulence( vec3 p )
{
  return dBillowyTurbulence( p, 4, 2.0f, 0.5f );
}
//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
vec4 dBillowyTurbulence( vec3 p, int octaves, float lacunarity, float gain )
{
  vec4 sum   = vec4( 0.0f );
  float freq  = 1.0f;
  float amp   = 0.5f;
  
  for( int i = 0; i < octaves; i++ ){
    vec4 n     = dBillowedNoise( p * freq );
    sum        += n*amp;
    freq       *= lacunarity;
    amp        *= gain;
  }
  
  return sum;
}

//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p )
{
  return ridgedMF( p, 1.0f, 4, 2.0f, 0.5f );
}
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p, float ridgeOffset )
{
  return ridgedMF( p, ridgeOffset, 4, 2.0f, 0.5f );
}
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain )
{
  float sum = 0;
  float freq  = 1.0;
  float amp = 0.5;
  float prev  = 1.0;
  
  for( int i = 0; i < octaves; i++ ){
    float n = ridgedNoise( p * freq, ridgeOffset );
    n     *= n;
    sum   += n*amp*prev;
    prev  = n;
    freq  *= lacunarity;
    amp   *= gain;
  }
  return sum;
}

//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dRidgedMF( vec2 p )
{
  return dRidgedMF( p, 1.0f, 4, 2.0f, 0.5f );
}
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dRidgedMF( vec2 p, float ridgeOffset )
{
  return dRidgedMF( p, ridgeOffset, 4, 2.0f, 0.5f );
}
//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec3 dRidgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain )
{
  vec3 sum = vec3( 0.0f );
  float freq  = 1.0;
  float amp = 0.5;
  vec3 prev  = vec3( 1.0f );
  
  for( int i = 0; i < octaves; i++ ){
    vec3 n = ridgeOffset - abs( dnoise( p * freq ) );
    n     *= n;
    sum   += n*amp*prev;
    prev  = n;
    freq  *= lacunarity;
    amp   *= gain;
  }
  return sum;
}


//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p )
{
  return ridgedMF( p, 1.0f, 4, 2.0f, 0.5f );
}
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p, float ridgeOffset )
{
  return ridgedMF( p, ridgeOffset, 4, 2.0f, 0.5f );
}
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
float ridgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain )
{
  float sum = 0;
  float freq  = 1.0;
  float amp = 0.5;
  float prev  = 1.0;
  
  for( int i = 0; i < octaves; i++ ){
    float n = ridgedNoise( p * freq, ridgeOffset );
    n     *= n;
    sum   += n*amp*prev;
    prev  = n;
    freq  *= lacunarity;
    amp   *= gain;
  }
  return sum;
}
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p )
{
  return dRidgedMF( p, 1.0f, 4, 2.0f, 0.5f );
}
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p, float ridgeOffset )
{
  return dRidgedMF( p, ridgeOffset, 4, 2.0f, 0.5f );
}
//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
vec4 dRidgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain )
{
  vec4 sum = vec4( 0.0f );
  float freq  = 1.0;
  float amp = 0.5;
  vec4 prev  = vec4( 1.0f );
  
  for( int i = 0; i < octaves; i++ ){
    vec4 n = ridgeOffset - abs( dnoise( p * freq ) );
    n     *= n;
    sum   += n*amp*prev;
    prev  = n;
    freq  *= lacunarity;
    amp   *= gain;
  }
  return sum;
}


/*
// IQ cos noise
// https://www.shadertoy.com/view/MtsSRf
// Created by Beautypi/2015
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0

float cosNoise( in vec2 p )
{
    return 0.5*( sin(p.x) + sin(p.y) );
}
*/

/* 
// IQ Simplex 2D
// https://www.shadertoy.com/view/Msf3WH
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Simplex Noise (http://en.wikipedia.org/wiki/Simplex_noise), a type of gradient noise
// that uses N+1 vertices for random gradient interpolation instead of 2^N as in regular
// latice based Gradient Noise.

vec2 hash( vec2 p )
{
    p = vec2( dot(p,vec2(127.1,311.7)),
              dot(p,vec2(269.5,183.3)) );

    return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float noise( in vec2 p )
{
    const float K1 = 0.366025404; // (sqrt(3)-1)/2;
    const float K2 = 0.211324865; // (3-sqrt(3))/6;

    vec2 i = floor( p + (p.x+p.y)*K1 );
    
    vec2 a = p - i + (i.x+i.y)*K2;
    vec2 o = (a.x>a.y) ? vec2(1.0,0.0) : vec2(0.0,1.0); //vec2 of = 0.5 + 0.5*vec2(sign(a.x-a.y), sign(a.y-a.x));
    vec2 b = a - o + K2;
    vec2 c = a - 1.0 + 2.0*K2;

    vec3 h = max( 0.5-vec3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );

    vec3 n = h*h*h*h*vec3( dot(a,hash(i+0.0)), dot(b,hash(i+o)), dot(c,hash(i+1.0)));

    return dot( n, vec3(70.0) );
    
}

/* IQ Value Noise 2D
// https://www.shadertoy.com/view/lsf3WH
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Value Noise (http://en.wikipedia.org/wiki/Value_noise), not to be confused with Perlin's
// Noise, is probably the simplest way to generate noise (a random smooth signal with 
// mostly all its energy in the low frequencies) suitable for procedural texturing/shading,
// modeling and animation.
//
// It produces lowe quality noise than Gradient Noise (https://www.shadertoy.com/view/XdXGW8)
// but it is slightly faster to compute. When used in a fractal construction, the blockyness
// of Value Noise gets qcuikly hidden, making it a very popular alternative to Gradient Noise.
//
// The princpiple is to create a virtual grid/latice all over the plane, and assign one
// random value to every vertex in the grid. When querying/requesting a noise value at
// an arbitrary point in the plane, the grid cell in which the query is performed is
// determined (line 30), the four vertices of the grid are determined and their random
// value fetched (lines 35 to 38) and then bilinearly interpolated (lines 35 to 38 again)
// with a smooth interpolant (line 31 and 33).


float hash( vec2 p )
{
    float h = dot(p,vec2(127.1,311.7));
    
    return -1.0 + 2.0*fract(sin(h)*43758.5453123);
}

float noise( in vec2 p )
{
    vec2 i = floor( p );
    vec2 f = fract( p );
    
    vec2 u = f*f*(3.0-2.0*f);

    return mix( mix( hash( i + vec2(0.0,0.0) ), 
                     hash( i + vec2(1.0,0.0) ), u.x),
                mix( hash( i + vec2(0.0,1.0) ), 
                     hash( i + vec2(1.0,1.0) ), u.x), u.y);
}

// ----------------------------------------------- */

/*
// IQ Value Noise 2D with derivatives
// https://www.shadertoy.com/view/MdX3Rr
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

#define SC (250.0)

// value noise, and its analytical derivatives
vec3 noised( in vec2 x )
{
    vec2 p = floor(x);
    vec2 f = fract(x);
    vec2 u = f*f*(3.0-2.0*f);
    float a = texture2D(iChannel0,(p+vec2(0.5,0.5))/256.0,-100.0).x;
    float b = texture2D(iChannel0,(p+vec2(1.5,0.5))/256.0,-100.0).x;
    float c = texture2D(iChannel0,(p+vec2(0.5,1.5))/256.0,-100.0).x;
    float d = texture2D(iChannel0,(p+vec2(1.5,1.5))/256.0,-100.0).x;
    return vec3(a+(b-a)*u.x+(c-a)*u.y+(a-b-c+d)*u.x*u.y,
                6.0*f*(1.0-f)*(vec2(b-a,c-a)+(a-b-c+d)*u.yx));
} 
*/

/*
// IQ Value Noise 3D
// https://www.shadertoy.com/view/4sfGzS#
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


// Fast 3D (value) noise by using two cubic-smooth bilinear interpolations in a LUT, 
// which is much faster than its hash based (purely procedural) counterpart.
//
// Note that instead of fetching from a grey scale texture twice at an offset of (37,17)
// pixels, the green channel of the texture is a copy of the red channel offset that amount
// (thx Dave Hoskins for the suggestion to try this)

//#define USE_PROCEDURAL

//===============================================================================================
//===============================================================================================
//===============================================================================================

#ifdef USE_PROCEDURAL
float hash( float n ) { return fract(sin(n)*753.5453123); }
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    
    float n = p.x + p.y*157.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+157.0), hash(n+158.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}
#else
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    
    vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;
    vec2 rg = texture2D( iChannel0, (uv+0.5)/256.0, -100.0 ).yx;
    return mix( rg.x, rg.y, f.z );
}
#endif

*/

/* 
// IQ Gradient Noise 2D
// https://www.shadertoy.com/view/XdXGW8
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Gradient Noise (http://en.wikipedia.org/wiki/Gradient_noise), not to be confused with
// Value Noise, and neither with Perlin's Noise (which is one form of Gradient Noise)
// is probably the most convenient way to generate noise (a random smooth signal with 
// mostly all its energy in the low frequencies) suitable for procedural texturing/shading,
// modeling and animation.
//
// It produces smoother and higher quality than Value Noise, but it's of course slighty more
// expensive.
//
// The princpiple is to create a virtual grid/latice all over the plane, and assign one
// random vector to every vertex in the grid. When querying/requesting a noise value at
// an arbitrary point in the plane, the grid cell in which the query is performed is
// determined (line 32), the four vertices of the grid are determined and their random
// vectors fetched (lines 37 to 40). Then, the position of the current point under 
// evaluation relative to each vertex is doted (projected) with that vertex' random
// vector, and the result is bilinearly interpolated (lines 37 to 40 again) with a 
// smooth interpolant (line 33 and 35).

vec2 hash( vec2 p )
{
    p = vec2( dot(p,vec2(127.1,311.7)),
              dot(p,vec2(269.5,183.3)) );

    return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float noise( in vec2 p )
{
    vec2 i = floor( p );
    vec2 f = fract( p );
    
    vec2 u = f*f*(3.0-2.0*f);

    return mix( mix( dot( hash( i + vec2(0.0,0.0) ), f - vec2(0.0,0.0) ), 
                     dot( hash( i + vec2(1.0,0.0) ), f - vec2(1.0,0.0) ), u.x),
                mix( dot( hash( i + vec2(0.0,1.0) ), f - vec2(0.0,1.0) ), 
                     dot( hash( i + vec2(1.0,1.0) ), f - vec2(1.0,1.0) ), u.x), u.y);
}
*/