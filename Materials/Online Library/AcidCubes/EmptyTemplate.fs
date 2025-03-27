/*{
    "CREDIT": "Shadertoy ttccDX\nby yozic",
    "DESCRIPTION": "acid donut",
    "TAGS": "trippy",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 }, 
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#define PI 3.141592
#define BALLS 16.

mat2 rotate2d(float a) {
  return mat2(cos(a), -sin(a), sin(a), cos(a));
}

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = texCoord;
  uv.x -= .5;
  uv.y -= .5;

	uv *= mat_scale;
 
  uv *= 100.;
  float dist = length((uv));
  vec4 fragColor = vec4(0.);

  for (float i = 0.; i < BALLS; i++) {
    uv.y += .5 * (i / 20.) * cos(uv.y / 1000. + mat_time / 4.) + sin(uv.x / 50. - mat_time / 2.)*0.1;
    uv.x += .5 * (i) * sin(uv.x / 300. + mat_time / 6.) * sin(uv.y / 50. + mat_time / 5.)*0.1;
    float t = .01 * dist * (i) * PI / BALLS * (5. + 1.);
    vec2 p = 8. * vec2(-1. * cos(t), 1. * sin(t / 6.));
    p /= sin(PI * sin(uv.x / 10.) * cos(uv.y / 11.));
    vec3 col = cos(vec3(0, 1, -1) * PI * 2. / 3. + PI * (mat_time / 5. + float(i) / 5.)) * 0.5 + 0.5;
    fragColor += vec4(float(i) * .2 / length(uv - p * 0.9) * col, 1.);
  }


	return fragColor;
}