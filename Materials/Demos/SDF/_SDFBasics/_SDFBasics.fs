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
            "DEFAULT": [ 0.5, 0.5, 0.5, 1.0 ]
        },
        {
            "NAME": "spectrum_16",
            "TYPE": "audioFFT",
            "SIZE": 16,
            "ATTACK": 0.0,
            "DECAY": 0.0,
            "RELEASE": 0.5
        },
        {
            "Label": "Stroke",
            "NAME": "strokeColor",
            "TYPE": "color",
            "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ]
        },
        {
            "Label": "Fill",
            "NAME": "fillColor",
            "TYPE": "color",
            "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ]
        },
        {
            "Label": "Speed", "NAME": "speed", "TYPE": "float",
            "MIN": -4.0, "MAX": 4.0, "DEFAULT": 0.05
        },
        {
            "Label": "Scale", "NAME": "scale", "TYPE": "float",
            "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5
        },
        {
            "Label": "Stroke Width", "NAME": "strokeWidth", "TYPE": "float",
            "MIN": 0.0, "MAX": 0.02, "DEFAULT": 0.0025
        },
        {
            "Label": "test", "NAME": "test", "TYPE": "float",
            "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0
        }
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "speed"}}
    ],
    "IMPORTED": {
        "noiseLUT": { "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT", }
    }
}*/

#define SDF_ANTIALIASING_HIGH

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

//in float vTest[4];

float morph( vec2 p, float d1, float d2, float t )
{

    return mix( d1, d2, smoothstep( 0.25, 0.75, t ) );
    //  f(x) =  |SDT1|  /  (  |SDT1| + |SDT2| ) 
    //return 1.0 / mix( 1.0 / d1, 1.0 / d2, t );
    
    //float sub   = max( d1, -d2 );
    //float inters = max( d1, d2 );
    //return mix( min( max( d1, d2 ), max( d1, -d2 ) ), inters, t );
}



float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

mat3 setCamera( in vec3 ro, in vec3 ta, float cr )
{
    vec3 cw = normalize(ta-ro);
    vec3 cp = vec3(sin(cr), cos(cr),0.0);
    vec3 cu = normalize( cross(cw,cp) );
    vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}


vec2 map( in vec3 pos )
{
    vec2 res = vec2( sdBox( pos-vec3( 0.0,0.0,0.0), vec3(0.25) ), 3.0 );
    return res;
}

vec2 castRay( in vec3 ro, in vec3 rd )
{
    float tmin      = 1.0;
    float tmax      = 5.0;
    float precis    = 0.02;
    float t         = tmin;
    float m         = -1.0;
    for( int i=0; i<15; i++ )
    {
        vec2 res = map( ro+rd*t );
        if( res.x<precis || t>tmax ) break;
        t += res.x;
        m = res.y;
    }

    if( t>tmax ) m = -1.0;
    return vec2( t, m );
}


vec3 render( in vec3 ro, in vec3 rd )
{ 
    vec3 col = vec3(0.7, 0.9, 1.0) +rd.y*0.8;
    vec2 res = castRay( ro, rd );
    return vec3( res.y ) * col * 0.5;
}

/**
 useful with domain repetition.
 shapes are only rendered between min and max limits
 using abs makes this symmetric (left/right and front/ back)
 */
vec2 absClamp( vec2 p, vec2 a , vec2 b )
{
    return clamp( abs( p ), a, b );
}


float udHexPrismin( vec2 p, float h )
{
    vec2 q = abs(p);
    return max(q.x+q.y*0.57735,q.y*1.1547)-h;
}


float udHexPrism( vec2 p, float h ) {
    vec2 q = abs(p);
    //return max(q.x*0.866025 + q.y*0.5, q.y)-h;
    // can also be expressed as dot prod.
    return max(dot(q, vec2(0.866025, 0.5)), q.y)-h;
}


