/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{
		  "NAME": "mat_waveform",
		  "TYPE": "audio",
		},

        {"LABEL": "Global/Count", "NAME": "mat_shape_count", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 50 }, 
        {"LABEL": "Global/V Spacing", "NAME": "mat_v_spacing", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 }, 
        {"LABEL": "Global/Translation Y", "NAME": "mat_base_transitiony", "TYPE": "float", "DEFAULT": -1.0, "MIN": -1.0, "MAX": 1.0 },
    		{"LABEL": "Global/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
    		{"LABEL": "Audio/Power", "NAME": "mat_audiopower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
    		{"LABEL": "Audio/Scale", "NAME": "mat_audioscale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.4 },
    		{"LABEL": "Noise/Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
    		{"LABEL": "Noise/Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
    		{"LABEL": "Noise/Speed", "NAME": "mat_noisespeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
	    {"LABEL": "Texture/Level", "NAME": "mat_texture_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
	    {"LABEL": "Texture/Texture", "NAME": "mat_texture", "TYPE": "image" },
	    {"LABEL": "Texture/X Range", "NAME": "mat_texture_x_range", "TYPE": "floatRange", "MIN": 0, "MAX": 1, "DEFAULT":[0,1] },
	    {"LABEL": "Texture/Y Range", "NAME": "mat_texture_y_range", "TYPE": "floatRange", "MIN": 0, "MAX": 1, "DEFAULT":[0,1] },
    ],
	"GENERATORS": [
		{"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noisespeed", "speed_curve":2, "bpm_sync": false, "link_speed_to_global_bpm":true}},
	],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4000
    }
}*/

#include "auto_all.glsl"

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
  int pointsPerShape = pointCount / mat_shape_count;
  shapeNumber = pointNumber / pointsPerShape;
  if (shapeNumber >= mat_shape_count) {
    shapeNumber = -1; // point will be ignored if shape number is negative
    return;
  }
  float normalizedShapeId = float(shapeNumber)/mat_shape_count;

  // Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
  float normalizedPosInShape = float(pointNumber-shapeNumber*pointsPerShape)/(pointsPerShape-1);
    
  // 2 points per line
  int lineNumber = shapeNumber;

  float size = getSizeValue(normalizedShapeId);

  pos = vec2(size * (-1 + 2*normalizedPosInShape),mat_v_spacing*(-1+2*normalizedShapeId+mat_base_transitiony));

  // Move
  float translateX = 0;
  if (mat_automoveactive) {
      if (mat_automoveactive) {
        // "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
        if (mat_automoveshape == 0) {
          translateX = mat_automovesize * sin((mat_move_position+normalizedShapeId*mat_automoveoffset)*2*PI) / 2;
        } else if (mat_automoveshape == 1) {
          translateX = mat_automovesize * fract(mat_move_position+normalizedShapeId*mat_automoveoffset);
        } else if (mat_automoveshape == 2) {
          translateX = mat_automovesize * (0.5-abs(mod((mat_move_position+normalizedShapeId*mat_automoveoffset)*2+1,2)-1));
        } else if (mat_automoveshape == 3) {
          translateX = mat_automovesize * (0.5-step(0.5,mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)));
        } else if (mat_automoveshape == 4) {
          translateX = mat_automovesize * (0.5*noise(vec2((mat_move_position+normalizedShapeId*mat_automoveoffset*99.5),0)));
        } else {
          translateX = mat_automovesize * (-0.5 * sin(-PI/2 + mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)*PI));
        }
      }
  }
  pos.y = -1 + 2*(fract((pos.y+1)/2+translateX));
 
  if (mat_audiopower > 0) {
    pos.y += IMG_NORM_PIXEL(mat_waveform,vec2(normalizedPosInShape*mat_audioscale,0)).r * mat_audiopower*mat_audiopower;
  }

  if (mat_noisepower > 0) {
    pos.y += mat_noisepower * noise(mat_noisescale * vec2(normalizedPosInShape,normalizedShapeId) + vec2(0,mat_noise_time));
  }

  mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
  pos = (vec3(pos,1) * transformMatrix).xy;

  vec4 textureColor = vec4(1);
  if (mat_texture_level > 0) {
	textureColor = mix(textureColor,
                       IMG_NORM_PIXEL(mat_texture,vec2(mat_texture_x_range.x+(mat_texture_x_range.y-mat_texture_x_range.x)*(0.5+pos.x/2),
													  mat_texture_y_range.x+(mat_texture_y_range.y-mat_texture_y_range.x)*(0.5-pos.y/2))),
                       mat_texture_level);
  }

  color = vec4(getLightValue(normalizedShapeId) * mat_color * textureColor * getColorValue(normalizedShapeId)); 
}
