/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inspired by Andy Warhol - Velvet Underground / 1967",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Quality", "NAME": "mat_quality", "TYPE": "int", "MIN": 10, "MAX": 100, "DEFAULT": 30 },

		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. }, 
		{ "LABEL": "Rotation", "NAME": "mat_rot", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.1 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.7 },
 		{ "LABEL": "deform Banana", "NAME": "mat_deform", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Noise Banana", "NAME": "mat_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Noise Scale", "NAME": "mat_noise_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 

		{ "LABEL": "Target", "NAME": "mat_target", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0, 1 ] },
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, 2.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 2., -0.05 ] },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

const vec2 res = vec2(1080.);
const float precis = 0.001;

const vec3 yellow = vec3(0.9, 0.9, 0.);

//////// Utility
vec2 rotate2d(vec2 v, float a) {
	float s = sin(a);
	float c = cos(a);
	mat2 m = mat2(c, -s, s, c);
	return m * v;
}

////// Signed Distance Fields

//iq
float sdPlane( vec3 p, vec4 n )
{
  	return dot(p, n.xyz) + n.w;
}

//iq
float sdVerticalCapsule( vec3 p, float h, float r )
{
      p.y -= clamp( p.y, 0.0, h );
      return length( p ) - r;
}

//iq
float sdRoundBox( vec3 p, vec3 b, float r )
{
    vec3 q = abs(p) - b;
    return length(max(q, 0.0)) + min(max(q.x, max(q.y, q.z)), 0.0) - r;
}

//https://www.iquilezles.org/www/articles/smin/smin.htm
float smin( float a, float b, float k )
{
    float h = clamp( 0.5 + 0.5 * (b-a)/k, 0.0, 1.0 );
    return mix(b, a, h) - k * h * (1.0-h);
}

vec4 map( vec3 pos, bool doColor )
{
	// rotate world
	pos.y *= 0.9;
	float R = mat_rot * PI * 2.;
    pos.xz *= mat2(cos(-R), -sin(-R), sin(-R), cos(-R));

	// sine deformation
	float s = 0.15 * mat_deform;
	float sc = 4.;
	pos.x += sin(pos.y * sc + mat_animation_time) * s;
	pos.y += cos(pos.x * sc + mat_animation_time) * s;

	// noise deformation
    vec3 dN =  dfBm(pos * mat_noise_scale + vec3(0., 0., mat_animation_time)).yzw * 0.1;
	pos += dN * mat_noise;
	float dist = 10000.;

	// banana offset
    vec3 banPos = pos + vec3(0, 1.5, 0);
    banPos.x += sin(banPos.y) / 1.7;
    float dBan = sdVerticalCapsule(banPos, 2.7, 0.50);

    vec3 planePos = banPos;
    planePos.y -= 1.5;
    planePos.z = abs(planePos.z);
    float dPlane = sdPlane(planePos - vec3(-.6, 0, -0.25), (vec4(-0.5, 0.0, 0.2, 0)));
    dBan = max(dPlane, dBan);

    vec3 brown = vec3(0.);
    
	float MAX_DIST = 1000.;
	vec3 col = yellow;

    if(dBan < 0.5 && doColor)
    {
     	dist = dBan;

		float stain1 = 1.0-fBm(pos * vec3(0.25, 0.51, 1.) + 0.880);
		stain1 = step(0.9, stain1);

		float stain2 = billowedNoise(pos * vec3(0.8, 0.3, 2.) + 1.760 - vec3( 0., 0.304, 0.));
		stain2 = step(0.3, stain2);

		float microstain = fBm(pos * vec3(4., 2, 1) + fBm(pos * 4.));
		microstain = step(0.1, microstain);

		float micromask = 1. - step(0., fBm(vec3(0., 0.304, 0.) + vec3(rotate2d(pos.xy, 0.4), pos.z) * vec3(2., 0.2, 1) + 0.064) + 0.064);

		float stain = stain1 * stain2  + microstain*micromask;
		stain = clamp(stain, 0., 1.);

        vec3 banCol =  vec3(stain);

		col = yellow;	
		col *= banCol;
    }
    
    vec3 banPosStem = banPos;
    banPosStem.x += sin(banPos.y / 1.5);
    float dBanStem = sdRoundBox(banPosStem - vec3(1.18, 3.2, 0), vec3(0.12, 0.206, 0.08), 0.1);
    
    float distTemp = dist;
    dist = smin(dist, dBanStem, 0.1);
    if(dBanStem < distTemp && doColor)
    {  
     	col = mix(col, brown, step(2.8, banPos.y));     
    }
    
    vec3 banBtmPos = banPos;
    float ang = -PI/4.0;
    banBtmPos.x -= 0.1 * fBm(5.0 * banBtmPos)
        				   * step(-0.6, banBtmPos.y) * smoothstep(0.43, 0.63, banBtmPos.x)
       				   * smoothstep(banBtmPos.y, banBtmPos.y + 0.25, -0.1);

    banBtmPos.xy = mat2(cos(ang), sin(ang), -sin(ang), cos(ang)) * banBtmPos.xy;
    float dBanBtm = sdRoundBox(banBtmPos-vec3(-0.12, -0.40, 0), vec3(0.06, 0.06, 0.03), 0.2);
    distTemp = dist;
    dist = smin(dist, dBanBtm, 0.25);
   if(dBanBtm < distTemp && doColor)
    {
       col = mix(col, brown, step(-0.58, banBtmPos.y));
    }
    
    float dBanCrease = sdVerticalCapsule(banPos - vec3(0.5, -0.25, 0), 3.7, 0.01);
    dist = smin(dist, dBanCrease, 0.2);
		
	return vec4(dist, mix(yellow, col, float(doColor)));
}

