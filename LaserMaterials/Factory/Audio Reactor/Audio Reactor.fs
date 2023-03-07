/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Audio reactive Laser Thing",
    "TAGS": "laser",
    "VSN": "1.3",
    "INPUTS": [

	 	{
            "NAME": "mat_waveform",
            "TYPE": "audio",
        },

		{"LABEL": "Global/Precision", "NAME": "mat_x", "TYPE": "int", "MIN": 2, "MAX": 300, "DEFAULT": 300 },
		
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
		{"LABEL": "Global/Size", "NAME": "mat_size", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 },
		{"LABEL": "Global/Feedback", "NAME": "mat_feedback", "TYPE": "float", "MIN": 0.0, "MAX": 0.999, "DEFAULT": 0. },
		{"LABEL": "Global/Polar", "NAME": "mat_polar", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" },
		{"LABEL": "Global/Audio Power", "NAME": "mat_apower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{"LABEL": "Global/Automove", "NAME": "mat_automove", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" }, 
		{"LABEL": "Global/Autorotate", "NAME": "mat_autorotate", "TYPE": "bool",  "DEFAULT": true, "FLAGS":"button" }, 
		{ "Label": "Noise/Type", "NAME": "mat_type","TYPE": "long", "DEFAULT": "Flow", "VALUES": [ "Flow", "Billow","White" ] },
		{"LABEL": "Noise/Speed", "NAME": "mat_nspeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.3 },  
		{"LABEL": "Noise/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.9 }, 
		{"LABEL": "Noise/Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 }, 
		{"LABEL": "Noise/Restrict to Y", "NAME": "mat_restrict", "TYPE": "bool", "DEFAULT": false, "FLAGS":"button" },  

		{ "LABEL": "Color/Out", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Center", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1., 1., 1., 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 2,"link_speed_to_global_bpm":true}},
        {"NAME": "mat_ntime", "TYPE": "time_base", "PARAMS": {"speed": "mat_nspeed","speed_curve": 2,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_x",
       "ANGLE_OPTIMIZATION": false
    }
}*/
#include "MadNoise.glsl"

const float pi = 3.14159265359;

float parabola( float x, float k )
{
    return pow( 4.0*x*(1.0-x), k );
}
mat2 rotate(float a) {
	float s = sin(a);
	float c = cos(a);
	return mat2(c, -s, s, c);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float t = mat_time;
	t = floor(t) + smoothstep(0.,1,fract(t));
	float normalizedPos = float(pointNumber)/(max(pointCount-1,2));

	float len = mat_size + float(mat_polar);
	float W = len/float(mat_x-1.);
	float nx = -len*0.5 + pointNumber*W  ;

	vec2 pos2d = vec2(nx,0.);

	vec3 n = vec3(0.);

 if(mat_polar){
	
	pos2d = vec2(sin(normalizedPos * 2. * pi), cos(normalizedPos *2. * pi))*mat_size;
	pos2d *= mix(1.,texture(mat_waveform,vec2(parabola(normalizedPos,2.)*normalizedPos,0.5)).r,0.2*mat_apower*4.);
    
	} else {

	pos2d.y += texture(mat_waveform,vec2(parabola(normalizedPos,2.)*normalizedPos,0.5)).r*mat_apower*4.;

	}

	if (mat_type==0) n = dFlowNoise( pos2d*mat_scale, mat_ntime )*mat_power;
	if (mat_type==1) n = ((dBillowedNoise( vec3(pos2d*mat_scale, mat_ntime) ).xyz)*2.-1.5)*mat_power;
	if (mat_type==2) n = ((dRidgedMF( vec3(pos2d*mat_scale, mat_ntime)*2.-1. ).xyz))*mat_power;

	if(mat_restrict) n.y = 0.;
	int idx = pointNumber/mat_x;
	pos2d +=  n.yz*0.1;

	float r = vnoise(vec2(t,3.45))-0.5;
	pos2d *= rotate(r*4.*pi*float(mat_autorotate));
	
	float x = vnoise(vec2(t + 0.23,1.45))-0.5;
	float y = vnoise(vec2(t + 4.23,8.0034))-0.5;
	pos2d += vec2(x,y)*float(mat_automove);

	vec2 lastPos = texture(mm_LastFrameData,vec2(float(pointNumber+0.5)/pointCount,0.2)).rg;

	pos = pos2d;

	if (lastPos.x > -1 && lastPos.x < 1) {
		pos = mix(pos,lastPos,mat_feedback);
	}


	shapeNumber = idx;
	color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,parabola(normalizedPos,10)),1);
}
