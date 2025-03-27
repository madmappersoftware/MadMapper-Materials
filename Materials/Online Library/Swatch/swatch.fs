/*{
    "CREDIT": "Joe Griffith",
    "DESCRIPTION": "All your favorite Colors of Gel",
    "TAGS": "Theatrical",
    "VSN": "1.0",
    "INPUTS": [ 
  		
		{
            "NAME": "lightSource",
            "LABEL": "Light Source",
            "TYPE": "long",
            "VALUES": [
"40W Tungsten 2600k",
"100W Tungsten  2850k",
"575W HPL 3200k",
"Carbon Arc 5200k",
"Direct Sunlight 6000k",
"Mercury Vapor","Sodium Vapor",
"Metal Halide",
"High Pressure Sodium",
"Warm Flourescent",
"Standard Flourescent",
"Cool White Flourescent",
"Full Spectrum Flourescent",
"Black Light"],

"DEFAULT": "575W HPL 3200k",
            "FLAGS": "generate_as_define"
        }, 
				
		{
            "NAME": "Gel",
            "LABEL": "Gel",
            "TYPE": "long",
            "VALUES": [
"R01 Light Bastard Amber",
"R02 Bastard Amber",
"R03 Dark Bastard Amber",
"R04 Medium Bastard Amber",
"R304 Pale Apricot",
"R05 Rose Tint",
"R305 Rose Gold",
"R06 No Color Straw",
"R07 Pale Yellow",
"R08 Pale Gold",
"R09 Pale Amber Gold",
"R10 Medium Yellow",
"R11 Light Straw",
"R12 Straw",
"R312 Canary",
"R13 Straw Tint","R14 Medium Straw",
"R15 Deep Straw",
"R16 Light Amber",
"R17 Light Flame",
"R317 Apricot",
"R18 Flame",
"R19 Fire",
"R20 Medium Amber",
"R21 Golden Amber",
"R321 Soft Golden Amber",
"R22 Deep Amber",
"R23 Orange",
"R24 Scarlet",
"R25 Orange Red",
"R26 Light Red",
"R27 Medium Red",
"R30 Light Salmon Pink",
"R31 Salmon Pink",
"R32 Medium Salmon Pink",
"R33 No Color Pink",
"R34 Flesh Pink",
"R35 Light Pink",
"R36 Medium Pink",
"R37 Pale Rose Pink",
"R337 True Pink",
"R38 Light Rose",
"R339 Broadway Pink",
"R40 Light Salmon",
"R41 Salmon",
"R42 Deep Salmon",
"R342 Rose Pink",
"R43 Deep Pink",
"R44 Middle Rose",
"R344 Follies Pink",
"R45 Rose",
"R46 Magenta",
"R47 Light Rose Purple",
"R48 Rose Purple",
"R49 Medium Purple",
"R50 Mauve",
"R51 Surprise Pink",
"R52 Light Lavender",
"R53 Pale Lavender",
"R54 Special Lavender",
"R55 Lilac",
"R355 Pale Violet",
"R56 Gypsy Lavender",
"R356 Middle Lavender",
"R57 Lavender",
"R357 Royal Lavender",
"R58 Deep Lavender",
"R358 Rose Indigo",
"R59 Indigo",
"R359 Medium Violet",
"R60 No Color Blue",
"R61 Mist Blue",
"R62 Booster Blue",
"R63 Pale Blue",
"R64 Light Steel Blue",
"R65 Daylight Blue",
"R66 Cool Blue",
"R67 Light Sky Blue",
"R68 Sky Blue",
"R69 Brilliant Blue",
"R70 Nile Blue",
"R71 Sea Blue",
"R72 Azure Blue",
"R73 Peacock Blue",
"R74 Night Blue",
"R76 Light Green Blue",
"R77 Green Blue",
"R78 Trudy Blue",
"R378 Alice Blue",
"R79 Bright Blue",
"R80 Primary Blue",
"R81 Urban Blue",
"R82 Surprise Blue",
"R83 Medium Blue",
"R383 Sapphire Blue",
"R84 Zephyr Blue",
"R85 Deep Blue",
"R385 Royal Blue",
"R86 Pea Green",
"R87 Pale Yellow Green",
"R88 Light Green",
"R388 Gaslight Green",
"R89 Moss Green",
"R389 Chroma Green",
"R90 Dark Yellow Green",
"R91 Primary Green",
"R92 Turquoise",
"R93 Blue Green",
"R94 Kelly Green",
"R95 Medium Blue Green",
"R96 Lime",
"R97 Light Grey",
"R397 Pale Grey",
"R98 Medium Grey",
"R99 Chocolate",
"L02 Rose Pink",
"L03 Lavender Tint",
"L04 Medium Bastard Amber",
"L07 Pale Yellow",
"L08 Dark Salmon",
"L09 Pale Amber Gold",
"L10 Medium Yellow",
"L13 Straw Tint",
"L15 Deep Straw",
"L19 Fire",
"L20 Medium Amber",
"L21 Gold Amber",
"L22 Dark Amber",
"L24 Scarlet",
"L26 Bright Red",
"L27 Medium Red",
"L35 Light Pink",
"L36 Mesium Pink",
"L46 Dark Magenta",
"L48 Rose Purple",
"L52 Light Lavender",
"L53 Paler Lavender",
"L58 Lavender",
"L61 Mist Blue",
"L63 Pale Blue",
"L68 Sky Blue",
"L79 Just Blue",
"L85 Deeper Blue",
"L89 Moss Green",
"L90 Dark Yellow Green",
"L101 Yellow",
"L102 Light Amber",
"L103 Straw",
"L104 Deep Amber",
"L105 Orange",
"L106 Primary Red",
"L107 Light Rose",
"L109 Light Salman",
"L110 Middle Rose",
"L111 Dark Pink",
"L113 Magenta",
"L115 Peacock Blue",
"L116 Medium Blue Green",
"L117 Steel Blue",
"L118 Light Blue",
"L119 Dark Blue",
"L120 Deep Blue",
"L121 Lee Green",
"L122 Fern Green",
"L124 Dark Green",
"L126 Mauve",
"L127 Smokey Pink",
"L128 Bright Pink",
"L129 Heavy Frost",
"L130 Clear",
"L132 Medium Blue",
"L134 Golden Amber",
"L135 Deep Golden Amber",
"L136 Pale Lavender",
"L137 Special Lavender",
"L138 Pale Green",
"L139 Primary Green",
"L141 Bright Blue",
"L142 Pale Violet",
"L143 Pale Navy Blue",
"L144 No Colour Blue",
"L147 Apricot",
"L148 Bright Rose",
"L151 Gold Tint",
"L152 Pale Gold",
"L153 Pale Salmon",
"L154 Pale Rose",
"L156 Chocolate",
"L157 Pink",
"L158 Deep Orange",
"L159 No Color Straw",
"L161 Slate Blue",
"L162 Bastard Amber",
"L164 Flame Red",
"L165 Daylight Blue",
"L166 Pale Red",
"L170 Deep Lavender",
"L174 Dark Steel Blue",
"L176 Loving Amber",
"L179 Chrome Orange",
"L180 Dark Lavender",
"L181 Congo Blue",
"L182 Light Red",
"L183 Moonlight Blue",
"L184 Cosmetic Peach",
"L185 Cosmetic Burgundy",
"L186 Cosmetic Silver Rose",
"L187 Cosmetic Rouge",
"L188 Cosmetic Highlight",
"L189 Cosmetic Silver Moss",
"L190 Cosmetic Emerald",
"L191 Cosmetic Aqua Blue",
"L192 Flesh Pink",
"L193 Rosy Gold",
"L194 Surprise Pink",
"L195 Zenith Blue",
"L196 True Blue",
"L197 Alice Blue",
"L328 Follies Pink",
"L332 Special Rose Pink",
"L343 Special Medium Lavender",
"L344 Violet",
"L353 Lighter Blue",
"L354 Special Steel Blue",
"L363 Special Medium Blue",
"G105 Antique Rose",
"G110 Dark Rose",
"G120 Bright Pink",
"G130 Rose",
"G140 Dark Magenta",
"G150 Pink Punch",
"G155 Light Pink",
"G160 Chorus Pink",
"G170 Flesh Pink",
"G180 Cherry",
"G190 Cold Pink",
"G195 Nymph Pink",
"G220 Pink Magenta",
"G235 Pink Red",
"G245 Light Red",
"G250 Medium Red XT",
"G260 Rosy Amber",
"G270 Red Orange",
"G280 Fire Red",
"G290 Fire Orange",
"G305 French Rose",
"G315 Autumn Glory",
"G320 Peach",
"G323 Indian Summer",
"G325 Bastard Amber",
"G330 Sepia",
"G335 Coral",
"G340 Light Bastard Amber",
"G343 Honey",
"G345 Deep Amber",
"G350 Dark Amber",
"G360 Amber Blush",
"G363 Sand",
"G364 Pale Honey",
"G365 Warm Straw",
"G370 Spice",
"G375 Flame",
"G380 Golden Tan",
"G385 Light Amber",
"G390 Walnut",
"G420 Medium Amber",
"G440 Very Light Straw",
"G450 Saffron",
"G460 Mellow Yellow",
"G470 Pale Gold",
"G480 Medium Yellow",
"G510 No Color Straw",
"G520 New Straw",
"G535 Lime",
"G540 Pale Green",
"G570 Light Green Yellow",
"G650 Grass Green",
"G655 Rich Green",
"G660 Medium Green",
"G680 Kelly Green",
"G685 Pistachio",
"G690 Bluegrass",
"G710 Blue Green",
"G720 Light Steel Blue",
"G725 Princess Blue",
"G730 Azure Blue",
"G740 Off Blue",
"G750 Nile Blue",
"G760 Aqua Blue",
"G770 Christel Blue",
"G780 Shark Blue",
"G790 Electric Blue",
"G810 Moon Blue",
"G815 Moody Blue",
"G820 Full Light Blue",
"G830 North Sky Blue",
"G835 Aztec Blue",
"G840 Steel Blue",
"G842 Whisper Blue",
"G845 Cobalt",
"G847 City Blue",
"G848 Bonus Blue",
"G850 Blue Primary",
"G860 Sky Blue",
"G870 Winter Blue",
"G880 Daylight Blue",
"G882 Southern Sky",
"G885 Blue Ice",
"G888 Blue Belle",
"G890 Dark Sky Blue",
"G905 Dark Blue",
"G910 Alice Blue",
"G915 Twilight",
"G920 Pale Lavender",
"G925 Cosmic Blue",
"G930 Real Congo Blue",
"G940 Light Purple",
"G945 Royal Purple",
"G948 African Violet",
"G950 Purple",
"G960 Medium Lavender",
"G970 Special Lavender",
"G980 Surprise Pink",
"G990 Dark Lavender",
"G995 Orchid"],


"DEFAULT": "R80 Primary Blue",
            "FLAGS": "generate_as_define"
        },  
		
		
		{
            "NAME": "mode",
            "LABEL": "Mode",
            "TYPE": "long",
            "VALUES": [
"Transparent",
"Solid",
],

"DEFAULT": "Solid",
            "FLAGS": "generate_as_define"
        }, 
      ],
}*/


