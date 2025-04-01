// SaturdayShader Week 30 : Wisps
// by Joseph Fiola (http://www.joefiola.com)
// 2016-03-12

// Based on Week 29 Saturday Shader + "WAVES" Shadertoy by bonniem
// https://www.shadertoy.com/view/4dsGzH


/*{
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "Joseph Fiola",
  "DESCRIPTION": "Based on Week 29 Saturday Shader + 'WAVES' Shadertoy by bonniem - https://www.shadertoy.com/view/4dsGzH",
  "TAGS": [ "Generator"],
  "INPUTS": [
    {
      "NAME": "lines",
      "TYPE": "float",
      "DEFAULT": 50,
      "MIN": 1,
      "MAX": 200
    },
    {
      "NAME": "linesStartOffset",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": 0,
      "MAX": 1
    },
    {
      "NAME": "amp",
      "TYPE": "float",
      "DEFAULT": 0.1,
      "MIN": 0,
      "MAX": 1
    },
    {
      "NAME": "glow",
      "TYPE": "float",
      "DEFAULT": -6,
      "MIN": -40,
      "MAX": 0
    },
    {
      "NAME": "mod1",
      "TYPE": "float",
      "DEFAULT": 1,
      "MIN": 0,
      "MAX": 1
    },
    {
      "NAME": "mod2",
      "TYPE": "float",
      "DEFAULT": 0.01,
      "MIN": -1,
      "MAX": 1
    },
    {
      "NAME": "twisted",
      "TYPE": "float",
      "DEFAULT": 0.01,
      "MIN": -0.5,
      "MAX": 0.5
    },
    {
      "NAME": "zoom",
      "TYPE": "float",
      "DEFAULT": 11,
      "MIN": 0,
      "MAX": 100
    },
    {
      "NAME": "rotateCanvas",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": 0,
      "MAX": 1
    },
    {
      "NAME": "scroll",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": 0,
      "MAX": 1
    },
    {
      "NAME": "pos",
      "TYPE": "point2D",
      "DEFAULT": [
        0.5,
        0.5
      ],
      "MIN": [
        0,
        0
      ],
      "MAX": [
        1,
        1
      ]
    },
	{ "LABEL": "Scroll/Auto Scroll", "NAME": "mat_auto_scroll_active", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" },  
    { "LABEL": "Scroll/Speed", "NAME": "mat_auto_scroll_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
    { "LABEL": "Scroll/BPM Sync", "NAME": "mat_bpm_sync", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
  ],
  "GENERATORS": [
	{"NAME": "mat_auto_scroll_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_auto_scroll_speed", "bpm_sync": "mat_bpm_sync", "speed_curve":3}},
  ]
}*/


#define PI 3.14159265359
#define TWO_PI 6.28318530718

mat2 rotate2d(float _angle){
    return mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle));
}

vec4 materialColorForPixel(vec2 texCoord)
{
	vec2 uv = texCoord;
	uv -= vec2(pos.x,1-pos.y);
	uv *= zoom; // Scale the coordinate system
	uv = rotate2d(rotateCanvas*-TWO_PI) * uv; 
	
	float scrollValue = scroll;
	if (mat_auto_scroll_active) scrollValue += fract(mat_auto_scroll_time);
	
	// waves
	vec3 wave_color = vec3(0.0);
	
	float wave_width = 0.01;
	//uv  = -1.0 + 2.0 * uv;
	//uv.y += 0.1;
	for(float i = 0.0; i < 200.0; i++) {
		if (i > lines) break;
		
		uv = rotate2d(twisted*-TWO_PI) * uv; 
		
		uv.y +=  sin(sin(uv.x + i*mod1 + (scrollValue * TWO_PI) ) * amp + (mod2 * PI));

		
		if(lines * linesStartOffset - 1.0 <= i) {
			wave_width = abs(1.0 / (50.0 * uv.y * glow));
			wave_color += vec3(wave_width, wave_width, wave_width);
		}
	}
	
	return vec4(wave_color, 1.0);
}