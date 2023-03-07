out float adjusted_bpm_pos;

#define PI 3.1415926535897932384626433832795

void materialVsFunc(vec2 uv) {
	adjusted_bpm_pos = mod(strob_position + mat_offset,1); // Get full value on beat
	if (adjusted_bpm_pos < decay) {
		adjusted_bpm_pos = 1;
	} else {
		// get back a value from 0-1 (from end of decay to 1 - end of beat)
		adjusted_bpm_pos = (adjusted_bpm_pos - decay) * 1 / (1 - decay);
		if (adjusted_bpm_pos < release) {
			adjusted_bpm_pos = 1 - adjusted_bpm_pos * 1 / release;
		} else {
			adjusted_bpm_pos = 0;
		}
	}

    if (shape == 0) { // Out
        adjusted_bpm_pos = adjusted_bpm_pos;
    } else if (shape == 1) { // In
        adjusted_bpm_pos = 1-adjusted_bpm_pos;
    } else { // Smooth
        adjusted_bpm_pos = (1+sin((-0.25+adjusted_bpm_pos)*2*PI))/2;
    }
}
