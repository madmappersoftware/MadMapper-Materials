/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Fun Spiral\nPush the laser surface Max Speed to the max for best results",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 500, "DEFAULT": 500 }, 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Revolutions", "NAME": "mat_rev", "TYPE": "float", "MIN": 1., "MAX": 100.0, "DEFAULT": 100. },  
		{"LABEL": "Global/Attitude", "NAME": "mat_attitude", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. },
{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1. },
{"LABEL": "Global/Damping", "NAME": "mat_damping", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.9 },

{"LABEL": "Global/Divisions", "NAME": "mat_div", "TYPE": "int", "MIN": 1, "MAX": 1000, "DEFAULT": 1 },
{"LABEL": "Global/Clamp to Border", "NAME": "mat_clamp", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" },

		{"LABEL": "Global/Start Radius", "NAME": "mat_start", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

		{"LABEL": "Noise/Frequency", "NAME": "mat_nfreq", "TYPE": "float", "MIN": 0., "MAX": 10.0, "DEFAULT": 4. },  
		{"LABEL": "Noise/Amplitude", "NAME": "mat_namp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 

		{ "LABEL": "Color/Center", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Outwards", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},
    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "INDEX_COUNT": "mat_amount",
    }
}*/

#include "MadNoise.glsl"

mat2 mrot(float a)
{
	return mat2(cos(a),sin(a),-sin(a),cos(a));
}

void vectorMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber)
{
    float normalizedPos = float(pointNumber)/(pointCount-1);
	float rev = mat_rev;
	pos = vec2(cos(normalizedPos*rev),sin(normalizedPos*rev))*(pow(normalizedPos,mat_attitude)+mat_start);
	vec2 n = dFlowNoise(pos*mat_nfreq,mat_time+normalizedPos*mat_nfreq).yz;
	pos += n*0.1*mat_namp*length(pos);
	pos *= mrot(mat_time);
	pos *= mat_scale;
	if(mat_clamp)pos = clamp(pos,vec2(-1.),vec2(1.));
	vec2 lpos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg; // previous position
	pos = mix(pos, lpos,vec2(mat_damping));	// apply damping
	shapeNumber = pointNumber/(2+(1000/mat_div));
    color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1.);
}