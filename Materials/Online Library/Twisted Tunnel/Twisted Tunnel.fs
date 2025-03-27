/*{
    "CREDIT": "Tater, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/7dVXDt",

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
            "LABEL": "Camera/Enable",
            "NAME": "mat_camera_enable",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },

        {
            "LABEL": "Camera/Camera",
            "NAME": "mat_camera",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },

        {
            "LABEL": "Tunnel/Direction",
            "NAME": "mat_direction",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,-0.8]
        },

        {
            "LABEL": "Tunnel/Distance",
            "NAME": "mat_dist",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/Depth",
            "NAME": "mat_depth",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tunnel/Thickness",
            "NAME": "mat_thick",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tunnel/Twist",
            "NAME": "mat_twist",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Tunnel/Factor 1",
            "NAME": "mat_p1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Tunnel/Factor 2",
            "NAME": "mat_p2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Tunnel/Steps",
            "NAME": "mat_steps",
            "TYPE": "float",
            "MIN": 15.0,
            "MAX": 300.0,
            "DEFAULT": 178.0
        },

        {
            "LABEL": "Spiral/Speed",
            "NAME": "mat_a1_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Spiral/BPM Sync",
            "NAME": "mat_a1_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Spiral/Reverse",
            "NAME": "mat_a1_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Spiral/Offset",
            "NAME": "mat_a1_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Spiral/Offset Scale",
            "NAME": "mat_a1_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Spiral/Strob",
            "NAME": "mat_a1_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Travel/Speed",
            "NAME": "mat_a2_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Travel/BPM Sync",
            "NAME": "mat_a2_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Travel/Reverse",
            "NAME": "mat_a2_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },

        {
            "Label": "Travel/Offset",
            "NAME": "mat_a2_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Travel/Offset Scale",
            "NAME": "mat_a2_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Travel/Strob",
            "NAME": "mat_a2_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        {
            "LABEL": "Color/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.094,
                0.0,
                0.2,
                1.0
            ]
        },

        {
            "LABEL": "Color/Cycle 1",
            "NAME": "mat_color_p1",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color/Cycle 2",
            "NAME": "mat_color_p2",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Color/Gain",
            "NAME": "mat_gain",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
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
            "NAME": "mat_a1_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a1_speed",
                "speed_curve":2,
                "reverse": "mat_a1_reverse",
                "strob" : "mat_a1_strob",
                "bpm_sync": "mat_a1_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_a2_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_a2_speed",
                "speed_curve":2,
                "reverse": "mat_a2_reverse",
                "strob" : "mat_a2_strob",
                "bpm_sync": "mat_a2_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]

}*/

#include "MadCommon.glsl"

float mat_a1_time = (mat_a1_source - mat_a1_offset * 8. * mat_a1_offset_scale) * 0.5;
float mat_a2_time = (mat_a2_source - mat_a2_offset * 8. * mat_a2_offset_scale) * 0.5;
vec2 matRot2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// #define MDIST 60.0
// #define STEPS 178.0
// #define MDIST 60.0

float MDIST = 60. * mat_depth;
// #define STEPS 30.0

#define rot(a) mat2(cos(a),sin(a),-sin(a),cos(a))
#define pmod(p,x) (mod(p,x)-0.5*(x))

float pi = PI;


vec3 hsv(vec3 c){
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}
//My poor mans version of Javad Taba's helix function
vec3 spiral(vec3 p, float R){

    p.xz*=rot(p.y/R);
    vec2 s = sign(p.xz);
    p.xz=abs(p.xz) / pow(mat_p2,0.25) - R*0.5 * mat_p1;



    p.xz*=rot(mat_a1_time*pi/3.);

    float poy = p.y;
    p.y=0.;
    //This is supposed to correct for the distortion that happens
    //when you twist a shape by rotating it over an axis.
    //In my head it should work a lot better than it does, but
    //it definitely helps a little bit so I guess it's better than nothing
    p.yz*=rot(mix(0.,pi/4.,1./(R*0.5+1.5)))*-sign(s.x*s.y);
    p.y=poy * mat_twist;
    return p;
}


