/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nadapted from Inigo Iquilez\nrequires a fast Gpu",
    "TAGS": "SDF",
    "INPUTS": [ 
		{ "Label": "SDF/SDF Precision", "NAME": "uPrecision", "TYPE": "int", "MIN": 0, "MAX": 200, "DEFAULT": 200 },
		{ "LABEL": "SDF/SDF Grow", "NAME": "uGrow", "TYPE": "float", "MIN": 0.2, "MAX": 2.0, "DEFAULT": 1.6 },
		{ "LABEL": "SDF/Light Precision", "NAME": "uShade", "TYPE": "int", "MIN": 1, "MAX": 64, "DEFAULT": 32 },
		{ "Label": "Speed/Speed X", "NAME": "x_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.7 }, 
        { "Label": "Speed/Speed Y", "NAME": "y_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.7 },
		{ "Label": "Speed/Speed Z", "NAME": "z_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.1 }, 
        { "Label": "Speed/Speed W", "NAME": "w_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.2 },


		{ "LABEL": "Deform_X", "NAME": "uDeformX", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.20 },
		{ "LABEL": "Deform_Y", "NAME": "uDeformY", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.90 },
		{ "LABEL": "Deform_Z", "NAME": "uDeformZ", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.40 },
		{ "LABEL": "Deform_W", "NAME": "uDeformW", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.80 },
		
		{ "LABEL": "Cam/Zoom", "NAME": "zoom", "TYPE": "float", "MIN": 0.05, "MAX": 5.0, "DEFAULT": 3.0 },
		{ "LABEL": "Cam/Orbit", "NAME": "uCam", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Cam/Target", "NAME": "uTarget", "TYPE": "point2D", "MAX": [ 2.0, 2.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Environment", "NAME": "uEnv", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Color/Fog", "NAME": "uFog", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },
		{ "LABEL": "Color/Base Color", "NAME": "uColorBase", "TYPE": "color", "DEFAULT": [ 0.3, 0.3, 0.3, 1.0 ] },
		{ "LABEL": "Color/Main Color", "NAME": "uColorMain", "TYPE": "color", "DEFAULT": [ 0.6, 0.6, 0.6, 1.0 ] },
		{ "LABEL": "Color/Shade Color", "NAME": "uColorLight", "TYPE": "color", "DEFAULT": [ 0.9, 0.9, 0.9, 1.0 ] },
		{ "LABEL": "Color/Tint Color", "NAME": "uColorTint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
      ],
	 "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
		{"NAME": "x_time", "TYPE": "time_base", "PARAMS": {"speed": "x_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "y_time", "TYPE": "time_base", "PARAMS": {"speed": "y_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
		{"NAME": "z_time", "TYPE": "time_base", "PARAMS": {"speed": "z_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "w_time", "TYPE": "time_base", "PARAMS": {"speed": "w_speed", "speed_curve": 2, "link_speed_to_global_bpm":true}},
    ],
	    "IMPORTED": [
        {"NAME": "env", "PATH": "flare.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
float hash1( float n )
{
    return fract(sin(n)*43758.5453123);
}

float hash1( in vec2 f ) 
{ 
    return fract(sin(f.x+131.1*f.y)*43758.5453123); 
}

const float PHI = 1.6180339887498948482045868343656;

vec3 forwardSF( float i, float n) 
{
    float phi = 2.0*PI*fract(i/PHI);
    float zi = 1.0 - (2.0*i+1.0)/n;
    float sinTheta = sqrt( 1.0 - zi*zi);
    return vec3( cos(phi)*sinTheta, sin(phi)*sinTheta, zi);
}

vec4 grow = vec4(1.0);

vec3 mapP( vec3 p )
{
    p.xyz += 1.000*cos(  2.0*p.yzx +x_time)*grow.x*uDeformX;
    p.xyz += 0.500*cos(  4.0*p.yzx -y_time)*grow.y*uDeformY;
    p.xyz += 0.250*sin(  8.0*p.yzx +z_time)*grow.z*uDeformZ;	
    p.xyz += 0.125*cos( 16.0*p.yzx -w_time)*grow.w*uDeformW;	
    return p;
}

float map( vec3 q )
{
    vec3 p = mapP( q );
    float d = length( p ) - 1.5;
	return d * 0.05;
}

float intersect( in vec3 ro, in vec3 rd )
{
	const float maxd = 8.0;

	float precis = 0.001;
    float h = 1.0;
    float t = 1.0;
    for( int i=0; i<uPrecision; i++ )
    {
        if( (h<precis) || (t>maxd) ) break;
	    h = map( ro+rd*t );
        t += h;
    }

    if( t>maxd ) t=-1.0;
	return t;
}

vec3 calcNormal( in vec3 pos )
{
    vec3 eps = vec3(0.005,0.0,0.0);
	return normalize( vec3(
           map(pos+eps.xyy) - map(pos-eps.xyy),
           map(pos+eps.yxy) - map(pos-eps.yxy),
           map(pos+eps.yyx) - map(pos-eps.yyx) ) );
}

float calcAO( in vec3 pos, in vec3 nor, in vec2 pix )
{
	float ao = 0.0;
    for( int i=0; i<uShade; i++ )
    {
        vec3 ap = forwardSF( float(i), 64.0 );
		ap *= sign( dot(ap,nor) ) * hash1(float(i));
        ao += clamp( map( pos + nor*0.05 + ap*1.0 )*32.0, 0.0, 1.0 );
    }
	ao /= float(uShade);
	
    return clamp( ao*ao, 0.0, 1.0 );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord;
	vec2 p = uv*2.0 - vec2(1.0);

	vec2 q = uv;
    vec2 m = vec2(0.5);
 
    grow = vec4(uGrow);
	
    //-----------------------------------------------------
    // camera
    //-----------------------------------------------------
	
	float an = 0.5;
	vec3 ro = vec3(-uCam.x,uCam.y,zoom+2);
	
	// target
    vec3 ta = vec3(uTarget,0.0);
    // camera matrix
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(0.0,1.0,0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
	// create view ray
	vec3 rd = normalize( p.x*uu + p.y*vv + 4.5*ww );
	
    //-----------------------------------------------------
	// render
    //-----------------------------------------------------
    
	vec3 col = vec3(0);

	// raymarch
    float t = intersect(ro,rd);

    if( t>0.0 )
    {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal(pos);
		vec3 ref = reflect( rd, nor );
        vec3 sor = nor;
        
        vec3 q = mapP( pos );
        float occ = calcAO( pos, nor, gl_FragCoord.xy ); occ = occ*occ;
		
        // materials
		col = uColorBase.rgb;
        float ar = clamp(1.0-0.7*length(q-pos),0.0,1.0);
		
		// Main Color
        col = mix( col, uColorMain.rgb + col, ar);

        float ks = 0.5;
        ks = 0.5 + 1.0*ks;
        ks *= (1.0-ar);
        
        // lighting
        float sky = 0.5 + 0.5*nor.y;
        float fre = clamp( 1.0 + dot(nor,rd), 0.0, 1.0 );
        float spe = pow(max( dot(-rd,nor),0.0),8.0);
		// lights
		vec3 lin  = vec3(0.0);
		     lin += 3.0*vec3(0.7,0.80,1.00)*sky*occ;
             lin += 1.0*fre*uColorLight.rgb*(0.1+0.9*occ);
        col += 0.3*ks*4.0*vec3(0.7,0.8,1.00)*smoothstep(0.0,0.2,ref.y)*(0.05+0.95*pow(fre,5.0))*(0.5+0.5*nor.y)*occ;
        col += 0.0*ks*1.5*spe*occ*col.x;
        col += 2.0*ks*1.0*pow(spe,8.0)*occ*col.x;
        col = col * lin;
		
		vec3 environment = texture(env,ref.xy).rgb;
		col += environment*clamp(pos.z,0.0,1.0)*uEnv;
		col *= pow(clamp(pos.z,0.0,1.0),uFog*5.0);
    }else{discard;}

	col = pow(col,vec3(0.4545));
  //  col = pow( col, vec3(1.0,1.0,1.4) ) + vec3(0.0,0.02,0.14);  
	col *= uColorTint.rgb;
	
	//////// brightness + contrast
	// Apply contrast
    col = mix(vec3(0.5), col, contrast);
    // Apply brightness
    col += vec3(brightness);
		
	return vec4(col,1);
}

// Originally Created by inigo quilez - iq/2015
// Adapted by Franz / 1024
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
