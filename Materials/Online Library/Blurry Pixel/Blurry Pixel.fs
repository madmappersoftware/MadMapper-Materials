/*{
    "CREDIT": "Jean Schneider /1024",
    "DESCRIPTION": "Cellular noise.",
    "VSN": "1.0",
    "TAGS": "noise",
    "INPUTS": [  
        { "LABEL": "X pixels", "NAME": "mat_nX", "TYPE": "int", "MIN": 1, "MAX": 200.0, "DEFAULT": 8.0 },  
        { "LABEL": "Y pixels", "NAME": "mat_nY", "TYPE": "int", "MIN": 1, "MAX": 200.0, "DEFAULT": 8.0 },  
        { "LABEL": "Seed", "NAME": "mat_seed", "TYPE": "int", "MIN": 1, "MAX": 60.0, "DEFAULT": 1.0 },  
        { "LABEL": "Blur", "NAME": "mat_blurPower", "TYPE": "float", "MIN": 0.001, "MAX": 1.5, "DEFAULT": 0.6 },
   		{ "LABEL": "Gradient Pixel", "NAME": "mat_gradientPixel", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 1 },
        { "LABEL": "Pixel rate", "NAME": "mat_pixRate", "TYPE": "float", "MIN": 0, "MAX": 1, "DEFAULT": 0.3 },  
		{ "LABEL": "Brightness", "NAME": "brightness", "TYPE": "float", "MIN": 0, "MAX": 3.0, "DEFAULT": 1.7 },
		        
		{ "LABEL": "Move/Speed Z", "NAME": "zspeed", "TYPE": "float", "MIN": -10.0, "MAX": 10.0, "DEFAULT": 0.7 },  

		{ "LABEL": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
 
		{ "LABEL": "CPU/Iteration Blur", "NAME": "iterationBlur", "TYPE": "int", "MIN": 1, "MAX": 10, "DEFAULT": 3 },  

  ],
    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "zspeed"}}
    ],
    "IMPORTED": {
        "noiseLUT": { "PATH": "noiseLUT.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT" }
    }
}*/


#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

float invBlurPower = 1/(exp(pow(mat_blurPower,2))-1);
//float expo_pixRate = exp(pow(mat_pixRate,2));
float pixRate = -mat_pixRate+1;
float expo_brightness = exp((brightness-1)*3);
float gradientPix = mat_gradientPixel;

//which cells are activated
float ramp(float x){
	if(x<pixRate){return 0;}
	else{return mix(1,1/(-pixRate+1)*x+(-pixRate/(-pixRate+1)),gradientPix);}
}

float pixLight (int cellx, int celly){
	 return ramp(vnoise(vec3(float(cellx),float(celly),mat_time+mat_seed*1000)));
}

//calculate the mean distance of a pixel from the other cells
float pointHeigth(vec2 uv, int cellx, int celly){
	float meanDistance = 0;
	int x=1;
	int y=1;
	for(int x = -iterationBlur;x<=iterationBlur;x++){
		for(int y = -iterationBlur;y<=iterationBlur;y++){
			meanDistance += exp(-pow( sqrt(pow(0.5-uv[0]+x,2)+pow(0.5-uv[1]+y,2)),2)*invBlurPower)*pixLight(cellx+x,celly+y);
		}
	}
	return meanDistance;
}

vec4 materialColorForPixel( vec2 texCoord )
{	
	int cellx = int(floor(texCoord[0]*mat_nX));
	int celly = int(floor(texCoord[1]*mat_nY));
	int numberCell = cellx + celly * cellx;

	//cells division
	vec2 uv = texCoord;
    uv[0] *= mat_nX;
	uv[1] *= mat_nY;
    uv = fract(uv);

	vec3 color = vec3(pointHeigth(uv,cellx,celly))*foregroundColor.rgb*expo_brightness+backgroundColor.rgb;	

    color = color;

    return vec4(color, 1.0f );
}
