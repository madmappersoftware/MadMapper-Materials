/*{
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Material to visualize modules like weather or pollution",
    "TAGS": "weather, pollution, visualization",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Position", "NAME": "mat_position", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 

		{ "LABEL": "Gradient/Spectrum", "NAME": "mat_spectrum", "TYPE": "long", "VALUES": ["Custom gradient", "Visible wavelength", "Temperature", "Sun", "Rayleigh scattering"], "DEFAULT": "Custom gradient", "FLAGS": "generate_as_define" },
 		{ "LABEL": "Gradient/Custom start", "NAME": "mat_custom_start", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
		{ "LABEL": "Gradient/Custom end", "NAME": "mat_custom_end", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
   	 "IMPORTED": [
		{"NAME": "tex_visible_wavelength", "PATH": "visibleWavelength.png", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
		{"NAME": "tex_temperature", "PATH": "temperature.png", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
    	{"NAME": "tex_sun", "PATH": "sun.png", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
		{"NAME": "tex_rayleigh_scattering", "PATH": "rayleighScattering.png", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
    ]
}*/

// Ressources function : https://thebookofshaders.com/05/kynd.png

#include "MadSDF.glsl"
#include "MadCommon.glsl"

float plot(vec2 st, float pct, float w){
  return smoothstep( pct-w*.5, pct, st.y) - smoothstep( pct, pct+w*.5, st.y);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float t = mat_time;
	float thickness = 0.005;
	vec2 uv = texCoord;
	vec3 black = vec3(0.);

	// SPECTRUM COLORS
	vec3 solidColor = vec3(0.);
	vec3 gradientColor = vec3(0.);

    #if defined(mat_spectrum_IS_Visible_wavelength)
		solidColor = texture(tex_visible_wavelength, vec2(mat_position, 0.)).rgb;
		gradientColor = texture(tex_visible_wavelength, uv).rgb;
    #elif defined(mat_spectrum_IS_Temperature)
		solidColor = texture(tex_temperature, vec2(mat_position, 0.)).rgb;
		gradientColor = texture(tex_temperature, uv).rgb;
	 #elif defined(mat_spectrum_IS_Sun)
		solidColor = texture(tex_sun, vec2(mat_position, 0.)).rgb;
		gradientColor = texture(tex_sun, uv).rgb;
	 #elif defined(mat_spectrum_IS_Rayleigh_scattering)
		solidColor = texture(tex_rayleigh_scattering, vec2(mat_position, 0.)).rgb;
		gradientColor = texture(tex_rayleigh_scattering, uv).rgb;
	#else
		solidColor = mix(mat_custom_start, mat_custom_end, mat_position).rgb;
		gradientColor = mix(mat_custom_start, mat_custom_end, uv.x).rgb;
    #endif

	// SOLID
	float solidSDF = rectangle(uv, vec2(0., 0.125), vec2(1., 0.125));
	vec3 solid = fill(black, solidColor, solidSDF);

	// GRADIENT
	float gradientSDF = rectangle(uv, vec2(0., 0.25 + 0.125),vec2(1., 0.125));
	vec3 gradient = fill(black, gradientColor, gradientSDF);

	// GRADIENT LINE
	vec2 gradientLinePos = vec2(uv.x-mat_position, uv.y);
	float gradientLineSDF = line(gradientLinePos, vec2(0., 0.25+thickness*.5), vec2(0., 0.5-thickness*.5), thickness);
	vec3 gradientLine = fill(black, vec3(1.), gradientLineSDF);

	// FUNCTION LINE
	float y = 1. - pow(cos(PI * uv.x - PI*.5), 2.) * .48;
	float functionLine = plot(uv, y, thickness);

	// FUNCTION CIRCLE
	vec2 functionCirclePos = vec2(mat_position,  1. - pow(cos(PI * mat_position - PI*.5), 2.) * .48);
	float functionCircleSDF = circle(uv, functionCirclePos, 0.02);
	vec3 functionCircle = fill(black, vec3(1.), functionCircleSDF);

	// FINAL RESULT
	vec3 col = solid + gradient + gradientLine + functionLine + functionCircle;

	return vec4(col, 1.);
}