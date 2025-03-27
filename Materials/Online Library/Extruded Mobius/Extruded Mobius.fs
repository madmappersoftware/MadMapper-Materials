/*{
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/NlKXWt",

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
            "LABEL": "Mobius Spiral/FOV",
            "NAME": "mat_fov",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Mobius Spiral/Distort",
            "NAME": "mat_distort",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Mobius Spiral/Height",
            "NAME": "mat_height",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Mobius Spiral/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 8,
            "MAX": 128,
            "DEFAULT": 128
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
            "LABEL": "Alpha/Sensitivity",
            "NAME": "mat_luma_sensitivity",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 2.0
        },
        {
            "LABEL": "Alpha/Threshold",
            "NAME": "mat_luma_threshold",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.25
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
    uv *= mat_scale;

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


/*

    Extruded Mobius Spiral
    ----------------------

    As you can see, this is an extruded Mobius spiral pattern. It's an
    extension of Mla's nicely written "Complex Atanh" example. If you're
    interested in complex transformations, Mobius spirals etc, that
    particular example is the one I'd recommend looking at.

    Technically, there's not a lot to this: Perform the required 2D
    transformations then pass the results into an extrusion algorithm.
    The code looks more complicated than it has to be due to the decision
    on my part to both extrude the cells and cater for three different
    pylon shapes -- It seemed like a good idea at the time. :)

    A double spiral is a simple addition to the regular Mobius spiral
    combination that most people use, but I couldn't for the life of me
    remember how to produce one, so was pretty happy to see how to do
    that in Mla's original.

    I wasn't really sure whether this would work or not, since I figured
    mutating space so much prior to raymarching would make the surface
    very difficult to hone in on, but relatively speaking, things came
    together surprisingly well, and I can thank Mla's attention to
    function order, scaling, etc. Even so, the hackory police and the
    Lipschitz Surface Constraint Commission probably won't be happy with
    this example at all. :D

    Anyway, there are a few define options below: SHAPE, ROW_OFFSET, etc.
    When using the one spiral option, the surface kind of has the feel of
    a traced out Doyle spiral, but I'd like to produce the real thing at
    some stage.


    Based on the following:

    // The original and cleaner looking 2D version.
    Complex Atanh - mla
    https://www.shadertoy.com/view/tsBXRW

    // Another one of those shaders that somehow slipped under
    // radar. Some of the visuals are strikingly beautiful.
    Complex Atanh Revisited - mla
    https://www.shadertoy.com/view/fldGRB

*/



// Off the rows by half a cell to produce a brickwork feel. The staggered
// effect can also make a quantized image look smoother.
#define ROW_OFFSET

// Pylon cross section shape.
// Square: 0, Circle: 1, Hexagon: 2.
#define SHAPE 2

// Colorful face decorations.
#define FACE_DECO

// Double spiral. The single version is cleaner, but not as interesting.
// Thanks to Mla for this addition.
#define DOUBLE_SPIRAL

// Boring out holes in the blocks.
//#define HOLES

// Raising the faces of the pylon tops. I find it can help bounce the light
// off the surface in a more reflective way.
//#define RAISED

// Putting a ridge decoraction on the pylon tops.
//#define RIDGES

// Originally for debug purposes, but it's decorative in its own way
//#define VERT_LINES


// The hexagons must use offset rows.
#if SHAPE == 2
#ifndef ROW_OFFSET
#define ROW_OFFSET
#endif
#endif

#ifdef ROW_OFFSET
#if SHAPE >= 1
const vec2 rDim = vec2(1, 2.*.8660254);
#else
const vec2 rDim = vec2(1, 2);
#endif
#else
const vec2 rDim = vec2(1, 2);
#endif

// Global tile scale.
vec2 scale = vec2(1./8.);

// Max ray distance.
#define FAR 20.


// Scene object ID to separate the mesh object from the terrain.
float objID;


// Standard 2D rotation formula.
mat2 rot2(in float a){ float c = cos(a), s = sin(a); return mat2(c, -s, s, c); }


