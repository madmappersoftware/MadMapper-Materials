/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "frz /1024 architecture",
    "DESCRIPTION": "Living concrete",
    "TAGS": "texture",
    "VSN": "1.0",
    "INPUTS": [ 

		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 }, 
		{ "LABEL": "Precision", "NAME": "mat_precision", "TYPE": "int", "MIN": 0, "MAX": 64, "DEFAULT": 8 },
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 0.1 },
        { "LABEL": "Separation", "NAME": "mat_separation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Alive", "NAME": "mat_alive", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },

		{ "LABEL": "Noise/Noise Power", "NAME": "mat_npower", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Noise/Noise Scale", "NAME": "mat_nscale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 1.0 },
		{ "LABEL": "Noise/Noise Advance", "NAME": "mat_nspeed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },  

		{ "LABEL": "Light/Light Position", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ], "DEFAULT": [ 0.8, 0.8 ] },
		{ "LABEL": "Light/Add Normals", "NAME": "mat_normal", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },

		{ "LABEL": "ZColor/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "ZColor/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "ZColor/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "ZColor/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
		{"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_nspeed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"


//-------------------------------------------------
float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}

//---------------------------------
float smax(float a, float b, float k)
{
    return log(exp(k * a) + exp(k * b)) / k;
}

float smin2(float a, float b, float k)
{
    return -log(exp(-k * a) + exp(-k * b)) / k;
}

float map(in vec3 pos)
{
    float d = 1e10;
      
	float D = mat_separation*4.;
	float s = mat_scale;
    vec3 q = pos;

    d = min(d,sdSphere(q-vec3(D,0,0.),s + mat_alive*sin(mat_animation_time*0.2)));
	d = smin2(d,sdSphere(q+vec3(D,0.,0.),s + mat_alive*sin(mat_animation_time*0.2)),0.75);

    float n = billowedNoise(
					mat_nscale*(pos+ vec3(0.,0.,0.))*(1. + mat_alive*sin(mat_animation_time)*0.62)*0.2 
				  + fBm( (q+ vec3(0.,0.,-mat_noise_time*0.2))*2.*(0.5 + mat_nscale*0.5) +mat_animation_time*0.1));
    
	d += n*0.025*mat_npower;
      
    return d;
}

// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
vec3 calcNormal( in vec3 pos )
{
    const float ep = 0.0001;
    vec2 e = vec2(1.0,-1.0)*0.5773;
    return normalize( e.xyy*map( pos + e.xyy*ep ) + 
					  e.yyx*map( pos + e.yyx*ep ) + 
					  e.yxy*map( pos + e.yxy*ep ) + 
					  e.xxx*map( pos + e.xxx*ep ) );
}

#define AA 1
const vec3 ZERO3 = vec3(0.);

vec3 hsl2rgb(vec3 c)
{
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// make a color 
	vec3 color = ZERO3;
  	vec3 tot   = ZERO3;
    vec3 norm  = ZERO3;
    
    #if AA>1
    for( int m=0; m<AA; m++ )
    for( int n=0; n<AA; n++ )
    {
        // pixel coordinates
        vec2 o = vec2(float(m),float(n)) / float(AA) - 0.5;
        vec2 p = -1 + uv*2.+o*0.005;
        #else    
        vec2 p = vec2(-1.)+uv*2.;
        #endif
 
        vec3 ro = vec3(0.0,3.0,6.0);
        vec3 rd = normalize(vec3(p-vec2(0.0,1.0),-2.0));

        float t = 5.0;
        for( int i=0; i<mat_precision; i++ )
        {
            vec3 p = ro + t*rd;
            float h = map(p);
            if( abs(h)<0.01 || t>6.0 ) break;
            t += h;
        }

        vec3 col = vec3(0.0);

        if( t<10.0 )
        {
            vec3 pos = ro + t*rd;
            vec3 nor = calcNormal(pos);
            float dif = clamp(dot(nor,(vec3(mat_offset,0.))),0.0,1.0);
            col = vec3(0.025,0.05,0.08) + dif*vec3(1.0,0.9,0.8);
            norm = nor;
        }

        col = sqrt( col );
	    tot += col;

    #if AA>1
    }
    tot /= float(AA*AA);
    #endif
    
	color = tot;
	color = mix(color,(norm)*tot*2,mat_normal*0.3);

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
