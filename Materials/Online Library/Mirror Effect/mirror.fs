/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Mirror Effect",
    "TAGS": "effect",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Source", "NAME": "cam", "TYPE": "image"},  
		{ "LABEL": "Zoom", "NAME": "zoom", "TYPE": "float", "MIN": -25.0, "MAX": 25.0, "DEFAULT": 2.0 },  	
   		{ "LABEL": "Mode", "NAME": "mode", "TYPE": "long", "VALUES": ["Norm", "Quad", "Horizontal","Verticle","Quad Morph"],"DEFAULT": "Quad", "FLAGS": "generate_as_define" },
		{ "LABEL": "Invert", "NAME": "invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
   ],
}*/
	

vec4 materialColorForPixel( vec2 texCoord )
{		

	
	#if defined(mode_IS_Norm)
		if (invert){
			return vec4(IMG_NORM_PIXEL(cam,((texCoord - .5) *zoom)));	
		}else{			
			return vec4(IMG_NORM_PIXEL(cam,(-(texCoord - .5) *zoom)));	
		}

	
	#elif defined(mode_IS_Quad)
		
		if (invert){
			return vec4(IMG_NORM_PIXEL(cam,vec2(abs(texCoord - .5)*zoom)));				
		}else{			
			return vec4(IMG_NORM_PIXEL(cam,vec2(-abs(texCoord - .5)*zoom)));	
		}
			
	#elif defined(mode_IS_Quad_Morph)
		
		if (invert){
			return vec4(IMG_NORM_PIXEL(cam,vec2(cos(texCoord - .5)*(zoom*2))));	
		}else{
			return vec4(IMG_NORM_PIXEL(cam,vec2(-cos(texCoord - .5)*(zoom*2))));
		}
	
		
    #elif defined(mode_IS_Horizontal)		
		if (invert){
			return vec4(IMG_NORM_PIXEL(cam,vec2(abs(texCoord.x - 0),abs(texCoord.y - 0.5) *zoom)));		
		}else{
			return vec4(IMG_NORM_PIXEL(cam,vec2(abs(texCoord.x - 0),-abs(texCoord.y - 0.5) *zoom)));	
		}
	
	
	#elif defined(mode_IS_Verticle)		 
		if (invert){
			return vec4(IMG_NORM_PIXEL(cam,vec2(abs(texCoord.x - 0.5),abs(texCoord.y - 0) *(zoom*0.5))));
		}else{
			return vec4(IMG_NORM_PIXEL(cam,vec2(-abs(texCoord.x - 0.5),abs(texCoord.y - 0) *(zoom*0.5))));
		}
		
	#endif
		
	return vec4(IMG_NORM_PIXEL(cam,(texCoord)));
		
}
