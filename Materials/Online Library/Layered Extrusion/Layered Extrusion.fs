/*{
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/Wsc3Ds",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Layered Extrusion/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Layered Extrusion/Rotate",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Layered Extrusion/Smoothing",
            "NAME": "mat_smoothing",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 16.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Animation/Amplitude",
            "NAME": "mat_amp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "Label": "Animation/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Scroll/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Scroll/Fixed Direction",
            "NAME": "mat_shift_fixed_dir",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_shift_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_shift_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_shift_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_shift_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_shift_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Texture/Use Custom Texture",
            "NAME": "mat_use_custom_texture",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture/Texture",
            "NAME": "mat_texture",
            "TYPE": "image",
        },

        {
            "LABEL": "Color/Top Color",
            "NAME": "mat_t_color",
            "TYPE": "color",
            "DEFAULT": [
                0.9,
                0.7,
                0.55,
                1.0
            ]
        },
        {
            "LABEL": "Color/Bottom Color",
            "NAME": "mat_b_color",
            "TYPE": "color",
            "DEFAULT": [
                0.26,
                0.52,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Side Color",
            "NAME": "mat_s_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                0.6,
                0.4,
                1.0
            ]
        },


        {
            "NAME": "mat_brightness",
            "LABEL": "Color/Brightness",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_contrast",
            "LABEL": "Color/Contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1
        },
        {   "NAME": "mat_saturation",
            "LABEL": "Color/Saturation",
            "TYPE": "float",
            "MIN": -1.0,
            "MAX": 3.0,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_hue_shift",
            "LABEL": "Color/Hue",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1,
            "DEFAULT": 0.0
        },
        {
            "NAME": "mat_invert",
            "LABEL": "Color/Invert",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        }


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
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_shift_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_shift_speed",
                "speed_curve":2,
                "reverse": "mat_shift_reverse",
                "strob" : "mat_shift_strob",
                "bpm_sync": "mat_shift_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ],
    "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": "95b90082f799f48677b4f206d856ad572f1d178c676269eac6347631d4447258.jpg"
        }
    }

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset) * 2.;

/*

    Faux Layered Extrusion
    ----------------------

    Applying some simple layering techniques to an octagonal diamond Truchet object
    to give it an extruded appearance -- No 3D was harmed during the making of this
    example, as they say. :)

    Graphics designers have been layering 2D fields to produce faux 3D effects for
    years. A lot of it is common sense, and easy to apply. Referring to the imagery,
    you can see that it's a simple application of three main layers -- A drop shadow
    layer, an extruded layer on top of that, and the top layer. Each layer requires
    a separate sample. The main object, in this case, is a simplified blobby Truchet
    based on an octagon diamond grid, but it can be anything simple -- The Truchet
    is simplified because not all octagonal tile combinations are used. I've also
    sprinkled in some extra beacon-like objects between the layers to enhance the
    illusion a bit.

    The code itself is just an application of the above, with some extra window
    dressing thrown in. I made a lot of it up as I went along, so I wouldn't pay too
    much attention to it. It's helpful to be comfortable with applying a 2D field
    layer using the "mix" and "smoothstep" functions, and for anyone who's not, it
    only takes a few minutes to figure out.


    References:

    // Fizzer put together a Truchet pattern based on an octagonal diamond grid a
    // while back. The Truchet here is a different kind (blobs instead of arcs), but
    // is essentially based on the same premise.
    4.8^2 Truchet - Fizzer
    https://www.shadertoy.com/view/MlyBRG

    // BigWIngs's popular Youtube channel. It's always informative seeing how others
    // approach various graphics topics.
    Shader Coding: Truchet Tiling Explained! -  The Art of Code
    https://www.youtube.com/watch?v=2R7h76GoIJM

*/

// Display the octagonal diamond grid. If you'd like a rough idea how the Truchet is
// constructed, uncomment this. From more information, see the "distField" function.
//#define SHOW_GRID

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// Standard 2D rotation formula.
mat2 rot2(in float a){ float c = cos(a), s = sin(a); return mat2(c, -s, s, c); }


// IQ's vec2 to float hash.
float hash21(vec2 p){  return fract(sin(dot(p, vec2(27.619, 57.583)))*43758.5453); }

