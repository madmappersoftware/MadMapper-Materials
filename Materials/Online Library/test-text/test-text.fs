/*{
    "CREDIT": "Shane, adapted by Jason Beyers",

    "DESCRIPTION": "Raymarching a textured XY plane, with options for custom texture inputs. From https://www.shadertoy.com/view/ldt3RN",

    "VSN": "1.0",

    "IMPORTED": {
        "mat_default_tex": {
            "NAME": "mat_default_tex",
            "PATH": "ad56fba948dfba9ae698198c109e71f118a54d209c0ea50d77ea546abad89c57.png"
        }
    },

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
            "LABEL": "UV/Shift",
            "NAME": "mat_shift_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },





        {
            "LABEL": "Scroll/Shift",
            "NAME": "mat_scroll_amount",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Scroll/Animate",
            "NAME": "mat_scroll_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "Label": "Scroll/Direction",
            "NAME": "mat_scroll_angle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Scroll/Speed",
            "NAME": "mat_scroll_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Scroll/BPM Sync",
            "NAME": "mat_scroll_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Scroll/Reverse",
            "NAME": "mat_scroll_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Scroll/Offset",
            "NAME": "mat_scroll_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Scroll/Strob",
            "NAME": "mat_scroll_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "Label": "Rotate/Amount",
            "NAME": "mat_rotate",
            "TYPE": "float",
            "MIN": -360.0,
            "MAX": 360.,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Rotate/Animate",
            "NAME": "mat_rotate_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Bounce",
            "NAME": "mat_rotate_bounce",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rotate_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rotate_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rotate_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rotate_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Strob",
            "NAME": "mat_rotate_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },



        {
            "Label": "Distort/Amplitude",
            "NAME": "mat_distort",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Distort/Animate",
            "NAME": "mat_distort_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "LABEL": "Distort/Speed",
            "NAME": "mat_distort_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.5
        },
        {
            "LABEL": "Distort/BPM Sync",
            "NAME": "mat_distort_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Distort/Reverse",
            "NAME": "mat_distort_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Distort/Offset",
            "NAME": "mat_distort_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Distort/Strob",
            "NAME": "mat_distort_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },









        {
            "LABEL": "Lighting/Power",
            "NAME": "mat_light_power",
            "TYPE": "float",
            "MIN": 0.1,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/Diffuse",
            "NAME": "mat_light_diffuse",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/Specular",
            "NAME": "mat_light_spec",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 8.0,
            "DEFAULT": 1.5
        },
        {
            "LABEL": "Lighting/Position",
            "NAME": "mat_light_pos",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Lighting/Animate",
            "NAME": "mat_light_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },

        {
            "LABEL": "Lighting/Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Lighting/BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Lighting/Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Lighting/Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Lighting/Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },


        {
            "LABEL": "Texture/Use Custom Texture",
            "NAME": "mat_use_custom_tex",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Texture/Texture",
            "NAME": "mat_tex",
            "TYPE": "image",
        },
        {
            "LABEL": "Texture/Aspect",
            "NAME": "mat_tex_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture/Scale",
            "NAME": "mat_tex_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Texture/Shift",
            "NAME": "mat_tex_offset",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },

        {
            "LABEL": "Shape/Use Custom Shape",
            "NAME": "mat_use_custom_shape",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Shape/Shape",
            "NAME": "mat_shape",
            "TYPE": "image",
        },
        {
            "LABEL": "Shape/Aspect",
            "NAME": "mat_shape_aspect",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Scale",
            "NAME": "mat_shape_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Shape/Tile",
            "NAME": "mat_shape_tile",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },


        {
            "LABEL": "Color/Colored",
            "NAME": "mat_colored",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
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
            "NAME": "mat_scroll_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_scroll_speed",
                "speed_curve":2,
                "reverse": "mat_scroll_reverse",
                "strob" : "mat_scroll_strob",
                "bpm_sync": "mat_scroll_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },

        {
            "NAME": "mat_rotate_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rotate_speed",
                "speed_curve":2,
                "reverse": "mat_rotate_reverse",
                "strob" : "mat_rotate_strob",
                "bpm_sync": "mat_rotate_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_distort_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_distort_speed",
                "speed_curve":2,
                "reverse": "mat_distort_reverse",
                "strob" : "mat_distort_strob",
                "bpm_sync": "mat_distort_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset * 8.;

float mat_scroll_time = mat_scroll_time_source - mat_scroll_offset * 8.;

float mat_rotate_time = (mat_rotate_time_source - mat_rotate_offset * 8.) * 2.;

float mat_distort_time = (mat_distort_time_source - mat_distort_offset * 8.) * 2.;

vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}




// Bioorganic Wall
// ---------------

// Raymarching a textured XY plane. Basically, just an excuse to try out the new pebbled texture.



// Tri-Planar blending function. Based on an old Nvidia writeup:
// GPU Gems 3 - Ryan Geiss: http://http.developer.nvidia.com/GPUGems3/gpugems3_ch01.html
vec3 tex3D(in vec3 p, in vec3 n ){

    n = max(n*n, 0.001);
    n /= (n.x + n.y + n.z );

    if (mat_use_custom_tex) {

        vec2 coord1 = p.yz;
        vec2 coord2 = p.zx;
        vec2 coord3 = p.xy;

        // coord1 -= vec2(0.5);
        coord1.x *= mat_tex_aspect;
        coord1 *= mat_tex_scale;
        // coord1 += vec2(0.5);

        // coord2 -= vec2(0.5);
        coord2.x *= mat_tex_aspect;
        coord2 *= mat_tex_scale;
        // coord2 += vec2(0.5);

        // coord3 -= vec2(0.5);
        coord3.x *= mat_tex_aspect;
        coord3 *= mat_tex_scale;
        // coord3 += vec2(0.5);

        vec2 tex_offset = mat_tex_offset;
        tex_offset += vec2(0.5);
        tex_offset.x = 1. - tex_offset.x;
        tex_offset -= vec2(0.5);


        coord1 += tex_offset;
        coord2 += tex_offset;
        coord3 += tex_offset;

        return (IMG_NORM_PIXEL(mat_tex, coord1)*n.x + IMG_NORM_PIXEL(mat_tex, coord2)*n.y + IMG_NORM_PIXEL(mat_tex, coord3)*n.z).xyz;


    } else {
        return (IMG_NORM_PIXEL(mat_default_tex, p.yz)*n.x + IMG_NORM_PIXEL(mat_default_tex, p.zx)*n.y + IMG_NORM_PIXEL(mat_default_tex, p.xy)*n.z).xyz;

    }



}

// Raymarching a textured XY-plane, with a bit of distortion thrown in.
float map(vec3 p){

    // A bit of cheap, lame distortion for the heaving in and out effect.

    float distort_time = 0.;

    if (mat_distort_animate) {
        distort_time = mat_distort_time;
    }

    p.xy += sin(p.xy*7. + cos(p.yx*13. + distort_time))*.01 * mat_distort;

    // Back plane, placed at vec3(0., 0., 1.), with plane normal vec3(0., 0., -1).
    // Adding some height to the plane from the texture. Not much else to it.

    if (mat_use_custom_shape) {

        vec2 coord;

        if (mat_shape_tile) {
            coord = mod(p.xy,1.0);
            coord -= vec2(0.5);
            coord.x *= mat_shape_aspect;
            coord *= mat_shape_scale;
            coord += vec2(0.5);

        } else {
            coord = p.xy;
            coord.x *= mat_shape_aspect;
            coord *= mat_shape_scale;
        }
        return 1. - p.z - IMG_NORM_PIXEL(mat_shape,coord).x*.1;
    } else {
		vec2 coord = fract(p.xy/2)*2;
		if (coord.x>1) coord.x=2; else coord.x=0;
		if (coord.y>1) coord.y=2; else coord.y=0;
		//coord = fract(p.xy);
		return 1. - IMG_NORM_PIXEL(mat_default_tex,coord).x*.1;
    }


    // Flattened tops.
    //float t = IMG_NORM_PIXEL(mat_default_tex,mod(p.xy,1.0)).x;
    //return 1. - p.z - smoothstep(0., .7, t)*.06 - t*t*.03;

}


// Tetrahedral normal, courtesy of IQ.
vec3 getNormal( in vec3 pos )
{
    vec2 e = vec2(0.002, -0.002);
    return normalize(
        e.xyy * map(pos + e.xyy) +
        e.yyx * map(pos + e.yyx) +
        e.yxy * map(pos + e.yxy) +
        e.xxx * map(pos + e.xxx));
}

// Ambient occlusion, for that self shadowed look.
// Based on the original by IQ.
float calculateAO(vec3 p, vec3 n){

   const float AO_SAMPLES = 5.0;
   float r = 1.0, w = 1.0, d0;

   for (float i=1.0; i<=AO_SAMPLES; i++){

      d0 = i/AO_SAMPLES;
      r += w * (map(p + n * d0) - d0);
      w *= 0.5;
   }
   return clamp(r, 0.0, 1.0);
}

// Cool curve function, by Shadertoy user, Nimitz.
//
// It gives you a scalar curvature value for an object's signed distance function, which
// is pretty handy for all kinds of things. Here, it's used to darken the crevices.
//
// From an intuitive sense, the function returns a weighted difference between a surface
// value and some surrounding values - arranged in a simplex tetrahedral fashion for minimal
// calculations, I'm assuming. Almost common sense... almost. :)
//
// Original usage (I think?) - Cheap curvature: https://www.shadertoy.com/view/Xts3WM
// Other usage: Xyptonjtroz: https://www.shadertoy.com/view/4ts3z2
float curve(in vec3 p){

    const float eps = 0.02, amp = 7., ampInit = 0.5;

    vec2 e = vec2(-1., 1.)*eps; //0.05->3.5 - 0.04->5.5 - 0.03->10.->0.1->1.

    float t1 = map(p + e.yxx), t2 = map(p + e.xxy);
    float t3 = map(p + e.xyx), t4 = map(p + e.yyy);

    return clamp((t1 + t2 + t3 + t4 - 4.*map(p))*amp + ampInit, 0., 1.);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv += vec2(0.5);
    uv = matRot2D(uv, 2*PI*mat_rotate / 360);
    uv -= vec2(0.5);

    vec2 uv_shift = mat_shift_amount;
    uv_shift += vec2(0.5);
    uv_shift.x = 1.-uv_shift.x;
    uv_shift -= vec2(0.5);
    uv += uv_shift;

    uv *= mat_scale * 128.;

    // Unit directional ray.
    vec3 rd = normalize(vec3(uv, 100.));

    // Rotating the XY-plane back and forth, for a bit of variance.
    // Compact 2D rotation matrix, courtesy of Shadertoy user, "Fabrice Neyret."
    vec2 a;

    if (mat_rotate_animate) {
        if (mat_rotate_bounce) {
            a = sin(vec2(1.5707963, 0) + sin(mat_rotate_time/4.)*.3);
        } else {
            a = sin(vec2(1.5707963, 0) + (mat_rotate_time/4.)*.3);
        }
    } else {
        a = sin(vec2(1.5707963, 0));
    }

    rd.xy = mat2(a, -a.y, a.x)*rd.xy;

    // Ray origin. Moving in the X-direction to the right.

    float scroll_time = mat_scroll_time;

    vec2 scroll_offset = mat_scroll_amount;
    scroll_offset += vec2(0.5);
    scroll_offset.x = 1.-scroll_offset.x;
    scroll_offset -= vec2(0.5);


    if (!mat_scroll_animate) {
        scroll_time = 0.;
    }

    float scroll_time_x = scroll_time * cos(PI * mat_scroll_angle);
    float scroll_time_y = scroll_time * sin(PI * mat_scroll_angle);

    vec3 ro = vec3(scroll_time_x*.25 + scroll_offset.x, scroll_time_y*.25 + scroll_offset.y, 0.);

    // Light position, hovering around camera.

    vec2 light_pos = mat_light_pos;

    light_pos += vec2(0.5);
    light_pos.y = 1. - light_pos.y;
    light_pos -= vec2(0.5);

    vec3 lp;
    if (mat_light_animate) {
        lp = ro + vec3(cos(mat_time/2.)*.5 + light_pos.x, sin(mat_time/2.)*.5 + light_pos.y, 0.);
    } else {
        lp = ro + vec3(light_pos.x, light_pos.y, 0.);
    }



    // Standard raymarching segment. Because of the straight forward setup, very few
    // iterations are needed.
    float d, t=0.;
    for(int j=0;j<16;j++){

        d = map(ro + rd*t); // distance to the function.

        // The plane "is" the far plane, so no far plane break is needed.
        if(d<0.001) break;

        t += d*.7; // Total distance from the camera to the surface.

    }


    // Surface postion, surface normal and light direction.
    vec3 sp = ro + rd*t;
    vec3 sn = getNormal(sp);
    vec3 ld = lp - sp;


    // Retrieving the texel at the surface postion. A tri-planar mapping method is used to
    // give a little extra dimension. The time component is responsible for the texture movement.
    float c = 1. - tex3D(sp*8. - vec3(sp.x, sp.y, scroll_time/4.+sp.x+sp.y), sn).x;


    // Taking the original grey texel shade and colorizing it. Most of the folowing lines are
    // a mixture of theory and trial and error. There are so many ways to go about it.
    //
    vec3 orange = vec3(min(c*1.5, 1.), pow(c, 2.), pow(c, 8.)); // Cheap, orangey palette.

    vec3 oC = orange; // Initializing the object (bumpy wall) color.

    if (mat_colored) {

        // Old trick to shift the colors around a bit. Most of the figures are trial and error.
        oC = mix(oC, oC.zxy, cos(rd.zxy*6.283 + sin(sp.yzx*6.283))*.25+.75);
        oC = mix(oC.yxz, oC, (sn)*.5+.5); // Using the normal to colorize.

        oC = mix(orange, oC, (sn)*.25+.75);
        oC *= oC*1.5;
    } else {
        // Plain, old black and white. In some ways, I prefer it. Be sure to comment out the above, though.

        oC = vec3(pow(c, 1.25));

    }




    // Lighting.
    //
    float lDist = max(length(ld), 0.001)  / mat_light_power; // Light distance.
    float atten = 1./(1. + lDist*.125); // Light attenuation.

    ld /= lDist; // Normalizing the light direction vector.

    float diff = max(dot(ld, sn), 0.) * mat_light_diffuse; // Diffuse.
    float spec = pow(max( dot( reflect(-ld, sn), -rd ), 0.0 ), 32. / pow(mat_light_spec, 2.)); // Specular.
    float fre = clamp(dot(sn, rd) + 1., .0, 1.); // Fake fresnel, for the glow.

    // Shading.
    //
    // Note, there are no actual shadows. The camera is front on, so the following two
    // functions are enough to give a shadowy appearance.
    float crv = curve(sp); // Curve value, to darken the crevices.
    float ao = calculateAO(sp, sn); // Ambient occlusion, for self shadowing.
    // Not all that necessary, but adds a bit of green to the crevice color to give a fake,
    // slimey appearance.
    vec3 crvC = vec3(crv, crv*1.3, crv*.7)*.25 + crv*.75;
    crvC *= crvC;

    // Combining the terms above to light up and colorize the texel.
    vec3 col = (oC*(diff + .5) + vec3(.5, .75, 1.)*spec*2.) + vec3(.3, .7, 1.)*pow(fre, 3.)*5.;
    // Applying the shades.
    col *= (atten*crvC*ao);

    // I might be stating the obvious here, but if you ever want to see what effect each individual
    // component has on the overall scene, just put the variable in at the end, by itself.
    //col = vec3(ao); // col = vec3(crv); // etc.
    // Presenting to the screen.
    out_color = vec4(sqrt(max(col, 0.)), 1.);




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
