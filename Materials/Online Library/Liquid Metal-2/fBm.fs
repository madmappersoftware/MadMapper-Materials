/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Converted by ProjectileObjects from Shadertoy by mrange",
    "DESCRIPTION": "fBM Liquid Metal as Material",
    "TAGS": "noise, metal",
    "VSN": "1.0",
    "INPUTS": [
	    { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.0 },
	    { "LABEL": "Ripple", "NAME": "ripple", "TYPE": "float", "MIN": 0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "LABEL": "Scale", "NAME": "uScale", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 0.416 },
		{ "LABEL": "Noise 1", "NAME": "uN1", "TYPE": "float", "MIN": 0, "MAX": 5.0, "DEFAULT": 1.36 },
		{ "LABEL": "Noise 2", "NAME": "uN2", "TYPE": "float", "MIN": 0, "MAX": 5.0, "DEFAULT": 1.52 },
		{ "LABEL": "Noise 3", "NAME": "uN3", "TYPE": "float", "MIN": 0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Height", "NAME": "h", "TYPE": "float", "MIN": -100, "MAX": 100.0, "DEFAULT": 0.0 },
		{ "LABEL": "Depth", "NAME": "d", "TYPE": "float", "MIN": -100, "MAX": 100.0, "DEFAULT": 0.0 },


	//    { "LABEL": "Global/Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },

		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : 0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Red", "NAME": "colorR", "TYPE": "float", "MIN": 1, "MAX": 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Blue", "NAME": "colorB", "TYPE": "float", "MIN": 0.6, "MAX": 5.0, "DEFAULT": 0.6 },
		{ "LABEL": "Color/Green", "NAME": "colorG", "TYPE": "float", "MIN": 0.8, "MAX": 5.0, "DEFAULT": 0.8 },
	  ],
}*/


#define PI  3.141592654
#define TAU (2.0*PI)

void rot(inout vec2 p, float a) {
  float c = cos(a*uN2);
  float s = sin(a*uN3);
  p = vec2(c*p.x + s*p.y, -s*p.x + c*p.y);
}

float hash(in vec2 co) {
  return fract(sin(dot(co.xy ,vec2(12.9898,58.233))) * 13758.5453);
}

vec2 hash2(vec2 p) {
  p = vec2(dot(p,vec2(127.1,311.7)), dot(p,vec2(269.5,183.3)));
  return fract(sin(p)*18.5453);
}

float psin(float a) {
  return 0.5 + 0.5*sin(a);
}

// TANH Function (Hyperbolic Tangent)
float tanh(float val)
{
    float tmp = exp(val);
    float tanH = (tmp - 1.0 / tmp) / (tmp + 1.0 / tmp);
    return tanH*ripple;
}
float onoise(vec2 x) 
{
  x *= 0.5;
  float a = sin(x.x);
  float b = sin(x.y);
  float c = mix(a, b, psin(TAU*tanh(a*b+a+b)));
  
  return c;
}

float vnoise(vec2 x) {
  vec2 i = floor(x);
  vec2 w = fract(x);
    
#if 1
  // quintic interpolation
  vec2 u = w*w*w*(w*(w*6.0-15.0)+10.0);
#else
  // cubic interpolation
  vec2 u = w*w*(3.0-2.0*w);
#endif    

  float a = hash(i+vec2(0.0,0.0));
  float b = hash(i+vec2(1.0,0.0));
  float c = hash(i+vec2(0.0,1.0));
  float d = hash(i+vec2(1.0,1.0));
    
  float k0 =   a;
  float k1 =   b - a;
  float k2 =   c - a;
  float k3 =   d - c + a - b;

  float aa = mix(a, b, u.x);
  float bb = mix(c, d, u.x);
  float cc = mix(aa, bb, u.y);
  
  return k0 + k1*u.x + k2*u.y + k3*u.x*u.y;
}

float fbm1(vec2 p) {
  vec2 op = p;
  const float aa = 0.45;
  const float pp = 2.03;
  const vec2 oo = -vec2(1.23, 1.5);
  const float rr = 1.2;
  
  float h = 0.0;
  float d = 0.0;
  float a = 1.0;
  
  for (int i = 0; i < 5; ++i) {
    h += a*onoise(p);
    d += (a);
    a *= aa;
    p += oo;
    p *= pp;
    rot(p, rr);
  }
  
  return mix((h/d), -0.5*(h/d), pow(vnoise(0.9*op), 0.25));
}

