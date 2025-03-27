/*{
    "CREDIT": "CaliCoastReplay & mu6k, adapted by Jason Beyers",

    "DESCRIPTION": "Gum balls with camera controls. From https://www.shadertoy.com/view/Mss3WN",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Gum/Shift",
            "NAME": "mat_camera",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]

        },


        {
            "LABEL": "Gum/Pitch",
            "NAME": "mat_obj_offset1",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Gum/Roll",
            "NAME": "mat_obj_offset2",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Gum/Depth",
            "NAME": "mat_depth",
            "TYPE": "float",
            "MIN": -2.0,
            "MAX": 2.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Gum/Scale",
            "NAME": "mat_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Gum/Quality",
            "NAME": "mat_occ_quality",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 8,
            "DEFAULT": 4
        },
        {
            "LABEL": "Gum/Render",
            "NAME": "mat_render",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 50,
            "DEFAULT": 33
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
            "LABEL": "Background/Custom Background",
            "NAME": "mat_custom_background",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Background/Input",
            "NAME": "mat_input_image",
            "TYPE": "image"
        },
        {
            "LABEL": "Background/Level",
            "NAME": "mat_background",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },



        {
            "NAME": "mat_obj",
            "LABEL": "Color/Object",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
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
    ]

}*/

#include "MadCommon.glsl"

float mat_time = mat_time_source - mat_offset;

#define A5


/*
Simple remix by CaliCoastReplay

--Just coloring/positional experimentations of...

MetaHexaBalls  by mu6k, Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

Original at:
https://www.shadertoy.com/view/Mss3WN

 muuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuusk!*/

#define occlusion_enabled
int occlusion_quality = mat_occ_quality;
//#define occlusion_preview

#define noise_use_smoothstep

#define light_color vec3(0.1,0.4,0.6)
#define light_direction normalize(vec3(.2,1.0,-0.2))
#define light_speed_modifier 1.0

vec3 object_color = vec3(0.9,0.1,0.1) * mat_obj;
#define object_count 16
#define object_speed_modifier 1.0

float render_steps = mat_render;

float hash(float x)
{
    return fract(sin(x*.0127863)*17143.321);
}

float hash(vec2 x)
{
    return fract(cos(dot(x.xy,vec2(2.31,53.21))*124.123)*412.0);
}

vec3 cc(vec3 color, float factor,float factor2) //a wierd color modifier
{
    float w = color.x+color.y+color.z;
    return mix(color,vec3(w)*factor,w*factor2);
}

float hashmix(float x0, float x1, float interp)
{
    x0 = hash(x0);
    x1 = hash(x1);
    #ifdef noise_use_smoothstep
    interp = smoothstep(0.0,1.0,interp);
    #endif
    return mix(x0,x1,interp);
}

float noise(float p) // 1D noise
{
    float pm = mod(p,1.0);
    float pd = p-pm;
    return hashmix(pd,pd+1.0,pm);
}

vec3 rotate_y(vec3 v, float angle)
{
    float ca = cos(angle); float sa = sin(angle);
    return v*mat3(
        +ca, +.0, -sa,
        +.0,+1.0, +.0,
        +sa, +.0, +ca);
}

vec3 rotate_x(vec3 v, float angle)
{
    float ca = cos(angle); float sa = sin(angle);
    return v*mat3(
        +1.0, +.0, +.0,
        +.0, +ca, -sa,
        +.0, +sa, +ca);
}

float max3(float a, float b, float c)//returns the maximum of 3 values
{
    return max(a,max(b,c));
}

vec3 bpos[object_count];//position for each metaball

float dist(vec3 p)//distance function
{
    float d=256.0;
    float nd;
    for (int i=0 ;i<object_count; i++)
    {
        vec3 np = p+bpos[i];
        float shape0 = max3(abs(np.x),abs(np.y),abs(np.z))-1.0;
        float shape1 = length(np)-1.0;
        nd = shape0+(shape1-shape0)*2.0;
        d = mix(d,nd,smoothstep(-1.0,+1.0,d-nd));
    }
    return d;
}

vec3 normal(vec3 p,float e) //returns the normal, uses the distance function
{
    float d=dist(p);
    return normalize(vec3(dist(p+vec3(e,0,0))-d,dist(p+vec3(0,e,0))-d,dist(p+vec3(0,0,e))-d));
}

vec3 light = light_direction; //global variable that holds light direction

