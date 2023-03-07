/*
MadMapper Signed Distance Field Glsl Library

Copyright (c) 2016, Garage Cube, All rights reserved.
Copyright (c) 2016, Simon Geilfus, All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that
the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and
 the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
 the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.
*/

// Options
//______________________________________________

//#define SDF_ANTIALIASING_NONE
//#define SDF_ANTIALIASING_MEDIUM
//#define SDF_ANTIALIASING_HIGH

// Defaut to SDF_ANTIALIASING_HIGH if nothing specified
#if ! defined( SDF_ANTIALIASING_NONE ) && ! defined( SDF_ANTIALIASING_MEDIUM ) && ! defined( SDF_ANTIALIASING_HIGH )
    #define SDF_ANTIALIASING_HIGH
#endif

#if defined( SDF_ANTIALIASING_MEDIUM ) && ! defined( SDF_ANTIALIASING_MEDIUM_DIST_SCALE )
    #define SDF_ANTIALIASING_MEDIUM_DIST_SCALE 850.0
#endif

// 2D Primitives
//______________________________________________

//! Returns the signed distance of a circle
float circle( vec2 p, float radius );
//! Returns the signed distance of a circle
float circle( vec2 p, vec2 center, float radius );
//! Returns the signed distance of a box
float rectangle( vec2 p, float radius );
//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 size );
//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 center, float radius );
//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 center, vec2 size );
//! Returns the signed distance of a rounded box
float roundedRectangle( vec2 p, vec2 size, float cornerRadius );
//! Returns the signed distance of a rounded box
float roundedRectangle( vec2 p, vec2 center, vec2 size, float cornerRadius );
//! Returns the signed distance of a triangle
float triangle( vec2 p, float radius );
//! Returns the signed distance of a triangle
float triangle( vec2 p, vec2 center, float radius );
//! Returns the signed distance of an hexagon
float hexagon( vec2 p, float radius );
//! Returns the signed distance of an hexagon
float hexagon( vec2 p, vec2 center, float radius );
//! Returns the signed distance of a line
float line( vec2 p, vec2 start, vec2 end, float width );
//! Returns the signed distance of an arc
float arc( vec2 p, float radius, float angle, float width );

// Colors
//______________________________________________
//! Returns the colored inner distance field
vec3 fill( vec3 a, vec3 b, float dist );
//! Returns the colored inner distance field
vec4 fill( vec4 a, vec4 b, float dist );
//! Returns the colored border of the distance field
vec3 stroke( vec3 a, vec3 b, float dist, float width );
//! Returns the colored border of the distance field
vec4 stroke( vec4 a, vec4 b, float dist, float width );
//! Returns the colored blurred border of the distance field
vec3 glow( vec3 color, float dist, float width );
//! Returns the colored blurred border of the distance field
vec4 glow( vec4 color, float dist, float width );

// 2D Transforms
//______________________________________________

//! Returns a translated position
vec2 translate( vec2 p, vec2 t );
//! Returns a rotated position
vec2 rotate( vec2 p, float a );
//! Returns a transformed position
vec2 transform( vec2 p, mat2 m );

// 2D Domain and Distance Operations
//______________________________________________

//! Returns the repeated position every offset
vec2 repeat( vec2 p, vec2 offset );
//! Returns the repeated position every offset with the ID for the current cell
vec2 repeat( vec2 p, vec2 offset, out vec2 cellId );
//! Returns the repeated position with an offset between each a of size b
vec2 repeat( vec2 p, vec2 offset, vec2 a, vec2 b );
//! Returns the repeated position with an offset between each a of size b with the ID for the current cell
vec2 repeat( vec2 p, vec2 offset, vec2 a, vec2 b, out vec2 cellId );
//! Returns the repeated position radially each angle
vec2 repeatRadial( vec2 p, float angle );
//! Returns the repeated position radially each angle with the ID for the current cell
vec2 repeatRadial( vec2 p, float angle, out vec2 cellId );
//! Returns the repeated position radially each angle starting at angleOffset
vec2 repeatRadial( vec2 p, float angle, float angleOffset );
//! Returns the repeated position radially each angle starting at angleOffset with the ID for the current cell
vec2 repeatRadial( vec2 p, float angle, float angleOffset, out vec2 cellId );

//! Returns the union of two signed distance field
float blend( float d1, float d2 );
//! Returns the smooth blend of two signed distance field
float smoothBlend( float d1, float d2, float k );
//! Returns the subtraction of a signed distance field by another signed distance field 
float subtract( float d1, float d2 );
//! Returns the intersection of two signed distance field
float intersect( float d1, float d2 );


