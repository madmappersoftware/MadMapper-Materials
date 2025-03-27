/*{
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Inspired by Sol Lewitt - Wall Drawing NO 565 / 1988",
    "TAGS": "painting",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.4 }, 
        { "LABEL": "Mix Noise", "NAME": "mat_mix_noise", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

vec2 hash2( vec2 p )
{
    // Procedural white noise	
	return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

float U;

vec3 voronoi( in vec2 x )
{
    vec2 n = floor(x);
    vec2 f = fract(x);

    //----------------------------------
    // first pass: regular voronoi
    //----------------------------------
	vec2 mg, mr;

    float md = 8.0;
    for( int j=-1; j<=1; j++ )
    for( int i=-1; i<=1; i++ )
    {
        vec2 g = vec2(float(i),float(j));
		vec2 o = hash2( n + g );


		#ifdef ANIMATE
        o = 0.5 + 0.5*sin( mat_animation_time + 6.2831*o );
        #endif	
        vec2 r = g + o - f;
        float d = dot(r,r);

        if( d<md )
        {
            md = d;
            mr = r;
            mg = g;
        }
    }

    //----------------------------------
    // second pass: distance to borders
    //----------------------------------
    md = 8.0;
    for( float j=-1; j<=1; j++ )
    for( float i=-1; i<=1; i++ )
    {
        vec2 g = mg + vec2(float(i),float(j));
		vec2 o = hash2( n + g );
		#ifdef ANIMATE
        o = vec2(0.5 + 0.5*sin( mat_animation_time + 6.2831 ))*o;		
        #endif	
        vec2 r = g + o - f;
		U = o.x;
        if( dot(mr-r,mr-r)>0.00001 )
        md = min( md, dot( 0.75*(mr+r), normalize(r-mr) ) );
    }

    return vec3( md, mr );
}

mat3 rotateZ(float rad) {
    float c = cos(rad);
    float s = sin(rad);
    return mat3(
        c, s, 0.0,
        -s, c, 0.0,
        0.0, 0.0, 1.0
    );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord-0.5;

	// modify uv with material inputs
	uv *= mat_scale;
	

	float F1 = worleyNoise(vec3(uv*10.,mat_animation_time));
	float F2 = billowedNoise(vec3(uv*10.,mat_animation_time));
	vec2 F = vec2(F1,F2)*0.03*mat_mix_noise;
	
	// make a color out of it
	vec3 color = vec3( 1.);

    // computer voronoi patterm
    vec3 c = voronoi( (14.0+6.0*sin(0.2))*(uv+F) );

	// isolines
    vec3 col = c.x*(0.5 + 0.5*sin(64.0*c.x))*vec3(1.0);
    // borders	

	col = vec3(smoothstep( 0.04, 0.07, c.x ) );

	float r = billowedNoise(vec2(U*12.32,mat_animation_time))-0.5;
	vec3 UV = vec3(uv,0.);
	UV = rotateZ(1.+U*2.+F.x+mat_rotation*r*PI)*UV;
	float H = smoothstep(0.35,0.45,fract(UV.y*100.)) - smoothstep(0.9,1.,fract(UV.y*100.));
	col -= vec3(H);

	return vec4(col,1.0);
}
