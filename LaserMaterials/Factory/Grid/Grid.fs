/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
	"CREDIT": "Mad Team",
	"DESCRIPTION": "Grid",
	"TAGS": "template",
	"VSN": "1.0",
	"INPUTS": [ 
		{"LABEL": "Divisions X", "NAME": "mat_div_x", "TYPE": "int", "MIN": 0, "MAX": 40, "DEFAULT": 4.0 }, 
		{"LABEL": "Divisions Y", "NAME": "mat_div_y", "TYPE": "int", "MIN": 0, "MAX": 40, "DEFAULT": 4.0 }, 
		{"LABEL": "Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1], "FLAGS" : "no_alpha"}, 
	],
	"RENDER_SETTINGS": {
		"POINT_COUNT": 800
	}
}*/

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	shapeNumber = pointNumber / 2;
	if (shapeNumber > (mat_div_y+1) + (mat_div_x+1)) {
		shapeNumber = -1;
		return;
	}

	int isEndPoint = pointNumber % 2;

	if (shapeNumber < mat_div_y+1) {
		// Vertical Line
		if (mat_div_y == 0) {
			shapeNumber = -1;
			return;
		}
		int lineIdx = shapeNumber;
		pos.x = -1 + 2 * isEndPoint;
		pos.y = -1 + 2 * (float(lineIdx)/mat_div_y);
	} else {
		// Horizontal Line
		if (mat_div_x == 0) {
			shapeNumber = -1;
			return;
		}
		int lineIdx = shapeNumber - (mat_div_y+1);
		pos.x = -1 + 2 * (float(lineIdx)/mat_div_x);
		pos.y = -1 + 2 * isEndPoint;
	}

	color = mat_color;
}