// Implementation
//______________________________________________

//! Returns the signed distance of a circle
float circle( vec2 p, float radius )
{
    return length( p ) - radius;
}

//! Returns the signed distance of a circle
float circle( vec2 p, vec2 center, float radius )
{
    return length( p - center ) - radius;
}

//! Returns the signed distance of a box
float rectangle( vec2 p, float radius )
{
    vec2 d = abs( p ) - vec2( radius );
    return min( max( d.x, d.y ), 0.0 ) + length( max( d, 0.0 ) );
}

//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 size )
{
    vec2 d = abs( p ) - size;
    return min( max( d.x, d.y ), 0.0 ) + length( max( d, 0.0 ) );
}

//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 center, float radius )
{
    vec2 d = abs( p - center ) - vec2( radius );
    return min( max( d.x, d.y ), 0.0 ) + length( max( d, 0.0 ) );
}

//! Returns the signed distance of a box
float rectangle( vec2 p, vec2 center, vec2 size )
{
    vec2 d = abs( p - center ) - size;
    return min( max( d.x, d.y ), 0.0 ) + length( max( d, 0.0 ) );
}

//! Returns the signed distance of a rounded box
float roundedRectangle( vec2 p, vec2 size, float cornerRadius )
{
  return length( max( abs( p ) - size + cornerRadius, 0.0 ) ) - cornerRadius;
}

//! Returns the signed distance of a rounded box
float roundedRectangle( vec2 p, vec2 center, vec2 size, float cornerRadius )
{
  return length( max( abs( p - center ) - size + cornerRadius, 0.0 ) ) - cornerRadius;
}

//! Returns the signed distance of a triangle
float triangle( vec2 p, float radius )
{
    return max( abs( p ).x * 0.866025 + p.y * 0.5, -p.y ) - radius * 0.5;
}

//! Returns the signed distance of a triangle
float triangle( vec2 p, vec2 center, float radius )
{
    p = p - center;
    return max( abs( p ).x * 0.866025 + p.y * 0.5, -p.y ) - radius * 0.5;
}

//! Returns the signed distance of an hexagon
float hexagon( vec2 p, float radius )
{
    vec2 absP = abs(p);
    return max((absP.x*0.866025+absP.y*0.5),absP.y) - radius;
}
//! Returns the signed distance of an hexagon
float hexagon( vec2 p, vec2 center, float radius )
{
    p = p - center;
    vec2 absP = abs(p);
    return max((absP.x*0.866025+absP.y*0.5),absP.y) - radius;
}

//! Returns the signed distance of a line
float line( vec2 p, vec2 start, vec2 end, float width )
{
    vec2 dir = start - end;
    float lngth = length(dir);
    dir /= lngth;
    vec2 proj = max(0.0, min(lngth, dot((start - p), dir))) * dir;
    return length( (start - p) - proj ) - (width / 2.0);
}

//! Returns the signed distance of an arc
float arc( vec2 p, float radius, float angle, float width )
{
    width   *= 0.5;
    radius  -= width;
    vec2 n  = vec2(cos( angle ), sin( angle ));
    return max( -( abs( p ).x * n.x + p.y * n.y ), abs( circle( p, radius ) ) - width );
}

// Colors
//______________________________________________

float fillDist( float dist )
{
#if defined( SDF_ANTIALIASING_NONE )
    return step( dist, 0.0 );
#elif defined( SDF_ANTIALIASING_MEDIUM )
    return clamp( -dist * SDF_ANTIALIASING_MEDIUM_DIST_SCALE, 0.0, 1.0 );
#elif defined( SDF_ANTIALIASING_HIGH )
    //return smoothstep( fwidth( dist ), 0.0, dist );
    float aa = length( vec2( dFdx( dist ), dFdy( dist ) ) );
    return smoothstep( aa, -aa, dist );
#endif
}
float strokeDist( float dist, float width )
{
#if defined( SDF_ANTIALIASING_NONE )
    float s = max( -dist - width, dist );
    return step( s, 0.0 );
#elif defined( SDF_ANTIALIASING_MEDIUM )
    dist = dist * SDF_ANTIALIASING_MEDIUM_DIST_SCALE;
    width = width * SDF_ANTIALIASING_MEDIUM_DIST_SCALE;
    return clamp( - dist, 0.0, 1.0 ) - clamp( - dist - width, 0.0, 1.0 );
#elif defined( SDF_ANTIALIASING_HIGH )
    //float s = max( dist - width, -dist );
    //return smoothstep( fwidth( s ), 0.0, s );    
    float aa = length( vec2( dFdx( -dist ), dFdy( -dist ) ) );
    return 1.0f - ( smoothstep( aa, -aa, -dist ) + smoothstep( -aa, aa, -dist - width ) );
#endif
}

