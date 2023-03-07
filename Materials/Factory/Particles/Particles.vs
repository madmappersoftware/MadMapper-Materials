#include "MadNoise.glsl"

#ifdef SURFACE_IS_surface3D
	// With Intel Iris Pro, we get a switch to CPU renderer if using 16 particles - too many uniforms ?
	#define NUM_PARTICLES 8
#else
	#define NUM_PARTICLES 16
#endif

flat out vec3 vParticles[NUM_PARTICLES];

void materialVsFunc( vec2 uv ) {
	const float invFftSize = 1.0f / float( NUM_PARTICLES );
	for( int i = 0; i < NUM_PARTICLES; i++ ) {
		// update position
    	vParticles[i].xy = vec2( curlNoise( vec2( i * 0.235678 + animation_time * 0.025 ), animation_time * 0.125 ) * 0.08 );

    	// update size
		float unsignedNoise = vnoise( 10.0 * vParticles[i].xy + vec2( animation_time + i * 3.5678 ) );
        float soundA = bassStrokeWidth * texture( spectrum_16, vec2( float(i) * invFftSize, 0.0 ) ).r;        
		vParticles[i].z	= unsignedNoise * soundA * soundA * 3.0 + unsignedNoise * 0.1;
    }
}