vec4 intersect( in vec3 ro, in vec3 rd, in bool doColor )
{
	float h=precis*2.0;
    vec3 c;
    float t = 0.0;
	float maxd = 20.0;
    vec3 sid = vec3(1.);

    for( int i=0; i<mat_quality; i++ )
    {
        if( abs(h)<precis||t>maxd ) break;
        t += h;
	    vec4 res = map( ro + rd * t, doColor );
        h = res.x;
	    sid = res.yzw;
    }

    if( t>maxd ) sid=vec3(1.);

    return vec4( t, sid );
}

vec3 calcNormal( in vec3 pos )
{
    vec3 eps = vec3(precis, 0.0, 0.0);
    vec3 nor;
    nor.x = map(pos+eps.xyy, false).x - map(pos-eps.xyy, false).x;
    nor.y = map(pos+eps.yxy, false).x - map(pos-eps.yxy, false).x;
    nor.z = map(pos+eps.yyx, false).x - map(pos-eps.yyx, false).x;

    return normalize(nor);
}

float Fresnel(  in vec3 PixelNormal, in vec3 LightDir )
{
    float NdotL = max( 0, dot( PixelNormal, LightDir ));

    return   pow( ( 1 - NdotL ), 1.7 );
}

vec4 materialColorForPixel( vec2 texCoord )
{	
	vec2 uv = texCoord;
	uv.y = 1.- uv.y;  // flip for thumb
	uv = uv * 2. - 1.;  
	uv *= mat_scale;

   	vec2 mo = mat_offset;
	vec2 p = uv;

    // camera
	float an1 = mo.x;
	float an2 = 1.+ mo.y;
    vec3 ro = 4. * normalize(vec3(sin(an2) * cos(an1), cos(an2) - 0.5, sin(an2) * sin(an1)));
	vec3 ta = vec3(0.0, mat_target);
    vec3 ww = normalize(ta - ro);
    vec3 uu = normalize(cross(vec3(0.0, 1.0, 1.0), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec3 rd = normalize( p.x * uu + p.y * vv + 1. * ww );
	
	vec3 col = vec3(1.);
    vec4 tmat = intersect(ro, rd, true);

    // geometry
    vec3 pos = ro + tmat.x * rd;
    vec3 nor = calcNormal(pos);

 	col = tmat.yzw;

	if(pos.z > -1.){
		float F = 1. - step(0.01, Fresnel(nor,normalize(ro + vec3(-2., 2., 10.))));
		float U = 1. -step(0.55, vnoise(pos*vec3(3., 0.8, 1.)));
		col = mix(col, yellow, U * F * (1.-step(1.5, pos.y)));
	}

	return vec4(col, 1.0);
}