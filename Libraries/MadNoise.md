####*```MadNoise.glsl```*
#### Noise Library

MadNoise is a small library that implements a set of 2D & 3D noises in GLSL.
While not really documented for now, here are the different functions availble in any shader that includes "MadNoise.glsl"
Note that in MadMapper, any shader can include the file by adding
```c++
  #include "MadNoise.glsl"
```

Library declarations:

```c++
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
```