float hash21(vec2 p){

    /*
    // Attempting to fix accuracy problems on some systems by using
    // a slight variation on Dave Hoskin's hash formula, here:
    // https://www.shadertoy.com/view/4djSRW
    vec3 p3 = fract(vec3(p.xyx)*.1031);
    p3 += dot(p3, p3.yzx + 33.333);
    return fract((p3.x + p3.y)*p3.z);
    */

    //p = mod(p, (vec2(B, A)*K));

    // IQ's vec2 to float hash.
    //p = floor(p*32768.)/32768.;
    return fract(sin(dot(p, vec2(27.617, 57.743)))*43758.5453);

}

/*
// Commutative smooth maximum function. Provided by Tomkh, and taken
// from Alex Evans's (aka Statix) talk:
// http://media.lolrus.mediamolecule.com/AlexEvans_SIGGRAPH-2015.pdf
// Credited to Dave Smith @media molecule.
float smax(float a, float b, float k){

   float f = max(0., 1. - abs(b - a)/k);
   return max(a, b) + k*.25*f*f;
}
*/

// IQ's extrusion formula.
float opExtrusion(in float sdf, in float pz, in float h){

    vec2 w = vec2( sdf, abs(pz) - h );
    return min(max(w.x, w.y), 0.) + length(max(w, 0.));

    /*
    // Slight rounding. A little nicer, but slower.
    const float sf = .015;
    vec2 w = vec2( sdf, abs(pz) - h - sf/2.);
    return min(max(w.x, w.y), 0.) + length(max(w + sf, 0.)) - sf;
    */
}

#if SHAPE == 2
// Signed distance to a regular hexagon, with a hacky smoothing variable thrown
// in. -- It's based off of IQ's more exact pentagon method.
float sHexS(in vec2 p, float r, in float sf){

      // Pointed top.
      //const vec3 k = vec3(-.8660254, .5, .57735); // pi/6: cos, sin, tan.
      // Flat top.
      const vec3 k = vec3(.5, -.8660254, .57735); // pi/6: cos, sin, tan.

      // X and Y reflection.
      p = abs(p);
      p -= 2.*min(dot(k.xy, p), 0.)*k.xy;

      r -= sf;
      // Polygon side.
      // Pointed top.
      //return length(p - vec2(clamp(p.x, -k.z*r, k.z*r), r))*sign(p.y - r) - sf;
      // Flat top.
      return length(p - vec2(r, clamp(p.y, -k.z*r, k.z*r)))*sign(p.x - r) - sf;

}
#endif



// Height map value.
float hm(in vec2 p){

     return (sin(6.2831*(p.y*2. + p.x) + mat_time*2.)*.5 + .5);

    // Regular random values.
    // This won't work for the sqrt(3.) scaling, but there are
    // ways around it. It's not used in this example anyway.
    //float h = hash21(p);
    //return (sin(6.2831*h + mat_time*2.)*.5 + .5);

}

// IQ's signed box formula.
float sBoxS(in vec2 p, in vec2 b, in float sf){

  vec2 d = abs(p) - b + sf;
  return min(max(d.x, d.y), 0.) + length(max(d, 0.)) - sf;
}


// Global local 2D grid coordinates. Hacked in.
vec2 gP;
// A global responsible for tempering the height of the pylong near
// the pylon center.
float tempR;

// A regular extruded block grid.
//
// The idea is very simple: Produce a normal grid full of packed square pylons.
// That is, use the grid cell's center pixel to obtain a height value (read in
// from a height map), then render a pylon at that height.

