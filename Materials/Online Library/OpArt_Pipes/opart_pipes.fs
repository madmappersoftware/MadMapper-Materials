/*{
    "CREDIT": "1024 architecture\nadapted from shadertoy\noriginal by XORXOR",
    "TAGS": "graphic",
    "INPUTS": [ 
        { "LABEL": "speed", "NAME": "speed", "TYPE": "float", "MIN": -4.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "lookat_X", "NAME": "lookatX", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "lookat_Y", "NAME": "lookatY", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "lookat_z", "NAME": "lookatZ", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
      ],
	 "GENERATORS": [
         {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
     ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

float cylinder( vec3 p )
{
    return length( p.xy ) - 0.9;
}

vec3 opTwist( vec3 p )
{
    float a = -0.1 * animation_time - 0.02 * p.z;
    float c = cos( a );
    float s = sin( a );
    mat2 m = mat2( c, -s, s, c );
    return vec3( m * p.xy, p.z );
}

vec2 opRep( vec2 p, float size, float start, float stop )
{
    float halfSize = size * 0.5;
    vec2 c = floor( p / size );
    p = mod( p, size ) - halfSize;
    if ( ( c.x > stop ) || ( c.y > stop ) )
    {
        p += size * ( c - stop );
    }
    if ( ( c.x < start ) || ( c.y < start ) )
    {
        p += size * ( c - start );
    }
    return p;
}

float map( vec3 p )
{
    p = opTwist( p );
    p.xy = opRep( p.xy, 10.0, -7.0, 7.0 );
    float d = cylinder( p );
    return d;
}

float scene( vec3 ro, vec3 rd )
{
    float t = 0.01;
    for ( int i = 0; i < 200; i++ )
    {
        vec3 p = ro + t * rd;
        float d = map( p );
        if ( d < 0.001 )
        {
            return t;
        }
        t += d;
    }
    return -1.0;
}

vec3 calcNormal( vec3 pos )
{
    vec3 eps = vec3( 0.001, 0.0, 0.0 );
    float pd = map( pos );
    vec3 n = vec3(
            pd - map( pos - eps.xyy ),
            pd - map( pos - eps.yxy ),
            pd - map( pos - eps.yyx ) );
    return normalize( n );
}

float calcAo( vec3 pos, vec3 n )
{
    float occ = 0.0;
    for ( int i = 0; i < 5; i++ )
    {
        float hp = 0.1 + 2.0 * float( i );
        float dp = map( pos + n * hp );
        occ += ( hp - dp );
    }
    return clamp( 1.0 - 0.04 * occ, 0.0, 1.0 );
}

vec3 render( vec3 ro, vec3 rd )
{
    float d = scene( ro, rd );
    vec3 col = vec3( 0 );
    if ( d > 0.0 )
    {
        vec3 pos = ro + d * rd;
        vec3 twistPos = opTwist( pos );
        float t = -15.0 * animation_time;
        col = vec3( floor( 0.3 * mod( twistPos.z + t, 6.0 ) ) );
        vec3 nor = calcNormal( pos );
        float ao = calcAo( pos, nor );
        col *= vec3( ao );
        float fog = ( 40.0 + pos.z ) / 30.0;
        col *= fog;
    }
	return col;
}
	
	
	
vec4 materialColorForPixel( vec2 texCoord )
{
	
	vec3 eye = vec3( 0.0, 0.0, 20.0 );
    vec3 target = vec3( lookatX*10.,lookatY*10.,lookatZ*10. );
    vec3 cw = normalize( target - eye );
    vec3 cu = cross( cw, vec3( 0, 1, 0 ) );
    vec3 cv = cross( cu, cw );
    mat3 cm = mat3( cu, cv, cw );

    vec3 col = vec3( 0.0 );

    for ( int i = 0; i < 2; i++ )
    {
        vec2 off = vec2( mod( float( i ), 2.0 ), mod( float( i / 2 ), 2.0 ) ) / 2.0;
        vec2 uv = texCoord*2.0 - vec2(1);
        vec3 rd = cm * normalize( vec3( uv, 2.5 ) );

        col += render( eye, rd );

    }
    col *= 0.5;

	
	return vec4(col,1.0);
}
