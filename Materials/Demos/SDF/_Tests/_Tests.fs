/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mm team",
    "CATEGORIES": [
        "Image Control"
    ],
    "INPUTS": [
        {
            "Label": "Back Color",
            "NAME": "backgroundColor",
            "TYPE": "color",
            "DEFAULT": [
                0.0,
                0.0,
                0.0,
                1.0
            ]
        },
        {
            "Label": "Front Color",
            "NAME": "foregroundColor",
            "TYPE": "color",
            "DEFAULT": [
                1.0,
                1.0,
                1.0,
                1.0
            ]
        },
        {
            "Label": "Scale",
            "NAME": "scale",
            "TYPE": "float",
            "MIN": 0.00001,
            "MAX": 35.0,
            "DEFAULT": 20.0
        },
        {
            "Label": "Contrast",
            "NAME": "contrast",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Saturation",
            "NAME": "saturation",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Brightness",
            "NAME": "brightness",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Exposure",
            "NAME": "exposure",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 10.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Speed X",
            "NAME": "xspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Speed Y",
            "NAME": "yspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Speed Z",
            "NAME": "zspeed",
            "TYPE": "float",
            "MIN": -4.0,
            "MAX": 4.0,
            "DEFAULT": 0.05
        },
        {
            "NAME": "NOISE3D",
            "LABEL": "3D Noise",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define"
        }
    ],
    "GENERATORS": [
        {"NAME": "xtime", "TYPE": "time_base", "PARAMS": {"speed": "xspeed"}},
        {"NAME": "ytime", "TYPE": "time_base", "PARAMS": {"speed": "yspeed"}},
        {"NAME": "ztime", "TYPE": "time_base", "PARAMS": {"speed": "zspeed"}}
    ],
    "IMPORTED": {
        "noiseLUT": {
            "PATH": "noiseLUT.png",
            "GL_TEXTURE_MIN_FILTER": "LINEAR",
            "GL_TEXTURE_MAG_FILTER": "LINEAR",
            "GL_TEXTURE_WRAP": "REPEAT",
        }
    }
}*/

#define NOISE_TEXTURE_BASED

#define SDF_ANTIALIASING_HIGH

#define SDF_ANTIALIASING_MEDIUM_DIST_SCALE 1850.0

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

#define SDF_DIST_SMOOTH 200.0


float arc( vec2 p, float radius, float angle, float width )
{
    width   *= 0.5;
    radius  -= width;
    //angle   = radians( angle ) * 0.5;
    vec2 n  = vec2(cos( angle ), sin( angle ));
    return max( -( abs( p ).x * n.x + p.y * n.y ), abs( circle( p, radius ) ) - width );
}


vec4 glow( vec4 a, vec4 b, float dist, float width ) 
{
    float s = max( dist, -dist );
    return mix( a, b, 1.0 - smoothstep( 0, width, s ) );
}

float cross( vec2 p, vec2 size )
{
    return blend( rectangle( p, size.xy ), rectangle( p, size.yx ) );
}

//in vec3 vNoises[32];
in float vTest;

vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p = texCoord;
   // vec2 Pi = floor(p);
    //vec2 Pf = p - Pi;


    float dist = 1;
        //box( rotate( translate( p, vec2( 0.5 ) + 0.2 * vec2( dvnoise( vec2( ztime ) ) ) ), xtime ), vec2( 0.05 ) );
        //box( rotate( repeat( translate( p, vec2( 0.5 ) ), vec2( 0.1, 0.1 ) ), xtime ), vec2( 0.025 ) );

   /* dist = arc( translate( p, vec2( 0.5 ) ), 0.1, radians(20.0), 0.05 );
    
    float angle = 350.0;
    //angle -= PI;
    angle = radians( angle );
    vec2 n =vec2(cos( angle ), sin( angle ));
    float width = 0.06;
    float radius = 0.2;
    float atan1 = atan( -p.y, p.x );
    float atan2 = atan( -n.y, n.x );// - atan1;
    */

    //dist = fract( 0.5 * ( angle ) );