vec4 materialColorForPixel( vec2 texCoord )
{
    vec2 p      = texCoord - vec2( 0.5 );
    
    vec2 cellId;
    float cell  = 0.125;
    //evenOdd *= step( 0.9, mod( floor( p.y / cell ), 2.0 ) );
    vec2 offsets = vec2( smoothstep( -1.0, 0, cos(mat_time*0.1) ), 
                        -smoothstep( 0, 1.0, cos(mat_time*0.1) ) ) * cell * 10;

    p = absClamp( p, vec2( -0.25 ), vec2( 0.25 ) );
    //p           = repeat( p, vec2( cell ), offsets, vec2( 2 ), cellId );
    //p = abs(p);
    
    //p         = rotate( p, 0.2 );

   /* // camera   
    vec3 ro = vec3( -0.5+3.5*cos(0.1*mat_time ), 1.0, 0.5 + 3.5*sin(0.1*mat_time ) );
    vec3 ta = vec3( -0.5, -0.4, 0.5 );
    
    // camera-to-world transformation
    mat3 ca = setCamera( ro, vec3(0), 0.0 );
    
    // ray direction
    vec3 rd = ca * normalize( vec3( (texCoord - vec2( 0.5 )).xy,2.0) );


    // render   
    vec3 col = render( ro, rd );
*/
    float evenOdd = cos(mat_time);// step( 0.5, mod( cellId.x + cell, 2.0 ) );
    float dist = circle( p, cell * 2.045 );// mix( circle( p, cell * 0.25 ), rectangle( p, vec2( cell * 0.25 ) ), evenOdd );
    //dist = subtract( sin( dist * 200.0 ), dist );
    vec4 color = fill( backgroundColor, vec4( fillColor ), dist );
    //color.xy = cellId * 0.1;

    /*float shape0 = subtract( rectangle( p, 0.1 * cell ), rectangle( p, 0.05 * cell ) );
    float shape1 = triangle( p, 0.1 * cell );//subtract( triangle( p, 0.1 ), triangle( p, 0.05 ) );
  //  float dist   = morph( p, shape0, shape1, cos( mat_time ) * 0.5 + 0.5 ); 
    
    float dist = shape0;

    //p = mod( uv * 3.0, p );

    float size = 0.025;*/
    //dist = circle( p, size );


/*  vec2 cellId;
    p = repeat( p, vec2( 0.1 ), cellId );

    float scl = 0.2;
    float dist  = rectangle( p, scl * 0.2 );
    scl = 0.2 * vnoise( cellId );
    dist = subtract( dist, rectangle( p, scl * vec2( 0.05, 0.01 ) ) );
    dist = subtract( dist, rectangle( p, scl * vec2( 0.01, 0.05 ) ) );
*/
    /*
    float dist = 1.0;

    float circle0 = circle( p, vec2( abs(cos(mat_time)) ), 0.1 );
    circle0 = smoothBlend( circle0, circle( p, vec2( 0.8 ), 0.1 ), 0.6 );
    circle0 = smoothBlend( circle0, circle( p, vec2( 0.8, abs(cos(mat_time)) ), 0.1 ), 0.5 );

    float circle1 = circle( p, vec2( abs( sin( mat_time ) ), 0.8 ), 0.2 );
    circle1 = smoothBlend( circle1, circle( p, vec2( 0.8 ), 0.2 ), 0.8 );

    dist = intersect( circle0, circle1 );
    //dist = circle0;

    // fill
    vec4 color = fill( backgroundColor, vec4( fillColor ), circle0 );
    color = fill( color, vec4(1,1,0,1), circle1 );
    color = fill( color, strokeColor, dist );
    // stroke
    color = stroke( color, vec4(1), dist, strokeWidth );*/
    /*color += glow( strokeColor * 0.5, dist, strokeWidth * 500.0 * size );
    color += glow( strokeColor * 0.5, dist, strokeWidth * 1400.0 * size );
    color += glow( vec4(1), dist, strokeWidth * 300.0 * size );
    color += glow( vec4(0.5), dist, strokeWidth * 260.0 * size );
    color += glow( vec4(0.5), dist, strokeWidth * 1260.0 * size );*/

    color.a = 1.0;

    //color = vec4( c, 1.0 );

    //return vec4( uv, 0.0, 1.0 );
    return saturate( color );
}

