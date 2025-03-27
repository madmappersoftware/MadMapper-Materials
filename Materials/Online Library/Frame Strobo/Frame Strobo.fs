/*{
    "CREDIT": "frz/ 1024 architecture",
    "DESCRIPTION": "Strobes for 1 frame every n frames",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Every n frames", "NAME": "mat_frame", "TYPE": "int", "MIN": 2, "MAX": 60, "DEFAULT": 2 }, 
    ],

}*/

vec4 materialColorForPixel( vec2 texCoord )
{

	int k = int(FRAMEINDEX) % mat_frame; 
	vec3 col = vec3(0.);
	if (k== 0) col = vec3(1.);
	return vec4(col,1.);
}