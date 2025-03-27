/*{
    "CREDIT": "Jason Beyers",
    "DESCRIPTION": "Converted from ISF",
    "TAGS": "converted_from_isf",
    "VSN": "1.0",
    "INPUTS": [

		{
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

      ],
	"GENERATORS": [
        {
            "NAME": "mat_time",
            "TYPE": "time_base",
            "PARAMS": {
                "speed": "mat_speed",
                "speed_curve":2,
                "strob" : "mat_strob",
                "reverse": "mat_reverse",
                "bpm_sync": "mat_bpm_sync",
                "link_speed_to_global_bpm":true
            }
        }
    ]
}*/




//vec3 iResolution = vec3(RENDERSIZE, 1.);


#define R mat2(cos(1.57*vec4(0,3,1,0)    // R(-1.25) = tilted grid

float adjusted_time(float m_time, float m_decay, float m_release)
{

    float adj_time;

    if (m_time < m_decay) {
        adj_time = 1;
    } else {
        // get back a value from 0-1 (from end of decay to 1 - end of beat)
        adj_time = (m_time - m_decay) * 1 / (1 - m_decay);
        if (m_time < m_release) {
            adj_time = 1 - adj_time * 1 / m_release;
        } else {
            adj_time = 0;
        }
    }

    return adj_time;

}


vec4 materialColorForPixel( vec2 texCoord ) {


	vec2 U = texCoord;

	vec4 O = vec4(1.0, 1.0, 1.0, 1.0);

	U *= mat_zoom;

    U *= 10;

	float adj_time = mat_time;
	adj_time = adj_time - mat_offset;



	//adj_time = bpm_offset;

	//adj_time = adjusted_time(adj_time, mat_decay, mat_release);



	//adj_time = mat_animation_time;



	//U *=  15./ iResolution.y;

    //            time rotation          cross center = 0 mod 3 in tilted grid
  //U = abs(ceil( R+iDate.w)) * ( U - R-1.25))*ceil( R+1.25))*U/3.2 -.5 )*3.2 ) -.5 ));
  //O = vec4(U.x==0.&&U.y<2.||U.y==0.&&U.x<2.);                       // draw cross

    // antialiased version: +7 chars
    //       time rotation          cross center = 0 mod 3 in tilted grid
     U = abs( R+adj_time)) * ( U - R-1.25))*ceil( R+1.25))*U/3.2 -.5 )*3.2 )  );

    //U = abs( R+vec4(adj_time))) * ( U - R-1.25))*ceil( R+1.25))*U/3.2 -.5 )*3.2 )  );
    O = smoothstep(.5, .4, max(min(U.x,U.y),max(U.x,U.y)-1.) ) +O-O;   // draw cross
	return O;
}

/**/


