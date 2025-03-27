/*{
		"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "XYZ Synthetizer with Oscilators & LFOs, audio synth style",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 
		{"LABEL": "Global/Z Fade", "NAME": "mat_z_fade", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 

		{"LABEL": "X/Waveform", "NAME": "mat_x_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Sin" }, 
		{"LABEL": "X/Level", "NAME": "mat_x_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "X/Freq", "NAME": "mat_x_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{"LABEL": "X/Freq LFO1", "NAME": "mat_x_freq_lfo1", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.015 }, 
		{"LABEL": "X/Freq LFO2", "NAME": "mat_x_freq_lfo2", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "X/Phase", "NAME": "mat_x_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "X/Phase Speed", "NAME": "mat_x_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 

		{"LABEL": "Y/Waveform", "NAME": "mat_y_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Sin" }, 
		{"LABEL": "Y/Level", "NAME": "mat_y_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Y/Freq", "NAME": "mat_y_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.0 }, 
		{"LABEL": "Y/Freq LFO1", "NAME": "mat_y_freq_lfo1", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Y/Freq LFO2", "NAME": "mat_y_freq_lfo2", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.15 }, 
		{"LABEL": "Y/Phase", "NAME": "mat_y_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 
		{"LABEL": "Y/Phase Speed", "NAME": "mat_y_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 

		{"LABEL": "Z/Waveform", "NAME": "mat_z_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Square" }, 
		{"LABEL": "Z/Level", "NAME": "mat_z_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Z/Freq", "NAME": "mat_z_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Z/Freq LFO1", "NAME": "mat_z_freq_lfo1", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Z/Freq LFO2", "NAME": "mat_z_freq_lfo2", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Z/Phase", "NAME": "mat_z_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Z/Phase Speed", "NAME": "mat_z_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.3 }, 

		{"LABEL": "Light/Waveform", "NAME": "mat_light_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Sin" }, 
		{"LABEL": "Light/Level", "NAME": "mat_light_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Light/Freq", "NAME": "mat_light_freq", "TYPE": "float", "MIN": 0.0, "MAX": 8.0, "DEFAULT": 2.0 }, 
		{"LABEL": "Light/Freq LFO1", "NAME": "mat_light_freq_lfo1", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Light/Freq LFO2", "NAME": "mat_light_freq_lfo2", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Light/Phase", "NAME": "mat_light_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Light/Phase Speed", "NAME": "mat_light_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 

		{"LABEL": "LFO1/Waveform", "NAME": "mat_lfo1_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Sin" }, 
		{"LABEL": "LFO1/Level", "NAME": "mat_lfo1_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "LFO1/Freq", "NAME": "mat_lfo1_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.07 }, 
		{"LABEL": "LFO1/Phase", "NAME": "mat_lfo1_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "LFO1/Speed", "NAME": "mat_lfo1_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.7 }, 

		{"LABEL": "LFO2/Waveform", "NAME": "mat_lfo2_waveform", "TYPE": "long", "VALUES": ["Sin","Tri","Square","Noise"], "DEFAULT": "Sin" }, 
		{"LABEL": "LFO2/Level", "NAME": "mat_lfo2_level", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 }, 
		{"LABEL": "LFO2/Freq", "NAME": "mat_lfo2_freq", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.2 }, 
		{"LABEL": "LFO2/Phase", "NAME": "mat_lfo2_phase", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
		{"LABEL": "LFO2/Speed", "NAME": "mat_lfo2_phase_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 

		{"LABEL": "Color/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
    ],
  "GENERATORS": [
    {
      "NAME": "mat_phase_x_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_x_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_phase_y_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_y_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_phase_z_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_z_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_phase_light_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_light_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_phase_lfo1_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_lfo1_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
    {
      "NAME": "mat_phase_lfo2_pos",
      "TYPE": "time_base",
      "PARAMS": {"speed": "mat_lfo2_phase_speed", "bpm_sync": "mat_bpmsync", "speed_curve":2, "link_speed_to_global_bpm":true, "max_value":10000 }
    },
  ],
  "RENDER_SETTINGS": {
	"POINT_COUNT": 1024,
	"PRESERVE_ORDER": true,
	"ANGLE_OPTIMIZATION": false,
	"SKIP_BLACK": false,
	"MAX_SPEED": 4
  }
}*/

