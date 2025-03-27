/*{
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/wlscWX",

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
            "LABEL": "Pyramid/Offset",
            "NAME": "mat_p_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pyramid/Deviation",
            "NAME": "mat_p_deviation",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Pyramid/Edge",
            "NAME": "mat_p_edge",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
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
            "LABEL": "Lighting/Distance",
            "NAME": "mat_p_bump",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Lighting/Speed",
            "NAME": "mat_light_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/BPM Sync",
            "NAME": "mat_light_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Lighting/Reverse",
            "NAME": "mat_light_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Lighting/Offset",
            "NAME": "mat_light_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Lighting/Strob",
            "NAME": "mat_light_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
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
        },
        {
            "NAME": "mat_light_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_light_speed",
                "speed_curve":2,
                "reverse": "mat_light_reverse",
                "strob" : "mat_light_strob",
                "bpm_sync": "mat_light_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = (mat_time_source - mat_offset * 8. * mat_offset_scale) * 0.5;

float mat_shift_time = mat_shift_time_source - mat_shift_offset;
float mat_light_time = (mat_light_time_source - mat_light_offset) * 0.5;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// From SHANE

/*

    Pyramid Pattern
    ---------------

    An animated pyramid pattern, which is based on old code that I
    quickly repurposed after looking at Oggbog's "Flipping triangles"
    example.

    I've departed from the original considerably, but it's essentially
    the same thing. The pyramid centers have been offset in correlation
    with a directional gradient that indexes an underlying noise
    function. I've applied some exaggerated bumped highlighting to give
    the pattern a sharp abstract scaly appearance.

    The cells are arranged in a square grid with each alternate row
    offset by half a cell to give a more distributed feel. In fact, I
    almost coded a hexagonal version, but figured this had more of an
    angular feel.

    I've commented the code, but there's nothing in here that's
    particularly taxing on the brain. If I had it my way, I'd code
    simple little geometric patterns all day, since it's kind of
    therapeutic, but I've got some so-called hard stuff to get back
    to... Well, it's hard for me anyway. :)



    // Here's a much simpler version.
    Offset Pyramid Squares - Shane
    https://www.shadertoy.com/view/tlXcDs

    // Great motion: I sometimes go overboard with bells and
    // whistles, whereas this is elegantly simple.
    Pyramid torsion - AntoineC
    https://www.shadertoy.com/view/lsVczK

    // Inspired by:
    Flipping triangles - Oggbog
    https://www.shadertoy.com/view/ttsyD2


*/

// Offsetting alternate rows -- I feel it distributes the effect more,
// but if you prefer more order, comment out the following:
#define OFFSET_ROW


// Standard 2D rotation formula.
mat2 rot2(in float a){ float c = cos(a), s = sin(a); return mat2(c, -s, s, c); }


// IQ's vec2 to float hash.
float hash21(vec2 p){  return fract(sin(dot(p, vec2(27.619, 57.583)))*43758.5453); }


vec2 cellID; // Individual Voronoi cell IDs.


// vec2 to vec2 hash.
vec2 hash22B(vec2 p) {

    // Faster, but doesn't disperse things quite as nicely. However, when framerate
    // is an issue, and it often is, this is a good one to use. Basically, it's a tweaked
    // amalgamation I put together, based on a couple of other random algorithms I've
    // seen around... so use it with caution, because I make a tonne of mistakes. :)
    float n = sin(dot(p, vec2(41, 289)));
    return fract(vec2(262144, 32768)*n)*2. - 1.;
    //return sin( p*6.2831853 + mat_time );
}

// Based on IQ's gradient noise formula.
float n2D3G( in vec2 p ){

    // Cell ID and local coordinates.
    vec2 i = floor(p); p -= i;

    // Four corner samples.
    vec4 v;
    v.x = dot(hash22B(i), p);
    v.y = dot(hash22B(i + vec2(1, 0)), p - vec2(1, 0));
    v.z = dot(hash22B(i + vec2(0, 1)), p - vec2(0, 1));
    v.w = dot(hash22B(i + 1.), p - 1.);

    // Cubic interpolation.
    p = p*p*(3. - 2.*p);

    // Bilinear interpolation -- Along X, along Y, then mix.
    return mix(mix(v.x, v.y, p.x), mix(v.z, v.w, p.x), p.y);

}

