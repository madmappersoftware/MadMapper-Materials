/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Tater, adapted by Jason Beyers",

    "DESCRIPTION": "From https://www.shadertoy.com/view/fd3SRN",

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
            "LABEL": "Cube/Orbit",
            "NAME": "mat_orbit",
            "TYPE": "point2D",
            "MIN": [-1.0,-1.0],
            "MAX": [1.0,1.0],
            "DEFAULT": [0.0,0.0]
        },
        {
            "LABEL": "Cube/Scale",
            "NAME": "mat_cube_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Cube/Block Size",
            "NAME": "mat_size",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Cube/Min Size",
            "NAME": "mat_min_size",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },



        {
            "LABEL": "Cube/Pad Factor",
            "NAME": "mat_pad_factor",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Cube/Break Chance",
            "NAME": "mat_break_chance",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },


        {
            "LABEL": "Cube/Cycle",
            "NAME": "mat_cycle",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Cube/Seed",
            "NAME": "mat_seed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Cube/Iterations",
            "NAME": "mat_iterations",
            "TYPE": "int",
            "MIN": 1,
            "MAX": 5,
            "DEFAULT": 5
        },
        {
            "LABEL": "Explode/Amplitude",
            "NAME": "mat_explode_amp",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Amp Factor 1",
            "NAME": "mat_explode_factor1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Amp Factor 2",
            "NAME": "mat_explode_factor2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Mod 1",
            "NAME": "mat_explode_mod1",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Mod 2",
            "NAME": "mat_explode_mod2",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Mod 3",
            "NAME": "mat_explode_mod3",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Mod 4",
            "NAME": "mat_explode_mod4",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/Animate",
            "NAME": "mat_explode_animate",
            "TYPE": "bool",
            "DEFAULT": true,
            "FLAGS": "button"
        },
        {
            "LABEL": "Explode/Speed",
            "NAME": "mat_explode_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Explode/BPM Sync",
            "NAME": "mat_explode_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Explode/Reverse",
            "NAME": "mat_explode_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Explode/Offset",
            "NAME": "mat_explode_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Explode/Offset Scale",
            "NAME": "mat_explode_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Explode/Strob",
            "NAME": "mat_explode_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Explode/Restart",
            "NAME": "mat_explode_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Rotate/Speed",
            "NAME": "mat_rot_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Rotate/BPM Sync",
            "NAME": "mat_rot_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Rotate/Reverse",
            "NAME": "mat_rot_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "Label": "Rotate/Offset",
            "NAME": "mat_rot_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Rotate/Offset Scale",
            "NAME": "mat_rot_offset_scale",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Rotate/Strob",
            "NAME": "mat_rot_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "LABEL": "Rotate/Restart",
            "NAME": "mat_rot_restart",
            "TYPE": "event",
        },
        {
            "LABEL": "Color/Lighting",
            "NAME": "mat_lighting",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Color/Shadow",
            "NAME": "mat_shadow",
            "TYPE": "float",
            "MIN": 0.01,
            "MAX": 2.0,
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
        },
        {
            "LABEL": "Background/Back Color",
            "NAME": "mat_back_color",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },
        {
            "LABEL": "Background/Gradient",
            "NAME": "mat_back_grad",
            "TYPE": "bool",
            "DEFAULT": true,
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
            "NAME": "mat_explode_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_explode_speed",
                "speed_curve":2,
                "reverse": "mat_explode_reverse",
                "strob" : "mat_explode_strob",
                "reset": "mat_explode_restart",
                "bpm_sync": "mat_explode_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
        {
            "NAME": "mat_rot_time_source",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_rot_speed",
                "speed_curve":2,
                "reverse": "mat_rot_reverse",
                "strob" : "mat_rot_strob",
                "reset": "mat_rot_restart",
                "bpm_sync": "mat_rot_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        },
    ]

}*/

#include "MadCommon.glsl"

float mat_explode_time = mat_explode_time_source - mat_explode_offset * 4. * mat_explode_offset_scale;
float mat_rot_time = mat_rot_time_source - mat_rot_offset * 2. * PI * mat_rot_offset_scale;

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
    uv *= mat_scale;

    uv = mirrorUV(uv);

    vec2 uv_shift = mat_shift_amount * mat_shift_scale / 2.;
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

//Building on ideas from
//https://www.shadertoy.com/view/NsKGDy
//https://www.shadertoy.com/view/7sKGRy
//https://www.shadertoy.com/view/fsyGD3
//https://www.shadertoy.com/view/fdyGDt
//https://www.shadertoy.com/view/7dVGDd

//heavily inspired by
//https://twitter.com/adamswaab/status/1437498093797212165

//Toggle Shadows
//#define USE_SHADOWS