#if 0
    float cellSize = 0.015 * scale;
    p = repeat( p, vec2( cellSize ) );

    vec2 pp = vec2( 0, 0 ) + texCoord / cellSize;
    vec2 Pi = floor( pp );
    vec2 Pf = pp - Pi;

    float signedNoise = noise( Pi );
    float unsignedNoise = signedNoise * 0.5 + 0.5;
    p = rotate( p, signedNoise + ztime * sign( signedNoise ) );

    float scl = cellSize * unsignedNoise;
    dist = blend( rectangle( p, scl * vec2( 0.333, 0.075 ) ), rectangle( p, scl * vec2( 0.075, 0.333 ) ) );

#elif 0
    float cellSize = 0.015 * scale;
    p = translate( texCoord, vec2( 0.5 ) );    
    p = repeatRadial( p, 0.3, ytime );
    p = translate( p, vec2( 0.2, 0.0 ) );    
    dist = cross( p, vec2( 0.333, 10.075 ) * cellSize );

    p = translate( texCoord, vec2( 0.5 ) );    
    p = repeatRadial( p, 0.3, xtime );
    p = translate( p, vec2( 0.1, 0.0 ) );    
    dist = subtract( dist, cross( p, vec2( 1.333, 0.075 ) * cellSize ) );

    float circleGridCellSize = 0.125;
    p = translate( texCoord, vec2( 0.5 ) );  
    p = rotate( p, ztime );
    p = translate( p, vec2( circleGridCellSize * 0.5 ) );  
    p = repeat( p, vec2( circleGridCellSize ) );
    dist = subtract( dist, circle( p, circleGridCellSize * 0.25 ) );   

    //p = repeat( p, vec2( cellSize ) );

#elif 0

    dist = circle( p - vec2( 0.5 ), 0.2 );
    dist = subtract( dist, circle( p - vec2( 0.5 ), 0.1 ) );

#elif 0
    // grid
    float cellSize = 0.015 * scale;
    p = repeat( p, vec2( cellSize ) );

    // cross
    dist = blend(
            rectangle( p, cellSize * vec2( 0.333, 0.075 ) ), 
            rectangle( p, cellSize * vec2( 0.075, 0.333 ) ) 
            );

    // circle
   // dist = circle( p, cellSize * 0.25 );

#elif 0
    
    // rect radial grid
    p = translate( p, vec2( 0.5, 0.5 ) );
    p = repeatRadial( p, 0.1 );
    p = translate( p, vec2( 0.1, 0.0 ) );
    p = repeat( p, vec2( 0.04, 0.0 ) );
    
    dist = rectangle( p, vec2( 0.015, 0.01 - 0.035 * length( texCoord - vec2(0.5) ) ) );

#elif 0
    // graphic circle
    p = translate( p, vec2( 0.5, 0.5 ) );
    p = repeat( p, vec2( 0.04, 0.02 ) );
    
    dist = rectangle( p, vec2( 0.015, 0.01 - 0.035 * length( texCoord - vec2(0.5) ) ) );

#elif 0
    // cellId usage example
    vec2 cellId;
#if 0
    p = repeat( p, vec2( 0.1 ), cellId );
#else
    p = translate( p, vec2( 0.5 ) );
    p = rotate( p, xtime );
    p = repeatRadial( p, ( PI * 2.0 ) / 35.0, cellId );
    p = translate( p, vec2( 0.25, 0.0 ) );
#endif
    
    float size = 0.02 * vnoise( cellId * 0.5 );
    dist = rectangle( p, size );

#elif 0
// spiral
    p = translate( p, vec2( 0.5 ) );
    dist = 0.0;
    for( float i = 0.7; i >= 0.01; i -= 0.035 ) {
        p = rotate( p, xtime * 0.01 );
        dist = subtract( dist, rectangle( p, i ) );
        dist = blend( dist, rectangle( p, i - 0.015 ) );
    }

#elif 1
    dist = circle( p, vec2( 0.5 ), 0.2 );
    dist = blend( dist, rectangle( p, vec2( 0.85, 0.2 ), 0.1 ) );
    dist = subtract( dist, rectangle( p, vec2( 0.85, 0.2 ), vec2( 0.05, 0.01 ) ) );
    dist = subtract( dist, rectangle( p, vec2( 0.85, 0.2 ), vec2( 0.01, 0.05 ) ) );
    dist = blend( dist, triangle( rotate( p-vec2( 0.1, 0.1 ), xtime ), 0.04 ) );

    float lines = blend( blend( 
                line( p, vec2( 0, cos(ytime) * 0.5 + 0.5 ), vec2( 1, cos(ytime) * 0.5 + 0.5 ), 0.1 ),
                line( p, vec2( 0, cos(ytime + 0.333 ) * 0.5 + 0.5 ), vec2( 1, cos(ytime + 0.333 ) * 0.5 + 0.5 ), 0.1 ) ),
                line( p, vec2( 0, cos(ytime + 0.666 ) * 0.5 + 0.5 ), vec2( 1, cos(ytime + 0.666 ) * 0.5 + 0.5 ), 0.1 ) );
    dist = intersect( dist, lines );

   // dist = lines;