vec3 fill( vec3 a, vec3 b, float dist ) 
{
    return mix( a, b, fillDist( dist ) );
}
vec4 fill( vec4 a, vec4 b, float dist ) 
{
    return mix( a, b, fillDist( dist ) );
}
vec3 stroke( vec3 a, vec3 b, float dist, float width ) 
{
    return mix( a, b, strokeDist( dist, width ) );
}
vec4 stroke( vec4 a, vec4 b, float dist, float width ) 
{
    return mix( a, b, strokeDist( dist, width ) );
}
vec3 glow( vec3 color, float dist, float width ) 
{
    return color * ( 1.0 - smoothstep( -width, width, abs( dist ) ) );
}
vec4 glow( vec4 color, float dist, float width ) 
{
    return color * ( 1.0 - smoothstep( -width, width, abs( dist ) ) );
}

// Transforms
//______________________________________________

//! Returns a translated position
vec2 translate( vec2 p, vec2 t )
{
    return p - t;
}
//! Returns a rotated position
vec2 rotate( vec2 p, float a )
{
    mat2 m = mat2(cos(a), -sin(a), sin(a), cos(a));
    return p * m;
}
//! Returns a transformed position
vec2 transform( vec2 p, mat2 m )
{
    return p * m;
}

// 2D Domain and Distance Operations
//______________________________________________

//! Returns the repeated position every offset
vec2 repeat( vec2 p, vec2 offset )
{
    return mod( p, offset ) - 0.5 * offset;
}

//! Returns the repeated position every offset with the ID for the current cell
vec2 repeat( vec2 p, vec2 offset, out vec2 cellId )
{
    cellId = floor( p / offset );
    return mod( p, offset ) - 0.5 * offset;
}

//! Returns the repeated position with an offset between each a of size b
vec2 repeat( vec2 p, vec2 offset, vec2 a, vec2 b )
{
    p += mod( floor( p / offset ).yx, b ) * a;
    return mod( p, offset ) - 0.5 * offset;
}

//! Returns the repeated position with an offset between each a of size b with the ID for the current cell
vec2 repeat( vec2 p, vec2 offset, vec2 a, vec2 b, out vec2 cellId )
{
    p += mod( floor( p / offset ).yx, b ) * a;
    cellId = floor( p / offset );
    return mod( p, offset ) - 0.5 * offset;
}

//! Returns the repeated position radially each angle
vec2 repeatRadial( vec2 p, float angle )
{
    float a = atan( p.y, p.x ) + angle * 0.5;
    float r = length( p );
    a       = mod( a, angle ) - angle * 0.5;
    return vec2( cos( a ), sin( a ) ) * r;      
}

//! Returns the repeated position radially each angle with the ID for the current cell
vec2 repeatRadial( vec2 p, float angle, out vec2 cellId )
{
    float a = atan( p.y, p.x ) + angle * 0.5;
    float r = length( p );
    cellId  = vec2( floor( a / angle ) );    
    a       = mod( a, angle ) - angle * 0.5;
    return vec2( cos( a ), sin( a ) ) * r;    
}

//! Returns the repeated position radially each angle starting at angleOffset
vec2 repeatRadial( vec2 p, float angle, float angleOffset )
{
    float a = angleOffset + atan( p.y, p.x ) + angle * 0.5;
    float r = length( p );
    a       = mod( a, angle ) - angle * 0.5;
    return vec2( cos( a ), sin( a ) ) * r;    
}

//! Returns the repeated position radially each angle starting at angleOffset with the ID for the current cell
vec2 repeatRadial( vec2 p, float angle, float angleOffset, out vec2 cellId )
{
    float a = angleOffset + atan( p.y, p.x ) + angle * 0.5;
    float r = length( p );
    cellId  = vec2( floor( a / angle ) );   
    a       = mod( a, angle ) - angle * 0.5;
    return vec2( cos( a ), sin( a ) ) * r;    
}

//! Returns the union of two signed distance field
float blend( float d1, float d2 )
{
    return min( d1, d2 );
}

float smin( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}
//! Returns the smooth blend of two signed distance field
float smoothBlend( float d1, float d2, float k )
{
    return smin( d1, d2, k );
}
//! Returns the subtraction of a signed distance field by another signed distance field 
float subtract( float d1, float d2 )
{
    return max(d1, -d2);
}
//! Returns the intersection of two signed distance field
float intersect( float d1, float d2 )
{
    return max(d1, d2);
}