/* // Particles with GLOW

    float size = 0.025;
    dist = circle( p, size );
    // fill
    vec4 color = fill( backgroundColor, vec4( fillColor ), dist );
    // stroke
    //color = stroke( color, strokeColor, dist, strokeWidth );
    color += glow( strokeColor * 0.5, dist, strokeWidth * 500.0 * size );
    color += glow( strokeColor * 0.5, dist, strokeWidth * 1400.0 * size );
    color += glow( vec4(1), dist, strokeWidth * 300.0 * size );
    color += glow( vec4(0.5), dist, strokeWidth * 260.0 * size );
    color += glow( vec4(0.5), dist, strokeWidth * 1260.0 * size );
*/

/* // Rounded Rectangle
// https://www.shadertoy.com/view/4llXD7
float sdRoundBox( in vec2 p, in vec2 b, in float r ) 
{
    vec2 q = abs(p) - b;
    vec2 m = vec2( min(q.x,q.y), max(q.x,q.y) );
    float d = (m.x > 0.0) ? length(q) : m.y; 
    return d - r;
}

*/

/* // Ellipse
// https://www.shadertoy.com/view/4sS3zz
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Analytical distance to an 2D ellipse, which is more complicated than it seems. It ends up being
// a quartic equation, which can be resolved through a cubic, then a quadratic. Some steps through the
// derivation can be found in this article: 
//
// http://iquilezles.org/www/articles/ellipsedist/ellipsedist.htm
//

float sdEllipse( vec2 p, in vec2 ab )
{
    p = abs( p ); if( p.x > p.y ){ p=p.yx; ab=ab.yx; }
    
    float l = ab.y*ab.y - ab.x*ab.x;
    
    float m = ab.x*p.x/l; 
    float n = ab.y*p.y/l; 
    float m2 = m*m;
    float n2 = n*n;
    
    float c = (m2 + n2 - 1.0)/3.0; 
    float c3 = c*c*c;

    float q = c3 + m2*n2*2.0;
    float d = c3 + m2*n2;
    float g = m + m*n2;

    float co;

    if( d<0.0 )
    {
        float p = acos(q/c3)/3.0;
        float s = cos(p);
        float t = sin(p)*sqrt(3.0);
        float rx = sqrt( -c*(s + t + 2.0) + m2 );
        float ry = sqrt( -c*(s - t + 2.0) + m2 );
        co = ( ry + sign(l)*rx + abs(g)/(rx*ry) - m)/2.0;
    }
    else
    {
        float h = 2.0*m*n*sqrt( d );
        float s = sign(q+h)*pow( abs(q+h), 1.0/3.0 );
        float u = sign(q-h)*pow( abs(q-h), 1.0/3.0 );
        float rx = -s - u - c*4.0 + 2.0*m2;
        float ry = (s - u)*sqrt(3.0);
        float rm = sqrt( rx*rx + ry*ry );
        float p = ry/sqrt(rm-rx);
        co = (p + 2.0*g/rm - m)/2.0;
    }

    float si = sqrt( 1.0 - co*co );
 
    vec2 r = vec2( ab.x*co, ab.y*si );
    
    return length(r - p ) * sign(p.y-r.y);
}
*/

