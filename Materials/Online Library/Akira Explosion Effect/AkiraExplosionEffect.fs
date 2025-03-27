/*{
	"DESCRIPTION": "Converted from https://www.shadertoy.com/view/XdjcWc - Akira Explosion Effect - by Koltes. With some quality reduction for smoothness (was quite slow in default configuration)",
	    "RESOURCE_TYPE": "Material For MadMapper",
	"CREDIT": "by you",
	"TAGS": ["noise","texture"],
    "INPUTS": [
        { "LABEL": "Complexity", "NAME": "mat_complexity", "TYPE": "int", "MIN": 0, "MAX": 50, "DEFAULT": 15 },  
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1 },  
        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
        { "LABEL": "Reset", "NAME": "mat_reset", "TYPE": "event", "FLAGS": "button" },  
    ],
    "GENERATORS": [
        {"NAME": "mat_iTime", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve": 3, "reset": "mat_reset", "link_speed_to_global_bpm":true}},
    ]
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);


#define TAU 6.2831853
mat2 rz2(float a){float c=cos(a),s=sin(a);return mat2(c,s,-s,c);}

float hlx(vec3 p){
    float m=TAU/5.;
    float t = mat_iTime;
    float a=mod(atan(p.y,p.x)-p.z*0.5+t,m)-.5*m;
    float l=length(p.xy);
    float r = .1+.2*(sin(abs(p.z*4.)+t*4.)*.5+.5);
    return length(vec2(a*l,length(p.xy)-(.5+abs(p.z)*2.)*(1.+sin(abs(p.z)-t*10.)*.5+.5)*0.2))-r;
}

vec3 hsv(vec3 c) {
    vec4 k=vec4(1.,2./3.,1./3.,3.);
    vec3 p=abs(fract(c.xxx+k.xyz)*6.-k.www);
    return c.z*mix(k.xxx,clamp(p-k.xxx,0.,1.),c.y);
}

float map(vec3 p){
    p.xy*=rz2(mat_iTime*.3+length(p)*.05);
    p.yz*=rz2(mat_iTime*.5);
    p.xz*=rz2(mat_iTime*.7);
    float d=min(min(hlx(p.xyz), hlx(p.yzx)),hlx(p.zxy))*(1.+sin(length(p*1.)-mat_iTime*4.)*0.8);
    vec3 n=normalize(sign(p));
    float d2=max(length(max(abs(p)-vec3(1.),0.)),dot(p,n)-2.3);
    return d;//min(d,d2);
}
float st(float x,float m){return floor(x*m)/m;}
vec4 materialColorForPixel(vec2 fragCoord)
{
    vec2 uv = fragCoord.xy-vec2(.5);
    vec3 ro=vec3(uv,sin(mat_iTime)*2.-20.*mat_scale*mat_scale),rd=vec3(uv,1.),mp=ro;
    float shade = 0.;
    int i = 0;
    float t = mat_iTime;
    for(;i<mat_complexity;++i){
        float md=map(mp);
        if(md<.01){
            break;
        }
        mp+=rd*md*(.25*50/mat_complexity);
    }
    float r=float(i)/50.;
    vec4 fragColor = vec4(hsv(vec3(st(length(mp)*.01-mat_iTime*0.2,6.),.8,1.)),1.);
    fragColor *= 1.-r*(1.-r)*-1.;
    fragColor *= length(mp-ro)*.02;
    fragColor = 1.-fragColor;
    if(length(mp)>20.)fragColor=vec4(0.);
	return fragColor;
}