vec4 blocks(vec3 q3){



    // Brick dimension: Length to height ratio with additional scaling.
    vec2 l = scale;
    vec2 s = scale*2.;
    #if SHAPE == 2 // Hexagon.
    vec2 hSc = vec2(1);//vec2(1, scale.y/scale.x*2./1.732);
    #elif SHAPE == 1 // Circle with an offset.
    #ifdef ROW_OFFSET
    vec2 hSc = vec2(1);//vec2(1, scale.y/scale.x*2./1.732);
    #endif
    #endif



    float minSc = min(scale.x, scale.y);

    // Distance.
    float d = 1e5;
    // Cell center, local coordinates and overall cell ID.
    vec2 p, ip;

    // Individual brick ID.
    vec2 id = vec2(0);

    // Four block corner postions.
    #ifdef ROW_OFFSET
    // Offset rows.
    vec2[4] ps4 = vec2[4](vec2(-.25, .25), vec2(.25), vec2(.5, -.25), vec2(0, -.25));
    #else
    vec2[4] ps4 = vec2[4](vec2(-.25, .25), vec2(.25), vec2(.25, -.25), vec2(-.25));
    #endif

    float data = 0.; // Extra data.

    int i;

    for(int i = 0; i<4; i++){


        // Local coordinates.
        p = q3.xy;
        ip = floor(p/s - ps4[i]); // Local tile ID.

        // Correct positional individual tile ID.
        vec2 idi = (ip + .5 + ps4[i])*s;

        p -= idi; // New local position.


        // The extruded block height. See the height map function, above.
        // An extra line is needed for this example.
        vec2 index = mod(idi, rDim.yx)/rDim.yx;

        // We also have an additional height tempering value for
        // the spiral centers.
        float h = hm(index)*tempR*.1;


        #if SHAPE == 2
        // Hexagon option: Multiply scale by "vec2(1, 1.732/2.)".
        float di2D = sHexS(p, minSc/1.732 - .0035, .015);
        #elif SHAPE == 1
        // Circle.
        #ifdef ROW_OFFSET
        float di2D = length(p) - minSc/1.732 + .0035;
        #else
        float di2D = length(p) - l.x/2. + .0035;
        #endif
        #else
        // Square.
        float di2D = sBoxS(p, l/2. - .0035, .02);
        #endif



        #ifdef HOLES
        // Boring out the objects.
        di2D = max(di2D, -(di2D + minSc/3.));
        #endif

        // The extruded distance function value.
        float di = opExtrusion(di2D, (q3.z + h - 1.), h + 1.);



        // Lego.
        //float cap = opExtrusion(di2D + .0465, (q3.z + h - 1. + .035), h + 1.);
        //di = min(di, cap);


        #ifdef RAISED
        // Raised tops.
        di += di2D*.5*tempR;
        #endif

        #ifdef RIDGES
        // Putting ridges on the faces.
        di += smoothstep(-.5, .5, sin(di2D/minSc*6.2831*3.))*.01;
        #endif




        // If applicable, update the overall minimum distance value,
        // ID, and box ID.
        if(di<d){
            d = di;
            id = idi;
            // Extra data. In this case, the 2D distance field.
            data = di2D;

            gP = p;
        }

    }

    // Return the distance, position-base ID and box ID.
    return vec4(d, id, data);
}


//////

// Mla's complex functions: Most people have a copy of these lying
// around. They're pretty easy to derive.
//
vec2 cMul(vec2 z, vec2 w){ return mat2(z, -z.y, z.x)*w; }

vec2 cInv(vec2 z){ return vec2(z.x, -z.y)/dot(z, z); }

vec2 cDiv(vec2 z, vec2 w){ return cMul(z, cInv(w)); }

vec2 cLog(vec2 z){ return vec2(log(length(z)), atan(z.y, z.x)); }

// Inverse hyperbolic tangent: The pattern looks loxodromic. I'm not
// technically sure what you're supposed to call this particular
// combination, but there's complex division and polar stuff, so it's
// not surprising that it looks like a Mobius spiral combination.
vec2 caTanh(vec2 z, float sc) {

    // You could take the functions above and start grouping things if you
    // wanted more compactness, and possibly speed, but I'm leaving it as is.
    return cLog(cDiv(vec2(sc, 0) + z, vec2(sc, 0) - z));
}

//////


// Block ID -- It's a bit lazy putting it here, but it works. :)
vec4 gID;