vec2 map(vec3 p){
    float t = mat_a2_time*0.5;
    // p.y+=sin(-p.z*0.1)*2.;

    // tunnel curvature
    // p.y-=p.z*p.z*0.008 * pow(mat_p3, 1.);

    vec2 direction = mat_direction;
    direction += vec2(0.5);
    direction.x = 1.-direction.x;
    direction-= vec2(0.5);

    p.x+=direction.x*p.z*p.z*0.01;
    p.y+=direction.y*p.z*p.z*0.01;

    p.zy*=rot(pi/2.);
    vec3 po = p;

    p.y-=t*pi*4.339;

    vec2 a = vec2(1);
    vec2 b = vec2(2);

    p.xz*=rot(-0.05*(mat_a2_time/3.));
    p = spiral(p,6.6);
    p = spiral(p,2.);
    p = spiral(p,1.);
    p = spiral(p,0.4);
    //there are some small artifacts but you dont notice them ;)
    // p = spiral(p,0.6);

    //p = spiral(p,0.1);
    //vec2 d = abs(p.xz);
    //a.x = max(d.x,d.y)-1.0;
    a.x = length(p.xz)-0.1 * pow(mat_thick, 1.75);
    //a.x = max((abs(po.y)-7.),a.x);
    a.x*=0.6;
    return vec2(a);
}
vec3 norm(vec3 p){
    vec2 e = vec2(0.005,0);
    return normalize(map(p).x-vec3(
    map(p-e.xyy).x,
    map(p-e.yxy).x,
    map(p-e.yyx).x));
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

    vec3 col = vec3(0);
    vec3 ro = vec3(0,0.,-1.)*1.5*pow(mat_dist,2.5);
    if(mat_camera_enable){
        vec2 camera = mat_camera;

        camera.y *= -1;
        camera += vec2(0.5);

        ro.yz*=rot(3.0*(camera.y-0.5));
        ro.zx*=rot(-7.0*(camera.x-0.5));
    }
    vec3 lk = vec3(0,0,0);
    vec3 f = normalize(lk-ro);
    vec3 r = normalize(cross(vec3(0,1,0),f));
    vec3 rd = normalize(f*(0.9)+uv.x*r+uv.y*cross(f,r));
    vec3 p = ro;
    float dO = 0.;
    bool hit = false;
    vec2 d= vec2(0);
    for(float i = 0.; i<mat_steps; i++){
        p = ro+rd*dO;
        d = map(p);

        dO+=d.x;
        if(abs(d.x)<0.005||i>mat_steps-1.5){
            hit = true;
            break;
        }
        if(dO>MDIST){
            dO = MDIST;
            break;
        }
    }
    if(hit)
    {
        vec3 ld = normalize(-vec3(p.x,p.y,p.z-5.));

        //sss from nusan
        float sss=0.01;
        for(float i=1.; i<20.; ++i){
            float dist = i*0.35 * pow(mat_color_p1, 2.);
            sss += smoothstep(0.,1.,map(p+ld*dist).x/dist)*0.18*1.25;
        }
        for(float i=1.; i<5.; ++i){
            float dist = i*0.7 * pow(mat_color_p2, 4.);
            sss += smoothstep(0.,1.,map(p-ld*dist).x/dist)*0.25;
        }
        vec3 al = vec3(0.204,0.267,0.373);

        // al = vec3(1.);
        vec3 n = norm(p);
        vec3 r = reflect(rd,n);
        float diff = max(0.,dot(n,ld));
        float amb = dot(n,ld)*0.45+0.55;
        float spec = pow(max(0.,dot(r,ld)),40.0);
        #define AO(a,n,p) smoothstep(-a,a,map(p+n*a).x)
        float ao = AO(.3,n,p)*AO(.5,n,p)*AO(.9,n,p);

        col = al*
        mix(vec3(0.169,0.000,0.169),vec3(0.984,0.996,0.804),mix(amb,diff,0.75))
        +spec*0.3;

        col *= mat_gain;

        // col = al*
        // mix(vec3(0.0,0.0,0.0),vec3(1.),mix(amb,diff,0.75))
        // +spec*0.3;


        col+=sss*hsv(vec3(0.76,0.9,1.35));
        // col+=sss*hsv(vec3(1.));

        // col*=mix(ao,1.,0.5);
        col = pow(col,vec3(0.7));
    }

    // vec3 bg = mix(vec3(0.094,0.000,0.200),vec3(0.600,0.000,0.600),length(rd.xy)-0.65);
    // col = mix(col,bg,pow(dO/MDIST,2.5));
    // out_color = vec4(col,1.0);


    vec4 bg = mix(mat_back_color,vec4(0.6,0.0,0.6,1.0),length(rd.xy)-0.65);
    out_color = mix(vec4(col,1.),bg,pow(dO/MDIST,2.5));

    // out_color = vec4(col * mat_front_color.rgb, 1.);




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
