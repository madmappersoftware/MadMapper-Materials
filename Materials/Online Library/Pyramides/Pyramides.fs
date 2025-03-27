/*{
    "CREDIT": "mad-matt",
    "DESCRIPTION": "Animate Pyramides with a light",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
        { "LABEL": "Grid Width", "NAME": "mat_grid_width", "TYPE": "int", "MIN": 1, "MAX": 200, "DEFAULT": 10 },
        { "LABEL": "Angle Vars", "NAME": "mat_angle_vars", "TYPE": "long", "DEFAULT": "1", "VALUES": ["1","2","4","8","16"] },
		{ "LABEL": "Light/Ambient Color", "NAME": "mat_ambient_color", "TYPE": "color", "DEFAULT": [ 0.1, 0.1, 0.1, 0.1 ] },
        { "LABEL": "Light/Light Color", "NAME": "mat_lightColor", "TYPE": "color", "DEFAULT": [1,1,1,1] },
        { "LABEL": "Light/Light Pos XY", "NAME": "mat_lightXY", "TYPE": "point2D", "MIN": [-2.0,-2.0], "MAX": [2.0,2.0], "DEFAULT": [0.0,0.0] },
        { "LABEL": "Light/Light Pos Z", "NAME": "mat_lightZ", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Light/Light Atten.", "NAME": "mat_lightAttenuation", "TYPE": "float", "MIN": 0.0, "MAX": 0.5, "DEFAULT": 0.1 },
		{ "LABEL": "Spot Light/Spot Light", "NAME": "mat_spotLight", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Spot Light/Light Dir", "NAME": "mat_spotLightDir", "TYPE": "point2D", "MIN": [-1.0,-1.0], "MAX": [1.0,1.0], "DEFAULT":  [0.0,0.0] },
        { "LABEL": "Spot Light/Cutoff", "NAME": "mat_spotLightCutoff", "TYPE": "float", "MIN": 0.001, "MAX": 1.0, "DEFAULT": 0.5 },
        { "LABEL": "Spot Light/Exponent", "NAME": "mat_spotLightExponent", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ]
}*/

//#include "MadCommon.glsl"
//#include "MadNoise.glsl"
//#include "MadSDF.glsl"	
#define PI 3.1415926535897932384626433832795

// float distToLine(vec2 pt1, vec2 pt2, vec2 testPt)
// {
//   vec2 lineDir = pt2 - pt1;
//   vec2 perpDir = vec2(lineDir.y, -lineDir.x);
//   vec2 dirToPt1 = pt1 - testPt;
//   return abs(dot(normalize(perpDir), dirToPt1));
// }

bool pointInTriangle(vec2 P, vec2 A, vec2 B, vec2 C) {
    // Compute vectors
    vec2 v0 = C - A;
    vec2 v1 = B - A;
    vec2 v2 = P - A;

    // Compute dot products
    float dot00 = dot(v0,v0);
    float dot01 = dot(v0,v1);
    float dot02 = dot(v0,v2);
    float dot11 = dot(v1,v1);
    float dot12 = dot(v1,v2);

    // Compute barycentric coordinates
    float denom = dot00 * dot11 - dot01 * dot01;
    if (denom==0) return false;
    float invDenom = 1 / denom;
    float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
    float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

    // Check if point is in triangle
    return (u >= 0) && (v >= 0) && (u + v <= 1);
}

vec2 rotate2D(vec2 v, float a) {
    float s = sin(a);
    float c = cos(a);
    mat2 m = mat2(c, -s, s, c);
    return m * v;
}

vec4 materialColorForPixel( vec2 texCoord )
{
	texCoord.y = 1-texCoord.y;

	// get texture coordinates
	vec2 cell = vec2(int(texCoord.x * mat_grid_width),int(texCoord.y * mat_grid_width));
	vec2 posInCell = texCoord * mat_grid_width - cell;
    posInCell = vec2(0.5) + (posInCell-vec2(0.5))*sqrt(2);
    int cellId = int(cell.x + cell.y * mat_grid_width);
    int angleVariations = int(pow(2,mat_angle_vars));

    if (mat_angle_vars>0) {
        posInCell = vec2(0.5) + rotate2D(posInCell-vec2(0.5),(cellId%angleVariations) * 2*PI/angleVariations);
    }

    vec3 faceNormal;

	// This is our triangle
	vec2 triangleP1 = vec2(0,0);
	vec2 triangleP2 = vec2(1,0);
	vec2 triangleP3 = vec2(0.5,1);
	vec2 pyramideCenter = vec2(0.5,0.5);

    // Check on which part we are
    if (pointInTriangle(posInCell,triangleP1,triangleP2,pyramideCenter)) {
        faceNormal = normalize(vec3(0,-0.5,0.5));
        //return vec4(1,0,0,1);
    }
    else if (pointInTriangle(posInCell,triangleP1,triangleP3,pyramideCenter)) {
        faceNormal = normalize(vec3(-0.5,0.25,0.5));
        //return vec4(0,1,0,1);
    }
    else if (pointInTriangle(posInCell,triangleP2,triangleP3,pyramideCenter)) {
        faceNormal = normalize(vec3(0.5,0.25,0.5));
        //return vec4(0,0,1,1);
    } else {
        return vec4(0,0,0,1);
    }
        
    if (mat_angle_vars>0) {
        faceNormal.xy = rotate2D(faceNormal.xy,-(cellId%angleVariations) * 2*PI/angleVariations);
    }

	vec3 color = vec3(1);

	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_ambient_color.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));

	// Light pos with XY from [0,0] to [mat_grid_width,mat_grid_width]
	vec3 lightPos = vec3((vec2(1)+mat_lightXY)*0.5*mat_grid_width,mat_lightZ);
    vec3 vecToLight = lightPos-(vec3(texCoord * mat_grid_width,0));

    vec3 vecToLightNormalized = normalize(vecToLight);
    float lambertTerm = dot(faceNormal,vecToLightNormalized);
    if(lambertTerm > 0.0)
    {
        float lightDistance = length(vecToLight);
        float attenuation = min(1.0, 1.0 / (mat_lightAttenuation * lightDistance * lightDistance));

        if (mat_spotLight) {
            float clampedCosine = max(0.0, dot(-vecToLightNormalized, normalize(vec3(mat_spotLightDir-mat_lightXY,-1))));
            float maxCosine = cos(mat_spotLightCutoff);
            if (clampedCosine < maxCosine) // outside of spotlight cone
                attenuation = 0.0;
            else
                attenuation = attenuation * pow(1.-((1.-clampedCosine) / (1.-maxCosine)), mat_spotLightExponent);
        }

        vec3 light_color = mat_lightColor.rgb * lambertTerm;
        color += light_color * attenuation;
    }

	return vec4(color,1.0);	
}
