/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "mad-matt",
    "DESCRIPTION": "Rythmic Patterns for chases",
    "VSN": "1.0",
    "TAGS": "line,graphic,rythmic",
    "INPUTS": [
        { "LABEL": "Definition", "NAME": "mat_definition", "TYPE": "int", "MIN" : 8, "MAX" : 1024, "DEFAULT": 1024 },

        { "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN" : 0, "MAX" : 4.0, "DEFAULT": 0.5 },
        { "LABEL": "Attack", "NAME": "mat_attack", "TYPE": "float", "DEFAULT": 0, "MIN": 0, "MAX": 1 },
        { "LABEL": "Decay", "NAME": "mat_decay", "TYPE": "float", "MIN" : 0.0, "MAX" : 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Release", "NAME": "mat_release", "TYPE": "float", "DEFAULT": 0.3, "MIN": 0.0, "MAX": 1.0 },
        { "LABEL": "BPM Sync", "NAME": "mat_bpmsync", "TYPE": "bool", "DEFAULT": true, "FLAGS": "button" }
    ], 
    "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "bpm_sync": "mat_bpmsync", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ]
}*/

float valueAtPos(float pos)
{
	float value = 0;
	if (pos < mat_attack) {
		value = pos / mat_attack;
	} else if (pos < mat_attack + mat_decay) {
		value = 1;
	} else if (pos < mat_attack + mat_decay+ mat_release) {
		value = pow(1 - (pos-(mat_attack + mat_decay)) / mat_release,2);
	}
	return value;
}

#define H_CELL_COUNT 32
#define V_CELL_COUNT 8
#define LINES_SIZE (1/32.)

vec4 materialColorForPixel(vec2 texCoord)
{
	texCoord.x += LINES_SIZE/H_CELL_COUNT;
	texCoord.y += LINES_SIZE/(2*V_CELL_COUNT);

	float value = 0;
	if (abs(fract(texCoord.x*H_CELL_COUNT)-LINES_SIZE/2)<(LINES_SIZE/2)) {
		value += 1 - abs(fract(texCoord.x*H_CELL_COUNT)-LINES_SIZE/2)/(LINES_SIZE/2);
	}
	if (abs(fract(texCoord.y*2*V_CELL_COUNT)-LINES_SIZE/2)<(LINES_SIZE/2)) {
		value = max(value, 1 - abs(fract(texCoord.y*2*V_CELL_COUNT)-LINES_SIZE/2)/(LINES_SIZE/2));
	}

	int cellY = 1+int(texCoord.y * V_CELL_COUNT);
	bool isCurveCell = fract(texCoord.y * V_CELL_COUNT) > 0.5;
	if (!isCurveCell) {
		texCoord.x = int(texCoord.x*mat_definition) / float(mat_definition);
	}
	texCoord.x = fract(texCoord.x + mat_animation_time);
	float pos = fract(texCoord.x*cellY);
	
	if (isCurveCell) {
		float yPosInCurveCell = (1 - 2 * (fract(texCoord.y * 8)-0.5)) * 1.1;

		float value1 = valueAtPos(pos-1.0/800);
		float value2 = valueAtPos(pos+1.0/800);

        float minY = min(value1,value2);
        float maxY = max(value1,value2)+20.0/800;

        value += (yPosInCurveCell>=minY&&yPosInCurveCell<=maxY)?1:0;

    		return vec4(vec3(value), 1);
	}  else {
		float value1 = valueAtPos(pos);
		float value2 = valueAtPos(pos+0.9/mat_definition);
		value += (value1+value2)/2;
    		return vec4(vec3(value), 1);
	}
}
