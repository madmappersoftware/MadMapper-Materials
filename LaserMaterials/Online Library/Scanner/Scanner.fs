/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple Horizontal Scanner",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 1 }, 
		{"LABEL": "Global/Height", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.7 },  
		{"LABEL": "Global/Rotation", "NAME": "mat_rotation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{"LABEL": "Global/Offset Y", "NAME": "mat_offset", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },


		{"LABEL": "Movement/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  	
		{"LABEL": "Movement/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		

		{ "NAME": "TYPE", "Label": "Noise/Type", "TYPE": "long", "DEFAULT": "Flow", "FLAGS": "generate_as_define", 
			"VALUES": ["Flow", "White", "Billowed", ] },
		{"LABEL": "Noise/Speed", "NAME": "mat_noise_speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Noise/Amplitude", "NAME": "mat_noise_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Top", "NAME": "mat_topColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Down", "NAME": "mat_downColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
		{"NAME": "mat_instance_count", "TYPE": "multiplier", "PARAMS": {"value1": "mat_amount", "value2" : 2}},
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
		{"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noise_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_instance_count",
    }
}*/
#include "MadNoise.glsl"

mat2 matrot(float a) { return mat2(cos(a),-sin(a),sin(a),cos(a));}
float Mhash13(vec3 p3)
{
	p3  = fract(p3 * .1031);
    p3 += dot(p3, p3.zyx + 31.32);
    return fract((p3.x + p3.y) * p3.z);
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    float normalizedPos = float(pointNumber)/(pointCount-1);

	float pointPos = (pointNumber%2)*2.-1.;

	float n = 0.;
#if defined( TYPE_IS_Flow )
        n = flowNoise(vec2(mat_noise_time,float(pointNumber/2) ),1.345)*mat_noise_amp;
#elif defined( TYPE_IS_White )
		n = Mhash13(vec3(mat_noise_time,float(pointNumber/2),1.345))*mat_noise_amp;
#elif defined( TYPE_IS_Billowed )
		n = (billowedNoise(vec3(mat_noise_time,float(pointNumber/2)*100. ,1.345)*2.-1.))*mat_noise_amp*2.;
#endif

    pos = vec2(0., pointPos*mat_scale);

	pos *= matrot(mat_rotation*3.14156);
	pos.x += n;
	pos.y += + mat_offset;
	pos.x += sin(mat_time )*mat_amp;

//	vec2 lastPos = texture(mm_LastFrameData,vec2(float(pointNumber+0.5)/pointCount,0.2)).rg;
//
//	if (lastPos.x > -1 && lastPos.x < 1) {
//		pos = mix(pos,lastPos,mat_feedback);
//	}

    shapeNumber =  (pointNumber/2);

    color = vec4(mix(mat_topColor.rgb,mat_downColor.rgb,vec3(pointNumber%2)),1.);
}
