/*{
	"CREDIT": "by mojovideotech",
  "DESCRIPTION": "",
  "TAGS": "generator,graphic",
	"CATEGORIES": [
	],
	"INPUTS": [
 {
      "MAX": [
        0.35,
        0.99
      ],
      "MIN": [
        -0.35,
         0.01
      ],
      "DEFAULT":[0.0,0.0],
      "NAME": "horizon",
      "TYPE": "point2D"
},
 {
      "MAX": [
        0.999,
        0.999
      ],
      "MIN": [
         0.001,
         0.001
      ],
      "DEFAULT":[0.9,0.9],
      "NAME": "grid",
      "TYPE": "point2D"
},
    {
      "MAX": 33,
      "MIN": 1,
      "DEFAULT":9,
      "NAME": "zoom",
      "TYPE": "float"
},
    {
      "MAX": 4.5,
      "MIN": 3.5,
      "DEFAULT":3.75,
      "NAME": "fog",
      "TYPE": "float"
},
    {
      "MAX": 6.0,
      "MIN": 1.0,
      "DEFAULT":3.0,
      "NAME": "tint",
      "TYPE": "float"
}
	]
}*/

// GridZoomSandwich by mojovideotech
// based on : http://glslsandbox.com/e#25640.0

#ifdef GL_ES
precision mediump float;
#endif

vec4 materialColorForPixel(vec2 texCoord) {
	vec2 position = texCoord;

	float y = position.y - horizon.y;
	float yy = abs(y);
	if (y > 0.5) {
		gl_FragColor = vec4(0.25);
	} 
	
	y /= (horizon.y) ;
	
	float z = 1.0 / yy;
	float x = (position.x - 0.5) / yy;
	float color = 0.01;
	if (sin(z * 8.0 + TIME* zoom) > grid.y) {
		color += 0.3;
	} else {
	}
	if (sin(x * 40.0 + TIME *  - horizon.x * 100.0) > grid.x) {
		color += 0.3;
	} else {
	}
	vec4 result = vec4( vec3( color * (7.0 - tint), color * (tint + 1.0), sin( color + TIME / 2.0 ) * 0.85 ), 6.0 );

	if (yy < 0.99) {
		result *= (yy*(5.0-fog));
	}

  return result;
}