/* // Ellipse - Distance Estimation
// https://www.shadertoy.com/view/MdfGWn
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// An example on how to compute a distance estimation for an ellipse (which provides
// constant thickness to its boundary). This is achieved by dividing the implicit 
// description by the modulo of its gradient. The same process can be applied to any
// shape defined by an implicity formula (ellipses, metaballs, fractals, mandelbulbs).
//
// top    left : f(x,y)
// top    right: f(x,y) divided by analytical gradient
// bottom left : f(x,y) divided by numerical GPU gradient
// bottom right: f(x,y) divided by numerical gradient
//
// More info here:
//
// http://www.iquilezles.org/www/articles/distance/distance.htm

float a = 1.0;
float b = 3.0;
float r = 0.9 + 0.1*sin(3.1415927*TIME);

float e = 2.0/iResolution.y;

// f(x,y) (top left)
float ellipse1(vec2 p)
{
    float f = abs(length( p*vec2(a,b) )-r);
    return f;
}

// f(x,y) divided by analytical gradient (top right)
float ellipse2(vec2 p)
{
    float f = length( p*vec2(a,b) );
    return abs(f-r)*f/(length(p*vec2(a*a,b*b)));
}

// f(x,y) divided by numerical GPU gradient (bottom left)
float ellipse3(vec2 p)
{
    float f = ellipse1(p);
    float g = length( vec2(dFdx(f),dFdy(f))/e );
    return f/g;
}

// f(x,y) divided by numerical gradient (bottom right)
float ellipse4(vec2 p)
{
    float f = ellipse1(p);
    float g = length( vec2(ellipse1(p+vec2(e,0.0))-ellipse1(p-vec2(e,0.0)),
                           ellipse1(p+vec2(0.0,e))-ellipse1(p-vec2(0.0,e))) )/(2.0*e);
    return f/ g;
}
*/

/* // COLOR PALETTES
// https://www.shadertoy.com/view/ll2GD3
// Created by inigo quilez - iq/2015
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


// A simple way to create color variation in a cheap way (yes, trigonometrics ARE cheap
// in the GPU, don't try to be smart and use a triangle wave instead).

// See http://iquilezles.org/www/articles/palettes/palettes.htm for more information


vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = fragCoord.xy / iResolution.xy;
    
    // animate
    p.x += 0.01*TIME;
    
    // compute colors
    vec3                col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67) );
    if( p.y>(1.0/7.0) ) col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.10,0.20) );
    if( p.y>(2.0/7.0) ) col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.3,0.20,0.20) );
    if( p.y>(3.0/7.0) ) col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,0.5),vec3(0.8,0.90,0.30) );
    if( p.y>(4.0/7.0) ) col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,0.7,0.4),vec3(0.0,0.15,0.20) );
    if( p.y>(5.0/7.0) ) col = pal( p.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(2.0,1.0,0.0),vec3(0.5,0.20,0.25) );
    if( p.y>(6.0/7.0) ) col = pal( p.x, vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.0,1.0,1.0),vec3(0.0,0.25,0.25) );
    

    // band
    float f = fract(p.y*7.0);
    // borders
    col *= smoothstep( 0.49, 0.47, abs(f-0.5) );
    // shadowing
    col *= 0.5 + 0.5*sqrt(4.0*f*(1.0-f));
    // dithering
    col += (1.0/255.0)*texture2D( iChannel0, fragCoord.xy/iChannelResolution[0].xy ).xyz;

    fragColor = vec4( col, 1.0 );
}
*/