//RGB Color Values for Gel adapted from http://www.derekleffew.com/referencedocumentsandwebsites

//Light Source Correction Values adapted from http://planetpixelemporium.com/tutorialpages/light.html

#include "MadCommon.glsl"
#include "MadNoise.glsl"

vec4 materialColorForPixel( vec2 texCoord )
{
float red = 1;
float green = 1;
float blue = 1;
float transmission = 1;
vec3 color;	
	
	color = vec3(1,1,1);
	
	
    #if defined(Gel_IS_R01_Light_Bastard_Amber)
    red = 1;
	green = .78;
	blue = .65;
	transmission = .44;
	
    #elif defined(Gel_IS_R02_Bastard_Amber)
	red = 1;
	green = 0.75;
	blue = .63;
	transmission = .28;

	#elif defined(Gel_IS_R03_Dark_Bastard_Amber)
	red = 1;
	green = 0.62;
	blue = .47;
	transmission = .38;

    #elif defined(Gel_IS_R04_Medium_Bastard_Amber)
	red = .99;
	green = 0.62;
	blue = .49;
	transmission = .44;
	
	#elif defined(Gel_IS_R05_Rose_Tint)
	red = 1;
	green = 0.75;
	blue = .82;
	transmission = .2;
	
	#elif defined(Gel_IS_R06_No_Color_Straw)
	red = .99;
	green = 1;
	blue = .84;
	transmission = .08;

	#elif defined(Gel_IS_R07_Pale_Yellow)
	red = 1;
	green = 1;
	blue = .69;
	transmission = .04;

	
	#elif defined(Gel_IS_R08_Pale_Gold)
	red = .99;
	green = 0.72;
	blue = .31;
	transmission = .14;

	
	#elif defined(Gel_IS_R09_Pale_Amber_Gold)
	red = .99;
	green = 0.72;
	blue = .31;
	transmission = .26;
	
	#elif defined(Gel_IS_R10_Medium_Yellow)
	red = 1;
	green = 1;
	blue = .13;
	transmission = .08;
	
	#elif defined(Gel_IS_R11_Light_Straw)
	red = .99;
	green = 0.8;
	blue = .14;
	transmission = .18;
	
	#elif defined(Gel_IS_R12_Straw)
	red = .94;
	green = 1;
	blue = 0;
	transmission = .12;
	
	
	#elif defined(Gel_IS_R13_Straw_Tint)
	red = 1;
	green = 0.78;
	blue = .45;
	transmission = .22;
	
	#elif defined(Gel_IS_R14_Medium_Straw)
	red = 1;
	green = 0.67;
	blue = .19;
	transmission = .32;
	
	#elif defined(Gel_IS_R15_Deep_Straw)
	red = .97;
	green = 0.58;
	blue = 0;
	transmission = .35;
	
	#elif defined(Gel_IS_R16_Light_Amber)
	red = 1;
	green = 0.38;
	blue = 0;
	transmission = .32;
	
	#elif defined(Gel_IS_R17_Light_Flame)
	red = 0.94;
	green = 0.25;
	blue = 0.13;
	transmission = 0.44;
	
	#elif defined(Gel_IS_R18_Flame)
	red = .94;
	green = 0.06;
	blue = 0;
	transmission = 0.44;
	
	
	#elif defined(Gel_IS_R19_Fire)
	red = 1;
	green = 0.2;
	blue = .2;
	transmission = .80;
	
	#elif defined(Gel_IS_R20_Medium_Amber)
	red = .99;
	green = 0.55;
	blue = .26;
	transmission = .46;
	
	#elif defined(Gel_IS_R21_Golden_Amber)
	red = .93;
	green = 0.38;
	blue = .01;
	transmission = .57;

	#elif defined(Gel_IS_R22_Deep_Amber)
	red = .99;
	green = 0.25;
	blue = .19;
	transmission = .74;

	#elif defined(Gel_IS_R23_Orange)
	red = .99;
	green = 0.42;
	blue = .26;
	transmission = .68;

	#elif defined(Gel_IS_R24_Scarlet)
	red = .75;
	green = 0;
	blue = 0;
	transmission = .78;

	#elif defined(Gel_IS_R25_Orange_Red)
	red = 1;
	green = 0;
	blue = 0;
	transmission = .86;

	#elif defined(Gel_IS_R26_Light_Red)
	red = .62;
	green = 0;
	blue = 0;
	transmission = .84;

	#elif defined(Gel_IS_R27_Medium_Red)
	red = .38;
	green = 0;
	blue = 0;
	transmission = .96;

	#elif defined(Gel_IS_R30_Light_Salmon_Pink)
	red = .1;
	green = 0.4;
	blue = .35;
	transmission = .56;

	#elif defined(Gel_IS_R31_Salmon_Pink)
	red = .1;
	green = 0.39;
	blue = .53;
	transmission = .54;

	#elif defined(Gel_IS_R32_Medium_Salmon_Pink)
	red = .99;
	green = 0;
	blue = .22;
	transmission = .72;
	
	#elif defined(Gel_IS_R33_No_Color_Pink)
	red = 1;
	green = 0.5;
	blue = .63;
	transmission = .35;

	#elif defined(Gel_IS_R34_Flesh_Pink)
	red = 1;
	green = 0.13;
	blue = .25;
	transmission = .55;

	#elif defined(Gel_IS_R35_Light_Pink)
	red = 1;
	green = 0.64;
	blue = .69;
	transmission = .34;

	#elif defined(Gel_IS_R36_Medium_Pink)
	red = .91;
	green = 0.36;
	blue = .54;
	transmission = .54;

	#elif defined(Gel_IS_R37_Pale_Rose_Pink)
	red = 1;
	green = 0.5;
	blue = .69;
	transmission = .44;
	
	
	#elif defined(Gel_IS_R38_Light_Rose)
	red = 1;
	green = 0.38;
	blue = .69;
	transmission = .51;

	#elif defined(Gel_IS_R40_Light_Salmon)
	red = 1;
	green = 0;
	blue = 0;
	transmission = .66;

	#elif defined(Gel_IS_R41_Salmon)
	red = .94;
	green = 0;
	blue = .6;
	transmission = .76;

	#elif defined(Gel_IS_R42_Deep_Salmon)
	red = .56;
	green = 0;
	blue = 0;
	transmission = .92;

	#elif defined(Gel_IS_R43_Deep_Pink)
	red = 1;
	green = 0;
	blue = .44;
	transmission = .72;

	#elif defined(Gel_IS_R44_Middle_Rose)
	red = .87;
	green = 0;
	blue = .44;
	transmission = .74;

	#elif defined(Gel_IS_R45_Rose)
	red = .56;
	green = 0;
	blue = .13;
	transmission = .92;

	#elif defined(Gel_IS_R46_Magenta)
	red = .38;
	green = 0;
	blue = 0;
	transmission = .84;

	#elif defined(Gel_IS_R47_Light_Rose_Purple)
	red = .25;
	green = 0;
	blue = .25;
	transmission = .84;

	#elif defined(Gel_IS_R48_Rose_Purple)
	red = .72;
	green = 0.01;
	blue = .72;
	transmission = .84;

	#elif defined(Gel_IS_R49_Medium_Purple)
	red = .44;
	green = 0;
	blue = .31;
	transmission = .96;

	#elif defined(Gel_IS_R50_Mauve)
	red = .44;
	green = 0;
	blue = 0;
	transmission = .86;

	#elif defined(Gel_IS_R51_Surprise_Pink)
	red = 1;
	green = 0.5;
	blue = 1;
	transmission = .46;

	#elif defined(Gel_IS_R52_Light_Lavender)
	red = .69;
	green = 0.6;
	blue = .94;
	transmission = .74;

	#elif defined(Gel_IS_R53_Pale_Lavender)
	red = .75;
	green = 0.75;
	blue = 1;
	transmission = .36;

	#elif defined(Gel_IS_R54_Special_Lavender)
	red = .75;
	green = 0.63;
	blue = 1;
	transmission = .5;

	#elif defined(Gel_IS_R55_Lilac)
	red = .43;
	green = 0.31;
	blue = 1;
	transmission = .37;

	#elif defined(Gel_IS_R56_Gypsy_Lavender)
	red = .25;
	green = 0;
	blue = .5;
	transmission = .96;

	#elif defined(Gel_IS_R57_Lavender)
	red = .25;
	green = 0;
	blue = .5;
	transmission = .76;

	#elif defined(Gel_IS_R58_Deep_Lavender)
	red = .25;
	green = 0;
	blue = .5;
	transmission = .90;

	#elif defined(Gel_IS_R59_Indigo)
	red = .19;
	green = 0;
	blue = .31;
	transmission = .98;

	#elif defined(Gel_IS_R60_No_Color_Blue)
	red = .5;
	green = 0.75;
	blue = 1;
	transmission = .38;

	#elif defined(Gel_IS_R61_Mist_Blue)
	red = .31;
	green = 0.75;
	blue = .82;
	transmission = .34;

	#elif defined(Gel_IS_R62_Booster_Blue)
	red = .31;
	green = 0.56;
	blue = .75;
	transmission = .66;

	#elif defined(Gel_IS_R63_Pale_Blue)
	red = .06;
	green = 0.56;
	blue = .75;
	transmission = .44;

	#elif defined(Gel_IS_R64_Light_Steel_Blue)
	red = 0;
	green = 0.31;
	blue = .94;
	transmission = .74;

	#elif defined(Gel_IS_R65_Daylight_Blue)
	red = 0;
	green = 0.25;
	blue = 1;
	transmission = .65;

	#elif defined(Gel_IS_R66_Cool_Blue)
	red = 0;
	green = 0.75;
	blue = .75;
	transmission = .33;

	#elif defined(Gel_IS_R67_Light_Sky_Blue)
	red = 0;
	green = 0.38;
	blue = .1;
	transmission = .74;

	#elif defined(Gel_IS_R68_Sky_Blue)
	red = 0;
	green = 0;
	blue = .82;
	transmission = .86;

	#elif defined(Gel_IS_R69_Brilliant_Blue)
	red = 0;
	green = 0.25;
	blue = .87;
	transmission = .82;

	#elif defined(Gel_IS_R70_Nile_Blue)
	red = .06;
	green = 0.63;
	blue = .69;
	transmission = .55;

	#elif defined(Gel_IS_R71_Sea_Blue)
	red = 0;
	green = 0.5;
	blue = .56;
	transmission = .7;

	#elif defined(Gel_IS_R72_Azure_Blue)
	red = 0;
	green = 0.63;
	blue = .81;
	transmission = .56;

	#elif defined(Gel_IS_R73_Peacock_Blue)
	red = .06;
	green = 0.56;
	blue = .56;
	transmission = .72;

	#elif defined(Gel_IS_R74_Night_Blue)
	red = 0;
	green =0;
	blue = .69;
	transmission = .96;

	#elif defined(Gel_IS_R76_Light_Green_Blue)
	red = 0;
	green = 0.5;
	blue = .56;
	transmission = .91;

	#elif defined(Gel_IS_R77_Green_Blue)
	red = 0;
	green = 0.5;
	blue = .87;
	transmission = .91;

	#elif defined(Gel_IS_R78_Trudy_Blue)
	red = .13;
	green = 0.6;
	blue = .69;
	transmission = .81;

	#elif defined(Gel_IS_R79_Bright_Blue)
	red = 0;
	green = 0;
	blue = .82;
	transmission = .92;

	#elif defined(Gel_IS_R80_Primary_Blue)
	red = 0;
	green = 0;
	blue = .5;
	transmission = .91;

	#elif defined(Gel_IS_R81_Urban_Blue)
	red = .14;
	green = 0;
	blue = .79;
	transmission = .90;

	#elif defined(Gel_IS_R82_Surprise_Blue)
	red = .6;
	green = 0;
	blue = .44;
	transmission = .94;

	#elif defined(Gel_IS_R83_Medium_Blue)
	red = 0;
	green = 0;
	blue = .38;
	transmission = .96;

	#elif defined(Gel_IS_R84_Zephyr_Blue)
	red = .12;
	green = 0;
	blue = .64;
	transmission = .86;

	#elif defined(Gel_IS_R85_Deep_Blue)
	red = 0;
	green = 0;
	blue = .38;
	transmission = .97;

	#elif defined(Gel_IS_R86_Pea_Green)
	red = 0;
	green = 0.56;
	blue = 0;
	transmission = .44;

	#elif defined(Gel_IS_R87_Pale_Yellow_Green)
	red = .75;
	green = 1;
	blue = .5;
	transmission = .15;

	#elif defined(Gel_IS_R89_Moss_Green)
	red = 0;
	green = .48;
	blue = 0;
	transmission = .55;

	#elif defined(Gel_IS_R90_Dark_Yellow_Green)
	red = .06;
	green = 0.37;
	blue = 0;
	transmission = .87;

	#elif defined(Gel_IS_R91_Primary_Green)
	red = 0;
	green = 0.25;
	blue = 0;
	transmission = 93;

	#elif defined(Gel_IS_R92_Turquoise)
	red = 0;
	green = 0.75;
	blue = .5;
	transmission = .41;

	#elif defined(Gel_IS_R93_Blue_Green)
	red = 0;
	green = 0.69;
	blue = .38;
	transmission = .65;

	#elif defined(Gel_IS_R94_Kelly_Green)
	red = .16;
	green = 0.53;
	blue = .41;
	transmission = .75;

	#elif defined(Gel_IS_R95_Medium_Blue_Green)
	red = 0;
	green = 0.38;
	blue = .31;
	transmission = .85;

	#elif defined(Gel_IS_R96_Lime)
	red = .75;
	green = 1;
	blue = 0;
	transmission = 2;

	#elif defined(Gel_IS_R97_Light_Grey)
	red = .75;
	green = 0.75;
	blue = .75;
	transmission = .5;

	#elif defined(Gel_IS_R98_Medium_Grey)
	red = .44;
	green = 0.44;
	blue = .44;
	transmission = .25;

	#elif defined(Gel_IS_R99_Chocolate)
	red = .56;
	green = 0.44;
	blue = .38;
	transmission = .65;

	#elif defined(Gel_IS_R304_Pale_Apricot)
	red = 1;
	green = 0.63;
	blue = .5;
	transmission = .21;

	#elif defined(Gel_IS_R305_Rose_Gold)
	red = 1;
	green = 0.5;
	blue = .38;
	transmission = .25;

	#elif defined(Gel_IS_R312_Canary)
	red = .99;
	green = 0.87;
	blue = .35;
	transmission = .15;

	#elif defined(Gel_IS_R317_Apricot)
	red = .94;
	green = 0.06;
	blue = 0;
	transmission = .49;

	#elif defined(Gel_IS_R321_Soft_Golden_Amber)
	red = .87;
	green = 0.13;
	blue = 0;
	transmission = .61;

	#elif defined(Gel_IS_R337_True_Pink)
	red = 1;
	green = 0.54;
	blue = .77;
	transmission = .45;

	#elif defined(Gel_IS_R339_Broadway_Pink)
	red = 1;
	green = 0;
	blue = .56;
	transmission = .45;

	#elif defined(Gel_IS_R342_Rose_Pink)
	red = .81;
	green = 0;
	blue = .31;
	transmission = .84;

	#elif defined(Gel_IS_R344_Follies_Pink)
	red = .88;
	green = 0;
	blue = .5;
	transmission = .79;

	#elif defined(Gel_IS_R355_Pale_Violet)
	red = .25;
	green = 0.13;
	blue = .63;
	transmission = .80;

	#elif defined(Gel_IS_R356_Middle_Lavender)
	red = .68;
	green = 0.21;
	blue = 1;
	transmission = .73;

	#elif defined(Gel_IS_R357_Royal_Lavender)
	red = .19;
	green = 0.0;
	blue = .44;
	transmission = .95;

	#elif defined(Gel_IS_R358_Rose_Indigo)
	red = .13;
	green = 0;
	blue = .31;
	transmission = .95;

	#elif defined(Gel_IS_R359_Medium_Violet)
	red = .06;
	green = 0;
	blue = .38;
	transmission = .95;

	#elif defined(Gel_IS_R378_Alice_Blue)
	red = .31;
	green = 0;
	blue = .82;
	transmission = .85;

	#elif defined(Gel_IS_R383_Sapphire_Blue)
	red = .1;
	green = 0;
	blue = .51;
	transmission = .96;

	#elif defined(Gel_IS_R385_Royal_Blue)
	red = 0;
	green = 0;
	blue = .31;
	transmission = .96;

	#elif defined(Gel_IS_R388_Gaslight_Green)
	red = .0;
	green = 0.88;
	blue = .0;
	transmission = .24;

	#elif defined(Gel_IS_R389_Chroma_Green)
	red = 0;
	green = 0.5;
	blue = 0;
	transmission = .6;

	#elif defined(Gel_IS_R397_Pale_Grey)
	red = .88;
	green = 0.88;
	blue = .88;
	transmission = .3;	

	#elif defined(Gel_IS_L02_Rose_Pink)
	red = 0.85;
	green = 0;
	blue = 0.57;
	transmission = .67;

	#elif defined(Gel_IS_L03_Lavender_Tint)
	red = 0.86;
	green = 0.69;
	blue = 0.1;
	transmission = .25;

	#elif defined(Gel_IS_L04_Medium_Bastard_Amber)
	red = 1;
	green = 0.49;
	blue = 0.25;
	transmission = .36;

	#elif defined(Gel_IS_L07_Pale_Yellow)
	red = 1;
	green = 1;
	blue = 0.62;
	transmission = .15;

	#elif defined(Gel_IS_L08_Dark_Salmon)
	red = 1;
	green = 0.15;
	blue = 0.11;
	transmission = .65;

	#elif defined(Gel_IS_L09_Pale_Amber_Gold)
	red = 1;
	green = 0.63;
	blue = 0.25;
	transmission = .29;

	#elif defined(Gel_IS_L10_Medium_Yellow)
	red = 1;
	green = 0.86;
	blue = 0;
	transmission = .13;

	#elif defined(Gel_IS_L13_Straw_Tint)
	red = 1;
	green = 0.71;
	blue = 0.38;
	transmission = .28;

	#elif defined(Gel_IS_L15_Deep_Straw)
	red = 0.99;
	green = 0.51;
	blue = 0;
	transmission = .39;

	#elif defined(Gel_IS_L19_Fire)
	red = 0.99;
	green = 0;
	blue = 0.03;
	transmission = .81;

	#elif defined(Gel_IS_L20_Medium_Amber)
	red = 0.1;
	green = 0.61;
	blue = 0.42;
	transmission = .49;

	#elif defined(Gel_IS_L21_Gold_Amber)
	red = 0.99;
	green = 0.36;
	blue = 0.06;
	transmission = .69;

	#elif defined(Gel_IS_L22_Dark_Amber)
	red = 0.87;
	green = 0.13;
	blue = 0.13;
	transmission = .76;

	#elif defined(Gel_IS_L24_Scarlet)
	red = 0.87;
	green = 0;
	blue = 0;
	transmission = .81;

	#elif defined(Gel_IS_L26_Bright_Red)
	red = 0.79;
	green = 0;
	blue = 0;
	transmission = .91;

	#elif defined(Gel_IS_L27_Medium_Red)
	red = 0.26;
	green = 0;
	blue = 0.03;
	transmission = .96;
	
	#elif defined(Gel_IS_L35_Light_Pink)
	red = 1;
	green = 0.36;
	blue = 0.51;
	transmission = .39;

	#elif defined(Gel_IS_L36_Mesium_Pink)
	red = 1;
	green = 0.38;
	blue = 0.52;
	transmission = .67;

	#elif defined(Gel_IS_L46_Dark_Magenta)
	red = 0.38;
	green = 0;
	blue = 0;
	transmission = .94;

	#elif defined(Gel_IS_L48_Rose_Purple)
	red = 0.38;
	green = 0;
	blue = 0.36;
	transmission = .85;

	#elif defined(Gel_IS_L52_Light_Lavender)
	red = 0.39;
	green = 0;
	blue = 0.68;
	transmission = .55;

	#elif defined(Gel_IS_L53_Paler_Lavender)
	red = 0.64;
	green = 0.75;
	blue = 1;
	transmission = .38;

	#elif defined(Gel_IS_L58_Lavender)
	red = 0.25;
	green = 0;
	blue = 0.5;
	transmission = .91;

	#elif defined(Gel_IS_L61_Mist_Blue)
	red = 0.35;
	green = 0.86;
	blue = 0.98;
	transmission = .38;

	#elif defined(Gel_IS_L63_Pale_Blue)
	red = 0;
	green = 0.63;
	blue = 0.87;
	transmission = .46;

	#elif defined(Gel_IS_L68_Sky_Blue)
	red = 0;
	green = 0;
	blue = 0.58;
	transmission = .87;

	#elif defined(Gel_IS_L79_Just_Blue)
	red = 0.04;
	green = 0.04;
	blue = 0.60;
	transmission = .94;

	#elif defined(Gel_IS_L85_Deeper_Blue)
	red = 0;
	green = 0;
	blue = 0.26;
	transmission = .97;

	#elif defined(Gel_IS_L89_Moss_Green)
	red = 0;
	green = 0.62;
	blue = 0;
	transmission = .70;

	#elif defined(Gel_IS_L90_Dark_Yellow_Green)
	red = 0.02;
	green = 0.24;
	blue = 0;
	transmission = .89;
	
	#elif defined(Gel_IS_L101_Yellow)
	red = 0.1;
	green = 0.75;
	blue = 0.0;
	transmission = .2;
	
	#elif defined(Gel_IS_L102_Light_Amber)
	red = 0.98;
	green = 0.82;
	blue = 0.07;
	transmission = .25;

	#elif defined(Gel_IS_L103_Straw)
	red = 0.1;
	green = 0.87;
	blue = 0.71;
	transmission = .18;

	#elif defined(Gel_IS_L104_Deep_Amber)
	red = 0.98;
	green = 0.64;
	blue = 0.13;
	transmission = .36;

	#elif defined(Gel_IS_L105_Orange)
	red = 0.99;
	green = 0.47;
	blue = 0.01;
	transmission = .59;

	#elif defined(Gel_IS_L106_Primary_Red)
	red = 0.5;
	green = 0;
	blue = 0;
	transmission = .91;

	#elif defined(Gel_IS_L107_Light_Rose)
	red = 1;
	green = 0.31;
	blue = 0.43;
	transmission = .52;

	#elif defined(Gel_IS_L109_Light_Salman)
	red = 0.95;
	green = 0.4;
	blue = 0.4;
	transmission = .45;

	#elif defined(Gel_IS_L110_Middle_Rose)
	red = 1;
	green = 0.41;
	blue = 0.66;
	transmission = .53;

	#elif defined(Gel_IS_L111_Dark_Pink)
	red = 0.93;
	green = 0.22;
	blue = 0.59;
	transmission = .68;
	
	#elif defined(Gel_IS_L113_Magenta)
	red = 0.69;
	green = 0;
	blue = 0;
	transmission = .89;

	#elif defined(Gel_IS_L115_Peacock_Blue)
	red = 0.08;
	green = 0.61;
	blue = 0.55;
	transmission = .65;

	#elif defined(Gel_IS_L116_Medium_Blue_Green)
	red = 0;
	green = 0.31;
	blue = 0.13;
	transmission = .81;

	#elif defined(Gel_IS_L117_Steel_Blue)
	red = 0;
	green = 0.87;
	blue = 1;
	transmission = .45;

	#elif defined(Gel_IS_L118_Light_Blue)
	red = 0.07;
	green = 0.46;
	blue = 1;
	transmission = .76;

	#elif defined(Gel_IS_L119_Dark_Blue)
	red = 0;
	green = 0.06;
	blue = 0.5;
	transmission = .97;

	#elif defined(Gel_IS_L120_Deep_Blue)
	red = 0.14;
	green = 0.11;
	blue = 0.46;
	transmission = .99;

	#elif defined(Gel_IS_L121_Lee_Green)
	red = 0;
	green = 0.75;
	blue = 0;
	transmission = .36;

	#elif defined(Gel_IS_L122_Fern_Green)
	red = 0;
	green = 0.91;
	blue = 0.0;
	transmission = .48;

	#elif defined(Gel_IS_L124_Dark_Green)
	red = 0.0;
	green = 0.64;
	blue = 0.0;
	transmission = .69;


	#elif defined(Gel_IS_L126_Mauve)
	red = 0.38;
	green = 0;
	blue = 0.44;
	transmission = .96;

	#elif defined(Gel_IS_L127_Smokey_Pink)
	red = 0.44;
	green = 0.19;
	blue = 0.25;
	transmission = .88;

	#elif defined(Gel_IS_L128_Bright_Pink)
	red = 0.8;
	green = 0.07;
	blue = 0.45;
	transmission = .86;

	#elif defined(Gel_IS_L130_Clear)
	red = 1;
	green = 1;
	blue = 1;
	transmission = .05;

	#elif defined(Gel_IS_L132_Medium_Blue)
	red = 0;
	green = 0;
	blue = 0.75;
	transmission = .91;

	#elif defined(Gel_IS_L134_Golden_Amber)
	red = 0.1;
	green = 0.45;
	blue = 0.27;
	transmission = .62;

	#elif defined(Gel_IS_L135_Deep_Golden_Amber)
	red = 0.62;
	green = 0.13;
	blue = 0.13;
	transmission = .81;

	#elif defined(Gel_IS_L136_Pale_Lavender)
	red = 0.69;
	green = 0.5;
	blue = 0.87;
	transmission = .57;

	#elif defined(Gel_IS_L137_Special_Lavender)
	red = 0.33;
	green = 0.16;
	blue = 0.81;
	transmission = .74;

	#elif defined(Gel_IS_L138_Pale_Green)
	red = 0.5;
	green = 1;
	blue = 0.13;
	transmission = .20;

	#elif defined(Gel_IS_L139_Primary_Green)
	red = 0;
	green = 0.31;
	blue = 0;
	transmission = .85;

	#elif defined(Gel_IS_L141_Bright_Blue)
	red = 0;
	green = 0.19;
	blue = 0.82;
	transmission = .82;

	#elif defined(Gel_IS_L142_Pale_Violet)
	red = 0.19;
	green = 0;
	blue = 0.82;
	transmission = .82;

	#elif defined(Gel_IS_L143_Pale_Navy_Blue)
	red = 0;
	green = 0.53;
	blue = 0.59;
	transmission = .84;

	#elif defined(Gel_IS_L144_No_Colour_Blue)
	red = 0;
	green = 0.44;
	blue = 0.87;
	transmission = .68;

	#elif defined(Gel_IS_L147_Apricot)
	red = 1;
	green = 0.46;
	blue = 0.28;
	transmission = .47;

	#elif defined(Gel_IS_L148_Bright_Rose)
	red = 0.87;
	green = 0.01;
	blue = 0.38;
	transmission = .86;

	#elif defined(Gel_IS_L151_Gold_Tint)
	red = 1;
	green = 0.73;
	blue = 0.66;
	transmission = .31;

	#elif defined(Gel_IS_L152_Pale_Gold)
	red = 1;
	green = 0.79;
	blue = 0.68;
	transmission = .29;

	#elif defined(Gel_IS_L153_Pale_Salmon)
	red = 1;
	green = 0.65;
	blue = 0.72;
	transmission = .35;
	

	#elif defined(Gel_IS_L154_Pale_Rose)
	red = 1;
	green = 0.8;
	blue = 0.75;
	transmission = .27;

	#elif defined(Gel_IS_L156_Chocolate)
	red = 0.47;
	green = 0.36;
	blue = 0.36;
	transmission = .74;

	#elif defined(Gel_IS_L157_Pink)
	red = 0.88;
	green = 0;
	blue = 0.31;
	transmission = .64;

	#elif defined(Gel_IS_L158_Deep_Orange)
	red = 0.99;
	green = 0.37;
	blue = 0.16;
	transmission = .70;

	#elif defined(Gel_IS_L159_No_Color_Straw)
	red = 0.1;
	green = 0.1;
	blue = 0.79;
	transmission = .11;

	#elif defined(Gel_IS_L161_Slate_Blue)
	red = 0.06;
	green = 0.19;
	blue = 0.56;
	transmission = .75;

	#elif defined(Gel_IS_L162_Bastard_Amber)
	red = 1;
	green = 0.63;
	blue = 0.5;
	transmission = .22;

	#elif defined(Gel_IS_L164_Flame_Red)
	red = 0.89;
	green = 0;
	blue = 0;
	transmission = .82;

	#elif defined(Gel_IS_L165_Daylight_Blue)
	red = 0;
	green = 0.25;
	blue = 1;
	transmission = .8;

	#elif defined(Gel_IS_L166_Pale_Red)
	red = 0.92;
	green = 0.01;
	blue = 0.40;
	transmission = .75;

	#elif defined(Gel_IS_L170_Deep_Lavender)
	red = 0.71;
	green = 0;
	blue = 0.86;
	transmission = .74;

	#elif defined(Gel_IS_L174_Dark_Steel_Blue)
	red = 0;
	green = 0.27;
	blue = 0.99;
	transmission = .70;

	#elif defined(Gel_IS_L176_Loving_Amber)
	red = 0.94;
	green = 0.44;
	blue = 0.38;
	transmission = .50;

	#elif defined(Gel_IS_L179_Chrome_Orange)
	red = 0.1;
	green = 0.49;
	blue = 0.18;
	transmission = .46;

	#elif defined(Gel_IS_L180_Dark_Lavender)
	red = 0.44;
	green = 0;
	blue = 0.87;
	transmission = .93;

	#elif defined(Gel_IS_L181_Congo_Blue)
	red = 0.05;
	green = 0.02;
	blue = 0.44;
	transmission = .99;

	#elif defined(Gel_IS_L182_Light_Red)
	red = 0.56;
	green = 0;
	blue = 0;
	transmission = .89;

	#elif defined(Gel_IS_L183_Moonlight_Blue)
	red = 0.13;
	green = 0.38;
	blue = 1;
	transmission = .81;

	#elif defined(Gel_IS_L184_Cosmetic_Peach)
	red = 1;
	green = 0.94;
	blue = 0.87;
	transmission = .43;

	#elif defined(Gel_IS_L185_Cosmetic_Burgundy)
	red = 0.75;
	green = 0.62;
	blue = 0.63;
	transmission = .48;

	#elif defined(Gel_IS_L186_Cosmetic_Silver_Rose)
	red = 1;
	green = 0.63;
	blue = 0.81;
	transmission = .49;

	#elif defined(Gel_IS_L187_Cosmetic_Rouge)
	red = 1;
	green = 0.61;
	blue = 0.53;
	transmission = .46;

	#elif defined(Gel_IS_L188_Cosmetic_Highlight)
	red = 1;
	green = .82;
	blue = 0.78;
	transmission = .44;

	#elif defined(Gel_IS_L189_Cosmetic_Silver_Moss)
	red = 0.84;
	green = 1;
	blue = 0.71;
	transmission = .39;

	#elif defined(Gel_IS_L190_Cosmetic_Emerald)
	red = 1;
	green = 0.96;
	blue = 0.84;
	transmission = .40;

	#elif defined(Gel_IS_L191_Cosmetic_Aqua_Blue)
	red = 0.38;
	green = 1;
	blue = 0.87;
	transmission = .47;

	#elif defined(Gel_IS_L192_Flesh_Pink)
	red = 0.87;
	green = 0.13;
	blue = 0.38;
	transmission = .65;

	#elif defined(Gel_IS_L193_Rosy_Gold)
	red = 1;
	green = 0.32;
	blue = 0.4;
	transmission = .64;

	#elif defined(Gel_IS_L194_Surprise_Pink)
	red = 0.38;
	green = 0.25;
	blue = 0.75;
	transmission = .78;

	#elif defined(Gel_IS_L195_Zenith_Blue)
	red = 0;
	green = 0.06;
	blue = 0.38;
	transmission = .97;

	#elif defined(Gel_IS_L196_True_Blue)
	red = 0;
	green = 0.25;
	blue = 1;
	transmission = .73;

	#elif defined(Gel_IS_L197_Alice_Blue)
	red = 0.13;
	green = 0.25;
	blue = 0.75;
	transmission = .90;

	#elif defined(Gel_IS_L328_Follies_Pink)
	red = 0.8;
	green = 0;
	blue = 0.17;
	transmission = .80;

	#elif defined(Gel_IS_L332_Special_Rose_Pink)
	red = 0.76;
	green = 0;
	blue = 0.29;
	transmission = .89;
	
	
	#elif defined(Gel_IS_L343_Special_Medium_Lavender)
	red = 0.13;
	green = 0;
	blue = 0.5;
	transmission = .94;


	#elif defined(Gel_IS_L344_Violet)
	red = 0.13;
	green = 0.13;
	blue = 0.75;
	transmission = .80;
	

	#elif defined(Gel_IS_L353_Lighter_Blue)
	red = 0.11;
	green = 0.53;
	blue = 0.86;
	transmission = .59;

	#elif defined(Gel_IS_L354_Special_Steel_Blue)
	red = 0;
	green = 0.87;
	blue = 0.87;
	transmission = .61;

	#elif defined(Gel_IS_L363_Special_Medium_Blue)
	red = 0;
	green = 0;
	blue = 0.38;
	transmission = .96;

	#elif defined(Gel_IS_G105_Antique_Rose)
	red = 0.74;
	green = 0.98;
	blue = 0.58;
	transmission = .42;

	#elif defined(Gel_IS_G110_Dark_Rose)
	red = 0.91;
	green = 0.13;
	blue = 0.64;
	transmission = .78;
	
	
	#elif defined(Gel_IS_G120_Bright_Pink)
	red = 0.93;
	green = 0.24;
	blue = 0.58;
	transmission = .79;
	
	#elif defined(Gel_IS_G130_Rose)
	red = 0.95;
	green = 0.35;
	blue = 0.60;
	transmission = .64;
	
	#elif defined(Gel_IS_G140_Dark_Magenta)
	red = 0.73;
	green = 0.05;
	blue = 0.64;
	trans = .90;
	
	#elif defined(Gel_IS_G150_Pink_Punch)
	red = 0.75;
	green = 0;
	blue = 0.38;
	transmission = .73;
	
	#elif defined(Gel_IS_G155_Light_Pink)
	red = 0.98;
	green = 0.67;
	blue = 0.82;
	transmission = .29;
	
	#elif defined(Gel_IS_G160_Chorus_Pink)
	red = 0.99;
	green = 0.64;
	blue = 0.82;
	transmission = .38;
	
	#elif defined(Gel_IS_G170_Flesh_Pink)
	red = 0.98;
	green = 0.40;
	blue = 0.71;
	transmission = .53;
	
	#elif defined(Gel_IS_G180_Cherry)
	red = 0.98;
	green = 0.11;
	blue = 0.37;
	transmission = .73;
	
	#elif defined(Gel_IS_G190_Cold_Pink)
	red = 0.99;
	green = 0.55;
	blue = 0.68;
	transmission = .49;
	
	#elif defined(Gel_IS_G195_Nyph_Pink)
	red = 0.99;
	green = 0.57;
	blue = 0.72;
	transmission = .47;
	
	#elif defined(Gel_IS_G220_Pink_Magenta)
	red = 0.81;
	green = 0.02;
	blue = 0.29;
	transmission = .82;
	
	#elif defined(Gel_IS_G235_Pink_Red)
	red = 0.98;
	green = 0.21;
	blue = 0.28;
	transmission = .82;
	
	#elif defined(Gel_IS_G245_Light_Red)
	red = 0.82;
	green = 0.02;
	blue = 0.08;
	transmission = .87;
	
	#elif defined(Gel_IS_G250_Medium_Red_XT)
	red = 0.45;
	green = 0;
	blue = 0.06;
	transmission = .92;
	
	#elif defined(Gel_IS_G260_Rosy_Amber)
	red = 0.99;
	green = 0.49;
	blue = 0.60;
	transmission = .50;
	
	#elif defined(Gel_IS_G270_Red_Orange)
	red = 0.44;
	green = 0;
	blue = 0;
	transmission = .83;
	
	#elif defined(Gel_IS_G280_Fire_Red)
	red = 1;
	green = 0.04;
	blue = 0.04;
	transmission = .74;
	
	#elif defined(Gel_IS_G290_Fire_Orange)
	red = 0.90;
	green = 0.07;
	blue = 0;
	transmission = .72;
	
	#elif defined(Gel_IS_G305_French_Rose)
	red = 1;
	green = 0.59;
	blue = 0.67;
	transmission = .30;
	
	#elif defined(Gel_IS_G315_Autumn_Glory)
	red = 0.99;
	green = 0.28;
	blue = 0.24;
	transmission = .63;
	
	#elif defined(Gel_IS_G320_Peach)
	red = 0.99;
	green = 0.33;
	blue = 0.15;
	transmission = .53;
	
	#elif defined(Gel_IS_G323_Indian_Summer)
	red = 1;
	green = 0.13;
	blue = 0.13;
	transmission = .54;
	
	#elif defined(Gel_IS_G325_Bastard_Amber)
	red = 1;
	green = 0.68;
	blue = 0.59;
	transmission = .28;
	
	#elif defined(Gel_IS_G330_Sepia)
	red = 0.71;
	green = 0.41;
	blue = 0.43;
	transmission = .63;
	
	#elif defined(Gel_IS_G335_Coral)
	red = 0.62;
	green = 0;
	blue = 0;
	transmission = .56;
	
	#elif defined(Gel_IS_G340_Light_Bastard_Amber)
	red = 0.1;
	green = 0.82;
	blue = 0.65;
	transmission = .22;
	
	#elif defined(Gel_IS_G343_Honey)
	red = 0.44;
	green = 0.06;
	blue = 0;
	transmission = .38;
	
	#elif defined(Gel_IS_G345_Deep_Amber)
	red = 1;
	green = 0;
	blue = 0;
	transmission = .59;
	
	#elif defined(Gel_IS_G350_Dark_Amber)
	red = 0.99;
	green = 0.40;
	blue = 0.15;
	transmission = .49;
	
	#elif defined(Gel_IS_G360_Amber_Blush)
	red = 0.94;
	green = 0.81;
	blue = 0.70;
	transmission = .27;
	
	#elif defined(Gel_IS_G363_Sand)
	red = 0.1;
	green = 0.87;
	blue = 0.69;
	transmission = .15;
	
	#elif defined(Gel_IS_G364_Pale_Honey)
	red = 0.87;
	green = 0.58;
	blue = 0.25;
	transmission = .18;
	
	#elif defined(Gel_IS_G365_Warm_Straw)
	red = 0.94;
	green = 0.81;
	blue = 0.70;
	transmission = .21;
	
	#elif defined(Gel_IS_G370_Spice)
	red = 0.44;
	green = 0.31;
	blue = 0.31;
	transmission = .79;
	
	#elif defined(Gel_IS_G375_Flame)
	red = 0.99;
	green = 0.48;
	blue = 0.21;
	transmission = .28;
	
	#elif defined(Gel_IS_G380_Golden_Tan)
	red = 0.49;
	green = 0.32;
	blue = 0.27;
	transmission = .64;
	
	#elif defined(Gel_IS_G385_Light_Amber)
	red = 0.99;
	green = 0.71;
	blue = 0.53;
	transmission = .23;
	
	#elif defined(Gel_IS_G390_Walnut)
	red = 0.21;
	green = 0.15;
	blue = 0.16;
	transmission = .89;
	
	#elif defined(Gel_IS_G420_Medium_Amber)
	red = 0.99;
	green = 0.79;
	blue = 0.38;
	transmission = .29;
	
	#elif defined(Gel_IS_G440_Very_Light_Straw)
	red = 1;
	green = 0.91;
	blue = 0.73;
	transmission = .17;
	
	#elif defined(Gel_IS_G450_Saffron)
	red = 0.90;
	green = 0.62;
	blue = 0.09;
	transmission = .17;
	
	#elif defined(Gel_IS_G460_Mellow_Yellow)
	red = 0.97;
	green = 0.99;
	blue = 0.32;
	transmission = .17;
	
	#elif defined(Gel_IS_G470_Pale_Gold)
	red = 0.94;
	green = 1;
	blue = 0.22;
	transmission = .13;
	
	#elif defined(Gel_IS_G480_Medium_Yellow)
	red = 0.96;
	green = 0.92;
	blue = 0.11;
	transmission = .12;
	
	#elif defined(Gel_IS_G510_No_Color_Straw)
	red = 0.99;
	green = 0.98;
	blue = 0.73;
	transmission = .10;
	
	#elif defined(Gel_IS_G520_New_Straw)
	red = 0.78;
	green = 1;
	blue = 0.65;
	transmission = .15;
	
	#elif defined(Gel_IS_G535_Lime)
	red = 0.38;
	green = 1;
	blue = 0.54;
	transmission = .14;
	
	#elif defined(Gel_IS_G540_Pale_Green)
	red = 0.57;
	green = 1;
	blue = 0.36;
	transmission = .25;
	
	#elif defined(Gel_IS_G570_Light_Green_Yellow)
	red = 0.1;
	green = 0.89;
	blue = 0.4;
	transmission = .54;
	
	#elif defined(Gel_IS_G650_Grass_Green)
	red = 0;
	green = 0.42;
	blue = 0;
	transmission = .94;
	
	#elif defined(Gel_IS_G655_Rich_Green)
	red = 0;
	green = 0.05;
	blue = 0;
	transmission = .88;
	
	#elif defined(Gel_IS_G660_Medium_Green)
	red = 0;
	green = 0.7;
	blue = 0.02;
	transmission = .64;
	
	#elif defined(Gel_IS_G680_Kelly_Green)
	red = 0.15;
	green = 0.71;
	blue = 0.44;
	transmission = .65;
	
	#elif defined(Gel_IS_G685_Pistachio)
	red = 0;
	green = 0.17;
	blue = 0.06;
	transmission = .70;
	
	#elif defined(Gel_IS_G690_Bluegrass)
	red = 0;
	green = 0.22;
	blue = 0.13;
	transmission = .83;
	
	#elif defined(Gel_IS_G710_Blue_Green)
	red = 0.23;
	green = 0.46;
	blue = 0.38;
	transmission = .86;
	
	#elif defined(Gel_IS_G720_Light_Steel_Blue)
	red = 0.47;
	green = 0.89;
	blue = 1;
	transmission = .44;
	
	#elif defined(Gel_IS_G725_Princess_Blue)
	red = 0;
	green = 0.35;
	blue = 0.44;
	transmission = .68;
	
	#elif defined(Gel_IS_G730_Azure_Blue)
	red = 0;
	green = 0.4;
	blue = 0.48;
	transmission = .73;
	
	#elif defined(Gel_IS_G740_Off_Blue)
	red = 0;
	green = 0.65;
	blue = 0.90;
	transmission = .83;
	
	#elif defined(Gel_IS_G750_Nile_Blue)
	red = 0;
	green = 0.51;
	blue = 0.66;
	transmission = .86;
	
	#elif defined(Gel_IS_G760_Aqua_Blue)
	red = 0.13;
	green = 0.44;
	blue = 0.47;
	transmission = .86;
	
	#elif defined(Gel_IS_G770_Christel_Blue)
	red = 0.18;
	green = 0.60;
	blue = 0.64;
	transmission = .81;
	
	#elif defined(Gel_IS_G780_Shark_Blue)
	red = 0.21;
	green = 0.69;
	blue = 0.75;
	transmission = .73;
	
	#elif defined(Gel_IS_G790_Electric_Blue)
	red = 0.41;
	green = 0.62;
	blue = 1;
	transmission = .60;
	
	#elif defined(Gel_IS_G810_Moon_Blue)
	red = 0;
	green = 0.24;
	blue = 0.69;
	transmission = .89;
	
	#elif defined(Gel_IS_G815_Moody_Blue)
	red = 0;
	green = 0.2;
	blue = 0.42;
	transmission = .8;
	
	#elif defined(Gel_IS_G820_Full_Light_Blue)
	red = 0.58;
	green = 0.73;
	blue = 1;
	transmission = .50;
	
	#elif defined(Gel_IS_G830_North_Sky_Blue)
	red = 0.86;
	green = 0.78;
	blue = 1;
	transmission = .49;
	
	#elif defined(Gel_IS_G835_Aztec_Blue)
	red = 0.0;
	green = 0.0;
	blue = 0.36;
	transmission = .94;
	
	#elif defined(Gel_IS_G840_Steel_Blue)
	red = 0.37;
	green = 0;
	blue = 0.98;
	transmission = .81;
	
	#elif defined(Gel_IS_G842_Whisper_Blue)
	red = 0;
	green = 0.54;
	blue = 1;
	transmission = .56;
	
	#elif defined(Gel_IS_G845_Cobalt)
	red = 0;
	green = 0;
	blue = 0.35;
	transmission = .96;
	
	#elif defined(Gel_IS_G847_City_Blue)
	red = 0;
	green = 0;
	blue = 0.38;
	transmission = .87;
	
	#elif defined(Gel_IS_G848_Bonus_Blue)
	red = 0;
	green = 0;
	blue = 0.42;
	transmission = .86;
	
	#elif defined(Gel_IS_G850_Blue_Primary)
	red = 0.25;
	green = 0;
	blue = 0.67;
	transmission = .95;
	
	#elif defined(Gel_IS_G860_Sky_Blue)
	red = 0.6;
	green = 0.35;
	blue = 1;
	transmission = .72;
	
	#elif defined(Gel_IS_G870_Winter_Blue)
	red = 0;
	green = 0.83;
	blue = 0.1;
	transmission = .24;
	
	#elif defined(Gel_IS_G880_Daylight_Blue)
	red = 0.35;
	green = 0;
	blue = 0.93;
	transmission = .78;
	
	#elif defined(Gel_IS_G882_Southern_Sky)
	red = 0;
	green = 0.11;
	blue = 0.28;
	transmission = .76;
	
	#elif defined(Gel_IS_G885_Blue_Ice)
	red = 0;
	green = 0.52;
	blue = 0.63;
	transmission = .40;
	
	#elif defined(Gel_IS_G888_Blue_Belle)
	red = 0.0;
	green = 0.09;
	blue = 0.22;
	transmission = .69;
	
	#elif defined(Gel_IS_G890_Dark_Sky_Blue)
	red = 0.01;
	green = 0.04;
	blue = 0.53;
	transmission = .97;
	
	#elif defined(Gel_IS_G905_Dark_Blue)
	red = 0;
	green = 0;
	blue = 0.42;
	transmission = .99;
	
	#elif defined(Gel_IS_G910_Alice_Blue)
	red = 0.06;
	green = 0;
	blue = 0.39;
	transmission = .89;
	
	#elif defined(Gel_IS_G915_Twilight)
	red = 0.05;
	green = 0.00;
	blue = 0.36;
	transmission = .96;
	
	#elif defined(Gel_IS_G920_Pale_Lavender)
	red = 0.95;
	green = 0.86;
	blue = 1;
	transmission = .37;
	
	#elif defined(Gel_IS_G925_Cosmic_Blue)
	red = 0;
	green = 0;
	blue = 0.05;
	transmission = .98;
	
	#elif defined(Gel_IS_G930_Real_Congo_Blue)
	red = 0.0;
	green = 0.0;
	blue = 1;
	transmission = .98;
	
	#elif defined(Gel_IS_G940_Light_Purple)
	red = 0.03;
	green = 0.10;
	blue = 0.75;
	transmission = .80;
	
	#elif defined(Gel_IS_G945_Royal_Purple)
	red = 0.27;
	green = 0.01;
	blue = 0.42;
	transmission = .98;
	
	#elif defined(Gel_IS_G948_African_Violet)
	red = 0.05;
	green = 0;
	blue = 0.13;
	transmission = .96;
	
	#elif defined(Gel_IS_G950_Purple)
	red = 0.25;
	green = 0.01;
	blue = 0.38;
	transmission = .93;
	
	#elif defined(Gel_IS_G960_Medium_Lavender)
	red = 0.54;
	green = 0.02;
	blue = 0.82;
	transmission = .78;
	
	#elif defined(Gel_IS_G970_Special_Lavender)
	red = 0.69;
	green = 0.13;
	blue = 0.98;
	transmission = .68; 	
	
	#elif defined(Gel_IS_G980_Surprise_Pink)
	red = 0.88;
	green = 0.67;
	blue = 0.99;
	transmission = .48;
	
	#elif defined(Gel_IS_G990_Dark_Lavender)
	red = 0.41;
	green = 0.2;
	blue = 0.62;
	transmission = .85;
	
	#elif defined(Gel_IS_G995_Orchid)
	red = 0.14;
	green = 0;
	blue = 0.11;
	transmission = .94;

	
	
	#endif

		
		
		
		
		
		
		
	#if defined(lightSource_IS_40W_Tungsten_2600k)
			color = vec3(1,0.7725,.5607);		
	#elif defined(lightSource_IS_100W_Tungsten_2850k)
			color = vec3(1,0.8392,0.6666);
	#elif defined(lightSource_IS_575W_HPL_3200k)
			color = vec3(1,0.945,0.8784);
	#elif defined(lightSource_IS_Carbon_Arc_5200k)
			color = vec3(1,0.98,0.9568);
	#elif defined(lightSource_IS_Direct_Sunlight_6000k)
			color = vec3(1,1,1);
	#elif defined(lightSource_IS_Mercury_Vapor)
			color = vec3(0.84705,0.968627,1);	
	#elif defined(lightSource_IS_Sodium_Vapor)
			color = vec3(1,0.8196,0.6980);
	#elif defined(lightSource_IS_Metal_Halide)
			color = vec3(0.949,0.988235,1);	
	#elif defined(lightSource_IS_High_Pressure_Sodium)
			color = vec3(1,0.71764,0.298039);
	#elif defined(lightSource_IS_Warm_Flourescent)
			color = vec3(1,0.956862,0.898039);
	#elif defined(lightSource_IS_Standard_Flourescent)
			color = vec3(0.956862,1,0.98039);	
	#elif defined(lightSource_IS_Cool_White_Flourescent)
			color = vec3(0.83137,0.921568,1);
	#elif defined(lightSource_IS_Full_Spectrum_Flourescent)
			color = vec3(1,0.95686,0.94901);
	#elif defined(lightSource_IS_Black_Light)
			color = vec3(0.654901,0,1);	
	#endif	

		
		
		
	color = mix(vec3(red,green,blue),color,0.5);
	
	
	
    #if defined(mode_IS_Solid)
    transmission = 1;
	#endif

	
	
	return vec4(color,transmission);
}
