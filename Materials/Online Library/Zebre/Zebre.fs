// SaturdayShader Week 16 : Zebre
// by Joseph Fiola (http://www.joefiola.com)
// 2015-12-05
// Based on Patricio Gonzalez Vivo's "Wood Texture" example on http://patriciogonzalezvivo.com/2015/thebookofshaders/edit.html#11/wood.frag @patriciogv ( patriciogonzalezvivo.com ) - 2015


/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joseph Fiola",
	"DESCRIPTION": "",
	"TAGS": [ "Generator","waves", "random" ],
	"INPUTS": [
	
		{
			"NAME": "lineScale",
			"TYPE": "float",
			"DEFAULT": 2.0,
			"MIN": 0.0005,
			"MAX": 10.0
		},
		{
			"NAME": "harmonic",
			"TYPE": "float",
			"DEFAULT": 2.0,
			"MIN": 0.0,
			"MAX": 200.0
		},
		{
			"NAME": "lineOffsetSpeed",
			"TYPE": "float",
			"DEFAULT": 0.5,
			"MIN": 0.0,
			"MAX": 100.0
		},
		{
			"NAME": "brightness",
			"TYPE": "float",
			"DEFAULT": 0.4,
			"MIN": 0.1,
			"MAX": 10.0
		},
		{
			"NAME": "contrast",
			"TYPE": "float",
			"DEFAULT": 0.0,
			"MIN": 0.0,
			"MAX": 0.5
		},
		{
			"NAME": "contrastShift",
			"TYPE": "float",
			"DEFAULT": 0.0,
			"MIN": -0.5,
			"MAX": 0.5
		},
		{
			"NAME": "randomMultiply",
			"TYPE": "float",
			"DEFAULT": 43758.5453123,
			"MIN": 0.0,
			"MAX": 50000.0
		},
		{
			"NAME": "randomAmt",
			"TYPE": "point2D",
			"DEFAULT": [
				12.9898,
				78.233
			],
			"MIN": [
				0.0,
				0.0
			],
			"MAX": [
				100.0,
				100.0
			]
		},
		{
			"NAME": "origin",
			"TYPE": "point2D",
			"DEFAULT": [
				0.5,
				0.5
			],
			"MIN": [
				0.0,
				0.0
			],
			"MAX": [
				1.0,
				1.0
			]
		},
		{
			"NAME": "xyStretch",
			"TYPE": "point2D",
			"DEFAULT": [
				6.0,
				3.0
			],
			"MIN": [
				0.0,
				0.0
			],
			"MAX": [
				100.0,
				100.0
			]
		},
				{
			"NAME": "xyNoiseFactor",
			"TYPE": "point2D",
			"DEFAULT": [
				10.0,
				12.0
			],
			"MIN": [
				0.0,
				0.0
			],
			"MAX": [
				100.0,
				100.0
			]
		}
	
	]
}*/


#ifdef GL_ES
precision mediump float;
#endif


float mat_random (in vec2 st) { 
    return fract(sin(dot(st.xy,
                         vec2(randomAmt.x,randomAmt.y))) 
                * randomMultiply);
}

// Value noise by Inigo Quilez - iq/2013
// https://www.shadertoy.com/view/lsf3WH
float mat_noise(vec2 st) {

    vec2 i = floor(st);
	vec2 f = fract(st);


    vec2 u = f*f*(3.0-2.0*f);
    return mix( mix( mat_random( i + vec2(0.0,0.0) ), 
                     mat_random( i + vec2(1.0,0.0) ), u.x),
                mix( mat_random( i + vec2(0.0,1.0) ), 
                     mat_random( i + vec2(1.0,1.0) ), u.x), u.y);
}


mat2 mat_rotate2d(float angle){
    return mat2(cos(angle *xyNoiseFactor.x),-sin(angle),
                sin(angle * xyNoiseFactor.y),cos(angle));
}


float mat_lines(in vec2 pos, float b){
    float scale = lineScale;
    pos *= scale;
    return smoothstep(0.0,
                    .5+b*.5,
                    abs((sin(pos.x*3.1415)+b*2.0))* brightness);
}


vec4 materialColorForPixel(vec2 texCoord) {
    vec2 st = texCoord.xy;
    st -= vec2(origin);

    vec2 pos = st.yx*vec2(xyStretch);

    float pattern = pos.x;

    // Add noise
    pos = mat_rotate2d( mat_noise(pos) ) * pos * harmonic + (TIME * lineOffsetSpeed);
    
    // Draw lines
    pattern = mat_lines(pos,0.5);
    
    //adjust contrast
	pattern += smoothstep(0.0+contrast+contrastShift,1.0-contrast+contrastShift, pattern);

    return vec4(vec3(pattern),1.0);
}
