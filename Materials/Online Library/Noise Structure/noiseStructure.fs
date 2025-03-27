/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Mirrored Noise with transparency",
    "TAGS": "graphic",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "BG Alpha", "NAME": "alpha", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 2.0 },  
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 5.0},  
 		{ "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": .5 },  
		{ "LABEL": "Noise type", "NAME": "noiseType", "TYPE": "long","VALUES": ["Worley Noise","fBm","Ridged Noise","Billowy Turbulence","Billowed Noise","Simple"],"DEFAULT": "Billowed Noise","FLAGS": "generate_as_define"},		
	 ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
	
	float n;
	
	vec3 uv = vec3(abs(texCoord-.5),(TIME*speed))*scale;
		
	
	#if defined(noiseType_IS_Worley_Noise)
 		n = worleyNoise( uv );
    
	#elif defined(noiseType_IS_fBm)
		n = fBm( uv );
		
	#elif defined(noiseType_IS_Ridged_Noise)
		n = ridgedNoise( uv );
	
	#elif defined(noiseType_IS_Billowy_Turbulence)
		n = billowyTurbulence( uv );
	
	#elif defined(noiseType_IS_Simple)	
		n = noise( uv );
	
	#elif defined(noiseType_IS_Billowed_Noise)
		n = billowedNoise( uv );
	
	#endif	
		
	return vec4(n,n,n,n+alpha);
}
