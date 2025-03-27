/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "tdhooper. Adapted by Jason Beyers.",
    "DESCRIPTION": "Rotating torus. From https://www.shadertoy.com/view/wsfGDS",
    "VSN": "1.0",
    "INPUTS": [
		{
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 50.0,
            "DEFAULT": 10.0
        },
        {
            "Label": "Focal",
            "NAME": "mat_focal_length",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 50.0,
            "DEFAULT": 10.0
        },
		{
            "LABEL": "BPM Sync",
            "NAME": "mat_bpmsync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
		{
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
		{
            "Label": "Offset",
            "NAME": "bpm_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Strob",
            "NAME": "bpm_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

      ],
	 "GENERATORS": [
        {
            "NAME": "mat_animation_time",
            "TYPE": "time_base",
            "PARAMS":
                {
                    "speed": "mat_speed",
                    "speed_curve":2,
                    "strob" : "bpm_strob",
                    "bpm_sync": "mat_bpm_sync", "reverse": "mat_reverse", "link_speed_to_global_bpm":true
                }
        },
    ]
}*/


float adjusted_time(float m_time, float m_decay, float m_release)
{

    float adj_time;

    if (m_time < m_decay) {
        adj_time = 1;
    } else {
        // get back a value from 0-1 (from end of decay to 1 - end of beat)
        adj_time = (m_time - m_decay) * 1 / (1 - m_decay);
        if (m_time < m_release) {
            adj_time = 1 - adj_time * 1 / m_release;
        } else {
            adj_time = 0;
        }
    }

    return adj_time;
}

vec3 iResolution = vec3(RENDERSIZE, 1.);


/*

	Clifford Torus Rotation
	-----------------------

	Getting a good distance for this 4D stereographic projection was
	tricky, see the notes in 'Main SDF', or just toggle DEBUG below to
	see what's going on.

	See also:

	* Animation by Jason Hise https://www.youtube.com/watch?v=1_pzjvVixL0
	* Clifford Torus by mia https://www.shadertoy.com/view/3ss3z4
	* https://en.wikipedia.org/wiki/Clifford_torus
	* http://virtualmathmuseum.org/Surface/clifford_torus/clifford_torus.html

*/

// #define DEBUG

// --------------------------------------------------------
// HG_SDF
// https://www.shadertoy.com/view/Xs3GRB
// --------------------------------------------------------

#include "MadCommon.glsl"

// #define PI 3.14159265359

void pR(inout vec2 p, float a) {
    p = cos(a)*p + sin(a)*vec2(p.y, -p.x);
}

vec2 pMod2(inout vec2 p, vec2 size) {
    vec2 c = floor((p + size*0.5)/size);
    p = mod(p + size*0.5,size) - size*0.5;
    return c;
}

float smax(float a, float b, float r) {
    vec2 u = max(vec2(r + a,r + b), vec2(0));
    return min(-r, max (a, b)) + length(u);
}

float fTorus(vec3 p, float smallRadius, float largeRadius) {
    return length(vec2(length(p.xz) - largeRadius, p.y)) - smallRadius;
}


// --------------------------------------------------------
// Spectrum colour palette
// IQ https://www.shadertoy.com/view/ll2GD3
// --------------------------------------------------------

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d ) {
    return a + b*cos( 6.28318*(c*t+d) );
}

vec3 spectrum(float n) {
    return pal( n, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67) );
}


// --------------------------------------------------------
// Main SDF
// --------------------------------------------------------

// Inverse stereographic projection of p,
// p4 lies onto the unit 3-sphere centered at 0.
// - Syntopia https://www.shadertoy.com/view/XdfGW4
vec4 inverseStereographic(vec3 p) {
    float r = length(p);
    vec4 p4 = vec4(2.*p,1.-r*r)*1./(1.+r*r);
    return p4;
}

float fTorus(vec4 p4, out vec2 uv) {

    // Torus distance
    float d = length(p4.xy) / length(p4.zw) - 1.;

    if (d > 0.) {
        // The distance outside the torus gets exponentially large
        // because of the stereographic projection. So use the inside
        // of an inverted torus for the outside distance.
        d = 1. - length(p4.zw) / length(p4.xy);
    }

    // Because of the projection, distances aren't lipschitz continuous,
    // so scale down the distance at the most warped point - the inside
    // edge of the torus such that it is 1:1 with the domain.
    d /= PI;

    // UV coordinates over the surface, from 0 - 1
    uv = (vec2(
        atan(p4.y, p4.x),
        atan(p4.z, p4.w)
    ) / PI) * .5 + .5;

    return d;
}

// Distances get warped by the stereographic projection, this applies
// some hacky adjustments which makes them lipschitz continuous.

// I don't really understand why, and the numbers have been hand
// picked by comparing our 4D torus SDF to a usual 3D torus of the
// same size, see DEBUG.

// vec3 p
//   Original 3D domain, centered with the stereographic projection;
//   basically it should not be scaled or translated.

// vec3 d
//   SDF to fix, this should be applied after the last step of
//   modelling on the torus.

// vec3 threshold
//   The fix causes a blob artefact at the origin, so return the
//   original SDF when below this distance to the surface. Smaller
//   values result in a faster ray hit, but can cause more artefacts.

float fixDistance(vec3 p, float d, float threshold) {
    d *= PI;

    float od = d;
    float sn = sign(d);

    d = abs(d);
    d = d * dot(p, p) / 4.2;
    if (d > 1.) {
        d = pow(d, .5);
        d = (d - 1.) * 1.8 + 1.;
    }
    d *= sn;

    if (abs(d) < threshold) {
        d = od / PI;
    }

    return d;
}