#elif 0
    
    p = translate( p, vec2( 0.5 ) );

    for( int i = 0; i < 16; ++i ) {
        vec2 pp = rotate( p, PI * cos( xtime + 15.0 * vnoise( vec2(ytime+i-67.891) ) ) );
        dist = blend( dist, arc( pp, 
            0.02 + float(i) * 0.02,                         // radius
            0.1 + vnoise( vec2( ytime+i + 12.34 ) ) * 2.0,    // angle
            vnoise( vec2(ytime+i) ) * 0.03 + 0.005 ) );       // width
    }
    //dist = cos( dist * cos( xtime * noise( p * 0.0001 ) ) * 600.0 );
#elif 0
    
    p = translate( p, vec2( 0.5 ) );

    for( int i = 0; i < 16; ++i ) {
        vec2 pp = rotate( p, PI * cos( xtime + 15.0 * vNoises[i].x ) );
        dist = blend( dist, arc( pp, 
            0.02 + float(i) * 0.02,                         // radius
            0.1 + vNoises[i].y * 2.0,    // angle
            vNoises[i].z * 0.03 + 0.005 ) );       // width
    }
    //dist = cos( dist * cos( xtime * noise( p * 0.0001 ) ) * 600.0 );

#elif 1
    
    p = translate( p, vec2( 0.5 ) );
    dist = circle( p, vTest );

#endif


    //dist = subtract( circle( p, 0.35 * cellSize * vnoise( Pi + ztime + 123.456 ) ), dist );
    //dist = merge( circle( p, 0.15 * cellSize * nn ), dist );

        //abs( circle( p, radius ) ) - width );
    //dist = fract(-0.5*(dist/PI));

    //dist = line( p, vec2( -0.2 ), vec2( 0.2 ), 0.01 );


    //dist *= 350.0;

    // background
    float n = 0.5;//length( bkg ) / 25.0f;// vnoise( bkg );
    vec4 col = vec4( vec3( n ), 1.0 );
    // fill
    col = fill( col, vec4( 1 ), dist );
    // stroke
    col = stroke( col, vec4( 0.0 ), dist, 0.0025 );

    //col = glow( col, vec4( 1.0 ), dist, 0.05 );
    //col = vec4( cos( dist * 450.0 ) );

    //col = vec4( vec3( dist ), 1.0 );
    col.a = 1.0;
    //col.rg = bkg * 0.1;
    return saturate( col );

    /*float c = circle( translate(p, vec2( 0.5, 0.5 )), 0.25 );
    c = box( repeat( rotate( translate(p, vec2( 0.5, 0.5 ) ), xtime ), vec2( 0.1 ) ), vec2( 0.025 ) );

    vec2 start = vec2( 0.1 );
    vec2 end = vec2( 0.9 );
    c = line( p, start, end, vnoise( p * 30.0 ) * 0.15 );

    c = circle( 
        repeat( 
            translate( p, vec2( 0.5, 0.5 ) ),
            vec2( 0.1, 0.1 ) ), 
        vnoise( p * 70.0 ) * 0.015 );


    float dist = c * 350.0;// smoothstep(0.1,0.14,c);
    
    vec4 col = vec4(0.5, 0.5, 0.5, 1.0);// * (1.0 - length(c - p)/iResolution.x);
    // grid
    //col *= clamp(min(mod(p.y, 10.0), mod(p.x, 10.0)), 0.9, 1.0);
    
    // shape fill
    col = fill( col, vec4( 1.0 ), dist );
    // shape outline
    col = stroke( col, vec4( 0.0 ), dist, 3.5 );*/

    //return col;//clamp( col, 0.0, 1.0 );
}