// Two layers of noise.
float fBm(vec2 p){ return n2D3G(p)*.66 + n2D3G(p*2.)*.34; }


float bMap(vec2 p){

    // Put the grid on an angle to interact with the light a little better.
    p *= rot2(-PI/5.);

    #ifdef OFFSET_ROW
    // Tacky way to construct an offset square grid.
    if(mod(floor(p.y), 2.)<.5) p.x += .5 * mat_p_offset;
    #endif


    // Cell ID and local coordinates.
    vec2 ip = floor(p);
    p -= ip + .5;

    // Recording the cell ID.
    cellID = ip;

    // Transcendental angle function... Made up on the spot.
    //float ang = dot(sin(ip/4. - cos(ip.yx/2. + mat_time))*6.2831, vec2(.5));

    // Noise function. I've rotated the point around a bit so that the
    // objects hang down due to gravity at the zero mark.
    float ang = -3.14159*3./5. + (fBm(ip/8. + mat_time/3.))*6.2831*2.;
    // Offset point within the cell. You could increase this to cell edges
    // (.5), but it starts to look a little weird at that point.
    vec2 offs = vec2(cos(ang), sin(ang))*.35 * mat_p_deviation;

    // Linear pyramid shading, according to the offset point. Basically, you
    // want a value of zero at the edges and a linear increase to one at the
    // offset point peak. As you can see, I've just hacked in something quick
    // that works, but there'd be more elegant ways to achieve the same.
    if(p.x<offs.x)  p.x = 1. - (p.x + .5)/abs(offs.x  + .5);
    else p.x = (p.x - offs.x)/(.5 - offs.x);

    if(p.y<offs.y) p.y = 1. - (p.y + .5)/abs(offs.y + .5);
    else p.y = (p.y - offs.y)/(.5 - offs.y);

    // Return the offset pyramid distance field. Range: [0, 1].
    return 1. - max(p.x, p.y);
}


// Standard function-based bump mapping function, with an edge value
// included for good measure.
vec3 doBumpMap(in vec2 p, in vec3 n, float bumpfactor, inout float edge){

    // Sample difference. Usually, you'd have different ones for the gradient
    // and the edges, but we're finding a happy medium to save cycles.
    vec2 e = vec2(.025, 0);

    float f = bMap(p); // Bump function sample.
    float fx = bMap(p - e.xy); // Same for the nearby sample in the X-direction.
    float fy = bMap(p - e.yx); // Same for the nearby sample in the Y-direction.
    float fx2 = bMap(p + e.xy); // Same for the nearby sample in the X-direction.
    float fy2 = bMap(p + e.yx); // Same for the nearby sample in the Y-direction.

    vec3 grad = (vec3(fx - fx2, fy - fx2, 0))/e.x/2.;

    // Edge value: There's probably all kinds of ways to do it, but this will do.
    edge = length(vec2(fx, fy) + vec2(fx2, fy2) - f*2.);
    //edge = (fx + fy + fx2 + fy2 - f*4.);
    //edge = abs(fx + fx2 - f*2.) + abs(fy + fy2 - f*2.);
    //edge /= e.x;
    edge = smoothstep(0., 1., edge/e.x) * mat_p_edge;

    // Applying the bump function gradient to the surface normal.
    grad -= n*dot(n, grad);

    // Return the normalized bumped normal.
    return normalize( n + grad*bumpfactor );

}