vec2 modelUv;
bool hitDebugTorus = false;

float map(vec3 p, float time) {

    #ifdef DEBUG
        if (p.x < 0.) {
            hitDebugTorus = true;
            return abs(fTorus(p.xzy, 1.002, 1.4163));
        }
    #endif

    vec4 p4 = inverseStereographic(p);

    // The inside-out rotation puts the torus at a different
    // orientation, so rotate to point it at back in the same
    // direction
    pR(p4.zy, time * -PI / 2.);

    // Rotate in 4D, turning the torus inside-out
    pR(p4.xw, time * -PI / 2.);

    vec2 uv;
    float d = fTorus(p4, uv);
    modelUv = uv;

    #ifdef DEBUG
        d = abs(d);
        d = fixDistance(p, d, 1.);
        return d;
    #endif

    // Recreate domain to be wrapped around the torus surface
    // xy = surface / face, z = depth / distance
    vec3 pp = p;
    float uvScale = 2.25; // Magic number that makes xy distances the same scale as z distances
    p = vec3(uv * uvScale, d);

    // Draw some repeated circles

    float n = 10.;
    float repeat = uvScale / n;

    p.xy += repeat / 2.;
    pMod2(p.xy, vec2(repeat));

    d = length(p.xy) - repeat * .4;
    d = smax(d, abs(p.z) - .013, .01);

    d = fixDistance(pp, d, .01);

    return d;
}

bool hitDebugPlane = false;

float mapDebug(vec3 p, float time) {
    float d = map(p, time);
    #ifndef DEBUG
        return d;
    #endif
    float plane = min(abs(p.z), abs(p.y));
    hitDebugPlane = plane < abs(d);
    return d;
    //return hitDebugPlane ? plane : d;
}


// --------------------------------------------------------
// Rendering
// --------------------------------------------------------

vec3 calcNormal(vec3 p, float time) {
  vec3 eps = vec3(.0001,0,0);
  vec3 n = vec3(
    map(p + eps.xyy, time) - map(p - eps.xyy, time),
    map(p + eps.yxy, time) - map(p - eps.yxy, time),
    map(p + eps.yyx, time) - map(p - eps.yyx, time)
  );
  return normalize(n);
}

mat3 calcLookAtMatrix(vec3 ro, vec3 ta, vec3 up) {
    vec3 ww = normalize(ta - ro);
    vec3 uu = normalize(cross(ww,up));
    vec3 vv = normalize(cross(uu,ww));
    return mat3(uu, vv, ww);
}

const float ITER = 400.;
const float MAX_DIST = 12.;


vec4 materialColorForPixel( vec2 texCoord ) {




	float adj_time = mat_animation_time;
	//adj_time = fract(adj_time - bpm_offset);



	//adj_time = bpm_offset;

	// adj_time = adjusted_time(adj_time, mat_decay, mat_release);



	//adj_time = mat_animation_time;

    //float iTime = adj_time;



    // adj_time = mod(adj_time / 2., 1.);


    vec3 camPos = vec3(1.8, 5.5, -5.5);
    vec3 camTar = vec3(.1,0,.1);
    vec3 camUp = vec3(-1,0,-1.5);
    mat3 camMat = calcLookAtMatrix(camPos, camTar, camUp);

    float focalLength = 2.4 * mat_focal_length;
    //vec2 p = (-iResolution.xy + 2. * texCoord) / iResolution.y;

	vec2 p =  vec2(texCoord.x - 0.5, texCoord.y - 0.5);

	p *= mat_zoom;

    //vec2 p = texCoord;

    //p *= mat_zoom;



    vec3 rayDirection = normalize(camMat * vec3(p, focalLength));
    vec3 rayOrigin = camPos;
    vec3 rayPosition = rayOrigin;
    float rayLength = 0.;

    float distance = 0.;
    vec3 color = vec3(0);

    for (float i = 0.; i < ITER; i++) {
        rayLength += distance;
        rayPosition = rayOrigin + rayDirection * rayLength;
        distance = mapDebug(rayPosition, adj_time);

        if (distance < .001) {
            vec3 normal = calcNormal(rayPosition, adj_time);
            color = vec3(dot(normalize(vec3(1,.5,0)), normal) * .5 + .5);

            #ifdef DEBUG
                if (hitDebugPlane) {
                    // Display distance
                    float d = map(rayPosition, adj_time);
                    color = vec3(mod(abs(d) * 10., 1.));
                    color *= spectrum(abs(d));
                    color = mix(color, vec3(1), step(0., -d) * .25);
                } else if ( ! hitDebugTorus) {
                    // Color UVs
                    float repeat = 1. / 20.;
                    pMod2(modelUv, vec2(repeat));
                    color -= color * vec3(0,1,0) * smoothstep(0., .001, abs(modelUv.x) - repeat * .4);
                    color -= color * vec3(1,0,0) * smoothstep(0., .001, abs(modelUv.y) - repeat * .4);
                }
            #endif

            break;
        }

        if (rayLength > MAX_DIST) {
            break;
        }
    }

    #ifndef DEBUG
        float fog = pow(smoothstep(7.25, MAX_DIST, rayLength), .25);
        color = mix(color, vec3(0), fog);
        color = spectrum((color.r * 2. - 1.) * .2 + .4);
        color *= mix(1., .025, fog);
    #endif

    color = pow(color, vec3(1. / 2.2)); // Gamma
    return vec4(color, 1);



}




