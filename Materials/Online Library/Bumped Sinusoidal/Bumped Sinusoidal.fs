/*{
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "Bumped sinusoidal warp with option for texture input. From https:\/\/www.shadertoy.com\/view\/4l2XWK",

    "VSN": "1.1",

    "IMPORTED" : [
    {
      "NAME" : "iChannel0",
      "PATH" : "texture.jpg"
    }
  ],

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
            "LABEL": "Warp/Warp",
            "NAME": "mat_warp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Warp/Warp Cycle",
            "NAME": "mat_warp_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Warp/Warp Scale",
            "NAME": "mat_warp_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Warp/Iterations",
            "NAME": "mat_warp_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 20,
            "DEFAULT": 3
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
            "NAME": "mat_custom_texture",
            "TYPE": "image",
            "DEFAULT": false,
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
            "LABEL": "Animation/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 10.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

/*
    Bumped Sinusoidal Warp
    ----------------------

    Sinusoidal planar deformation, or the 2D sine warp effect to people
    like me. The effect has been around for years, and there are
    countless examples on the net. IQ's "Sculpture III" is basically a
    much more sophisticated, spherical variation.

    This particular version was modified from Fabrice's "Plop 2," which in
    turn was a simplified version of Fantomas's "Plop." I simply reduced
    the frequency and iteration count in order to make it less busy.

    I also threw in a texture, added point-lit bump mapping, speckles...
    and that's pretty much it. As for why a metallic surface would be
    defying the laws of physics and moving like this is anyone's guess. :)

    By the way, I have a 3D version, similar to this, that I'll put up at
    a later date.


    Related examples:

    Fantomas - Plop
    https://www.shadertoy.com/view/ltSSDV

    Fabrice - Plop 2
    https://www.shadertoy.com/view/MlSSDV

    IQ - Sculpture III (loosely related)
    https://www.shadertoy.com/view/XtjSDK

    Shane - Lit Sine Warp (far less code)
    https://www.shadertoy.com/view/Ml2XDV

*/

// Warp function. Variations have been around for years. This is
// almost the same as Fabrice's version:
// Fabrice - Plop 2
// https://www.shadertoy.com/view/MlSSDV
vec2 W(vec2 p){

    p = (p+3.)*4.;

    float t = mat_time/2.;

    // Layered, sinusoidal feedback, with time component.
    for (int i=0; i<mat_warp_iterations; i++){
        p += cos( p.yx*3. + vec2(t,1.57))/3.;
        p += sin( p.yx + t + vec2(1.57,0))/2.;
        p *= 1.3;
    }

    // A bit of jitter to counter the high frequency sections.
    p +=  fract(sin(p+vec2(13, 7))*5e5)*.03-.015;
    p += mat_warp_cycle;

    p *= mat_warp_scale;

    return mat_warp * (mod(p,2.)-1.); // Range: [vec2(-1), vec2(1)]

}

// Bump mapping function. Put whatever you want here. In this case,
// we're returning the length of the sinusoidal warp function.
float bumpFunc(vec2 p){

    return length(W(p))*.7071; // Range: [0, 1]

}

/*
// Standard ray-plane intersection.
vec3 rayPlane(vec3 p, vec3 o, vec3 n, vec3 rd) {

    float dn = dot(rd, n);

    float s = 1e8;

    if (abs(dn) > 0.0001) {
        s = dot(p-o, n) / dn;
        s += float(s < 0.0) * 1e8;
    }

    return o + s*rd;
}
*/

vec3 smoothFract(vec3 x){ x = fract(x); return min(x, x*(1.-x)*12.); }

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

    // PLANE ROTATION
    //
    // Rotating the canvas back and forth. I don't feel it adds value, in this case,
    // but feel free to uncomment it.
    //float th = sin(mat_time*0.1)*sin(mat_time*0.12)*2.;
    //float cs = cos(th), si = sin(th);
    //uv *= mat2(cs, -si, si, cs);

    // VECTOR SETUP - surface postion, ray origin, unit direction vector, and light postion.
    //
    // Setup: I find 2D bump mapping more intuitive to pretend I'm raytracing, then lighting a bump mapped plane
    // situated at the origin. Others may disagree. :)
    vec3 sp = vec3(uv, 0); // Surface posion. Hit point, if you prefer. Essentially, a screen at the origin.
    vec3 rd = normalize(vec3(uv, 1.)); // Unit direction vector. From the origin to the screen plane.
    vec3 lp = vec3(cos(mat_time)*0.5, sin(mat_time)*0.2, -1.); // Light position - Back from the screen.
    vec3 sn = vec3(0., 0., -1); // Plane normal. Z pointing toward the viewer.


