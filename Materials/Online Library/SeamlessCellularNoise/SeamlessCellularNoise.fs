/*{
    "CREDIT": "frz / lifted from shadertoy XdyBRc",
    "DESCRIPTION": "describe your material here",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 1 }, 
		{"LABEL": "Grid", "NAME": "mat_grid", "TYPE": "int", "MIN": 2, "MAX": 10, "DEFAULT": 2 },
    ],
}*/

/*
	--: Seamless cell noise : --
	   nachocpol@gmail.com
*/

vec2 ran2( vec2 p ) 
{
	return fract(sin(vec2(dot(p,vec2(127.1,311.7)),
			  			  dot(p,vec2(269.5,183.3))))
                 			* 43758.5453123);
}

float TileCell(vec2 tc, int grid, float seed)
{
    vec2 p   = tc * float(grid);
    vec2 ftc = fract(p);
    vec2 itc = floor(p);
    float m  = 1.0;
    
    for(int i = -1; i < 2; i++)
    {
        for(int j = -1; j < 2; j++)
        {
            vec2 n = vec2(i,j);
            vec2 q = itc + n;
            
            if(q.x == -1.0)q.x = float(grid-1);
            else if(q.x == float(grid))q.x = 0.0;
                
            if(q.y == -1.0)q.y =float(grid-1);
            else if(q.y == float(grid))q.y = 0.0;

            vec2 rp 	= ran2(q * seed);
            vec2 diff 	= n + rp - ftc;
            float dist 	= length(diff);
            m 			= min(m,dist);
        }
    } 
    
    return pow(1.0 - m,1.0);
}


vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv 	= texCoord;
	uv *= float(mat_scale);
	uv = fract(uv);
 
    float c 	= TileCell(uv,mat_grid,1.0);
	return vec4(c,c,c,1.);
}