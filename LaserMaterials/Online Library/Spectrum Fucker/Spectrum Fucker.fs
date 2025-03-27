/*{
	"RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "hexler",
    "DESCRIPTION": "Audio effect based on spectrum: https://vimeo.com/51993089.",
    "TAGS": "audio,reactive,spectrum",
    "VSN": "1.0",
    "INPUTS": [
        { "LABEL": "Count", "NAME": "mat_count", "TYPE": "int", "MIN" : 1, "MAX" : 40, "DEFAULT": 10 },
        { "LABEL": "Audio Level", "NAME": "mat_audio_level", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 1.0 },
        { "LABEL": "Speed", "NAME": "mat_animspeed", "TYPE": "float", "MIN" : 0.0, "MAX" : 2.0, "DEFAULT": 2.0 },
        { "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN" : 0.0, "MAX" : 3.0, "DEFAULT": 1.0 },
        { "LABEL": "Squirl", "NAME": "mat_squirl", "TYPE": "float", "MIN" : 0.0, "MAX" : 3.0, "DEFAULT": 1.0 },
        { "LABEL": "Distance", "NAME": "mat_distance", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 5.0 },
        { "NAME": "spectrum", "TYPE": "audioFFT", "SIZE": 3, "ATTACK": 0.0, "DECAY": 0.0, "RELEASE": 0.1 },
        { "LABEL": "Color 1", "NAME": "color1", "TYPE": "color", "DEFAULT": [1,1,1,1] },
        { "LABEL": "Color 2", "NAME": "color2", "TYPE": "color", "DEFAULT": [1,0,0,1] },
      ],
    "GENERATORS": [
        {"NAME": "anim_position", "TYPE": "time_base", "PARAMS": {"speed": "mat_animspeed","bpm_sync":true, "speed_curve": 3}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4096
    }
}*/

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
	int pointsPerShape = pointCount / mat_count;
	shapeNumber = pointNumber / pointsPerShape;
	if (shapeNumber >= mat_count) {
		shapeNumber = -1;
		return;
	}
	float normalizedPosInShape = float(pointNumber%pointsPerShape)/pointsPerShape;

	vec2 spec = vec2(IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r,IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r) * mat_audio_level * mat_audio_level * mat_audio_level;

	float x=0;
	float y=(-1+2*normalizedPosInShape);
	for (int i=0; i<shapeNumber; i++) {
		x += sin(i*mat_distance*mat_distance + spec.x * 30*fract(anim_position) + anim_position + mat_scale*mat_scale*y*mat_squirl) * spec.y;
	}

    pos = vec2(x,y);
    color = mix(color1,color2,float(shapeNumber)/(mat_count-1));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = 2*mat_scale*mat_scale*(texCoord-vec2(0.5));
	vec2 spec = vec2(IMG_NORM_PIXEL(spectrum,vec2(0.17,0)).r,IMG_NORM_PIXEL(spectrum,vec2(0.83,0)).r) * mat_audio_level * mat_audio_level;
	float col = 0.0;
	for (float i=0; i<30; i++) {
		uv.x += sin(i*mat_distance*mat_distance + spec.x * 30*fract(anim_position) + anim_position + uv.y*mat_squirl) * spec.y;
		col += abs(.066/uv.x) * spec.y;
	}
	return vec4(col,col,col,1);
}
