/*{
    "CREDIT": "1024 architecture\nFranz",
    "DESCRIPTION": "Raytraced Heightfield - Replace by your own heighfield or grayscale texture",
    "TAGS": "heightfield",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 },
		{ "LABEL": "Texture Mix", "NAME": "mat_mix", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Height", "NAME": "mat_height", "TYPE": "float", "MIN": -1., "MAX": 1.0, "DEFAULT": -1.0 },
		{ "LABEL": "Zoom", "NAME": "mat_zoom", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Bump", "NAME": "mat_bump", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Bump Power", "NAME": "mat_bump_power", "TYPE": "float", "MIN": 0., "MAX": 1.0, "DEFAULT": 0.0 }, 
        
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 2.0, 2.0 ], "MIN": [ -2.0, -2.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Light/Light Pos", "NAME": "mat_light", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Light/Light Pos Z", "NAME": "mat_light_z", "TYPE": "float", "MIN": -2, "MAX": 2.0, "DEFAULT": 0.0 }, 

		{ "LABEL": "Light/Specualar", "NAME": "mat_specular", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. },
		{ "LABEL": "Light/Fresnel", "NAME": "mat_fresnel", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. },
		{ "LABEL": "Light/Attenuation", "NAME": "mat_attenuation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. },
		{ "LABEL": "Light/Diff Color", "NAME": "mat_diff_color", "TYPE": "color", "DEFAULT": [ 0.75, 0.72, 0.7, 1.0 ] },
		{ "LABEL": "Light/Spec Color", "NAME": "mat_spec_color", "TYPE": "color", "DEFAULT": [ 0.90, 0.92, 0.92, 1.0 ] },
		{ "LABEL": "Light/Fresnel Color", "NAME": "mat_fre_color", "TYPE": "color", "DEFAULT": [ 0.92, 0.95, 0.9, 1.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
	  "IMPORTED": [
        {"NAME": "mat_tex_1", "PATH": "u.jpg", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
		{"NAME": "mat_tex_2", "PATH": "peebles.png", "GL_TEXTURE_MIN_FILTER": "LINEAR", "GL_TEXTURE_MAG_FILTER": "LINEAR", "GL_TEXTURE_WRAP": "REPEAT"},
    ]
}*/

#include "MadCommon.glsl"
	
const float PRECISION  = 0.001;
const float MAX_DISTANCE = 20.;

// Infinite zoom from https://www.shadertoy.com/view/ltjXWW
float zoom(vec3 p, sampler2D stex)
{
	float o = 0.;
	float s;

    const float L = 5.; // Layer number.
	for (float i = 0.; i<L; i++){
	
        // Fractional time component.
		s = fract((i - mat_time*2.)/L);
        
        // Two nearby texture samples.
        float expS = exp2(s*L)/8.;
        o += texture(stex, p.xy*expS + i*.9).x * (.5 - abs(s-.5))*8./L;
	}

	return o;
}

// SDF scene
float map(vec3 p){

	float bump = sin(length(p.xy*4.)+mat_bump*PI*2.);
	float tex = mix(
					mix(texture(mat_tex_1, p.xy).x, pow(zoom(p, mat_tex_1),1.4),mat_zoom),
			  		mix(texture(mat_tex_2, p.xy).x, pow(zoom(p, mat_tex_2),1.4),mat_zoom),
				mat_mix);

	return 1. - p.z - tex*mat_height*0.1 - bump*0.1*mat_bump_power;
 
}

// Raymarcher
float intersect( in vec3 ro, in vec3 rd )
{
	float h= PRECISION * 2.0;
    float t = 0.0;
	float maxd = MAX_DISTANCE;
    float sid = -1.0;
    for( int i=0; i<10; i++ )
    {
        if( abs(h)<PRECISION||t>maxd ) break;
        t += h;
	    h = map( ro+rd*t );
    }
    return t;
}

vec3 calcNormal( in vec3 pos )
{
    vec3  eps = vec3(PRECISION,0.0,0.0);
    vec3 nor;
    nor.x = map(pos+eps.xyy) - map(pos-eps.xyy);
    nor.y = map(pos+eps.yxy) - map(pos-eps.yxy);
    nor.z = map(pos+eps.yyx) - map(pos-eps.yyx);
    return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;
    float sca = 1.0;
    for( int i=0; i<5; i++ )
    {
        float hr = 0.01 + 0.12*float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos );
        occ += -(dd-hr)*sca;
        sca *= 0.95;
    }
    return clamp( 1.0 - 3.0*occ, 0.0, 1.0 );    
}

float sss(vec3 p, vec3 l , float d){
	return smoothstep(0.,1.,map(p+l*d)/d);
} 

// Cheap curvature: https://www.shadertoy.com/view/Xts3WM
float curve(in vec3 p){
    const float eps = 0.0225, amp = 7.5, ampInit = 0.525;

    vec2 e = vec2(-1., 1.)*eps;
    
    float t1 = map(p + e.yxx), t2 = map(p + e.xxy);
    float t3 = map(p + e.xyx), t4 = map(p + e.yyy);
    
    return clamp((t1 + t2 + t3 + t4 - 4.*map(p))*amp + ampInit, 0., 1.);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord - 0.5;
	uv *= mat_scale;
	uv += vec2(-mat_offset.x, mat_offset.y);
	
	vec3 color;

	vec3 rd = normalize(vec3(uv, 1.));
    
    // Ray origin. Moving in the X-direction to the right.
    vec3 ro = vec3(0.);  
    
	// raymarch to get distance to scene (surf.x) and material ID (surf.y)
    float surf = intersect(ro,rd);

  	// geometry
    vec3 pos = ro + surf * rd;
    vec3 nor = calcNormal(pos);

    // light 
    vec3 lp = ro + vec3(mat_light.x, -mat_light.y, mat_light_z);
    vec3 ld = (lp - pos);
    
    float lDist = max(length(ld), 0.001); 							
    float atten = mix(1.,1./(1. + lDist * 0.25), mat_attenuation); 	

    ld /= lDist; // Normalizing the light direction vector.
    
    float diff = max(dot(ld, nor), 0.); 
    float spec = pow(max( dot( reflect(-ld, nor), -rd ), 0.0 ), 32.) * mat_specular; 
    float fre = clamp(dot(nor, rd) + 1., .0, 1.) * mat_fresnel; 

    color = (mat_diff_color.xyz * (diff + .5) + mat_spec_color.xyz * spec * 2.) + mat_fre_color.xyz * pow(fre, 3.)*5.;
    
	// Ambiant occlusion
	float ao = calcAO(pos, nor);

    float crv = curve(pos); // Curve value, to darken the crevices.
    crv = smoothstep(0., 1., crv) * .5 + crv * .25 + .25; // Tweaking the curve value a bit.
    
	color *= (atten * crv * ao);

	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);

    // Apply brightness
    color += vec3(mat_brightness);

	// Apply Tint
	color *= mat_tint.rgb;

	// Apply Invert
	color = mix(color, vec3(1.0) - color, float(mat_invert));
		
	return vec4(color,1.0);
}