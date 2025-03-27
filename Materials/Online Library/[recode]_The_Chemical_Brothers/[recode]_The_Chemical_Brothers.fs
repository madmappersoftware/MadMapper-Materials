/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nTristan Le Moigne",
    "DESCRIPTION": "Inspired by The Chemical Brothers - Dig Your Own Hole/ 1997",
    "TAGS": "album cover",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Global/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.2, "MAX": 2.0, "DEFAULT": 1.4 },
		{ "LABEL": "Global/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 0.25,  0.25 ], "MIN": [ -0.25, -0.25 ], "DEFAULT": [ 0.0, -0.08 ]},

		{ "LABEL": "Circles/Count", "NAME": "mat_circlesCount", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 6 }, 
		{ "LABEL": "Circles/Offset", "NAME": "mat_circlesOffset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Circles/Inverse", "NAME": "mat_circlesInverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }, 

		{ "LABEL": "Rotation/Offset", "NAME": "mat_rotationOffset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Rotation/Speed", "NAME": "mat_rotationSpeed", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },

		{ "LABEL": "Noise/Speed", "NAME": "mat_noiseSpeed", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Noise/Offset", "NAME": "mat_noiseOffset", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Noise/Inscribed", "NAME": "mat_noiseInscribed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },

		{ "LABEL": "Tunnel/Speed", "NAME": "mat_tunnelSpeed", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },

		{ "LABEL": "Graphic/Filled", "NAME": "mat_graphicFilled", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 1 }, 
		{ "LABEL": "Graphic/Switch off", "NAME": "mat_graphicSwitchOff", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" }, 
    ],
	"GENERATORS": [
        { "NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
		{ "NAME": "mat_rotation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_rotationSpeed"} },
		{ "NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noiseSpeed"} },
		{ "NAME": "mat_tunnel_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_tunnelSpeed"} },
    ],
}*/

#include "MadCommon.glsl"
#include "MadSDF.glsl"
#include "MadNoise.glsl"

const float strokeWeight = 0.005;
const vec2 center = vec2(0.5);
const vec3 background = vec3(0.);
const vec3 strokeColor = vec3(1.);

vec4 materialColorForPixel( vec2 texCoord )
{
	float radius = 0.5;
	float t = mat_time * 10.;
	float tRotation = mat_rotation_time * 10.;
	float tNoise = mat_noise_time * 10.;
	float tTunnel = mat_tunnel_time * 10.;
	float fixedRadius = radius/mat_circlesCount;
	float startRotation = PI * 0.5 + tRotation;

	vec2 uv = (texCoord - center - vec2(mat_offset.x, -mat_offset.y)) * mat_scale;
	vec2 rotationOffset = vec2(0);

	vec3 color = background;

	for(int i=0; i < mat_circlesCount; i++)
	{
		// Distance fields : Draw them from big to little
		float circleDist = circle(uv, rotationOffset, radius);
		
		// Generate tunnel
		vec2 noiseOffset = vec2(noise(vec2(i, tNoise)), noise(vec2(i + 2.35711, tNoise)));
		noiseOffset = mix(noiseOffset, 
						  vec2(noiseOffset.x * 0.5 - 0.5, noiseOffset.y * 0.5 - 0.5), 
						  mat_noiseInscribed);

		noiseOffset *= mat_noiseOffset * 2;
	
		startRotation = mix(1, -1, step(0.5, float(mat_circlesInverse))) * startRotation;
		float circlesRotation = startRotation + (i+1) * mat_rotationOffset * 2. * PI ;

		if(i != mat_circlesCount-1)
		{
			vec2 nextOffset = rotationOffset + mix(vec2(0), vec2( cos(circlesRotation), sin(circlesRotation)) * fixedRadius, mat_circlesOffset);
			float nextRadius = fixedRadius*(mat_circlesCount-i-1);

			float startDist = circle(uv, rotationOffset, radius);
			float endDist = circle(uv, nextOffset, nextRadius);
			
			circleDist = mix(startDist, endDist, fract(tTunnel));
		}
	
		// Color fields
		vec3 fillCircle = fill(color, strokeColor, circleDist);
		vec3 strokeCircle = stroke(color, strokeColor, circleDist, strokeWeight);

		float conditionIsFilledAndIsSwitch = mix(step(mat_circlesCount - mat_graphicFilled, float(i)), 
												 0, 
												 step(0.5, float(mat_graphicSwitchOff)));

		color = mix(strokeCircle, fillCircle, conditionIsFilledAndIsSwitch);
		
		// Update offset & radius
		rotationOffset += mix(vec2(0), 
							  vec2(cos(circlesRotation), sin(circlesRotation)) * fixedRadius, 
							  mat_circlesOffset + noiseOffset);

		radius -= fixedRadius;
	}

  	color = stroke(color, strokeColor, circle(uv, vec2(0), 0.5), strokeWeight);

	return vec4(color, 1.);
}