float fbm2(vec2 p) {
  vec2 op = p;
  const float aa = 0.45;
  const float pp = 2.03;
  const vec2 oo = -vec2(1.23, 1.5);
  const float rr = 1.2;
  
  float h = 0.0;
  float d = 0.0;
  float a = 1.0;
  
  for (int i = 0; i < 7; ++i) {
    h += a*onoise(p);
    d += (a);
    a *= aa;
    p += oo;
    p *= pp;
    rot(p, rr);
  }
  
  return mix((h/d), -0.5*(h/d), pow(vnoise(0.9*op), 0.25));
}

float fbm3(vec2 p) {
  vec2 op = p;
  const float aa = 0.45;
  const float pp = 2.03;
  const vec2 oo = -vec2(1.23, 1.5);
  const float rr = 1.2;
  
  float h = 0.0+h;
  float d = 0.0+d;
  float a = 1.0;
  
  for (int i = 0; i < 3; ++i) {
    h += a*onoise(p);
    d += (a);
    a *= aa;
    p += oo;
    p *= pp;
    rot(p, rr);
  }
  
  return mix((h/d), -0.5*(h/d), pow(vnoise(0.9*op), 0.25));
}


float pattern(vec2 p) {
  vec2 v = vec2(fbm1(p), fbm1(p+0.7*vec2(1.0, 1.0)));
  
  rot(v, 1.0+TIME*0.1);
  
  vec2 vv = vec2(fbm2(p + 3.7*v), fbm2(p + -2.7*v.yx+0.7*vec2(1.0, 1.0)));

  rot(vv, -1.0+TIME*-0.21315);
    
  return fbm3(p*uN1 + 1.4*vv);
}

float height(vec2 p) {
  float a = 0.005*TIME+rotation;
  p += 5.0*vec2(cos(a), sin(a));
  p *= 2.0;
  p += 13.0;
  float h = pattern(p);
  float rs = 3.0;
  return 0.35*tanh(rs*h)/rs;
}

vec3 normal(vec2 p) {
  // As suggested by IQ, thanks!
  vec2 eps = -vec2(2.0/RENDERSIZE.y, 0.0);
  
  vec3 n;
  
  n.x = height(p + eps.xy) - height(p - eps.xy);
  n.y = 2.0*eps.x;
  n.z = height(p + eps.yx) - height(p - eps.yx);
  
  
  return normalize(n);
}

vec3 postProcess(vec3 col, vec2 q)  {
  col=pow(clamp(col,0.0,1.0),vec3(0.75)); 
  col=col*0.6+0.4*col*col*(3.0-2.0*col);  // contrast
  col=mix(col, vec3(dot(col, vec3(0.33))), -0.4);  // satuation
  col*=0.5+0.5*pow(19.0*q.x*q.y*(1.0-q.x)*(1.0-q.y),0.7);  // vigneting
  return col;
}

vec4 materialColorForPixel(vec2 texCoord)
{
	
  vec2 q = texCoord*vec2(uScale);
  vec2 p = -1. + 2. * q;
  p.x*=RENDERSIZE.x/RENDERSIZE.y;
 
  const vec3 lp1 = vec3(0.9, -0.5, 0.8);
  const vec3 lp2 = vec3(-0.9, -1.5, 0.9);
  float h = height(p);
  vec3 pp = vec3(p.x, h, p.y);
  float ll1 = length(lp1.xz - pp.xz);
  vec3 ld1 = normalize(lp1 - pp);
  vec3 ld2 = normalize(lp2 - pp);
 
  vec3 n = normal(p);
  float diff1 = max(dot(ld1, n), 0.0);
  float diff2 = max(dot(ld2, n), 0.0);
 
  vec3 baseCol = vec3(colorR, colorG, colorB);
  float oh = height(p + ll1*0.05*normalize(ld1.xz));
  const float level0 = 0.0;
  const float level1 = 0.125;
  // VERY VERY fake shadows + hilight
  vec3 scol = baseCol*(smoothstep(level0, level1, h) - smoothstep(level0, level1, oh));
  vec3 col = vec3(0.0);
  col += baseCol*pow(diff1, 6.0);
  col += 0.1*baseCol*pow(diff1, 1.5);
  col += 0.15*baseCol.zyx*pow(diff2, 8.0);
  col += 0.015*baseCol.zyx*pow(diff2, 2.0);
  col += scol*0.5;
  col = postProcess(col, q);
	
  return vec4(col, 5.0)*brightness;
}
 