// A hatch-like algorithm, or a stipple... or some kind of textured pattern.
float doHatch(vec2 p, float res){


    // The pattern is physically based, so needs to factor in screen resolution.
    p *= res/16.;

    // Random looking diagonal hatch lines.
    float hatch = clamp(sin((p.x - p.y)*3.14159*200.)*2. + .5, 0., 1.); // Diagonal lines.

    // Slight randomization of the diagonal lines, but the trick is to do it with
    // tiny squares instead of pixels.
    float hRnd = hash21(floor(p*6.) + .73);
    if(hRnd>.66) hatch = hRnd;


    return hatch;


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



    // if (mat_shift_animate) {

    //     if (mat_shift_fixed_dir) {
    //         float shift_time_x = mat_shift_time * cos(PI * mat_shift_angle);
    //         float shift_time_y = mat_shift_time * sin(PI * mat_shift_angle);
    //         uv.x -= shift_time_x;
    //         uv.y -= shift_time_y;
    //     } else {
    //         uv -= vec2(cos(mat_shift_time/3.)*1.5, -.5*mat_shift_time);
    //     }

    // }



    // Unit direction vector. Used for some mock lighting.
    vec3 rd = normalize(vec3(uv, .5));

    // Scaling and tranlation.
    const float gSc = 10.;
    // vec2 p = uv*gSc + vec2(0, mat_shift_time/2.);
    vec2 p = uv*gSc;

    if (mat_shift_animate) {
        float shift_time_x = mat_shift_time * cos(PI * mat_shift_angle/360.0 * 2.);
        float shift_time_y = mat_shift_time * sin(PI * mat_shift_angle/360.0 * 2.);

        p += vec2(shift_time_x, shift_time_y) / 2.;

    }

    vec2 oP = p; // Saving a copy for later.


    // Take a function sample.
    float m = bMap(p);

    vec2 svID = cellID;

    // Face normal for and XY plane sticking out of the screen.
    vec3 n = vec3(0, 0, -1);

    // Bump mapping the normal and obtaining an edge value.
    float edge = 0., bumpFactor = .25 * mat_p_bump;
    n = doBumpMap(p, n, bumpFactor, edge);

    // Light postion, sitting back from the plane and animated slightly.
    vec3 lp =  vec3(-0. + sin(mat_light_time)*.3, .0 + cos(mat_light_time*1.3)*.3, -1) - vec3(uv, 0);

    // Liight distance and normalizing.
    float lDist = max(length(lp), .001);
    vec3 ld = lp/lDist;
    // Unidirectional lighting -- Sometimes, it looks nicer.
    //vec3 ld = normalize(vec3(-.3 + sin(mat_light_time)*.3, .5 + cos(mat_light_time*1.3)*.2, -1));

    // Diffuse, specular and Fresnel.
    float diff = max(dot(n, ld), 0.);
    diff = pow(diff, 4.);
    float spec = pow(max(dot(reflect(-ld, n), -rd), 0.), 16.);
    // Fresnel term. Good for giving a surface a bit of a reflective glow.
    float fre = min(pow(max(1. + dot(n, rd), 0.), 4.), 3.);

    // Applying the lighting.
    vec3 col = vec3(.15)*(diff + .251 + spec*vec3(1, .7, .3)*9. + fre*vec3(.1, .3, 1)*12.);


    // Some dodgy fake reflections. This was made up on the fly. It's no sustitute for reflecting
    // into a proper back scene, but it's only here to add some subtle red colors.
    float rf = smoothstep(0., .35, bMap(reflect(rd, n).xy*2.)*fBm(reflect(rd, n).xy*3.) + .1);
    col += col*col*rf*rf*vec3(1, .1, .1)*15.;

    /*
    // Random blinking lights. Needs work. :)
    float rnd = hash21(svID);
    float rnd2 = hash21(svID + .7);
    rnd = sin(rnd*6.2831 + mat_time*1.);
    col *= mix(vec3(1), (.5 + .4*cos(6.2831*rnd2 + vec3(0, 1, 2)))*6., smoothstep(.96, .99, rnd));
    */

     // Using the distance function value for some faux shading.
    float shade = m*.83 + .17;
    col *= shade;

    // Apply the edging from the bump function. In some situations, this can add an
    // extra touch of dimension. It's so easy to apply that I'm not sure why people
    // don't use it more. Bump mapped edging works in 3D as well.
    col *= 1. - edge*.8;

    // Apply a cheap but effective hatch function.

    float res = 1.;;
    float hatch = doHatch(oP/gSc, res);
    col *= hatch*.5 + .7;

    // Just the distance function.
    //col = vec3(m);


    // Subtle vignette.
    //uv = gl_FragCoord.xy/RENDERSIZE.xy;
    //col *= pow(16.*uv.x*uv.y*(1. - uv.x)*(1. - uv.y) , .125);
    // Colored variation.
    //col = mix(col*vec3(.25, .5, 1)/8., col, pow(16.*uv.x*uv.y*(1. - uv.x)*(1. - uv.y) , .125));

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
