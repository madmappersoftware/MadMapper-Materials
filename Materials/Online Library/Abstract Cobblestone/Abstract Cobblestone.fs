/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/NsKSRz",

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
            "LABEL": "Cobblestone/Shape",
            "NAME": "mat_shape",
            "TYPE": "long",
            "VALUES": ["Square","Diamond","Hexagon","Octagon","Circle"],
            "DEFAULT": "Square"
        },

        {
            "LABEL": "Cobblestone/Cycle 1",
            "NAME": "mat_cycle_1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Cobblestone/Cycle 2",
            "NAME": "mat_cycle_2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Cobblestone/Cycle 3",
            "NAME": "mat_cycle_3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Cobblestone/Smooth",
            "NAME": "mat_smooth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },






        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_shift_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "Label": "Scroll/Direction",
            "NAME": "mat_shift_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 360.0,
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
            "Label": "Flicker/Power",
            "NAME": "mat_flicker_sat",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Flicker/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Flicker/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Flicker/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Flicker/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Flicker/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Palette",
            "NAME": "mat_palette",
            "TYPE": "long",
            "VALUES": ["Colorful","Earth"],
            "DEFAULT": "Colorful"
        },

        {
            "NAME": "mat_back_level",
            "LABEL": "Color/Back Overlay",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.15,
                0.15,
                0.15,
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8.;

float mat_shift_time = mat_shift_time_source - mat_shift_offset;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

float mat_luma(vec3 color) {
  return dot(color, vec3(0.299, 0.587, 0.114));
}

float mat_luma(vec4 color) {
  return dot(color.rgb, vec3(0.299, 0.587, 0.114));
}

// From SHANE

/*

    Abstract Geometric Pattern
    --------------------------

    Applying a very simple procedure involving multiple colored square overlays
    and basic lighting to produce a greeble-like surface in the style of abstract
    art... or something to that effect. :)

    I'd been experimenting with procedural greebled surfaces a while ago, and this
    was just a byproduct of that. I wouldn't call it a greebled surface per se, but
    if you mapped it onto a 3D surface, it'd have that feel. Thanks to the basic
    lighting, it also has a painted feel. On a side note, a lot of wrapped
    procedural textures you see have usually been highlighted in a similar wrappable
    unidirectional way.

    The technique is about as simple as it gets: Combine some offset squares to
    form a polygon with squared sides. Construct a square grid and render these
    in each cell. After that, combine a few resultant layers at various frequencies.
    The method is different, but similar to stacking Voronoi layers.

    I didn't put a lot of effort into rendering speed, as this is the kind of thing
    that you should probably prerender into one of the buffers prior to use. By the
    way, I have a couple of 3D greeble examples that I'll post later.


    Other examples:
    // Similar principals, and a nice result. By the way Piyushslayer has
    // some other really nice examples.
    Abstract Squircles - piyushslayer
    https://www.shadertoy.com/view/Wtc3D8


*/


// Just the two palattes - Vibrant: 0, Earth: 1.
// #define PALETTE 0

// Base shape -  Square: 0, Diamond: 1., Hexagon: 2, Octagon: 3, Circle: 4.
// #define SHAPE 4


// IQ's vec2 to float hash.
float hash21(vec2 p){ return fract(sin(dot(p, vec2(27.609, 57.583)))*43758.5453); }


// Compact, self-contained version of IQ's 2D value noise function.
float n2D(vec2 p){

    // Setup.
    // Any random integers will work, but this particular
    // combination works well.
    const vec2 s = vec2(1, 113);
    // Unique cell ID and local coordinates.
    vec2 ip = floor(p); p -= ip;
    // Vertex IDs.
    vec4 h = vec4(0., s.x, s.y, s.x + s.y) + dot(ip, s);

    // Smoothing.
    p = p*p*(3. - 2.*p);
    //p *= p*p*(p*(p*6. - 15.) + 10.); // Smoother.

    // Random values for the square vertices.
    h = fract(sin(h)*43758.5453);

    // Interpolation.
    h.xy = mix(h.xy, h.zw, p.y);
    return mix(h.x, h.y, p.x); // Output: Range: [0, 1].
}

// FBM -- Accumulated noise layers of modulated amplitudes and frequencies.
float fBm(vec2 p){

    // Layer the noise.
    float ns = 0., sum = 0., a = 1.;
    for(int i = 0; i<5; i++){

        ns += n2D(p)*a;
        p *= 2.5;
        sum += a;
        a /= 1.5;
    }

    return ns/sum; // Range: [0, 1].
}

/*
float sBoxS(in vec2 p, in vec2 b, in float rf){

  vec2 d = abs(p) - b + rf;
  return min(max(d.x, d.y), 0.) + length(max(d, 0.)) - rf;

}
*/

// Shape distance metrics. You could put whatever you feel like here.
float dist(vec2 p, vec2 b){


    if (mat_shape == 0) {
        // Square.
        p = abs(p) - b;
        return max(p.x, p.y);
    } else if (mat_shape == 1) {
          // Diamond.
        p = abs(p);
          return (p.x + p.y)*.7071 - mix(b.x, b.y, .5);
    } else if (mat_shape == 2) {
          // Hexagon.
        p = abs(p);
        return max(p.x*.8660254 + p.y*.5, p.y) - mix(b.x, b.y, .5);
    } else if (mat_shape == 3) {
        // Octagon.
        p = abs(p);
        return max((p.x + p.y)*.7071, max(p.x, p.y)) - mix(b.x, b.y, .5);
    } else {
        // Circle.
        return length(p) - (b.x + b.y)/2.;
    }

      //p = abs(p) - b;
      //p = p*.8660254 + p.yx*.5;
      //return max(p.x, p.y);

      //p = abs(p)-b;
      //p = (p + p.yx)*.7071;
      //return max(p.x, p.y);
}


// The cell shape: This is just a union of four offset shapes, but it could
// be anything you can dream up. This particular random shape exceeds the
// boundaries of the cell, so surrounding cells need to be accounted for.

float cellShape(vec2 p, vec2 ip){

    float d = 1e5;

    for(int i = 0; i<4; i++){

        // Four random values.
        float fi = 1./(1. + float(i));
        vec4 rnd = vec4(hash21(ip + .15*fi), hash21(ip + .23*fi),
                        hash21(ip + .32*fi), hash21(ip + .41*fi));

        // Offset position.
        vec2 q = p - (rnd.xy - .5)*1.5 * mat_cycle_1;

        // Render a square, diamond, or whatever.
        //float shape = sBoxS(q, .1 + rnd.zw*.3, min(rnd.z, rnd.w)*.07);
        float shape = dist(q, .1 + rnd.zw*.3); // Rectangular dimensions.
        //float shape = dist(q, vec2(.1 + length(rnd.zw)*.2)); // Square.

        // Take the minimum (union) of all combined shapes.
        d = min(d, shape);

    }

    // Holes... Didn't work. :)
    //d = abs(d  + .135) - .135;

    return d; // Return the random distance, or bound.

}



// Constructing the grid pattern: Render a random shape in each cell.
vec4 gridPattern(vec2 p, float sf){

    const vec2 sc = vec2(1);

    vec3 col = vec3(1);

    float alpha = .9;
    float d = 1e5;

    // The cell shapes exceed the cell boundaries, which means covering all
    // surrounding cells that the shape covers. In this case, there are 9.
    for(int i=-1; i<=1; i++){
        for(int j=-1; j<=1; j++){

            // Local cell coordinates and cell ID.
            vec2 cntr = vec2(i, j) - .5;
            vec2 q = p;
            vec2 ip = floor(q/sc - cntr) + .5;
            q -= ip*sc;

            // Cell shape.
            float c = cellShape(q, ip);

            // Using the cell ID to produce some random number.
            vec2 rnd = vec2(hash21(ip/23.), hash21(ip/113.)) - .5;

            rnd.y = mat_flicker_sat * smoothstep(.985, .997, sin(rnd.y*6.2831 + mat_time/2.)*.5  + .5 ); // saturation?

            vec3 cellCol;

            // Feeding the random cell-ID-based value into IQ's cool palette formula
            // to produce the shape color.
            if (mat_palette == 1) {
                // Earth tones.
                cellCol = (.5 + .46*cos(rnd.x*6.2831/2. + vec3(0, 1, 2)))/3.5;
                // Blinking highlights.
                cellCol = mix(cellCol, mix(cellCol, cellCol*vec3(1.8, .9, .3), 1.), rnd.y);
            } else {
                // Vibrant palette.
                cellCol = (.5 + .46*cos(rnd.x*6.2831/2.75 + vec3(0, 1, 2) + 2.2));
                // Blinking highlights.
                cellCol = mix(cellCol, mix(cellCol, cellCol.xzy, .6), rnd.y);
            }

            // Greyscale.
            //cellCol = vec3(dot(cellCol, vec3(.299, .587, .114)))/1.5;

            // Using the shape distance to produce a bit of shading.
            float sh = max(.5 - c/.1, 0.)*max(1. - dot(q, q)*.5, 0.)*2.5;

            // Rendering to the canvas. This is the way I like to do it, but you can
            // use whatever system you're comfortable with. The following are just some
            // Photoshop type laters: Shadow, outer edge, shaded color and an inner
            // edge for decoration.
            col = mix(col, vec3(0), (1. - smoothstep(0., sf*8., abs(c) - .01))*.5*alpha);
            col = mix(col, vec3(0), (1. - smoothstep(0., sf, abs(c) - .01)));
            col = mix(col, cellCol*sh, (1. - smoothstep(0., sf, c))*alpha);//
            col = mix(col, vec3(0), (1. - smoothstep(0., sf, abs(c + .06) - .005)));

            // Keeping a copy of the minimum overall distance for the layering routine.
            d = min(d, c);

        }
    }

    // Return the shaded shape color and associated distance.
    return vec4(col, d);

}

// Layering the grid pattern above.
vec3 layeredPattern(vec2 p, vec3 col, float sf){


    // Frequency (associated with scale) and alpha (transparency) values.
    float freq = 1., alpha = 1.;

    for(int i=0; i<4; i++){

        // Random values based on layer count.
        float fi = 1./(1. + float(i));
        vec2 rnd = vec2(hash21(vec2(0) + .1*fi), hash21(vec2(0) + .2*fi))*2.;

        // Render this particular grid layer.

        // Color and distance value.
        vec4 gCol = gridPattern((p - rnd)*freq, sf*freq);

        // Mix a drop shadow, edge and layer color onto the previous layer.
        col = mix(col, vec3(0), (1. - smoothstep(0., sf*8.*freq, abs(gCol.w) - .01))*.5*alpha);
        col = mix(col, vec3(0), (1. - smoothstep(0., sf*freq, abs(gCol.w) - .01))*alpha);
        col = mix(col, gCol.xyz, (1. - smoothstep(0., sf*freq, gCol.w))/freq*alpha);

        // Increace the frequency
        freq *= 1.4 + mat_cycle_3 - 1.;
        alpha *= .92 * mat_cycle_2;
    }

    /*
    // Overlayed diagonal stripe pattern.
    float pat = (abs(fract((p.x - p.y)*64.) - .5)*2. - .125)/64./2.;
    pat = smoothstep(0., sf, pat)*.5 + .65;
    col *= pat;
    */

    return col; // Overall texture value.

}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);


    uv *= mat_scale;

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);

    // Allow for more logical XY shifts in the presence of a custom rotation (for user input)
    uv_shift += vec2(0.5);
    uv_shift = matRot2D(uv_shift, 2*PI*(mat_rotate) / 360);
    uv_shift -= vec2(0.5);

    uv += uv_shift;

    // Scale and translation.
    const float sc = 1.5;
    // vec2 p = uv*sc + vec2(1, .25)*mat_time/5.;

    vec2 p = uv*sc;

    if (mat_shift_animate) {
        float shift_time_x = mat_shift_time * cos(PI * mat_shift_angle/360.0 * 2.);
        float shift_time_y = mat_shift_time * sin(PI * mat_shift_angle/360.0 * 2.);
        p += vec2(shift_time_x, shift_time_y) / 5.;

    }

    // Coordinate perturbation.
    p += vec2(n2D(p*24.), n2D(p*24. + 5.))*.004;

    // Smoothing factor.
    // float sf = sc/RENDERSIZE.y;

    float sf = sc/(1000. / pow(mat_smooth,2.));


    // Background color -- Most of this will be covered with colored shapes.
    // vec3 bg = vec3(.05);
    vec3 bg = vec3(0.05) * mat_back_level;

    // Two layered pattern samples. The second will be used for
    // highlighting.
    float sDist = 3./450.;
    vec3 col = layeredPattern(p, bg, sf);
    vec3 col2 = layeredPattern(p - normalize(vec2(1, 1))*sDist, bg, sf);

    // Two bump values. One from each direction.
    float b = max(dot(col2 - col, vec3(.299, .587, .114)), 0.)/sDist;
    float b2 = max(dot(col - col2, vec3(.299, .587, .114)), 0.)/sDist;


    // Applying some subtle fBm noise.
    float ns = fBm(p*40.)*.8 + .5;
    col *= ns;

    // Adding the bump highlights.
    col = col*(vec3(.2, .4, 1)*b*b*.005 + vec3(1, .15, .05)*b2*.04 + .6);

    /*
    // Subtle diagonal pattern overlay. Not used.
    float pat = (abs(fract((p.x - p.y)*64.) - .5)*2. - .125)/64./2.;
    pat = .25 + smoothstep(0., sf, pat);
    col *= pat;
    */

    /*
    // Cell borders.
    vec2 q = abs(fract(p) - .5);
    float bord = max(q.x, q.y) - .5;
    col = mix(col, vec3(1), (1. - smoothstep(0., sf*4., abs(bord) - .0025))*.75);
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, abs(bord) - .0025)));
    */


    // Subtle vignette.
    // uv = gl_FragCoord.xy/RENDERSIZE.xy;

    vec2 uvn = texCoord;

    // col *= pow(16.*uv_alt.x*uv.y*(1. - uv_alt.x)*(1. - uv_alt.y) , .0625);
    // Colored variation.
    // col = mix(col*vec3(1, .05, .15), col, pow(16.*uv.x*uv.y*(1. - uv.x)*(1. - uv.y) , .125));


    // Rought gamma correction.
    out_color = vec4(sqrt(max(col, 0.)), 1);



    vec4 out_orig = out_color;
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

    if (mat_luma(out_color) < 0.02) {
        out_color = mat_back_color;
    }

    return out_color;
}
