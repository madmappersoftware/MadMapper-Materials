/*
  Laser Material Documentation

  A bit like Materials in MadMapper, Laser Materials are glsl shader, compiled by and executed by the graphics card. A Material actually writes color in pixels in a texture (an image) to be displayed in MadMapper surfaces. A Laser Material is also writing to a texture, but it's not actually writing colors, it's writing one or multiple 2D pathes that include color. The Laser Material output is a list of pathes made of a list of sample points, each including a color and eventually user data.

  The Laser Material code consists of a function called laserMaterialFunc that takes as input the point number for which it will write data, and the total point count it should output. By default MadMapper will request 8192 samples but this can be changed in "RENDER_SETTINGS" / "POINT_COUNT" (documented below). For each sample, we provide 2D position, rgb color, shape number & eventually user data. The shape number allows MadMapper to know when you want to start a new path (for instance if you want to draw two circles, you don't want them connected).

  The output of the laserMaterialFunc is written to a texture. This is what the texture will contain the output of the laserMaterialFunc in this way:

    Line 0:
      r = x position of the sample point (-1 to 1)
      g = y position of the sample point (-1 to 1)
      b = shape number of this sample point (0,1,2,3...): each time shape number is different from previous sample, MadMapper will start a new shape
      a = 0
    Line 1:
      rgba = color (clamp in 0-1)
    Line 2:
      rgba = user data

  The Laser Material can access data generated at previous frame (the output texture of previous engine frame), which allows creating more logic in the pathes generation. This is passed as a sampler2D uniform named mm_LastFrameData.

*/

/*{
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Simple vector template",
    "TAGS": "laser",
    "VSN": "1.0",
    "INPUTS": [
		{"LABEL": "Global/Amount", "NAME": "mat_amount", "TYPE": "int", "MIN": 1, "MAX": 500, "DEFAULT": 10 }, 
		{"LABEL": "Global/Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },  
		{"LABEL": "Global/Period", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 0.7 },  
		{"LABEL": "Global/Amplitude", "NAME": "mat_amp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.3 }, 

		{ "LABEL": "Color/Left", "NAME": "mat_leftColor", "TYPE": "color", "DEFAULT": [ 1.0, 0.0, 0.0, 1.0 ] ,"FLAGS" :"no_alpha"},
		{ "LABEL": "Color/Right", "NAME": "mat_rightColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] ,"FLAGS" :"no_alpha"},

    ],

    "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed","speed_curve": 1,"link_speed_to_global_bpm":true}},
    ],
    "RENDER_SETTINGS": {
       "POINT_COUNT": "mat_amount"
    }
}*/

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
    float normalizedPos = float(pointNumber)/(pointCount-1);
    pos = vec2(normalizedPos*2.-1.,cos(normalizedPos*3.14159*mat_scale+mat_time)*mat_amp);
    shapeNumber = (pointNumber/2);
    color = vec4(mix(mat_leftColor.rgb,mat_rightColor.rgb,vec3(normalizedPos)),1.);
}
