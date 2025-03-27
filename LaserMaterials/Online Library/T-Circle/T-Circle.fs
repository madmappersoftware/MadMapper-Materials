/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "Just a Circle",
    "TAGS": "graphics",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Center", "NAME": "mat_center", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0, 0 ] },
		{ "LABEL": "Fade", "NAME": "mat_fade", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0 }, 
		{ "LABEL": "Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
		{ "LABEL": "Transforms/Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": -360.0, "MAX": 360.0, "DEFAULT": 0 }, 
		{ "LABEL": "Transforms/Scale X", "NAME": "mat_scale_x", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1 },
		{ "LABEL": "Transforms/Scale Y", "NAME": "mat_scale_y", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 1 },
    ],
    "RENDER_SETTINGS": {
        "POINT_COUNT": 1024
    }
}*/

#define PI 3.1415926535897932384626433832795

mat2 rot(float a) {
  float ca=cos(a);
  float sa=sin(a);
  return mat2(ca,sa,-sa,ca);  
}

vec2 generateCirclePoint(float posInShape, vec2 center, float radius, float overlapSize, out float alpha)
{
	float posWithOverlap = posInShape;
	posWithOverlap *= (1+overlapSize);
	if (posWithOverlap<overlapSize) {
		alpha = posWithOverlap/overlapSize;
	} else if (posWithOverlap>1) {
		alpha = 1-(posWithOverlap-1)/overlapSize;
	} else {
		alpha = 1;
	}
	return center + radius * vec2(cos(3.14159*2*posWithOverlap),sin(3.14159*2*posWithOverlap));
}


void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    float normalizedPos = float(pointNumber)/(pointCount-1);

	float alpha;
	pos = generateCirclePoint(normalizedPos, mat_center, mat_size, mat_fade, alpha);
	pos = rot(PI*mat_rotation/180)*pos.xy;
	pos *= vec2(mat_scale_x,mat_scale_y);

    shapeNumber = 0;
    color = vec4(mat_color.rgb * alpha,1);
}