/*
    // I deliberately left this block in to show that the above is a simplified version
    // of a raytraced plane. The "rayPlane" equation is commented out above.
    vec3 rd = normalize(vec3(uv, 1.));
    vec3 ro = vec3(0., 0., -1);
    // Plane normal.
    vec3 sn = normalize(vec3(cos(mat_time)*0.25, sin(mat_time)*0.25, -1));
    //vec3 sn = normalize(vec3(0., 0., -1));

    vec3 sp = rayPlane(vec3(0., 0., 0.), ro, sn, rd);
    vec3 lp = vec3(cos(mat_time)*0.5, sin(mat_time)*0.25, -1.);
*/


    // BUMP MAPPING - PERTURBING THE NORMAL
    //
    // Setting up the bump mapping variables. Normally, you'd amalgamate a lot of the following,
    // and roll it into a single function, but I wanted to show the workings.
    //
    // f - Function value
    // fx - Change in "f" in in the X-direction.
    // fy - Change in "f" in in the Y-direction.
    vec2 eps = vec2(4./RENDERSIZE.y, 0.);

    float f = bumpFunc(sp.xy); // Sample value multiplied by the amplitude.
    float fx = bumpFunc(sp.xy-eps.xy); // Same for the nearby sample in the X-direction.
    float fy = bumpFunc(sp.xy-eps.yx); // Same for the nearby sample in the Y-direction.

    // Controls how much the bump is accentuated.
    const float bumpFactor = 0.05;

    // Using the above to determine the dx and dy function gradients.
    fx = (fx-f)/eps.x; // Change in X
    fy = (fy-f)/eps.x; // Change in Y.
    // Using the gradient vector, "vec3(fx, fy, 0)," to perturb the XY plane normal ",vec3(0, 0, -1)."
    // By the way, there's a redundant step I'm skipping in this particular case, on account of the
    // normal only having a Z-component. Normally, though, you'd need the commented stuff below.
    //vec3 grad = vec3(fx, fy, 0);
    //grad -= sn*dot(sn, grad);
    //sn = normalize( sn + grad*bumpFactor );
    sn = normalize( sn + vec3(fx, fy, 0)*bumpFactor );


    // LIGHTING
    //
    // Determine the light direction vector, calculate its distance, then normalize it.
    vec3 ld = lp - sp;
    float lDist = max(length(ld), 0.001);
    ld /= lDist;
    // Light attenuation.
    float atten = 1./(1.0 + lDist*lDist*0.15);
    //float atten = min(1./(lDist*lDist*1.), 1.);

    // Using the bump function, "f," to darken the crevices. Completely optional, but I
    // find it gives extra depth.
    atten *= f*.9 + .1; // Or... f*f*.7 + .3; //  pow(f, .75); // etc.

    // Diffuse value.
    float diff = max(dot(sn, ld), 0.);
    // Enhancing the diffuse value a bit. Made up.
    diff = pow(diff, 4.)*0.66 + pow(diff, 8.)*0.34;
    // Specular highlighting.
    float spec = pow(max(dot( reflect(-ld, sn), -rd), 0.), 12.);

    // TEXTURE COLOR
    //
    // Combining the surface postion with a fraction of the warped surface position to index
    // into the texture. The result is a slightly warped texture, as a opposed to a completely
    // warped one. By the way, the warp function is called above in the "bumpFunc" function,
    // so it's kind of wasteful doing it again here, but the function is kind of cheap, and
    // it's more readable this way.

    vec3 texCol;
    if (mat_use_custom_texture) {
        texCol = IMG_NORM_PIXEL(mat_custom_texture,mod(sp.xy + W(sp.xy)/8.,1.0)).xyz;

    } else {
        texCol = IMG_NORM_PIXEL(iChannel0,mod(sp.xy + W(sp.xy)/8.,1.0)).xyz;
    }


    texCol *= texCol; // Rough sRGB to linear conversion... That's a whole other conversation. :)
    // A bit of color processing.
    texCol = smoothstep(0.05, .75, pow(texCol, vec3(.75, .8, .85)));

    // Textureless. Simple and elegant... so it clearly didn't come from me. Thanks Fabrice. :)
    //vec3 texCol = smoothFract( W(sp.xy).xyy )*.1 + .2;



    // FINAL COLOR
    // Using the values above to produce the final color.
    vec3 col = (texCol * (diff*vec3(1, .97, .92)*2. + 0.5) + vec3(1., .6, .2)*spec*2.)*atten;
    // Perform some statistically unlikely (but close enough) 2.0 gamma correction. :)
    out_color = vec4(sqrt(clamp(col, 0., 1.)), 1.);


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
