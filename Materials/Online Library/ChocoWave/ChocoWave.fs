/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Shadertoy ttl3Rj\nby  MrsBeanbag",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.5 }, 
		{ "LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.1, "MAX": 4.0, "DEFAULT": 2.0 }, 

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"	

//uncomment this line to get mouse control rofl
//#define MOUSE_CONTROL

// ray marching
const int max_iterations = 16;
const float grad_step = 0.0001;
const float min_dist = 0.002;
const float clip_far = 1000.0;

const float DEG_TO_RAD = PI / 180.0;

vec3 diffuse_colour = vec3(0.0);
vec3 final_diff_colour = vec3(0.0);

const vec3 sky_colour = vec3(0.2, 0.1, 0.4);
const vec3 clouds_colour = vec3(1.0);
const vec3 sun_colour = 32.0 * vec3(1.0, 1.0, 0.0);
const vec3 sun_pos = normalize(vec3( 20.0, 30.0, 30.0 ));

//get a scalar random value from a 3d value
float rand2dTo1d(vec2 value, vec2 dotDir) {
    //make value smaller to avoid artefacts
    vec2 smallValue = sin(value);
    //get scalar value from 3d vector
    float random = dot(smallValue, dotDir);
    //make value more random by making it bigger and then taking the factional part
    random = fract(sin(random) * 143758.5453);
    return random;
}

vec3 rand2dTo3d(vec2 value){
    return vec3(
        rand2dTo1d(value, vec2(12.989, 78.233)),
        rand2dTo1d(value, vec2(39.346, 11.135)),
        rand2dTo1d(value, vec2(73.156, 52.235))
    );
}

// this isn't very random but it's a good deal quicker than the above
vec2 random2( vec2 p ) {
    return fract(p * vec2(9.61, 3.28) - p.yx * vec2(5.85, 7.11));
}

float smax( float a, float b, float k ) {
    return -smin(-a, -b, k);
}

vec2 cell_position(vec2 cell) {
    vec2 point = random2(cell);
    point = 0.1 + 0.8*fract(6.2831*point);
    return cell + point;
}

float cell_height(vec2 cell) {
    return 1.0 + cos(dot(cell, vec2(0.3, 0.2)) + mat_animation_time);
	//return 2.0*rand2dTo1d(cell, vec2(52.235, 09.151));
}

float dist_field(vec3 p) {
    float md = 1000.0;
    vec2 mcell = vec2(0.0);
    vec2 mpos = vec2(0.0);
    vec2 fuv = floor(p.xz);
    for (int i = -1; i <= 1; ++i) {
	    for (int j = -1; j <= 1; ++j) {
            vec2 cell = fuv + vec2(float(i), float(j));
            vec2 pos = cell_position(cell);
    		vec2 mid = p.xz - pos;
            float d = dot(mid, mid);
            if (d < md) {
                md = d;
                mcell = cell;
                mpos = pos;
            }
        }
    }
    
    float mheight = cell_height(mcell);
    float plane = p.y;
    float outer = p.y + 2.1;
    for (int i = -1; i <= 1; ++i) {
	    for (int j = -1; j <= 1; ++j) {
            vec2 cell = fuv + vec2(float(i), float(j));
            if (cell != mcell) {
	            vec2 pos = cell_position(cell);
			    vec2 n = normalize(mpos - pos);
			    vec2 pt = (mpos + pos) * 0.5;
           	    float boundary = dot(n, p.xz - pt);
                float height = cell_height(cell);
	            float wall = smax(boundary, p.y + height, 0.1);
                outer = smin(outer, wall, 0.1);
			    plane = smax(plane, -boundary, 0.1);
            }
        }
    }
    if (outer >= 0.0) {
        plane = smax(plane, p.y + mheight, 0.1);
        plane = smin(plane, outer, 0.1);
    }
    diffuse_colour = vec3(0.9, 0.4, 0.1);
    return plane;
}

vec3 skybox(vec3 dir) {
    float horizon = smoothstep(0.0, 0.25, dir.y);
    float cloud = mix(0.8, 0.0, horizon);
    return mix(sky_colour, clouds_colour, cloud);
}

// get gradient in the world
vec3 gradient( vec3 p ) {
    const vec2 k = vec2(1,-1);
    const vec2 kg = vec2(grad_step,-grad_step);
    return vec3( k.xyy*dist_field( p + kg.xyy) +
                 k.yyx*dist_field( p + kg.yyx) +
                 k.yxy*dist_field( p + kg.yxy) +
                 k.xxx*dist_field( p + kg.xxx) );
}

// parallel light source
vec3 shading( vec3 v, vec3 n, vec3 dir, vec3 eye ) {
	float diffuse  = max( 0.0, dot( sun_pos, n ) );
	float shininess = 80.0;
	
	vec3 ref = reflect( dir, n );
	float specular = max( 0.0, dot( sun_pos, ref ) );
		
	specular = pow( specular, shininess );
		
	return final_diff_colour * diffuse + vec3(specular);
}

// ray marching
vec3 ray_marching(vec3 o, vec3 dir) {
    vec3 colour = vec3(0.0);
    vec3 acc_refl = vec3(1.0);

    // give the ray a bit of a head start by jumping
    // straight to the upper bounding plane!
    float t = 0.0;
    if (dir.y < 0.0) {
        t = -o.y/dir.y;
    } else {
        return skybox(dir);
    }
	t = max(t, min_dist*2.0);

    vec3 v;
    for (int i = 0; i < max_iterations; i++) {
       	float d = dist_field(v = o + dir * t);

        if (d <= min_dist) {
            break;
        }
		if ((t+=d) >= clip_far) {
            return skybox(dir);
	    }
    }
	final_diff_colour = diffuse_colour;
    
    vec3 n = normalize( gradient(v) );
    if (dot(n, sun_pos) < 0.0) {
        return colour;
    }

    // shadows
    t = 0.1;
    for (int i = 0; i < max_iterations; i++) {
        vec3 r = v + sun_pos * t;
		if (r.y > 0.0) {
		    colour += shading( v, n, dir, o );
            break;
	    }

        float d = dist_field(r);
        if (d <= min_dist) {
            break;
        }
        t+=d;
    }
    return colour;
}

// get ray direction
vec3 ray_dir( float fov, vec2 size, vec2 pos ) {
	vec2 xy = pos - size * 0.5;

	float cot_half_fov = tan( ( 90.0 - fov * 0.5 ) * DEG_TO_RAD );
	float z = size.y * 0.5 * cot_half_fov;

	return normalize( vec3( xy, -z ) );
}

// camera rotation : pitch, yaw
mat3 rotationXY( vec2 angle ) {
	vec2 c = cos( angle );
	vec2 s = sin( angle );

	return mat3(
		c.y      ,  0.0, -s.y,
		s.y * s.x,  c.x,  c.y * s.x,
		s.y * c.x, -s.x,  c.y * c.x
	);
}
const vec2 spin = vec2(0.005, 0.0041);
const float radius = 120.0;

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;
	// modify uv with material inputs
	uv *= mat_scale;
	uv += vec2(-1.);
	uv *= vec2(1.,-1.);


mat3 rot = rotationXY( vec2( -0.8, mat_animation_time * 0.2 ) );	
	// make a color out of it
	vec3 color = vec3(1.);
	
	// default ray origin
	vec3 eye = vec3( 0.0, 1.0, 0.0 );
	eye = rot * eye;
    
    // move in a sinusoidal path so we don't end up in floating point hell!
    vec2 camera = radius * cos(mat_animation_time * spin);
    
    eye += vec3(camera, 1.0).xzy;

    // default ray dir
	vec3 dir = rot * ray_dir( 60.0, vec2(1024.), 1024.*uv);
	color = ray_marching(eye, dir);

	
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
