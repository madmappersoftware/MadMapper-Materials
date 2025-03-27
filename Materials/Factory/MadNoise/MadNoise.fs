/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz / 1024 architecture",
    "DESCRIPTION": "Noise ToolBox",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 

	{ "NAME": "TYPE", "Label": "Type", "TYPE": "long", "DEFAULT": "Billowed", "FLAGS": "generate_as_define", 
	"VALUES": ["Worley", "Billowed", "Flow", "Simplex", "Ridged","fBm","Relief","TiledVoronoi","TiledFBM", "Hash"] },

	{ "LABEL": "Global Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
	{ "Label": "Strobe", "NAME": "mat_noise_strob", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0 }, 
	{ "LABEL": "Link to global BPM", "NAME": "mat_bpm_link", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
	{ "LABEL": "UV/Polar Coordinates", "NAME": "mat_polar", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button,generate_as_define" }, 	
	{ "LABEL": "UV/Speed X", "NAME": "mat_speed_x", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.0 },
	{ "LABEL": "UV/Speed Y", "NAME": "mat_speed_y", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
	{ "LABEL": "UV/Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 },
	{ "LABEL": "UV/Anamorphy", "NAME": "mat_anamorphic", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
	{ "LABEL": "UV/Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		
	{ "NAME": "GRADIENT", "Label": "Color/Gradient", "TYPE": "long", "DEFAULT": "Linear",
	"FLAGS": "generate_as_define",
 	"VALUES": [ "Linear", "Perturbed", "Reverse", "Camel","Multi","HalfScale","Scales","ScalesDense","BW"  ] },

	{ "LABEL": "Color/Cycle", "NAME": "mat_cycle", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0. },
	{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" }, 
	{ "LABEL": "Color/Rotation", "NAME": "mat_crot", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 }, 
    { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
    
	
     ],

	 "GENERATORS": [
    {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": false, "link_speed_to_global_bpm":"mat_bpm_link", "strob": "mat_noise_strob",}},
	{"NAME": "mat_x_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_x", "speed_curve":2,"bpm_sync": false, "link_speed_to_global_bpm":"mat_bpm_link"}},
	{"NAME": "mat_y_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed_y", "speed_curve":2,"bpm_sync": false, "link_speed_to_global_bpm":"mat_bpm_link"}},
    ],

    "IMPORTED": [
    {"NAME": "texture_noised", "PATH": "g_noised2.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
    {"NAME": "texture_reverse", "PATH": "g_reverse.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    {"NAME": "texture_camel", "PATH": "g_camel.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    {"NAME": "texture_halfscale", "PATH": "g_halfscales.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
    {"NAME": "texture_bw", "PATH": "g_BW.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
	{"NAME": "texture_scales", "PATH": "g_scales.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
	{"NAME": "texture_scales_dense", "PATH": "g_scales_dense.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},
	{"NAME": "texture_multi", "PATH": "g_multi.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "CLAMP_TO_EDGE"},

    ]
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"

///////////////////
// Noise functions
// Hash without Sine by DaveHoskins 
// https://www.shadertoy.com/view/4djSRW
//

float hash13(vec3 p3) {
    p3  = fract(p3 * 1031.1031);
    p3 += dot(p3, p3.yzx + 19.19);
    return fract((p3.x + p3.y) * p3.z);
}

float valueHash(vec3 p3) {
    p3  = fract(p3 * 0.1031);
    p3 += dot(p3, p3.yzx + 19.19);
    return fract((p3.x + p3.y) * p3.z);
}

float valueNoise( in vec3 x, float tile ) {
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    
    return mix(mix(mix( valueHash(mod(p+vec3(0,0,0),tile)), 
                        valueHash(mod(p+vec3(1,0,0),tile)),f.x),
                   mix( valueHash(mod(p+vec3(0,1,0),tile)), 
                        valueHash(mod(p+vec3(1,1,0),tile)),f.x),f.y),
               mix(mix( valueHash(mod(p+vec3(0,0,1),tile)), 
                        valueHash(mod(p+vec3(1,0,1),tile)),f.x),
                   mix( valueHash(mod(p+vec3(0,1,1),tile)), 
                        valueHash(mod(p+vec3(1,1,1),tile)),f.x),f.y),f.z);
}

float voronoi( vec3 x, float tile ) {
    vec3 p = floor(x);
    vec3 f = fract(x);

    float res = 100.;
    for(int k=-1; k<=1; k++){
        for(int j=-1; j<=1; j++) {
            for(int i=-1; i<=1; i++) {
                vec3 b = vec3(i, j, k);
                vec3 c = p + b;

                if( tile > 0. ) {
                    c = mod( c, vec3(tile) );
                }

                vec3 r = vec3(b) - f + hash13( c );
                float d = dot(r, r);

                if(d < res) {
                    res = d;
                }
            }
        }
    }

    return 1.-res;
}

float tilableVoronoi( vec3 p, const int octaves, float tile ) {
    float f = 1.;
    float a = 1.;
    float c = 0.;
    float w = 0.;

    if( tile > 0. ) f = tile;

    for( int i=0; i<octaves; i++ ) {
        c += a*voronoi( p * f, f );
        f *= 2.0;
        w += a;
        a *= 0.5;
    }

    return c / w;
}

float tilableFbm( vec3 p, const int octaves, float tile ) {
    float f = 1.;
    float a = 1.;
    float c = 0.;
    float w = 0.;

    if( tile > 0. ) f = tile;

    for( int i=0; i<octaves; i++ ) {
        c += a*valueNoise( p * f, f );
        f *= 2.0;
        w += a;
        a *= 0.5;
    }

    return c / w;
}

mat3 rotation(float angle, vec3 axis)
{
	float s = sin(-angle);
	float c = cos(-angle);
	float oc = 0.15 - c;
	vec3 sa = axis * s;
	vec3 oca = axis * oc;
	return mat3(	
		oca.x * axis + vec3(	c,	-sa.z,	sa.y),
		oca.y * axis + vec3( sa.z,	c,		-sa.x),		
		oca.z * axis + vec3(-sa.y,	sa.x,	c));	
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	uv = uv*2.-1.;

	// modify uv with material inputs
	uv *= vec2(mat_scale);
    #if defined( TYPE_IS_TiledFBM )
    	uv*= 0.1;
    #elif defined( TYPE_IS_TiledVoronoi )
    	uv*= 0.1;
    #endif

	uv.x *= 1. - max(-mat_anamorphic,0.);
	uv.y *= 1. - max(mat_anamorphic,0.);
	uv += vec2(-mat_offset.x,mat_offset.y);


    if(mat_polar){
        const float r_inner=0.25; 
        const float r_outer=0.75; 

        vec2 x = uv;
        float radius = length(x);
        float angle = atan(x.y, x.x);

        vec2 tc_polar; // the new polar texcoords
        // map radius so that for r=r_inner -> 0 and r=r_outer -> 1
        tc_polar.s = ( radius - r_inner) / (r_outer - r_inner);

        // map angle from [-PI,PI] to [0,1]
        tc_polar.t = angle * 0.5 / PI + 0.5;

        uv = tc_polar;
    }

	uv += vec2(mat_x_time,mat_y_time)*2.;
	float t = mat_animation_time;	
	float K = 0.;

    #if defined( TYPE_IS_Billowed )

         K = billowedNoise(vec3(uv ,t));

    #elif defined( TYPE_IS_Flow )

         K = flowNoise(uv,t);
    	 K = abs(K);

    #elif defined( TYPE_IS_Worley )

         K = 1.5*worleyNoise(vec3(uv,t));
    	 K = min(max(K,0.),1.);

    #elif defined( TYPE_IS_Curl )

         K = curlNoise(uv,t).y*0.5+0.5;
    	
    #elif defined( TYPE_IS_Simplex )

         K = noise(vec3(uv,t)) * 0.5f + 0.5f;	

    #elif defined( TYPE_IS_Ridged )

         K = ridgedNoise(vec3(uv,t));

    #elif defined( TYPE_IS_MF )

         K = ridgedMF(vec3(uv,t));
    	
    #elif defined( TYPE_IS_fBm )

         K = fBm(vec3(uv,t))*0.5+0.5;
    	
    #elif defined( TYPE_IS_Relief )

    	mat2 m = mat2( 1.6,  1.2, -1.2,  1.6 );
        float flow = sin(t*0.1+5.0)*1.0 + 3.;
    	uv *= 0.5;
        K  = 0.500 * ridgedMF( vec3(uv,t) ); uv = m*uv;
    	K += 0.250 * ridgedMF( uv +K*flow);  uv = m*uv;
    	K += 0.125 * ridgedMF( uv +K*flow);  uv = m*uv;
    	K *= 1.5;

    #elif defined( TYPE_IS_Combo )

    	float f = abs(flowNoise(uv + vec2(vnoise(uv),billowedNoise(uv)),t));   
    	mat2 m = mat2( 1.6,  1.2, -1.2,  1.6 );
        float flow = sin(t*0.1+5.0) + 4.;
    	uv *= 0.5;
        K  = 0.5000*worleyNoise( vec3(uv,f) ); uv = m*uv;
    	K += 0.2500*fBm( uv +K*flow + f); uv = m*uv;
    	K *= 2.5;

    #elif defined( TYPE_IS_TiledFBM )

    	K = tilableFbm( vec3(uv,t*0.3), 3, 10. );

    #elif defined( TYPE_IS_TiledVoronoi )
    	
    	K = tilableVoronoi( vec3(uv,t*0.1), 1, 10. );
     	K = min(max(K,0.),1.);

    #elif defined( TYPE_IS_Hash )
    	
    	K = valueHash(vec3(floor(uv*10.),t*0.1));
     	K = min(max(K,0.),1.);

    #endif

	K = clamp(K,0.,1.);

	float G = 0.;
    #if defined( GRADIENT_IS_Linear )
     //    G  = texture( texture_linear, vec2( K, 0.5f ) ).r;
    	 G = K;
    #elif defined( GRADIENT_IS_Perturbed )
         G  = texture( texture_noised, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_Reverse )
         G  = texture( texture_reverse, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_Camel )
         G  = texture( texture_camel, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_Multi )
         G  = texture( texture_multi, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_BW )
         G  = texture( texture_bw, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_HalfScale )
         G  = texture( texture_halfscale, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_Scales )
         G  = texture( texture_scales, vec2( K, 0.5f ) ).r;
    #elif defined( GRADIENT_IS_ScalesDense )
         G  = texture( texture_scales_dense, vec2( K, 0.5f ) ).r;
    #endif

	G = fract(G  + mat_cycle*0.9999);

    // Apply brightness
    G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0.,1.);

	if (mat_invert) G = 1 - G;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, G );
	color = mix(color,
				color * rotation(0.85*PI*2, vec3(-0.5+K,abs(cos(K)),abs(sin(K))))*2.5,
				mat_crot);

	vec4 final_color = vec4(color,1.0);	

	return final_color;
}