// vec2 to vec2 hash.
vec2 hash22(vec2 p) {

    // Faster, but doesn't disperse things quite as nicely. However, when framerate
    // is an issue, and it often is, this is a good one to use. Basically, it's a tweaked
    // amalgamation I put together, based on a couple of other random algorithms I've
    // seen around... so use it with caution, because I make a tonne of mistakes. :)
    float n = sin(dot(p, vec2(27, 57)));
    return fract(vec2(262144, 32768)*n)*2. - 1.;

    // Animated.
    //p = fract(vec2(262144, 32768)*n);
    //return sin(p*6.2831853 + mat_time);

}

// Based on IQ's gradient noise formula.
float n2D3G( in vec2 p ){

    vec2 i = floor(p); p -= i;

    vec4 v;
    v.x = dot(hash22(i), p);
    v.y = dot(hash22(i + vec2(1, 0)), p - vec2(1, 0));
    v.z = dot(hash22(i + vec2(0, 1)), p - vec2(0, 1));
    v.w = dot(hash22(i + 1.), p - 1.);

#if 1
    // Quintic interpolation.
    p = p*p*p*(p*(p*6. - 15.) + 10.);
#else
    // Cubic interpolation.
    p = p*p*(3. - 2.*p);
#endif

    return mix(mix(v.x, v.y, p.x), mix(v.z, v.w, p.x), p.y);
    //return v.x + p.x*(v.y - v.x) + p.y*(v.z - v.x) + p.x*p.y*(v.x - v.y - v.z + v.w);
}


// Truchet distance formula. It's set to circles, but you could try
// the octagonal distance metric, if you wanted.
float distT(vec2 p){

    return length(p);

    //p = abs(p);
    //return max(max(p.x, p.y), (p.x + p.y)*.7071);
}

// Beacon ID hack, based on the way the Truchet is produced.
float id = 0.01;


// A blobby octagonal diamond structure. Test to see whether the pixel is inside
// a diamond tile or an octagonal tile, then render the appropriate tile.
// A diamond tile will have two circles cut out at opposite ends, and an octagon
// will have four circles cut out at the correct positions. It's all pretty simple.
// However blobby Truchet tiles on square-based grids need to have their distances
// flipped on alternating checkered tiles. It slightly complicates the code, but
// I'm sure it's nothing that people can't handle. :)
//
// Uncomment the "SHOW_GRID" define and refer to imagery get better idea.
vec3 distFieldT(vec2 q){


    float d = 1e5;

    // Offsetting the diamond tiles by half a unit.
    vec2 p = q - .5;

    // Tile ID and local coordinates.
    vec2 ip = floor(p);
    p -= ip + .5;


    const float sqrt2 = sqrt(2.);


    // Side length. Due to the symmetry, it's the side length of both the
    // octagon and diamond.
    float s = 1./(1. + sqrt2);


    // 2D diamond field... The dimensions are calculated using basic trigonometry.
    // Although, I was still too lazy to do it myself.
    float dia = (abs(p.x) + abs(p.y))/sqrt2 - s/2.;


    id = -1.;

    // If we're inside a diamond, then render the diamond tile. Anything outside of this
    // will obviously be inside an octagon tile. In case it isn't obvious, you could test
    // for an octagonal hit too, but a diamond is easier.
    if(dia<.0){


        // Rotate random tiles.
        float rnd = hash21(ip);
        if(rnd<.5) p = rot2(3.14159/2.)*p;

        // Chop out two circles on opposite corners. Use the define to display
        // the grid and refer to the imagery.
        p.y = abs(p.y);
        d = min(d, distT(p - vec2(0, s/sqrt2)) - s/2.);

        // Flip the distances on alternating checkered tiles.
        float ch = mod(ip.x + ip.y, 2.);
        if(ch<.5) d = -d;

        // Flip the distances on random tiles as well.
        if(rnd<.5) d = -d;

        // Moving the tile ID away from the center of the octagonal tile
        // to the center of the diamond tile.
        ip += .5;

    }
    else {

        // If we're inside an octagon cell (outside a diamond), then obtain the
        // ID (similar to the diaomond ID, but offset by half a cell) and
        // fractional coordinates.
        p = q;
        vec2 ip = floor(p);
        p -= ip + .5; // Equivalent to: fract(p) - .5;

        // Rotate random tiles. You don't really the extra addition, but I
        // figured it might mix things up more... maybe. :)
        float rnd = hash21(ip + vec2(.1, .3));
        if(rnd<.5) p = rot2(3.14159/4.)*p;

        // Chop out four circles on opposite corners. Use the define to display
        // the grid and refer to the imagery.
        d = min(d, distT(p - vec2(-.5, s/2.)) - s/2.);
        d = min(d, distT(p - vec2(s/2., .5)) - s/2.);
        d = min(d, distT(p - vec2(.5, -s/2.)) - s/2.);
        d = min(d, distT(p - vec2(-s/2., -.5)) - s/2.);

        // Flip the distances on alternating checkered tiles.
        float ch = mod(ip.x + ip.y, 2.);
        if(ch>.5) {
            d = -d;
            id = -id;
        }

        // Flip the distances on random tiles as well.
        if(rnd<.5) {
            d = -d;
            id = -id;
        }
    }

    // Return the distance and center tile ID.
    return vec3(d, ip);

}