/* // ANTIALIASING
// https://www.shadertoy.com/view/4ssSRl
// Created by inigo quilez - iq/2014
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// distance to a line (can't get simpler than this)
float line( in vec2 a, in vec2 b, in vec2 p )
{
    vec2 pa = p - a;
    vec2 ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = (-iResolution.xy + 2.0*fragCoord.xy) / iResolution.yy;
    vec2 q = p;
    
    vec2 c = vec2(0.0);
    if( iMouse.z>0.0 ) c=(-iResolution.xy + 2.0*iMouse.xy) / iResolution.yy;
    
    // background   
    vec3 col = vec3(0.5,0.85,0.9)*(1.0-0.2*length(p));
    if( q.x>c.x && q.y>c.y ) col = pow(col,vec3(2.2));

    // zoom in and out  
    p *= 1.0 + 0.2*sin(TIME*0.4);
    
    
    // compute distance to a set of lines
    float d = 1e20; 
    for( int i=0; i<7; i++ )
    {
        float anA = 6.2831*float(i+0)/7.0 + 0.15*TIME;
        float anB = 6.2831*float(i+3)/7.0 + 0.20*TIME;
        vec2 pA = 0.95*vec2( cos(anA), sin(anA) );      
        vec2 pB = 0.95*vec2( cos(anB), sin(anB) );      
        float h = line( pA, pB, p );
        d = min( d, h );
    }

    // lines/start, left side of screen : not filtered
    if( q.x<c.x )
    {
        if( d<0.12 ) col = vec3(0.0,0.0,0.0); // black 
        if( d<0.04 ) col = vec3(1.0,0.6,0.0); // orange
    }
    // lines/start, right side of the screen: filtered
    else
    {
        float w = 0.5*fwidth(d); 
        w *= 1.5; // extra blur
        
        if( q.y<c.y )
        {
        col = mix( vec3(0.0,0.0,0.0), col, smoothstep(-w,w,d-0.12) ); // black
        col = mix( vec3(1.0,0.6,0.0), col, smoothstep(-w,w,d-0.04) ); // orange
        }
        else
        {
        col = mix( pow(vec3(0.0,0.0,0.0),vec3(2.2)), col, smoothstep(-w,w,d-0.12) ); // black
        col = mix( pow(vec3(1.0,0.6,0.0),vec3(2.2)), col, smoothstep(-w,w,d-0.04) ); // orange
        }
    }
    

    if( q.x>c.x && q.y>c.y )
        col = pow( col, vec3(1.0/2.2) );
    
    // draw left/right separating line
    col = mix( vec3(0.0), col, smoothstep(0.007,0.008,abs(q.x-c.x)) );
    col = mix( col, vec3(0.0), (1.0-smoothstep(0.007,0.008,abs(q.y-c.y)))*step(0.0,q.x-c.x) );
    
    
    fragColor = vec4( col, 1.0 );
}
*/

/* // SMOOTH HSV
// https://www.shadertoy.com/view/MsS3Wc
// Created by inigo quilez - iq/2014
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


// Converting from HSV to RGB leads to C1 discontinuities, for the RGB components
// are driven by picewise linear segments. Using a cubic smoother (smoothstep) makes 
// the color transitions in RGB C1 continuous when linearly interpolating the hue H.

// C2 continuity can be achieved as well by replacing smoothstep with a quintic
// polynomial. Of course all these cubic, quintic and trigonometric variations break 
// the standard (http://en.wikipedia.org/wiki/HSL_and_HSV), but they look better.


// Official HSV to RGB conversion 
vec3 hsv2rgb( in vec3 c )
{
    vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );

    return c.z * mix( vec3(1.0), rgb, c.y);
}

// Smooth HSV to RGB conversion 
vec3 hsv2rgb_smooth( in vec3 c )
{
    vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );

    rgb = rgb*rgb*(3.0-2.0*rgb); // cubic smoothing 

    return c.z * mix( vec3(1.0), rgb, c.y);
}
*/

/* // TUNNEL
    // angle of each pixel to the center of the screen
    vec2 center = texCoord - vec2( 0.5 );
    float a = atan( center.y, center.x );
    
    // modified distance metric (http://en.wikipedia.org/wiki/Minkowski_distance)
    float r = pow( pow(center.x*center.x,4.0) + pow(center.y*center.y,4.0), 1.0/8.0 );
    // float r = max( abs(p.x), abs(p.y) );
    
    // index texture by (animated inverse) radious and angle
    vec2 uv = vec2( 0.3/r + 0.2*mat_time, a/3.1415927 );
*/

