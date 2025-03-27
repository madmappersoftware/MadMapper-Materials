/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple vector template",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 500, "DEFAULT": 10 }, 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Period", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.7 },  
		{"LABEL": "Global/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Left", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Right", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_amount"
    }
}*/

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    float normalizedPos = float(pointNumber)/(pointCount-1);
    pos = vec2(normalizedPos*2.-1.,cos(normalizedPos*3.14159*mat_scale+mat_time)*mat_amp);
    shapeNumber = (pointNumber/2);
    color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1.);
}
