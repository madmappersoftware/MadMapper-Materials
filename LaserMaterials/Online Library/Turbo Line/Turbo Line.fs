/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple Line to be controlled externally by OSC",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
	{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 500, "DEFAULT": 300 }, 
	{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 }, 
	{ "LABEL": "Global/Position A", "NAME": "mat_posa", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ -1, 0.0 ] },
	{ "LABEL": "Global/Position B", "NAME": "mat_posb", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 1, 0.0 ] },

	{ "NAME": "TYPE", "Label": "Noise/Type", "TYPE": "long", "DEFAULT": "fBm", "VALUES": ["Flow", "White",  "fBm",] },
	{"LABEL": "Noise/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 2.0 },  
	{"LABEL": "Noise/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 5. },  
	{"LABEL": "Noise/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 

	{ "LABEL": "Color/Color A", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
	{ "LABEL": "Color/Color B", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_amount",
		"PRESERVE_ORDER": true,
    }
}*/

#include "MadNoise.glsl"

vec2 lhash22(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yzx+33.33);
    return fract((p3.xx+p3.yz)*p3.zy);
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{	
    float normalizedPos = float(pointNumber)/(pointCount-1);
    pos = mix(mat_posa,mat_posb,vec2(normalizedPos));

	float L = distance(mat_posa,mat_posb)*2.;

	float dista = distance(mat_posa,pos);
	float distb = distance(mat_posb,pos);

	vec2 n = vec2(0);
	if(TYPE == 0){
         n = dFlowNoise(pos*mat_scale,mat_time).yz;
	}else if(TYPE == 1){

		n = lhash22(vec2(floor(normalizedPos*10.*(mat_scale)),floor(mat_time*10)))*2.-1.;
	}else if(TYPE == 2){
		n = dfBm(vec3(pos*mat_scale,mat_time)).yz;
	}

	pos += n*mat_amp*2.*dista*distb*(1./L);

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	pos = mix(pos,lastFramePos,vec2(mat_feedback));

    shapeNumber = 10;
    color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1.);
}
