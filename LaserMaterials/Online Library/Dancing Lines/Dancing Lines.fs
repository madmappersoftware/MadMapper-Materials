/*{
  "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "mad Matt",
    "DESCRIPTION": "Animated path with different transformations",
    "TAGS": "atmos,graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "Draw Speed", "NAME": "mat_draw_speed", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 0.5 }, 
        {"LABEL": "Morph Speed", "NAME": "mat_morph_speed", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 0.5 }, 
        {"LABEL": "Length", "NAME": "mat_length", "TYPE": "float", "MIN": 0.0, "MAX": 20.0, "DEFAULT": 1 }, 
        {"LABEL": "Shape Gen", "NAME": "mat_shape_gen", "TYPE": "long", "DEFAULT": "Cos Combo", "VALUES": ["Cos Combo","Curl","Fbm"] }, 
        {"LABEL": "Close Shape", "NAME": "mat_close_shape", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }, 

        {"LABEL": "Color/Mode", "NAME": "mat_color_mode", "TYPE": "long", "DEFAULT": "Mix AB", "VALUES": ["Mix AB","Auto"] }, 
        {"LABEL": "Color/Color A", "NAME": "mat_color_a", "TYPE": "color", "DEFAULT": [1,1,1,1], "FLAGS": "no_alpha" },
        {"LABEL": "Color/Color B", "NAME": "mat_color_b", "TYPE": "color", "DEFAULT": [1,0,0,1], "FLAGS": "no_alpha" }, 

        {"LABEL": "Draw/Mode", "NAME": "mat_draw_mode", "TYPE": "long", "DEFAULT": "Fill", "VALUES": ["Fill","Points"] }, 
        {"LABEL": "Draw/Point Count", "NAME": "mat_point_count", "TYPE": "int", "MIN": 10, "MAX": 100, "DEFAULT": 20 }, 
    ],
    "GENERATORS": [
        {"NAME": "mat_draw_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_draw_speed"} },
        {"NAME": "mat_morph_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_morph_speed"} },
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": 4000,
       "PRESERVE_ORDER": true,
       "ANGLE_OPTIMIZATION": false
    }
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec3 mat_hsv2rgb(vec3 c) {
  vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
  vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
  return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

vec2 lissajous(float t, float a, float b, float d)
{
    return vec2(sin(a*t+d), sin(b*t));
}

vec2 posAtNormalizedPos(float normalizedPos)
{
	if (mat_shape_gen == 0) {
	    float animPos = normalizedPos + mat_draw_time; 
	    return 0.7*vec2(cos(animPos*mat_length+cos(4.2*animPos*mat_length)+mat_morph_time),cos(animPos*mat_length/3+sin(1.1+4.5*animPos*mat_length)+1.3*mat_morph_time));
	} else if (mat_shape_gen == 1) {
	    return curlNoise(vec2(normalizedPos*mat_length+mat_draw_time,mat_morph_time/2)).xy / 10;
	} else {
	    return dfBm(vec2(normalizedPos*mat_length/2+mat_draw_time/3,mat_morph_time/2)).yz / 5;
	}
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    float normalizedPos;
    if (mat_draw_mode==1) {
      if (pointNumber>=mat_point_count) {
        shapeNumber = -1;
        return;
      }
      shapeNumber = pointNumber;
      normalizedPos = float(pointNumber)/(mat_point_count-1);
    } else {
      shapeNumber = 0;
      normalizedPos = float(pointNumber)/(pointCount-1);
    }

	pos = posAtNormalizedPos(normalizedPos);

    if (mat_close_shape) {
      vec2 pos2 = posAtNormalizedPos(-1+normalizedPos);
      pos = mix(pos, pos2, normalizedPos);
    }

    if (mat_color_mode == 1) {
      color = vec4(mat_hsv2rgb(vec3(normalizedPos,1.,0.5)), 1);
    } else {
      color = mix(mat_color_a,mat_color_b,normalizedPos);
    }
}
