#include "MadNoise.glsl"

// With Intel Iris Pro, we get a switch to CPU renderer if using 16 particles - reaching uniform maximum number if using on a Surface 3D for instance
#define NUM_ARCS 8

flat out vec3 vNoises[NUM_ARCS];

void materialVsFunc( vec2 uv ) {
    for( int i = 0; i < NUM_ARCS; i++ ) {
        vNoises[i] = vec3( 
            noise( vec3( i - 67.891, i + 12.324, ytime ) ),
            noise( vec3( i - 93.643, i + 98.034, xtime ) ),
            noise( vec3( i - 71.321, i + 543.23, ytime ) ) * 0.5 + 0.5
            );
    }
}
