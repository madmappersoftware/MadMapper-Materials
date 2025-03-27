/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Line with settable points - for external control or cueing mostly.",
    "TAGS": "laser,tool,line",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "P1", "NAME": "mat_p1", "TYPE": "point2D", "DEFAULT": [0,-0.5], "MIN": [-1,-1], "MAX": [1,1] },
        {"LABEL": "P2", "NAME": "mat_p2", "TYPE": "point2D", "DEFAULT": [0,0.5], "MIN": [-1,-1], "MAX": [1,1] },
    		{"LABEL": "Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": 2
    }
}*/

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
  pos = (pointNumber == 0)?mat_p1:mat_p2;
  shapeNumber = 0;
  color = mat_color; 
}
