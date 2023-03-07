

//#include "MadNoise.glsl"

out float vTest;
out vec3 vNoises[32];
void materialVsFunc(vec2 posInUvs, vec2 posInSurface) {
    vTest = abs( cos( xtime ) ) * 0.1;
}
