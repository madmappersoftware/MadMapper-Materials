/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nadapted from Inigo Iquilez",
    "TAGS": "graphic",
    "INPUTS": [ 

		{ "Label": "Speed ", "NAME": "speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 1.0 },
		{ "Label": "Scale ", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 5.0 },
		{ "Label": "Rnd Position ", "NAME": "uRndPos", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "Label": "Color/Rnd Color ", "NAME": "uRndCol", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 2.0 },
      ],
	  	 "GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
float hash1( float n ) { return fract(sin(n)*43758.5453); }
vec2  hash2( vec2  p ) { p = vec2( dot(p,vec2(127.1,311.7)), dot(p,vec2(269.5,183.3)) ); return fract(sin(p)*43758.5453); }

vec4 voronoi( in vec2 x, float mode )
{
    vec2 n = floor( x );
    vec2 f = fract( x );

	vec3 m = vec3( 8.0 );
	float m2 = 8.0;
    for( int j=-2; j<=2; j++ )
    for( int i=-2; i<=2; i++ )
    {
        vec2 g = vec2( float(i),float(j) );
        vec2 o = pow(hash2( (n + g) ),vec2(uRndPos));

		// animate
        o = 0.5 + 0.5*sin( animation_time + 6.2831*o );
		vec2 r = g - f + o;

        // euclidean		
		vec2 d0 = vec2( sqrt(dot(r,r)), 1.0 );
        // manhattam		
		vec2 d1 = vec2( 0.71*(abs(r.x) + abs(r.y)), 1.0 );
        // triangular		
		vec2 d2 = vec2( max(abs(r.x)*0.866025+r.y*0.5,-r.y), 
				        step(0.0,0.5*abs(r.x)+0.866025*r.y)*(1.0+step(0.0,r.x)) );

		vec2 d = d0; 
		d=mix( d2, d0, fract(mode) );

        if( d.x<m.x )
        {
			m2 = m.x;
            m.x = d.x;
            m.y = hash1( dot(n+g,vec2(7.0,113.0) ) );
			m.z = d.y;
        }
		else if( d.x<m2 )
		{
			m2 = d.x;
		}
    }
    return vec4( m, m2-m.x );
}	

vec4 materialColorForPixel( vec2 texCoord )
{	
	float mode = 2.0;	
    vec2 p = texCoord.xy*vec2(1,-1);
    vec4 c = voronoi( scale*p, 2.0 );

    vec3 col = 0.5 + 0.5*sin( c.y*uRndCol*5.5 + vec3(1.0,1.0,1.0) );
    col *= sqrt( clamp( 1.0 - c.x, 0.0, 1.0 ) );
	col *= clamp( 0.5 + (1.0-c.z/2.0)*0.5, 0.0, 1.0 );
	col *= 0.4 + 0.6*sqrt(clamp( 4.0*c.w, 0.0, 1.0 ));
	
	//////// brightness + contrast
	// Apply contrast
    col = mix(vec3(0.5), col, contrast);
    // Apply brightness
    col += vec3(brightness);
	
	return vec4(col,1);
}

// Created by inigo quilez - iq/2014
// Adapted by Franz / 1024
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