float MDIST = 350.0;
float STEPS = 128.0;
#define rot(a) mat2(cos(a),sin(a),-sin(a),cos(a))
#define pmod(p,x) (mod(p,x)-0.5*(x))

//iq palette
vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d ){
    return a + b*cos(2.*PI*(c*t+d));
}
float h21 (vec2 a) {
    return fract(sin(dot(a.xy,vec2(12.9898,78.233)))*43758.5453123);
}
float h11 (float a) {
    return fract(sin((a)*12.9898)*43758.5453123);
}
float box(vec3 p, vec3 b){
    vec3 d = abs(p)-b;
    return max(d.x,max(d.y,d.z));
}
//iq box sdf
float ebox( vec3 p, vec3 b ){
  vec3 q = abs(p) - b;
  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

float swave(float x, float a){
    return (sin(mat_explode_mod4*x*PI/3.-PI/2. + (mat_explode_mod3 - 1.))/sqrt(a*a+sin(x*PI/3.-PI/2.)*sin(x*PI/3.-PI/2.)) / mat_explode_factor2 +1./sqrt(a*a+1.) * mat_explode_factor1)*0.5;
}
vec3 rdg = vec3(0);
float nsdf = 0.;
vec2 blocks(vec3 p, vec3 scl, vec3 rd){
    vec3 po = p;
    float t = mat_explode_time;

    if (!mat_explode_animate) {
        t = 0.;
    }

    bvec3 isEdge = bvec3(true);
    vec3 dMin = vec3(-0.5) * scl;
    vec3 dMax = vec3(0.5) * scl;
    vec3 dMini = dMin;
    vec3 dMaxi = dMax;

    float id = 0.;
    float seed = floor(t/4.);

    float MIN_SIZE = 0.5 * mat_min_size;
    float ITERS = float(mat_iterations);
    float PAD_FACTOR = 1.01 * mat_pad_factor;
    float BREAK_CHANCE = 0.2 * mat_break_chance;

    vec3 dim = dMax - dMin;
    //Big thanks for @0b5vr for cleaner version of subdiv algo
    for (float i = 0.; i < ITERS; i++) {

        vec3 divHash = vec3(
            h21( vec2( i + id, seed )),
            h21( vec2( i + id + 2.44, seed )),
            h21( vec2( i + id + 7.83, seed ))
        );
        if(i==0.0){
        divHash = vec3(0.49,0.5,.51) * mat_cycle;
        }
        if(i>0.){
            vec3 center = -(dMin + dMax)/2.0;

            vec3 cs = vec3(0.3) * (mat_seed + 1.);
            divHash = clamp(divHash,vec3(cs*sign(center)),vec3(1.0-cs*sign(-center)));

        }
        vec3 divide = divHash * dim + dMin;
        divide = clamp(divide, dMin + MIN_SIZE * PAD_FACTOR , dMax - MIN_SIZE * PAD_FACTOR );
        vec3 minAxis = min(abs(dMin - divide), abs(dMax - divide));

        float minSize = min( minAxis.x, min( minAxis.y, minAxis.z ) );
        bool smallEnough = minSize < MIN_SIZE;

        bool willBreak = false;
        if (i  > 0. && h11( id ) < BREAK_CHANCE ) { willBreak = true; }
        if (smallEnough && i  > 0.) { willBreak = true; }
        if( willBreak ) {
            break;
        }

        dMax = mix( dMax, divide, step( p, divide ));
        dMin = mix( divide, dMin, step( p, divide ));

        float pad = 0.01;
        if(dMaxi.x>dMax.x+pad&&dMini.x<dMin.x-pad)isEdge.x=false;
        if(dMaxi.y>dMax.y+pad&&dMini.y<dMin.y-pad)isEdge.y=false;
        if(dMaxi.z>dMax.z+pad&&dMini.z<dMin.z-pad)isEdge.z=false;

        vec3 diff = mix( -divide, divide, step( p, divide));
        id = length(diff + 1.0);

        dim = dMax - dMin;
    }
    float volume = dim.x*dim.y*dim.z;
    vec3 center = (dMin + dMax)/2.0;
    float b = 0.;



    if(any(isEdge)) {
        float expand;

        // if (mat_explode_animate) {
        //     expand = 1.0+(3.0-h11(id)*3.)*swave(t*3.0+h11(id)*1.5 * mat_explode_mod2,0.17 * mat_explode_mod1) * mat_explode_amp;
        // } else {
        //     expand = mat_explode_amp;
        // }


        expand = (1.0+(3.0-h11(id)*3.)*swave(t*3.0+h11(id)*1.5 * mat_explode_mod2,0.17 * mat_explode_mod1)) * mat_explode_amp;


        if(isEdge.x){
        center.x*=expand;
        }
        else if(isEdge.y){
        center.y*=expand;
        }
        else if(isEdge.z){
        center.z*=expand;
        }
    }
    vec3 edgeAxis = mix(dMin, dMax, step(0.0, rd));
    vec3 dAxis = abs(p - edgeAxis) / (abs(rd) + 1E-4);
    float dEdge = min(dAxis.x,min(dAxis.y,dAxis.z));
    b= dEdge;

    vec3 d = abs(center);
    dim-=0.4 / mat_size;
    float a = ebox(p-center,dim*0.5)-0.2;

    if(!any(isEdge)){
        a=b;

        nsdf =5.;
    }
    else nsdf = a;
    a = min(a, b);

    id = h11(id)*1000.0;

    return vec2(a,id);
}

vec3 map(vec3 p){
    float t = mat_rot_time;
    vec3 po = p;
    vec2 a = vec2(1);

    vec3 scl = vec3(10.) * mat_cube_scale;
    vec3 rd2 = rdg;

    p.xz*=rot(t);
    rd2.xz*=rot(t);
    p.xy*=rot(PI/4.);
    rd2.xy*=rot(PI/4.);
    a = blocks(p,scl,rd2)+0.01;

    a.x = max(box(p,vec3(scl*2.0)),a.x);

    return vec3(a,nsdf);
}
vec3 norm(vec3 p){
    vec2 e = vec2(0.01,0.);
    return normalize(map(p).x-vec3(
    map(p-e.xyy).x,
    map(p-e.yxy).x,
    map(p-e.yyx).x));
}



vec4 materialColorForPixel(vec2 texCoord)
{
    vec4 out_color; // Final output color
    vec2 uv = texCoord - vec2(0.5);

    uv = transformUV(uv);

    vec3 col = vec3(0);
    vec3 ro = vec3(0,3.5,-20)*2.;

    vec2 orbit = mat_orbit;

    orbit += vec2(0.5);
    orbit.y = 1.-orbit.y;
    orbit -= vec2(0.5);

    ro.yz*=rot(2.0*orbit.y);
    ro.zx*=rot(-7.0*orbit.x);

    ro.xz*=rot(-PI/4.);
    vec3 lk = vec3(0,0.,0);
    vec3 f = normalize(lk-ro);
    vec3 r = normalize(cross(vec3(0,1,0),f));
    vec3 rd = normalize(f*(0.99)+uv.x*r+uv.y*cross(f,r));
    rdg = rd;
    vec3 p = ro;
    float dO = 0.;
    vec3 d = vec3(0);
    bool hit = false;
    for(float i = 0.; i<STEPS; i++){
        p = ro+rd*dO;
        d = map(p);
        dO+=d.x;
        if(abs(d.x)<0.0001||i==STEPS-1.){
            hit = true;
            break;
        }
        if(d.x>MDIST){
            dO=MDIST;
            break;
        }
    }
    if(hit){
        vec3 ld = normalize(vec3(0.,1.,0));
        vec3 n = norm(p);
        vec3 r = reflect(rd,n);
        vec3 e = vec3(0.5);
        vec3 al = pal(fract(d.y)*0.8-0.15,e*1.4,e,e*2.0,vec3(0,0.33,0.66)) * mat_lighting;
        col = al;


        float shadow = 1. * 1./mat_shadow;

    #ifdef USE_SHADOWS
        rdg = ld;
        for(float h = 0.09; h<10.0;){
            vec3 dd = map(p+ld*h+n*0.2);
            if(dd.x<0.001){shadow = 0.3; break;}
            //shadow = min(shadow,dd.z*20.0);
            h+=dd.x;
        }
    #endif

        //lighting EQs from @blackle
        float spec = length(sin(r*5.)*.5+.5)/sqrt(3.);
        float fres = 1.-abs(dot(rd,n))*0.9;

        float diff = length(sin(n*2.)*.5+.65)/sqrt(3.);

        #define AO(a,n,p) smoothstep(-a,a,map(p+n*a).z)
        float ao = AO(0.3,n,p)*AO(.5,n,p)*AO(.9,n,p);
        col = al*diff+pow(spec,5.0)*fres*shadow;
        col*=pow(ao,0.2);
        col*=max(shadow,0.7);
    }
    // col = pow(col,vec3(0.9));
    // vec3 bg = vec3(0.698,0.710,0.878)*(1.0-length(uv)*0.5) * mat_back_gain;
    // col = mix(col,bg,pow(clamp(dO/MDIST,0.,1.),2.0));
    // out_color = vec4(col,1.0);

    col = pow(col,vec3(0.9));

    out_color = vec4(col,1.0);

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

    vec4 back_color = mat_back_color;
    if (mat_back_grad) {
        back_color *= (1.0-length(uv)*0.5);
    }
    out_color = mix(out_color,back_color,pow(clamp(dO/MDIST,0.,1.),2.0));

    return out_color;
}