/* // WIGGLE
    vec2 p      = texCoord;
    p           = translate( p, vec2( 0.5 ) );

    float cellSize = 0.05;
    p = repeat( p, vec2( cellSize ) );
  
    float shape0 = subtract( rectangle( p, cellSize * 0.3 ), rectangle( p, cellSize * 0.15 ) );
    //float shape1 = subtract( triangle( p, 0.2 ), triangle( p, 0.15 ) );
    float dist   = shape0;//morph( shape0, shape1, cos( mat_time ) * 0.5 + 0.5 ); 
    


    float effect = 0.005;
    float effectScale = 40.0;
    
    dist = dist - effect + effect * sin( texCoord.x * effectScale + cos( mat_time ) );
    dist = dist - effect + effect * cos( texCoord.y * effectScale + sin( mat_time ) );
*/

// Binary Gates
// https://www.shadertoy.com/view/Xdj3Rh
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// In electronics/logic, there are 16 possible gates that you can build with one output and 
// two inputs. Some of them have proper names, such as AND, OR, XOR, NAND or NOR (gates 1, 
// 7, 6, 15 and 8 respectivelly), but others don't.

// When these gates are made not digital/discrete but continuous in the interval 0..1, two
// dimensional gradients of values appear. Some of them have also proper names in image
// manipulation software, such as "screen" (which is a gate OR), "multiply" (which is
// gate AND) or "exclusion" (which is gate XOR).

// The behavior of the gates also matches that of the 16 posible bilinear interpolations
// of binary values in the corners of a quad.

// float bilin( float u, float v, float a, float c, float b, float d )
// {
//     return mix( mix(a,b,u),
//                 mix(c,d,u), v );
// }

// void mainImage( out vec4 fragColor, in vec2 fragCoord )
// {
//     vec2 uv = fragCoord.xy / iResolution.xy;
    
//     float c = 0.0;
    
//     float r = floor(uv.y*4.0 )*4.0 + floor(uv.x*4.0 );
    
