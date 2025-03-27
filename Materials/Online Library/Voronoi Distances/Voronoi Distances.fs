/*
{

        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Derived by Tim Brassey from Shadertoy's Iteration Stripes by iq",
    "DESCRIPTION": "Iteration Stripes",
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "INPUTS": [

		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.2 }, 
		{"LABEL": "Shadow", "NAME": "mat_shadow", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{"LABEL": "Zoom", "NAME": "mat_zoom", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 11.0 },
		{"LABEL": "Tile X", "NAME": "mat_tilex", "TYPE": "int", "MIN": 1, "MAX": 6, "DEFAULT": 2 },
		{"LABEL": "Tile Y", "NAME": "mat_tiley", "TYPE": "int", "MIN": 1, "MAX": 6, "DEFAULT": 2 },
		{"LABEL": "Mirror X", "NAME": "mat_mirrorx", "TYPE": "bool", "DEFAULT": true },
		{"LABEL": "Mirror Y", "NAME": "mat_mirrory", "TYPE": "bool", "DEFAULT": true },
		{	"LABEL": "Fore Color",
			"NAME": "mat_color1",
			"TYPE": "color",
			"DEFAULT": [ 1.0, 0.5, 0.0, 1.0 ],
			"FLAGS": "no_alpha"
		},
		{	"LABEL": "BG Color",
			"NAME": "mat_color2",
			"TYPE": "color",
			"DEFAULT": [ 0.0, 0.0, 1.0, 1.0 ],
		},

    ],
	"GENERATORS": [
	{
		"NAME": "mat_time",
		"TYPE": "time_base",
		"PARAMS": {"speed": "mat_speed", "speed_curve": 2}
	}
	]

}

*/

   
// The MIT License
// Copyright ÃƒÆ’Ã†â€™Ãƒâ€ Ã¢â‚¬â„¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€¦Ã‚Â¡ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â€šÂ¬Ã…Â¡ÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â© 2013 Inigo Quilez
// https://www.youtube.com/c/InigoQuilez
// https://iquilezles.org/
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// I've not seen anybody out there computing correct cell interior distances for Voronoi
// patterns yet. That's why they cannot shade the cell interior correctly, and why you've
// never seen cell boundaries rendered correctly.
//
// However, here's how you do mathematically correct distances (note the equidistant and non
// degenerated grey isolines inside the cells) and hence edges (in yellow):
//
// https://iquilezles.org/articles/voronoilines
//
// More Voronoi shaders:
//
// Exact edges:  https://www.shadertoy.com/view/ldl3W8
// Hierarchical: https://www.shadertoy.com/view/Xll3zX
// Smooth:       https://www.shadertoy.com/view/ldB3zc
// Voronoise:    https://www.shadertoy.com/view/Xd23Dh

#define ANIMATE
#define TWO_PI 6.28318530718

vec2 hash2( vec2 p )
{
    // texture based white noise
    //return textureLod( iChannel0, (p+0.5)/256.0, 0.0 ).xy;

    // procedural white noise
    return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

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
        
        o = 0.5 + 0.5*sin( mat_time + TWO_PI * o );
        
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
    for( int j=-2; j<=2; j++ )
    for( int i=-2; i<=2; i++ )
    {
        vec2 g = mg + vec2(float(i),float(j));
        vec2 o = hash2( n + g );
        
        o = 0.5 + 0.5*sin( mat_time + TWO_PI * o );
        
        vec2 r = g + o - f;

        if( dot(mr-r,mr-r)>0.00001 )
        md = min( md, dot( 0.5*(mr+r), normalize(r-mr) ) );
    }

    return vec3( md, mr );
}

vec4 materialColorForPixel( vec2 texCoord )
{
	
	float boundx = 1. / mat_tilex;
	bool mirrorx = mat_mirrorx && mod(floor( texCoord.x / boundx ), 2.0) == 0.0;
	float boundedx = mod( texCoord.x, boundx );

	float boundy = 1. / mat_tiley;
	bool mirrory = mat_mirrory && mod(floor( texCoord.y / boundy ), 2.0) == 0.0;
	float boundedy = mod( texCoord.y, boundy );
    vec2 p = vec2(  mirrorx ? boundx - boundedx : boundedx , mirrory ? boundy - boundedy : boundedy ); //iResolution.xx;

    vec3 c = voronoi( mat_zoom * p );

    // isolines
    vec3 col = vec3(mat_color2.r, mat_color2.g, mat_color2.b); //c.x*(0.5 + 0.5*sin(64.0*c.x))*vec3(1.0);
    // borders
    col = mix( vec3(mat_color1.r,mat_color1.g,mat_color1.b), col, smoothstep( 0.05, 0.05 + mat_shadow / 0.2 * .25, c.x ) );
    // feature points
    //float dd = length( c.yz );
    //col = mix( vec3(1.0,0.6,0.1), col, smoothstep( 0.0, 0.12, dd) );
    //col += vec3(1.0,0.6,0.1)*(1.0-smoothstep( 0.0, 0.04, dd));

    return vec4(col,1.0);
}