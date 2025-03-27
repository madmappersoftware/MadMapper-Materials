/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by Stevie Wonder - Songs In The Key Of Life / 1976",
    "VSN": "1.0",
    "TAGS": "album cover",
    "INPUTS": [  
        { "LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0, "MAX": 2, "DEFAULT": 1.15 },  
        { "LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.0 },  
		{ "LABEL": "Global/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 0.25,  0.25 ], "MIN": [ -0.25, -0.25 ], "DEFAULT": [ 0, 0 ]},		

		{ "LABEL": "Shape/Circles Count", "NAME": "mat_circlesCount", "TYPE": "int", "MIN": 0, "MAX": 10, "DEFAULT": 5 },
		{ "LABEL": "Shape/Circles Scale", "NAME": "mat_circlesScale", "TYPE": "float", "MIN": -1, "MAX": 1, "DEFAULT": 0 },   
		{ "LABEL": "Shape/Offset", "NAME": "mat_offsetCircles", "TYPE": "point2D", "MAX": [ 0.25,  0.25 ], "MIN": [ -0.25, -0.25 ], "DEFAULT": [0, 0]},		
 
		{ "LABEL": "Noise/Fbm Freq", "NAME": "mat_fbmFrequency", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 10 },  
		{ "LABEL": "Noise/Fbm Amp", "NAME": "mat_fbmAmplitude", "TYPE": "float", "MIN": 0, "MAX": 3, "DEFAULT": 1 },  
		{ "LABEL": "Noise/Ridged Freq", "NAME": "mat_ridgedFrequency", "TYPE": "float", "MIN": 0, "MAX": 10, "DEFAULT": 7 },  
		{ "LABEL": "Noise/Ridged Amp", "NAME": "mat_ridgedAmplitude", "TYPE": "float", "MIN": 0, "MAX": 3, "DEFAULT": 0.5 },

		{ "LABEL": "Audio/Sensibility", "NAME": "mat_audioSensibility", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0 },
        { "LABEL": "Audio/FFT", "NAME": "mat_spectrum128", "TYPE": "audioFFT", "SIZE": 128, "ATTACK": 0.2, "DECAY": 0.0, "RELEASE": 0.6 },

        { "LABEL": "Hue/Hue", "NAME": "mat_hue", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0, "FLAGS": "hidden" },  
		{ "LABEL": "Hue/Min", "NAME": "mat_hueMin", "TYPE": "float", "MIN": -0.5, "MAX": 0.5, "DEFAULT": 0, "FLAGS": "hidden" }, 
		{ "LABEL": "Hue/Max", "NAME": "mat_hueMax", "TYPE": "float", "MIN": -1.0, "MAX": 0.5, "DEFAULT": 0, "FLAGS": "hidden" }, 
		{ "LABEL": "Hue/Power", "NAME": "mat_huePower", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0, "FLAGS": "hidden" }, 
		{ "LABEL": "Hue/Pow", "NAME": "mat_huePow", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 1.8, "FLAGS": "hidden" },
		{ "LABEL": "Hue/Diff", "NAME": "mat_hueDiff", "TYPE": "float", "MIN": -2, "MAX": 2, "DEFAULT": 0, "FLAGS": "hidden" },  

		{ "LABEL": "Saturation/Min", "NAME": "mat_saturationMin", "TYPE": "float", "MIN": 0, "MAX": 0.5, "DEFAULT": 0, "FLAGS": "hidden" }, 
		{ "LABEL": "Saturation/Max", "NAME": "mat_saturationMax", "TYPE": "float", "MIN": 0, "MAX": 0.1, "DEFAULT": 0.05, "FLAGS": "hidden" }, 
		{ "LABEL": "Saturation/Power", "NAME": "mat_saturationPower", "TYPE": "float", "MIN": 0.0, "MAX": 1, "DEFAULT": 0.1, "FLAGS": "hidden" }, 
		{ "LABEL": "Saturation/Pow", "NAME": "mat_saturationPow", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 2, "FLAGS": "hidden"  }, 
 
		{ "LABEL": "Value/Min", "NAME": "mat_valueMin", "TYPE": "float", "MIN": -0.5, "MAX": 0.5, "DEFAULT": -0.27, "FLAGS": "hidden"},
		{ "LABEL": "Value/Max", "NAME": "mat_valueMax", "TYPE": "float", "MIN": -1, "MAX": 4, "DEFAULT": 1.4, "FLAGS": "hidden" },    
		{ "LABEL": "Value/Power", "NAME": "mat_valuePower", "TYPE": "float", "MIN": 0.0, "MAX": 2, "DEFAULT": 0.96, "FLAGS": "hidden" },  

		{ "LABEL": "Graphic/Tex Luminance", "NAME": "mat_textureLuminance", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1, "FLAGS": "hidden" },  
        { "LABEL": "Graphic/Background", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 0.0 ], "FLAGS": "hidden" }
    ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "link_speed_to_global_bpm":true}}
    ],
  	"IMPORTED": [
        {"NAME": "tex_wonderFace", "PATH": "wonder_face.jpg"},
		{"NAME": "tex_lut", "PATH": "lut.png"},
    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

const vec2[5] CIRCLES_OFFSET = vec2[5](vec2(0), vec2(0.05, -0.08), vec2(0.05, -0.14), vec2(0.04,-0.15), vec2(0.04,-0.15));
const vec2 GLOBAL_OFFSET = vec2(-0.05, 0);

vec4 materialColorForPixel( vec2 texCoord )
{
	// DECLARATIONS
	float t = mat_time;
	float radius = 0.8;
	vec2 center = vec2(0.5);
	vec2 offset = center + vec2(mat_offset.x, -mat_offset.y) + GLOBAL_OFFSET;
    vec2 uv = (texCoord - offset) * mat_scale;
	vec3 color = vec3(smoothstep(0.27, 1.02, length(uv )));
 
	for(int i = 1; i < mat_circlesCount + 1; i++) {
		// GENERATE NOISE
		float ridged = ridgedNoise(vec3(uv * mat_ridgedFrequency, i + t)) * mat_ridgedAmplitude;
		float fbm = (fBm(vec3(uv * mat_fbmFrequency, 3421)) * 0.5 + 0.5) * mat_fbmAmplitude;
		float noiseLength = fbm + ridged;

		// CALCULATE RADIUS SIZE
		float size = texture(mat_spectrum128, vec2(noise( vec2(t, i ) ), 0 )).r * mat_audioSensibility;
		radius *= mat_circlesScale *0.2+ 0.75;

		// CIRCLE DISTANCE FIELD
		vec2 circlesOffset = vec2(mat_offsetCircles.x * i, -mat_offsetCircles.y * i) + CIRCLES_OFFSET[i-1];
		vec2 circlesOffset2 = vec2(mat_offsetCircles.x * (i+1), -mat_offsetCircles.y * (i+1)) + CIRCLES_OFFSET[i];

		float circleDist = circle(uv, circlesOffset, radius + size * 0.1);
		float circleDistFix = circle(uv, circlesOffset, radius);

		float circleDistNoise = circleDist + noiseLength * 0.05;
		float circleDistNoise2 = circle(uv, circlesOffset2, radius + size * 0.1)+ noiseLength * 0.05;

		// COLOR CIRCLES
		float hue = smoothstep(mat_hueMin, mat_hueMax * pow(i, mat_huePow), -circleDist * mat_hueDiff) * mat_huePower;
		float saturation = smoothstep(mat_saturationMin, 
									  mat_saturationMax * pow(i, mat_saturationPow), 
									  -circleDist) * mat_saturationPower;

		float value = smoothstep(mat_valueMin + (i/mat_circlesCount), 
								 pow((mat_circlesCount - i + 3)*0.1/mat_circlesCount, mat_valueMax),
								 circleDistNoise2) * mat_valuePower;

		vec3 hsv = vec3(hue*0., saturation, value);

		vec3 circleColor = hsv2rgb(hsv) ;//+ hsv * 0.1;
		color = fill(color, circleColor, circleDistNoise);

		// ADD TEXTURE
		if(i == mat_circlesCount){
			ivec2 texSize = textureSize(tex_wonderFace,0);
			vec3 faceTexture = texture(tex_wonderFace, (uv+0.12- circlesOffset)*4).rgb;
			faceTexture = mix(faceTexture, vec3(1), step(0, circleDistFix+0.03));
			faceTexture = mix(faceTexture, color, step(0, circleDistNoise));

			color *= faceTexture;	
		}
	}

	color.r = clamp(color.r, 0., 1.);
	vec3 final_color = texture(tex_lut, vec2(color.g, 1)).rgb;

    return vec4(final_color, 1);
}