//     float a = fract(uv.x*4.0);
//     float b = fract(uv.y*4.0);
// #if 1
//                       /* 0011 = A         */
//                       /* 0101 = B         */
//                       /* ----   --------- */
//          if( r< 0.5 ) /* 0000 = RESET     */ c = 0.0;
//     else if( r< 1.5 ) /* 0001 = A AND B   */ c = a*b;
//     else if( r< 2.5 ) /* 0010 = A AND !B  */ c = a - a*b;
//     else if( r< 3.5 ) /* 0011 = A         */ c = a;
//     else if( r< 4.5 ) /* 0100 = !A AND B  */ c = b - a*b;
//     else if( r< 5.5 ) /* 0101 = B         */ c = b;
//     else if( r< 6.5 ) /* 0110 = A XOR B   */ c = a + b - 2.0*a*b;
//     else if( r< 7.5 ) /* 0111 = A OR B    */ c = a + b - a*b;
//     else if( r< 8.5 ) /* 1000 = A NOR B   */ c = 1.0 - a - b + a*b;
//     else if( r< 9.5 ) /* 1001 = A XNOR B  */ c = 1.0 - b - a + 2.0*a*b;
//     else if( r<10.5 ) /* 1010 = !B        */ c = 1.0 - b;
//     else if( r<11.5 ) /* 1011 = !A NAND B */ c = 1.0 - b + a*b;
//     else if( r<12.5 ) /* 1100 = !A        */ c = 1.0 - a;
//     else if( r<13.5 ) /* 1101 = A NAND !B */ c = 1.0 - a + a*b;
//     else if( r<14.5 ) /* 1110 = A NAND B  */ c = 1.0 - a*b;
//     else if( r<15.5 ) /* 1111 = SET       */ c = 1.0;
// #else
//                       /* 0011 = A         */
//                       /* 0101 = B         */
//                       /* ----   --------- */
//          if( r< 0.5 ) /* 0000 = RESET     */ c = bilin( a, b, 0.,0.,0.,0. );
//     else if( r< 1.5 ) /* 0001 = A AND B   */ c = bilin( a, b, 0.,0.,0.,1. );
//     else if( r< 2.5 ) /* 0010 = A AND !B  */ c = bilin( a, b, 0.,0.,1.,0. );
//     else if( r< 3.5 ) /* 0011 = A         */ c = bilin( a, b, 0.,0.,1.,1. );
//     else if( r< 4.5 ) /* 0100 = !A AND B  */ c = bilin( a, b, 0.,1.,0.,0. );
//     else if( r< 5.5 ) /* 0101 = B         */ c = bilin( a, b, 0.,1.,0.,1. );
//     else if( r< 6.5 ) /* 0110 = A XOR B   */ c = bilin( a, b, 0.,1.,1.,0. );
//     else if( r< 7.5 ) /* 0111 = A OR B    */ c = bilin( a, b, 0.,1.,1.,1. );
//     else if( r< 8.5 ) /* 1000 = A NOR B   */ c = bilin( a, b, 1.,0.,0.,0. );
//     else if( r< 9.5 ) /* 1001 = A XNOR B  */ c = bilin( a, b, 1.,0.,0.,1. );
//     else if( r<10.5 ) /* 1010 = !B        */ c = bilin( a, b, 1.,0.,1.,0. );
//     else if( r<11.5 ) /* 1011 = !A NAND B */ c = bilin( a, b, 1.,0.,1.,1. );
//     else if( r<12.5 ) /* 1100 = !A        */ c = bilin( a, b, 1.,1.,0.,0. );
//     else if( r<13.5 ) /* 1101 = A NAND !B */ c = bilin( a, b, 1.,1.,0.,1. );
//     else if( r<14.5 ) /* 1110 = A NAND B  */ c = bilin( a, b, 1.,1.,1.,0. );
//     else if( r<15.5 ) /* 1111 = SET       */ c = bilin( a, b, 1.,1.,1.,1. );
// #endif      
        
//     vec3 col = vec3(c);
        
//     col = mix( col, vec3(0.9,0.5,0.3), smoothstep( 0.490, 0.495, abs(a-0.5) ) );
//     col = mix( col, vec3(0.9,0.5,0.3), smoothstep( 0.485, 0.490, abs(b-0.5) ) );

//     fragColor = vec4(col,1.0);
// }

