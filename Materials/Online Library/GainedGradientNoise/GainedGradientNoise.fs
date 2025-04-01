/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "shadertoy 3sXcRj by raabix",
    "DESCRIPTION": "noisy rocky surface",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.01, "MAX": 1.0, "DEFAULT": 0.5 }, 
        { "LABEL": "Octaves", "NAME": "mat_octaves", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1. },
	 	{ "LABEL": "Power", "NAME": "mat_power", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 },	
		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.8 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/


#define DO_ROTATE         true  
#define BOXFILTER_SAMPLES 2.0
#define PPI                3.14159265359

// Hashes by Dave Hoskins
// https://www.shadertoy.com/view/4djSRW
//----------------------------------------------------------------------------------------
///  2 out, 2 in...
vec2 hash22(vec2 p)
{

    vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yzx+33.33);
    return fract((p3.xx+p3.yz)*p3.zy);
}


float gainf(float x, float k) 
{
    //remap k, so k is driven by 0...1 range
    k = k < 0.5 ? 2.*k : 1./(1.-(k-0.5)*2.);
    float a = 0.5*pow(2.0*((x<0.5)?x:1.0-x), k);
    return (x<0.5)?a:1.0-a;
}

// performs a golden-ratio rotation
vec2 rot_golden(vec2 pos,vec2 uv)
{
    // golden/ration in radians: 3.883222072739204862525004958380
    float sine   = -0.67549029078724183023998;
    float cosine = -0.73736888126104662389070;
    mat2 rot = mat2(cosine, -sine, sine, cosine);
    uv -= pos; 
    uv = rot * uv  ;
    return uv + pos;
}


float gradnoise_random(vec2 uv)
{
    vec2 p = floor(uv),
        f = (fract(uv));
    
    vec2 u = f*f*f*(6.0*f*f - 15.0*f +10.0);  // from Ken Perlin's improved noise
    //vec2 u = f*f*(3.-2.*f);                 // simpler formula (s-curve)
    float dot1, dot2, dot3, dot4;
    
    dot1 = dot(2.*(hash22(p)) - 1.0, f);
    dot2 = dot(2.*(hash22(p + vec2(1., 0.)))-1.0 , f - vec2(1.,0.));
    dot3 = dot(2.*(hash22(p + vec2(0., 1.)))-1.0, f - vec2(0.,1.));
    dot4 = dot(2.*(hash22(p + vec2(1., 1.)))-1.0, f - vec2(1.,1.));
   
    float m1, m2;
    
    m1 = mix(dot1, dot2, u.x);
    m2 = mix(dot3, dot4, u.x);
    
    return mix(m1, m2, u.y);
}

// This gradient noise makes sure gradients lie on the unit-circle and it 
float gradnoise_circular(vec2 uv, float power)
{
    vec2 p = floor(uv),
        f = (fract(uv));
    
    vec2 u = f*f*f*(6.0*f*f - 15.0*f +10.0);
    //vec2 u = f*f*(3.-2.*f);
    float dot1, dot2, dot3, dot4;
    
    vec2 hash00, hash10, hash01, hash11;
    vec2 grad00, grad10, grad01, grad11;
  
    hash00 = hash22(p);
    hash01 = hash22(p + vec2(0., 1.));
    hash10 = hash22(p + vec2(1., 0.));
    hash11 = hash22(p + vec2(1., 1.));

    // Calculate gradients. The sin and cos part makes sure that the gradient vectors are
    // unit length. Since the hash values range from 0...1 we need to bring them in the
    // range of 2 PI which describes a whole circle, so gradient vectors point in any
    // possible direction.
    // The pow function at the end is what makes the intensities of the gradients more
    // uneven, so we get a more irregular pattern of the noise. If the 'power' value gets 
    // too high, the noise starts looking bad though.
    // One could
 	//Gradients shall lie on the unit circle
    grad00 = vec2(sin(hash00.x * PPI * 2. + mat_animation_time), cos(hash00.x * PPI * 2. + mat_animation_time)) * (pow(hash00.y, power));
    grad01 = vec2(sin(hash01.x * PPI * 2. + mat_animation_time), cos(hash01.x * PPI * 2. + mat_animation_time)) * (pow(hash01.y, power));
    grad10 = vec2(sin(hash10.x * PPI * 2. + mat_animation_time), cos(hash10.x * PPI * 2. + mat_animation_time)) * (pow(hash10.y, power));
    grad11 = vec2(sin(hash11.x * PPI * 2. + mat_animation_time), cos(hash11.x * PPI * 2. + mat_animation_time)) * (pow(hash11.y, power));
    
    dot1 = dot(grad00, f);
    dot2 = dot(grad10, f - vec2(1.,0.));
    dot3 = dot(grad01, f - vec2(0.,1.));
    dot4 = dot(grad11, f - vec2(1.,1.));
  
  
    float m1, m2;
    
    m1 = mix(dot1, dot2, u.x);
    m2 = mix(dot3, dot4, u.x);
   
   // return abs(box_mueller_transform(hash00)).x;
    return mix(m1, m2, u.y);
}

vec2 calc_pos_width(float pos, float width)
{
    float low, high;
    
    low = pos - width/2.0;
    high = pos + width/2.0;
    return clamp(vec2(low, high), vec2(0.0), vec2(1.0));
}

float oct_gained_gradnoise(vec2 uv, float octaves, float roughness, float octscale, float power)
{
    float a = 0.;
    float intensity = 1.;
    float oct_intensity = 1.0; // first intensity is 1.0, successive octaves get scaled by roughness value
    float total_scale = 0.0;
    float remap;

    float contrast = 0.999;
    float gain = .00111;

    for(float i = 0.; i < octaves; i++)
    {
        float gradnoise_lookup = gradnoise_circular(uv, power)*1.75;
        
        remap = gradnoise_lookup;
        remap = gainf((gradnoise_lookup*.5 + .5), gain);
        remap = ((remap - 0.5) * 2.); 
        
		total_scale += oct_intensity;
        a += remap* oct_intensity;
        oct_intensity*= roughness; // get intensity for current octave
        
        if(DO_ROTATE)
        	uv = rot_golden(vec2(11.1231, 11.1231), uv) * octscale;
        else
        	uv = uv * octscale;
    }
    // applying contrast and clamping it
	
    a = (a/(1.0 -contrast)) ;
    a= clamp(a/total_scale, -1., 1.0);
    return a; // if we were dividing just by octaves we'd get an intensity shift
}	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	uv = uv*2.-1.;
	// modify uv with material inputs
	uv *= mat_scale*0.1;
	uv += vec2(-mat_offset.x,mat_offset.y)*0.1;
	
    int octaves = int(ceil(mat_octaves * 10.));
    float power = mat_power * 6.+.1;
    float roughness = 1.1; //uvMouse.y*1.5;
    
    
    // Static gradnoise
    float octscale = 2.0; //factor by which each octave gets smaller than the previous one
    float final_value;
    
    final_value = oct_gained_gradnoise(uv * 2., float(octaves), roughness, octscale, power);
    
    final_value = final_value *0.5+0.5;
	
	// make a color out of it
	vec3 color = vec3(final_value);

	
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
