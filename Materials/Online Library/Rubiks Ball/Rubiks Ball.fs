/*{
    "CREDIT": "tdhooper, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/tslfWs",

    "VSN": "1.0",

    "INPUTS": [



        {
            "LABEL": "UV/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "UV/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "UV/Shift Scale",
            "NAME": "mat_shift_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "UV/Shift Type",
            "NAME": "mat_shift_type",
            "TYPE": "long",
            "VALUES": ["Pre Rotate","Post Rotate"],
            "DEFAULT": "Post Rotate"
        },
        {
            "LABEL": "UV/Mirror X",
            "NAME": "mat_mirror_x",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },
        {
            "LABEL": "UV/Mirror Y",
            "NAME": "mat_mirror_y",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button",
        },

        {
            "LABEL": "Ball/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },

        {
            "LABEL": "Ball/Thickness",
            "NAME": "mat_thick",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ball/Corner",
            "NAME": "mat_corner",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ball/Shadow",
            "NAME": "mat_shadow",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Ball/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 5,
            "MAX": 200,
            "DEFAULT": 200
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
            "Label": "Animation/Offset Scale",
            "NAME": "mat_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "LABEL": "Animation/Restart",
            "NAME": "mat_restart",
            "TYPE": "event",
        },

        {
            "LABEL": "Alpha/Luma to Alpha",
            "NAME": "mat_luma_to_alpha",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.02
        },
        {
            "LABEL": "Alpha/Mode",
            "NAME": "mat_luma_mode",
            "TYPE": "long",
            "VALUES": ["Before Color Controls", "After Color Controls"],
            "DEFAULT": "Before Color Controls",
            "FLAGS": "generate_as_define"
        },


        {
            "LABEL": "Color/Brightness",
            "NAME": "mat_brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Contrast",
            "NAME": "mat_contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {
            "LABEL": "Color/Saturation",
            "NAME": "mat_saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Hue",
            "NAME": "mat_hue_shift",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Color/Invert",
            "NAME": "mat_invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },


    ],
    "GENERATORS": [
        {
            "NAME": "mat_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "reverse": "mat_reverse",
                "strob" : "mat_strob",
                "reset": "mat_restart",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8. * mat_offset_scale;

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 mirrorUV(vec2 uv) {
    uv += vec2(0.5);
    if (mat_mirror_x) {
        if (uv.x > 0.5)   {
            uv.x = 1.0-uv.x;
        }
    }
    if (mat_mirror_y) {
        if (uv.y > 0.5) {
            uv.y = 1.0-uv.y;
        }
    }
    uv -= vec2(0.5);
    return uv;
}

vec2 transformUV(vec2 uv) {

    if (mat_lock_aspect) {
        uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    }
    uv *= mat_scale * 1.75;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // XY shift pre rotate
    if (mat_shift_type == 0) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, -1. * 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    // XY shift post rotate
    if (mat_shift_type == 1) {

        // Preserve direction
        uv_shift += vec2(0.5);
        uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
        uv_shift -= vec2(0.5);
        uv += uv_shift;
    }

    return uv;
}


#define LOOP_DURATION 8.
#define MOVE_COUNT 6.
#define TIME_OFFSET .3

// axisX, axisY, axisZ, turns
vec4 moves[6] = vec4[6](
    vec4(1,0,0, 2.),
    vec4(0,1,0, -1.),
    vec4(0,-1,0, -3.),
    vec4(0,0,-1, 2.),
    vec4(0,-1,0, -1.),
    vec4(0,1,0, -3.)
);


// Quaternions
// https://github.com/mattatz/ShibuyaCrowd/blob/master/source/shaders/common/quaternion.glsl

#define QUATERNION_IDENTITY vec4(0, 0, 0, 1)

// #define PI 3.1415926

// Quaternion multiplication
// http://mathworld.wolfram.com/Quaternion.html
vec4 qmul(vec4 q1, vec4 q2) {
    return vec4(
        q2.xyz * q1.w + q1.xyz * q2.w + cross(q1.xyz, q2.xyz),
        q1.w * q2.w - dot(q1.xyz, q2.xyz)
    );
}

// Vector rotation with a quaternion
// http://mathworld.wolfram.com/Quaternion.html
vec3 rotate_vector(vec3 v, vec4 r) {
    vec4 r_c = r * vec4(-1, -1, -1, 1);
    return qmul(r, qmul(vec4(v, 0), r_c)).xyz;
}

// A given angle of rotation about a given axis
vec4 rotate_angle_axis(float angle, vec3 axis) {
    float sn = sin(angle * 0.5);
    float cs = cos(angle * 0.5);
    return vec4(axis * sn, cs);
}

vec4 q_conj(vec4 q) {
    return vec4(-q.x, -q.y, -q.z, q.w);
}

vec4 q_slerp(vec4 a, vec4 b, float t) {
    // if either input is zero, return the other.
    if (length(a) == 0.0) {
        if (length(b) == 0.0) {
            return QUATERNION_IDENTITY;
        }
        return b;
    } else if (length(b) == 0.0) {
        return a;
    }

    float cosHalfAngle = a.w * b.w + dot(a.xyz, b.xyz);

    if (cosHalfAngle >= 1.0 || cosHalfAngle <= -1.0) {
        return a;
    } else if (cosHalfAngle < 0.0) {
        b.xyz = -b.xyz;
        b.w = -b.w;
        cosHalfAngle = -cosHalfAngle;
    }

    float blendA;
    float blendB;
    if (cosHalfAngle < 0.99) {
        // do proper slerp for big angles
        float halfAngle = acos(cosHalfAngle);
        float sinHalfAngle = sin(halfAngle);
        float oneOverSinHalfAngle = 1.0 / sinHalfAngle;
        blendA = sin(halfAngle * (1.0 - t)) * oneOverSinHalfAngle;
        blendB = sin(halfAngle * t) * oneOverSinHalfAngle;
    } else {
        // do lerp if angle is really small.
        blendA = 1.0 - t;
        blendB = t;
    }

    vec4 result = vec4(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
    if (length(result) > 0.0) {
        return normalize(result);
    }
    return QUATERNION_IDENTITY;
}

vec2 rand(vec2 n) {
    return fract(sin(n) * 43758.5453123) * 2. - 1.;
}

vec2 srand(vec2 n, float hard) {
    vec2 nf = floor(n);
    vec2 nc = ceil(n);
    return mix(rand(nf), rand(nc), smoothstep(.5 * hard, 1. - .5 * hard, fract(n)));
}

vec2 mainSound( in int samp, float time )
{
    time += TIME_OFFSET;
    // shift time to stop clipping at start of move
    float index = floor((time + .05) / LOOP_DURATION * MOVE_COUNT);
    float moveIndex = mod(index - 1., MOVE_COUNT);
    float turns = abs(moves[int(moveIndex)].w);
    float volume = pow(turns / 3., 1.5);

    float t = mod(time, LOOP_DURATION);
    t = mod(t, LOOP_DURATION / MOVE_COUNT);
    vec2 s = srand(vec2(t * 2.5, t * 2. + .02) * 1000., .0) * exp(-100. * t);
    s += sin(vec2(t * 5000.) / mix(1.1, 1., rand(vec2(index+.2)).x) / (1. + t * .5)) * exp(-30. * t) * .1;

    return s * volume;
}



//========================================================
// Utils
//========================================================

// HG_SDF

void pR(inout vec2 p, float a) {
    p = cos(a)*p + sin(a)*vec2(p.y, -p.x);
}

float vmin(vec3 v) {
    return min(min(v.x, v.y), v.z);
}

float vmax(vec3 v) {
    return max(max(v.x, v.y), v.z);
}

float fBox(vec3 p, vec3 b) {
    vec3 d = abs(p) - b;
    return length(max(d, vec3(0))) + vmax(min(d, vec3(0)));
}

float smin(float a, float b, float k){
    float f = clamp(0.5 + 0.5 * ((a - b) / k), 0., 1.);
    return (1. - f) * a + f  * b - f * (1. - f) * k;
}

float smax(float a, float b, float k) {
    return -smin(-a, -b, k);
}

// Easings

float range(float vmin, float vmax, float value) {
  return clamp((value - vmin) / (vmax - vmin), 0., 1.);
}

float almostIdentity(float x) {
    return x*x*(2.0-x);
}

float circularOut(float t) {
  return sqrt((2.0 - t) * t);
}

// Spectrum palette, iq https://www.shadertoy.com/view/ll2GD3

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d ) {
    return a + b*cos( 6.28318*(c*t+d) );
}

vec3 spectrum(float n) {
    return pal( n, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67) );
}

// rotate on axis, blackle
vec3 erot(vec3 p, vec3 ax, float ro) {
  return mix(dot(ax,p)*ax, p, cos(ro))+sin(ro)*cross(ax,p);
}

//========================================================
// Animation
//========================================================

// see common tab for a list of moves (rotations)

bool lightingPass;
float time;

void applyMomentum(inout vec4 q, float time, int i, vec4 move) {

    float turns = move.w;
    vec3 axis = move.xyz;

    float duration = abs(turns);
    float rotation = PI / 2. * turns * .75;

    float start = float(i + 1);
    float t = time * MOVE_COUNT;
    float ramp = range(start, start + duration, t);
    float angle = circularOut(ramp) * rotation;
    vec4 q2 = rotate_angle_axis(angle, axis);
    q = qmul(q, q2);
}

void applyMove(inout vec3 p, int i, vec4 move) {

    float turns = move.w;
    vec3 axis = move.xyz;

    float rotation = PI / 2. * turns;

    float start = float(i);
    float t = time * MOVE_COUNT;
    float ramp = range(start, start + 1., t);
    ramp = pow(almostIdentity(ramp), 2.5);
    float angle = ramp * rotation;

    bool animSide = vmax(p * -axis) > 0.;
    if (animSide) {
        angle = 0.;
    }

    p = erot(p, axis, angle) ;
}

vec4 momentum(float time) {
    vec4 q = QUATERNION_IDENTITY;
    applyMomentum(q, time, 5, moves[5]);
    applyMomentum(q, time, 4, moves[4]);
    applyMomentum(q, time, 3, moves[3]);
    applyMomentum(q, time, 2, moves[2]);
    applyMomentum(q, time, 1, moves[1]);
    applyMomentum(q, time, 0, moves[0]);
    return q;
}

vec4 momentumLoop(float time) {
    vec4 q;

    // end state
    q = momentum(3.);
    q = q_conj(q);
    q = q_slerp(QUATERNION_IDENTITY, q, time);

    // next loop
    q = qmul(momentum(time + 1.), q);

    // current loop
    q = qmul(momentum(time), q);

    return q;
}


//========================================================
// Modelling
//========================================================

vec4 mapBox(vec3 p) {

    // shuffle blocks
    pR(p.xy, step(0., -p.z) * PI / -2.);
    pR(p.xz, step(0., p.y) * PI);
    pR(p.yz, step(0., -p.x) * PI * 1.5);

    // face colors
    vec3 face = step(vec3(vmax(abs(p))), abs(p)) * sign(p);
    float faceIndex = max(vmax(face * vec3(0,1,2)), vmax(face * -vec3(3,4,5)));
    vec3 col = spectrum(faceIndex / 6. + .1 + .5);

    // offset sphere shell
    float thick = .033 * mat_thick;
    float d = length(p + vec3(.1,.02,.05)) - .4;
    d = max(d, -d - thick);

    // grooves
    vec3 ap = abs(p);
    float l = sqrt(sqrt(1.) / 3.);
    vec3 plane = cross(abs(face), normalize(vec3(1)));
    float groove = max(-dot(ap.yzx, plane), dot(ap.zxy, plane));
    d = smax(d, -abs(groove), .01);

    float gap = .005;

    // block edge
    float r = .05 * mat_corner;
    float cut = -fBox(abs(p) - (1. + r + gap), vec3(1.)) + r;
    d = smax(d, -cut, thick / 2.);

    // adjacent block edge bounding
    float opp = vmin(abs(p)) + gap;
    opp = max(opp, length(p) - 1.);
    if (opp < d) {
        return vec4(opp, vec3(-1));
    }

    return vec4(d, col * .4);
}

vec4 map(vec3 p) {

    vec2 orbit = mat_orbit;
    // orbit += vec2(0.5);
    // orbit.x = 1.-orbit.x;
    // orbit -= vec2(0.5);

    pR(p.yz, (orbit.y * 2. + 1.) * 2.);
    pR(p.xz, (orbit.x * 2. + 1.) * 4.);


    //p.z *= -1.;
    pR(p.xz, time * PI * 2.);
    //pR(p.yz, time * PI * -2.);
    //pR(p.xy, PI);

    vec4 q = momentumLoop(time);
    p = rotate_vector(p, q);

    applyMove(p, 5, moves[5]);
    applyMove(p, 4, moves[4]);
    applyMove(p, 3, moves[3]);
    applyMove(p, 2, moves[2]);
    applyMove(p, 1, moves[1]);
    applyMove(p, 0, moves[0]);

    return mapBox(p);
}


//========================================================
// Rendering
//========================================================

mat3 calcLookAtMatrix( in vec3 ro, in vec3 ta, in float roll )
{
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(sin(roll),cos(roll),0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
    return mat3( uu, vv, ww );
}

// https://iquilezles.org/articles/normalsSDF
vec3 calcNormal(vec3 p)
{
    const float h = 0.001;
    // #define ZERO (min(FRAMEINDEX,0)) // non-constant zero
    vec3 n = vec3(0.0);
    for( int i=0; i<4; i++ )
    {
        vec3 e = 0.5773*(2.0*vec3((((i+3)>>1)&1),((i>>1)&1),(i&1))-1.0);
        n += e*map(p+e*h).x;
    }
    return normalize(n);
}


// origin sphere intersection
// returns entry and exit distances from ray origin
vec2 iSphere( in vec3 ro, in vec3 rd, float r )
{
    vec3 oc = ro;
    float b = dot( oc, rd );
    float c = dot( oc, oc ) - r*r;
    float h = b*b - c;
    if( h<0.0 ) return vec2(-1.0);
    h = sqrt(h);
    return vec2(-b-h, -b+h );
}

// https://www.shadertoy.com/view/lsKcDD
float softshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax )
{
    float res = 1.0;

    // iq optimisation, stop looking for occluders when we
    // exit the bounding sphere for the model
    vec2 bound = iSphere(ro, rd, .55);
    tmax = min(tmax, bound.y);

    float t = mint;
    float ph = 1e10;

    for( int i=0; i<100; i++ )
    {
        vec4 hit = map( ro + rd*t );
        float h = hit.x;
        if (hit.y > 0.) { // don't create shadows from bounding objects
            res = min( res, 10.0*h/t );
        }
        t += h;
        if( res<0.0001 || t>tmax ) break;

    }

    return clamp( res, 0.0, 1.0 );
}

vec3 render(vec2 p) {

    vec3 col = vec3(.02,.01,.025) * 0.;

    // raymarch

    vec3 camPos = vec3(0,0,2.);
    mat3 camMat = calcLookAtMatrix( camPos, vec3(0,0,-1), 0.);
    vec3 rd = normalize( camMat * vec3(p.xy, 2.8) );
    vec3 pos = camPos;

    vec2 bound = iSphere(pos, rd, .55);
    if (bound.x < 0.) {
        return col;
    }

    lightingPass = false;
    float rayLength = bound.x;
    float dist = 0.;
    bool background = true;
    vec4 res;

    for (int i = 0; i < mat_iterations; i++) {
        rayLength += dist;
        pos = camPos + rd * rayLength;
        res = map(pos);
        dist = res.x;

        if (abs(dist) < .001) {
            background = false;
            break;
        }

        if (rayLength > bound.y) {
            break;
        }
    }

    // shading
    // https://www.shadertoy.com/view/Xds3zN

    lightingPass = true;

    if ( ! background) {

        col = res.yzw;
        vec3 nor = calcNormal(pos);
        vec3 lig = normalize(vec3(-.33,.3,.25));
        vec3 lba = normalize( vec3(.5, -1., -.5) );
        vec3 hal = normalize( lig - rd );
        float amb = sqrt(clamp( 0.5+0.5*nor.y, 0.0, 1.0 ));
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        float bac = clamp( dot( nor, lba ), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);
        float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );

        // iq optimisation, skip shadows when we're facing away
        // from the light
        if( dif > .001) dif *= softshadow( pos, lig, 0.001, .9 );

        float occ = 1.;

        float spe = pow( clamp( dot( nor, hal ), 0.0, 1.0 ),16.0)*
            dif *
            (0.04 + 0.96*pow( clamp(1.0+dot(hal,rd),0.0,1.0), 5.0 ));

        vec3 lin = vec3(0.0);
        lin += 2.80*dif*vec3(1.30,1.00,0.70);
        lin += 0.55*amb*vec3(0.40,0.60,1.15)*occ;
        lin += 1.55*bac*vec3(0.25,0.25,0.25)*occ*vec3(2,0,1);
        lin += 0.25*fre*vec3(1.00,1.00,1.00)*occ;

        col = col*lin;
        col += 5.00*spe*vec3(1.10,0.90,0.70);
    }

    return col;
}

float vmul(vec2 v) {
    return v.x * v.y;
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    float mTime = (mat_time + TIME_OFFSET) / LOOP_DURATION;

    vec3 col = vec3(0);

    time = mod(mTime, 1.);
    vec2 p = uv;
    col += render(p);

    col = pow( col, vec3(0.4545) * mat_shadow );

    out_color = vec4(col, 1.);

    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        if (mat_luma(out_color.rgb) < mat_luma_threshold) {
            out_color.a = 0.;
        }
    }

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

    // Luma to alpha (after color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 1) {
        if (mat_luma(out_color.rgb) < mat_luma_threshold) {
            out_color.a = 0.;
        }
    }

    return out_color;
}
