/*{
    "CREDIT": "Furtive Vision, Collectif Scale",
    "DESCRIPTION": "Shutter by frame",
    "VSN": "1.0",
    "TAGS": "color,gradient,Collectif Scale",
	"INPUTS": [
        { "Label": "Frame on", "NAME": "frame_on", "TYPE": "int", "MIN": 1.0, "MAX": 20.0, "DEFAULT": 1.0 },
        { "Label": "Frame off", "NAME": "frame_off", "TYPE": "int", "MIN": 1.0, "MAX": 50.0, "DEFAULT": 3.0 },
        { "Label": "Color/Back Color", "NAME": "backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ] },
        { "Label": "Color/Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
	],
}*/


vec4 materialColorForPixel(vec2 texCoord)
{
  // float frame_on= 2.;
  // float frame_off = 10.;
  // float amount =  mod(FRAMEINDEX / 2., 2) * 0.5;

  float amount = mod(FRAMEINDEX, frame_on + frame_off);
  amount = step(amount, frame_on);
  // amount = sin(amount * 3.14);
  // amount = 0.;
	vec4 backColor = backgroundColor;
	vec4 frontColor = foregroundColor;
	return mix(backColor,frontColor,amount);
}
