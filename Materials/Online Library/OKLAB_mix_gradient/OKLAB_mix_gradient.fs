/*{
    "CREDIT": "frz",
    "DESCRIPTION": "okLab color mix",
    "TAGS": "utility",
    "VSN": "1.0",
    "INPUTS": [ 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.02, 0.31, 1.0, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 0.0, 1.0 ], "FLAGS": "no_alpha" }, 
    ],
}*/

//By Inigo Quilez, under MIT license
//https://www.shadertoy.com/view/ttcyRS
vec3 oklab_mix(vec3 lin1, vec3 lin2, float a)
{
    // https://bottosson.github.io/posts/oklab
    const mat3 kCONEtoLMS = mat3(                
         0.4121656120,  0.2118591070,  0.0883097947,
         0.5362752080,  0.6807189584,  0.2818474174,
         0.0514575653,  0.1074065790,  0.6302613616);
    const mat3 kLMStoCONE = mat3(
         4.0767245293, -1.2681437731, -0.0041119885,
        -3.3072168827,  2.6093323231, -0.7034763098,
         0.2307590544, -0.3411344290,  1.7068625689);
                    
    // rgb to cone (arg of pow can't be negative)
    vec3 lms1 = pow( kCONEtoLMS*lin1, vec3(1.0/3.0) );
    vec3 lms2 = pow( kCONEtoLMS*lin2, vec3(1.0/3.0) );
    // lerp
    vec3 lms = mix( lms1, lms2, a );
    // gain in the middle (no oklab anymore, but looks better?)
    lms *= 1.0+0.2*a*(1.0-a);
    // cone to rgb
    return kLMStoCONE*(lms*lms*lms);
}

vec3 linear_from_srgb(vec3 rgb)
{
    return pow(rgb, vec3(2.2));
}
vec3 srgb_from_linear(vec3 lin)
{
    return pow(lin, vec3(1.0/2.2));
}

vec4 materialColorForPixel( vec2 texCoord )
{
	float  m = texCoord.x;
	vec3 col1 = oklab_mix(mat_backgroundColor.rgb,mat_foregroundColor.rgb,m);
	vec3 col2 = mix(mat_backgroundColor.rgb,mat_foregroundColor.rgb,m);

	vec3 col = col1;
//	col = srgb_from_linear(col);

	if(texCoord.y < 0.5)col = col2;


	return vec4(col,1.);
}