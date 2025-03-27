/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Author unknown, adapted by Jason Beyers",

    "DESCRIPTION": "From http:\/\/glslsandbox.com\/e#52067.3",

    "VSN": "1.0",

    "INPUTS": [


        {
            "LABEL": "Texture Mode/Lock Texture Aspect",
            "NAME": "mat_lock_aspect",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
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
            "LABEL": "Fractal/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.5,0.5]
        },
        {
            "LABEL": "Fractal/Orbit Scale",
            "NAME": "mat_orbit_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Fractal/Size",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Fractal/Fill",
            "NAME": "mat_fill",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Fractal/Trace",
            "NAME": "mat_trace",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Fractal/Surface",
            "NAME": "mat_surface",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },

        {
            "LABEL": "Fractal/Explode",
            "NAME": "mat_explode",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Fractal/Iterations 1",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 20,
            "DEFAULT": 20
        },
        {
            "LABEL": "Fractal/Iterations 2",
            "NAME": "mat_iterations2",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 100,
            "DEFAULT": 60
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
            "DEFAULT": "After Color Controls",
            "FLAGS": "generate_as_define"
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
    uv *= mat_scale * 1.2;

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

const vec2 T=vec2(1.0,0.1);
float c=cos(mat_time),s=sin(mat_time);
vec3 dir=vec3(mat_orbit * mat_orbit_scale,.5);
float ang=mat_time;
vec4 rot4=vec4(normalize(dir)*sin(0.5*ang),cos(0.5*ang));
vec4 rot4inv=vec4(-rot4.xyz,rot4.w)/dot(rot4,rot4);
vec4 mul4(vec4 a,vec4 b)
{
    return vec4(a.xyz*b.w+a.w*b.xyz-cross(a.xyz,b.xyz),a.w*b.w-dot(a.xyz,b.xyz));
}
vec4 inv4(vec4 a)
{
    a.xyz=-a.xyz;
    return a/dot(a,a);
}
vec3 rotate(vec3 pos)
{
    vec4 pos1=vec4(pos,1.0);
    vec4 q=mul4(rot4,mul4(pos1,rot4inv));
    return q.xyz;
}
float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  vec2 r=vec2(length(p.xy)-t.x,p.z);
  vec2 s=vec2(length(p.yz)-t.x,p.x);
  return min(length(s)-t.y,min(length(r)-t.y, length(q)-t.y));
}

float sdBox(vec3 p, vec3 b) {
    p = abs(p) - b;
    return length(max(p, 0.0)) + min(max(p.x, max(p.y, p.z)), 0.0);
}

float R=1. * mat_size;


vec3 ballInverse(vec3 pos,vec3 ballPos,float ballR,inout float dr)
{
    vec3 pb=pos-ballPos;
    float k=ballR*ballR/dot(pb,pb);
    dr*=k;
    return pb*k+ballPos;
}
const int balls=8;
const float sqrt3=sqrt(3.0),sqrt6=sqrt(6.0),sqrt2=sqrt(2.0);
const vec3 lightr=normalize(vec3(0.4,0.3,1.2)),lightg=normalize(vec3(-0.4,0.3,1.5)),lightb=normalize(vec3(0.0,-.5,1.2));
vec3 ballpos[balls];
float ballR[balls];
float dist(vec3 pos)
{
    ballpos[0]=vec3(0.0,0.0,0.0);
    ballR[0]=(sqrt2-1.0)*R;
    ballpos[1]=vec3(-sqrt2,0.0,0.)*R;
    ballR[1]=R;
    ballpos[2]=vec3(sqrt2,0.0,0.)*R;
    ballR[2]=R;
    ballpos[3]=vec3(0.0,-sqrt2,0.)*R;
    ballR[3]=R;
    ballpos[4]=vec3(0.0,sqrt2,0.)*R;
    ballR[4]=R;
    ballpos[5]=vec3(0.0,0.0,sqrt2)*R;
    ballR[5]=R;
    ballpos[6]=vec3(0.0,0.0,-sqrt2)*R;
    ballR[6]=R;
    ballpos[7]=vec3(0.0,0.0,0.0);
    ballR[7]=(sqrt2+1.0)*R;
    float dr=1.0,interation=0.0;
    bool transformed=false;
    for(int n=0;n<mat_iterations;n++){
        transformed=false;
        for(int i=0;i<balls;i++)if(length(pos-ballpos[i])<ballR[i]){pos=ballInverse(pos,ballpos[i],ballR[i],dr);transformed=true;interation+=1.0;break;}
        if(!transformed)break;
    }
    float r=length(pos.xy);
    return 0.25*r/dr;

}
float trace(vec3 pos,vec3 dir,out vec3 target)
{
    float total=0.0,d;
    for(int i=0;i<mat_iterations2;i++){
        d=dist(pos) / (mat_explode * 4. + 1.);
        total+=d;
        pos+=d*dir;
        if(d<0.005 * mat_fill)break;
    }
//  if(d>1.)total=-1.0;
    target=pos;
    return total * mat_trace;
}

vec3 color(vec3 pos,vec3 eyepos)
{
    vec3 dir=normalize(pos-eyepos),target,lr,lg,lb;
    float d=trace(pos,dir,target),r,g,b;
    if(d<8.0){
        d=dist(target) * pow(mat_surface,0.02);
        float dx=dist(vec3(target.x+0.0001,target.y,target.z))-d;
        float dy=dist(vec3(target.x,target.y+0.0001,target.z))-d;
        float dz=dist(vec3(target.x,target.y,target.z+0.0001))-d;
        vec3 normal=normalize(vec3(dx,dy,dz));
        lr=rotate(lightr);
        lb=rotate(lightb);
        lg=rotate(lightg);
        r=dot(normal,lr);g=dot(normal,lg);b=dot(normal,lb);
    }
    return vec3(r,g,b);
}

vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec2 position = uv;

    vec3 pos=vec3(position,3.0),eyepos=vec3(0.0,0.0,5.0);
    pos=rotate(pos);
    eyepos=rotate(eyepos);
    vec3 clr = color(pos,eyepos);
    clr=pow(clr,vec3(0.2));

    out_color = vec4( clr, 1.0 );


    // Luma to alpha (before color controls)
    if (mat_luma_to_alpha && mat_luma_mode == 0) {
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
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
        out_color.a = mat_luma(out_color * mat_luma_sensitivity * 4.) - mat_luma_threshold * 4.;
    }

    return out_color;
}