// The beacon distance function.
float distS(vec2 p){

    // Circle.
    return length(p);

    // Other metrics to try.
    //p = abs(p);
    //return max(max(p.x, p.y), (p.x + p.y)*.7071);

    //return max(p.x, p.y);
    //return max(p.x*.8660254 + p.y*.5, p.y);
    //return (p.x + p.y)*.7071;



}

// The beacon-like shape distance field function.
vec3 distFieldS(vec2 p){

    // p += vec2(0, .5);
    vec2 ip = floor(p);
    p -= ip + .5;

    // Applying just a small random center offset.
    float d = distS(p - (hash22(ip) - .5)*.05) - .115;

    return vec3(d, ip);
}


#ifdef SHOW_GRID
// The ocatagonal-dimond grid boundaries.
float gridField(vec2 q){

    // Offsetting the diamond tiles by half a unit.
    vec2 p = q - .5;
    vec2 ip = floor(p);
    p -= ip + .5;


    // 2D diamond field... The dimensions are calculated using basic trigonometry.
    // Although, I was still too lazy to do it myself.
    float dia = abs(p.x) + abs(p.y) - (1. - sqrt(2.)/2.);

    float d = 1e5;

    // If we're inside a diamond, then render the diamond tile. Anything outside of this
    // will obviously be inside an octagon tile.
    if(dia<.0){

        d = dia;

        ip += .5;

    }
    else {

        // If we're inside an octagon cell (outside a diamond), then obtain the
        // ID (similar to the diaomond ID, but offset by half a cell) and
        // fractional coordinates.
        p = q;
        vec2 ip = floor(p);
        p -= ip + .5; // Equivalent to: fract(p) - .5;


        float oct = max((abs(p.x) + abs(p.y))/sqrt(2.), max(abs(p.x), abs(p.y))) - .5;
        d = oct;
    }

    d = abs(d) - .01;


    return d;

}
#endif


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);


    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    uv *= mat_scale;

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    float gSc = 4.5;

    uv *= gSc;

    if (mat_shift_animate) {
        float shift_time = mat_shift_time_source - mat_shift_offset;

        if (mat_shift_fixed_dir) {
            float shift_time_x = shift_time * cos(PI * mat_shift_angle);
            float shift_time_y = shift_time * sin(PI * mat_shift_angle);
            uv.x -= shift_time_x;
            uv.y -= shift_time_y;
        } else {
            uv -= vec2(cos(shift_time/3.)*1.5, -.5*shift_time);
        }

    }



    float iRes = 1.;



    // Scaling and translation.

    //rot2(3.14159/4.)*
    // Depending on perspective; Moving the oject toward the bottom left,
    // or the camera in the north east (top right) direction.

    // vec2 p = uv*gSc - vec2(cos(mat_time/3.)*1.5, -.5*mat_time);
    vec2 p = uv;

    vec2 oP = p;

    // Smoothing factor, based on resolution and scale.
    // float sf = 1./RENDERSIZE.y*gSc;
    float sf = gSc * mat_smoothing / 1000.;

    // Wobbling the coordinates, just a touch, in order to give a subtle hand drawn appearance.
    p += vec2(n2D3G(p*4.), n2D3G(p*4. + 7.3))*.025;
    // Main layer shadow distance and shadow vector. The distance and angle are chosen to suit
    // the example.
    float vsd = .08;
    vec2 vs = rot2(6.2831/8.*3.5)*vec2(0, 1);
    //
    // The top layer of the main object. Without the extrusion and shadows, this would be
    // all that you'd need.
    vec3 d = distFieldT(p);
    float svID = id; // An ID hack to distinguish between sea and land beacons.
    //
    // Extrusion layer, shadow layer and highlight layer. I think you could get away with one
    // less sample, but I was feeling lazy. At any rate, four samples is much cheaper than an
    // the work required to produce real 3D extrusion.
    vec3 d3d = distFieldT(p - vs*vsd);
    vec3 dsh = distFieldT(p - vs*vsd*2.5);
    vec3 dh = distFieldT(p + vs*vsd);

    // The same for the beacons. I've offset the beacon's top layer a bit to give the impression
    // that it's... sitting higher... I put this together a while back and I'm pretty sure I was
    // sober, so I must have had my reasons. :D
    float vsd2 = .06;
    vec3 d2 = distFieldS(p + vs/2.*vsd2);
    vec3 d3d2 = distFieldS(p - vs/2.*vsd2);
    vec3 dsh2 = distFieldS(p - vs*vsd2*2.5);
    vec3 dh2 = distFieldS(p + vs*vsd2);


    // Random beacon numbers. Some are rendered and some are not.
    float rnd = hash21(d2.yz + .07);
    float rnd2 = hash21(d2.yz + .34);
    float rndS = hash21(dsh2.yz + .07);

    // It took me a while to figure out how I'd named these -- Botton color,
    // top color and side (extruded) color. :)

    // vec3 bCol = vec3(.26, .52, 1); //vec3(.7, .55, .45);
    // vec3 tCol = vec3(.9, .7, .55); //vec3(.7, .9, .4); //vec3(.9, .8, .7);
    // vec3 sCol = vec3(1.2, .6, .4);

    vec3 bCol = mat_b_color.rgb;
    vec3 tCol = mat_t_color.rgb;
    vec3 sCol = mat_s_color.rgb;




    // Initializing the overall scene color to the blue sea color.
    vec3 col = bCol;

    // Texture coordinates -- Rotated to match the angle of the extruded layer.
    vec2 pTx = rot2(-6.2831/8.*3.5)*p;
    // Rotating the top layers an extra 45-90 degrees, but it can be anything that
    // you feel looks OK.
    vec2 pTx2 = rot2(-6.2831/8.*3.5 - 3.14159/4.)*p;

    // Extruded and top line patterns.
    float lnPat = clamp(cos(pTx.x*6.2831*22.)*2. + 1.5, 0., 1.);
    float lnPat2 = clamp(cos(pTx2.x*6.2831*22.)*2. + 1.5, 0., 1.);

    // Extruded layer texture, combined with the extruded layer line pattern.

    vec3 tx;

    if (mat_use_custom_texture) {
        tx = IMG_NORM_PIXEL(mat_texture,mod(pTx/gSc,1.0)).xyz; tx *= tx;

    } else {
        tx = IMG_NORM_PIXEL(iChannel0,mod(pTx/gSc,1.0)).xyz; tx *= tx;

    }

    tx = min(smoothstep(-.1, .3, tx), 1.);
    tx *= lnPat*.5 + .7;



    // Top layer texture, combined with the top layer line pattern.

    vec3 tx2;
    if (mat_use_custom_texture) {

        tx2 = IMG_NORM_PIXEL(mat_texture,mod(pTx2/gSc,1.0)).xyz;
        tx2 *= tx2;
    } else {
        tx2 = IMG_NORM_PIXEL(iChannel0,mod(pTx2/gSc,1.0)).xyz;
        tx2 *= tx2;
    }


    tx2 = min(smoothstep(-.1, .3, tx2), 1.);
    tx2 *= lnPat2*.3 + .8;

    // Applying some texturing to the blue sea-like layer.
    col *= tx2;

    // Varying the thickness of the layers with a time varying function. This is just
    // a little animation to add extra visual interest. Set it to zero to see what it does.
    float sFunc = (n2D3G(p/gSc*3. - mat_time/4.)*.67 + n2D3G(p/gSc*6. - mat_time/2.)*.33)*.16 * mat_amp;
    // Adding the variance to the main layers.
    d.x += sFunc;
    d3d.x += sFunc;
    dsh.x += sFunc;
    // Adding a little less to the beacon layers.
    d2.x += sFunc/1.5;
    d3d2.x += sFunc/1.5;
    dsh2.x += sFunc/1.5;


    // The main extruded layer, complete with fake AO and stroke lines.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf*4., d3d.x - vsd/2. - .07))*.5);
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, d3d.x - vsd/2. - .035)));
    col = mix(col, sCol*tx, (1. - smoothstep(0., sf, d3d.x - vsd/2.)));

    // The main object drop shadow.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf*4., dsh.x - vsd*2.5/2.))*.5);



    // The extruded layers of the beacons that sit at water level... Why they'd do that
    // is anyone's guess, but I thought it looked more interesting. :)
    if(rnd<.5){

        col = mix(col, vec3(0), (1. - smoothstep(0., sf, d3d2.x - vsd2/2. - .035)));
        col = mix(col, sCol*tx, (1. - smoothstep(0., sf, d3d2.x - vsd2/2.)));

        // Shadow.
        col = mix(col, vec3(0), (1. - smoothstep(0., sf*4., dsh2.x - vsd2*2.5/2.))*.5);
    }

    // The top layer of the main Truchet object. Commenting these out looks interesting,
    // but takes away the extruded look.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, d.x - .035))*.98);
    col = mix(col, tCol*tx2, (1. - smoothstep(0., sf, d.x)));



    // Highlighting -- This gives the scene objects a shiny specular-like rounded look.
    // Without it, everything looks extruded, but flat. Note the (450./iRes) term; It's
    // hacked in there to keep the highlighting width consistant with screen changes.
    col = mix(col, min(col*1.5, 1.), (1. - smoothstep(0., sf*4., max(-d3d.x, d.x)))*.9);
    col = mix(col, vec3(1.5)*tx2, (1. - smoothstep(0., sf*4., max(-d3d.x, d.x + vsd*(450./iRes))))*.9);



    // Beacon layer.
    if(rnd<.5 && rndS<.5){


        // Extruded beacon layers.
        if(svID>0.){
            col = mix(col, vec3(0), (1. - smoothstep(0., sf, d3d2.x - vsd2/2. - .035)));
            col = mix(col, sCol*tx, (1. - smoothstep(0., sf, d3d2.x - vsd2/2.)));

            col = mix(col, vec3(0), (1. - smoothstep(0., sf*4., dsh2.x - vsd2*2.5/2.))*.5);
        }
        // Beacon color.
        //vec3 dCol = vec3(1, rnd*.65 + .35, rnd);
        // IQ's handy palette formula.
        vec3 dCol = .525 + .425*cos(rnd2*6.2831 + vec3(11, 22, 3)/3.);

        // Beacon tops.
        col = mix(col, vec3(0), (1. - smoothstep(0., sf, d2.x - .035))*.98);
        col = mix(col, dCol*tx2, (1. - smoothstep(0., sf, d2.x)));
        //
        // Beacon top centers.
        dCol = .525 + .425*cos(rnd2*6.2831 + vec3(11, 22, 3)/3. + .25);
        float sz = (.05 + rnd2*.035);
        col = mix(col, vec3(0), (1. - smoothstep(0., sf, d2.x + sz - .035))*.98);
        col = mix(col, dCol*tx2, (1. - smoothstep(0., sf, d2.x + sz)));


        // Highlighting.
        col = mix(col, min(col*1.5, 1.), (1. - smoothstep(0., sf*4., max(-d3d2.x, d2.x)))*.9);
        col = mix(col, min(col*3., 1.), (1. - smoothstep(0., sf*4., max(-d3d2.x, d2.x + vsd2*(450./iRes))))*.9);

    }

    #ifdef SHOW_GRID
    float grid = gridField(p);
    col = mix(col, col*5., (1. - smoothstep(0., sf*2., grid - .035)));
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, grid - .0275)));
    col = mix(col, vec3(1.25)*tx2, (1. - smoothstep(0., sf, grid + .005)));
    #endif

    // Output to screen
    out_color = vec4(sqrt(max(col, 0.)), 1);




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


    return out_color;
}