#include "MadCommon.glsl"
#include "auto_all.glsl"

float getShapeValue(float normalizedPos, int shape, float phase)
{
	if (shape == 0) {
		return cos(PI*(2*normalizedPos+phase));
	} else if (shape == 1) {
		return -1+2*fract(normalizedPos+phase);
	} else if (shape == 2) {
		return fract(normalizedPos+phase)>0.5?1:-1;
	} else if (shape == 3) {
		return noise(vec2(normalizedPos*4+phase,0));
	}
}

vec3 computeXyzAt(float normalizedPosInShape)
{
	float lfo1 = mat_lfo1_level * getShapeValue(normalizedPosInShape*mat_lfo1_freq*mat_lfo1_freq,mat_lfo1_waveform,mat_lfo1_phase + mat_phase_lfo1_pos);
	float lfo2 = mat_lfo2_level * getShapeValue(normalizedPosInShape*mat_lfo2_freq*mat_lfo2_freq,mat_lfo2_waveform,mat_lfo2_phase + mat_phase_lfo2_pos);

	float xFreq = mat_x_freq+(mat_x_freq_lfo1*lfo1)+(mat_x_freq_lfo2*lfo2);
	float yFreq = mat_y_freq+(mat_y_freq_lfo1*lfo1)+(mat_y_freq_lfo2*lfo2);
	float zFreq = mat_z_freq+(mat_z_freq_lfo1*lfo1)+(mat_z_freq_lfo2*lfo2);

	float x = mat_x_level * getShapeValue(normalizedPosInShape*xFreq*xFreq,mat_x_waveform,mat_x_phase + mat_phase_x_pos);
	float y = mat_y_level * getShapeValue(normalizedPosInShape*yFreq*yFreq,mat_y_waveform,mat_y_phase + mat_phase_y_pos);
	float z = mat_z_level * getShapeValue(normalizedPosInShape*zFreq*zFreq,mat_z_waveform,mat_z_phase + mat_phase_z_pos);

	return vec3(x,y,z);
}

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	float normalizedPosInShape = float(pointNumber)/(pointCount-1);

	// For closing the shape, we mix it with shape at normalizedPosInShape-1
	vec3 pos3D = mix(computeXyzAt(normalizedPosInShape),computeXyzAt(normalizedPosInShape-1),normalizedPosInShape);

	vec4 pos4D = vec4(pos3D,1) * make3dTransformMatrix();
	pos3D = pos4D.xyz / pos4D.w;

	pos = mat_scale * pos3D.xy * (2-pos3D.z);

	float lfo1 = mat_lfo1_level * getShapeValue(normalizedPosInShape*mat_lfo1_freq*mat_lfo1_freq,mat_lfo1_waveform,mat_lfo1_phase + mat_phase_lfo1_pos);
	float lfo2 = mat_lfo2_level * getShapeValue(normalizedPosInShape*mat_lfo2_freq*mat_lfo2_freq,mat_lfo2_waveform,mat_lfo2_phase + mat_phase_lfo2_pos);

	float lightFreq = mat_light_freq+(mat_light_freq_lfo1*lfo1)+(mat_light_freq_lfo2*lfo2);

	float light = mix(1,0.5+0.5*getShapeValue(normalizedPosInShape*lightFreq*lightFreq,mat_light_waveform,mat_light_phase + mat_phase_light_pos*lightFreq),mat_light_level);

	color = vec4(light) * (1-mat_z_fade * pos3D.z);

	shapeNumber = 0;
}
