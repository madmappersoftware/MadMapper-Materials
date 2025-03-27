/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Ctrl-Z",
    "DESCRIPTION": "Dot Ring",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Count", "NAME": "mat_count", "TYPE": "int", "MIN": 1, "MAX": 100, "DEFAULT": 12 },
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.5 },
		{"LABEL": "Global/x Trigo", "NAME": "mat_xTrigo", "TYPE": "long", "VALUES": ["Cosinus","Sinus","Tangent", "Inv cosinus","Inv sinus","Inv tangent", "div Cosinus", "div Sinus", "div Tangent"], "DEFAULT": "Cosinus" },
		{"LABEL": "Global/y Trigo", "NAME": "mat_yTrigo", "TYPE": "long", "VALUES": ["Cosinus","Sinus","Tangent", "Inv cosinus","Inv sinus","Inv tangent", "div Cosinus", "div Sinus", "div Tangent"], "DEFAULT": "Sinus" },	
		
		{"LABEL": "Transforms/Center", "NAME": "mat_center", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0, 0 ] },

		{"LABEL": "Color/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [ 0.0, 1.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_count"
    }
}*/

#define PI 3.1415926535897932384626433832795

#include "auto_all.glsl"

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
	float normalizedShapeId = float(pointNumber)/mat_count;
	float angle = 2*PI/mat_count;
	float t = pointNumber % mat_count;
	float ta = angle * t ;

		if (mat_xTrigo == 0){
		pos.x=cos(ta)* mat_size;
		}
		if (mat_xTrigo == 1){
		pos.x=sin(ta)* mat_size;
		}
		if (mat_xTrigo == 2){
		pos.x=tan(ta)* mat_size;
		}
		if (mat_xTrigo == 3){
		pos.x=acos(ta)* mat_size;
		}
		if (mat_xTrigo == 4){
		pos.x=asin(ta)* mat_size;
		}
		if (mat_xTrigo == 5){
		pos.x=atan(ta)* mat_size;
		}
		if (mat_xTrigo == 6){
		pos.x=cos(ta)/ mat_size;
		}
		if (mat_xTrigo == 7){
		pos.x=sin(ta)/ mat_size;
		}
		if (mat_xTrigo == 8){
		pos.x=tan(ta)/ mat_size;
		}

		//Y Trigo

		if (mat_yTrigo == 0){
		pos.y=cos(ta)* mat_size;
		}
		if (mat_yTrigo == 1){
		pos.y=sin(ta)* mat_size;
		}
		if (mat_yTrigo == 2){
		pos.y=tan(ta)* mat_size;
		}
		if (mat_yTrigo == 3){
		pos.y=acos(ta)* mat_size;
		}
		if (mat_yTrigo == 4){
		pos.y=asin(ta)* mat_size;
		}
		if (mat_yTrigo == 5){
		pos.y=atan(ta)* mat_size;
		}
		if (mat_yTrigo == 6){
		pos.y=cos(ta)/ mat_size;
		}
		if (mat_yTrigo == 7){
		pos.y=sin(ta)/ mat_size;
		}
		if (mat_yTrigo == 8){
		pos.y=tan(ta)/ mat_size;
		}


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
	mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
	pos = (vec3(pos,1) * transformMatrix).xy+mat_center;
	
	shapeNumber = pointNumber;

    //autocolor
	if(mat_autocoloractive){
	color = vec4(getLightValue(normalizedShapeId) * getColorValue(normalizedShapeId)); 
	}
	//autolight
	else if(mat_autolightactive){
	color = vec4(mat_color * getLightValue(normalizedShapeId));
	}
	else{
	color = vec4(mat_color);
	}
	
}


