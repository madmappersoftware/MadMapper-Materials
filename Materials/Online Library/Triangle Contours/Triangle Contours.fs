/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "From https:\/\/www.shadertoy.com\/view\/WtfGDX",

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
            "LABEL": "Contours/Water",
            "NAME": "mat_water_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Water Edge",
            "NAME": "mat_water_edge_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Beach",
            "NAME": "mat_beach_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Beach Edge",
            "NAME": "mat_beach_edge_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Grass",
            "NAME": "mat_grass_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Terrain",
            "NAME": "mat_terrain_level",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Smooth 1",
            "NAME": "mat_smooth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Contours/Smooth 2",
            "NAME": "mat_smooth2",
            "TYPE": "float",
            "MIN": 0.01,
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
    uv *= mat_scale * 0.75;

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


    Triangle Grid Contouring
    ------------------------

    Using a 2D simplex grid to construct the isolines of a 2D field function, namely
    gradient noise. I'm not entirely sure what to call the process. Since it's the
    triangular version of the marching squares algorithm, you'd think it'd be called
    "marching triangles," but that term is used to describe grid point cloud related
    triangulation. Therefore, "triangle grid contouring" will do. :)

    I've been coding up some Wang tile related patterns on square grids lately, which got
    me thinking about attempting the same on a triangle grid. Whilst doing that, I got
    sidetracked and wondered what contour lines created with the triangular equivalent of
    a marching squares algorithm would look like, and here we are. :)

    I put this together for novelty purposes, but I'd imagine there'd be some practical
    aspects associated with it; Vector contour point lists would be an obvious one, and
    to a lesser extent, triangulated height maps. However, rendering smooth curves would
    be one of the main benefits. Only one unique linear interpolant is rendered through
    each triangle, which means that Bezier point information via neighboring triangles
    with shared edges would be easy to obtain.... I might demonstrate that at a later
    date, but for now, a novel proof of concept will do.


*/


// If you were rendering from a vertex shader, or just pushing out a triangle list
// general, then you'd need to triangulate the triangles that have been split into
// quads. The process is almost trivial with just one contour, and slightly more
// involved with two, but not too difficult. Anyway, here's a visual representation.
// Aesthetically, I kind of like it, but it's a little busy, so is off by default.
//#define TRIANGULATE_CONTOURS

// Filling the cells with a concentric triangle pattern. I couldn't decide whether
// to include it, or not, so it's here as an option.
#define TRIANGLE_PATTERN

// Render green grass on the terrain. Uncommented leaves dry terrain.
#define GRASS

// Standard 2D rotation formula.
mat2 rot2(in float a){ float c = cos(a), s = sin(a); return mat2(c, -s, s, c); }


// Standard vec2 to float hash - Based on IQ's original.
float hash21(vec2 p){ return fract(sin(dot(p, vec2(141.13, 289.97)))*43758.5453); }


