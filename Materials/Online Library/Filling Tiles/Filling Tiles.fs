/*{
    "CREDIT": "Hanley, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/Xd3SW8",

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
            "LABEL": "Animation/Fill Curve",
            "NAME": "mat_fill_curve",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Animation/Empty Curve",
            "NAME": "mat_exit_curve",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Animation/Border Curve",
            "NAME": "mat_border_curve",
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
            "LABEL": "Color/Gradient",
            "NAME": "mat_use_gradient",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "Label": "Color/Gradient Lvl",
            "NAME": "mat_gradient",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Color/Tint",
            "NAME": "mat_front_color",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "LABEL": "Color/Background",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
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
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8.;

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


#define TWO_PI 2.*PI

float polygonDistanceField(in vec2 pixelPos, in int N) {
    // N = number of corners
    float a = atan(pixelPos.y, pixelPos.x) + PI/2.; // angle
    float r = TWO_PI/float(N); // ~?
    // shapping function that modulates the distances
    float distanceField = cos(floor(0.5 + a/r) * r - a) * length(pixelPos);
    return distanceField;
}

float minAngularDifference(in float angleA, in float angleB) {
    // Ensure input angles are -Ï€ to Ï€
    angleA = mod(angleA, TWO_PI);
    if (angleA>PI) angleA -= TWO_PI;
    if (angleA<PI) angleA += TWO_PI;
    angleB = mod(angleB, TWO_PI);
    if (angleB>PI) angleB -= TWO_PI;
    if (angleB<PI) angleB += TWO_PI;

    // Calculate angular difference
    float angularDiff = abs(angleA - angleB);
    angularDiff = min(angularDiff, TWO_PI - angularDiff);
    return angularDiff;
}

float map(in float value, in float istart, in float istop, in float ostart, in float ostop) {
    return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
}
float mapAndCap(in float value, in float istart, in float istop, in float ostart, in float ostop) {
    float v = map(value, istart, istop, ostart, ostop);
    v = max( min(ostart,ostop), v);
    v = min( max(ostart,ostop), v);
    return v;
}


// rotate matrix
mat2 rotate2d(float angle) {
    return mat2(cos(angle), -sin(angle),
                sin(angle),  cos(angle) );
}

// scale matrix
mat2 scale(vec2 scale) {
    return mat2(scale.x, 0,
                0, scale.y);
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

    float u_time = mat_time;

    // vec3 color = vec3(0.2);
    vec3 color = vec3(0.0);
    float t = u_time;
    vec2 st = uv + vec2(0.5);

    float divisions = 4.;
    vec2 mst = st;
    mst *= divisions;
    // give each cell an index number according to position (left-right, down-up)
    float index = 0.;
    float cellx = floor(mst.x);
    float celly = floor(mst.y);
    index += floor(mst.x);
    index += floor(mst.y)*divisions;
    // tile mst
    mst = mod(mst, 1.);

    ////
    // draw square tile

    // t = 1.6;
    float tt = t-(sin(cellx*.3)+cos(celly*.3))*.5; //t * .3;
    float squareProgress = mod(tt*.3, 1.); //0.22; // mouse_n.x; //0.2; //mod(t*.3, 1.);
    float squareEntryProgress = mapAndCap(squareProgress, 0., 0.6, 0., 1.); //mod(t*.7, 1.); //mouse_n.x;
    float squareExitProgress = mapAndCap(squareProgress, 0.9, .999, 0., 1.);
        squareExitProgress = pow(squareExitProgress, 3. * mat_exit_curve);
    float borderProgress = mapAndCap(squareEntryProgress,0.,0.55,0.,1.);
        borderProgress = pow(borderProgress, 1.5 * mat_border_curve);
    float fillProgress = mapAndCap(squareEntryProgress,0.4, 0.9, 0., 1.);
        fillProgress = pow(fillProgress, 4. * mat_fill_curve);
    // MATRIX MANIP
    mst = mst*2.-1.; // centre origin point
    // rotate
    // mst = rotate2d(floor(mod(index,2.))*PI*.5 + PI*.25)*mst;
    mst = rotate2d(cellx*PI*.5 + celly*PI*.5 + PI*.25)*mst;
    float d = polygonDistanceField(mst, 4);
    float r = map(squareExitProgress, 0., 1., 0.7, 0.); // 0.5;
    float innerCut = map(fillProgress, 0., 1., 0.9, 0.0001); //0.9; //mouse_n.x;
    float buf = 1.01;
    float shape = smoothstep(r*buf, r, d) - smoothstep(r*innerCut, r*innerCut/buf, d);
    // add smoother shape glow
    buf = 1.5;
    float shape2 = smoothstep(r*buf, r, d) - smoothstep(r*innerCut, r*innerCut/buf, d);
    // shape += shape2*.5;
    // angular mask on square tile
    float sta = atan(mst.y, mst.x); // st-angle - technically its msta here
    float targetAngle = map(borderProgress, 0., 1., 0., PI)+PI*.251;
    float adiff = minAngularDifference(sta, targetAngle);
    float arange = map(borderProgress, 0., 1., 0., PI);
    float amask = 1. - smoothstep(arange, arange, adiff);
    shape *= amask;
    // color
    // color = vec3(shape) * vec3(0.8, 0.6, 0.8)*2.;

    // color = vec3(shape) * (vec3(1.-st.x, st.y, st.y)+vec3(.2));
    color = vec3(shape);
    if (mat_use_gradient) {
        color *= (vec3(1.-st.x, st.y, st.y)+vec3(.2 * 1./mat_gradient));
    }

    // color += vec3(mst.y, 0., mst.x);

    out_color = vec4(color,1.0) * mat_front_color;

    if (mat_luma(color) < 0.02) {
        out_color = mat_back_color;
    }



    // out_color = mix(mat_back_color, mat_front_color, mat_luma(col));

    return out_color;
}