/* // https://www.shadertoy.com/view/XsG3WV
float heart(vec2 i) {
    // From: https://www.shadertoy.com/view/ldVGzt by coyote & Fabrice
    i.y += .034;
    i *= 1.1;
    return sqrt(dot(i, i) - abs(i.x)*i.y);
}

float diamond(vec2 i) {
    i = abs(i);
    return (i.x+i.y);
}

float square(vec2 i) {
    i = abs(i);
    return max(i.x,i.y);
}

float circle(vec2 i) {
    return length(i);
}

float circle2(vec2 i) {
    
    return length(vec2(abs(length(i)-.5),max(0.,abs(atan(i.x,i.y))/2.-sin(TIME)-.7)))+.35;
   //return length(vec2(abs(length(i)-.5),1.3-2.*atan(i.x,i.y)))+.35;
}

float honeycomb(vec2 i) {
    i.x*=.866;
    i = abs(i);
    return max(i.x+i.y*.5,i.y);
}

float segment(vec2 uv)
{
    uv = abs(uv);
    float f = max(0.45+uv.x,0.225+uv.y+uv.x);
    return f;
}

float m(float a, float b)
{
    return min(a,b);
    //return 1./(1./a+1./b);
    //return length(vec2(a,b));
}

float sevenSegment(vec2 uv,int num)
{
    float seg= 5.0;
    seg = (num!=-1 && num!=1 && num!=4                    ?m(segment(uv.yx+vec2(-0.450, 0.000)),seg):seg);
    seg = (num!=-1 && num!=1 && num!=2 && num!=3 && num!=7?m(segment(uv.xy+vec2( 0.225,-0.225)),seg):seg);
    seg = (num!=-1 && num!=5 && num!=6                    ?m(segment(uv.xy+vec2(-0.225,-0.225)),seg):seg);
    seg = (num!=-1 && num!=0 && num!=1 && num!=7          ?m(segment(uv.yx+vec2( 0.000, 0.000)),seg):seg);
    seg = (num==0 || num==2 || num==6 || num==8           ?m(segment(uv.xy+vec2( 0.225, 0.225)),seg):seg);
    seg = (num!=-1 && num!=2                              ?m(segment(uv.xy+vec2(-0.225, 0.225)),seg):seg);
    seg = (num!=-1 && num!=1 && num!=4 && num!=7          ?m(segment(uv.yx+vec2( 0.450, 0.000)),seg):seg);
    
    return seg;
}

vec2 rotate(vec2 i,float a) {
   return i *mat2(cos(a), -sin(a),
                  sin(a), cos(a));
}

float getShape(int nr, vec2 uv) {
    
    //return circle2(uv);
        
    bool outline = false;
    if (nr<10)
        return sevenSegment(uv,nr);
    else {
        outline = (nr>=15);
        nr = int(mod(float(nr),5.));
    }

    float x = 0.0;
    if (nr==0) x = heart    (uv); else
    if (nr==1) x = diamond  (uv); else
    if (nr==2) x = square   (uv); else
    if (nr==3) x = circle   (uv); else
               x = honeycomb(uv);
        
    return outline ? 0.45+abs(x-.5) : x;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord-0.5*iResolution.xy) / iResolution.y *1.1;
    
    //uv = rotate(uv,TIME);
    
    float gt = TIME;
    int shapeNr1 = int(mod(gt    ,20.0));
    int shapeNr2 = int(mod(gt+1.0,20.0));
    float mixPerc = mod(gt, 1.0);
    
    // Start morph in the middle
    mixPerc = smoothstep(0.2,0.8,mixPerc);
    
    // Add bounce to morph
    mixPerc = sin(pow(mixPerc,2.5)*2.2)/sin(2.2);
    
    // Linear mix
    //float x = mix(getShape(shapeNr1,uv),
    //              getShape(shapeNr2,uv),
    //              mixPerc);
    
    // Alternate mix
    float x = 1.0/mix(1.0/getShape(shapeNr1,uv),
                      1.0/getShape(shapeNr2,uv),
                      mixPerc);
                      
    vec3 clr = vec3(0.0);
    
    float px2 = 2.0/iResolution.y;
    // distance lines
    clr.g = 0.6-0.6*smoothstep( 0.0,
                                px2,
                                abs(mod(x-.001,.05)-0.025));
    // shape
    clr.b = 1.0-smoothstep(0.5,
                           0.5+px2,
                           x);
    if (iMouse.w>0.) {
        clr.r = 0.7-0.7*smoothstep(0.49,0.49+px2, x); // The numbers
        clr.g = 0.7-0.7*smoothstep(0.00,px2, abs(x-0.49)); // Yellow outline
        clr.b = 0.4-0.4*smoothstep(0.43,0.53,1.0-x); // Background with shadow
        clr.rg += 0.12-0.12*smoothstep(0.00,0.04, abs(x-0.49)); // Yellow glow
        clr += 0.12-0.12*smoothstep(0.40,0.50, x); // Stretchmarks
        clr -= clr.b*(1.0- smoothstep( 0.0,
                                   px2,
                                   abs(mod(x-.001,.05)-0.025)));
    }
    
    fragColor = vec4(clamp(clr,0.0,1.0),1.0);
}*/