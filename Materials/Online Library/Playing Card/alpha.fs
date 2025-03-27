/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "Playing Cards",
    "TAGS": "games",
    "VSN": "1.0",
    "INPUTS": [   
		{
            "NAME": "suit",
            "LABEL": "Suit",
            "TYPE": "long",
            "VALUES": ["Clubs","Hearts","Spades","Diamonds"],"DEFAULT": "Clubs",
            "FLAGS": "generate_as_define"
        },
		
		{
            "NAME": "card",
            "LABEL": "Card",
            "TYPE": "long",
            "VALUES": ["Ace","Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten","Jack","Queen","King"],"DEFAULT": "Ace",
            "FLAGS": "generate_as_define"
        }
     
	  ],
	  "IMPORTED": {
		"cardImage": {
			"PATH": "deckAlpha.png"
		}
	}
	  	  
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
		
vec4 materialColorForPixel( vec2 texCoord )
{
	
	float suitOffset = 0;
	float cardOffset = 0;

	#if defined(suit_IS_Clubs)
 	suitOffset = 2.98;
    
	#elif defined(suit_IS_Hearts)
	suitOffset = 1;
		
	#elif defined(suit_IS_Diamonds)
	suitOffset = 1.99;
	
	#elif defined(suit_IS_Spades)
	suitOffset = 0.02;
		
	#endif	
		
#if defined(card_IS_Ace)
 
	cardOffset = 0.03;
    #elif defined(card_IS_Two)
	cardOffset = 1.02;
		
	#elif defined(card_IS_Three)
	cardOffset = 2.02;
	
	#elif defined(card_IS_Four)
	cardOffset = 3.02;
		
    #elif defined(card_IS_Five)
	cardOffset = 4.01;
		
	#elif defined(card_IS_Six)
	cardOffset = 5.01;
	
	#elif defined(card_IS_Seven)
	cardOffset = 6.0;
    #elif defined(card_IS_Eight)
	cardOffset = 7.0;
		
	#elif defined(card_IS_Nine)
	cardOffset = 7.99;
	
	#elif defined(card_IS_Ten)
	cardOffset = 8.98;
    #elif defined(card_IS_Jack)
	cardOffset = 9.98;
		
	#elif defined(card_IS_Queen)
	cardOffset = 10.97;
	
	    #elif defined(card_IS_King)
	cardOffset = 11.97;
			
	#endif	
			
	vec4 check = IMG_NORM_PIXEL(cardImage,vec2(((texCoord.x+cardOffset)/13),(texCoord.y+suitOffset)/4));
	
	return vec4(check);
}
