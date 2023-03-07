#include "MadNoise.glsl"

//out float vTest[4];

flat out vec3 vNoises[16];

void materialVsFunc( vec2 posInUvs, vec2 posInSurface ) {
	for( int i = 0; i < 16; i++ ) {
        float sound = texture( spectrum_16, vec2( float( i ) / 16.0f, 0.0 ) ).r;
    	vNoises[i] = vec3( curlNoise( vec2( i * 0.235678 + mat_time * 0.025 ), mat_time * 0.125 ) * 0.08,
    						vnoise( vec2( mat_time, i * 3.5678 ) ) * sound * sound * 3.0 );
    }
}