// The scene's distance function.
float map(vec3 p){


    // Back plane.
    float fl = -p.z + .01;

    //////////////
    // Complex transformations.

    // Performing some complex operations on "p.xy" to make some cool looking
    // Mobius spirals. Complex, as in the complex plane; The operations are
    // actually quite simple. :)

    // Rotation about the XY plane. Equivalent to a 2D complex multiplication operation.
    vec2 z = rot2(-sin(mat_time/3.)*.65)*p.xy; // Same as: cmul(vec2(cos(a), sin(a))*c, z2);
    // Tempering the extrusion height toward the spiral origins to lessen Moire effects
    // and general artifacts.
    const float sc = 1.5; // Effects spiral distance.
    float r = min(length(z - vec2(sc, 0)), length(z - vec2(-sc, 0)));
    tempR = r;
    z = caTanh(-z, sc)/6.2831;


    #ifdef DOUBLE_SPIRAL
    vec2 z2 = rot2(-cos(mat_time/3.)*.65*2.)*p.xy;
    // Performing another inverse hyperbolic tangent operation. It's very simple
    // to do, once someone shows you the answer. :)
    float sc2 = .75 * mat_distort;
    float r2 = min(length(z2.xy - vec2(sc2, 0)), length(z2.xy - vec2(-sc2, 0)));
    tempR = min(tempR, r2);
    z += caTanh(z2, sc2)/6.2831;
    #endif

    // Tempering the height of the pylons eminating from the spiral centers.
    // It looks way too messy if you don't do this.
    tempR = smoothstep(.1, .5, tempR) * mat_height;

    // More movement. Not necessary, but it looks more interesting.
    z.y = fract(z.y + mat_time*.1);
    // More scaling.
    z = cMul(rDim, z);

    // I like this addition, but I think it dizzies things too much.
    //z.y -= mat_time/6.;

    // Set the XY plane coordinates to the new transformed ones.
    p.xy = z;

    //////////////


    // Extrude.
    vec4 d4 = blocks(p);
    gID = d4; // Save the distance, cell ID, and 2D face distance.


    // Object ID.
    objID = d4.x < fl? 0. : 1.;

    // Minimum distance for the scene.
    return min(d4.x, fl);

}

// Basic raymarcher.
float trace(in vec3 ro, in vec3 rd){

    // Overall ray distance and scene distance.
    float t = 0., d;

    for(int i = 0; i<mat_iterations; i++){

        d = map(ro + rd*t);
        // Note the "t*b + a" addition. Basically, we're putting less emphasis on accuracy, as
        // "t" increases. It's a cheap trick that works in most situations... Not all, though.
        if(abs(d)<.001 || t>FAR) break; // Alternative: 0.001*max(t*.25, 1.), etc.

        t += d*.8;
    }

    return min(t, FAR);
}


// Standard normal function. It's not as fast as the tetrahedral calculation, but more symmetrical.
vec3 getNormal(in vec3 p, float t) {
    const vec2 e = vec2(.001, 0);
    return normalize(vec3(map(p + e.xyy) - map(p - e.xyy), map(p + e.yxy) - map(p - e.yxy),
                          map(p + e.yyx) - map(p - e.yyx)));
}


// Cheap shadows are hard. In fact, I'd almost say, shadowing particular scenes with limited
// iterations is impossible... However, I'd be very grateful if someone could prove me wrong. :)
float softShadow(vec3 ro, vec3 lp, vec3 n, float k){

    // More would be nicer. More is always nicer, but not always affordable. :)
    const int maxIterationsShad = 32;

    ro += n*.0015; // Coincides with the hit condition in the "trace" function.
    vec3 rd = lp - ro; // Unnormalized direction ray.

    float shade = 1.;
    float t = 0.;
    float end = max(length(rd), .0001);
    //float stepDist = end/float(maxIterationsShad);
    rd /= end;

    // Max shadow iterations - More iterations make nicer shadows, but slow things down. Obviously,
    // the lowest number to give a decent shadow is the best one to choose.
    for (int i = 0; i<maxIterationsShad; i++){

        float d = map(ro + rd*t);
        shade = min(shade, k*d/t);
        //shade = min(shade, smoothstep(0., 1., k*h/dist)); // Thanks to IQ for this tidbit.
        // So many options here, and none are perfect: dist += clamp(d, .01, stepDist), etc.
        t += clamp(d, .01, .1);


        // Early exits from accumulative distance function calls tend to be a good thing.
        if (d<0. || t>end) break;
    }

    // Sometimes, I'll add a constant to the final shade value, which lightens the shadow a bit --
    // It's a preference thing. Really dark shadows look too brutal to me. Sometimes, I'll add
    // AO also just for kicks. :)
    return max(shade, 0.);
}