vec3 background(vec3 d)//render background
{
    if (mat_custom_background) {
        return IMG_NORM_PIXEL(mat_input_image, d.xy).rgb * mat_background;

    } else {
        float t=mat_time*0.5*light_speed_modifier;
        float qq = dot(d,light)*.5+.5;
        float bgl = qq;
        float q = (bgl+noise(bgl*6.0+t)*.85+noise(bgl*12.0+t)*.85);
        q+= pow(qq,32.0)*2.0;
        vec3 sky = vec3(0.1,0.4,0.6)*q;
        vec3 hsv_sky = rgb2hsv(sky);
        hsv_sky.x += t/10.0;
        hsv_sky.z += 0.1;
        sky = hsv2rgb(hsv_sky);
        return sky * mat_background;
    }
}

float occlusion(vec3 p, vec3 d)//returns how much a point is visible from a given direction
{
    float occ = 1.0;
    p=p+d;
    for (int i=0; i<occlusion_quality; i++)
    {
        float dd = dist(p);
        p+=d*dd;
        occ = min(occ,dd);
    }
    return max(.0,occ);
}

vec3 object_material(vec3 p, vec3 d)
{
    vec3 color = normalize(object_color*light_color);
    vec3 n = normal(p,0.1);
    vec3 r = reflect(d,n);

    float reflectance = dot(d,r)*.5+.5;reflectance=pow(reflectance,2.0);
    float diffuse = dot(light,n)*.5+.5; diffuse = max(.0,diffuse);

    #ifdef occlusion_enabled
        float oa = occlusion(p,n)*.4+.6;
        float od = occlusion(p,light)*.95+.05;
        float os = occlusion(p,r)*.95+.05;
    #else
        float oa=1.0;
        float ob=1.0;
        float oc=1.0;
    #endif

    #ifndef occlusion_preview
        color =
        color*oa*.2 + //ambient
        color*diffuse*od*.7 + //diffuse
        background(r)*os*reflectance*.7; //reflection
    #else
        color=vec3((oa+od+os)*.3);
    #endif

    return color;
}

float offset1 = 4.7 + mat_obj_offset1;
float offset2 = 4.6 + mat_obj_offset2;


vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_scale;

    vec3 mouse = vec3(0.);

    float t = mat_time*.5*object_speed_modifier + 2.0;

    for (int i=0 ;i<object_count; i++) //position for each metaball
    {
        float wave_movement = sin(t + float(i));
        bpos[i] = 3.0*wave_movement*wave_movement*vec3(
            sin(t*0.967+float(i)*42.0),
            sin(t*.423+float(i)*152.0),
            sin(t*.76321+float(i)));
    }

    //setup the camera
    vec3 p = vec3(.0,0.0,-4.0);

    vec2 camera = mat_camera;
    camera += vec2(0.5);
    camera.x = 1.-camera.x;
    camera -= vec2(0.5);

    p.x += camera.x * 2.5;
    p.y += camera.y * 2.5;
    p.z += mat_depth;


    p = rotate_x(p,mouse.y*9.0+offset1);
    p = rotate_y(p,mouse.x*9.0+offset2);
    vec3 d = vec3(uv,1.0);
    d.z -= length(d)*.5; //lens distort
    d = normalize(d);
    d = rotate_x(d,mouse.y*9.0+offset1);
    d = rotate_y(d,mouse.x*9.0+offset2);

    //and action!
    float dd;
    vec3 color;
    for (int i=0; i<render_steps; i++) //raymarch
    {
        dd = dist(p);
        p+=d*dd*.7;
        if (dd<.04 || dd>4.0) break;
    }

    if (dd<0.5) //close enough
    {
        color = object_material(p,d);
        color.r += .3 + sin(mat_time)/10.0;
        color.b += .2 + uv.y + cos(mat_time)/10.0;
        color.g += .1 + uv.x - uv.y/2.0 + sin(mat_time)/10.0;
        color *= (.7 + background(d)/2.0);
    }
    else
        color = background(d);

    //post procesing
    color *=.85;
    color = mix(color,color*color,0.3);
    color -= hash(color.xy+uv.xy)*.015;
    color -= length(uv)*.1;
    color =cc(color,.5,.6);

    //HSV post processing
    vec3 hsv_color = rgb2hsv(color);
    hsv_color.y += 0.05;
    hsv_color.z += 0.05;
    color = hsv2rgb(hsv_color);
    out_color = vec4(color,1.0);




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