// vec2 to vec2 hash.
vec2 hash22(vec2 p) {

    // Faster, but doesn't disperse things quite as nicely. However, when framerate
    // is an issue, and it often is, this is a good one to use. Basically, it's a tweaked
    // amalgamation I put together, based on a couple of other random algorithms I've
    // seen around... so use it with caution, because I make a tonne of mistakes. :)
    float n = sin(dot(p, vec2(41, 289)));
    //return fract(vec2(262144, 32768)*n)*2. - 1.;

    // Animated.
    p = fract(vec2(262144, 32768)*n);
    return sin(p*6.2831853 + mat_time);

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


// The isofunction. Just a single noise function, but it can be more elaborate.
float isoFunction(in vec2 p){ return n2D3G(p/4. + .07); }


// Unsigned distance to the segment joining "a" and "b".
float distLine(vec2 a, vec2 b){


    b = a - b;
    float h = clamp(dot(a, b)/dot(b, b), 0., 1.);
    return length(a - b*h);
}


// Based on IQ's signed distance to the segment joining "a" and "b".
float distEdge(vec2 a, vec2 b){

    //if(abs(dot(a, a) - dot(b, b))>1e-5)



    return dot((a + b)*.5, normalize((b - a).yx*vec2(-1, 1)) );
    //else return 1e5;

}



// Interpolating along the edge connecting vertices v1 and v2 with respect to the isovalue.
vec2 inter(in vec2 p1, in vec2 p2, float v1, float v2, float isovalue){

    // The interpolated point will fall somewhere between the vertex points p1 and p2.
    // Obviously if the isovalue is closer to p1, then the interpolated point will be
    // closer to p1, and vice versa.
    //
    // If you're wondering about the weird numerical hacks on the end, it's a fudge keep the
    // lines away from the triangle edges. Because this is a per grid cell implementation,
    // there's neighboring cell overlap to deal with, which basically means rendering more
    // cells. Typically, that's not particularly difficult to deal with, but can be slower.
    // Either way, I wanted to keep things simple... and I'm lazy. Hence, the fugde. :)
    return mix(p1, p2, (isovalue - v1)/(v2 - v1)*.75 + .25/2.);

    // The mix bit -- without the numberical hacks -- is equivalent to:
    //return p1 + (isovalue - v1)/(v2 - v1)*(p2 - p1);

    // This is probably more correct, but we seem to be getting away with the line above.
    //float inter = v1 == v2 ? .5 : (isovalue - v1) /(v2 - v1);
    //return mix(p1, p2, inter);
}

// Isoline function.
int isoLine(vec3 n3, vec2 ip0, vec2 ip1, vec2 ip2, float isovalue, float i,
          inout vec2 p0, inout vec2 p1){


    // Points where the lines cut the edges.
    p0 = vec2(1e5), p1 = vec2(1e5);

    // Marching triangles.. Is that a thing? Either way, it's similar to marching
    // squares, but with triangles. In other words, obtain the underlying function
    // value at all three vertices of the triangle cell, compare them to the
    // isovalue (over or under), then render a line between the corresponding edges.
    //
    // The line cuts each edge in accordance with the isovalues at each edge, which
    // means interpolating between the two.

    // Bitwise accumulation to produce a unique index number upon which to make
    // decisions. It's a pretty standard technique.
    //
    // Minumum threshold value... It's an ID, of sorts.
    int iTh = 0;
    //
    // If the first vertex is over the isovalue threshold, add four, etc.
    if(n3.x>isovalue) iTh += 4;
    if(n3.y>isovalue) iTh += 2;
    if(n3.z>isovalue) iTh += 1;


    // A value of 1 or 6 means constructing a line between the
    // second and third edges, and so forth.
    if(iTh == 1 || iTh == 6){ // 12-20

        p0 = inter(ip1, ip2, n3.y, n3.z, isovalue); // Edge two.
        p1 = inter(ip2, ip0, n3.z, n3.x, isovalue); // Edge three.

    }
    else if(iTh == 2 || iTh == 5){ // 01-12

        p0 = inter(ip0, ip1, n3.x, n3.y, isovalue); // Edge one.
        p1 = inter(ip1, ip2, n3.y, n3.z, isovalue); // Edge two.

    }
    else if(iTh == 3 || iTh == 4){ // 01-20

        p0 = inter(ip0, ip1, n3.x, n3.y, isovalue); // Edge one.
        p1 = inter(ip2, ip0, n3.z, n3.x, isovalue); // Edge three.

    }


    // For the last three cases, we're after the other side of
    // the line, and this is a quick way to do that. Uncomment
    // to see why it's necessary.
    if(iTh>=4 && iTh<=6){ vec2 tmp = p0; p0 = p1; p1 = tmp; }

    // Just to make things more confusing, it's necessary to flip coordinates on
    // alternate triangles, due to the simplex grid triangle configuration. This
    // line basically represents an hour of my life that I won't get back. :D
    if(i == 0.){ vec2 tmp = p0; p0 = p1; p1 = tmp; }


    // Return the ID, which will be used for rendering purposes.
    return iTh;


}

/*
vec3 softLight(vec3 s, vec3 d){

    vec3 a = d - (1. - 2.*s)*d*(1. - d), b = d + (2.*s - 1.)*d*((16.*d - 12.)*d + 3.),
         c = d + (2.*s - 1.)*(sqrt(d) - d);

    return vec3(s.x<.5? a.x : d.x<.25? b.x : c.x, s.y<.5? a.y : d.y<.25? b.y : c.y,
                s.z<.5? a.z : d.z<.25? b.z : c.z);

}
*/

vec3 simplexContour(vec2 p){



    // Scaling constant.
    const float gSc = 8.;
    p *= gSc;


    // Keeping a copy of the orginal position.
    vec2 oP = p;

    // Wobbling the coordinates, just a touch, in order to give a subtle hand drawn appearance.
    p += vec2(n2D3G(p*3.5), n2D3G(p*3.5 + 7.3))*.015;



    // SIMPLEX GRID SETUP

    vec2 s = floor(p + (p.x + p.y)*.36602540378); // Skew the current point.

    p -= s - (s.x + s.y)*.211324865; // Use it to attain the vector to the base vertex (from p).

    // Determine which triangle we're in. Much easier to visualize than the 3D version.
    float i = p.x < p.y? 1. : 0.; // Apparently, faster than: i = step(p.y, p.x);
    vec2 ioffs = vec2(1. - i, i);

    // Vectors to the other two triangle vertices.
    vec2 ip0 = vec2(0), ip1 = ioffs - .2113248654, ip2 = vec2(.577350269);


    // Centralize everything, so that vec2(0) is in the center of the triangle.
    vec2 ctr = (ip0 + ip1 + ip2)/3.; // Centroid.
    //
    ip0 -= ctr; ip1 -= ctr; ip2 -= ctr; p -= ctr;



    // Take a function value (noise, in this case) at each of the vertices of the
    // individual triangle cell. Each will be compared the isovalue.
    vec3 n3;
    n3.x = isoFunction(s);
    n3.y = isoFunction(s + ioffs);
    n3.z = isoFunction(s + 1.);


    // Various distance field values.
    float d = 1e5, d2 = 1e5, d3 = 1e5, d4 = 1e5, d5 = 1e5;


    // The first contour, which separates the terrain (grass or barren) from the beach.
    float isovalue = 0.;

    // The contour edge points that the line will run between. Each are passed into the
    // function below and calculated.
    vec2 p0, p1;

    // The isoline. The edge values (p0 and p1) are calculated, and the ID is returned.
    int iTh = isoLine(n3, ip0, ip1, ip2, isovalue, i, p0, p1);

    // The minimum distance from the pixel to the line running through the triangle edge
    // points.
    d = min(d, distEdge(p - p0, p - p1));



    //if(iTh == 0) d = 1e5;

    // Totally internal, which means a terrain (grass) hit.
    if(iTh == 7){ // 12-20

        // Triangle.
        //d = min(min(distEdge(p - ip0, p - ip1), distEdge(p - ip1, p - ip2)),
                  //distEdge(p - ip0, p - ip2));

        // Easier just to set the distance to a hit.
        d = 0.;
    }



    // Contour lines.
    d3 = min(d3, distLine((p - p0), (p - p1)));
    // Contour points.
    d4 = min(d4, min(length(p - p0), length(p - p1)));





    // Displaying the 2D simplex grid. Basically, we're rendering lines between
    // each of the three triangular cell vertices to show the outline of the
    // cell edges.
    float tri = min(min(distLine(p - ip0, p - ip1), distLine(p - ip1, p - ip2)),
                  distLine(p - ip2, p - ip0)) / mat_smooth2;

    // Adding the triangle grid to the d5 distance field value.
    d5 = min(d5, tri);


    // Dots in the centers of the triangles, for whatever reason. :) Take them out, if
    // you prefer a cleaner look.
    d5 = min(d5, length(p) - .02);

    ////////
    #ifdef TRIANGULATE_CONTOURS
    vec2 oldP0 = p0;
    vec2 oldP1 = p1;

    // Contour triangles: Flagging when the triangle cell contains a contour line, or not.
    float td = (iTh>0 && iTh<7)? 1. : 0.;

    // Subdivide quads on the first contour.
    if(iTh==3 || iTh==5 || iTh==6){

        // Grass (non-beach land) only quads.
        vec2 pt = p0;
        if(i==1.) pt = p1;
        d5 = min(d5, distLine((p - pt), (p - ip0)));
        d5 = min(d5, distLine((p - pt), (p - ip1)));
        d5 = min(d5, distLine((p - pt), (p - ip2)));
    }
    #endif
    ////////


    // The second contour: This one demarcates the beach from the sea.
    isovalue = -.15;

    // The isoline. The edge values (p0 and p1) are calculated, and the ID is returned.
    int iTh2 = isoLine(n3, ip0, ip1, ip2, isovalue, i, p0, p1);

    // The minimum distance from the pixel to the line running through the triangle edge
    // points.
    d2 = min(d2, distEdge(p - p0, p - p1));

    // Make a copy.
    float oldD2 = d2;

    if(iTh2 == 7) d2 = 0.;
    if(iTh == 7) d2 = 1e5;
    d2 = max(d2, -d);


    // Contour lines - 2nd (beach) contour.
    d3 = min(d3, distLine((p - p0), (p - p1)));
    // Contour points - 2nd (beach) contour.
    d4 = min(d4, min(length(p - p0), length(p - p1)));

    d4 -= .075;
    d3 -= .0125;

    ////////
    #ifdef TRIANGULATE_CONTOURS
    // Triangulating the contours.

    // This logic was put in at the last minute, and isn't my finest work. :)
    // It seems to work, but I'd like to tidy it up later.

    // Flagging when the triangle contains a second contour line, or not.
    float td2 = (iTh2>0 && iTh2<7)? 1. : 0.;


    if(td==1. && td2==1.){
        // Both contour lines run through a triangle, so you need to do a little more
        // subdividing.

        // The beach colored quad between the first contour and second contour.
        d5 = min(d5, distLine(p - p0, p - oldP0));
        d5 = min(d5, distLine(p - p0, p - oldP1));
        d5 = min(d5, distLine(p - p1, p - oldP1));

        // The quad between the water and the beach.
        if(oldD2>0.){
            vec2 pt = p0;
            if(i==1.) pt = p1;
            d5 = min(d5, distLine(p - pt, p - ip0));
            d5 = min(d5, distLine(p - pt, p - ip1));
            d5 = min(d5, distLine(p - pt, p - ip2));
        }
    }
    else if(td==1. && td2==0.){

        // One contour line through the triangle.

        // Beach and grass quads.
        vec2 pt = oldP0;
        if(i==1.) pt = oldP1;
        d5 = min(d5, distLine(p - pt, p - ip0));
        d5 = min(d5, distLine(p - pt, p - ip1));
        d5 = min(d5, distLine(p - pt, p - ip2));
    }
    else if(td==0. && td2==1.){

        // One contour line through the triangle.

        // Beach and water quads.
        vec2 pt = p0;
        if(i==1.) pt = p1;
        d5 = min(d5, distLine(p - pt, p - ip0));
        d5 = min(d5, distLine(p - pt, p - ip1));
        d5 = min(d5, distLine(p - pt, p - ip2));
    }

    #endif
    ////////


    // The screen coordinates have been scaled up, so the distance values need to be
    // scaled down.
    d /= gSc;
    d2 /= gSc;
    d3 /= gSc;
    d4 /= gSc;
    d5 /= gSc;



    // Rendering - Coloring.

    // Initial color.
    vec3 col = vec3(1, .85, .6);

    // Smoothing factor.
    float sf = .004 * mat_smooth;

    // Water.
    if(d>0. && d2>0.) col = mat_water_level*vec3(1, 1.8, 3)*.45;
     // Water edging.
    if(d>0.) col = mix(col, mat_water_edge_level*vec3(1, 1.85, 3)*.3, (1. - smoothstep(0., sf, d2 - .012)));

    // Beach.
    col = mix(col, mat_beach_level*vec3(1.1, .85, .6),  (1. - smoothstep(0., sf, d2)));
    // Beach edging.
    col = mix(col, mat_beach_edge_level*vec3(1.5, .9, .6)*.6, (1. - smoothstep(0., sf, d - .012)));

    #ifdef GRASS
    // Grassy terrain.
    col = mix(col, mat_grass_level*vec3(1, .8, .6)*vec3(.7, 1., .75)*.95, (1. - smoothstep(0., sf, d)));
    #else
    // Alternate barren terrain.
    col = mix(col, mat_terrain_level*vec3(1, .82, .6)*.95, (1. - smoothstep(0., sf, d)));
    #endif




    // Abstract shading, based on the individual noise height values for each triangle.
    if(d2>0.) col *= (abs(dot(n3, vec3(1)))*1.25 + 1.25)/2.;
    else col *= max(2. - (dot(n3, vec3(1)) + 1.45)/1.25, 0.);

    // More abstract shading.
    //if(iTh!=0) col *= float(iTh)/7.*.5 + .6;
    //else col *= float(3.)/7.*.5 + .75;


    ////////
    #ifdef TRIANGULATE_CONTOURS
    //if(td==1. || td2==1.) col *= vec3(1, .4, .8);
    #endif
    ////////

    ////////
    #ifdef TRIANGLE_PATTERN
    // A concentric triangular pattern.
    float pat = abs(fract(tri*12.5 + .4) - .5)*2.;
    col *= pat*.425 + .75;
    #endif
    ////////




    // Triangle grid overlay.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, d5))*.95);



    // Lines.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, d3)));


    // Dots.
    col = mix(col, vec3(0), (1. - smoothstep(0., sf, d4)));
    col = mix(col, vec3(1), (1. - smoothstep(0., sf, d4 + .005)));



    // Rough pencil color overlay... The calculations are rough... Very rough, in fact,
    // since I'm only using a small overlayed portion of it. Flockaroo does a much, much
    // better pencil sketch algorithm here:
    //
    // When Voxels Wed Pixels - Flockaroo
    // https://www.shadertoy.com/view/MsKfRw
    //
    // Anyway, the idea is very simple: Render a layer of noise, stretched out along one
    // of the directions, then mix a similar, but rotated, layer on top. Whilst doing this,
    // compare each layer to it's underlying grey scale value, and take the difference...
    // I probably could have described it better, but hopefully, the code will make it
    // more clear. :)
    //
    // Tweaked to suit the brush stroke size.
    vec2 q = oP*1.5;
    // I always forget this bit. Without it, the grey scale value will be above one,
    // resulting in the extra bright spots not having any hatching over the top.
    col = min(col, 1.);
    // Underlying grey scale pixel value -- Tweaked for contrast and brightness.
    float gr = sqrt(dot(col, vec3(.299, .587, .114)))*1.25;
    // Stretched fBm noise layer.
    float ns = (n2D3G(q*4.*vec2(1./3., 3))*.64 + n2D3G(q*8.*vec2(1./3., 3))*.34)*.5 + .5;
    // Compare it to the underlying grey scale value.
    ns = gr - ns;
    //
    // Repeat the process with a rotated layer.
    q *= rot2(3.14159/3.);
    float ns2 = (n2D3G(q*4.*vec2(1./3., 3))*.64 + n2D3G(q*8.*vec2(1./3., 3))*.34)*.5 + .5;
    ns2 = gr - ns2;
    //
    // Mix the two layers in some way to suit your needs. Flockaroo applied commone sense,
    // and used a smooth threshold, which works better than the dumb things I was trying. :)
    ns = smoothstep(0., 1., min(ns, ns2)); // Rough pencil sketch layer.
    //
    // Mix in a small portion of the pencil sketch layer with the clean colored one.
    col = mix(col, col*(ns + .35), .4);
    // Has more of a colored pencil feel.
    //col *= vec3(.8)*ns + .5;
    // Using Photoshop mixes, like screen, overlay, etc, gives more visual options. Here's
    // an example, but there's plenty more. Be sure to uncomment the "softLight" function.
    //col = softLight(col, vec3(ns)*.75);
    // Uncomment this to see the pencil sketch layer only.
    //col = vec3(ns);


    /*
    // Just some line overlays.
    vec2 pt = p;
    float offs = -.5;
    if(i<.5) offs += 2.;//pt.xy = -pt.xy;
    pt = rot2(6.2831/3.)*pt;
    float pat2 = clamp(cos(pt.x*6.2831*14. - offs)*2. + 1.5, 0., 1.);
    col *= pat2*.4 + .8;
    */


    // Cheap paper grain.
    //oP = floor(oP/gSc*1024.);
    //vec3 rn3 = vec3(hash21(oP), hash21(oP + 2.37), hash21(oP + 4.83));
    //col *= .9 + .1*rn3.xyz  + .1*rn3.xxx;


    // Return the simplex weave value.
    return col;


}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    // Position with some scrolling, and screen rotation to level the pattern.
    vec2 p = rot2(3.14159/12.)*uv + vec2(.8660254, .5)*mat_time/16.;

    // The simplex grid contour map... or whatever you wish to call it. :)
    vec3 col = simplexContour(p);

    // Subtle vignette.
    // uv = gl_FragCoord.xy/RENDERSIZE.xy;
    // col *= pow(16.*uv.x*uv.y*(1. - uv.x)*(1. - uv.y) , .0625) + .1;
    // Colored variation.
    //col = mix(col.zyx/2., col, pow(16.*uv.x*uv.y*(1. - uv.x)*(1. - uv.y) , .125));

    // Rough gamma correction.
    out_color = vec4(sqrt(max(col, 0.)), 1);


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity) - mat_luma_threshold * 4. - 1.;
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
        out_color.a = mat_luma(out_color * mat_luma_sensitivity);
    }

    return out_color;
}