// I keep a collection of occlusion routines. This one is pretty standard.
// I'm assuming this one based on IQ's original.
float calcAO(vec3 p, vec3 n){

    float occ = 1.; // Occlusion.
    float ds = .01; // Analogous to sample spread. // .01*t
    float k = .05/ds;  // Intensity.
    float dst = ds*2.; // Initial distance.

    for(int i = 0; i<5; i++){
        occ -= (dst - map(p + n*dst))*k;
        dst += ds;
        k *= .5;
    }

    return clamp(occ, 0., 1.);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);


    #ifdef ROW_OFFSET
    #if SHAPE >= 1
    scale *= vec2(2./1.732, 1);
    #endif
    #endif

    // Camera Setup.
    // Slightly tilted camera, just to prove it's 3D. :)
    vec3 ro = vec3(0, -1, -2.2); // Camera position, doubling as the ray origin.
    vec3 lk = ro + vec3(0, .1, .25); // "Look At" position.

    // Light positioning. One is just in front of the camera, and the other is in front of that.
    vec3 lp = ro + vec3(-.5, 1, 1);// Put it a bit in front of the camera.

    // Using the above to produce the unit ray-direction vector.
    float FOV = 1.333 * mat_fov; // FOV - Field of view.
    vec3 fwd = normalize(lk-ro);
    vec3 rgt = normalize(vec3(fwd.z, 0., -fwd.x ));
    // "right" and "forward" are perpendicular, due to the dot product being zero. Therefore, I'm
    // assuming no normalization is necessary? The only reason I ask is that lots of people do
    // normalize, so perhaps I'm overlooking something?
    vec3 up = cross(fwd, rgt);
    // rd - Ray direction.
    //vec3 rd = normalize(fwd + FOV*uv.x*rgt + FOV*uv.y*up);
    vec3 rd = normalize(uv.x*rgt + uv.y*up + fwd/FOV);

    // Swiveling the camera about the XY-plane.
    rd.xy *= rot2( sin(mat_time)/32. );



    // Raymarch to the scene.
    float t = trace(ro, rd);

    // Save the block field value, block ID and 2D data field value.
    vec4 svGID = gID;

    // Object ID.
    float svObjID = objID;

    // Height tempering for the spiral centers.
    float svTempR = tempR;

    // Pylon face local coordinates.
    vec2 svP = gP;


    // Initiate the scene color to black.
    vec3 col = vec3(0);

    // The ray has effectively hit the surface, so light it up.
    if(t < FAR){


        // Surface position and surface normal.
        vec3 sp = ro + rd*t;
        vec3 sn = getNormal(sp, t);

                // Light direction vector.
        vec3 ld = lp - sp;
        // Distance from respective light to the surface point.
        float lDist = max(length(ld), .001);

        // Normalize the light direction vector.
        ld /= lDist;


        // Shadows and ambient self shadowing.
        float sh = softShadow(sp, lp, sn, 16.);
        float ao = calcAO(sp, sn); // Ambient occlusion.

        // Light attenuation, based on the distances above.
        float atten = 1./(1. + lDist*.05);

        // Diffuse lighting.
        float diff = max( dot(sn, ld), 0.);
        //diff = pow(diff, 4.)*2.; // Ramping up the diffuse.

        // Specular lighting.
        float spec = pow(max(dot(reflect(ld, sn), rd ), 0.), 32.);

        // Fresnel term. Good for giving a surface a bit of a reflective glow.
        float fre = pow(clamp(1. - abs(dot(sn, rd))*.5, 0., 1.), 2.);

        // Schlick approximation. I use it to tone down the specular term. It's pretty subtle,
        // so could almost be aproximated by a constant, but I prefer it. Here, it's being
        // used to give a hard clay consistency... It "kind of" works.
        float Schlick = pow( 1. - max(dot(rd, normalize(rd + ld)), 0.), 5.);
        float freS = mix(.15, 1., Schlick);  //F0 = .2 - Glass... or close enough.


        // Obtaining the texel color.
        vec3 texCol = vec3(.6);
        // The extruded grid.
        if(svObjID<.5){

            // Wrapping the colors properly.
            vec2 index = mod(svGID.yz, rDim.yx)/rDim.yx;
            //index = floor(index*8.)/8. + 1./16.;

            // Using the color index to produce two different colors.
            vec3 col1 = .5 + .45*cos(6.2831*index.y + vec3(0, 1, 2)*2.);
            vec3 col2 = .5 + .45*cos(6.2831*index.y + 3.14159/2.5 + vec3(0, 1, 2)*1.35);
            texCol = col1;
            // Debug coloring.
            //texCol = mod(floor(index.x*16.), 2.)<.5? vec3(.05) :  texCol;
            //texCol = mod(floor(index.y*8.), 2.)<.5?  vec3(.05) : vec3(.9);
            //texCol = vec3(.05);


            // The dark lines, etc, need to look crisp, so we need a derivative
            // based smoothing factor. This can either be done the easy way via
            // hardware, or the harder but more maliable and reliable way. The
            // hardware version is below for a comparison.
            #if 1
            float di2D = svGID.w;
            float tmp = map(sp - vec3(3./450., 0, 0)); // Nearby X sample.
            float di2DX = gID.w; // dX.
            tmp = map(sp - vec3(0, 3./450., 0)); // Nearby Y sample.
            float di2DY = gID.w; // dY.
            //tmp = map(sp - vec3(0, 0, 3./450.));
            //float di2DZ = gID.w;//

            vec3 dF = (vec3(di2DX, di2DY, 1e5) - di2D); // Rought partial differential.
            // Technically not fwidth, but I prefer it.
            float sf = length(dF.xy); // Fwidth: abs(dF.x) + abs(dF.y);
            #else
            // The one line hardware version. Much cheaper, but not always reliable.
            float sf = fwidth(svGID.w);
            #endif


            #ifdef VERT_LINES
            // Lines eminating from the center to the vertices.
            #if SHAPE == 2
            const float aNum = 6.;
            vec2 z = rot2(3.14159/aNum)*svP;
            //float ch = smoothstep(-sf, sf, (abs(fract(a*6. - .5) - .5) - .45)/6.);
            #else
            const float aNum = 4.;
            vec2 z = svP;
            #endif
            float a = mod(atan(z.x, z.y), 6.2831)/6.2831;
            a = (floor(a*aNum) + .5)/aNum;
            z *= rot2(a*6.2831);
            texCol = mix(texCol, vec3(0), (1. - smoothstep(0., sf, abs(z.x) - .001))*.95);
            #endif


            #ifdef FACE_DECO
            float rim = .04;
            #ifdef ROW_OFFSET
            #if SHAPE >= 1
            rim /= .8660254;
            #endif
            #endif
            texCol = mix(texCol, vec3(0), (1. - smoothstep(0., sf, svGID.w + rim))*.95);
            texCol = mix(texCol, col2, (1. - smoothstep(0., sf, svGID.w + rim + .005)));
            #endif


            // Dark edges.
            float h = hm(index); // Pylon height.
            float lw = .0035;
            float dS = abs(svGID.w) - lw/2.; // 2D face field value.
            texCol = mix(texCol, texCol/3., (1. - smoothstep(0., sf, dS)));
            dS = max(dS, abs(sp.z + h*.1*svTempR*2.) - lw/2.); // Just the rim.
            texCol = mix(texCol, vec3(0), (1. - smoothstep(0., sf, dS))*.95);


        }
        else {

            // The dark floor in the background.
            texCol = vec3(.05);
        }


        // Combining the above terms to produce the final color.
        col = texCol*(diff*sh + .3 + vec3(.25, .5, 1)*fre*0. + vec3(1, .97, .92)*spec*freS*2.*sh);

        // Shading.
        col *= ao*atten;


    }


    // Rought gamma correction.
    out_color = vec4(sqrt(max(col, 0.)), 1);


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 2.) - mat_luma_threshold * 4. - 1.;
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
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 2.) - mat_luma_threshold * 4. - 1.;
    }

    return out_color;
}
